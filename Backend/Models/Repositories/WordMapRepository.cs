using Backend.Models.Database;
namespace Backend.Models.Repositories
{
    public class WordMapRepository : Repository<Context, WordMap>, IWordMapRepository
    {
        public WordMapRepository(Context context) : base(context)
        {
        }
    }
}