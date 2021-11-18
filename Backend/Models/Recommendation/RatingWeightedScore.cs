namespace Backend.Models.Recommendation
{
    public class RatingWeightedScore : IRatingWeightedScore
    {
        public int MovieId { get; set; }
        public double WeightedScore { get; set; }
    }
}