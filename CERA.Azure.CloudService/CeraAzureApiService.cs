using CERA.AuthenticationService;
using CERA.CloudService;
using CERA.CloudService.CERAEntities;
using CERA.Entities;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Rest;
using System.Collections.Generic;

namespace CERA.Azure.CloudService
{
    public class CeraAzureApiService : ICeraAzureApiService
    {

       // ICeraAuthenticator authenticator;
        public object GetMonthlyBillingsList()
        {
            return new object();
        }

        public object GetResourcesList()
        {
            return new object();
        }

        public object GetSqlDbsList()
        {
            return new object();
        }

        public object GetSqlServersList()
        {
            return new object();
        }

        public object GetSubscriptionsList()
        {
            return new object();
        }

        public object GetSurvicePlansList()
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

        public object GetWebAppsList()
        {
            return new object();
        }

        public List<CeraSubscriptionList> GetSubscriptionsList(string authority, string clientId, string clientSecret, string redirectUri, string tenantId)
        {
            CeraAzureAuthenticator authenticator = new CeraAzureAuthenticator(authority, clientId, clientSecret, redirectUri);
            var token = authenticator.GetAuthToken();
            TokenCredentials tokenCredentials = new TokenCredentials(token);
            var azureCredentials = new AzureCredentials(tokenCredentials, tokenCredentials, tenantId, AzureEnvironment.AzureGlobalCloud);
            var restClient = RestClient
            .Configure()
            .WithEnvironment(AzureEnvironment.AzureGlobalCloud)
            .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
            .WithCredentials(azureCredentials)
            .Build();
            var azure = Microsoft.Azure.Management.Fluent.Azure.Authenticate(restClient, tenantId);
            var sample = azure.Subscriptions.ListAsync().Result;
            List<CeraSubscriptionList> subscriptions = new List<CeraSubscriptionList>();
            
            foreach (var sub in sample)
            {
                sub.Inner.AuthorizationSource = "RoleBased";
                
                CeraSubscriptionList list = new CeraSubscriptionList { SubscriptionId = sub.SubscriptionId, DisplayName = sub.DisplayName ,TenantID = sub.Inner.TenantId,AuthorizationSource = sub.Inner.AuthorizationSource};
                subscriptions.Add(list);
            }
            return subscriptions;
        }

        //List<SubscriptionList> ICeraCloudApiService.GetSubscriptionsList()
        //{
        //    throw new System.NotImplementedException();
        //}

        
    }
}
