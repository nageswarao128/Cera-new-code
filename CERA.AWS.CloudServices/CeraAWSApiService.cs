using CERA.AuthenticationService;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
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
        public List<CeraPlatformConfigViewModel> _platformConfigs { get; set; }

        public object GetCloudMonthlyBillingList()
        {
            return new object();
        }

        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo)
        {
            return new List<CeraResources>();
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

        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo)
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
        public List<CeraResources> GetResourcesList()
        {
            return new List<CeraResources>();
        }
        public List<CeraVM> GetVMList()
        {
            return new List<CeraVM>();
        }

        public void Initialize(string tenantId, string clientID, string clientSecret,string authority)
        {
            throw new NotImplementedException();
        }
    }
}
