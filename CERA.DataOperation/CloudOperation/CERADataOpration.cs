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
        private readonly CeraSpContext _spContext;

        public CERADataOperation(CeraDbContext dbContext, ICeraLogger logger, ICeraConverter converter,CeraSpContext spContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _converter = converter;
            _spContext = spContext;
        }
      
        public object AddServicePlanData(object data)
        {
            return new object();
        }

        public object AddSqlDbData(object data)
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
        /// <summary>
        /// This method will inserts the subscriptions data into database
        /// </summary>
        /// <param name="subscriptions"></param>
        /// <returns>returns 1 or 0</returns>
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
        /// <summary>
        /// This method will retrives the Subscriptions data from database
        /// </summary>
        /// <returns>returns Subscriptions data</returns>
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
        /// This method will inserts the SqlServer data into database
        /// </summary>
        /// <param name="sqlServer"></param>
        /// <returns>returns 1 or 0</returns>
        public int AddSqlServerData(List<CeraSqlServer> sqlServer)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var ceraSqlServers = _dbContext.SqlServers.ToList();
                foreach (var item in ceraSqlServers)
                {
                    _dbContext.SqlServers.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var sqlservers in sqlServer)
                {
                    _dbContext.SqlServers.Add(sqlservers);
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
        /// This method will retrives the SqlServer data from database
        /// </summary>
        /// <returns>returns SqlServer data</returns>
        public List<CeraSqlServer> GetSqlServers()
        {
            try
            {
                var sqlServers = _dbContext.SqlServers.ToList();
                _logger.LogInfo("Data retrieved for SqlServers List from Database");
                return sqlServers;
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
        /// <summary>
        /// This method will inserts the Tenant data into database
        /// </summary>
        /// <param name="tenants"></param>
        /// <returns>returns 1 or 0</returns>
        public int AddTenantData(List<CeraTenants> tenants)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var Tenant = _dbContext.CeraTenants.ToList();
                foreach (var item in Tenant)
                {
                    _dbContext.CeraTenants.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var Tenants in tenants)
                {
                    _dbContext.CeraTenants.Add(Tenants);
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
        /// This method will retrives the Tenants data from database
        /// </summary>
        /// <returns>returns Tenant data</returns>
        public List<CeraTenants> GetTenants()
        {
            try
            {
                var tenants = _dbContext.CeraTenants.ToList();
                _logger.LogInfo("Data retrieved for Tenants List from Database");
                return tenants;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will inserts the WebApp data into database
        /// </summary>
        /// <param name="webApps"></param>
        /// <returns>returns 1 or 0</returns>
        public int AddWebAppData(List<CeraWebApps> webApps)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var ceraWebApps = _dbContext.CeraWebApps.ToList();
                foreach (var item in ceraWebApps)
                {
                    _dbContext.CeraWebApps.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var Webapps in webApps)
                {
                    _dbContext.CeraWebApps.Add(Webapps);
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
        /// This method will retrives the WebApps data from database
        /// </summary>
        /// <returns>returns WebApps data</returns>
        public List<CeraWebApps> GetWebApps()
        {
            try
            {
                var webApps = _dbContext.CeraWebApps.ToList();
                _logger.LogInfo("Data retrieved for WebApps List from Database");
                return webApps;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will inserts the AppServicePlans data into database
        /// </summary>
        /// <param name="ceraAppServices"></param>
        /// <returns>returns 1 or 0</returns>
        public int AddAppServicePlans(List<CeraAppServicePlans> ceraAppServices)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var ceraappservice = _dbContext.AppServicePlans.ToList();
                foreach (var item in ceraappservice)
                {
                    _dbContext.AppServicePlans.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var ceraAppService in ceraAppServices)
                {
                    _dbContext.AppServicePlans.Add(ceraAppService);
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
        /// This method will retrives the AppServicePlans data from database
        /// </summary>
        /// <returns>returns AppServicePlans data</returns>
        public List<CeraAppServicePlans> GetAppServicePlans()
        {
            try
            {
                var appServicePlans = _dbContext.AppServicePlans.ToList();
                _logger.LogInfo("Data retrieved for AppServicePlans List from Database");
                return appServicePlans;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will inserts the Disks data into database
        /// </summary>
        /// <param name="Disks"></param>
        /// <returns>returns 1 or 0</returns>
        public int AddDisksData(List<CeraDisks> Disks)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var ceraDisks = _dbContext.Disks.ToList();
                foreach (var item in ceraDisks)
                {
                    _dbContext.Disks.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var disks in Disks)
                {
                    _dbContext.Disks.Add(disks);
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
        public int AddResourceHealth(List<CeraResourceHealth> resourceHealth)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var health = _dbContext.ResourceHealth.ToList();
                foreach (var item in health)
                {
                    _dbContext.ResourceHealth.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in resourceHealth)
                {
                    _dbContext.ResourceHealth.Add(item);
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

        public List<CeraResourceHealth> GetResourceHealth()
        {
            try
            {
                var resourceHealth = _dbContext.ResourceHealth.ToList();
                _logger.LogInfo("Data retrieved for Resources Health List from Database");
                return resourceHealth;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }

        public int AddCompliances(List<CeraCompliances> data)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var compliances = _dbContext.Compliances.ToList();
                foreach (var item in compliances)
                {
                    _dbContext.Compliances.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in data)
                {
                    _dbContext.Compliances.Add(item);
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
        public int AddPolicyData(List<CeraPolicy> data)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var policies = _dbContext.Policies.ToList();
                foreach (var item in policies)
                {
                    _dbContext.Policies.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in data)
                {
                    _dbContext.Policies.Add(item);
                }
                int record = _dbContext.SaveChanges();
                _logger.LogInfo("Policy Data Imported Successfully");
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return 0;
            }
        }
        public List<CeraCompliances> GetCompliances()
        {
            try
            {
                var compliances = _dbContext.Compliances.ToList();
                _logger.LogInfo("Data retrieved for Compliances List from Database");
                return compliances;
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
        /// <summary>
        /// This method will retrives the Disks data from database
        /// </summary>
        /// <returns>returns Disks data</returns>

        public List<CeraDisks> GetDisks()
        {
            try
            {
                var Disks = _dbContext.Disks.ToList();
                _logger.LogInfo("Data retrieved for Disks List from Database");
                return Disks;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
        public int AddRateCard(List<CeraRateCard> data)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var rateCard = _dbContext.RateCard.ToList();
                foreach (var item in rateCard)
                {
                    _dbContext.RateCard.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in data)
                {
                    _dbContext.RateCard.Add(item);
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

        public List<CeraRateCard> GetRateCard()
        {
            try
            {
                var rateCard = _dbContext.RateCard.ToList();
                _logger.LogInfo("Data retrieved for RateCard List from Database");
                return rateCard;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
        public int AddUsageDetails(List<CeraUsage> data)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var usage = _dbContext.UsageDetails.ToList();
                foreach (var item in usage)
                {
                    _dbContext.UsageDetails.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in data)
                {
                    _dbContext.UsageDetails.Add(item);
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
        public List<CeraUsage> GetUsageDetails()
        {
            try
            {
                var usage = _dbContext.UsageDetails.ToList();
                _logger.LogInfo("Data retrieved for Usage Details List from Database");
                return usage;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
        public List<CeraPolicy> GetPolicy()
        {
            try
            {
                var policy = _dbContext.Policies.ToList();
                _logger.LogInfo("Data retrieved for Policy List from Database");
                return policy;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
        public int AddLocationsData(List<AzureLocations> data)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var locations = _dbContext.Locations.ToList();
                foreach (var item in locations)
                {
                    _dbContext.Locations.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in data)
                {
                    _dbContext.Locations.Add(item);
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
        public List<AzureLocations> GetLocations()
        {
            try
            {
                var locations = _dbContext.Locations.ToList();
                _logger.LogInfo("Data retrieved for locations List from Database");
                return locations;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
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

        public int AddSqlDbData(List<CeraSqlServer> data)
        {
            throw new NotImplementedException();
        }

        public object AddSqlServerData(object data)
        {
            throw new NotImplementedException();
        }
    }
}
