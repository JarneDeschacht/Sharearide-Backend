using CarpoolApplication.Data.Mappers;
using CarpoolApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarpoolApplication.Data
{
    public class CarpoolContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public CarpoolContext(DbContextOptions<CarpoolContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
