using CERA.CloudService;
using CERA.Converter;
using CERA.Entities;
using System;
using System.Collections.Generic;

namespace CERA.TestClient
{
    class Program
    {
        static ICeraConverter _converter;
        static List<CeraPlatformConfig> _platformConfigs = new List<CeraPlatformConfig>() {
            new CeraPlatformConfig(){PlatformName =   "Azure", APIClassName = "CERA.Azure.CloudService.CeraAzureApiService", DllPath = @"D:\ClientWorks\Quadrant\QHub Team\CERA\CERA.Azure.CloudService\bin\Debug\netstandard2.1\CERA.Azure.CloudService.dll"},
            new CeraPlatformConfig(){PlatformName =   "Aws", APIClassName = "CERA.AWS.CloudService.CeraAWSApiService", DllPath = @"D:\ClientWorks\Quadrant\QHub Team\CERA\CERA.AWS.CloudServices\bin\Debug\netstandard2.1\CERA.AWS.CloudService.dll"},
            //new CeraPlatformConfig(){PlatformName =   "GCP", APIClassName = "", DllPath = ""},
            //new CeraPlatformConfig(){PlatformName =   "IBM", APIClassName = "", DllPath = ""},
        };
        static void Main(string[] args)
        {
            _converter = new CeraConverter();
            foreach (var platformConfig in _platformConfigs)
            {
                ICeraCloudApiService _service = _converter.CreateInstance(platformConfig.DllPath, platformConfig.APIClassName);
                var x = _service.GetCloudSubscriptionList();
            }
            Console.WriteLine("Hello World!");
        }
    }
}
