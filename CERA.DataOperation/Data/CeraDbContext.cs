using CERA.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CERA.DataOperation.Data
{
    public class CeraDbContext : IdentityDbContext
    {
        public CeraDbContext(DbContextOptions<CeraDbContext> options) : base(options)
        {

        }
        public DbSet<CeraSubscription> Subscriptions { get; set; }
        public DbSet<RegisterOrg> RegisterOrganisation { get; set; }
        public DbSet<ManagePlatform> ManagePlatform { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<RegisterOrg>().HasNoKey();
            //modelBuilder.Entity<ManagePlatform>().HasNoKey();
        }
      

    }
}