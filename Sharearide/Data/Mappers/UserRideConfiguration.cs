using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sharearide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Data.Mappers
{
    public class UserRideConfiguration : IEntityTypeConfiguration<UserRide>
    {
        public void Configure(EntityTypeBuilder<UserRide> builder)
        {
            builder.ToTable("UserRide");
            builder.HasKey(t => new { t.UserId, t.RideId });

            builder.HasOne(t => t.User)
                    .WithMany(t => t.UserRides)
                    .HasForeignKey(t => t.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pt => pt.Ride)
                    .WithMany(pt => pt.UserRides)
                    .HasForeignKey(pt => pt.RideId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
