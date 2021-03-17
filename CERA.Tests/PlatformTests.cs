using CERA.AWS.CloudService;
using CERA.Azure.CloudService;
using CERA.CloudService;
using CERA.Converter;
using CERA.DataOperation;
using CERA.DataOperation.Data;
using CERA.Entities.ViewModels;
using CERA.Logging;
using CERA.Platform;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CERA.Tests
{
    [TestClass]

    public class PlatFormTests
    {
        public ServiceProvider service()
        {
            var serviceprovider = new ServiceCollection()
                                                .AddLogging(logging => logging.AddConsole())
                                                .AddTransient<ICeraCloudApiService, CeraCloudApiService>()
                                                .AddTransient<ICeraAzureApiService, CeraAzureApiService>()
                                                .AddTransient<ICeraAwsApiService, CeraAWSApiService>()
                                                .AddTransient<ICeraDataOperation, CERADataOperation>()
                                                .AddTransient<ICeraPlatform, CeraCloudApiService>()
                                                .AddTransient<ICeraConverter, CeraConverter>()
                                                .AddSingleton<ICeraLogger, CERALogger>()
                                                .AddDbContext<CeraDbContext>(x => x.UseSqlServer(connectionString: "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_Cera; Integrated Security= true;"))
                                                .BuildServiceProvider();
            return serviceprovider;
        }
        [TestMethod]
        public void GetSubscriptionList()
        {
            var serviceprovider = service();

            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            var data = serviceprovider.GetService<ICeraPlatform>();
            data.ClientName = "Quadrant";
            var test = data.GetCloudSubscriptionList(requestInfo);
        }
        [TestMethod]
        public void GetResourcesList()
        {
            var serviceprovider = service();

            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            var data = serviceprovider.GetService<ICeraPlatform>();
            data.ClientName = "Quadrant";
            var test = data.GetCloudResourceList(requestInfo);
        }
        [TestMethod]
        public void GetResourceGroupList()
        {
            var serviceprovider = service();

            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            var data = serviceprovider.GetService<ICeraPlatform>();
            data.ClientName = "Quadrant";
            var test = data.GetCloudResourceGroups(requestInfo);
        }
        [TestMethod]
        public void GetStorageAccountList()
        {
            var serviceprovider = service();

            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            var data = serviceprovider.GetService<ICeraPlatform>();
            data.ClientName = "Quadrant";
            var test = data.GetCloudStorageAccountList(requestInfo);
        }
        [TestMethod]
        public void GetVMList()
        {
            var serviceprovider = service();

            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            var data = serviceprovider.GetService<ICeraPlatform>();
            data.ClientName = "Quadrant";
            var test = data.GetCloudVMList(requestInfo);
        }
        [TestMethod]
        public void GetDbSubscriptions()
        {
            var serviceprovider = service();
            var data = serviceprovider.GetService<ICeraPlatform>();
            var test = data.GetSubscriptionList();

        }
        [TestMethod]
        public void GetDbResources()
        {
            var serviceprovider = service();
            var data = serviceprovider.GetService<ICeraPlatform>();
            var test = data.GetResourcesList();
        }
        [TestMethod]
        public void GetDbResourceGroups()
        {
            var serviceprovider = service();
            var data = serviceprovider.GetService<ICeraPlatform>();
            var test = data.GetResourceGroupsList();
        }
        [TestMethod]
        public void GetDbStorageAccounts()
        {
            var serviceprovider = service();
            var data = serviceprovider.GetService<ICeraPlatform>();
            var test = data.GetStorageAccountList();
        }
        [TestMethod]
        public void GetDbVM()
        {
            var serviceprovider = service();
            var data = serviceprovider.GetService<ICeraPlatform>();
            var test = data.GetVMList();
        }

    }
}
