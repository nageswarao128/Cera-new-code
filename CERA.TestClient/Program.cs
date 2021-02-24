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
            string authority = "https://login.microsoftonline.com/73d3d213-f87a-4465-9a7e-67bd625fdf9c/v2.0";
            string ClientId = "218411ec-50a7-4c7e-b671-e3434f3775d3";
            string ClientSecret = "HX.pn3IxFE.vH1b~xY8u3Sw078LywJO_iU";
            string redirectUri = "https://management.azure.com";
            string TenantId = "73d3d213-f87a-4465-9a7e-67bd625fdf9c";
            CeraAzureApiService apiService = new CeraAzureApiService();
            var list = apiService.GetSubscriptionsList(authority, ClientId, ClientSecret, redirectUri, TenantId);
            Console.WriteLine(list);

            CERADataOperation dataOperation = new CERADataOperation(dbContext);
            var sample = dataOperation.AddSubscriptionData(list);



        }
    }
}
