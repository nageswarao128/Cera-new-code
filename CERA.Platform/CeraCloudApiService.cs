using CERA.Converter;
using CERA.DataOperation;
using CERA.Entities;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using CERA.Platform;
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
        /// <summary>
        /// This method retrives the available cloud platforms data from the database based on 
        /// the client name
        /// </summary>
        void GetPlatforms()
        {
            PlatformConfigs = _dataOps.GetClientOnboardedPlatforms(ClientName);
        }

        public object GetCloudResourceList()
        {
            return new object();
        }

        public object GetCloudSqlDbList()
        {
            return new object();
        }

        public object GetCloudSqlServerList()
        {
            return new object();
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the subscription data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of subscription data</returns>
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

        public List<CeraVM> GetCloudVMList()
        {
            var allvms = new List<CeraVM>();
            return allvms;
        }

        public object GetCloudWebAppList()
        {
            return new object();
        }

        public void GetAllResources()
        {
            GetCloudServicePlanList();
            GetCloudTenantList();
            GetCloudVMList();
            GetCloudWebAppList();
        }
        /// <summary>
        /// This method retrives subscription data from database
        /// </summary>
        /// <returns></returns>
        public List<CeraSubscription> GetSubscriptionList()
        {
            Logger.LogInfo("Requested data for Subcription List from Database called");
            return _dataOps.GetSubscriptions();
        }

        public void Initialize(string tenantId, string clientID, string clientSecret,string authority)
        {
            _cloudApiServices.Initialize(tenantId, clientID, clientSecret,authority);
        }
        /// <summary>
        /// This method will send the cloud platform details to the database layer to insert the data
        /// into database
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public int OnBoardClientPlatform(AddClientPlatformViewModel platform)
        {
            return _dataOps.OnBoardClientPlatform(platform);
        }
        /// <summary>
        /// This method will send the organisation details to the database layer to insert the data
        /// into database
        /// </summary>
        /// <param name="OrgDetails"></param>
        /// <returns></returns>
        public int OnBoardOrganization(AddOrganizationViewModel OrgDetails)
        {
            return _dataOps.OnBoardOrganization(OrgDetails);
        }
        /// <summary>
        /// This method will send the cloud platform details to the database layer to insert the data
        /// into database
        /// </summary>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public int OnBoardCloudProvider(AddCloudPluginViewModel plugin)
        {
            return _dataOps.OnBoardCloudProvider(plugin);
        }
        /// <summary>
        /// This method retrives the available cloud platforms data from the database based on 
        /// the client name
        /// </summary>
        /// <param name="ClientName"></param>
        /// <returns></returns>
        public List<CeraPlatformConfigViewModel> GetClientOnboardedPlatforms(string ClientName)
        {
            return _dataOps.GetClientOnboardedPlatforms(ClientName);
        }
    }
}
