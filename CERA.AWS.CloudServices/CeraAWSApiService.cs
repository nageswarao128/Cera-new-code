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
            throw new NotImplementedException();
        }

        public object GetCloudSqlDbList()
        {
            throw new NotImplementedException();
        }

        public object GetCloudSqlServerList()
        {
            throw new NotImplementedException();
        }

        public object GetCloudSubscriptionList()
        {
            throw new NotImplementedException();
        }

        public object GetCloudServicePlanList()
        {
            throw new NotImplementedException();
        }

        public object GetCloudTenantList()
        {
            throw new NotImplementedException();
        }

        public List<CeraVM> GetCloudVMList()
        {
            return new List<CeraVM>();
        }

        public object GetCloudWebAppList()
        {
            throw new NotImplementedException();
        }
    }
}
