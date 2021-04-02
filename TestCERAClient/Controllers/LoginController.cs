using CERA.AuthenticationService;
using CERAAPI.Data;
using CERAAPI.Entities;
using CERAAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CERAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ICeraClientAuthenticator _ceraClientAuthenticator;
        
        IConfiguration _configuration;
        CeraAPIUserDbContext _db;
        public LoginController( IConfiguration configuration, CeraAPIUserDbContext db,ICeraClientAuthenticator ceraClientAuthenticator)
        {
            _configuration = configuration;
            _db = db;
            _ceraClientAuthenticator = ceraClientAuthenticator;
        }
        /// <summary>
        /// This method will registers the user
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        [HttpPost]
        public object Register([FromBody] RegisterUser registerUser)
        {
            var result = _ceraClientAuthenticator.RegisterUser(registerUser);
            return result;
        }
        
    }
}
