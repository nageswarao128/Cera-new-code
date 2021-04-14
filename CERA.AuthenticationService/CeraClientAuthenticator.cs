using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
        public async Task<string> AddUser(RegisterUser user)
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
        public async Task<object> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                return user;
            }
            return null;
        }
        public List<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();
            var user = _userManager.Users;
            foreach(var item in user)
            {
                users.Add(new UserModel
                {
                    id = item.Id,
                    userName=item.UserName,
                    emailId=item.Email
                });
            }
            return users;
        }
        public async Task<UserModel> GetUser(string id)
        {
            UserModel user = new UserModel();
            user.id = id;
            var result = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(result);
            user = new UserModel
            {
                id = result.Id,
                userName = result.UserName,
                emailId = result.Email,
                roles = role
            };
            return user;
        }
        public async Task<object> UpdateUser(UpdateUserModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.Id);
            user.UserName = userModel.UserName;
            user.Email = userModel.EmailId;
            user.UpdatedTime = DateTime.Now;
            var result = await _userManager.UpdateAsync(user);
            var role = await _userManager.GetRolesAsync(user);
            if(userModel.Role[0] != role[0])
            {
               await _userManager.RemoveFromRoleAsync(user, role[0]);
               await _userManager.AddToRoleAsync(user, userModel.Role[0]);
            }
            return result;
        }
        public async Task<object> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(user);
            if (role.Count > 0)
            {
                await _userManager.RemoveFromRoleAsync(user, role[0]);
            }
            var result = await _userManager.DeleteAsync(user);
            return result;
        }
            
    }
}
