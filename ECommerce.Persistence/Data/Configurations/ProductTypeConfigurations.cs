using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Domain.Models.ProductModule;

namespace ECommerce.Persistence.Data.Configurations
{
    public class ProductTypeConfigurations : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(P => P.Name)
                        .HasMaxLength(200);
        }
    }
}
