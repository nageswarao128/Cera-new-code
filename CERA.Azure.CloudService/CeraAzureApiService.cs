using CERA.AuthenticationService;
using CERA.CloudService;
using CERA.Entities;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
                                ResourceGroupName = storage.ResourceGroupName

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

        public object GetCloudSqlServerList()
        {
            return new object();
        }
        public void Initialize(string tenantId, string clientID, string clientSecret, string authority)
        {
            authenticator = new CeraAzureAuthenticator(Logger);
            authenticator.Initialize(tenantId, clientID, clientSecret, authority);
        }
        public void Initialize()
        {
            string clientId = AzureAuth.Default.ClientId;
            string tenantId = AzureAuth.Default.tenantId;
            string clientSecret = AzureAuth.Default.Clientsecert;
            string authority = AzureAuth.Default.authority;
            authenticator = new CeraAzureAuthenticator(Logger);
            authenticator.Initialize(tenantId, clientId, clientSecret, authority);
        }

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

        public object GetCloudTenantList()
        {
            return new object();
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
                        Logger.LogInfo("Parsing Completed Resources List To CERA Resources");

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
        public async Task<List<CeraResourceHealth>> GetCloudResourceHealth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            //CeraResourceHealthDTO dTO = new CeraResourceHealthDTO();
            const string url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.ResourceHealth/availabilityStatuses?api-version=2020-05-01-preview";
            try
            {
                Initialize();
                string token = authenticator.GetAuthToken();
                if (token != null)
                {
                    foreach (var sub in subscriptions)
                    {
                        string uri = string.Format(url, sub.SubscriptionId);
                        HttpClient client = new HttpClient();
                        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
                        var data = await requestMessage.Content.ReadAsStringAsync();
                        List<CeraResourceHealthDTO> ceraResourceHealthDTO = new List<CeraResourceHealthDTO>();
                        ceraResourceHealthDTO = JsonConvert.DeserializeObject<List<CeraResourceHealthDTO>>(data);
                        List<CeraResourceHealth> resourceHealth = new List<CeraResourceHealth>();
                        foreach (var item in ceraResourceHealthDTO)
                        {
                            resourceHealth.Add(new CeraResourceHealth
                            {
                                Name = item.Name,
                                Location = item.Location,
                                Type = item.Type,
                                AvailabilityState = item.Properties.AvailabilityState
                            });
                        }
                        return resourceHealth;
                    }

                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public List<CeraVM> GetCloudVMList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public object GetCloudWebAppList()
        {
            return new object();
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

        public Task<List<CeraResourceHealth>> GetCloudResourceHealth(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraResourceHealth> GetCeraResourceHealthList()
        {
            throw new NotImplementedException();
        }
    }
}
