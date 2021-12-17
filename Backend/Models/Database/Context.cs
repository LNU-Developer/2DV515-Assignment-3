using Microsoft.EntityFrameworkCore;
#nullable disable

namespace Backend.Models.Database
{
    public partial class Context : DbContext
    {
        public string DbPath { get; private set; }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PageWord> PageWords { get; set; }
        public virtual DbSet<WordMap> WordMaps { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}