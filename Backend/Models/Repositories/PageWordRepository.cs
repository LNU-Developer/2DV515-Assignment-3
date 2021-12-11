using Backend.Models.Database;
namespace Backend.Models.Repositories
{
    public class PageWordRepository : Repository<Context, PageWord>, IPageWordRepository
    {
        public PageWordRepository(Context context) : base(context)
        {
        }
    }
}