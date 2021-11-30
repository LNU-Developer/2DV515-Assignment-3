using Backend.Models.Database;
using System.Collections.Generic;
namespace Backend.Models.Clustering
{
    public class Centroid
    {
        public List<WordDto> Words { get; set; }
        public List<Blog> Assignments { get; set; }
    }
}