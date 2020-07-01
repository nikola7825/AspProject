using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title)
                .IsRequired();

            builder.Property(p => p.Summary)
                .IsRequired();

            builder.Property(p => p.Text)
                .IsRequired();

            builder.HasMany(p => p.PostTags)
                .WithOne(pt => pt.Post)
                .HasForeignKey(pt => pt.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
