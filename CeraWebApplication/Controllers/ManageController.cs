using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CeraWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CeraWebApplication.Utility;
using Newtonsoft.Json;

namespace CeraWebApplication.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly ILogger<ManageController> _logger;
        public ManageController(ILogger<ManageController> logger)
        {
            _logger = logger;
        }
        const string SyncApiUrl = Utilities.SyncApiUrl;
        const string DataApiUrl = Utilities.DataApiUrl;

        [HttpGet]
       public IActionResult AddOrganisation()
        {
            return View();
        }
       [HttpPost]
       public async Task<IActionResult> AddOrganisation(AddOrgModel orgModel)
        {
            if (orgModel.CloudProviderName == "Azure")
            {
                orgModel.FullyQualifiedClassName = "CERA.Azure.CloudService.CeraAzureApiService";
                orgModel.DllPath = "../wwwroot/CERA.Azure.CloudService.dll";
            }
            else
            {
                orgModel.FullyQualifiedClassName = "CERA.AWS.CloudService.CeraAWSApiService";
                orgModel.DllPath = "../wwwroot/CERA.AWS.CloudService.dll";
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync<AddOrgModel>($"{SyncApiUrl}/api/Manage/AddOrganisation", orgModel))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return this.Json(apiresponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Cera");
                    }
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCloudCredentials()
        {
            IEnumerable<UserCloudDetails> cloudDetails = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CERAData/GetClientCloudDetails"))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        cloudDetails = JsonConvert.DeserializeObject<List<UserCloudDetails>>(apiresponse);
                        ViewBag.cloudDetails = cloudDetails.ToList();
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Cera");
                    }
                }
            }
        }
    }
}
