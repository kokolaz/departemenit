using DepartemenIT.Models;
using Microsoft.EntityFrameworkCore;

namespace DepartemenIT.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
