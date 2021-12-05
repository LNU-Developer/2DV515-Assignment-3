using System.Collections.Generic;
namespace Backend.DTOs
{
    public class CentroidDto
    {
        public int Id { get; set; }
        public List<BlogDto> Assignments { get; set; }
    }
}