using CERA.AuthenticationService;
using CERA.CloudService;
using CERA.CloudService.CERAEntities;
using CERA.Entities;
using CERA.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CERA.Azure.CloudService
{
    public class CeraAzureApiService : ICeraAzureApiService
    {
        public CeraAzureApiService()
        {

        }
        ICeraAuthenticator authenticator;
        private ICeraLogger _logger;

        public CeraAzureApiService(ICeraLogger logger)
        {
            _logger = logger;
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
            authenticator = new CeraAzureAuthenticator(_logger);
            authenticator.Initialize(tenantId, clientID, clientSecret);
        }

        public List<CeraSubscription> GetCloudSubscriptionList()
        {
            try
            {
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                _logger.LogInfo("Auth Client Initialized");
                var azureSubscriptions = authClient.Subscriptions.ListAsync().Result;
                _logger.LogInfo("Got Subscription List from Azure Cloud Provider");
                if (azureSubscriptions != null)
                {
                    _logger.LogInfo("Parsing Subscription List To CERA Subscription");
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
                    _logger.LogInfo("Parsing Completed Subscription List To CERA Subscription");
                    return subscriptions;
                }
                _logger.LogInfo("No Subscription List found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
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
