using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Specialized;
using CERA.AuthenticationService;
using CERA.CloudService;
using CERA.Entities;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Microsoft.Azure.Management.Fluent.Azure;
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
        private const string cloudProvider = "Azure";

        public CeraAzureApiService(ICeraLogger logger)
        {
            Logger = logger;

        }
        public void Initialize(string tenantId, string clientID, string clientSecret, string authority)
        {
            authenticator = new CeraAzureAuthenticator(Logger);
            authenticator.Initialize(tenantId, clientID, clientSecret, authority);
        }
        public void Initialize(RequestInfoViewModel requestInfo)
        {
            string clientId = requestInfo.clientId;
            string tenantId = requestInfo.tenantId;
            string clientSecret = requestInfo.clientSecret;
            string authority = "https://login.microsoftonline.com/{0}/v2.0";
            authenticator = new CeraAzureAuthenticator(Logger);
            authenticator.Initialize(tenantId, clientId, clientSecret, authority);
        }
        public IAuthenticated GetToken(RequestInfoViewModel requestInfo)
        {

            if (requestInfo.token != null)
            {
                Logger.LogInfo("Obtained ID Token");
                authenticator = new CeraAzureAuthenticator(Logger);
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential(requestInfo.token, requestInfo.tenantId);
                return authClient;
            }
            else
            {
                Initialize(requestInfo);
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                return authClient;
            }
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
                //Initialize();
                //var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                var authClient = GetToken(requestInfo);
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
                            if (resource.Tags.Count > 0)
                            {
                                ceraResources.Add(new CeraResources
                                {
                                    Name = resource.Name,
                                    RegionName = resource.RegionName,
                                    ResourceGroupName = resource.ResourceGroupName,
                                    ResourceType = resource.ResourceType,
                                    Id = resource.Id,
                                    ResourceProviderNameSpace = resource.ResourceProviderNamespace,
                                    Tags = true,
                                    CloudProvider = cloudProvider,
                                    IsActive = true,
                                    SubscriptionId = sub.SubscriptionId

                                });
                            }
                            else
                            {
                                ceraResources.Add(new CeraResources
                                {
                                    Name = resource.Name,
                                    RegionName = resource.RegionName,
                                    ResourceGroupName = resource.ResourceGroupName,
                                    ResourceType = resource.ResourceType,
                                    Id = resource.Id,
                                    ResourceProviderNameSpace = resource.ResourceProviderNamespace,
                                    Tags = false,
                                    CloudProvider = cloudProvider,
                                    IsActive = true,
                                    SubscriptionId = sub.SubscriptionId
                                });
                            }

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
                Logger.LogError("Failed get Azure Resources List");
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
        public async Task<List<CeraResourceGroups>> GetCloudResourceGroups(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize(requestInfo);
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
                                SubscriptionId = sub.SubscriptionId,
                                provisioningstate = resource.ProvisioningState,
                                CloudProvider = cloudProvider,
                                IsActive = true,
                                Resourcegroupid = resource.Id

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
                Logger.LogError("Failed get Azure Resource Groups List");
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
        public async Task<List<CeraStorageAccount>> GetCloudStorageAccountList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize(requestInfo);
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                List<CeraStorageAccount> ceraStorageAccounts = new List<CeraStorageAccount>();
                List<CeraSynapseDTO> ceraSynapses = new List<CeraSynapseDTO>();
                string url;
                url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.Synapse/workspaces?api-version=2021-06-01";
                var data = CallAzureEndPoint(requestInfo, url, subscriptions);
                JObject result = JObject.Parse(data.Result);
                var clientarray = result["value"].Value<JArray>();
                ceraSynapses = clientarray.ToObject<List<CeraSynapseDTO>>();
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
                                StorageAccountId = storage.Id,
                                Name = storage.Name,
                                RegionName = storage.RegionName,
                                ResourceGroupName = storage.ResourceGroupName,
                                CloudProvider = cloudProvider,
                                IsActive = true,
                                SubscriptionId = sub.SubscriptionId,
                                ResourceType = storage.Type.Split("/")[0],
                                //blobSize = size
                            });
                        }
                    }
                    if (ceraSynapses != null)
                    {
                        foreach (var storage in ceraSynapses)
                        {
                            ceraStorageAccounts.Add(new CeraStorageAccount
                            {
                                StorageAccountId = storage.id,
                                Name = storage.name,
                                RegionName = storage.location,
                                ResourceGroupName = storage.id.Split("/")[4],
                                CloudProvider = cloudProvider,
                                IsActive = true,
                                SubscriptionId = sub.SubscriptionId,
                                ResourceType = storage.type.Split("/")[0]
                            });
                        }
                    }
                    Logger.LogInfo("Parsing Completed stroageAccount List To CERA Resources");
                    return ceraStorageAccounts;

                }
                Logger.LogInfo("No StorageAccount List found");
                return null;
            }
            catch (Exception ex)
            {

                Logger.LogException(ex);
                return null;
            }
        }

        public async Task<List<StorageSize>> GetCloudStorageSize(RequestInfoViewModel requestInfo, List<CeraStorageAccount> storageAccounts)
        {
            List<StorageSize> storageSizes = new List<StorageSize>();
            List<StorageAccountKey> storageAccountKeys = new List<StorageAccountKey>();
            List<StorageContainersList> containersLists = new List<StorageContainersList>();
            List<StorageContainerKeysMapDTO> storageContainerKeys = new List<StorageContainerKeysMapDTO>();
            List<StorageblobSize> storageblobSizes = new List<StorageblobSize>();
            storageAccountKeys = await GetStorageAccountkeys(requestInfo, storageAccounts);
            containersLists = await GetStorageContainers(requestInfo, storageAccounts);
            foreach (var container in containersLists)
            {
                foreach (var key in storageAccountKeys)
                {
                    if (container.storageAccountName == key.storageAccountName)
                    {
                        storageContainerKeys.Add(new StorageContainerKeysMapDTO
                        {
                            storageAccountId=key.storageAccountId,
                            storageAccountName = key.storageAccountName,
                            containerName = container.container,
                            key = key.key
                        });
                    }
                }
            }

            storageblobSizes = GetBlobContainerSize(storageContainerKeys);
            foreach (var storage in storageblobSizes)
            {
                //float? size = 0;
                //foreach (var sizes in storageblobSizes)
                //{
                //    if (sizes.storageType=="Blob" && sizes.storageAccountName.Contains(storage.Name))
                //    {
                //        size += sizes.size;
                //    }
                //}
                storageSizes.Add(new StorageSize
                {
                    ResourceID = storage.storageAccountId,
                    Name=storage.name,
                    StorageType = storage.storageType,
                    Size = storage.size
                });
            }
            storageContainerKeys.Clear();
            storageblobSizes.Clear();
            containersLists.Clear();
            containersLists = await GetStorageFileShares(requestInfo, storageAccounts);
            foreach (var container in containersLists)
            {
                foreach (var key in storageAccountKeys)
                {
                    if (container.storageAccountName == key.storageAccountName)
                    {
                        storageContainerKeys.Add(new StorageContainerKeysMapDTO
                        {
                            storageAccountId=key.storageAccountId,
                            storageAccountName = key.storageAccountName,
                            containerName = container.container,
                            key = key.key
                        });
                    }
                }
            }
            storageblobSizes = GetStorageFileShareSize(storageContainerKeys);
            foreach (var storage in storageblobSizes)
            {
                //float? size = 0;
                //foreach (var sizes in storageblobSizes)
                //{
                //    if (sizes.storageType == "FileShare" && sizes.storageAccountName.Contains(storage.Name))
                //    {
                //        size += sizes.size;
                //    }
                //}
                storageSizes.Add(new StorageSize
                {
                    ResourceID = storage.storageAccountId,
                    Name=storage.name,
                    StorageType = storage.storageType,
                    Size = storage.size
                });
            }
            return storageSizes;
        }
        public List<StorageblobSize> GetBlobContainerSize(List<StorageContainerKeysMapDTO> storageContainerKeys)
        {


            List<StorageblobSize> blobSize = new List<StorageblobSize>();
            try
            {
                foreach (var item in storageContainerKeys)
                {
                    float? size = 0;
                    
                    string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1};EndpointSuffix=core.windows.net", item.storageAccountName, item.key);

                    BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, item.containerName);

                    var blobs = blobContainerClient.GetBlobs();
                    foreach (BlobItem blobItem in blobs)
                    {
                        size += (blobItem.Properties.ContentLength / 1024f) / 1024f;
                        
                    }

                    blobSize.Add(new StorageblobSize
                    {
                        storageAccountId=item.storageAccountId,
                        storageAccountName = item.storageAccountName,
                        name=item.containerName,
                        storageType ="Blob",
                        size = size
                    });
                }
                return blobSize;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        public List<StorageblobSize> GetStorageFileShareSize(List<StorageContainerKeysMapDTO> storageContainerKeys)
        {


            List<StorageblobSize> sizes = new List<StorageblobSize>();
            try
            {
                foreach (var item in storageContainerKeys)
                {
                    float? size = 0;
                    string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1};EndpointSuffix=core.windows.net", item.storageAccountName, item.key);

                    ShareClient cloudFile = new ShareClient(connectionString, item.containerName);
                    var abc = cloudFile.GetShareLeaseClient();
                    var data = cloudFile.GetStatistics();
                    size = (data.Value.ShareUsageInBytes / 1024f) / 1024f;

                    sizes.Add(new StorageblobSize
                    {
                        storageAccountId=item.storageAccountId,
                        storageAccountName = item.storageAccountName,
                        name=item.containerName,
                        storageType="FileShare",
                        size = size
                    });
                }
                return sizes;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        public async Task<List<StorageAccountKey>> GetStorageAccountkeys(RequestInfoViewModel requestInfo, List<CeraStorageAccount> storageAccounts)
        {
            List<StorageAccountKeysDTO> storageAccountKeys = new List<StorageAccountKeysDTO>();
            List<StorageAccountKey> accountKeys = new List<StorageAccountKey>();
            try
            {
                Initialize(requestInfo);
                string token = authenticator.GetAuthToken();
                var keyData = string.Empty;
                if (token != null)
                {
                    foreach (var item in storageAccounts)
                    {
                        string keyUrl = "https://management.azure.com/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Storage/storageAccounts/{2}/listKeys?api-version=2021-04-01";
                        string keyUri = string.Format(keyUrl, item.SubscriptionId, item.ResourceGroupName, item.Name);

                        HttpClient client = new HttpClient();
                        HttpRequestMessage keyRequestMessage = new HttpRequestMessage(HttpMethod.Post, keyUri);
                        keyRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        HttpResponseMessage keyResponseMessage = await client.SendAsync(keyRequestMessage);

                        keyData = await keyResponseMessage.Content.ReadAsStringAsync();
                        JObject result1 = JObject.Parse(keyData);
                        var clientarray1 = result1["keys"].Value<JArray>();
                        storageAccountKeys = clientarray1.ToObject<List<StorageAccountKeysDTO>>();

                        foreach (var keys in storageAccountKeys)
                        {
                            if (keys.keyName == "key1")
                            {
                                accountKeys.Add(new StorageAccountKey
                                {
                                    storageAccountId=item.StorageAccountId,
                                    storageAccountName = item.Name,
                                    key = keys.value
                                });
                            }
                        }
                    }
                    return accountKeys;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        public async Task<List<StorageContainersList>> GetStorageContainers(RequestInfoViewModel requestInfo, List<CeraStorageAccount> storageAccounts)
        {
            try
            {
                List<StorageContainersDTO> storageContainers = new List<StorageContainersDTO>();
                List<StorageContainersList> containersLists = new List<StorageContainersList>();
                Initialize(requestInfo);
                string token = authenticator.GetAuthToken();
                var data = string.Empty;
                if (token != null)
                {
                    string url = "https://management.azure.com/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Storage/storageAccounts/{2}/blobServices/default/containers?api-version=2021-04-01";
                    foreach (var item in storageAccounts)
                    {
                        string uri = string.Format(url, item.SubscriptionId, item.ResourceGroupName, item.Name);
                        HttpClient client = new HttpClient();
                        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
                        data = await responseMessage.Content.ReadAsStringAsync();
                        JObject result = JObject.Parse(data);
                        var clientarray = result["value"].Value<JArray>();
                        storageContainers = clientarray.ToObject<List<StorageContainersDTO>>();
                        foreach (var containers in storageContainers)
                        {
                            containersLists.Add(new StorageContainersList
                            {
                                storageAccountName = item.Name,
                                container = containers.name
                            });
                        }
                    }
                    return containersLists;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        public async Task<List<StorageContainersList>> GetStorageFileShares(RequestInfoViewModel requestInfo, List<CeraStorageAccount> storageAccounts)
        {
            try
            {
                List<StorageContainersDTO> storageContainers = new List<StorageContainersDTO>();
                List<StorageContainersList> containersLists = new List<StorageContainersList>();
                Initialize(requestInfo);
                string token = authenticator.GetAuthToken();
                var data = string.Empty;
                if (token != null)
                {
                    string url = "https://management.azure.com/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Storage/storageAccounts/{2}/fileServices/default/shares?api-version=2021-04-01";
                    foreach (var item in storageAccounts)
                    {
                        string uri = string.Format(url, item.SubscriptionId, item.ResourceGroupName, item.Name);
                        HttpClient client = new HttpClient();
                        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
                        data = await responseMessage.Content.ReadAsStringAsync();
                        if (!data.Contains("FeatureNotSupportedForAccount"))
                        {
                            JObject result = JObject.Parse(data);
                            var clientarray = result["value"].Value<JArray>();
                            storageContainers = clientarray.ToObject<List<StorageContainersDTO>>();
                            foreach (var containers in storageContainers)
                            {
                                containersLists.Add(new StorageContainersList
                                {
                                    storageAccountName = item.Name,
                                    container = containers.name
                                });
                            }
                        }

                    }
                    return containersLists;
                }
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
                Initialize(requestInfo);
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                List<CeraSqlServer> cerasqlServer = new List<CeraSqlServer>();
                foreach (var sub in subscriptions)
                {
                    var azureSqlserver = authClient.WithSubscription(sub.SubscriptionId).SqlServers.ListAsync().Result;
                    //var tenant = authClient.Tenants.ListAsync().Result;

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
                                ResourceGroupName = sqlServer.ResourceGroupName,
                                SqlServerId = sqlServer.Id,
                                CloudProvider = cloudProvider,
                                IsActive = true

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
                Logger.LogError("Failed get Azure SqlServers List");
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
                //Initialize();
                //var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                var authClient = GetToken(requestInfo);
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
                            CloudProvider = cloudProvider,
                            IsActive = true

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
                Logger.LogError("Failed get Azure Subscriptions List");
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
                Initialize(requestInfo);
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
                            TenantId = Tenants.TenantId,
                            CloudProvider = cloudProvider,
                            IsActive = true
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
                Logger.LogError("Failed get Azure Tenants List");
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
        public async Task<List<CeraVM>> GetCloudVMList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize(requestInfo);
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
                                Id = virtualMachine.Id,
                                VMName = virtualMachine.Name,
                                SubscriptionId = sub.SubscriptionId,
                                RegionName = virtualMachine.RegionName,
                                ResourceGroupName = virtualMachine.ResourceGroupName,
                                CloudProvider = cloudProvider,
                                IsActive = true,
                                ResourceType = virtualMachine.Type.Split("/")[0]
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
                Logger.LogError("Failed get Azure VM's List");
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
        public async Task<List<CeraWebApps>> GetCloudWebAppList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize(requestInfo);
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
                                WebAppId = WebApps.Id,
                                Name = WebApps.Name,
                                RegionName = WebApps.RegionName,
                                SubscriptionId = sub.SubscriptionId,
                                ResourceGroupName = WebApps.ResourceGroupName,
                                CloudProvider = cloudProvider,
                                IsActive = true,
                                ResourceType = WebApps.Type.Split("/")[0]
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
                Logger.LogError("Failed get Azure WebApps List");
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
                Initialize(requestInfo);
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
                                ResourceGroupName = appService.ResourceGroupName,
                                AppServicePlanId = appService.Id,
                                CloudProvider = cloudProvider,
                                IsActive = true

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
                Logger.LogError("Failed get Azure App ServicePlans List");
                Logger.LogException(ex);
                return null;
            }
        }
        public List<CeraPolicy> GetCloudPolicies(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize(requestInfo);
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential();
                Logger.LogInfo("Auth Client Initialized");
                foreach (var sub in subscriptions)
                {
                    var Policies = authClient.WithSubscription(sub.SubscriptionId).PolicyAssignments.ListAsync().Result;
                    Logger.LogInfo("Got Policies List from Azure Cloud Provider");
                    if (Policies != null)
                    {
                        Logger.LogInfo("Parsing Policies List To CERA Resources");
                        List<CeraPolicy> policy = new List<CeraPolicy>();
                        foreach (var item in Policies)
                        {
                            policy.Add(new CeraPolicy
                            {
                                PolicyId = item.Id,
                                PrincipleName = item.DisplayName,
                                ResourceGroupName = item.Scope.Remove(0, 67),
                                Scope = item.Scope,
                                Key = item.Key,
                                CloudProvider = cloudProvider,
                                IsActive = true
                            });
                        }
                        Logger.LogInfo("Parsing Completed Policies List To CERA Resources");

                        return policy;
                    }
                }
                Logger.LogInfo("No Policies List found");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed get Azure Policies List");
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
                Initialize(requestInfo);
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
                                ResourceGroupName = disk.ResourceGroupName,
                                DiskId = disk.Id,
                                CloudProvider = cloudProvider,
                                IsActive = true

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
                Logger.LogError("Failed get Azure Disks List");
                Logger.LogException(ex);
                return null;
            }
        }
        public List<CeraResourceHealth> GetCloudResourceHealth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            const string url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.ResourceHealth/availabilityStatuses?api-version=2020-05-01-preview";
            var data = CallAzureEndPoint(requestInfo, url, subscriptions);
            if (data == null)
            {
                return null;
            }
            List<CeraResourceHealthDTO> ceraResourceHealthDTO = new List<CeraResourceHealthDTO>();
            JObject result = JObject.Parse(data.Result);
            var clientarray = result["value"].Value<JArray>();
            ceraResourceHealthDTO = clientarray.ToObject<List<CeraResourceHealthDTO>>();
            List<CeraResourceHealth> resourceHealth = new List<CeraResourceHealth>();
            foreach (var item in ceraResourceHealthDTO)
            {
                resourceHealth.Add(new CeraResourceHealth
                {
                    ResourceId = item.id.Replace("/providers/Microsoft.ResourceHealth/availabilityStatuses/current", ""),
                    Name = item.name,
                    Location = item.location,
                    Type = item.type,
                    AvailabilityState = item.properties.availabilityState,
                    CloudProvider = cloudProvider,
                    IsActive = true,
                    SubscriptionId = item.id.Substring(15, 36)

                });
            }
            return resourceHealth;
        }

        public List<CeraRateCard> GetCloudRateCardList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            const string url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.Commerce/RateCard?api-version=2016-08-31-preview&$filter=OfferDurableId eq 'MS-AZR-0003P' and Currency eq 'INR' and Locale eq 'en-IN' and RegionInfo eq 'IN'";
            var data = CallAzureEndPoint(requestInfo, url, subscriptions);
            if (data == null)
            {
                return null;
            }

            RateCardDTO rateCardDTO = new RateCardDTO();
            rateCardDTO = JsonConvert.DeserializeObject<RateCardDTO>(data.Result);
            List<CeraRateCard> ceraRateCard = new List<CeraRateCard>();
            foreach (var item in rateCardDTO.Meters)
            {
                ceraRateCard.Add(new CeraRateCard
                {
                    MeterId = item.MeterId,
                    MeterName = item.MeterName,
                    MeterCategory = item.MeterCategory,
                    MeterRegion = item.MeterRegion,
                    MeterStatus = item.MeterStatus,
                    MeterSubCategory = item.MeterSubCategory,
                    EffectiveDate = item.EffectiveDate,
                    IncludedQuantity = item.IncludedQuantity,
                    Unit = item.Unit,
                    Currency = "INR"
                });
            }
            return ceraRateCard;
        }
        public async Task<List<CeraUsage>> GetCloudUsageDetails(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            string url;
            url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.Consumption/usageDetails?api-version=2018-03-31&$expand=properties/additionalProperties";
            List<CeraUsage> usageDetails = new List<CeraUsage>();
            do
            {
                var data = CallAzureEndPoint(requestInfo, url, subscriptions);
                if (data == null)
                {
                    return null;
                }

                BillingDTO billingDTO = new BillingDTO();
                billingDTO = JsonConvert.DeserializeObject<BillingDTO>(data.Result);
                url = billingDTO.nextLink;
                for (int i = 0; i < billingDTO.value.Length; i++)
                {
                    string location = MapLocation(billingDTO.value[i].properties.instanceLocation);
                    usageDetails.Add(new CeraUsage
                    {
                        id = billingDTO.value[i].id,
                        name = billingDTO.value[i].name,
                        type = billingDTO.value[i].type,
                        billingPeriodId = billingDTO.value[i].properties.billingPeriodId,
                        usageStart = billingDTO.value[i].properties.usageStart,
                        usageEnd = billingDTO.value[i].properties.usageEnd,
                        instanceId = billingDTO.value[i].properties.instanceId,
                        instanceName = billingDTO.value[i].properties.instanceName,
                        instanceLocation = location,
                        meterId = billingDTO.value[i].properties.meterId,
                        usageQuantity = billingDTO.value[i].properties.usageQuantity,
                        pretaxCost = billingDTO.value[i].properties.pretaxCost,
                        currency = billingDTO.value[i].properties.currency,
                        isEstimated = billingDTO.value[i].properties.isEstimated,
                        subscriptionGuid = billingDTO.value[i].properties.subscriptionGuid,
                        subscriptionName = billingDTO.value[i].properties.subscriptionName,
                        product = billingDTO.value[i].properties.product,
                        consumedService = billingDTO.value[i].properties.consumedService,
                        partNumber = billingDTO.value[i].properties.partNumber,
                        resourceGuid = billingDTO.value[i].properties.resourceGuid,
                        offerId = billingDTO.value[i].properties.offerId,
                        chargesBilledSeparately = billingDTO.value[i].properties.chargesBilledSeparately,
                        meterDetails = billingDTO.value[i].properties.meterDetails,
                        additionalProperties = billingDTO.value[i].properties.additionalProperties,
                        CloudProvider = cloudProvider,
                        IsActive = true,
                        SubscriptionId = billingDTO.value[i].id.Substring(15, 36)


                    });
                }
            }
            while (url != null);
            return usageDetails;
        }
        public List<UsageByMonth> GetCloudUsageByMonth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            string url;
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-30);
            string usageStart = startDate.ToShortDateString();
            string usageEnd = endDate.ToShortDateString();
            url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.Consumption/usageDetails?$filter=properties%2FusageStart%20ge%20'{1}'%20and%20properties%2FusageEnd%20le%20'{2}'&$top=1000&api-version=2018-03-31";
            List<UsageByMonth> usageDetails = new List<UsageByMonth>();
            do
            {
                var data = GetUsageData(requestInfo, url, subscriptions, usageStart, usageEnd);
                if (data == null)
                {
                    return null;
                }

                BillingDTO billingDTO = new BillingDTO();
                billingDTO = JsonConvert.DeserializeObject<BillingDTO>(data.Result);
                url = billingDTO.nextLink;
                for (int i = 0; i < billingDTO.value.Length; i++)
                {
                    string location = MapLocation(billingDTO.value[i].properties.instanceLocation);
                    string month = billingDTO.value[i].properties.usageStart.ToString("MMMM");
                    usageDetails.Add(new UsageByMonth
                    {
                        id = billingDTO.value[i].id,
                        name = billingDTO.value[i].name,
                        type = billingDTO.value[i].type,
                        billingPeriodId = billingDTO.value[i].properties.billingPeriodId,
                        usageMonth = month,
                        usageStart = billingDTO.value[i].properties.usageStart,
                        usageEnd = billingDTO.value[i].properties.usageEnd,
                        instanceId = billingDTO.value[i].properties.instanceId,
                        instanceName = billingDTO.value[i].properties.instanceName,
                        instanceResourceGroup = billingDTO.value[i].properties.instanceId.Split("/")[4],
                        instanceLocation = location,
                        actualLocation = billingDTO.value[i].properties.instanceLocation,
                        meterId = billingDTO.value[i].properties.meterId,
                        usageQuantity = billingDTO.value[i].properties.usageQuantity,
                        pretaxCost = billingDTO.value[i].properties.pretaxCost,
                        currency = billingDTO.value[i].properties.currency,
                        isEstimated = billingDTO.value[i].properties.isEstimated,
                        subscriptionGuid = billingDTO.value[i].properties.subscriptionGuid,
                        subscriptionName = billingDTO.value[i].properties.subscriptionName,
                        product = billingDTO.value[i].properties.product,
                        consumedService = billingDTO.value[i].properties.consumedService,
                        partNumber = billingDTO.value[i].properties.partNumber,
                        resourceGuid = billingDTO.value[i].properties.resourceGuid,
                        offerId = billingDTO.value[i].properties.offerId,
                        chargesBilledSeparately = billingDTO.value[i].properties.chargesBilledSeparately,
                        meterDetails = billingDTO.value[i].properties.meterDetails,
                        additionalProperties = billingDTO.value[i].properties.additionalProperties,
                        CloudProvider = cloudProvider,
                        IsActive = true,
                        SubscriptionId = billingDTO.value[i].id.Substring(15, 36)
                    });
                }
            }
            while (url != null);
            return usageDetails;
        }
        public List<UsageHistory> GetCloudUsageHistory(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            string url;
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddMonths(-6);
            string usageStart = startDate.ToShortDateString();
            string usageEnd = endDate.ToShortDateString();
            url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.Consumption/usageDetails?$filter=properties%2FusageStart%20ge%20'{1}'%20and%20properties%2FusageEnd%20le%20'{2}'&$top=1000&api-version=2018-03-31";

            List<UsageHistory> usageDetails = new List<UsageHistory>();
            do
            {
                var data = GetUsageData(requestInfo, url, subscriptions, usageStart, usageEnd);
                if (data == null)
                {
                    return null;
                }

                BillingDTO billingDTO = new BillingDTO();
                billingDTO = JsonConvert.DeserializeObject<BillingDTO>(data.Result);
                url = billingDTO.nextLink;
                for (int i = 0; i < billingDTO.value.Length; i++)
                {
                    string location = MapLocation(billingDTO.value[i].properties.instanceLocation);
                    string month = billingDTO.value[i].properties.usageStart.ToString("MMMM");
                    usageDetails.Add(new UsageHistory
                    {
                        id = billingDTO.value[i].id,
                        name = billingDTO.value[i].name,
                        type = billingDTO.value[i].type,
                        billingPeriodId = billingDTO.value[i].properties.billingPeriodId,
                        usageMonth = month,
                        usageStart = billingDTO.value[i].properties.usageStart,
                        usageEnd = billingDTO.value[i].properties.usageEnd,
                        instanceId = billingDTO.value[i].properties.instanceId,
                        instanceName = billingDTO.value[i].properties.instanceName,
                        instanceResourceGroup = billingDTO.value[i].properties.instanceId.Split("/")[4],
                        instanceLocation = location,
                        actualLocation = billingDTO.value[i].properties.instanceLocation,
                        meterId = billingDTO.value[i].properties.meterId,
                        usageQuantity = billingDTO.value[i].properties.usageQuantity,
                        pretaxCost = billingDTO.value[i].properties.pretaxCost,
                        currency = billingDTO.value[i].properties.currency,
                        isEstimated = billingDTO.value[i].properties.isEstimated,
                        subscriptionGuid = billingDTO.value[i].properties.subscriptionGuid,
                        subscriptionName = billingDTO.value[i].properties.subscriptionName,
                        product = billingDTO.value[i].properties.product,
                        consumedService = billingDTO.value[i].properties.consumedService,
                        partNumber = billingDTO.value[i].properties.partNumber,
                        resourceGuid = billingDTO.value[i].properties.resourceGuid,
                        offerId = billingDTO.value[i].properties.offerId,
                        chargesBilledSeparately = billingDTO.value[i].properties.chargesBilledSeparately,
                        meterDetails = billingDTO.value[i].properties.meterDetails,
                        additionalProperties = billingDTO.value[i].properties.additionalProperties,
                        CloudProvider = cloudProvider,
                        IsActive = true
                    });
                }
            }
            while (url != null);
            return usageDetails;
        }

        public List<AzureLocations> GetCloudLocations(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            const string url = "https://management.azure.com/subscriptions/{0}/locations?api-version=2020-01-01";
            var data = CallAzureEndPoint(requestInfo, url, subscriptions);
            if (data == null)
            {
                return null;
            }

            List<AzureLocationsDTO> locationsDTO = new List<AzureLocationsDTO>();
            JObject result = JObject.Parse(data.Result);
            var clientarray = result["value"].Value<JArray>();
            locationsDTO = clientarray.ToObject<List<AzureLocationsDTO>>();

            List<AzureLocations> locations = new List<AzureLocations>();
            foreach (var item in locationsDTO)
            {
                locations.Add(new AzureLocations
                {
                    LocationId = item.id,
                    displayName = item.displayName,
                    geographyGroup = item.metadata.geographyGroup,
                    latitude = item.metadata.latitude,
                    longitude = item.metadata.longitude,
                    name = item.name,
                    regionalDisplayName = item.regionalDisplayName,
                    physicalLocation = item.metadata.physicalLocation,
                    regionType = item.metadata.regionType,
                    CloudProvider = cloudProvider,
                    IsActive = true
                });
            }

            return locations;
        }
        public List<CeraCompliances> GetCloudCompliances(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            const string url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.Security/compliances?api-version=2017-08-01-preview";
            var data = CallAzureEndPoint(requestInfo, url, subscriptions);
            if (data == null)
            {
                return null;
            }

            CeraCompliancesDTO ceraCompliancesDTO = new CeraCompliancesDTO();
            ceraCompliancesDTO = JsonConvert.DeserializeObject<CeraCompliancesDTO>(data.Result);
            List<CeraCompliances> ceraCompliances = new List<CeraCompliances>();
            foreach (var item in ceraCompliancesDTO.value)
            {
                ceraCompliances.Add(new CeraCompliances
                {
                    Name = item.name,
                    Type = item.type,
                    AssessmentType = item.properties.assessmentResult[0].type,
                    CompliancesId = item.id,
                    CloudProvider = cloudProvider,
                    IsActive = true
                });
            }
            return ceraCompliances;
        }
        public List<AdvisorRecommendations> GetCloudAdvisorRecommendations(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            const string url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.Security/compliances?api-version=2017-08-01-preview";
            var data = CallAzureEndPoint(requestInfo, url, subscriptions);
            if (data == null)
            {
                return null;
            }

            List<AdvisorRecommendationsDTO> advisorRecommendationsDTO = new List<AdvisorRecommendationsDTO>();
            JObject result = JObject.Parse(data.Result);
            var clientarray = result["value"].Value<JArray>();
            advisorRecommendationsDTO = clientarray.ToObject<List<AdvisorRecommendationsDTO>>();
            //  advisorRecommendationsDTO = JsonConvert.DeserializeObject<List<AdvisorRecommendationsDTO>>(data.Result);
            List<AdvisorRecommendations> recommendations = new List<AdvisorRecommendations>();
            foreach (var item in advisorRecommendationsDTO)
            {
                recommendations.Add(new AdvisorRecommendations
                {
                    recommendationId = item.id,
                    resourceId = item.properties.resourceMetadata.resourceId,
                    location = item.properties.extendedProperties.location,
                    category = item.properties.category,
                    impact = item.properties.impact,
                    CloudProvider = cloudProvider,
                    IsActive = true
                });
            }
            return recommendations;
        }
        public async Task<string> CallAzureEndPoint(RequestInfoViewModel requestInfo, string url, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize(requestInfo);
                string token = authenticator.GetAuthToken();
                var data = string.Empty;
                if (token != null)
                {
                    foreach (var sub in subscriptions)
                    {
                        string uri = string.Format(url, sub.SubscriptionId);
                        HttpClient client = new HttpClient();
                        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
                        data = await responseMessage.Content.ReadAsStringAsync();
                    }
                    return data;

                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        public async Task<string> GetUsageData(RequestInfoViewModel requestInfo, string url, List<CeraSubscription> subscriptions, string usageStart, string usageEnd)
        {
            try
            {
                Initialize(requestInfo);
                string token = authenticator.GetAuthToken();
                var data = string.Empty;
                if (token != null)
                {
                    foreach (var sub in subscriptions)
                    {
                        string uri = string.Format(url, sub.SubscriptionId, usageStart, usageEnd);
                        HttpClient client = new HttpClient();
                        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
                        data = await responseMessage.Content.ReadAsStringAsync();
                    }
                    return data;

                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public string MapLocation(string location)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("US East", "eastus");
            keyValues.Add("US West", "westus");
            keyValues.Add("IN Central", "centralindia");
            keyValues.Add("Unknown", "global");
            keyValues.Add("IN South", "southindia");
            keyValues.Add("US Central", "centralus");
            keyValues.Add("Intercontinental", "southindia");
            keyValues.Add("EU North", "northeurope");
            keyValues.Add("Asia", "asia");
            keyValues.Add("US South Central", "southcentralus");
            keyValues.Add("US East 2", "eastus2");
            keyValues.Add("IN West","westindia");
            keyValues.Add("US West 2", "westus2");
            keyValues.Add("US West 3", "westus3");
            if (keyValues.ContainsKey(location))
            {
                return keyValues[location];
            }
            return null;
        }

        public Task<List<CeraVM>> GetCloudVMList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public Task<List<CeraWebApps>> GetCloudWebAppList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }
        public List<CeraSubscription> GetSubscriptionList()
        {
            return new List<CeraSubscription>();
        }
        public List<CeraResources> GetResourcesList()
        {
            return new List<CeraResources>();
        }
        public List<ResourcesModel> GetVMList()
        {
            return new List<ResourcesModel>();
        }
        public List<ResourceGroupsVM> GetResourceGroupsList()
        {
            return new List<ResourceGroupsVM>();
        }
        public Task<List<CeraResourceGroups>> GetCloudResourceGroups(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public Task<List<CeraStorageAccount>> GetCloudStorageAccountList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<StorageAccountsVM> GetStorageAccountList()
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

        public List<ResourcesModel> GetWebAppsList()
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

        public List<CeraResourceHealth> GetCloudResourceHealth(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<ResourceHealthViewDTO> GetCeraResourceHealthList()
        {
            throw new NotImplementedException();
        }

        public List<CeraCompliances> GetCloudCompliances(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraCompliances> GetCompliancesList()
        {
            throw new NotImplementedException();
        }
        public List<CeraRateCard> GetCloudRateCardList(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraRateCard> GetRateCardList()
        {
            throw new NotImplementedException();
        }

        public Task<List<CeraUsage>> GetCloudUsageDetails(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<CeraUsage> GetUsageDetails()
        {
            throw new NotImplementedException();
        }

        public List<CeraResourceTypeUsage> GetResourceTypeUsageDetails()
        {
            throw new NotImplementedException();
        }

        public List<ResourceTypeCount> GetResourceTypeCounts()
        {
            throw new NotImplementedException();
        }

        public List<CeraPolicy> GetCloudPolicies(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }



        public List<CeraPolicy> GetPolicies()
        {
            throw new NotImplementedException();
        }

        public List<ResourceTagsCount> GetResourceTagsCount()
        {
            throw new NotImplementedException();
        }

        public List<AzureLocations> GetCloudLocations(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }



        public List<AzureLocations> GetLocations()
        {
            throw new NotImplementedException();
        }

        public List<AdvisorRecommendations> GetAdvisorRecommendations()
        {
            throw new NotImplementedException();
        }

        public List<AdvisorRecommendations> GetCloudAdvisorRecommendations(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<UsageByMonth> GetCloudUsageByMonth(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<UsageByMonth> GetUsageByMonth()
        {
            throw new NotImplementedException();
        }
        public List<UsageHistory> GetCloudUsageHistory(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }
        public List<UsageHistory> GetUsageHistory()
        {
            throw new NotImplementedException();
        }
    }
}
