using System;
using System.Threading.Tasks;

namespace Backend.Models.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IMovieRepository Movies { get; }
        IRatingRepository Ratings { get; }
        IWordRepository Words { get; }
        IBlogRepository Blogs { get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}