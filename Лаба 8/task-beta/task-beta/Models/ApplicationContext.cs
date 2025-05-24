using Microsoft.EntityFrameworkCore;
using ProductModel.Models;
using OrderModel.Models;

namespace task_beta.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> products {  get; set; }
        public DbSet<Order> orders { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("product");
            modelBuilder.Entity<Order>().ToTable("orderinfo");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.OrderEmail).IsRequired(false);
            });
        }
    }
}
