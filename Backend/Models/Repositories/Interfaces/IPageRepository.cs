using Backend.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Backend.Models.Repositories
{
    public interface IPageRepository : IRepository<Page>
    {
        Task<List<Page>> GetAllPagesWithWords();
        IQueryable<Page> GetAllPagesWithWordsQuery();
    }
}
