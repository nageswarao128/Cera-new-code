using CERA.AWS.CloudService;
using CERA.Azure.CloudService;
using CERA.CloudService;
using CERA.Entities.ViewModel;
using CERA.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CERA.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var serviceprovider = new ServiceCollection()
                                    .AddLogging(logging => logging.AddConsole())
                                    .AddTransient<ICeraCloudApiService, CeraAzureApiService>()
                                    .AddTransient<ICeraAzureApiService,CeraAzureApiService>()
                                    .AddTransient<ICeraAwsApiService,CeraAWSApiService>()
                                    .AddSingleton<ICeraLogger, CERALogger>()
                                    .BuildServiceProvider();

            RequestInfoViewModel requestInfo = new RequestInfoViewModel();
            var data = serviceprovider.GetService<ICeraCloudApiService>();
            var test = data.GetCloudSubscriptionList(requestInfo);
        }
    }
}
