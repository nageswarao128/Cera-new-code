using CeraWebApplication.Models;
using CeraWebApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CeraWebApplication.ViewComponents
{
    [ViewComponent]
    public class ViewUtilities:ViewComponent
    {
        const string DataApiUrl = Utilities.DataApiUrl;
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
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
                        clouds.Add(new UserCloud
                        {
                            cloudName="All Clouds"
                        });
                        ViewBag.clouds = clouds.Select(x => x.cloudName).ToList();
                        return View();
                    }
                    else
                    {
                        return View("ErrorPage", "Cera");
                    }
                }
            }
        }
    }
}
