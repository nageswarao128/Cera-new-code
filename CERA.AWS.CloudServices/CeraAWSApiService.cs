using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;
using Amazon.EC2;
using Amazon.ElasticBeanstalk;
using Amazon.RDS;
using Amazon.ResourceGroups;
using Amazon.ResourceGroups.Model;
using Amazon.S3;
using CERA.AuthenticationService;
using CERA.Entities.Models;
using CERA.Entities.ViewModels;
using CERA.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CERA.AWS.CloudService
{
    public class CeraAWSApiService : ICeraAwsApiService
    {
        public CeraAWSApiService()
        {

        }
        ICeraAuthenticator authenticator;

        public ICeraLogger Logger { get; set; }
        public List<CeraPlatformConfigViewModel> _platformConfigs { get; set; }
        string accessId = AWSAuth.Default.AccessId;
        string secretKey = AWSAuth.Default.SecretKey;
        Amazon.RegionEndpoint regionEndpoint = Amazon.RegionEndpoint.APSouth1;
        public object GetCloudMonthlyBillingList()
        {
            return new object();
        }

        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo)
        {
            return new List<CeraResources>();
        }

        public object GetCloudSqlDbList()
        {
            return new object();
        }

        public object GetCloudSqlServerList()
        {
            return new object();
        }

        public List<CeraSubscription> GetCloudSubscriptionList(RequestInfoViewModel requestInfo)
        {
            return new List<CeraSubscription>();
        }

        public object GetCloudServicePlanList()
        {
            return new object();
        }

        public List<CeraTenants> GetCloudTenantList(RequestInfoViewModel requestInfo)
        {
            return new List<CeraTenants>();
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

        public void Initialize(string tenantId, string clientID, string clientSecret,string authority)
        {
            throw new NotImplementedException();
        }

        public List<CeraResources> GetCloudResourceList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CeraVM>> GetCloudVMList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            AmazonEC2Client client = new AmazonEC2Client(accessId, secretKey, regionEndpoint);
            var vm = await client.DescribeInstancesAsync();
            throw new NotImplementedException();
        }

        public async Task<List<CeraResourceGroups>> GetCloudResourceGroups(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            AmazonResourceGroupsClient client = new AmazonResourceGroupsClient(accessId, secretKey, regionEndpoint);
            ListGroupsRequest model = new ListGroupsRequest();
            var res = await client.ListGroupsAsync(model);
            throw new NotImplementedException();
        }

        public Task<List<CeraResourceGroups>> GetCloudResourceGroups(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CeraStorageAccount>> GetCloudStorageAccountList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            AmazonRDSClient client = new AmazonRDSClient(accessId, secretKey, regionEndpoint);
            var storageAccounts = await client.DescribeDBInstancesAsync();
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

        public List<CeraSqlServer> GetCloudSqlServersList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
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

        public async Task<List<CeraWebApps>> GetCloudWebAppList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            AmazonElasticBeanstalkClient client = new AmazonElasticBeanstalkClient(accessId,secretKey,regionEndpoint);
            var webApp = await client.DescribeApplicationsAsync();
            throw new NotImplementedException();
        }

        public List<ResourcesModel> GetWebAppsList()
        {
            throw new NotImplementedException();
        }

        public List<CeraAppServicePlans> GetCloudAppServicePlansList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
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

        public List<CeraDisks> GetCloudDisksList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
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

        public List<CeraResourceHealth> GetCloudResourceHealth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
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

        public List<CeraCompliances> GetCloudCompliances(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
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

        public List<CeraRateCard> GetCloudRateCardList(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
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

        public async Task<List<CeraUsage>> GetCloudUsageDetails(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            AmazonCostExplorerClient data = new AmazonCostExplorerClient(accessId,secretKey,regionEndpoint);
            GetCostAndUsageRequest request = new GetCostAndUsageRequest();

            request.Granularity = Granularity.MONTHLY;
            List<Amazon.CostExplorer.Model.GroupDefinition> groupDefinitions = new List<GroupDefinition>();
            Amazon.CostExplorer.Model.GroupDefinition group = new GroupDefinition();
            group.Type = "DIMENSION";
            group.Key = "SERVICE";

            groupDefinitions.Add(group);

            request.GroupBy = groupDefinitions;

            DateInterval start = new DateInterval();
            start.Start = "2021-08-01";
            start.End = "2021-08-31";
            request.TimePeriod = start;

            List<string> req = new List<string>();
            req.Add("BLENDED_COST");
            //req.Add("UNBLENDED_COST");
            //req.Add("USAGE_QUANTITY");
            //req.Add("AMORTIZED_COST");
            //req.Add("NET_AMORTIZED_COST");
            //req.Add("NET_UNBLENDED_COST");
            //req.Add("NORMALIZED_USAGE_AMOUNT");

            request.Metrics = req;

            var usage = await data.GetCostAndUsageAsync(request);
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

        public List<CeraPolicy> GetCloudPolicies(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
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

        public List<AzureLocations> GetCloudLocations(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
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

        public List<AdvisorRecommendations> GetCloudAdvisorRecommendations(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<UsageByMonth> GetCloudUsageByMonth(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<UsageByMonth> GetCloudUsageByMonth(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<UsageHistory> GetCloudUsageHistory(RequestInfoViewModel requestInfo)
        {
            throw new NotImplementedException();
        }

        public List<UsageHistory> GetCloudUsageHistory(RequestInfoViewModel requestInfo, List<CeraSubscription> subscriptions)
        {
            throw new NotImplementedException();
        }

        public List<UsageByMonth> GetUsageByMonth()
        {
            throw new NotImplementedException();
        }

        public List<UsageHistory> GetUsageHistory()
        {
            throw new NotImplementedException();
        }

        public List<StorageSize> GetCloudStorageSize(List<CeraStorageAccount> storageAccounts)
        {
            throw new NotImplementedException();
        }

        public Task<List<StorageSize>> GetCloudStorageSize(RequestInfoViewModel requestInfo, List<CeraStorageAccount> storageAccounts)
        {
            throw new NotImplementedException();
        }

        
    }
}
