﻿using CERA.Converter;
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
        /// <summary>
        /// This method will inserts the resources data into database
        /// </summary>
        /// <param name="resources"></param>
        /// <returns>returns 1 or 0</returns>
        public int AddResourcesData(List<CeraResources> resources)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var resource = _dbContext.Resources.ToList();
                foreach(var item in resource)
                {
                    _dbContext.Resources.Remove(item);
                    
                }
                _dbContext.SaveChanges();
                foreach (var Resource in resources)
                {
                    
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

        /// <summary>
        /// This method will retrives the Resources data from database
        /// </summary>
        /// <returns>returns Resources data</returns>
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
        /// <summary>
        /// This method will inserts the StorageAccount data into database
        /// </summary>
        /// <param name="resources"></param>
        /// <returns>returns 1 or 0</returns>
        public int AddStorageAccountData(List<CeraStorageAccount> storageAccounts)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var storage = _dbContext.StorageAccounts.ToList();
                foreach(var item in storage)
                {
                    _dbContext.StorageAccounts.Remove(item);
                    
                }
                _dbContext.SaveChanges();
                foreach (var ceraStorage in storageAccounts)
                {
                    _dbContext.StorageAccounts.Add(ceraStorage);
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
        /// <summary>
        /// This method will retrives the StorageAccount data from database
        /// </summary>
        /// <returns>returns StorageAccount data</returns>
        public List<CeraStorageAccount> GetStorageAccount()
        {
            try
            {
                var storageAccounts = _dbContext.StorageAccounts.ToList();
                _logger.LogInfo("Data retrieved for StorageAccounts List from Database");
                return storageAccounts;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will inserts the resourceGroups data into database
        /// </summary>
        /// <param name="resources"></param>
        /// <returns>returns 1 or 0</returns>
        public int AddResourceGroupData(List<CeraResourceGroups> resources)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var resourcegroup = _dbContext.resourceGroups.ToList();
                foreach(var item in resourcegroup)
                {
                    _dbContext.resourceGroups.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var Resource in resources)
                {
                    _dbContext.resourceGroups.Add(Resource);
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
        /// <summary>
        /// This method will retrives the ResourceGroups data from database
        /// </summary>
        /// <returns>returns ResourceGroups data</returns>
        public List<CeraResourceGroups> GetResourceGroups()
        {
            try
            {
                var ResourceGroups = _dbContext.resourceGroups.ToList();
                _logger.LogInfo("Data retrieved for ResourceGroups List from Database");
                return ResourceGroups;
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
                var sub = _dbContext.Subscriptions.ToList();
                foreach(var item in sub)
                {
                    _dbContext.Subscriptions.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var subscription in subscriptions)
                {
                    _dbContext.Subscriptions.Add(subscription);
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
        /// <summary>
        /// This method will inserts the Virtual MAchines data into database
        /// </summary>
        /// <param name="resources"></param>
        /// <returns>returns 1 or 0</returns>
        public int AddVMData( List<CeraVM> ceraVMs)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var VM = _dbContext.ceraVMs.ToList();
                foreach(var item in VM)
                {
                    _dbContext.ceraVMs.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var vm in ceraVMs)
                {
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
        /// <summary>
        /// This method will retrives the VirtualMachines data from database
        /// </summary>
        /// <returns>returns VirtualMachines data</returns>
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
