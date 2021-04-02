using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CERA.AuthenticationService
{
    public class CeraClientAuthenticator:ICeraClientAuthenticator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        CeraClientAuthenticatorContext _dbContext;
        public CeraClientAuthenticator(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,CeraClientAuthenticatorContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }
        public async Task<string> RegisterUser(RegisterUser user)
        {
            ApplicationUser applicationUser;
            var userExist = await _userManager.FindByNameAsync(user.UserName);
            if (userExist != null)
            {
                return "User Already Exist";
            }
            applicationUser = new ApplicationUser()
            {
                Email = user.EmailID,
                UserName = user.UserName,
                OrgName = user.OrgName,
                CreatedTime = user.CreatedTime,
            };
            var result = await _userManager.CreateAsync(applicationUser, user.Password);
            if (!result.Succeeded)
            {
                return "Check the given inputs and try again";
            }
            
            if(user.Role == "Admin")
            {
                if (!await _roleManager.RoleExistsAsync(user.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(user.Role));
                }
                var role = await _userManager.AddToRoleAsync(applicationUser, user.Role);
                if (!role.Succeeded)
                {
                    return "Error while regitering role to user";
                }
            }
            else
            {
                if (!await _roleManager.RoleExistsAsync(user.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(user.Role));
                }
                var role = await _userManager.AddToRoleAsync(applicationUser, user.Role);
                if (!role.Succeeded)
                {
                    return "Error while regitering role to user";
                }
            }
           
            return "User registered Successfully";
        }

        public async Task<object> LoginUser(LoginModel loginModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            //var client = _db.Clients.Where(x => x.PrimaryContactName == user.UserName).FirstOrDefault();
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authsigningkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CER@AP!@!@#$%^&*()"));

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
                var token1 = new { token = tokenHandler.WriteToken(token), expiration = token.ValidTo };
                return token1; 
            }
            return "Unauthorized User";
        }
    }
}
