using Backend.Models.Database;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;
namespace Backend.Models.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<List<Blog>> GetAllBlogsWithData();
        IIncludableQueryable<Blog, Word> GetAllBlogsWithDataReference();
    }

}
