using Sharearide.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Data.Mappers
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasMany(u => u.UserRides)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(u => u.Rides);
        }
    }
}
