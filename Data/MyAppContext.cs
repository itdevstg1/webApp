using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
namespace WebApplication1.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }

        public DbSet<Category> Categories { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder); ;
        //    modelBuilder.Entity<Category>().HasData(
        //          new Category { Id = 1, Name = "Laptop" },
        //          new Category { Id = 2, Name = "Shoes" }
        //        );
        //}


    }
}
