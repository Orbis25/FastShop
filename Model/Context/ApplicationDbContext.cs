﻿using DataLayer.Models.Cart;
using DataLayer.Models.Categories;
using DataLayer.Models.Configurations;
using DataLayer.Models.Countries;
using DataLayer.Models.Emails;
using DataLayer.Models.Emails.Templates;
using DataLayer.Models.Products;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Enums;
using Model.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasQueryFilter(x => x.State != State.Deleted);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(x => x.State != State.Deleted)
                .HasMany(x => x.CartItems)
                .WithOne(x => x.Product)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProductPic>().HasQueryFilter(x => x.State != State.Deleted);
            modelBuilder.Entity<Cupon>().HasQueryFilter(x => x.State != State.Deleted);
            modelBuilder.Entity<Sale>().HasQueryFilter(x => x.State != State.Deleted);
            modelBuilder.Entity<Offert>().HasQueryFilter(x => x.State != State.Deleted);

          
        }


        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cupon> Cupons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<DetailSale> DetailSales { get; set; }

        public DbSet<ProductPic> ProductPics { get; set; }
        public DbSet<ImageOffert> ImageOfferts { get; set; }
        public DbSet<Offert> Offerts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<AvaibleCountry> AvaibleCountries { get; set; }
        public DbSet<AvaibleCity> AvaibleCities { get; set; }

        public DbSet<ProductDetail> ProductDetail { get; set; }

    }
}
