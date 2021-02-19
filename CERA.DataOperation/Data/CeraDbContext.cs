using CERA.Entities;
using Microsoft.EntityFrameworkCore;

namespace CERA.DataOperation.Data
{
    public class CeraDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\ProjectsV13;Initial Catalog=db_Cera"
                    );
        }
        public DbSet<CeraSubscription> Subscriptions { get; set; }
    }
}