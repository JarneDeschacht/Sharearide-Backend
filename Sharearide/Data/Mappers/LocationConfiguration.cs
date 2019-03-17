using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Data.Mappers
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");
            builder.HasKey(l => l.LocationId);
            builder.Property(l => l.Number)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(l => l.Street)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(l => l.CompanyName)
                .HasMaxLength(100);
            builder.HasOne(l => l.City)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
