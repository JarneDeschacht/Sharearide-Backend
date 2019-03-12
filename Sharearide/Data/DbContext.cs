using Sharearide.Data.Mappers;
using Sharearide.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharearide.Data
{
    public class sharearideContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public sharearideContext(DbContextOptions<sharearideContext> options)
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
