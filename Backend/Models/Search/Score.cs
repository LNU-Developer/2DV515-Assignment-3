using Backend.Models.Database;
namespace Backend.Models.Search
{
    public class Score
    {
        public Page Page { get; set; }
        public double Content { get; set; }
        public double Location { get; set; }
        public double Distance { get; set; }
    }
}