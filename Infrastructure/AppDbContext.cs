using Domain.Entities;
using Domain.Models.Entities;
using Infrastructure.EntityTypeConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}


        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<ParentCategory> ParentCategories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ProductCartRel> ProductCartRels { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new CategoryConfig());
            builder.ApplyConfiguration(new ProductConfig());
            builder.ApplyConfiguration(new PropertyConfig());
            builder.ApplyConfiguration(new ProductPropertyConfig());
            builder.ApplyConfiguration(new ParentCategoryConfig());
            builder.ApplyConfiguration(new ShoppingCartConfig());
            builder.ApplyConfiguration(new ProductCartRelConfig());

            base.OnModelCreating(builder);
        }

    }
}
