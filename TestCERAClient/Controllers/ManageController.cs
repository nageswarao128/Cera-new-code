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
        private readonly CeraAPIUserDbContext _db;
        ICeraPlatform _platform;
        ICeraClientAuthenticator _ceraAuthenticator;
        public ManageController(CeraAPIUserDbContext db, ICeraPlatform platform,ICeraClientAuthenticator ceraAuthenticator)
        {
            _db = db;
            _platform = platform;
            _ceraAuthenticator = ceraAuthenticator;
        }
        /// <summary>
        /// This method will returns the available users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<UserModel> GetUsers()
        {
            return _ceraAuthenticator.GetUsers();
        } 
        [HttpGet]
        public Task<UserModel> GetUser(string id)
        {
            return _ceraAuthenticator.GetUser(id);
        }
        [HttpPut]
        public Task<object> UpdateUser(UpdateUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            return _ceraAuthenticator.UpdateUser(userModel);
        }
        [HttpDelete]
        public Task<object> DeleteUser(string id)
        {
            return _ceraAuthenticator.DeleteUser(id);
        }
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
            if (_platform.OnBoardOrganization(organizationViewModel) >= 0)
            {
                if (_platform.OnBoardCloudProvider(cloudPluginViewModel) >= 0)
                {
                    if (_platform.OnBoardClientPlatform(clientPlatformViewModel) > 0)
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
            if (_platform.OnBoardOrganization(Org) < 1)
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

                if (_platform.OnBoardClientPlatform(platform) < 1)
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

                if (_platform.OnBoardCloudProvider(plugin) < 1)
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
