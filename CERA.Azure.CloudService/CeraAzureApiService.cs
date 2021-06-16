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


        public CeraAzureApiService(ICeraLogger logger)
        {
            Logger = logger;

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
        public IAuthenticated GetToken(string token,string tenantId)
        {
            
            if (token != null)
            {
                Logger.LogInfo("Obtained ID Token");
                authenticator = new CeraAzureAuthenticator(Logger);
                var authClient = authenticator.GetAuthenticatedClientUsingAzureCredential(token, tenantId);
                return authClient;
            }
            else
            {
                Initialize();
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
                var authClient = GetToken(requestInfo.token, requestInfo.tenantId);
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
                                    Tags = true
                                }) ;
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
                                    Tags = false
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
                    var tenant = authClient.Tenants.ListAsync().Result;

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
                var authClient = GetToken(requestInfo.token, requestInfo.tenantId);
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
        public List<CeraPolicy> GetCloudPolicies(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize();
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
                                 ResourceGroupName = item.Scope.Remove(0,67),
                                 Scope = item.Scope,
                                 Key = item.Key 
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
        public List<CeraResourceHealth> GetCloudResourceHealth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            const string url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.ResourceHealth/availabilityStatuses?api-version=2020-05-01-preview";
            var data = CallAzureEndPoint(url, subscriptions);
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
                    ResourceId = item.id.Replace("/providers/Microsoft.ResourceHealth/availabilityStatuses/current",""),
                    Name = item.name,
                    Location = item.location,
                    Type = item.type,
                    AvailabilityState = item.properties.availabilityState
                });
            }
            return resourceHealth;
        }

        public List<CeraRateCard> GetCloudRateCardList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            const string url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.Commerce/RateCard?api-version=2016-08-31-preview&$filter=OfferDurableId eq 'MS-AZR-0003P' and Currency eq 'INR' and Locale eq 'en-IN' and RegionInfo eq 'IN'";
            var data = CallAzureEndPoint(url, subscriptions);
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
        public List<CeraUsage> GetCloudUsageDetails(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            const string url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.Consumption/usageDetails?api-version=2018-03-31&$expand=properties/additionalProperties";
            var data = CallAzureEndPoint(url, subscriptions);
            if (data == null)
            {
                return null;
            }

            List<UsageDTO> usageDetailsDTO = new List<UsageDTO>();
            JObject result = JObject.Parse(data.Result);
            var clientarray = result["value"].Value<JArray>();
            usageDetailsDTO = clientarray.ToObject<List<UsageDTO>>();

            List<CeraUsage> usageDetails = new List<CeraUsage>();
            foreach (var item in usageDetailsDTO)
            {
                string location = MapLocation(item.usageProperties.instanceLocation);
                usageDetails.Add(new CeraUsage
                {
                    id = item.id,
                    name = item.name,
                    type = item.type,
                    billingPeriodId = item.usageProperties.billingPeriodId,
                    usageStart = item.usageProperties.usageStart,
                    usageEnd = item.usageProperties.usageEnd,
                    instanceId = item.usageProperties.instanceId,
                    instanceName = item.usageProperties.instanceName,
                    instanceLocation = location,
                    meterId = item.usageProperties.meterId,
                    usageQuantity = item.usageProperties.usageQuantity,
                    pretaxCost = item.usageProperties.pretaxCost,
                    currency = item.usageProperties.currency,
                    isEstimated = item.usageProperties.isEstimated,
                    subscriptionGuid = item.usageProperties.subscriptionGuid,
                    subscriptionName = item.usageProperties.subscriptionName,
                    product = item.usageProperties.product,
                    consumedService = item.usageProperties.consumedService,
                    partNumber = item.usageProperties.partNumber,
                    resourceGuid = item.usageProperties.resourceGuid,
                    offerId = item.usageProperties.offerId,
                    chargesBilledSeparately = item.usageProperties.chargesBilledSeparately,
                    meterDetails = item.usageProperties.meterDetails,
                    additionalProperties = item.usageProperties.additionalProperties
                });
            }

            return usageDetails;
        }
        public List<AzureLocations> GetCloudLocations(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            const string url = "https://management.azure.com/subscriptions/{0}/locations?api-version=2020-01-01";
            var data = CallAzureEndPoint(url, subscriptions);
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
                    regionType = item.metadata.regionType 
                });
            }

            return locations;
        }
        public List<CeraCompliances> GetCloudCompliances(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            const string url = "https://management.azure.com/subscriptions/{0}/providers/Microsoft.Security/compliances?api-version=2017-08-01-preview";
            var data = CallAzureEndPoint(url, subscriptions);
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
                    AssessmentType = item.properties.assessmentResult[0].type
                });
            }
            return ceraCompliances;
        }
        public async Task<string> CallAzureEndPoint(string url, List<CeraSubscription> subscriptions)
        {
            try
            {
                Initialize();
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
            if (keyValues.ContainsKey(location))
            {
                return keyValues[location];
            }
            return null;
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

        public List<CeraUsage> GetCloudUsageDetails(RequestInfoViewModel requestInfo)
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
    }
}
