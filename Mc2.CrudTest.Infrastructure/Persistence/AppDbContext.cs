using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrusTest.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .Property(c => c.PhoneNumber)
                .HasMaxLength(15);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth })
                .IsUnique();
        }
    }
}
