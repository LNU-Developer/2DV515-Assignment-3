using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public partial class WordReference
    {
        public int WordReferenceId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        [Required]
        public int WordId { get; set; }
        public virtual Word Word { get; set; }

    }
}
