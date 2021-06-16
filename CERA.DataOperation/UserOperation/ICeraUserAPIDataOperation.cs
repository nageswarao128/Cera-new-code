
using CERA.Entities.ViewModels;
using System.Collections.Generic;

namespace CERA.DataOperation
{
    public partial interface ICeraDataOperation
    {
        public List<CeraPlatformConfigViewModel> GetClientOnboardedPlatforms(string ClientName);
        public int OnBoardClientPlatform(AddClientPlatformViewModel platform);
        public int OnBoardOrganization(AddOrganizationViewModel OrgDetails);
        public int OnBoardCloudProvider(AddCloudPluginViewModel plugin);
        public List<CeraResourceTypeUsage> ResourceUsage();
        public List<CeraResourceTypeUsage> ResourceUsage(string location);
        public List<ResourceTypeCount> GetResourceTypeCount();
        public List<ResourceTypeCount> GetResourceTypeCount(string location);
        public List<ResourceHealthViewDTO> ResourceHealth();
        public List<UserClouds> GetUserClouds();
        public List<ClientCloudDetails> GetClientCloudDetails(string clientName);
        public List<ResourceTagsCount> GetResourceTagsCount();
        public List<ResourceTagsCount> GetResourceTagsCount(string location);
        public List<ResourceLocations> GetResourceLocations();
        public List<ResourceLocations> GetResourceLocations(string location);
    }
}
