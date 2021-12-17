using Backend.Models.Database;
namespace Backend.Models.Search
{
    public class Score
    {
        public Score(Page page, double content, double location)
        {
            Page = page;
            Content = content;
            Location = 0.8 * location;
            TotalScore = 1 * content + 0.8 * location;
        }
        public Page Page { get; set; }
        public double Content { get; set; }
        public double Location { get; set; }
        public double Distance { get; set; }
        public double TotalScore { get; set; }
    }
}