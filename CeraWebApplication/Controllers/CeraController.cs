using CERA.AuthenticationService;
using CeraWebApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using CeraWebApplication.Utility;

namespace CeraWebApplication.Controllers
{
    [Authorize]
    public class CeraController : Controller
    {
        private readonly ILogger<CeraController> _logger;
        
        public CeraController(ILogger<CeraController> logger)
        {
            _logger = logger;            
        }
  
        const string SyncApiUrl = Utilities.SyncApiUrl;
        const string DataApiUrl = Utilities.DataApiUrl;
        /// <summary>
        /// This method gives the home page view
        /// </summary>
        /// <returns></returns>
        public IActionResult LandingPage()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            UserDetails userDetails = new UserDetails();
            UserModel user = new UserModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync<LoginModel>($"{SyncApiUrl}/api/Login/Login", loginModel))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    if (apiresponse.Contains("userName"))
                    {
                        userDetails = JsonConvert.DeserializeObject<UserDetails>(apiresponse);
                        HttpContext.Session.SetString("userId", userDetails.id);
                        using (var result = await httpClient.GetAsync($"{SyncApiUrl}/api/Manage/GetUserProfile?name=" + userDetails.userName))
                        {
                            if (result.IsSuccessStatusCode)
                            {
                                string apiResult = await result.Content.ReadAsStringAsync();
                                user = JsonConvert.DeserializeObject<UserModel>(apiResult);

                            }
                        }
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, userDetails.userName));
                        foreach(var item in user.roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));
                        }
                        //var claims = new List<Claim>
                        //{
                        //    new Claim(ClaimTypes.Name,userDetails.userName),
                        //    new Claim(ClaimTypes.Role,userDetails.roles)
                        //};
                        var auth = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        
                        var principle = new ClaimsPrincipal(auth);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                        return RedirectToAction("DashBoard", "Cera");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
        }
        //[HttpGet]
        //public IActionResult Logout()
        //{
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> Logout() 
        {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            IEnumerable<CeraUsers> users = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/Manage/GetUsers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        users = JsonConvert.DeserializeObject<List<CeraUsers>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            return View(users.ToList());
        }
        [AllowAnonymous]
        public IActionResult AddUser()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddUser(AddCeraUser ceraUser)
        {
        
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync<AddCeraUser>($"{SyncApiUrl}/api/Login/Register",ceraUser))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                       return RedirectToAction("ManageUsers", "Cera");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser(string id)
        {
            CeraUserModel user = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/Manage/GetUser?id="+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        user = JsonConvert.DeserializeObject<CeraUserModel>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(CeraUserModel userModel)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsJsonAsync<CeraUserModel>($"{SyncApiUrl}/api/Manage/UpdateUser", userModel))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ManageUsers", "Cera");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            CeraUserModel user = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/Manage/GetUser?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        user = JsonConvert.DeserializeObject<CeraUserModel>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(CeraUserModel userModel)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{SyncApiUrl}/api/Manage/DeleteUser?id="+userModel.Id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ManageUsers", "Cera");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
        }
        public async Task<IActionResult> DashBoard()
        {
            IEnumerable<ResourceTypeCount> resourceCount = null;
            IEnumerable<ResourceTypeUsage> resourceUsage = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceTypeCount"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        resourceCount = JsonConvert.DeserializeObject<List<ResourceTypeCount>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/UsageByMonth"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        resourceUsage = JsonConvert.DeserializeObject<List<ResourceTypeUsage>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            IEnumerable<CeraSubscription> subscriptions = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBSubscriptions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        subscriptions = JsonConvert.DeserializeObject<List<CeraSubscription>>(apiResponse);
                        ViewBag.subscriptions = subscriptions.ToList();
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            
            List<UserCloud> clouds = new List<UserCloud>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CERAData/GetUserClouds"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        clouds = JsonConvert.DeserializeObject<List<UserCloud>>(apiResponse);
                        
                        ViewBag.clouds = clouds.ToList();
                    }
                    else
                    {
                        return View("Error", "Cera");
                    }
                }
            }
            List<ResourceTagsCountVM> resourceTags = new List<ResourceTagsCountVM>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CERAData/GetResourceTagsCount"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        resourceTags = JsonConvert.DeserializeObject<List<ResourceTagsCountVM>>(apiResponse);
                        
                        ViewBag.tags = resourceTags.ToList();
                    }
                    else
                    {
                        return View("Error", "Cera");
                    }
                }
            }
            List<Locations> locations = new List<Locations>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CERAData/GetResourceLocations"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        locations = JsonConvert.DeserializeObject<List<Locations>>(apiResponse);

                        ViewBag.locations = locations.ToList();
                    }
                    else
                    {
                        return View("Error", "Cera");
                    }
                }
            }
            List<locationFilter> filter = new List<locationFilter>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CERAData/GetMapLocationsFilter"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        filter = JsonConvert.DeserializeObject<List<locationFilter>>(apiResponse);

                        ViewBag.mapdataFilter = filter.ToList();
                    }
                    else
                    {
                        return View("Error", "Cera");
                    }
                }
            }

            List<DashboardCountModel> dashboardCount = new List<DashboardCountModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CERAData/GetDashboardCount"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        dashboardCount = JsonConvert.DeserializeObject<List<DashboardCountModel>>(apiResponse);

                        ViewBag.dashboardCount = dashboardCount.ToList();
                    }
                    else
                    {
                        return View("Error", "Cera");
                    }
                }
            }

            ViewBag.count = resourceCount.OrderBy(x=>x.ResourceType== "Others").ThenBy(x=>x.ResourceType=="Network").ThenBy(x=>x.ResourceType== "Storage").ThenBy(x=>x.ResourceType=="Compute");
            ViewBag.usage = resourceUsage;
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> UsageCloudByMonth(string cloudprovider)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/UsageCloudByMonth?cloudprovider=" + cloudprovider))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }

        }
        [HttpGet]
        public async Task<JsonResult> GetResourceTagsCloudCount(string cloudprovider)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceTagsCloudCount?cloudprovider=" + cloudprovider))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetResourceUsageByLocation(string location)
        {
            using(var httpclient = new HttpClient())
            {
                using(var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/ResourceUsageByLocation?location="+location))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetResourceCloudCount(string cloudprovider)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceCloudCount?cloudprovider=" + cloudprovider))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetResourcetypeCloudCount(string location,string cloudprovider)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceTypecloudCount?location=" + location+"&cloudprovider="+cloudprovider))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        public async Task<JsonResult> ResourceSubscriptionCloudspentUsage(string cloudprovider, string subscriptionid)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/ResourceSubscriptionCloudspentUsage?cloudprovider=" + cloudprovider + "&subscriptionid=" + subscriptionid))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        public async Task<JsonResult> GetResourceSubscriptionCloudTagsCount(string cloudprovider, string subscriptionid)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceSubscriptionCloudTagsCount?cloudprovider=" + cloudprovider + "&subscriptionid=" + subscriptionid))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
  public async Task<JsonResult> UsageSubscriptionByMonth(string cloudprovider, string subscriptionid)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/UsageSubscriptionByMonth?cloudprovider=" + cloudprovider + "&subscriptionid=" + subscriptionid))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetResourceCloudTagsCount(string location, string cloudprovider)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceCloudTagsCount?location=" + location + "&cloudprovider=" + cloudprovider))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }

        public async Task<JsonResult> GetSubscriptionLocationList(string location, string cloudprovider, string subscriptionid)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetSubscriptionLocationList?location=" + location + "&cloudprovider=" + cloudprovider + "&subscriptionid="+ subscriptionid))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        public async Task<JsonResult> ResourceSubscriptionCloudUsage(string location, string cloudprovider, string subscriptionid)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/ResourceSubscriptionCloudUsage?location=" + location + "&cloudprovider=" + cloudprovider + "&subscriptionid=" + subscriptionid))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        public async Task<JsonResult> GetResourceSubscriptionCloudTagsCount(string location, string cloudprovider, string subscriptionid)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceSubscriptionCloudTagsCounts?location=" + location + "&cloudprovider=" + cloudprovider + "&subscriptionid=" + subscriptionid))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
         public async Task<JsonResult> GetSubscriptionTypeList(string subscriptionId, string cloudprovider)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetSubscriptionTypeList?subscriptionid=" + subscriptionId + "&cloudprovider=" + cloudprovider))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        public async Task<JsonResult> GetDashboardCountFilters(string location, string cloudprovider)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetDashboardCountFilters?location=" + location + "&cloudprovider=" + cloudprovider))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> userprofile()
        {
            string name = HttpContext.User.Identity.Name;
           
          UserModel user = new UserModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/Manage/GetUserProfile?name=" +name))
                {
                    if (response.IsSuccessStatusCode)
                    {                      
                        string apiResponse = await response.Content.ReadAsStringAsync();                      
                       user = JsonConvert.DeserializeObject<UserModel>(apiResponse);
                        ViewBag.data = user;
                        return View(user);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }

           
        }
        
  public async Task<JsonResult> GetDashboardCountLocation(string location)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetDashboardCountLocation?location=" + location))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        public async Task<JsonResult> GetSubscription()
        {
            IEnumerable<CeraSubscription> subscriptions = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBSubscriptions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        subscriptions = JsonConvert.DeserializeObject<List<CeraSubscription>>(apiResponse);
                    }
                   
                }
            }
            return Json(subscriptions);
        }
        public async Task<JsonResult> GetDashboardCountCloud(string cloudprovider)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetDashboardCountCloud?cloudprovider=" + cloudprovider))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        public async Task<JsonResult> GetDashboardSubscriptionCountFilters(string cloudprovider, string subscriptionid)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetDashboardSubscriptionCountFilters?cloudprovider=" + cloudprovider + "&subscriptionid=" + subscriptionid))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
  public async Task<JsonResult> GetDashboardSubscriptionLocationFilters(string location, string cloudprovider, string subscriptionid)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetDashboardSubscriptionLocationFilters?location=" + location + "&cloudprovider=" + cloudprovider +"&subscriptionid=" + subscriptionid))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }

        [HttpGet]
        public async Task<JsonResult> ResourceCloudUsage(string location, string cloudprovider)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/ResourceCloudUsage?location=" + location + "&cloudprovider=" + cloudprovider))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetResourceCountByLocation(string location)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceTypeCountByLocation?location=" + location))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetResourceTagsByLocation(string location)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceTagsCountByLocation?location=" + location))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetResourceByLocation(string location)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceByLocations?location=" + location))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return Json(apiResponse);
                    }
                    return null;
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetResourceTypeUsage()
        {
            IEnumerable<ResourceTypeUsage> resourceTypeUsages = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetResourceTypeUsageDetails"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        resourceTypeUsages = JsonConvert.DeserializeObject<List<ResourceTypeUsage>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                    ViewBag.usage = apiResponse;
                }
                
            }
            return View(resourceTypeUsages.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> GetResourceHealth()
        {
            IEnumerable<CeraResourceHealth> resourceHealths = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBResourceHealth"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        resourceHealths = JsonConvert.DeserializeObject<List<CeraResourceHealth>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            return View(resourceHealths.ToList());
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
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBSubscriptions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        subscriptions = JsonConvert.DeserializeObject<List<CeraSubscription>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
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
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/Cera/GetCloudSubscriptions"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
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
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBResourceGroups"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        ResourceGroups = JsonConvert.DeserializeObject<List<CeraResourceGroup>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            ViewBag.data = ResourceGroups.ToList();
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
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/cera/GetCloudResourceGroups"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
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
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBStorageAccount"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        StorageAccounts = JsonConvert.DeserializeObject<List<CeraStorageAccount>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            ViewBag.data = StorageAccounts.ToList();
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
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/cera/GetCloudStorageAccount"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
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
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBResources"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        Resources = JsonConvert.DeserializeObject<List<CeraResources>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
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
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/cera/GetCloudResources"))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
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
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBVM"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        CeraVMs = JsonConvert.DeserializeObject<List<CeraVM>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            ViewBag.data = CeraVMs.ToList();
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
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/cera/GetCloudVM"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
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
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBSqlServer"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        sqlServers = JsonConvert.DeserializeObject<List<CeraSqlServer>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
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
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/cera/GetCloudSqlServer"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
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
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBTenants"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        tenants = JsonConvert.DeserializeObject<List<CeraTenants>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
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
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/cera/GetCloudTenants"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
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
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBWebApps"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        webApps = JsonConvert.DeserializeObject<List<CeraWebApps>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            ViewBag.data = webApps.ToList();
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
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/cera/GetCloudWebApps"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
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
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBAppServicePlans"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        appServicePlans = JsonConvert.DeserializeObject<List<CeraAppServicePlans>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
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
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/cera/GetCloudAppServicePlans"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
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
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBDisks"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        Disks = JsonConvert.DeserializeObject<List<CeraDisks>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            return View(Disks.ToList());
        }
        public IActionResult Notifications()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAdvisorRecommendationsDetails()
        {
            IEnumerable<AdvisorRecommendations> advisorRecommendations  = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetAdvisorRecommendations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        advisorRecommendations = JsonConvert.DeserializeObject<List<AdvisorRecommendations>>(apiResponse);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            return View(advisorRecommendations.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> GetPolicyDetails()
        {
            IEnumerable<CeraPolicies> policies = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBPolicies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        policies = JsonConvert.DeserializeObject<List<CeraPolicies>>(apiResponse);
                        ViewBag.data = policies.ToList();
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            IEnumerable<CeraSubscription> subscriptions = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetDBSubscriptions"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        subscriptions = JsonConvert.DeserializeObject<List<CeraSubscription>>(apiResponse);
                        ViewBag.subscriptions = subscriptions;
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            IEnumerable<PolicyRules> policyRules = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetPolicyRules"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        policyRules = JsonConvert.DeserializeObject<List<PolicyRules>>(apiResponse);
                        ViewBag.policyRules = policyRules;
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            return View(policies.ToList());
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
                using (var response = await httpClient.GetAsync($"{SyncApiUrl}/api/cera/GetCloudDisks"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return View("GetCloudSubscriptionDetails");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> SyncCloudData(string TenantId)
        {
            RequestBaseVm request = new RequestBaseVm();
            using (var httpClient = new HttpClient())
            {
                
                string accessToken = HttpContext.Session.GetString("accessToken");
                request.tenantId = TenantId;
                request.token = accessToken;
                using (var response = await httpClient.PostAsJsonAsync<RequestBaseVm>($"{SyncApiUrl}/api/Cera/SyncCloudData",request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return this.Json(response);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddPolicyRules(List<PolicyRules> data)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync<List<PolicyRules>>($"{SyncApiUrl}/api/cera/AddPolicyRules",data))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetPolicyDetails","Cera");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUsageReportsData()
        {
            IEnumerable<CostUsage> costUsage = null;
            IEnumerable<UsageByLocation> usageByLocations = null;
            IEnumerable<UsageByResourceGroup> usageByResourceGroups = null;
            IEnumerable<UsageHistoryBymonth> usageHistoryBymonth = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/UsageByMonth"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        costUsage = JsonConvert.DeserializeObject<List<CostUsage>>(apiResponse);
                        ViewBag.UsageByMonth = costUsage;
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/UsageHistory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        costUsage = JsonConvert.DeserializeObject<List<CostUsage>>(apiResponse);
                        ViewBag.UsageHistory = costUsage;
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetUsageByLocation"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        usageByLocations = JsonConvert.DeserializeObject<List<UsageByLocation>>(apiResponse);
                        ViewBag.UsageByLocation = usageByLocations;
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetUsageByResourceGroup"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        usageByResourceGroups = JsonConvert.DeserializeObject<List<UsageByResourceGroup>>(apiResponse);
                        ViewBag.UsageByResourceGroup = usageByResourceGroups;
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/UsageHistoryByMonth"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        usageHistoryBymonth = JsonConvert.DeserializeObject<List<UsageHistoryBymonth>>(apiResponse);
                        ViewBag.UsageHistoryByMonth = usageHistoryBymonth;
                        ViewBag.Months = usageHistoryBymonth.Select(x => x.usageMonth).Distinct();
                        var abc = usageHistoryBymonth.OrderBy(o => DateTime.Parse(o.usageMonth.ToString()));
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> GetUsageChartsData()
        {
            IEnumerable<CostUsage> costUsage = null;
            IEnumerable<UsageByLocation> usageByLocations = null;
            IEnumerable<UsageByResourceGroup> usageByResourceGroups = null;
            IEnumerable<UsageHistoryBymonth> usageHistoryBymonth = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/UsageByMonth"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        costUsage = JsonConvert.DeserializeObject<List<CostUsage>>(apiResponse);
                        ViewBag.UsageByMonth = costUsage;
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/UsageHistory"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        costUsage = JsonConvert.DeserializeObject<List<CostUsage>>(apiResponse);
                        ViewBag.UsageHistory = costUsage;
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetUsageByLocation"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        usageByLocations = JsonConvert.DeserializeObject<List<UsageByLocation>>(apiResponse);
                        ViewBag.UsageByLocation = usageByLocations;
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/GetUsageByResourceGroup"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        usageByResourceGroups = JsonConvert.DeserializeObject<List<UsageByResourceGroup>>(apiResponse);
                        ViewBag.UsageByResourceGroup = usageByResourceGroups;
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CeraData/UsageHistoryByMonth"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        usageHistoryBymonth = JsonConvert.DeserializeObject<List<UsageHistoryBymonth>>(apiResponse);
                        ViewBag.UsageHistoryByMonth = usageHistoryBymonth;
                        ViewBag.Months = usageHistoryBymonth.Select(x => x.usageMonth).Distinct();
                        var abc = usageHistoryBymonth.OrderBy(o => DateTime.Parse(o.usageMonth.ToString()));
                    }
                    else
                    {
                        return RedirectToAction("Error", "Cera");
                    }
                }
            }
            return View();

        }
        public async Task<JsonResult> GetUserClouds()
        {

            List<UserCloud> clouds = new List<UserCloud>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{DataApiUrl}/api/CERAData/GetUserClouds"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        clouds = JsonConvert.DeserializeObject<List<UserCloud>>(apiResponse);
                        
                        //ViewBag.clouds = clouds.Select(x => x.cloudName).ToList();
                        return Json(clouds);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        [HttpPost]
        public JsonResult UpdateAccesstoken(string accessToken)
        {
            HttpContext.Session.SetString("accessToken", accessToken);
            return Json("Session Created");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
