using CERA.Converter;
using CERA.DataOperation;
using CERA.Entities;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using CERA.Platform;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CERA.CloudService
{
    public sealed class CeraCloudApiService : ICeraPlatform
    {
        public string ClientName { get; set; }
        ICeraCloudApiService _cloudApiServices;
        ICeraDataOperation _dataOps;
        ICeraConverter _converter;
        public ICeraLogger Logger { get; set; }

        List<CeraPlatformConfigViewModel> PlatformConfigs { get; set; }

        public CeraCloudApiService(
            ICeraDataOperation dataOps,
            ICeraLogger logger,
            ICeraConverter converter)
        {
            _dataOps = dataOps;
            Logger = logger;
            _converter = converter;
        }

        public CeraCloudApiService()
        {
        }

        public object GetCloudMonthlyBillingList()
        {
            return new object();
        }
        void GetPlatforms()
        {
            PlatformConfigs = _dataOps.GetClientOnboardedPlatforms(ClientName);
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the Resources data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of Resources data</returns>

        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo)
        {
            
            Logger.LogInfo("Get Cloud Resources List Called");
            List<CeraResources> resources = new List<CeraResources>();
            GetPlatforms();
            List<CeraSubscription> subscriptions = new List<CeraSubscription>();
            subscriptions = GetSubscriptionList();
            foreach (var platformConfig in PlatformConfigs)
            {
                _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                _cloudApiServices.Logger = Logger;              
                resources = _cloudApiServices.GetCloudResourceList(requestInfo, subscriptions);
                Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Resources");
                _dataOps.AddResourcesData(resources);
                resources.Clear();
                Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Resources to DB");
            }
            return resources;
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the ResourceGroups data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of ResourceGroups data</returns>
        public List<CeraResourceGroups> GetCloudResourceGroups(RequestInfoViewModel requestInfo)
        {
            
            Logger.LogInfo("Get Cloud ResourceGroups List Called");
            List<CeraResourceGroups> resources = new List<CeraResourceGroups>();
            
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                subscriptions = GetSubscriptionList();
                foreach (var platformConfig in PlatformConfigs)
                {
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    resources = _cloudApiServices.GetCloudResourceGroups(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Resources");
                    _dataOps.AddResourceGroupData(resources);
                    resources.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud ResourceGroups to DB");
                }
                return resources;
           
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the StorageAccount data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of StorageAccount data</returns>
        public List<CeraStorageAccount> GetCloudStorageAccountList(RequestInfoViewModel requestInfo)
        {
            Logger.LogInfo("Get Cloud StorageAccount List Called");
            List<CeraStorageAccount> storageAccount = new List<CeraStorageAccount>();
            GetPlatforms();
            List<CeraSubscription> subscriptions = new List<CeraSubscription>();
            subscriptions = GetSubscriptionList();
            foreach (var platformConfig in PlatformConfigs)
            {
                _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                _cloudApiServices.Logger = Logger;
                storageAccount = _cloudApiServices.GetCloudStorageAccountList(requestInfo, subscriptions);
                Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Resources");
                _dataOps.AddStorageAccountData(storageAccount);
                storageAccount.Clear();
                Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud StorageGroups to DB");
            }
            return storageAccount;
        }

        public object GetCloudSqlDbList()
        {
            return new object();
        }

        public object GetCloudSqlServerList()
        {
            return new object();
        }

        public List<CeraSubscription> GetCloudSubscriptionList(RequestInfoViewModel requestInfo)
        {
            Logger.LogInfo("Get Cloud Subcription List Called");
            List<CeraSubscription> subscriptions = new List<CeraSubscription>();
            GetPlatforms();
            foreach (var platformConfig in PlatformConfigs)
            {
                _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                _cloudApiServices.Logger = Logger;
                subscriptions = _cloudApiServices.GetCloudSubscriptionList(requestInfo);
                Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Subcription");
                _dataOps.AddSubscriptionData(subscriptions);
                subscriptions.Clear();
                Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Subcription to DB");
            }
            return subscriptions;
        }
        
        public object GetCloudServicePlanList()
        {
            return new object();
        }

        public object GetCloudTenantList()
        {
            return new object();
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the VirtualMachines data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of Virtual Machines data</returns>
        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo)
        {
            Logger.LogInfo("Get Cloud Virtual Machines List Called");
            List<CeraVM> vM = new List<CeraVM>();
            GetPlatforms();
            List<CeraSubscription> subscriptions = new List<CeraSubscription>();
            subscriptions = GetSubscriptionList();
            foreach (var platformConfig in PlatformConfigs)
            {
                _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                _cloudApiServices.Logger = Logger;
                vM = _cloudApiServices.GetCloudVMList(requestInfo,subscriptions);
                Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud virtual Machines");
               _dataOps.AddVMData(vM);
                vM.Clear();
                Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Virtual Machines to DB");
            }
            return vM;
        }
        public async Task<List<CeraResourceHealth>> GetCloudResourceHealth(RequestInfoViewModel requestInfo)
        {
            Logger.LogInfo("Get Cloud Resource Health List Called");
            List<CeraResourceHealth> resourceHealth = new List<CeraResourceHealth>();
            GetPlatforms();
            List<CeraSubscription> subscriptions = new List<CeraSubscription>();
            subscriptions = GetSubscriptionList();
            foreach (var platformConfig in PlatformConfigs)
            {
                _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                _cloudApiServices.Logger = Logger;
                resourceHealth = await _cloudApiServices.GetCloudResourceHealth(requestInfo,subscriptions);
                Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Resource Health");
                _dataOps.AddResourceHealth(resourceHealth);
                resourceHealth.Clear();
                Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Resource Health to DB");
            }
            return resourceHealth;
        }
        public object GetCloudWebAppList()
        {
            return new object();
        }

        public void GetAllResources()
        {
            GetCloudServicePlanList();
            GetCloudTenantList();
           // GetCloudVMList();
            GetCloudWebAppList();
        }

        public List<CeraSubscription> GetSubscriptionList()
        {
            Logger.LogInfo("Requested data for Subcription List from Database called");
            return _dataOps.GetSubscriptions();
        }
        /// <summary>
        /// This method retrives Resources data from database
        /// </summary>
        /// <returns>returns Resources data from database</returns>
        public List<CeraResources> GetResourcesList()
        {

            Logger.LogInfo("Requested data for Resources List from Database called");
            return _dataOps.GetResources();
        }
        /// <summary>
        /// This method retrives Virtual Machines data from database
        /// </summary>
        /// <returns>returns VirtualMAchines data from database</returns>
        public List<CeraVM> GetVMList()
        {
            Logger.LogInfo("Requested data for Virtual Machines List from Database called");
            return _dataOps.GetVM();
        }
        /// <summary>
        /// This method retrives ResourceGroups data from database
        /// </summary>
        /// <returns>returns ResourceGroups from database</returns>
        public List<CeraResourceGroups> GetResourceGroupsList()
        {
            Logger.LogInfo("Requested data for ResourceGroups List from Database called");
            return _dataOps.GetResourceGroups();
        }
        /// <summary>
        /// This method retrives StorageAccount data from database
        /// </summary>
        /// <returns>returns StorageAccount data from database</returns>
        public List<CeraStorageAccount> GetStorageAccountList()
        {
            Logger.LogInfo("Requested data for StorageAccount List from Database called");
            return _dataOps.GetStorageAccount();
        }
        public List<CeraResourceHealth> GetCeraResourceHealthList()
        {
            Logger.LogInfo("Requested data for Resource Health List from Database called");
            return _dataOps.GetResourceHealth();
        }
        public void Initialize(string tenantId, string clientID, string clientSecret,string authority)
        {
            _cloudApiServices.Initialize(tenantId, clientID, clientSecret,authority);
        }

        public int OnBoardClientPlatform(AddClientPlatformViewModel platform)
        {
            return _dataOps.OnBoardClientPlatform(platform);
        }

        public int OnBoardOrganization(AddOrganizationViewModel OrgDetails)
        {
            return _dataOps.OnBoardOrganization(OrgDetails);
        }

        public int OnBoardCloudProvider(AddCloudPluginViewModel plugin)
        {
            return _dataOps.OnBoardCloudProvider(plugin);
        }

        public List<CeraPlatformConfigViewModel> GetClientOnboardedPlatforms(string ClientName)
        {
            return _dataOps.GetClientOnboardedPlatforms(ClientName);
        }

        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraResourceGroups> GetCloudResourceGroups(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraStorageAccount> GetCloudStorageAccountList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<CeraResourceHealth>> GetCloudResourceHealth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

       
    }
}
