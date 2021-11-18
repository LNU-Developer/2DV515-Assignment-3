using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Database
{
    public partial class Word
    {
        public int WordId { get; set; }
        [Required]
        public string WordTitle { get; set; }
        public int Count { get; set; }
        [Required]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

    }
}
