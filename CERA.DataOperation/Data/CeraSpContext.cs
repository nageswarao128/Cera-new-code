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
            modelBuilder.Entity<ResourcesModel>().HasNoKey();
            modelBuilder.Entity<ResourceGroupsVM>().HasNoKey();
            modelBuilder.Entity<Dashboardresources>().HasNoKey();
            modelBuilder.Entity<StorageAccountsVM>().HasNoKey();
            modelBuilder.Entity<BarChartModel>().HasNoKey();
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
        public DbSet<ResourcesModel> resources { get; set; }
        public DbSet<ResourceGroupsVM> resourceGroups { get; set; }
        public DbSet<Dashboardresources> dashboardresources { get; set; }
        public DbSet<StorageAccountsVM> storageAccounts { get; set; }
        public DbSet<BarChartModel> barCharts { get; set; }
    }
}
