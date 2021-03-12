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
    }
}
