using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ComicContext : DbContext
    {
        public ComicContext(DbContextOptions<ComicContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
    }
}