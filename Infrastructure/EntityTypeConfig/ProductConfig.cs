using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityTypeConfig
{
    internal class ProductConfig : BaseEntityConfig<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductName).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.Price).HasPrecision(10,3).HasConversion<decimal>().IsRequired();

            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);

            base.Configure(builder);
        }
    }
}
