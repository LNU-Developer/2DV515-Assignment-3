using Backend.Models.Database;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
namespace Backend.Models.Repositories
{
    public class BlogRepository : Repository<Context, Blog>, IBlogRepository
    {
        public BlogRepository(Context context) : base(context)
        {
        }
        public async Task<List<Blog>> GetAllBlogsWithData()
        {
            return await _context.Blogs.Include(y => y.Words.OrderBy(x => x.WordTitle)).ToListAsync();
        }
        public IIncludableQueryable<Blog, ICollection<Word>> GetAllBlogsWithDataReference()
        {
            return _context.Blogs.Include(x => x.Words);
        }
    }
}