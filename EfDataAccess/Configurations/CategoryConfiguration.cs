using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
               .HasMaxLength(30)
               .IsRequired();

            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

        }
    }
}
