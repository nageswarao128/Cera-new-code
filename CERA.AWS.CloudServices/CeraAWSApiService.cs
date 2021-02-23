using CERA.AuthenticationService;
using CERA.CloudService;
using CERA.CloudService.CERAEntities;
using CERA.Entities;
using System;
using System.Collections.Generic;

namespace CERA.AWS.CloudService
{
    public class CeraAWSApiService : ICeraAwsApiService
    {
        public CeraAWSApiService()
        {

        }
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
            return new List<CeraSubscription>();
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

        public void Initialize(string tenantId, string clientID, string clientSecret)
        {
            throw new NotImplementedException();
        }

        

        public List<CeraSubscriptionList> GetSubscriptionsList(string authority, string clientId, string clientSecret, string redirectUrl, string tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
