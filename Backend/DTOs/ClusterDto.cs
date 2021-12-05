namespace Backend.DTOs
{
    public class ClusterDto
    {
        public ClusterDto Left { get; set; }
        public ClusterDto Right { get; set; }
        public ClusterDto Parent { get; set; }
        public BlogDto Blog { get; set; }
    }
}