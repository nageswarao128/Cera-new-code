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

        /// <summary>
        /// To add organisation details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddOrganisation()
        {
            return View();
        }

        /// <summary>
        /// To get available organisation details
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ManageOrganization()
        {
            IEnumerable<ManageOrg> manageOrg = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CERAData/ManageOrganization"))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        manageOrg = JsonConvert.DeserializeObject<List<ManageOrg>>(apiresponse);
                        ViewBag.cloudDetails = manageOrg.ToList();
                        return View(manageOrg.ToList());
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Cera");
                    }
                }
            }

        }

        /// <summary>
        /// To add organisation details
        /// </summary>
        /// <param name="orgModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddOrganisation(AddOrgModel orgModel)
        {
            if (orgModel.CloudProviderName == "Azure")
            {
                orgModel.FullyQualifiedClassName = "CERA.Azure.CloudService.CeraAzureApiService";
                orgModel.DllPath = "../wwwroot/CERA.Azure.CloudService.dll";
                //orgModel.DllPath = "../CERA.Azure.CloudService/bin/Debug/netstandard2.1/CERA.Azure.CloudService.dll";
            }
            else
            {
                orgModel.FullyQualifiedClassName = "CERA.AWS.CloudService.CeraAWSApiService";
                orgModel.DllPath = "../wwwroot/CERA.AWS.CloudService.dll";
                //orgModel.DllPath = "../CERA.AWS.CloudServices/bin/Debug/netstandard2.1/CERA.AWS.CloudService.dll";
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync<AddOrgModel>($"{SyncApiUrl}/api/Manage/AddOrganisation", orgModel))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ManageOrganization", "Cera");
                       
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Cera");
                    }
                }
            }
        }

        /// <summary>
        /// To get cloud details of the user
        /// </summary>
        /// <returns></returns>
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
