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
    public class PlatformTests
    {
        [TestMethod]
        public void GetSubscriptionTest()
        {
            var serviceprovider = new ServiceCollection()
                                    .AddLogging(logging => logging.AddConsole())
                                    .AddTransient<ICeraCloudApiService,CeraCloudApiService >()
                                    .AddTransient<ICeraAzureApiService,CeraAzureApiService>()
                                    .AddTransient<ICeraAwsApiService,CeraAWSApiService>()
                                    .AddTransient<ICeraDataOperation,CERADataOperation>()
                                    .AddTransient<ICeraPlatform,CeraCloudApiService>()
                                    .AddTransient<ICeraConverter,CeraConverter>()
                                    .AddDbContext<CeraDbContext>(x => x.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_Cera; Integrated Security= true;"))
                                    .AddSingleton<ICeraLogger, CERALogger>()
                                    .BuildServiceProvider();

            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            var data = serviceprovider.GetService<ICeraPlatform>();
            data.ClientName = "Quadrant";
            var test = data.GetCloudSubscriptionList(requestInfo);
        }
    }
}
