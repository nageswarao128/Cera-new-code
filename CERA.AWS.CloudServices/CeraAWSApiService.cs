using CERA.AuthenticationService;
using CERA.Entities;
using System;
using System.Collections.Generic;

namespace CERA.AWS.CloudService
{
    public class CeraAWSApiService : ICeraAwsApiService
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

        public object GetCloudSubscriptionList()
        {
            return new object();
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
        public object GetSubscriptionList()
        {
            return new object();
        }
    }
}
