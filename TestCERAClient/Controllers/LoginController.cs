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
            if (ModelState.IsValid)
            {
                var result = _ceraClientAuthenticator.AddUser(registerUser);
                return result;
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "User Details should not be null" });
        }
        /// <summary>
        /// This method is used for user login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task <object> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "User Details should not be null" });
            }
            var result = await _ceraClientAuthenticator.Login(loginModel);
            if (result != null)
            {
                return result;
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ResponseViewModel { Status = "Error", Message = "Invalid UserName or Password" });
        }
        [HttpPost]
        public async void Logout()
        {
            await HttpContext.SignOutAsync();
        }
    }
}
