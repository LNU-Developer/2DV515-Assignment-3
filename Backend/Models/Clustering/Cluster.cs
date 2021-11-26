using Backend.Models.Database;
namespace Backend.Models.Clustering
{
    public class Cluster
    {
        public Cluster Left { get; set; }
        public Cluster Right { get; set; }
        public Cluster Parent { get; set; }
        public Blog Blog { get; set; }
        public double Distance { get; set; }
    }
}