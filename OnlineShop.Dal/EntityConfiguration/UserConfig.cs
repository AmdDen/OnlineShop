using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Dal.EntityConfiguration
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.LastName)
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(x => x.City)
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(x => x.Address)
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(x => x.PostalCode)
               .HasMaxLength(50)
               .IsRequired(false);

        }
    }
}
