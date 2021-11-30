using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Backend.Models.Database
{
    public partial class Word
    {
        public Word()
        {
            Blogs = new HashSet<Blog>();
        }
        public int WordId { get; set; }
        [Required]
        public string WordTitle { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public double Amount { get; set; }
        public ICollection<Blog> Blogs { get; set; }

    }
}
