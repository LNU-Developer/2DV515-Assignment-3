using Backend.Models.Database;
namespace Backend.Models.Repositories
{
    public class WordReferenceRepository : Repository<Context, WordReference>, IWordReferenceRepository
    {
        public WordReferenceRepository(Context context) : base(context)
        {
        }
    }
}