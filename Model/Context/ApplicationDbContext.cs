using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Enums;
using Model.Models;

namespace OnlineShop.Data
{
    public  class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasQueryFilter(x => x.State != State.deleted);
            modelBuilder.Entity<Product>().HasQueryFilter(x => x.State != State.deleted);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Cupon> Cupons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProductPic> ProductPics { get; set; }
        public DbSet<ImageOffert> ImageOfferts { get; set; }
        public DbSet<Offert> Offerts { get; set; }
    }
}
