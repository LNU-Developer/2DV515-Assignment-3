using Backend.Models.Database;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace Backend.Models.Repositories
{
    public interface IWordRepository : IRepository<Word>
    {
        Task<List<Word>> GetAllWordsWithWordReferences();
    }

}
