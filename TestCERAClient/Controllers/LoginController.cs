using CERA.DataOperation.Data;
using CERA.Entities;
using CERAAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERAAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration _configuration;
        private readonly CeraDbContext _appDbContext;
        public LoginController(UserManager<IdentityUser> userManager, IConfiguration configuration, CeraDbContext appDbContext)
        {
            this.userManager = userManager;
            _configuration = configuration;
            _appDbContext = appDbContext;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var userExist = await userManager.FindByNameAsync(registerUser.userName);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { status = "Error", message = "User already Exists" });
            }
            IdentityUser user = new IdentityUser()
            {
                Email = registerUser.emailId,
                UserName = registerUser.userName
            };
            var result = await userManager.CreateAsync(user, registerUser.password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { status = "Error", message = "Check the given inputs and try again" });
            }
            return Ok(new ResponseModel { status = "Success", message = "User registered Successfully" });
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await userManager.FindByNameAsync(loginModel.userName);
            if (user != null && await userManager.CheckPasswordAsync(user, loginModel.password))
            {
                var authsigningkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(authsigningkey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        [HttpPost]
        public IActionResult RegisterOrganisation([FromBody] RegisterOrg registerOrg)
        {
            registerOrg.userId = Guid.NewGuid();
            _appDbContext.RegisterOrganisation.Add(registerOrg);
            var result = _appDbContext.SaveChanges();
            if (result < 1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { status = "Error", message = "Failed to insert data into DB" });
            }
            return Ok(new ResponseModel { status = "Success", message = "Data inserted into DB" });
        }
        [HttpPost]
        public IActionResult ManagePlatform([FromBody] ManagePlatform managePlatform)
        {
            _appDbContext.ManagePlatform.Add(managePlatform);
            var result = _appDbContext.SaveChanges();
            if (result < 1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { status = "Error", message = "Failed to insert data into DB" });
            }
            return Ok(new ResponseModel { status = "Success", message = "Data inserted into DB" });
        }
    }
}
