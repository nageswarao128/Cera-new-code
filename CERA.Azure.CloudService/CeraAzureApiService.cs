using CERA.AuthenticationService;
using CERA.Entities;
using System.Collections.Generic;

namespace CERA.Azure.CloudService
{
    public class CeraAzureApiService : ICeraAzureApiService
    {

        ICeraAuthenticator authenticator;
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
            authenticator = new CeraAzureAuthenticator();
            var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
            var azureSubscriptions = authClient.Subscriptions.ListAsync().Result;
            if (azureSubscriptions != null)
            {

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
                return subscriptions;
            }
            return null;
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
