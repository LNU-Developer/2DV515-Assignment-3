using Backend.Models.Database;
using System.Threading.Tasks;

namespace Backend.Models.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public UnitOfWork(Context context)
        {
            _context = context;
            Movies = new MovieRepository(_context);
            Ratings = new RatingRepository(_context);
            Users = new UserRepository(_context);
            Words = new WordRepository(_context);
            Blogs = new BlogRepository(_context);
            Pages = new PageRepository(_context);
            PageWords = new PageWordRepository(_context);
            WordMaps = new WordMapRepository(_context);
        }

        public IMovieRepository Movies { get; private set; }
        public IRatingRepository Ratings { get; private set; }
        public IUserRepository Users { get; private set; }
        public IBlogRepository Blogs { get; private set; }
        public IWordRepository Words { get; private set; }
        public IPageRepository Pages { get; private set; }
        public IPageWordRepository PageWords { get; private set; }
        public IWordMapRepository WordMaps { get; private set; }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}