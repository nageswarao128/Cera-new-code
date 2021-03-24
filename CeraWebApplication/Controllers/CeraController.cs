using CeraWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CeraWebApplication.Controllers
{
    public class CeraController : Controller
    {
        private readonly ILogger<CeraController> _logger;

        public CeraController(ILogger<CeraController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// This method gives the home page view
        /// </summary>
        /// <returns></returns>
        public IActionResult LandingPage()
        {
            return View();
        }
        /// <summary>
        /// This method will call the API to retrive subscription details from db 
        /// </summary>
        /// <returns>returns subscriptions in a view</returns>
        [HttpGet]
        public async Task<IActionResult> GetSubscriptionDetails()
        {
            IEnumerable<CeraSubscription> subscriptions = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44379/api/CeraData/GetDBSubscriptions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        subscriptions = JsonConvert.DeserializeObject<List<CeraSubscription>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Cera");
                    }
                }
            }
            return View(subscriptions.ToList());
        }
        /// <summary>
        /// This method will calls the API to sync the subscription data from cloud to db
        /// </summary>
        /// <returns>returns view page</returns>
        [HttpGet]
        public async Task<IActionResult> GetCloudSubscriptionDetails()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44389/api/Cera/GetCloudSubscriptions"))
                {                   
                    if (response.IsSuccessStatusCode)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
        }
        /// <summary>
        /// This method will call the API to retrive ResourceGroup details from db 
        /// </summary>
        /// <returns>returns ResourceGroups in a view</returns>
        [HttpGet]
        public async Task<IActionResult> GetResourceGroupDetails()
        {
            IEnumerable<CeraResourceGroup> ResourceGroups = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44379/api/CeraData/GetDBResourceGroups"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        ResourceGroups = JsonConvert.DeserializeObject<List<CeraResourceGroup>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
            return View(ResourceGroups.ToList());
        }
        /// <summary>
        /// This method will calls the API to sync the ResourceGroups data from cloud to db
        /// </summary>
        /// <returns>returns view page</returns>
        [HttpGet]
        public async Task<IActionResult> GetCloudResourceGroupDetails()
        { 
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44389/api/cera/GetCloudResourceGroups"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
        }
        /// <summary>
        /// This method will call the API to retrive StorageAccounts details from db 
        /// </summary>
        /// <returns>returns StorageAccounts in a view</returns>
        [HttpGet]
        public async Task<IActionResult> GetStorageAccountsDetails()
        {
            IEnumerable<CeraStorageAccount> StorageAccounts = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44379/api/CeraData/GetDBStorageAccount"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        StorageAccounts = JsonConvert.DeserializeObject<List<CeraStorageAccount>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
            return View(StorageAccounts.ToList());
        }
        /// <summary>
        /// This method will calls the API to sync the StorageAccount data from cloud to db
        /// </summary>
        /// <returns>returns view page</returns>
        [HttpGet]
        public async Task<IActionResult> GetCloudStorageAccountDetails()
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44389/api/cera/GetCloudStorageAccount"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
        }
        /// <summary>
        /// This method will call the API to retrive Resources details from db 
        /// </summary>
        /// <returns>returns Resources in a view</returns>
        [HttpGet]
        public async Task<IActionResult> GetResourceDetails()
        {
            IEnumerable<CeraResources> Resources = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44379/api/CeraData/GetDBResources"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        Resources = JsonConvert.DeserializeObject<List<CeraResources>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
            return View(Resources.ToList());
        }
        /// <summary>
        /// This method will calls the API to sync the Resources data from cloud to db
        /// </summary>
        /// <returns>returns view page</returns>
        [HttpGet]
        public async Task<IActionResult> GetCloudResourceDetails()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44389/api/cera/GetCloudResources"))
                {
                   
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
        }
        /// <summary>
        /// This method will call the API to retrive VM details from db 
        /// </summary>
        /// <returns>returns VM in a view</returns>
        [HttpGet]
        public async Task<IActionResult> GetVMDetails()
        {
            IEnumerable<CeraVM> CeraVMs = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44379/api/CeraData/GetDBVM"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        CeraVMs = JsonConvert.DeserializeObject<List<CeraVM>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
            return View(CeraVMs.ToList());
        }
        /// <summary>
        /// This method will calls the API to sync the VM data from cloud to db
        /// </summary>
        /// <returns>returns view page</returns>
        [HttpGet]
        public async Task<IActionResult> GetCloudVMDetails()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44389/api/cera/GetCloudVM"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
        }
        /// <summary>
        /// This method will call the API to retrive SqlServer details from db 
        /// </summary>
        /// <returns>returns SqlServer in a view</returns>
        [HttpGet]
        public async Task<IActionResult> GetSqlServerDetails()
        {
            IEnumerable<CeraSqlServer> sqlServers = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44379/api/CeraData/GetDBSqlServer"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        sqlServers = JsonConvert.DeserializeObject<List<CeraSqlServer>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
            return View(sqlServers.ToList());
        }
        /// <summary>
        /// This method will calls the API to sync the SqlServer data from cloud to db
        /// </summary>
        /// <returns>returns view page</returns>
        [HttpGet]
        public async Task<IActionResult> GetCloudSqlServerDetails()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44389/api/cera/GetCloudSqlServer"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
        }
        /// <summary>
        /// This method will call the API to retrive Tenant details from db 
        /// </summary>
        /// <returns>returns Tenant in a view</returns>
        [HttpGet]
        public async Task<IActionResult> GetTenantDetails()
        {
            IEnumerable<CeraTenants> tenants = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44379/api/CeraData/GetDBTenants"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        tenants = JsonConvert.DeserializeObject<List<CeraTenants>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
            return View(tenants.ToList());
        }
        /// <summary>
        /// This method will calls the API to sync the Tenant data from cloud to db
        /// </summary>
        /// <returns>returns view page</returns>
        [HttpGet]
        public async Task<IActionResult> GetCloudTenantDetails()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44389/api/cera/GetCloudTenants"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
        }
        /// <summary>
        /// This method will call the API to retrive WebApps details from db 
        /// </summary>
        /// <returns>returns WebApps in a view</returns>
        [HttpGet]
        public async Task<IActionResult> GetWebAppsDetails()
        {
            IEnumerable<CeraWebApps> webApps = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44379/api/CeraData/GetDBWebApps"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        webApps = JsonConvert.DeserializeObject<List<CeraWebApps>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
            return View(webApps.ToList());
        }
        /// <summary>
        /// This method will calls the API to sync the WebApps data from cloud to db
        /// </summary>
        /// <returns>returns view page</returns>
        [HttpGet]
        public async Task<IActionResult> GetCloudWebAppsDetails()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44389/api/cera/GetCloudWebApps"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
        }
        /// <summary>
        /// This method will call the API to retrive AppServicePlans details from db 
        /// </summary>
        /// <returns>returns AppServicePlans in a view</returns>
        [HttpGet]
        public async Task<IActionResult> GetAppServicePlansDetails()
        {
            IEnumerable<CeraAppServicePlans> appServicePlans = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44379/api/CeraData/GetDBAppServicePlans"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        appServicePlans = JsonConvert.DeserializeObject<List<CeraAppServicePlans>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
            return View(appServicePlans.ToList());
        }
        /// <summary>
        /// This method will calls the API to sync the AppServicePlan data from cloud to db
        /// </summary>
        /// <returns>returns view page</returns>
        [HttpGet]
        public async Task<IActionResult> GetCloudAppServicePlansDetails()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44389/api/cera/GetCloudAppServicePlans"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
        }
        /// <summary>
        /// This method will call the API to retrive Disks details from db 
        /// </summary>
        /// <returns>returns Disks in a view</returns>
        [HttpGet]
        public async Task<IActionResult> GetDisksDetails()
        {
            IEnumerable<CeraDisks> Disks = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44379/api/CeraData/GetDBDisks"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        Disks = JsonConvert.DeserializeObject<List<CeraDisks>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
            return View(Disks.ToList());
        }
        /// <summary>
        /// This method will calls the API to sync the Disks data from cloud to db
        /// </summary>
        /// <returns>returns view page</returns>
        [HttpGet]
        public async Task<IActionResult> GetCloudDisksDetails()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44389/api/cera/GetCloudDisks"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("ErrorPage", "Home");
                    }
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
