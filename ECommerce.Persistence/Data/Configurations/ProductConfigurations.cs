using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Domain.Models.ProductModule;

namespace ECommerce.Persistence.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name)
                 .HasMaxLength(200);

            builder.Property(P => P.Description)
                    .HasMaxLength(500);

            builder.Property(P => P.PictureUrl)
                    .HasMaxLength(200);

            builder.Property(P => P.Price)
                    .HasPrecision(18, 2);


            builder.HasOne(P => P.ProductBrand)
                .WithMany()
                .HasForeignKey(P => P.ProductBrandId);

            builder.HasOne(P => P.ProductCategory)
                .WithMany()
                .HasForeignKey(P => P.ProductCategoryId);
        }
    }
}
