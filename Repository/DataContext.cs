using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp01.Models;

namespace WebApp01.Repository
{
    public class DataContext : IdentityDbContext<AppUserModel>

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
            //
        }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CheckoutModel> Checkouts { get; set; }
        public DbSet<MailModel> Mails { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CheckoutModel>()
        //        .Property(b => b.Id)
        //        .ValueGeneratedOnAdd();
        //}
    }
}
