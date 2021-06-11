using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CERA.DataOperation.Data
{
    public class CeraDbContext : DbContext
    {
        //IConfiguration _Config;
        //public CeraDbContext(IConfiguration Config)
        //{
        //    _Config = Config;
        //}
        public CeraDbContext(DbContextOptions<CeraDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //optionsBuilder.UseSqlServer(_Config.GetConnectionString("")
                    //"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_Cera; Integrated Security= true;"
                    //);
        }
        
        public DbSet<CeraSubscription> Subscriptions { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientCloudPlugin> ClientCloudPlugins { get; set; }
        public DbSet<CloudPlugIn> CloudPlugIns { get; set; }
        public DbSet<CeraResources> Resources { get; set; }
        public DbSet<CeraVM> ceraVMs { get; set; }
        public DbSet<CeraResourceGroups> resourceGroups { get; set; }
        public DbSet<CeraStorageAccount> StorageAccounts { get; set; }
        public DbSet<CeraSqlServer> SqlServers { get; set; }
        public DbSet<CeraTenants> CeraTenants { get; set; }
        public DbSet<CeraWebApps> CeraWebApps { get; set; }

        public DbSet<CeraAppServicePlans> AppServicePlans { get; set; }
        public DbSet<CeraDisks> Disks { get; set; }

        public DbSet<CeraResourceHealth> ResourceHealth { get; set; }
        public DbSet<CeraCompliances> Compliances { get; set; }
        public DbSet<CeraRateCard> RateCard { get; set; }
        public DbSet<CeraUsage> UsageDetails { get; set; }
        public DbSet<CeraPolicy> Policies { get; set; }
        public DbSet<AzureLocations> Locations { get; set; }
    }
    
}