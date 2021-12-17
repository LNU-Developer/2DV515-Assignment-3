using Microsoft.EntityFrameworkCore;
namespace Backend.Models.Database
{
    [Index(nameof(PageId))]
    public partial class PageWord
    {
        public int PageWordId { get; set; }
        public int WordHashMapId { get; set; }
        public int PageId { get; set; }
        public int Order { get; set; }
        public Page Page { get; set; }
    }
}