using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            var data = _dbContext.CloudPlugIns.ToList();
            foreach (var item in data)
            {
                clouds.Add(new UserClouds
                {
                    cloudName = item.CloudProviderName
                });
            }
            return clouds;
        }
        public List<CeraResourceTypeUsage> ResourceUsage()
        {
            var data = _spContext.resourceUsage.FromSqlRaw<CeraResourceTypeUsage>("[dbo].[Sp_ResourceSpent]").ToList();
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
        public List<ResourceTagsCount> GetResourceTagsCount()
        {
            var data = _spContext.resourceTags.FromSqlRaw<ResourceTagsCount>("[dbo].[Sp_ResourceTags]").ToList();
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
        public List<ResourceTypeCount> GetResourceTypeCount()
        
        {
            
            var data = _spContext.resourceTypeCount.FromSqlRaw<ResourceTypeCount>("[dbo].[Sp_ResourceCount]").ToList();
            
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

        public List<ResourceLocations> GetResourceLocations(string location)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@location";
            parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
            parameter.Value = location;
            var data = _spContext.Locations.FromSqlRaw<ResourceLocations>("[dbo].[Sp_MapLocationData] @location", parameter).ToList();
            return data;
        }
    }
}
