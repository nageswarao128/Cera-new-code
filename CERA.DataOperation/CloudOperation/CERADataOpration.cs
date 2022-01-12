using CERA.Converter;
using CERA.DataOperation.Data;
using CERA.Entities;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using Microsoft.Data.SqlClient;
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

        public CERADataOperation(CeraDbContext dbContext, ICeraLogger logger, ICeraConverter converter, CeraSpContext spContext)
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
                List<CeraResources> dbdata = null;
                foreach (var item in resources)
                {
                    dbdata = _dbContext.Resources.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.ResourceID);
                var AddData = resources.Where(m => !diffids.Contains(m.ResourceID)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.Resources.Add(item);
                }
                var diffidsfordb = resources.Select(s => s.ResourceID);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.ResourceID)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.Resources.Update(data);
                }
                int record = _dbContext.SaveChanges();
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
                var Resources = _dbContext.Resources.Where(x => x.IsActive == true).ToList();
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

                List<CeraStorageAccount> dbdata = null;
                foreach (var item in storageAccounts)
                {
                    dbdata = _dbContext.StorageAccounts.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.StorageAccountId);
                var AddData = storageAccounts.Where(m => !diffids.Contains(m.StorageAccountId)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.StorageAccounts.Add(item);
                }
                var diffidsfordb = storageAccounts.Select(s => s.StorageAccountId);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.StorageAccountId)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.StorageAccounts.Update(data);
                }

                int record = _dbContext.SaveChanges();
                return record;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return 0;
            }
        }
        public int AddStorageSize(List<StorageSize> data)
        {
            try
            {
                var dbData = _dbContext.storageSize.ToList();
                foreach (var item in dbData)
                {
                    _dbContext.storageSize.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in data)
                {
                    _dbContext.storageSize.Add(item);
                }
                int record = _dbContext.SaveChanges();
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
        public List<StorageAccountsVM> GetStorageAccount()
        {
            try
            {
                var data = _spContext.storageAccounts.FromSqlRaw<StorageAccountsVM>("[dbo].[Sp_StorageAccounts]").ToList();
                _logger.LogInfo("Data retrieved for StorageAccounts List from Database");
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }


        public List<StorageSize> GetStorageSizes()
        {
            try
            {
                var data = _dbContext.storageSize.ToList();
                return data;
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
                List<CeraResourceGroups> dbdata = null;
                foreach (var item in resources)
                {
                    dbdata = _dbContext.resourceGroups.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.Name);
                var AddData = resources.Where(m => !diffids.Contains(m.Name)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.resourceGroups.Add(item);
                }
                var diffidsfordb = resources.Select(s => s.Name);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.Name)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.resourceGroups.Update(data);
                }
                int record = _dbContext.SaveChanges();
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
        public List<ResourceGroupsVM> GetResourceGroups()
        {
            try
            {
                var ResourceGroups = _spContext.resourceGroups.FromSqlRaw<ResourceGroupsVM>("[dbo].[Sp_ResourceGroups]").ToList();
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
                List<CeraSubscription> dbdata = null;
                foreach (var item in subscriptions)
                {
                    dbdata = _dbContext.Subscriptions.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.SubscriptionId);
                var AddData = subscriptions.Where(m => !diffids.Contains(m.SubscriptionId)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.Subscriptions.Add(item);
                }
                var diffidsfordb = subscriptions.Select(s => s.SubscriptionId);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.SubscriptionId)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.Subscriptions.Update(data);
                }
                int record = _dbContext.SaveChanges();
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
                var subscriptions = _dbContext.Subscriptions.Where(x => x.IsActive == true).ToList();

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
                List<CeraSqlServer> dbdata = null;
                foreach (var item in sqlServer)
                {
                    dbdata = _dbContext.SqlServers.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.Name);
                var AddData = sqlServer.Where(m => !diffids.Contains(m.Name)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.SqlServers.Add(item);
                }
                var diffidsfordb = sqlServer.Select(s => s.Name);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.Name)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.SqlServers.Update(data);
                }
                int record = _dbContext.SaveChanges();
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
        public int AddVMData(List<CeraVM> ceraVMs)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                List<CeraVM> dbdata = null;

                foreach (var item in ceraVMs)
                {
                    dbdata = _dbContext.ceraVMs.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.VMName);
                var AddData = ceraVMs.Where(m => !diffids.Contains(m.VMName)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.ceraVMs.Add(item);
                }
                var diffidsfordb = ceraVMs.Select(s => s.VMName);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.VMName)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.ceraVMs.Update(data);
                }
                int record = _dbContext.SaveChanges();
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
        public List<ResourcesModel> GetVM()
        {
            try
            {
                var Vm = _spContext.resources.FromSqlRaw<ResourcesModel>("[dbo].[Sp_VM]").ToList();
                _logger.LogInfo("Data retrieved for Virtual Machines List from Database");
                return Vm;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }

        public List<ResourcesModel> GetResourceGroupResources(string name)
        {
            try
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@name";
                parameter.SqlDbType= System.Data.SqlDbType.NVarChar;
                parameter.Value = name;
                var resources = _spContext.resources.FromSqlRaw<ResourcesModel>("Sp_ResourceGroupResources @name",parameter).ToList();
                _logger.LogInfo($"Resources Data retrieved for Resource Group : {name} ");
                return resources;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }

        public List<BarChartModel> GetBarChartCloudData(string cloud)
        {
            try
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@cloudProvider";
                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = cloud;
                var resources = _spContext.barCharts.FromSqlRaw<BarChartModel>("Sp_BarChartCloudFilter @cloudProvider", parameter).ToList();
                _logger.LogInfo($"Resources Data retrieved for Resource Group : {cloud} ");
                return resources;
            }
            catch (Exception ex)
            {

                _logger.LogException(ex);
                return null;
            }
        }
        public List<BarChartModel> GetBarChartSubscriptionData(string cloud, string subscriptionId)
        {
            try
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@cloud";
                parameter.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter.Value = cloud;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@subscriptionId";
                parameter1.SqlDbType = System.Data.SqlDbType.NVarChar;
                parameter1.Value = subscriptionId;

                var resources = _spContext.barCharts.FromSqlRaw<BarChartModel>("Sp_BarChartSubscriptionFilter @cloud,@subscriptionId", parameter,parameter1).ToList();
                _logger.LogInfo($"Resources Data retrieved for Resource Group : {subscriptionId} ");
                return resources;
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
                List<CeraTenants> dbdata = null;
                foreach (var item in tenants)
                {
                    dbdata = _dbContext.CeraTenants.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.TenantId);
                var AddData = tenants.Where(m => !diffids.Contains(m.TenantId)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.CeraTenants.Add(item);
                }
                var diffidsfordb = tenants.Select(s => s.TenantId);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.TenantId)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.CeraTenants.Update(data);
                }
                int record = _dbContext.SaveChanges();
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
                List<CeraWebApps> dbdata = null;
                foreach (var item in webApps)
                {
                    dbdata = _dbContext.CeraWebApps.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.Name);
                var AddData = webApps.Where(m => !diffids.Contains(m.Name)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.CeraWebApps.Add(item);
                }
                var diffidsfordb = webApps.Select(s => s.Name);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.Name)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.CeraWebApps.Update(data);
                }
                int record = _dbContext.SaveChanges();
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
        public List<ResourcesModel> GetWebApps()
        {
            try
            {
                var webApps = _spContext.resources.FromSqlRaw<ResourcesModel>("[dbo].[Sp_WebApps]").ToList();
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
                List<CeraAppServicePlans> dbdata = null;
                foreach (var item in ceraAppServices)
                {
                    dbdata = _dbContext.AppServicePlans.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.Name);
                var AddData = ceraAppServices.Where(m => !diffids.Contains(m.Name)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.AppServicePlans.Add(item);
                }
                var diffidsfordb = ceraAppServices.Select(s => s.Name);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.Name)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.AppServicePlans.Update(data);
                }
                int record = _dbContext.SaveChanges();
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
                List<CeraDisks> dbdata = null;
                foreach (var item in Disks)
                {
                    dbdata = _dbContext.Disks.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.Name);
                var AddData = Disks.Where(m => !diffids.Contains(m.Name)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.Disks.Add(item);
                }
                var diffidsfordb = Disks.Select(s => s.Name);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.Name)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.Disks.Update(data);
                }
                int record = _dbContext.SaveChanges();
                return record;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return 0;
            }
        }
        public int AddAdvisorRecommendations(List<AdvisorRecommendations> recommendations)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var advisorRecommendations = _dbContext.AdvisorRecommendations.ToList();
                foreach (var item in advisorRecommendations)
                {
                    _dbContext.AdvisorRecommendations.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in recommendations)
                {
                    _dbContext.AdvisorRecommendations.Add(item);
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
                List<CeraResourceHealth> dbdata = null;
                foreach (var item in resourceHealth)
                {
                    dbdata = _dbContext.ResourceHealth.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.ResourceId);
                var AddData = resourceHealth.Where(m => !diffids.Contains(m.ResourceId)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.ResourceHealth.Add(item);
                }
                var diffidsfordb = resourceHealth.Select(s => s.ResourceId);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.ResourceId)).ToList();

                foreach (var data in UpdateData)
                {
                    data.IsActive = false;
                    _dbContext.ResourceHealth.Update(data);
                }
                int record = _dbContext.SaveChanges();
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
                var resourceHealth = _dbContext.ResourceHealth.Where(x => x.IsActive == true).ToList();
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
                List<CeraCompliances> dbdata = null;
                foreach (var item in data)
                {
                    dbdata = _dbContext.Compliances.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.Name);
                var AddData = data.Where(m => !diffids.Contains(m.Name)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.Compliances.Add(item);
                }
                var diffidsfordb = data.Select(s => s.Name);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.Name)).ToList();

                foreach (var item in UpdateData)
                {
                    item.IsActive = false;
                    _dbContext.Compliances.Update(item);
                }
                int record = _dbContext.SaveChanges();
                return record;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return 0;
            }
        }
        public int AddPolicyRules(List<PolicyRules> data)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                var policies = _dbContext.PolicyRules.ToList();
                foreach (var item in policies)
                {
                    _dbContext.PolicyRules.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in data)
                {
                    item.id = Guid.NewGuid();
                    _dbContext.PolicyRules.Add(item);
                }
                int record = _dbContext.SaveChanges();
                _logger.LogInfo("Policy Rules Imported Successfully");
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
                List<CeraPolicy> dbdata = null;
                foreach (var item in data)
                {
                    dbdata = _dbContext.Policies.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.PolicyId);
                var AddData = data.Where(m => !diffids.Contains(m.PolicyId)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.Policies.Add(item);
                }
                var diffidsfordb = data.Select(s => s.PolicyId);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.PolicyId)).ToList();

                foreach (var item in UpdateData)
                {
                    item.IsActive = false;
                    _dbContext.Policies.Update(item);
                }
                int record = _dbContext.SaveChanges();
                return record;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return 0;
            }
        }
        public int AddUsageDetails(List<CeraUsage> data)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                List<CeraUsage> dbdata = null;
                foreach (var item in data)
                {
                    dbdata = _dbContext.UsageDetails.Where(x => x.CloudProvider == item.CloudProvider).ToList();
                }

                foreach (var item in dbdata)
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
        public int AddUsageByMonth(List<UsageByMonth> data)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                List<UsageByMonth> dbdata = null;
                foreach (var item in data)
                {
                    dbdata = _dbContext.UsageByMonth.Where(x => x.CloudProvider == item.CloudProvider).ToList();
                }
                foreach (var item in dbdata)
                {
                    _dbContext.UsageByMonth.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in data)
                {
                    _dbContext.UsageByMonth.Add(item);
                }
                int record = _dbContext.SaveChanges();
                _logger.LogInfo("Usage for month Data Imported Successfully");
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return 0;
            }
        }

        public int AddUsageHistory(List<UsageHistory> data)
        {
            try
            {
                _logger.LogInfo("Receive Data");
                List<UsageHistory> dbdata = null;
                foreach (var item in data)
                {
                    dbdata = _dbContext.usageHistory.Where(x => x.CloudProvider == item.CloudProvider).ToList();
                }
                foreach (var item in dbdata)
                {
                    _dbContext.usageHistory.Remove(item);
                }
                _dbContext.SaveChanges();
                foreach (var item in data)
                {
                    _dbContext.usageHistory.Add(item);
                }
                int record = _dbContext.SaveChanges();
                _logger.LogInfo("Usage History Data Imported Successfully");
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return 0;
            }
        }
        public List<UsageByMonth> GetUsageByMonth()
        {
            try
            {
                var usageByMonth = _dbContext.UsageByMonth.ToList();
                _logger.LogInfo("Data retrieved for Usage for Month from Database");
                return usageByMonth;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
        public List<UsageHistory> GetUsageHistory()
        {
            try
            {
                var usageHistory = _dbContext.usageHistory.ToList();
                _logger.LogInfo("Data retrieved for Usage History from Database");
                return usageHistory;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
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
                List<AzureLocations> dbdata = null;
                foreach (var item in data)
                {
                    dbdata = _dbContext.Locations.Where(x => x.CloudProvider == item.CloudProvider && x.IsActive == true).ToList();
                }

                var diffids = dbdata.Select(s => s.name);
                var AddData = data.Where(m => !diffids.Contains(m.name)).ToList();
                foreach (var item in AddData)
                {
                    _dbContext.Locations.Add(item);
                }
                var diffidsfordb = data.Select(s => s.name);
                var UpdateData = dbdata.Where(m => !diffidsfordb.Contains(m.name)).ToList();

                foreach (var item in UpdateData)
                {
                    item.IsActive = false;
                    _dbContext.Locations.Update(item);
                }
                int record = _dbContext.SaveChanges();
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
        public List<AdvisorRecommendations> GetAdvisorRecommendations()
        {
            try
            {
                return _dbContext.AdvisorRecommendations.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }
        }
        public List<PolicyRules> GetPolicyRules()
        {
            try
            {
                return _dbContext.PolicyRules.ToList();
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
