using System.Collections.Generic;
namespace Backend.Models.Database
{
    public partial class Page
    {
        public Page()
        {
            PageWords = new HashSet<PageWord>();
            Links = new HashSet<Link>();
        }
        public int PageId { get; set; }
        public string Url { get; set; }
        public ICollection<Link> Links { get; set; }
        public ICollection<PageWord> PageWords { get; set; }
    }
}