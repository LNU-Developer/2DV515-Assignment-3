using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Database;
using Microsoft.EntityFrameworkCore;
namespace Backend.Models.Repositories
{
    public class WordRepository : Repository<Context, Word>, IWordRepository
    {
        public WordRepository(Context context) : base(context)
        {
        }

        public async Task<List<Word>> GetDistinctWords()
        {
            var distinctWordList = new List<Word>();
            var wordList = await _context.Words.ToListAsync();
            foreach (var word in wordList)
            {
                if (!distinctWordList.Any(x => x.WordTitle == word.WordTitle))
                    distinctWordList.Add(new Word
                    {
                        WordTitle = word.WordTitle,
                        Min = word.Min,
                        Max = word.Max
                    });
            }
            return distinctWordList;
        }
    }
}