using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.AuthenticationService
{
    public class CeraClientAuthenticatorContext:IdentityDbContext<ApplicationUser>
    {
        public CeraClientAuthenticatorContext(DbContextOptions<CeraClientAuthenticatorContext>options):base(options)
        {

        }
    }
}
