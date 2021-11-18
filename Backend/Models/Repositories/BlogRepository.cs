using Backend.Models.Database;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace Backend.Models.Repositories
{
    public class BlogRepository : Repository<Context, Blog>, IBlogRepository
    {
        public BlogRepository(Context context) : base(context)
        {
        }
        public async Task<List<Blog>> GetAllBlogsWithData()
        {
            return await _context.Blogs.Include(x => x.WordReferences).ThenInclude(y => y.Word).ToListAsync();
        }
    }
}