using System.Collections.Generic;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
namespace Backend.Models.Repositories
{
    public class PageRepository : Repository<Context, Page>, IPageRepository
    {
        public PageRepository(Context context) : base(context)
        {
        }

        public async Task<List<Page>> GetAllPagesWithWords()
        {
            return await _context.Pages.Include(x => x.PageWords).AsNoTracking().ToListAsync();
        }

        public IQueryable<Page> GetAllPagesWithWordsQuery()
        {
            return _context.Pages.Include(x => x.PageWords).AsNoTracking();
        }
    }
}