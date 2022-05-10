using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityTypeConfig
{
    internal class PropertyConfig : BaseEntityConfig<Property>
    {
        public override void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();

            base.Configure(builder);
        }
    }
}
