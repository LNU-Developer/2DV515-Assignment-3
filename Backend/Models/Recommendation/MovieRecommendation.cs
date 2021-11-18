namespace Backend.Models.Recommendation
{
    public class MovieRecommendation : IMovieRecommendation
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int MovieYear { get; set; }
        public double AverageWeightedRating { get; set; }
    }
}