using CERA.Entities;
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
    }
}