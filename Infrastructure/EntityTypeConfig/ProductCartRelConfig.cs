using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityTypeConfig
{
    internal class ProductCartRelConfig : BaseEntityConfig<ProductCartRel>
    {
        public override void Configure(EntityTypeBuilder<ProductCartRel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).IsRequired();

            builder.HasOne(x => x.Product).WithMany(x => x.ProductCartRels).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.ShoppingCart).WithMany(x => x.ProductCartRels).HasForeignKey(x => x.ShoppingCartId);
            base.Configure(builder);
        }
    }
}
