using Backend.Models.Database;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace Backend.Models.Repositories
{
    public class WordRepository : Repository<Context, Word>, IWordRepository
    {
        public WordRepository(Context context) : base(context)
        {

        }
        public async Task<List<Word>> GetAllWordsWithWordReferences()
        {
            return await _context.Words.Include(x => x.WordReferences).ToListAsync();
        }
    }
}