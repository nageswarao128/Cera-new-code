﻿using CERA.AuthenticationService;

namespace CERA.CloudService
{
    public class CeraAzureApiService : ICeraCloudApiService
    {

        ICeraAuthenticator authenticator;
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

        public object GetTenantsList()
        {
            return new object();
        }

        public CeraVM GetVMsList()
        {
            return new CeraVM();
        }

        public object GetWebAppsList()
        {
            return new object();
        }
    }
}
