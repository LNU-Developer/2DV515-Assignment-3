using Backend.Models.Database;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace Backend.Models.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<List<Blog>> GetAllBlogsWithData();
    }

}
