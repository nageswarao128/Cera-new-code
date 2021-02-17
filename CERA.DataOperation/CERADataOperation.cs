using CERA.CloudService.CERAEntities;
using CERA.DataOperation.CeraContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CERA.DataOperation
{
   
    public class CERADataOperation : ICeraDataOperation
    {
        protected readonly CeraDbContext _dbContext;
       
        public CERADataOperation(CeraDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public object AddResourceData(object data)
        {
            return new object();
        }

        public object AddServicePlanData(object data)
        {
            return new object();
        }

        public object AddSqlDbData(object data)
        {
            return new object();
        }

        public object AddSqlServerData(object data)
        {
            return new object();
        }

        public object AddSubscriptionData(List<CeraSubscriptionList> data)
        {

            int StepID = 1;
            int BatchID = 1;
            var jsonData = JsonConvert.SerializeObject(data);
            var sp_parameters = new List<SqlParameter>() { new SqlParameter("json", jsonData) };
            int record = _dbContext.Database.ExecuteSqlRaw($"Exec Subscription_inserting @json" , sp_parameters);
            int sample = _dbContext.Database.ExecuteSqlRaw("Exec sp_GetSubscriptionList");


            return new object();
        }

        

        public object AddTenantData(object data)
        {
            return new object();
        }

        public object AddVMData(object data)
        {
            return new object();
        }

        public object AddWebAppData(object data)
        {
            return new object();
        }

        public object UpdateResourceData(object data)
        {
            return new object();
        }

        public object UpdateServicePlanData(object data)
        {
            return new object();
        }

        public object UpdateSqlDbData(object data)
        {
            return new object();
        }

        public object UpdateSqlServerData(object data)
        {
            return new object();
        }

        public object UpdateSubscriptionData(object data)
        {
            return new object();
        }

        public object UpdateTenantData(object data)
        {
            return new object();
        }

        public object UpdateVMData(object data)
        {
            return new object();
        }

        public object UpdateWebAppData(object data)
        {
            return new object();
        }

        
    }
}
