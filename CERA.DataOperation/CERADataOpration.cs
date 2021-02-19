using CERA.Converter;
using CERA.DataOperation.Data;
using CERA.Entities;
using CERA.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CERA.DataOperation
{
    public class CERADataOperation : ICeraDataOperation
    {
        private readonly ICeraLogger _logger;
        protected readonly CeraDbContext _dbContext;
        private readonly ICeraConverter _converter;

        public CERADataOperation(CeraDbContext dbContext, ICeraLogger logger, ICeraConverter converter)
        {
            _logger = logger;
            _dbContext = dbContext;
            _converter = converter;
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

        public int AddSubscriptionData(List<CeraSubscription> data)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                string jsonData = _converter.GenerateJson(data);
                _logger.LogInfo("Converted Data into JSON Format");
                var sp_parameters = new List<SqlParameter>() { new SqlParameter("json", jsonData) };
                int record = _dbContext.Database.ExecuteSqlRaw($"Exec usp_Subscription_insert @json", sp_parameters);
                _logger.LogInfo("Data Imported Successfully");
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return 0;
            }
        }

        public List<CeraSubscription> GetSubscriptions()
        {
            try
            {
                var subscriptions = _dbContext.Subscriptions.ToList();
                _logger.LogInfo("Data retrieved for Subcription List from Database");
                return subscriptions;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
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
