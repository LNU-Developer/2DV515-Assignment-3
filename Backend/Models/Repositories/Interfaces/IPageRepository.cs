using Backend.Models.Database;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace Backend.Models.Repositories
{
    public interface IPageRepository : IRepository<Page>
    {
        Task<List<Page>> GetAllPagesWithWords();
        IQueryable<Page> GetAllPagesWithWordsQuery();
    }
}
