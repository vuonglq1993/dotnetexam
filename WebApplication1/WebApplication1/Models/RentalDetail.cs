using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class RentalDetail
    {
        [Key]
        public int RentalDetailID { get; set; }

        [ForeignKey("Rental")]
        public int RentalID { get; set; }

        [ForeignKey("ComicBook")]
        public int ComicBookID { get; set; }

        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }

        public Rental Rental { get; set; }
        public ComicBook ComicBook { get; set; }
    }
}