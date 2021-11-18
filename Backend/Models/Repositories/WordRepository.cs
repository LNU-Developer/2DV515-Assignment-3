using Backend.Models.Database;
namespace Backend.Models.Repositories
{
    public class WordRepository : Repository<Context, Word>, IWordRepository
    {
        public WordRepository(Context context) : base(context)
        {
        }
    }
}