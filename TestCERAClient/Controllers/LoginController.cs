using CERA.AuthenticationService;
using CERAAPI.Data;
using CERAAPI.Entities;
using CERAAPI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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
            var result = _ceraClientAuthenticator.AddUser(registerUser);
            return result;
        }
        /// <summary>
        /// This method is used for user login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task <object> Login([FromBody] LoginModel loginModel)
        {
            var result = await _ceraClientAuthenticator.Login(loginModel);
            if (result != null)
            {
                //var auth = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                //auth.AddClaim(new Claim(ClaimTypes.Name, loginModel.UserName));
                //var principle = new ClaimsPrincipal(auth);

                //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle).Wait();
                return result;
            }
            return null;
        }
        [HttpPost]
        public async void Logout()
        {
            await HttpContext.SignOutAsync();
        }
    }
}
