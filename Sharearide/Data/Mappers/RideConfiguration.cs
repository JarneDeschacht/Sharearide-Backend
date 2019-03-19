using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Data.Mappers
{
    public class RideConfiguration : IEntityTypeConfiguration<Ride>
    {
        public void Configure(EntityTypeBuilder<Ride> builder)
        {
            builder.ToTable("Ride");
            builder.HasKey(r => r.RideId);
            builder.HasOne(r => r.PickUpLocation)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.DropOffLocation)
                .WithMany()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(r => r.Stopovers)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(r => r.People)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
