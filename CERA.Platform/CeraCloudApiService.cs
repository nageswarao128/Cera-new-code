﻿using CERA.Converter;
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
            if (ClientName != null)
            {
                Logger.LogInfo("Get Platforms is called");
                PlatformConfigs = _dataOps.GetClientOnboardedPlatforms(ClientName);
                if (PlatformConfigs.Count > 0)
                {
                    Logger.LogInfo("Platforms retrived successfully from DB");
                }
                else
                {
                    Logger.LogError("Platforms data is null");
                }
            }
            else
            {
                Logger.LogError("Client Name is Null");
            } 
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the Resources data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of Resources data</returns>

        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Cloud Resources List Called");
                List<CeraResources> resources = new List<CeraResources>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
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
            catch (Exception ex)
            {

                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the ResourceGroups data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of ResourceGroups data</returns>
        public async Task<List<CeraResourceGroups>> GetCloudResourceGroups(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Cloud ResourceGroups List Called");
                List<CeraResourceGroups> resources = new List<CeraResourceGroups>();

                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    resources = await _cloudApiServices.GetCloudResourceGroups(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Resources");
                    _dataOps.AddResourceGroupData(resources);
                    resources.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud ResourceGroups to DB");
                }
                return resources;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
           
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the StorageAccount data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of StorageAccount data</returns>
        public async Task<List<CeraStorageAccount>> GetCloudStorageAccountList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Cloud StorageAccount List Called");
                List<CeraStorageAccount> storageAccount = new List<CeraStorageAccount>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                List<StorageSize> storageSizes = new List<StorageSize>();
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    storageAccount = await _cloudApiServices.GetCloudStorageAccountList(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Resources");
                    _dataOps.AddStorageAccountData(storageAccount);
                    if(platformConfig.PlatformName== "Azure")
                    {
                      await GetCloudStorageSize(requestInfo,storageAccount);
                        
                    }
                    storageAccount.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud StorageGroups to DB");
                }
                return storageAccount;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public async Task<List<StorageSize>> GetCloudStorageSize(RequestInfoViewModel requestInfo ,List<CeraStorageAccount> storageAccounts)
        {
            
            List<StorageSize> storageSizes = new List<StorageSize>();
            GetPlatforms();
            foreach(var platforms in PlatformConfigs)
            {
                if (platforms.PlatformName == "Azure")
                {
                    requestInfo.tenantId = platforms.TenantId;
                    requestInfo.clientId = platforms.ClientId;
                    requestInfo.clientSecret = platforms.ClientSecret;
                    
                    _cloudApiServices = _converter.CreateInstance(platforms.DllPath, platforms.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    storageSizes = await _cloudApiServices.GetCloudStorageSize(requestInfo, storageAccounts);
                    _dataOps.AddStorageSize(storageSizes);
                    storageSizes.Clear();
                }
                
            }
            return storageSizes;
        }
        public object GetCloudSqlDbList()
        {
            return new object();
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the Subscription data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of Subscription data</returns>
        public List<CeraSubscription> GetCloudSubscriptionList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Cloud Subcription List Called");
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                GetPlatforms();
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
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
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        
        public object GetCloudServicePlanList()
        {
            return new object();
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the Tenants data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of Tenant data</returns>
        public List<CeraTenants> GetCloudTenantList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Cloud Tenants List Called");
                List<CeraTenants> Tenants = new List<CeraTenants>();
                GetPlatforms();
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    Tenants = _cloudApiServices.GetCloudTenantList(requestInfo);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Tenants");
                    _dataOps.AddTenantData(Tenants);
                    Tenants.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Tenants to DB");
                }
                return Tenants;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the VirtualMachines data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of Virtual Machines data</returns>
        public async Task<List<CeraVM>> GetCloudVMList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Cloud Virtual Machines List Called");
                List<CeraVM> vM = new List<CeraVM>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    vM = await _cloudApiServices.GetCloudVMList(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud virtual Machines");
                    _dataOps.AddVMData(vM);
                    vM.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Virtual Machines to DB");
                }
                return vM;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public List<CeraResourceHealth> GetCloudResourceHealth(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Cloud Resource Health List Called");
                List<CeraResourceHealth> resourceHealth = new List<CeraResourceHealth>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    resourceHealth = _cloudApiServices.GetCloudResourceHealth(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Resource Health");
                    _dataOps.AddResourceHealth(resourceHealth);
                    resourceHealth.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Resource Health to DB");
                }
                return resourceHealth;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the SqlServer data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of SqlServer data</returns>
        public List<CeraSqlServer> GetCloudSqlServersList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get SqlServer List Called");
                List<CeraSqlServer> sqlServers = new List<CeraSqlServer>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    sqlServers = _cloudApiServices.GetCloudSqlServersList(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud sql servers");
                    _dataOps.AddSqlServerData(sqlServers);
                    sqlServers.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Sqlserver to DB");
                }
                return sqlServers;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the WebApps data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of WebApps data</returns>
        public async Task<List<CeraWebApps>> GetCloudWebAppList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get AppServicePlan List Called");
                List<CeraWebApps> webApps = new List<CeraWebApps>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    webApps = await _cloudApiServices.GetCloudWebAppList(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud sql servers");
                    _dataOps.AddWebAppData(webApps);
                    webApps.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Virtual Machines to DB");
                }
                return webApps;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the AppServicePlan data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of AppServicePlan data</returns>
        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get AppServicePlans List Called");
                List<CeraAppServicePlans> appServicePlans = new List<CeraAppServicePlans>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    appServicePlans = _cloudApiServices.GetCloudAppServicePlansList(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud AppServicePlans");
                    _dataOps.AddAppServicePlans(appServicePlans);
                    appServicePlans.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud AppServicePlans to DB");
                }
                return appServicePlans;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// Based on the obtained cloud platforms this method will call the class with cloud service logic
        /// and retrives the Disks data and inserts the obatained data into database
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>This method returns the list of Disks data</returns>
        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Disks List Called");
                List<CeraDisks> Disks = new List<CeraDisks>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                subscriptions = GetSubscriptionList();
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    Disks = _cloudApiServices.GetCloudDisksList(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Disks");
                    _dataOps.AddDisksData(Disks);
                    Disks.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Disks to DB");
                }
                return Disks;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public List<CeraPolicy> GetCloudPolicies(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Policies List Called");
                List<CeraPolicy> policies = new List<CeraPolicy>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    policies = _cloudApiServices.GetCloudPolicies(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Policies");
                    _dataOps.AddPolicyData(policies);
                    policies.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Policies to DB");
                }
                return policies;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public int AddPolicyRules(List<PolicyRules> data)
        {
            Logger.LogInfo("Add Policy Rules Called");
            return _dataOps.AddPolicyRules(data);
        }
        public List<CeraCompliances> GetCloudCompliances(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Compliances List Called");
                List<CeraCompliances> compliances = new List<CeraCompliances>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    compliances = _cloudApiServices.GetCloudCompliances(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Compliances");
                    _dataOps.AddCompliances(compliances);
                    compliances.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Compliances to DB");
                }
                return compliances;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public List<CeraRateCard> GetCloudRateCardList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get RateCard List Called");
                List<CeraRateCard> rateCard = new List<CeraRateCard>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    rateCard = _cloudApiServices.GetCloudRateCardList(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Ratecard");
                    _dataOps.AddRateCard(rateCard);
                    rateCard.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud RateCard to DB");
                }
                return rateCard;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public async Task<List<CeraUsage>> GetCloudUsageDetails(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Usage Details List Called");
                List<CeraUsage> usage = new List<CeraUsage>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    usage = await _cloudApiServices.GetCloudUsageDetails(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Usage Details");
                    _dataOps.AddUsageDetails(usage);
                    usage.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Usage Details to DB");
                }
                return usage;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public List<UsageByMonth> GetCloudUsageByMonth(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Usage Details By Month List Called");
                List<UsageByMonth> usage = new List<UsageByMonth>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    usage = _cloudApiServices.GetCloudUsageByMonth(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Usage Details By Month");
                    _dataOps.AddUsageByMonth(usage);
                    usage.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Usage Details By Month to DB");
                }
                return usage;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<UsageHistory> GetCloudUsageHistory(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Usage History List Called");
                List<UsageHistory> usage = new List<UsageHistory>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    usage = _cloudApiServices.GetCloudUsageHistory(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Usage History Month");
                    _dataOps.AddUsageHistory(usage);
                    usage.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Usage History to DB");
                }
                return usage;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<AdvisorRecommendations> GetCloudAdvisorRecommendations(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Avisor Recommendations Details List Called");
                List<AdvisorRecommendations> recommendations  = new List<AdvisorRecommendations>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    recommendations = _cloudApiServices.GetCloudAdvisorRecommendations(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Advisor Recommendations");
                    _dataOps.AddAdvisorRecommendations(recommendations);
                    recommendations.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Advisor Recommendations to DB");
                }
                return recommendations;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<AzureLocations> GetCloudLocations(RequestInfoViewModel requestInfo)
        {
            try
            {
                Logger.LogInfo("Get Location Details List Called");
                List<AzureLocations> locations = new List<AzureLocations>();
                GetPlatforms();
                List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                
                foreach (var platformConfig in PlatformConfigs)
                {
                    requestInfo.tenantId = platformConfig.TenantId;
                    requestInfo.clientId = platformConfig.ClientId;
                    requestInfo.clientSecret = platformConfig.ClientSecret;
                    subscriptions = GetTenantSubscriptions(platformConfig.TenantId);
                    _cloudApiServices = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                    _cloudApiServices.Logger = Logger;
                    locations = _cloudApiServices.GetCloudLocations(requestInfo, subscriptions);
                    Logger.LogInfo($"Got data from {platformConfig.PlatformName} Cloud Location");
                    _dataOps.AddLocationsData(locations);
                    locations.Clear();
                    Logger.LogInfo($"Imported data for {platformConfig.PlatformName} Cloud Location to DB");
                }
                return locations;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public string SyncCloudData(RequestInfoViewModel requestInfoViewModel)
        {
            try
            {
                GetCloudResourceList(requestInfoViewModel);
                GetCloudSubscriptionList(requestInfoViewModel);
                return "Data Synced sucessfully";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            
        }
        /// <summary>
        /// This method retrives Subscription data from database
        /// </summary>
        /// <returns>returns Subscription data from database</returns>
        public List<CeraSubscription> GetSubscriptionList()
        {
            try
            {
                Logger.LogInfo("Requested data for Subcription List from Database called");
                return _dataOps.GetSubscriptions();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// This method retrives Resources data from database
        /// </summary>
        /// <returns>returns Resources data from database</returns>
        public List<CeraResources> GetResourcesList()
        {
            try
            {
                Logger.LogInfo("Requested data for Resources List from Database called");
                return _dataOps.GetResources();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// This method retrives Virtual Machines data from database
        /// </summary>
        /// <returns>returns VirtualMAchines data from database</returns>
        public List<ResourcesModel> GetVMList()
        {
            try
            {
                Logger.LogInfo("Requested data for Virtual Machines List from Database called");
                return _dataOps.GetVM();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// This method retrives ResourceGroups data from database
        /// </summary>
        /// <returns>returns ResourceGroups from database</returns>
        public List<ResourceGroupsVM> GetResourceGroupsList()
        {
            try
            {
                Logger.LogInfo("Requested data for ResourceGroups List from Database called");
                return _dataOps.GetResourceGroups();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// This method retrives StorageAccount data from database
        /// </summary>
        /// <returns>returns StorageAccount data from database</returns>
        public List<StorageAccountsVM> GetStorageAccountList()
        {
            try
            {
                Logger.LogInfo("Requested data for StorageAccount List from Database called");
                return _dataOps.GetStorageAccount();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }

        public List<StorageSize> GetStorageSizes()
        {
            try
            {
                Logger.LogInfo("Requested data for StorageAccount Sizes from Database called");
                return _dataOps.GetStorageSizes();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        /// <summary>
        /// This method retrives SqlServer data from database
        /// </summary>
        /// <returns>returns SqlServer data from database</returns>
        public List<CeraSqlServer> GetSqlServersList()
        {
            try
            {
                Logger.LogInfo("Requested data for SqlServer List from Database called");
                return _dataOps.GetSqlServers();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// This method retrives Tenants data from database
        /// </summary>
        /// <returns>returns Tenants data from database</returns>
        public List<CeraTenants> GetTenantsList()
        {
            try
            {
                Logger.LogInfo("Requested data for Tenants List from Database called");
                return _dataOps.GetTenants();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// This method retrives WebApp data from database
        /// </summary>
        /// <returns>returns WebApp data from database</returns>
        public List<ResourcesModel> GetWebAppsList()
        {
            try
            {
                Logger.LogInfo("Requested data for WebApps List from Database called");
                return _dataOps.GetWebApps();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// This method retrives AppServicePlan data from database
        /// </summary>
        /// <returns>returns AppServicePlan data from database</returns>
        public List<CeraAppServicePlans> GetAppServicePlansList()
        {
            try
            {
                Logger.LogInfo("Requested data for AppServicePlans List from Database called");
                return _dataOps.GetAppServicePlans();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        /// <summary>
        /// This method retrives Disks data from database
        /// </summary>
        /// <returns>returns Disks data from database</returns>
        public List<CeraDisks> GetDisksList()
        {
            try
            {
                Logger.LogInfo("Requested data for Disks List from Database called");
                return _dataOps.GetDisks();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }

        public List<ResourcesModel> GetResourceGroupResources(string name)
        {
            try
            {
                Logger.LogInfo($"Requested resources data for Resource Group: {name} from the Database");
                return _dataOps.GetResourceGroupResources(name);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        public List<ResourceHealthViewDTO> GetCeraResourceHealthList()
        {
            try
            {
                Logger.LogInfo("Requested data for Resource Health List from Database called");
                return _dataOps.ResourceHealth();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public List<CeraCompliances> GetCompliancesList()
        {
            try
            {
                Logger.LogInfo("Requested data for Compliances List from Database called");
                return _dataOps.GetCompliances();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public List<CeraRateCard> GetRateCardList()
        {
            try
            {
                Logger.LogInfo("Requested data for RateCard List from Database called");
                return _dataOps.GetRateCard();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public List<CeraUsage> GetUsageDetails()
        {
            try
            {
                Logger.LogInfo("Requested data for Usage Details from Database called");
                return _dataOps.GetUsageDetails();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public List<CeraPolicy> GetPolicies()
        {
            try
            {
                Logger.LogInfo("Requested data for Policies from Database called");
                return _dataOps.GetPolicy();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<CeraResourceTypeUsage> GetResourceTypeUsageDetails()
        {
            try
            {
                Logger.LogInfo("Requested data for ResourceType Usage Details from Database called");
                return _dataOps.ResourceUsage();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public List<ResourceTypeCount> GetResourceTypeCounts()
        {
            try
            {
                Logger.LogInfo("Requested data for ResourceType count Details from Database called");
                return _dataOps.GetResourceTypeCount();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
            
        }
        public List<ResourceTypeCount> GetSubscriptionLocationList(string location, string cloudprovider, string subscriptionid)
        {
            try
            {
                Logger.LogInfo("Requested data for ResourceType count Details from Database called");
                return _dataOps.GetSubscriptionLocationList(location,cloudprovider,subscriptionid);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
          public List<ResourceTypeCount> GetSubscriptionTypeList(string subscriptionId, string cloudprovider)
           {
            try
            {
                Logger.LogInfo("Requested data for ResourceType count Details from Database called");
                return _dataOps.GetSubscriptionTypeList(subscriptionId,cloudprovider);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<ResourceTagsCount> GetResourceTagsCount()
        {
            try
            {
                return _dataOps.GetResourceTagsCount();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<ResourceTagsCount> GetResourceTagsCloudCount(string cloudprovider)
        {
            try
            {
                return _dataOps.GetResourceTagsCloudCount(cloudprovider);
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<AdvisorRecommendations> GetAdvisorRecommendations()
        {
            try
            {
                return _dataOps.GetAdvisorRecommendations();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<AzureLocations> GetLocations()
        {
            try
            {
                return _dataOps.GetLocations();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<ResourceLocations> GetResourceLocations()
        {
            try
            {
                return _dataOps.GetResourceLocations();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<CostUsage> UsageByMonth()
        {
            try
            {
                return _dataOps.UsageByMonth();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }

        public List<CostUsage> UsageCloudByMonth(string cloudprovider)
        {
            try
            {
                return _dataOps.UsageCloudByMonth(cloudprovider);
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }

        public List<CostUsage> UsageHistory()
        {
            try
            {
                return _dataOps.UsageHistory();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }

        public List<UsageHistoryByMonth> UsageHistoryByMonth()
        {
            try
            {
                return _dataOps.UsageHistoryByMonth();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }

        public List<UsageByLocation> GetUsageByLocation()
        {
            try
            {
                return _dataOps.GetUsageByLocation();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }

        public List<UsageByResourceGroup> GetUsageByResourceGroup()
        {
            try
            {
                return _dataOps.GetUsageByResourceGroup();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCount(string cloudprovider, string subscriptionid)
        {
            try
            {
                return _dataOps.GetResourceSubscriptionCloudTagsCount(cloudprovider,subscriptionid);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
  public List<CostUsage> UsageSubscriptionByMonth(string cloudprovider, string subscriptionid)
        {
            try
            {
                return _dataOps.UsageSubscriptionByMonth(cloudprovider,subscriptionid);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        public List<BarChartModel> GetBarChartCloudData(string cloud)
        {
            try
            {
                return _dataOps.GetBarChartCloudData(cloud);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        public List<BarChartModel> GetBarChartSubscriptionData(string cloud, string subscriptionId)
        {
            try
            {
                return _dataOps.GetBarChartSubscriptionData(cloud, subscriptionId);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        public List<PolicyRules> GetPolicyRules()
        {
            try
            {
                return _dataOps.GetPolicyRules();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<UsageByMonth> GetUsageByMonth()
        {
            try
            {
                return _dataOps.GetUsageByMonth();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            };
        }

        public List<UsageHistory> GetUsageHistory()
        {
            try
            {
                return _dataOps.GetUsageHistory();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }

        public List<DashboardCountModel> GetDashboardCount()
        {
            try
            {
                return _dataOps.GetDashboardCount();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }

        public List<DashboardCountModel> GetDashboardSubscriptionCountFilters(string cloudprovider, string subscriptionid)
        {
            try
            {
                return _dataOps.GetDashboardSubscriptionCountFilters(cloudprovider,subscriptionid);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
  public List<DashboardCountModel> GetDashboardSubscriptionLocationFilters(string location, string cloudprovider, string subscriptionid)
        {
            try
            {
                return _dataOps.GetDashboardSubscriptionLocationFilters(location,cloudprovider,subscriptionid);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<ManageOrg> ManageOrganization()
        {
            try
            {
                return _dataOps.ManageOrganization();
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<DashboardCountModel> GetDashboardCountFilters(string location, string cloudprovider)
        {
            try
            {
                return _dataOps.GetDashboardCountFilters(location, cloudprovider);
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudspentUsage(string cloudprovider, string subscriptionid)
        {
            try
            {
                return _dataOps.ResourceSubscriptionCloudspentUsage(cloudprovider, subscriptionid);
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
          public List<DashboardCountModel> GetDashboardCountLocation(string location)
          {
            try
            {
                return _dataOps.GetDashboardCountLocation(location);
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }
        }
  public List<DashboardCountModel> GetDashboardCountCloud(string cloudprovider)
        {
            try
            {
                return _dataOps.GetDashboardCountCloud(cloudprovider);
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
                throw ex;
            }

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
        public List<ClientCloudDetails> GetClientCloudDetails(string clientName)
        {
            return _dataOps.GetClientCloudDetails(clientName);
        }
        public List<CeraResourceTypeUsage> ResourceUsage(string location)
        {
            return _dataOps.ResourceUsage(location);
        }
        public List<CeraResourceTypeUsage> ResourceCloudUsage(string location, string cloudprovider)
        {
            return _dataOps.ResourceCloudUsage(location, cloudprovider);
        }

        public List<CeraResourceTypeUsage> ResourceSubscriptionCloudUsage(string location, string cloudprovider, string subscriptionid)
        {
            return _dataOps.ResourceSubscriptionCloudUsage(location, cloudprovider, subscriptionid);
        }
  public List<ResourceTagsCount> GetResourceSubscriptionCloudTagsCount(string location, string cloudprovider, string subscriptionid)
        {
            return _dataOps.GetResourceSubscriptionCloudTagsCount(location, cloudprovider, subscriptionid);
        }
        public List<ResourceTypeCount> GetResourceTypeCount(string location)
        {
            return _dataOps.GetResourceTypeCount(location);
        }
        public List<ResourceTypeCount> GetResourceCloudCount(string cloudprovider)
        {
            return _dataOps.GetResourceCloudCount(cloudprovider);
        }
        public List<ResourceTagsCount> GetResourceTagsCount(string location)
        {
            return _dataOps.GetResourceTagsCount(location);
        }
        public List<ResourceTagsCount> GetResourceCloudTagsCount(string location, string cloudprovider)
        {
            return _dataOps.GetResourceCloudTagsCount(location, cloudprovider);
        }

        public List<ResourceLocations> GetResourceLocations(string location)
        {
            return _dataOps.GetResourceLocations(location);
        }
        public List<locationFilter> GetMapLocationsFilter()
        {
            return _dataOps.GetMapLocationsFilter();
        }
        public List<ResourceTypeCount> GetResourceTypecloudCount(string location, string cloudprovider)
        {
            return _dataOps.GetResourceTypecloudCount(location, cloudprovider);
        }

        public List<Dashboardresources> GetComputeResources()
        {
            return _dataOps.GetComputeResources();
        }

        public List<Dashboardresources> GetNetworkResources()
        {
            return _dataOps.GetNetworkResources();
        }

        public List<Dashboardresources> GetStorageResources()
        {
            return _dataOps.GetStorageResources();
        }

        public List<Dashboardresources> GetOtherResources()
        {
            return _dataOps.GetOtherResources();
        }
        public List<CeraSubscription> GetTenantSubscriptions(string tenantId)
        {
            return _dataOps.TenantSubscriptions(tenantId);
        }
        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<CeraVM>> GetCloudVMList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<CeraResourceGroups>> GetCloudResourceGroups(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<CeraStorageAccount>> GetCloudStorageAccountList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraSqlServer> GetCloudSqlServersList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<CeraWebApps>> GetCloudWebAppList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraResourceHealth> GetCloudResourceHealth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraCompliances> GetCloudCompliances(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<CeraRateCard> GetCloudRateCardList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<CeraUsage>> GetCloudUsageDetails(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<UserClouds> GetUserClouds()
        {
            return _dataOps.GetUserClouds();
        }


        public List<CeraPolicy> GetCloudPolicies(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<AzureLocations> GetCloudLocations(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }


        public List<AdvisorRecommendations> GetCloudAdvisorRecommendations(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<UsageByMonth> GetCloudUsageByMonth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<UsageHistory> GetCloudUsageHistory(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

    }
}
