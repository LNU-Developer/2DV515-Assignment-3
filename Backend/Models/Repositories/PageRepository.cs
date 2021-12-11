using Backend.Models.Database;
namespace Backend.Models.Repositories
{
    public class PageRepository : Repository<Context, Page>, IPageRepository
    {
        public PageRepository(Context context) : base(context)
        {
        }
    }
}