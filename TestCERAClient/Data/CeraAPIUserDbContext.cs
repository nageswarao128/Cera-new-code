using CERAAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERAAPI.Data
{
    public class CeraAPIUserDbContext : IdentityDbContext<CERAAPIUser>
    {
        public CeraAPIUserDbContext(DbContextOptions<CeraAPIUserDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //         "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_Cera_User; Integrated Security= true;"
        //         );
        //    base.OnConfiguring(optionsBuilder);
        //}
     
    }
}
