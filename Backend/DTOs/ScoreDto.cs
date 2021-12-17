namespace Backend.DTOs
{
    public class ScoreDto
    {
        public PageDto Page { get; set; }
        public double Content { get; set; }
        public double Location { get; set; }
        public double Distance { get; set; }
        public double TotalScore { get; set; }
    }
}