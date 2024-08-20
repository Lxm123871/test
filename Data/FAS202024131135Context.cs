using FAS202024131135.Models;
using Microsoft.EntityFrameworkCore;
namespace FAS202024131135.Data
{
    public class FAS202024131135Context : DbContext
    {
        public FAS202024131135Context(DbContextOptions<FAS202024131135Context> options): base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Asset>().ToTable("Asset");        
        }
    }
}
