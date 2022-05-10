using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityTypeConfig
{
    internal class ProductPropertyConfig : BaseEntityConfig<ProductProperty>
    {
        public override void Configure(EntityTypeBuilder<ProductProperty> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Value);

            builder.HasOne(x => x.Product).WithMany(x => x.ProductProperties).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Property).WithMany(x => x.ProductProperties).HasForeignKey(x => x.PropertyId);

            base.Configure(builder);
        }
    }
}
