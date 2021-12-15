namespace Backend.Models.Search
{
    public class Metric
    {
        public Metric(int pages)
        {
            Content = new double[pages];
            Location = new double[pages];
            Distance = new double[pages];
        }
        public double[] Content { get; set; }
        public double[] Location { get; set; }
        public double[] Distance { get; set; }
    }
}