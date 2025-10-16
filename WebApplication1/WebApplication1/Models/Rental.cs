using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public Customer Customer { get; set; }
        public ICollection<RentalDetail> RentalDetails { get; set; }
    }
}