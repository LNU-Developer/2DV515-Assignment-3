namespace Backend.Models.Database
{
    public partial class PageWord
    {
        public int PageWordId { get; set; }
        public int WordHashMapId { get; set; }
        public int PageId { get; set; }
        public Page Page { get; set; }
    }
}