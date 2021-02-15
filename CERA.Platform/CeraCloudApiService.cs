
using CERA.AWS.CloudService;
using CERA.Azure.CloudService;
using CERA.Entities;
using System.Collections.Generic;

namespace CERA.CloudService
{
    public sealed class CeraCloudApiService : ICeraCloudApiService
    {
        ICeraAzureApiService azureServices;
        ICeraAwsApiService awsServices;

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

        public object GetCloudTenantList()
        {
            return new object();
        }

        public List<CeraVM> GetCloudVMList()
        {
            var azvms = azureServices.GetCloudVMList();
            var awsvms = awsServices.GetCloudVMList();
            var allvms = new List<CeraVM>();
            allvms.AddRange(azvms);
            allvms.AddRange(awsvms);
            return allvms;
        }

        public object GetWebAppsList()
        {
            return new object();
        }

        public void GetAllResources()
        {
            GetSurvicePlansList();
            GetCloudTenantList();
            GetCloudVMList();
            GetWebAppsList();
        }


    }
}
