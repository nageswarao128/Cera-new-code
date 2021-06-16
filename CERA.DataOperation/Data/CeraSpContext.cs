using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.DataOperation.Data
{
    public class CeraSpContext: DbContext
    {
        public CeraSpContext(DbContextOptions<CeraSpContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceTypeCount>().HasNoKey();
            modelBuilder.Entity<ResourceLocations>().HasNoKey();
            modelBuilder.Entity<ResourceTagsCount>().HasNoKey();
            modelBuilder.Entity<CeraResourceTypeUsage>().HasNoKey();
        }
        public DbSet<ResourceTypeCount> resourceTypeCount { get; set; }
        public DbSet<ResourceLocations> Locations { get; set; }
        public DbSet<ResourceTagsCount> resourceTags { get; set; }
        public DbSet<CeraResourceTypeUsage> resourceUsage { get; set; }
    }
}
