using Backend.Models.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend.Models.Repositories
{
    public class PageWordRepository : Repository<Context, PageWord>, IPageWordRepository
    {
        public PageWordRepository(Context context) : base(context)
        {
        }

        public async Task<int> GetWordCountAsync(Page page, int[] words)
        {
            return await _context.PageWords.Where(z => z.PageId == page.PageId && words.Contains(z.WordHashMapId)).AsNoTracking().CountAsync();
        }
    }
}