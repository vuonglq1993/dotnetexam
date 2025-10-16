using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, StringLength(255)]
        public string FullName { get; set; }

        [Required, StringLength(15)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

        [Required, StringLength(255)]
        public string Password { get; set; }
    }
}