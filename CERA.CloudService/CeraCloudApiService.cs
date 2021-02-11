﻿
using CERA.AuthenticationService;

namespace CERA.CloudService
{
    public sealed class CeraCloudApiService : ICeraCloudApiService
    {
        ICeraAzureApiService azureServices;
        ICeraAWSApiService awsServices;

        public CeraCloudApiService()
        {

        }
        public object GetMonthlyBillingsList()
        {
            azureServices.GetHashCode();
            awsServices.GetHashCode();
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

        public void GetAllResources()
        {
            GetSurvicePlansList();
            GetTenantsList();
            GetVMsList();
            GetWebAppsList();
        }
    }
}
