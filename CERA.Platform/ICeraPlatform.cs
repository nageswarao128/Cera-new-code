using CERA.CloudService;
using CERA.Entities.ViewModels;
using System.Collections.Generic;

namespace CERA.Platform
{
    public interface ICeraPlatform : ICeraCloudApiService
    {
        public string ClientName { get; set; }
        public List<CeraPlatformConfigViewModel> GetClientOnboardedPlatforms(string ClientName);
        public int OnBoardClientPlatform(AddClientPlatformViewModel platform);
        public int OnBoardOrganization(AddOrganizationViewModel OrgDetails);
        public int OnBoardCloudProvider(AddCloudPluginViewModel plugin);
        public List<UserClouds> GetUserClouds();
        public List<ClientCloudDetails> GetClientCloudDetails(string clientName);
        public string SyncCloudData(RequestInfoViewModel requestInfoViewModel);
        public List<ResourceLocations> GetResourceLocations();
        public List<CeraResourceTypeUsage> ResourceUsage(string location);
        public List<ResourceTypeCount> GetResourceTypeCount(string location);
        public List<ResourceTagsCount> GetResourceTagsCount(string location);
        public List<ResourceLocations> GetResourceLocations(string location);
    }
}
