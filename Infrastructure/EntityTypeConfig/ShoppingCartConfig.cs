using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityTypeConfig
{
    internal class ShoppingCartConfig : BaseEntityConfig<ShoppingCart>
    {
        public override void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User).WithMany(x => x.ShoppingCarts).HasForeignKey(x => x.UserId);

            base.Configure(builder);
        }
    }
}
