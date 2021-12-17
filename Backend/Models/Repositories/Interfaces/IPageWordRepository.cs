using Backend.Models.Database;
using System.Threading.Tasks;
namespace Backend.Models.Repositories
{
    public interface IPageWordRepository : IRepository<PageWord>
    {
        Task<int> GetWordCountAsync(Page page, int[] words);
    }
}
