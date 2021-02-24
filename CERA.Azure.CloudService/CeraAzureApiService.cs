using CERA.AuthenticationService;
using CERA.Entities;
using CERA.Logging;
using System;
using System.Collections.Generic;

namespace CERA.Azure.CloudService
{
    public class CeraAzureApiService : ICeraAzureApiService
    {
        public CeraAzureApiService()
        {

        }
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
        public void Initialize(string tenantId, string clientID, string clientSecret)
        {
            authenticator = new CeraAzureAuthenticator(Logger);
            authenticator.Initialize(tenantId, clientID, clientSecret);
        }

        public List<CeraSubscription> GetCloudSubscriptionList()
        {
            try
            {
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
