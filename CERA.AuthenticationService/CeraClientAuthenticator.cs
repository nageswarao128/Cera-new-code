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
            try
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

                if (user.Role == "Admin")
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
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<object> Login(LoginModel loginModel)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(loginModel.UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public List<UserModel> GetUsers()
        {
            try
            {
                List<UserModel> users = new List<UserModel>();
                var user = _userManager.Users;
                foreach (var item in user)
                {
                    users.Add(new UserModel
                    {
                        id = item.Id,
                        userName = item.UserName,
                        emailId = item.Email
                    });
                }
                return users;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public async Task<UserModel> GetUser(string id)
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public async Task<object> UpdateUser(UpdateUserModel userModel)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userModel.Id);
                user.UserName = userModel.UserName;
                user.Email = userModel.EmailId;
                user.UpdatedTime = DateTime.Now;
                var result = await _userManager.UpdateAsync(user);
                var role = await _userManager.GetRolesAsync(user);
                if (userModel.Role[0] != role[0])
                {
                    await _userManager.RemoveFromRoleAsync(user, role[0]);
                    await _userManager.AddToRoleAsync(user, userModel.Role[0]);
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public async Task<object> DeleteUser(string id)
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
            
    }
}
