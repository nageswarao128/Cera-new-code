using CERA.DataOperation;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Platform;
using CERAAPI.Data;
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
        ICeraPlatform _platform;
        public ManageController(CeraAPIUserDbContext db, ICeraPlatform platform)
        {
            _db = db;
            _platform = platform;
        }

        [HttpPost]
        public IActionResult RegisterOrganisation([FromBody] AddOrganizationViewModel Org)
        {
            if (_platform.OnBoardOrganization(Org) < 1)
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
