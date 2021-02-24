using CERAAPI.Data;
using CERAAPI.Entities;
using CERAAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
            _db.Organizations.Add(new Organization()
            {
                UserId = Org.UserId,
                ContactPersonName = Org.ContactPersonName,
                Description = Org.Description,
                EmailId = Org.EmailId,
                OrganizationName = Org.OrganizationName,
                PhoneNo = Org.PhoneNo,
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
        public IActionResult ManagePlatform([FromBody] AddPlatformViewModel managePlatform)
        {
            _db.Platforms.Add(new Platform()
            {
                ClientId = managePlatform.ClientId,
                ClientSecret = managePlatform.ClientSecret,
                OrganizationId = managePlatform.OrganizationId,
                PlatformName = managePlatform.PlatformName,
                TenantId = managePlatform.TenantId
            });
            var result = _db.SaveChanges();
            if (result < 1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseViewModel { Status = "Error", Message = "Failed to insert data into DB" });
            }
            return Ok(new ResponseViewModel { Status = "Success", Message = "Data inserted into DB" });
        }

    }
}
