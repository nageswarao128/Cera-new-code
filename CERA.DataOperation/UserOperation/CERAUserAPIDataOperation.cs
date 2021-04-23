using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CERA.DataOperation
{
    public partial class CERADataOperation : ICeraDataOperation
    {
        /// <summary>
        /// This method will retrives PlatformName,APIClassName,DllPath data from database based on the
        /// client name
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns>returns a object containing PlatformName,APIClassName,DllPath</returns>
        public List<CeraPlatformConfigViewModel> GetClientOnboardedPlatforms(string ClientName)
        {
            var onboradedPlatforms = from clientPlugin in _dbContext.ClientCloudPlugins
                                     where clientPlugin.Client.ClientName == ClientName
                                     join cloudPlugin in _dbContext.CloudPlugIns
                                     on clientPlugin.PlugIn.Id equals cloudPlugin.Id
                                     select new CeraPlatformConfigViewModel
                                     {
                                         PlatformName = clientPlugin.PlugIn.CloudProviderName,
                                         APIClassName = clientPlugin.PlugIn.FullyQualifiedName,
                                         DllPath = clientPlugin.PlugIn.DllPath,
                                     };
            return onboradedPlatforms.ToList();
        }
        /// <summary>
        /// This will adds a organisation details into database
        /// </summary>
        /// <param name="OrgDetails"></param>
        /// <returns>returns 1 or 0</returns>
        public int OnBoardOrganization(AddOrganizationViewModel OrgDetails)
        {
            OrgDetails.UserId = Guid.NewGuid();
            _dbContext.Clients.Add(new Client()
            {
                Id = OrgDetails.UserId,
                PrimaryContactName = OrgDetails.ContactPersonName,
                ClientDescription = OrgDetails.Description,
                PrimaryEmail = OrgDetails.EmailId,
                ClientName = OrgDetails.OrganizationName,
                PrimaryPhone = OrgDetails.PhoneNo,
                PrimaryAddress = OrgDetails.PrimaryAddress
            });
            return _dbContext.SaveChanges();
        }
        /// <summary>
        /// This method will inserts the Client Cloud details into database
        /// </summary>
        /// <param name="platform"></param>
        /// <returns>returns 1 or 0</returns>
        public int OnBoardClientPlatform(AddClientPlatformViewModel platform)
        {
            var cloudPlugIn = _dbContext.CloudPlugIns.Where(x => x.CloudProviderName == platform.PlatformName).FirstOrDefault();
            var client = _dbContext.Clients.Where(x => x.ClientName == platform.OrganizationName || x.Id == platform.OrganizationId).FirstOrDefault();
            _dbContext.ClientCloudPlugins.Add(new ClientCloudPlugin()
            {
                ClientId = platform.ClientId,
                ClientSecret = platform.ClientSecret,
                Client = client,
                PlugIn = cloudPlugIn,
                TenantId = platform.TenantId
            });
            return _dbContext.SaveChanges();
        }
        /// <summary>
        /// This method will inserts the cloud platform details into database
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns>returns 1 or 0</returns>
        public int OnBoardCloudProvider(AddCloudPluginViewModel plugin)
        {
            _dbContext.CloudPlugIns.Add(new CloudPlugIn()
            {
                CloudProviderName = plugin.CloudProviderName,
                DllPath = plugin.DllPath,
                FullyQualifiedName = plugin.FullyQualifiedClassName,
                DateEnabled = DateTime.Now,
                Description = plugin.Description,
                DevContact = plugin.DevContact,
                SupportContact = plugin.SupportContact
            });
            return _dbContext.SaveChanges();
        }
        //public List<CeraResourceTypeUsage> ResourceUsage()
        //{
        //    string name = "Microsoft.Automation";
        //    var result = from resource in _dbContext.Resources
        //                 join usage in _dbContext.UsageDetails
        //                 on resource.ResourceProviderNameSpace equals usage.consumedService
        //                 select new CeraResourceTypeUsage
        //                 {
        //                     resourceType = resource.ResourceProviderNameSpace,
        //                     pretaxCost = usage.pretaxCost,
        //                     currency = usage.currency
        //                 };
        //    List<CeraResourceTypeUsage> resourceTypeUsages = result.ToList();
        //    Dictionary<string, decimal> keyValues = new Dictionary<string, decimal>();
        //    foreach(var item in resourceTypeUsages)
        //    {
        //        if (keyValues.ContainsKey(item.resourceType))
        //        {
        //            keyValues[item.resourceType] = keyValues[item.resourceType] + item.pretaxCost;
        //        }
        //        else
        //        {
        //            keyValues.Add(item.resourceType, decimal.Zero);
        //        }
        //    }
   
        //    //return result.ToList();
        //    return null;
        //}
        public List<CeraResourceTypeUsage> ResourceUsage()
        {
            List<CeraResourceTypeUsage> resourceTypeUsages = new List<CeraResourceTypeUsage>();
            var data = _dbContext.UsageDetails.ToList();
            Dictionary<string, decimal> keyValues = new Dictionary<string, decimal>();
            foreach(var item in data)
            {
                if (keyValues.ContainsKey(item.consumedService))
                {
                    keyValues[item.consumedService] = keyValues[item.consumedService] + item.pretaxCost;
                }
                else
                {
                   keyValues.Add(item.consumedService, item.pretaxCost);
                }   
            }
            foreach(var item in keyValues)
            {
                resourceTypeUsages.Add(new CeraResourceTypeUsage
                {
                    resourceType = item.Key.Remove(0,10),
                    pretaxCost = item.Value
                });
            }
            return resourceTypeUsages;
        }
        public List<ResourceTypeCount> GetResourceTypeCount()
        {
            List<ResourceTypeCount> resourceTypeCount = new List<ResourceTypeCount>();
            var data = _dbContext.Resources.ToList();
            Dictionary<string, int> keyValues = new Dictionary<string, int>();
            foreach (var item in data)
            {
                if (item.ResourceProviderNameSpace == "Microsoft.Automation" || item.ResourceProviderNameSpace == "Microsoft.Network" || item.ResourceProviderNameSpace == "Microsoft.Storage" || item.ResourceProviderNameSpace == "Microsoft.Compute")
                {

                    if (!keyValues.ContainsKey(item.ResourceProviderNameSpace))
                    {
                        keyValues.Add(item.ResourceProviderNameSpace, 1);   
                    }
                    else
                    {
                        keyValues[item.ResourceProviderNameSpace]++;
                    }
                }
                else
                {
                    if (!keyValues.ContainsKey("Microsoft.Others"))
                    {

                        keyValues.Add("Microsoft.Others", 1);
                        
                    }
                    else
                    {
                        keyValues["Microsoft.Others"]++;
                        
                    }
                }
            }
            foreach (var item in keyValues)
            {
                resourceTypeCount.Add(new ResourceTypeCount
                {
                    resourceType = item.Key.Remove(0,10),
                    count = item.Value
                });
            }
            return resourceTypeCount;
        }
    }
}
