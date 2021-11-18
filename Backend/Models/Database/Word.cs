using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Backend.Models.Database
{
    public partial class Word
    {
        public Word()
        {
            WordReferences = new HashSet<WordReference>();
        }
        public int WordId { get; set; }
        [Required]
        public string WordTitle { get; set; }
        public ICollection<WordReference> WordReferences { get; set; }

    }
}
