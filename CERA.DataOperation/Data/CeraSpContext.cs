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
            modelBuilder.Entity<CostUsage>().HasNoKey();
            modelBuilder.Entity<UsageByLocation>().HasNoKey();
            modelBuilder.Entity<UsageByResourceGroup>().HasNoKey();
            modelBuilder.Entity<UsageHistoryByMonth>().HasNoKey();
            modelBuilder.Entity<DashboardCountModel>().HasNoKey();
            modelBuilder.Entity<locationFilter>().HasNoKey();
            modelBuilder.Entity<ManageOrg>().HasNoKey();
        }
        public DbSet<ResourceTypeCount> resourceTypeCount { get; set; }
        public DbSet<ResourceLocations> Locations { get; set; }
        public DbSet<ResourceTagsCount> resourceTags { get; set; }
        public DbSet<CeraResourceTypeUsage> resourceUsage { get; set; }
        public DbSet<CostUsage> usageSp { get; set; }
        public DbSet<UsageHistoryByMonth> usageHistoryByMonth { get; set; }
        public DbSet<UsageByLocation> usageByLocation { get; set; }
        public DbSet<UsageByResourceGroup> usageByResourceGroup { get; set; }
        public DbSet<DashboardCountModel> dashboardCounts { get; set; }
        public DbSet<locationFilter> locationfilters { get; set; }
        public DbSet<ManageOrg> manageorg { get; set; }
    }
}
