using Backend.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Backend.Models.Repositories
{
    public interface IWordRepository : IRepository<Word>
    {
        Task<List<Word>> GetDistinctWords();
    }
}
