using CERA.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERA.DataOperation.Data
{
    public class CeraDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Data Source=(localdb)\ProjectsV13;Initial Catalog=SampleDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
            optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\ProjectsV13;Initial Catalog=db_Cera"
                    );
        }
        public DbSet<CeraSubscription> Subscriptions { get; set; }
    }
}