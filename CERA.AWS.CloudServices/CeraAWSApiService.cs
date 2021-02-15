using CERA.AuthenticationService;
using CERA.Entities;
using System;
using System.Collections.Generic;

namespace CERA.AWS.CloudService
{
    public class CeraAWSApiService : ICeraAwsApiService
    {
        ICeraAuthenticator authenticator;
        public object GetMonthlyBillingsList()
        {
            return new object();
        }

        public object GetResourcesList()
        {
            throw new NotImplementedException();
        }

        public object GetSqlDbsList()
        {
            throw new NotImplementedException();
        }

        public object GetSqlServersList()
        {
            throw new NotImplementedException();
        }

        public object GetSubscriptionsList()
        {
            throw new NotImplementedException();
        }

        public object GetSurvicePlansList()
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

        public object GetWebAppsList()
        {
            throw new NotImplementedException();
        }
    }
}
