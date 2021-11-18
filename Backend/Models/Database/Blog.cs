using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Backend.Models.Database
{
    public partial class Blog
    {
        public Blog()
        {
            Words = new HashSet<Word>();
        }
        public int BlogId { get; set; }
        [Required]
        public string BlogTitle { get; set; }
        public ICollection<Word> Words { get; set; }

    }
}
