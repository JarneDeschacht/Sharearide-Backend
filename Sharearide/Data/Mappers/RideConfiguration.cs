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
            builder.HasOne(r => r.PickUpLocation)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.DropOffLocation)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(r => r.LocationRides)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(r => r.Stopovers);
            builder.HasOne(r => r.Owner)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
