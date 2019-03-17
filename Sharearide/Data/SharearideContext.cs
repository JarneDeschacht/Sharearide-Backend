﻿using Sharearide.Data.Mappers;
using Sharearide.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Sharearide.Data
{
    public class SharearideContext : IdentityDbContext
    {
        public DbSet<User> Users_Domain { get; set; }

        public SharearideContext(DbContextOptions<SharearideContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new LocationConfiguration());
        }
    }
}
