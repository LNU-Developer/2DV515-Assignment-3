using Microsoft.EntityFrameworkCore;
namespace Backend.Models.Database
{
    [Index(nameof(Word))]
    public partial class WordMap
    {
        public int WordMapId { get; set; }
        public string Word { get; set; }
    }
}