namespace Backend.Models.Database
{
    public partial class Link
    {
        public int LinkId { get; set; }
        public string Url { get; set; }
        public int PageId { get; set; }
        public Page Page { get; set; }
        public int Order { get; set; }
    }
}