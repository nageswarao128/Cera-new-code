using CERA.AuthenticationService;
using CERA.Entities;
using System.Collections.Generic;

namespace CERA.Azure.CloudService
{
    public class CeraAzureApiService : ICeraAzureApiService
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
    }
}
