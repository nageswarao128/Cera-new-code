using CERA.AuthenticationService;
using CERA.Entities;
using CERA.Entities.ViewModel;
using CERA.Logging;
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

        public ICeraLogger Logger { get; set; }
        public List<CeraPlatformConfig> _platformConfigs { get; set; }

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

        public List<CeraSubscription> GetCloudSubscriptionList(RequestInfoViewModel requestInfo)
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
    }
}
