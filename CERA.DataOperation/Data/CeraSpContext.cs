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
        }
        public DbSet<ResourceTypeCount> resourceTypeCount { get; set; }
    }
}
