using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Backend.Models.Database
{
    public partial class Blog
    {
        public Blog()
        {
            WordReferences = new HashSet<WordReference>();
        }
        public int BlogId { get; set; }
        [Required]
        public string BlogTitle { get; set; }
        public ICollection<WordReference> WordReferences { get; set; }

    }
}
