using CERA.Entities.Models;
using CERA.Platform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CERAGetCallAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class CERADataController : ControllerBase
    {
        private readonly ILogger<CERADataController> _logger;
        ICeraPlatform _ceraCloud;
        public CERADataController(ILogger<CERADataController> logger, ICeraPlatform ceraCloud)
        {
            _logger = logger;
            _ceraCloud = ceraCloud;

        }
        [HttpGet]
        [Authorize]
        public IEnumerable<CeraSubscription> GetDBSubscriptions()
        {
            return _ceraCloud.GetSubscriptionList();
        }
        /// <summary>
        /// This method will retrives the Resources data from the database
        /// </summary>
        /// <returns>returns Resources data from database</returns>
        [HttpGet]
        public IEnumerable<CeraResources> GetDBResources()
        {
            return _ceraCloud.GetResourcesList();
        }
        /// <summary>
        /// This method will retrives the Virtual Machines data from the database
        /// </summary>
        /// <returns>returns Virtual Machines data from database</returns>
        [HttpGet]
        public IEnumerable<CeraVM> GetDBVM()
        {
            return _ceraCloud.GetVMList();
        }
        /// <summary>
        /// This method will retrives the ResourceGroups data from the database
        /// </summary>
        /// <returns>returns ResourceGroups data from database</returns>
        [HttpGet]
        public IEnumerable<CeraResourceGroups> GetDBResourceGroups()
        {
            return _ceraCloud.GetResourceGroupsList();
        }
        /// <summary>
        /// This method will retrives the StorageAccount data from the database
        /// </summary>
        /// <returns>returns StorageAccount data from database</returns>
        [HttpGet]
        public IEnumerable<CeraStorageAccount> GetDBStorageAccount()
        {
            return _ceraCloud.GetStorageAccountList();
        }
    }
}
