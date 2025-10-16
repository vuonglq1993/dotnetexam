using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ComicBook
    {
        [Key]
        public int ComicBookID { get; set; }

        [Required, StringLength(255)]
        public string Title { get; set; }

        [Required, StringLength(255)]
        public string Author { get; set; }

        [Range(0, 999999)]
        public decimal PricePerDay { get; set; }
    }
}