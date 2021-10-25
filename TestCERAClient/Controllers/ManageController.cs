using CERA.AuthenticationService;
using CERA.DataOperation;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Platform;
using CERAAPI.Data;
using CERAAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CERAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ManageController : ControllerBase
    {
        private readonly CeraAPIUserDbContext db;
        ICeraPlatform platform;
        ICeraClientAuthenticator ceraAuthenticator;
        public ManageController(CeraAPIUserDbContext _db, ICeraPlatform _platform,ICeraClientAuthenticator _ceraAuthenticator)
        {
            db = _db;
            platform = _platform;
            ceraAuthenticator = _ceraAuthenticator;
        }

        /// <summary>
        /// This method will returns the available users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<UserModel> GetUsers()
        {
            return ceraAuthenticator.GetUsers();
        } 

        /// <summary>
        /// gets a user details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<UserModel> GetUser(string id)
        {
            return ceraAuthenticator.GetUser(id);
        }

        /// <summary>
        /// get user details
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public  Task<UserModel> GetUserProfile(string name)
        {
            return ceraAuthenticator.GetUserProfile(name);
        }

        /// <summary>
        /// to update the user info
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut]
        public Task<object> UpdateUser(UpdateUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            return ceraAuthenticator.UpdateUser(userModel);
        }

        /// <summary>
        /// to delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public Task<object> DeleteUser(string id)
        {
            return ceraAuthenticator.DeleteUser(id);
        }

        /// <summary>
        /// to add a organisation/client
        /// </summary>
        /// <param name="orgModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddOrganisation([FromBody] RegisterOrgModel orgModel)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "Details should not be null" });
            }
            AddOrganizationViewModel organizationViewModel = new AddOrganizationViewModel{ 
                OrganizationName=orgModel.OrganizationName,
                PrimaryAddress = orgModel.PrimaryAddress,
                Description = orgModel.OrgDescription,
                ContactPersonName = orgModel.ContactPersonName,
                EmailId = orgModel.EmailId,
                PhoneNo = orgModel.PhoneNo,
                UserId = orgModel.UserId
            };
            AddCloudPluginViewModel cloudPluginViewModel = new AddCloudPluginViewModel {
                CloudProviderName = orgModel.CloudProviderName,
                DateEnabled = orgModel.DateEnabled,
                DllPath = orgModel.DllPath,
                FullyQualifiedClassName = orgModel.FullyQualifiedClassName,
                DevContact = orgModel.DevContact,
                Description = orgModel.Description,
                SupportContact = orgModel.SupportContact
            };
            AddClientPlatformViewModel clientPlatformViewModel = new AddClientPlatformViewModel
            {
                OrganizationName=orgModel.OrganizationName,
                PlatformName = orgModel.CloudProviderName,
                TenantId = orgModel.TenantId,
                ClientId = orgModel.ClientId,
                ClientSecret = orgModel.ClientSecret
            };
            if (platform.OnBoardOrganization(organizationViewModel) >= 0)
            {
                if (platform.OnBoardCloudProvider(cloudPluginViewModel) >= 0)
                {
                    if (platform.OnBoardClientPlatform(clientPlatformViewModel) > 0)
                    {
                        return Ok(new ResponseViewModel { Status = "Success", Message = "Data inserted into DB" });
                    }
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "Failed to insert data into DB" });
        }

        /// <summary>
        /// This method will inserts the organisation details into database 
        /// </summary>
        /// <param name="Org"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RegisterOrganisation([FromBody] AddOrganizationViewModel Org)
        {
            if (platform.OnBoardOrganization(Org) < 1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "Failed to insert data into DB" });
            }
            return Ok(new ResponseViewModel { Status = "Success", Message = "Data inserted into DB" });
        }

        /// <summary>
        /// This method will inserts the cloud details for the orgnisation into database
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ManagePlatform([FromBody] AddClientPlatformViewModel platform)
        {
            try
            {

                if (this.platform.OnBoardClientPlatform(platform) < 1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "Failed to insert data into DB" });
                }

                return Ok(new ResponseViewModel { Status = "Success", Message = "Data inserted into DB" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = ex.Message });
            }
        }

        /// <summary>
        /// This method will inserts the dll details for the cloud 
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ManagePlugin(AddCloudPluginViewModel plugin)
        {
            try
            {

                if (platform.OnBoardCloudProvider(plugin) < 1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "Failed to insert data into DB" });
                }

                return Ok(new ResponseViewModel { Status = "Success", Message = "Data inserted into DB" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = ex.Message });
            }
        }
    }
}
