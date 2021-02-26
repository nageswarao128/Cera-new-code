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
        private readonly UserManager<CERAAPIUser> _userManager;
        IConfiguration _configuration;
        CeraAPIUserDbContext _db;
        public LoginController(UserManager<CERAAPIUser> userManager, IConfiguration configuration, CeraAPIUserDbContext db)
        {
            _userManager = userManager;
            _configuration = configuration;
            _db = db;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerUser)
        {
            var userExist = await _userManager.FindByNameAsync(registerUser.UserName);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "User already Exists" });
            }
            CERAAPIUser user = new CERAAPIUser()
            {
                Email = registerUser.EmailID,
                UserName = registerUser.UserName
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "Check the given inputs and try again" });
            }
            return Ok(new ResponseViewModel { Status = "Success", Message = "User registered Successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            //var client = _db.Clients.Where(x => x.PrimaryContactName == user.UserName).FirstOrDefault();
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authsigningkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(authsigningkey, SecurityAlgorithms.HmacSha256Signature)
                };
                //if (client != null)
                //{
                //    tokenDescriptor.Subject.AddClaim(new Claim("orgname", client.ClientName));
                //}
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    expiration = token.ValidTo
                }); ;
            }
            return Unauthorized();
        }
    }
}
