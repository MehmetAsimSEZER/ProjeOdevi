using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityTypeConfig
{
    internal class UserConfig : BaseEntityConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Adress).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Email).IsRequired();

            base.Configure(builder);
        }
    }
}
