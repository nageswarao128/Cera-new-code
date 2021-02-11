using CERA.AuthenticationService;
using CERA.CloudService.CERAEntities;
using System;
using System.Collections.Generic;

namespace CERA.CloudService
{
    public class CeraAWSApiService : ICeraAWSApiService
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

        public object GetTenantsList()
        {
            throw new NotImplementedException();
        }

        public List<CeraVM> GetVMsList()
        {
            return new List<CeraVM>();
        }

        public object GetWebAppsList()
        {
            throw new NotImplementedException();
        }
    }
}
