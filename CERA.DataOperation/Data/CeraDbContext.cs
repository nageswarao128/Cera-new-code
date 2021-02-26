using CERA.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace CERA.DataOperation.Data
{
    public class CeraDbContext : DbContext
    {
        public CeraDbContext(DbContextOptions<CeraDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_Cera; Integrated Security= true;"
                    );
        }
        public DbSet<CeraSubscription> Subscriptions { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientCloudPlugin> ClientCloudPlugins { get; set; }
        public DbSet<CloudPlugIn> CloudPlugIns { get; set; }
    }
}