using CERA.AuthenticationService;
using CERA.CloudService;
using CERA.CloudService.CERAEntities;
using CERA.Entities;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            try
            {
                if (authority != null && clientId != null && clientSecret != null && redirectUri != null && tenantId != null)
                {
                    CeraAzureAuthenticator authenticator = new CeraAzureAuthenticator(authority, clientId, clientSecret, redirectUri);
                    var azure = authenticator.CreateRestClient();
                    var sample = azure.Subscriptions.ListAsync().Result;
                    List<CeraSubscriptionList> subscriptions = new List<CeraSubscriptionList>();

                    foreach (var sub in sample)
                    {
                        sub.Inner.AuthorizationSource = "RoleBased";

                        CeraSubscriptionList list = new CeraSubscriptionList { SubscriptionId = sub.SubscriptionId, DisplayName = sub.DisplayName, TenantID = sub.Inner.TenantId, AuthorizationSource = sub.Inner.AuthorizationSource };
                        subscriptions.Add(list);
                    }

                    EventLog.WriteEntry("CERA", "Obtained Subscription List", EventLogEntryType.Information);


                    return subscriptions;
                }
                else
                {
                    EventLog.WriteEntry("CERA", "authority or ClientID or ClientSecret or TenantId or RedirectUri  to obtain Subscription data should not be null", EventLogEntryType.Error);
                    return null;
                }
            }

            catch (Exception ex)
            {
                EventLog.WriteEntry("CERA", ex.Message, EventLogEntryType.Error);
                return null;
            }

        }




    }
}
