using CERAAPI.Data;
using CERAAPI.Entities;
using CERAAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CERAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ManageController : ControllerBase
    {
        private readonly CeraAPIUserDbContext _db;
        public ManageController(CeraAPIUserDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult RegisterOrganisation([FromBody] AddOrganizationViewModel Org)
        {
            Org.UserId = Guid.NewGuid();
            _db.Clients.Add(new Client()
            {
                Id = Org.UserId,
                PrimaryContactName = Org.ContactPersonName,
                ClientDescription = Org.Description,
                PrimaryEmail = Org.EmailId,
                ClientName = Org.OrganizationName,
                PrimaryPhone = Org.PhoneNo,
                PrimaryAddress = Org.PrimaryAddress
            });
            var result = _db.SaveChanges();
            if (result < 1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "Failed to insert data into DB" });
            }
            return Ok(new ResponseViewModel { Status = "Success", Message = "Data inserted into DB" });
        }
        [HttpPost]
        public IActionResult ManagePlatform([FromBody] AddClientPlatformViewModel platform)
        {
            try
            {
                var cloudPlugIn = _db.CloudPlugIns.Where(x => x.CloudProviderName == platform.PlatformName).FirstOrDefault();
                var client = _db.Clients.Where(x => x.ClientName == platform.OrganizationName || x.Id == platform.OrganizationId).FirstOrDefault();
                _db.ClientCloudPlugins.Add(new ClientCloudPlugin()
                {
                    ClientId = platform.ClientId,
                    ClientSecret = platform.ClientSecret,
                    Client = client,
                    PlugIn = cloudPlugIn,
                    TenantId = platform.TenantId
                });
                var result = _db.SaveChanges();
                if (result < 1)
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
        [HttpPost]
        public IActionResult ManagePlugin(AddCloudPluginViewModel plugin)
        {
            try
            {
                _db.CloudPlugIns.Add(new CloudPlugIn()
                {
                    CloudProviderName = plugin.CloudProviderName,
                    DllPath = plugin.DllPath,
                    FullyQualifiedClassName = plugin.FullyQualifiedClassName,
                    DateEnabled = DateTime.Now,
                    Description = plugin.Description,
                    DevContact = plugin.DevContact,
                    SupportContact = plugin.SupportContact
                });
                var result = _db.SaveChanges();
                if (result < 1)
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
