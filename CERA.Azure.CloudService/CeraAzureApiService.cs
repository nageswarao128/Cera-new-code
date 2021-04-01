using CERA.AuthenticationService;
using CERA.CloudService;
using CERA.Entities;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using ICeraAuthenticator = CERA.CloudService.ICeraAuthenticator;

namespace CERA.Azure.CloudService
{
    public class CeraAzureApiService : ICeraAzureApiService
    {
        public CeraAzureApiService()
        {
        }
        public List<CeraPlatformConfigViewModel> _platformConfigs { get; set; }
        ICeraAuthenticator authenticator;
      public List<CeraSubscription> _subscription { get; set; }
        public ICeraLogger Logger { get; set; }
        

        public CeraAzureApiService(ICeraLogger logger)
        {
            Logger = logger;
           
        }

        /// <summary>
        /// This method will calls the required authentication and after  authenticating it will 
        /// get the available Resources List from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="subscriptions"></param>
        /// <returns>returns a list of resources from Azure</returns>
        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                List<CeraResources> ceraResources = new List<CeraResources>();
                foreach (var sub in subscriptions)
                {
                    var azureResources = authClient.WithSubscription(sub.SubscriptionId).GenericResources.ListAsync().Result;
                    Logger.LogInfo("Got Resources List from a subscription in Azure Cloud Provider");
                    if (azureResources != null)
                    {
                        Logger.LogInfo("Parsing Resources List To CERA Resources");

                        foreach (var resource in azureResources)
                        {
                            ceraResources.Add(new CeraResources
                            {
                                Name = resource.Name,
                                RegionName = resource.RegionName,
                                ResourceGroupName = resource.ResourceGroupName,
                                ResourceType = resource.ResourceType
                            });
                        }
                    }
                    Logger.LogInfo("Parsing Completed Resources List To CERA Resources");
                    return ceraResources;

                }
                Logger.LogInfo("No Resources List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will calls the required authentication and after  authenticating it will 
        /// get the available ResourceGroups List from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="subscriptions"></param>
        /// <returns>returns a list of resourceGroups from Azure</returns>
        public List<CeraResourceGroups> GetCloudResourceGroups(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                List<CeraResourceGroups> ceraResourceGroups = new List<CeraResourceGroups>();
                foreach (var sub in subscriptions)
                {
                    var azureResourceGroups = authClient.WithSubscription(sub.SubscriptionId).ResourceGroups.ListAsync().Result;
                    Logger.LogInfo("Got Resources Groups List from a subscription in Azure Cloud Provider");
                    if (azureResourceGroups != null)
                    {
                        Logger.LogInfo("Parsing ResourceGroups List To CERA Resources");

                        foreach (var resource in azureResourceGroups)
                        {
                            ceraResourceGroups.Add(new CeraResourceGroups
                            {
                                Name = resource.Name,
                                RegionName = resource.RegionName,
                                provisioningstate = resource.ProvisioningState
                                
                            });
                        }
                    }
                    Logger.LogInfo("Parsing Completed ResourceGroups List To CERA Resources");
                    return ceraResourceGroups;

                }
                Logger.LogInfo("No ResourceGroup List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will calls the required authentication and after  authenticating it will 
        /// get the available StorageAccount List from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="subscriptions"></param>
        /// <returns>returns a list of StorageAccount from Azure</returns>
        public List<CeraStorageAccount> GetCloudStorageAccountList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                List<CeraStorageAccount> ceraStorageAccounts = new List<CeraStorageAccount>();
                foreach (var sub in subscriptions)
                {
                    var azureStorageAccount = authClient.WithSubscription(sub.SubscriptionId).StorageAccounts.ListAsync().Result;
                    Logger.LogInfo("Got StorageAccount  List from a subscription in Azure Cloud Provider");
                    if (azureStorageAccount != null)
                    {
                        Logger.LogInfo("Parsing ResourceGroups List To CERA Resources");

                        foreach (var storage in azureStorageAccount)
                        {
                            ceraStorageAccounts.Add(new CeraStorageAccount
                            {
                                Name = storage.Name,
                                RegionName = storage.RegionName,
                                ResourceGroupName=storage.ResourceGroupName

                            });
                        }
                    }
                    Logger.LogInfo("Parsing Completed ResourceGroups List To CERA Resources");
                    return ceraStorageAccounts;

                }
                Logger.LogInfo("No ResourceGroup List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will calls the required authentication and after  authenticating it will 
        /// get the available SqlServer List from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="subscriptions"></param>
        /// <returns>returns a list of SqlServer from Azure</returns>
        public List<CeraSqlServer> GetCloudSqlServersList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                List<CeraSqlServer> cerasqlServer = new List<CeraSqlServer>();
                foreach (var sub in subscriptions)
                {
                    var azureSqlserver = authClient.WithSubscription(sub.SubscriptionId).SqlServers.ListAsync().Result;
                    var tenant=authClient.Tenants.ListAsync().Result;

                    Logger.LogInfo("Got SqlServer List from a subscription in Azure Cloud Provider");
                    if (azureSqlserver != null)
                    {
                        Logger.LogInfo("Parsing SqlServer List To CERA Resources");

                        foreach (var sqlServer in azureSqlserver)
                        {
                            cerasqlServer.Add(new CeraSqlServer
                            {
                                Name = sqlServer.Name,
                                RegionName = sqlServer.RegionName,
                                ResourceGroupName = sqlServer.ResourceGroupName

                            });
                        }
                    }
                    Logger.LogInfo("Parsing Completed SqlServers List To CERA Resources");
                    return cerasqlServer;

                }
                Logger.LogInfo("No ResourceGroup List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();

        }

        public object GetCloudMonthlyBillingList()
        {
            return new object();
        }


        public object GetCloudSqlDbList()
        {
            return new object();
        }
        public void Initialize(string tenantId, string clientID, string clientSecret,string authority)
        {
            authenticator = new CeraAzureAuthenticator(Logger);
            authenticator.Initialize(tenantId, clientID, clientSecret,authority);
        }
        public void Initialize()
        {
            string clientId = AzureAuth.Default.ClientId;
            string tenantId = AzureAuth.Default.tenantId;
            string clientSecret = AzureAuth.Default.Clientsecert;
            string authority = AzureAuth.Default.authority;
            authenticator = new CeraAzureAuthenticator(Logger);
            authenticator.Initialize(tenantId, clientId, clientSecret,authority);
        }
        /// <summary>
        /// This method will calls the required authentication and after  authenticating it will 
        /// get the available Subscriptions List from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>returns a list of Subscriptions from Azure</returns>
        public List<CeraSubscription> GetCloudSubscriptionList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                var azureSubscriptions = authClient.Subscriptions.ListAsync().Result;
                Logger.LogInfo("Got Subscription List from Azure Cloud Provider");
                if (azureSubscriptions != null)
                {
                    Logger.LogInfo("Parsing Subscription List To CERA Subscription");
                    List<CeraSubscription> subscriptions = new List<CeraSubscription>();
                    foreach (var sub in azureSubscriptions)
                    {
                        subscriptions.Add(new CeraSubscription
                        {
                            SubscriptionId = sub.SubscriptionId,
                            DisplayName = sub.DisplayName,
                            TenantID = sub.Inner.TenantId,

                        });
                        
                    }
                    Logger.LogInfo("Parsing Completed Subscription List To CERA Subscription");
                    
                    return subscriptions;
                }
                Logger.LogInfo("No Subscription List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        public object GetCloudServicePlanList()
        {
            return new object();
        }
        /// <summary>
        /// This method will calls the required authentication and after  authenticating it will 
        /// get the available Tenant List from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns>returns a list of Tenants from Azure</returns>
        public List<CeraTenants> GetCloudTenantList(RequestInfoViewModel requestInfo)
        {
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                var azureTenants = authClient.Tenants.ListAsync().Result;
                Logger.LogInfo("Got Tenants List from Azure Cloud Provider");
                if (azureTenants != null)
                {
                    Logger.LogInfo("Parsing Tenants List To CERA Tenants");
                    List<CeraTenants> tenants = new List<CeraTenants>();
                    foreach (var Tenants in azureTenants)
                    {
                        tenants.Add(new CeraTenants
                        {
                            Key = Tenants.Key,
                            TenantId = Tenants.TenantId                            
                        });

                    }
                    Logger.LogInfo("Parsing Completed Tenants List To CERA Subscription");

                    return tenants;
                }
                Logger.LogInfo("No Tenants List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will calls the required authentication and after  authenticating it will 
        /// get the available VirtualMachines List from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="subscriptions"></param>
        /// <returns>returns a list of Virtual Machines from Azure</returns>
        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {            
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                foreach (var sub in subscriptions)
                {
                    var VM = authClient.WithSubscription(sub.SubscriptionId).VirtualMachines.ListAsync().Result;
                    Logger.LogInfo("Got Virtual Machines List from Azure Cloud Provider");
                    if (VM != null)
                    {
                        Logger.LogInfo("Parsing Virtual Machines List To CERA Resources");
                        List<CeraVM> ceraVM = new List<CeraVM>();
                        foreach (var virtualMachine in VM)
                        {
                            ceraVM.Add(new CeraVM
                            {
                                VMName = virtualMachine.Name,
                                RegionName = virtualMachine.RegionName,
                                ResourceGroupName = virtualMachine.ResourceGroupName,

                            });
                        }
                        Logger.LogInfo("Parsing Completed VM's List To CERA Resources");

                        return ceraVM;
                    }
                }
                Logger.LogInfo("No VM's List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will calls the required authentication and after  authenticating it will 
        /// get the available WebApp List from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="subscriptions"></param>
        /// <returns>returns a list of WebApp from Azure</returns>
        public List<CeraWebApps> GetCloudWebAppList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                foreach (var sub in subscriptions)
                {
                    var webapps = authClient.WithSubscription(sub.SubscriptionId).WebApps.ListAsync().Result;
                    Logger.LogInfo("Got WebApps List from Azure Cloud Provider");
                    if (webapps != null)
                    {
                        Logger.LogInfo("Parsing WebApps List To CERA Resources");
                        List<CeraWebApps> ceraWebApps = new List<CeraWebApps>();
                        foreach (var WebApps in webapps)
                        {
                            ceraWebApps.Add(new CeraWebApps
                            {
                                Name = WebApps.Name,
                                RegionName = WebApps.RegionName,
                                ResourceGroupName = WebApps.ResourceGroupName

                            });
                        }
                        Logger.LogInfo("Parsing Completed WebApps List To CERA Resources");

                        return ceraWebApps;
                    }
                }
                Logger.LogInfo("No WebApps List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will calls the required authentication and after  authenticating it will 
        /// get the available AppServicePlans List from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="subscriptions"></param>
        /// <returns>returns a list of AppServicePlans from Azure</returns>
        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                foreach (var sub in subscriptions)
                {
                    var AppServicePlan = authClient.WithSubscription(sub.SubscriptionId).AppServices.AppServicePlans.ListAsync().Result;
                    Logger.LogInfo("Got AppServicePlans List from Azure Cloud Provider");
                    if (AppServicePlan != null)
                    {
                        Logger.LogInfo("Parsing AppServicePlans List To CERA Resources");
                        List<CeraAppServicePlans> appServicePlans = new List<CeraAppServicePlans>();
                        foreach (var appService in AppServicePlan)
                        {
                            appServicePlans.Add(new CeraAppServicePlans
                            {
                                Name = appService.Name,
                                RegionName = appService.RegionName,
                                ResourceGroupName = appService.ResourceGroupName

                            });
                        }
                        Logger.LogInfo("Parsing Completed AppServicePlans List To CERA Resources");

                        return appServicePlans;
                    }
                }
                Logger.LogInfo("No WebApps List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        /// <summary>
        /// This method will calls the required authentication and after  authenticating it will 
        /// get the available Disks List from Azure cloud
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="subscriptions"></param>
        /// <returns>returns a list of Disks from Azure</returns>
        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize();
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                foreach (var sub in subscriptions)
                {
                    var Disks = authClient.WithSubscription(sub.SubscriptionId).Disks.ListAsync().Result;
                    Logger.LogInfo("Got Disks List from Azure Cloud Provider");
                    if (Disks != null)
                    {
                        Logger.LogInfo("Parsing Disks List To CERA Resources");
                        List<CeraDisks> ceraDisks = new List<CeraDisks>();
                        foreach (var disk in Disks)
                        {
                            ceraDisks.Add(new CeraDisks
                            {
                                Name = disk.Name,
                                RegionName = disk.RegionName,
                                ResourceGroupName = disk.ResourceGroupName

                            });
                        }
                        Logger.LogInfo("Parsing Completed Disks List To CERA Resources");

                        return ceraDisks;
                    }
                }
                Logger.LogInfo("No WebApps List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraWebApps> GetCloudWebAppList(RequestInfoViewModel requestInfo)
        {
            return new List<CeraWebApps>();
        }
        public List<CeraSubscription> GetSubscriptionList()
        {
            return new List<CeraSubscription>();
        }
        public List<CeraResources> GetResourcesList()
        {
            return new List<CeraResources>();
        }
        public List<CeraVM> GetVMList()
        {
            return new List<CeraVM>();
        }
        public List<CeraResourceGroups> GetResourceGroupsList()
        {
            return new List<CeraResourceGroups>();
        }
        public List<CeraResourceGroups> GetCloudResourceGroups(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }     

        public List<CeraStorageAccount> GetCloudStorageAccountList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraStorageAccount> GetStorageAccountList()
        {
            throw new NotImplementedException();
        }
        public List<CeraSqlServer> GetCloudSqlServersList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraSqlServer> GetSqlServersList()
        {
            throw new NotImplementedException();
        }

        public List<CeraTenants> GetTenantsList()
        {
            throw new NotImplementedException();
        }

        public List<CeraWebApps> GetWebAppsList()
        {
            throw new NotImplementedException();
        }

        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraAppServicePlans> GetAppServicePlansList()
        {
            throw new NotImplementedException();
        }
        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraDisks> GetDisksList()
        {
            throw new NotImplementedException();
        }
    }
}
