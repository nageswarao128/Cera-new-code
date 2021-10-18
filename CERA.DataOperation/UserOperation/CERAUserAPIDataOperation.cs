using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            if (ClientName == null)
            {
                _logger.LogError("Client Name is null while calling GetClientOnboardedPlatforms");
            }
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
        public List<ClientCloudDetails> GetClientCloudDetails(string clientName)
        {
            var data = from client in _dbContext.Clients
                       where
                        client.ClientName == clientName
                       join cloud in _dbContext.ClientCloudPlugins
                        on client.Id equals cloud.Client.Id
                       select new ClientCloudDetails
                       {
                           clientId = cloud.ClientId,
                           TenantId= cloud.TenantId
                       };
            return data.ToList();
        }
        /// <summary>
        /// This will adds a organisation details into database
        /// </summary>
        /// <param name="OrgDetails"></param>
        /// <returns>returns 1 or 0</returns>
        public int OnBoardOrganization(AddOrganizationViewModel OrgDetails)
        {
            if (_dbContext.Clients.Any(x => x.ClientName == OrgDetails.OrganizationName))
            {
                return 0;
            }
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
            if (_dbContext.CloudPlugIns.Any(x => x.CloudProviderName == plugin.CloudProviderName))
            {
                return 0;
            }

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

        public List<UserClouds> GetUserClouds()
        {
            List<UserClouds> clouds = new List<UserClouds>();
            var data = _dbContext.CloudPlugIns.Select(x=>x.CloudProviderName).ToList();
            foreach (var item in data)
            {
                clouds.Add(new UserClouds
                {
                    cloudName = item
                });
            }
            return clouds;
        }
        public List<CeraResourceTypeUsage> ResourceUsage()
        {
            var data = _spContext.resourceUsage.FromSqlRaw<CeraResourceTypeUsage>("[dbo].[Sp_ResourceSpent]").ToList();
            return data;
        }
        public List<ManageOrg> ManageOrganization()
        {
            var data = _spContext.manageorg.FromSqlRaw<ManageOrg>("[dbo].[sp_ManageOrg]").ToList();
            return data;
        }
        public List<CeraResourceTypeUsage> ResourceUsage(string location)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;
            var data = _spContext.resourceUsage.FromSqlRaw<CeraResourceTypeUsage>("[dbo].[Sp_ResourceLocationSpent] @location", parameter).ToList();
            return data;
        }
        public List<CeraResourceTypeUsage> ResourceCloudUsage(string location,string cloudprovider)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";

            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@cloudprovider";

            parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter1.Value = cloudprovider;

            var data =  _spContext.resourceUsage.FromSqlRaw<CeraResourceTypeUsage>("[dbo].[Sp_ResourceCloudLocationSpent] @location,@cloudprovider", parameter, parameter1).ToList();
            return data;
        }

        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudspentUsage(string cloudprovider, string subscriptionid)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@cloudprovider";

            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = cloudprovider;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@subscriptionid";

            parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter1.Value = subscriptionid;

            var data = _spContext.resourceUsage.FromSqlRaw<CeraResourceTypeUsage>("[dbo].[Sp_ResourceSubscriptionCloudSpent] @cloudprovider,@subscriptionid", parameter, parameter1).ToList();
            return data;
        }
       
        public List<ResourceTagsCount> GetResourceTagsCount()
        {
            var data = _spContext.resourceTags.FromSqlRaw<ResourceTagsCount>("[dbo].[Sp_ResourceTags]").ToList();
            return data;
        }

     
        public List<ResourceTagsCount> GetResourceTagsCloudCount(string cloudprovider)
        {

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@cloudprovider";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = cloudprovider;
            var data = _spContext.resourceTags.FromSqlRaw<ResourceTagsCount>("[dbo].[Sp_ResourceCloudTags] @cloudprovider", parameter).ToList();
            return data;
        }
        public List<ResourceTagsCount> GetResourceTagsCount(string location)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;
            var data = _spContext.resourceTags.FromSqlRaw<ResourceTagsCount>("[dbo].[Sp_ResourceLocationTags] @location", parameter).ToList();
            return data;
        }
        public List<ResourceTagsCount> GetResourceCloudTagsCount(string location,string cloudprovider)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";

            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@cloudprovider";

            parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter1.Value = cloudprovider;

            var data = _spContext.resourceTags.FromSqlRaw<ResourceTagsCount>("[dbo].[Sp_ResourceCloudLocationTags] @location,@cloudprovider", parameter, parameter1).ToList();
            return data;
        }
        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudUsage(string location, string cloudprovider,string subscriptionid)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";

            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@cloudprovider";

            parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter1.Value = cloudprovider;
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@subscriptionid";

            parameter2.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter2.Value = subscriptionid;
            var data = _spContext.resourceUsage.FromSqlRaw<CeraResourceTypeUsage>("[dbo].[Sp_ResourceSubscriptionCloudLocationSpent] @location,@cloudprovider,@subscriptionid", parameter, parameter1,parameter2).ToList();
            return data;
        }
        public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCount(string location, string cloudprovider,string subscriptionid)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";

            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@cloudprovider";

            parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter1.Value = cloudprovider;

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@subscriptionid";

            parameter2.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter2.Value = subscriptionid;
            var data = _spContext.resourceTags.FromSqlRaw<ResourceTagsCount>("[dbo].[Sp_ResourceSubscriptionCloudLocationTags] @location,@cloudprovider,@subscriptionid", parameter, parameter1,parameter2).ToList();
            return data;
        }
        public List<ResourceTypeCount> GetResourceTypeCount()
        
        {
            
            var data = _spContext.resourceTypeCount.FromSqlRaw<ResourceTypeCount>("[dbo].[Sp_ResourceCount]").ToList();
            
            return data;
        }
        public List<ResourceTypeCount> GetResourceCloudCount(string cloudprovider)

        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@cloudprovider";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = cloudprovider;
            var data = _spContext.resourceTypeCount.FromSqlRaw<ResourceTypeCount>("[dbo].[Sp_ResourceCloudSpent] @cloudprovider", parameter).ToList();

            return data;
        }

        public List<ResourceTypeCount> GetResourceTypeCount(string location)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;
            var data = _spContext.resourceTypeCount.FromSqlRaw<ResourceTypeCount>("[dbo].[Sp_ResourceLocationCount] @location", parameter).ToList();
            return data;
        }
        public List<ResourceTypeCount> GetSubscriptionTypeList(string subscriptionId,string cloudprovider)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@subscriptionid";

            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = subscriptionId;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@cloudprovider";

            parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter1.Value = cloudprovider;


            var data = _spContext.resourceTypeCount.FromSqlRaw<ResourceTypeCount>("[dbo].[sp_subscriptionfilter] @subscriptionid,@cloudprovider", parameter,parameter1).ToList();
            return data;
        }
        public List<ResourceTypeCount> GetSubscriptionLocationList(string location,string cloudprovider, string subscriptionid)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";

            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@cloudprovider";

            parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter1.Value = cloudprovider;
            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@subscriptionid";

            parameter2.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter2.Value = subscriptionid;

            var data = _spContext.resourceTypeCount.FromSqlRaw<ResourceTypeCount>("[dbo].[Sp_SubscriptionLocationfilter] @location,@cloudprovider,@subscriptionid", parameter, parameter1,parameter2).ToList();
            return data;
        }
        public List<ResourceTypeCount> GetResourceTypecloudCount(string location,string cloudprovider)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";
         
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@cloudprovider";

            parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter1.Value = cloudprovider;

            var data = _spContext.resourceTypeCount.FromSqlRaw<ResourceTypeCount>("[dbo].[Sp_ResourceCloudLocationcount] @location,@cloudprovider", parameter,parameter1).ToList();
            return data;
        }
       
        public List<ResourceHealthViewDTO> ResourceHealth()
        {
            
            var ResourceHealth = from resources in _dbContext.Resources
                                 join healths in _dbContext.ResourceHealth
                                 on resources.Id equals healths.ResourceId
                                 select new ResourceHealthViewDTO
                                 {
                                     ResourceName = resources.Name,
                                     ResourceGroupName = resources.ResourceGroupName,
                                     ResourceType = resources.ResourceProviderNameSpace.Remove(0, 10),
                                     Location = healths.Location,
                                     AvailabilityState = healths.AvailabilityState
                                 };
            return ResourceHealth.ToList();
        }

        public List<ResourceLocations> GetResourceLocations()
        {
            var data = _spContext.Locations.FromSqlRaw<ResourceLocations>("[dbo].[Sp_MapData]").ToList();
          
            return data;
        }
        public List<locationFilter> GetMapLocationsFilter()
        {
            var data = _spContext.locationfilters.FromSqlRaw<locationFilter>("[dbo].[Sp_MapDataFilter]").ToList();

            return data;
        }



        public List<ResourceLocations> GetResourceLocations(string location)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;
            var data = _spContext.Locations.FromSqlRaw<ResourceLocations>("[dbo].[Sp_MapLocationData] @location", parameter).ToList();
            return data;
        }

        public List<CostUsage> UsageByMonth()
        {
            return _spContext.usageSp.FromSqlRaw<CostUsage>("[dbo].[Sp_UsageByMonth]").ToList();
        }
        public List<CostUsage> UsageCloudByMonth(string cloudprovider)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@cloudprovider";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = cloudprovider;
            var data= _spContext.usageSp.FromSqlRaw<CostUsage>("[dbo].[Sp_UsageCloudByMonth] @cloudprovider", parameter).ToList();
            return data;
        }
        public List<CostUsage> UsageSubscriptionByMonth(string cloudprovider, string subscriptionid)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@cloudprovider";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = cloudprovider;
            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@subscriptionid";
            parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter1.Value = subscriptionid;
            var data = _spContext.usageSp.FromSqlRaw<CostUsage>("[dbo].[Sp_UsageSubscriptionByMonthh] @cloudprovider,@subscriptionid", parameter,parameter1).ToList();
            return data;
        }
        public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCount(string cloudprovider,string subscriptionid)
        {

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@cloudprovider";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = cloudprovider;
            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@subscriptionid";
            parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter1.Value = subscriptionid;
            var data = _spContext.resourceTags.FromSqlRaw<ResourceTagsCount>("[dbo].[Sp_ResourceSubscriptionCloudTags] @cloudprovider,@subscriptionid", parameter,parameter1).ToList();
            return data;
        }
        public List<CostUsage> UsageHistory()
        {
            return _spContext.usageSp.FromSqlRaw<CostUsage>("[dbo].[Sp_UsageByHistory]").ToList();
        }
        public List<UsageHistoryByMonth> UsageHistoryByMonth()
        {
            return _spContext.usageHistoryByMonth.FromSqlRaw<UsageHistoryByMonth>("[dbo].[Sp_UsageHistoryByMonth]").ToList();
        }
        public List<UsageByLocation> GetUsageByLocation()
        {
            return _spContext.usageByLocation.FromSqlRaw<UsageByLocation>("[dbo].[Sp_UsageByLocation]").ToList();
        }
        public List<UsageByResourceGroup> GetUsageByResourceGroup()
        {
            return _spContext.usageByResourceGroup.FromSqlRaw<UsageByResourceGroup>("[dbo].[Sp_UsageByResourceGroup]").ToList();
        }
        public List<DashboardCountModel> GetDashboardCount()
        {
            try
            {
                return _spContext.dashboardCounts.FromSqlRaw<DashboardCountModel>("Sp_Dashboardcounts").ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       
        public List<DashboardCountModel> GetDashboardCountFilters(string location,string cloudprovider)
        {
            try
            {

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@location";

                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = location;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@cloudprovider";

                parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter1.Value = cloudprovider;

                var data = _spContext.dashboardCounts.FromSqlRaw<DashboardCountModel>("[dbo].[Sp_DashboardCountFilters] @location,@cloudprovider", parameter, parameter1).ToList();
                return data;
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DashboardCountModel> GetDashboardSubscriptionCountFilters( string cloudprovider, string subscriptionid)
        {
            try
            {

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@cloudprovider";

                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = cloudprovider;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@subscriptionid";

                parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter1.Value = subscriptionid;

                var data = _spContext.dashboardCounts.FromSqlRaw<DashboardCountModel>("[dbo].[Sp_DashboardSubscriptionCountCloud] @cloudprovider,@subscriptionid", parameter, parameter1).ToList();
                return data;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<DashboardCountModel> GetDashboardSubscriptionLocationFilters(string location, string cloudprovider,string subscriptionid)
        {
            try
            {

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@location";

                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = location;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@cloudprovider";

                parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter1.Value = cloudprovider;
                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@subscriptionid";

                parameter2.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter2.Value = subscriptionid;

                var data = _spContext.dashboardCounts.FromSqlRaw<DashboardCountModel>("[dbo].[Sp_DashboardSubscriptionCountFilters] @location,@cloudprovider,@subscriptionid", parameter, parameter1,parameter2).ToList();
                return data;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DashboardCountModel> GetDashboardCountLocation(string location)
        {
            try
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@location";
                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = location;
                var data= _spContext.dashboardCounts.FromSqlRaw<DashboardCountModel>("[dbo].[Sp_DashboardCountLocation] @location", parameter).ToList();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<DashboardCountModel> GetDashboardCountCloud(string cloudprovider)
        {
            try
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@cloudprovider";
                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = cloudprovider;
                var data = _spContext.dashboardCounts.FromSqlRaw<DashboardCountModel>("[dbo].[Sp_DashboardCountCloud] @cloudprovider", parameter).ToList();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
