using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(25)
                .IsRequired();

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.HasMany(t => t.PostTags)
                .WithOne(pt => pt.Tag)
                .HasForeignKey(pt => pt.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
