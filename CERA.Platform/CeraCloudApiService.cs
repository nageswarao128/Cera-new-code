using CERA.Converter;
using CERA.DataOperation;
using CERA.Entities;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using CERA.Platform;
using System;
using System.Collections.Generic;

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

        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo)
        {
            
            Logger.LogInfo("Get Cloud Resources List Called");
            List<CeraResources> resources = new List<CeraResources>();
            GetPlatforms();
            foreach (var platformConfig in PlatformConfigs)
            {
                _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                _cloudApiServices.Logger = Logger;
                resources = _cloudApiServices.GetCloudResourceList(requestInfo);
                Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Resources");
                _dataOps.AddResourcesData(resources);
                resources.Clear();
                Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Resources to DB");
            }
            return resources;
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

        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo)
        {
            Logger.LogInfo("Get Cloud Virtual Machines List Called");
            List<CeraVM> vM = new List<CeraVM>();
            GetPlatforms();
            foreach (var platformConfig in PlatformConfigs)
            {
                _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                _cloudApiServices.Logger = Logger;
                vM = _cloudApiServices.GetCloudVMList(requestInfo);
                Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud virtual Machines");
               _dataOps.AddVMData(vM);
                vM.Clear();
                Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Virtual Machines to DB");
            }
            return vM;
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
        public List<CeraResources> GetResourcesList()
        {

            Logger.LogInfo("Requested data for Resources List from Database called");
            return _dataOps.GetResources();
        }
        public List<CeraVM> GetVMList()
        {
            Logger.LogInfo("Requested data for Virtual Machines List from Database called");
            return _dataOps.GetVM();
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
    }
}
