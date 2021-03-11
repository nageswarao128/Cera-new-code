using CERA.Converter;
using CERA.DataOperation.Data;
using CERA.Entities;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CERA.DataOperation
{
    public partial class CERADataOperation : ICeraDataOperation
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
        public int AddResourcesData(List<CeraResources> resources)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                foreach (var Resource in resources)
                {
                    if (_dbContext.Resources.Where(x => x.Name == Resource.Name).Count() > 0)
                       _dbContext.Resources.Remove(Resource);
                    _dbContext.Resources.Add(Resource);
                }
                int record = _dbContext.SaveChanges();
                _logger.LogInfo("Data Imported Successfully");
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return 0;
            }
        }
        public List<CeraResources> GetResources()
        {
            try
            {
                var Resources = _dbContext.Resources.ToList();
                _logger.LogInfo("Data retrieved for Resources List from Database");
                return Resources;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }



        public int AddSubscriptionData(List<CeraSubscription> subscriptions)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                foreach (var subscription in subscriptions)
                {
                    if (_dbContext.Subscriptions.Where(x => x.SubscriptionId == subscription.SubscriptionId).Count() > 0)
                        _dbContext.Subscriptions.Remove(subscription);
                    _dbContext.Subscriptions.Add(subscription);
                }
                int record = _dbContext.SaveChanges();
                //var sp_parameters = new List<SqlParameter>() { new SqlParameter("json", jsonData) };
                //int record = _dbContext.Database.ExecuteSqlRaw($"Exec usp_Subscription_insert @json", sp_parameters);
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

       

        public int AddVMData( List<CeraVM> ceraVMs)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                foreach (var vm in ceraVMs)
                {
                    if (_dbContext.ceraVMs.Where(x => x.VMName == vm.VMName).Count() > 0)
                        _dbContext.ceraVMs.Remove(vm);
                    _dbContext.ceraVMs.Add(vm);
                }
                int record = _dbContext.SaveChanges();
                _logger.LogInfo("Data Imported Successfully");
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return 0;
            }
        }
        public List<CeraVM> GetVM()
        {
            try
            {
                var Vm = _dbContext.ceraVMs.ToList();
                _logger.LogInfo("Data retrieved for Virtual Machines List from Database");
                return Vm;
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
