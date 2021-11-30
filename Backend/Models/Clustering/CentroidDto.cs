using Backend.Models.Database;
using System.Collections.Generic;
namespace Backend.Models.Clustering
{
    public class CentroidDto
    {
        public List<BlogDto> Assignments { get; set; }
    }
}