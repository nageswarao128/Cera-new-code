using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CERA.CloudService.CERAEntities;
using CERA.Entities;

namespace CERA.DataOperation.CeraContext
{
   public class CeraDbContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Data Source=(localdb)\ProjectsV13;Initial Catalog=SampleDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
            optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\ProjectsV13;Initial Catalog=CeraTestDatabase"
                    );
        }
        public DbSet<SubscriptionList> Subscriptiondata { get; set; }
    }
}
