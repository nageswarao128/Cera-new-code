using CERA.AWS.CloudService;
using CERA.Azure.CloudService;
using CERA.Converter;
using CERA.DataOperation;
using CERA.Entities;
using CERA.Logging;
using System.Collections.Generic;
using System.Reflection;

namespace CERA.CloudService
{
    public sealed class CeraCloudApiService : ICeraCloudApiService
    {
        ICeraCloudApiService _cloudApiServices;
        ICeraDataOperation _dataOps;
        ICeraConverter _converter;
        public ICeraLogger Logger { get; set; }

        List<CeraPlatformConfig> _platformConfigs = new List<CeraPlatformConfig>() {
            new CeraPlatformConfig(){PlatformName =   "Azure", APIClassName = "", DllPath = ""},
            new CeraPlatformConfig(){PlatformName =   "Aws", APIClassName = "", DllPath = ""},
            new CeraPlatformConfig(){PlatformName =   "GCP", APIClassName = "", DllPath = ""},
            new CeraPlatformConfig(){PlatformName =   "IBM", APIClassName = "", DllPath = ""},
        };

        public CeraCloudApiService(
            ICeraDataOperation dataOps,
            ICeraLogger logger,
            ICeraConverter converter)
        {
            _dataOps = dataOps;
            Logger = logger;
            _converter = converter;
        }
        public object GetCloudMonthlyBillingList()
        {
            return new object();
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

        public List<CeraSubscription> GetCloudSubscriptionList()
        {
            Logger.LogInfo("Get Cloud Subcription List Called");
            ///Get List of Cloud services for the client
            ///usig Reflection instantiate cloud service for corresponding Cloud Platform using itteration
            ///ICeraCloudApiService i = using Reflection create instance of cloud service
            List<CeraSubscription> subscriptions = new List<CeraSubscription>();
            foreach (var platformConfig in _platformConfigs)
            {
                ICeraCloudApiService _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                _cloudApiServices.Logger = Logger;
                subscriptions = _cloudApiServices.GetCloudSubscriptionList();
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

        public List<CeraSubscription> GetSubscriptionList()
        {
            Logger.LogInfo("Requested data for Subcription List from Database called");
            return _dataOps.GetSubscriptions();
        }

        public void Initialize(string tenantId, string clientID, string clientSecret)
        {
            _cloudApiServices.Initialize(tenantId, clientID, clientSecret);
        }
    }
}
