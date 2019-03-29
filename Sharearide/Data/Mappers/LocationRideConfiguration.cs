using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Data.Mappers
{
    public class LocationRideConfiguration : IEntityTypeConfiguration<LocationRide>
    {
        public void Configure(EntityTypeBuilder<LocationRide> builder)
        {
            builder.ToTable("LocationRide");
            builder.HasKey(t => new { t.LocationId, t.RideId });

            builder.HasOne(t => t.Location)
                    .WithMany()
                    .HasForeignKey(t => t.LocationId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pt => pt.Ride)
                    .WithMany(pt => pt.LocationRides)
                    .HasForeignKey(pt => pt.RideId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
