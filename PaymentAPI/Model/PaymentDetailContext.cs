using Microsoft.EntityFrameworkCore;

namespace PaymentAPI.Model
{
    public class PaymentDetailContext : DbContext
    {
        public PaymentDetailContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PaymentDetails> PaymentDetails { get; set; } 

        public DbSet<College> Colleges { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ImageModel> Images { get; set; }

    }

}