using Backend.Models.Database;
namespace Backend.Models.Repositories
{
    public class BlogRepository : Repository<Context, Blog>, IBlogRepository
    {
        public BlogRepository(Context context) : base(context)
        {
        }
    }
}