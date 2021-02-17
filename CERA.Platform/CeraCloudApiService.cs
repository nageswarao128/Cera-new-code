using CERA.Azure.CloudService;
using CERA.CloudService.CERAEntities;
using CERA.Entities;
using System.Collections.Generic;
using CERA.DataOperation;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using CERA.DataOperation.CeraContext;
using Microsoft.EntityFrameworkCore;

namespace CERA.CloudService
{
    public sealed class CeraCloudApiService : ICeraCloudApiService
    {
        ICeraAzureApiService azureServices;
        // ICeraAwsApiService awsServices;
        //ICeraDataOperation dataOperation;

       private CeraDbContext _DbContext;
        public CeraCloudApiService()
        {

        }
        public object GetMonthlyBillingsList()
        {
            azureServices.GetHashCode();
           // awsServices.GetHashCode();
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
            //var awsvms = awsServices.GetCloudVMList();
            var allvms = new List<CeraVM>();
            allvms.AddRange(azvms);
           // allvms.AddRange(awsvms);
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

        
        public List<CeraSubscriptionList> GetSubscriptionsList(string authority, string clientId, string clientSecret, string redirectUrl, string tenantId)
        {
            var subscriptions = azureServices.GetSubscriptionsList(authority, clientId, clientSecret, redirectUrl, tenantId);
           
            // CERADataOperation data = new CERADataOperation();
            // var sample = data.AddSubscriptionData(list);
            throw new System.NotImplementedException();
        }
    }
}
