using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Backend.Models.Services;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using TinyCsvParser;
using Backend.Models.CsvMappings;
using System.Text;
using System.Linq;
using Backend.Models.Repositories;
using System.IO;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "DataSource=recommendationdb;mode=memory;cache=shared";      // Please note that the in memory SQL is not thread-safe, as such it is only used here to showcase the different ML tools using this database.
            var keepAliveConnection = new SqliteConnection(connectionString);
            keepAliveConnection.Open();
            services.AddDbContext<Context>(optionsBuilder =>
            {
                optionsBuilder.UseSqlite(keepAliveConnection);
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<EuclideanDistanceService>();
            services.AddTransient<PearsonCorrelationService>();
            services.AddTransient<ItemBasedCollaborativeFilteringService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<KmeansService>();
            services.AddTransient<HierarchicalService>();

            services.AddCors(options =>
            {
                options.AddPolicy("BackendCors",
                      builder =>
                      {
                          builder.WithOrigins("*")
                                              .AllowAnyHeader()
                                              .AllowAnyMethod();
                      });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
               });
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Migrating database if it doesn't exist
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<Context>())
                {
                    context.Database.EnsureCreated();
                    AddRecommendationData(context);
                    AddClusterData(context);
                }

            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1");
                    c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
                });
            }

            app.UseCors("BackendCors");

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddClusterData(Context context)
        {
            using (var reader = new StreamReader("SeedData/blogdata.txt"))
            {
                string firstLine = reader.ReadLine();
                string[] unstructuredWords = firstLine.Split('\t');

                var words = new List<Word>();

                for (int i = 1; i < unstructuredWords.Length; i++) //Index 0 in this case is the column title Blogs, and not applicable for this dataset
                    words.Add(new Word { WordTitle = unstructuredWords[i] });

                var rowList = new List<string>();
                while (!reader.EndOfStream)
                    rowList.Add(reader.ReadLine());

                foreach (var row in rowList)
                {
                    var values = row.Split('\t');
                    //Figure out max and min of a specific word
                    for (int i = 1; i < values.Length; i++)
                    {

                        var count = Int32.Parse(values[i]);
                        var max = words[i - 1].Max;
                        var min = words[i - 1].Min;
                        if (max < count) words[i - 1].Max = count;
                        if (min > count) words[i - 1].Min = count;
                    }
                }

                foreach (var row in rowList)
                {
                    var values = row.Split('\t');

                    var blog = new Blog();
                    blog.BlogTitle = values[0];

                    for (int i = 1; i < values.Length; i++)
                    {
                        var count = Int32.Parse(values[i]);

                        var word = new Word
                        {
                            WordTitle = words[i - 1].WordTitle,
                            Amount = count,
                            Min = words[i - 1].Min,
                            Max = words[i - 1].Max,
                        };
                        blog.Words.Add(word);

                    }
                    context.Blogs.Add(blog);
                }
                context.SaveChanges();
            }
        }

        private static void AddRecommendationData(Context context)
        {
            var parserOptions = new CsvParserOptions(skipHeader: true, fieldsSeparator: ';');

            var userParser = new CsvParser<User>(parserOptions, new UserCsvMapping());
            var movieParser = new CsvParser<Movie>(parserOptions, new MovieCsvMapping());
            var ratingParser = new CsvParser<Rating>(parserOptions, new RatingCsvMapping());

            var users = userParser.ReadFromFile("SeedData/users.csv", Encoding.UTF8).ToList();
            var movies = movieParser.ReadFromFile("SeedData/movies.csv", Encoding.UTF8).ToList();
            var ratings = ratingParser.ReadFromFile("SeedData/ratings.csv", Encoding.UTF8).ToList();

            foreach (var user in users)
                context.Users.Add(user.Result);

            foreach (var movie in movies)
                context.Movies.Add(movie.Result);

            foreach (var rating in ratings)
                context.Ratings.Add(rating.Result);

            context.SaveChanges();
        }
    }
}
