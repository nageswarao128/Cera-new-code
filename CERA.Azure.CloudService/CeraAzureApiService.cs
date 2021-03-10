
using CERA.CloudService;
using CERA.Entities;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace CERA.Azure.CloudService
{
    public class CeraAzureApiService : ICeraAzureApiService
    {
        public CeraAzureApiService()
        {

        }
        public List<CeraPlatformConfigViewModel> _platformConfigs { get; set; }
        ICeraAuthenticator authenticator;
        public ICeraLogger Logger { get; set; }
        public CeraAzureApiService(ICeraLogger logger)
        {
            Logger = logger;
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
        /// <summary>
        /// This method will initialises the authenticator class and sends the required client cloud 
        /// details to the class for authentication
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="authority"></param>
        public void Initialize(string tenantId, string clientID, string clientSecret,string authority)
        {
            authenticator = new CeraAzureAuthenticator(Logger);
            authenticator.Initialize(tenantId, clientID, clientSecret,authority);
        }
        /// <summary>
        /// This method will initialises the authenticator class and sends the required client cloud 
        /// details to the class for authentication
        /// </summary>
        public void Initialize()
        {
            string clientID = AzureAuth.Default.clientId;
            string tenantId = AzureAuth.Default.tenantId;
            string clientSecret = AzureAuth.Default.clientSecret;
            string authority = AzureAuth.Default.authority;

            authenticator = new CeraAzureAuthenticator(tenantId, clientID, clientSecret,authority, Logger);
            authenticator.Initialize(tenantId, clientID, clientSecret,authority);
        }
        /// <summary>
        /// This method will calls the required authenticationa and after the authenticating it will 
        /// retrives available subscrption from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        public List<CeraSubscription> GetCloudSubscriptionList(RequestInfoViewModel requestInfo)
        {
            
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                var azureSubscriptions = authClient.Subscriptions.ListAsync().Result;
                Logger.LogInfo("Got Subscription List from Azure Cloud Provider");
                if (azureSubscriptions != null)
                {
                    Logger.LogInfo("Parsing Subscription List To CERA Subscription");
                    List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                    foreach (var sub in azureSubscriptions)
                    {
                        subscriptions.Add(new CeraSubscription
                        {
                            SubscriptionId = sub.SubscriptionId,
                            DisplayName = sub.DisplayName,
                            TenantID = sub.Inner.TenantId,
                        });
                    }
                    Logger.LogInfo("Parsing Completed Subscription List To CERA Subscription");
                    
                    return subscriptions;
                }
                Logger.LogInfo("No Subscription List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
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
            return new List<CeraVM>();
        }

        public object GetCloudWebAppList()
        {
            return new object();
        }
        public List<CeraSubscription> GetSubscriptionList()
        {
            return new List<CeraSubscription>();
        }
    }
}
