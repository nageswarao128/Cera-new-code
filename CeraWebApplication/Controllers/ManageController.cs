using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CeraWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

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
       [HttpGet]
       public IActionResult AddOrganisation()
        {
            return View();
        }
       [HttpPost]
       public async Task<IActionResult> AddOrganisation(AddOrgModel orgModel)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync<AddOrgModel>("https://localhost:44389/api/Manage/AddOrganisation", orgModel))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Dashboard", "Cera");
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
