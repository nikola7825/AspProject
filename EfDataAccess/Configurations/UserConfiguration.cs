using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(u => u.Username)
                .HasMaxLength(40)
                .IsRequired();

            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
