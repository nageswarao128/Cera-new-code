using CERA.Azure.CloudService;
using CERA.DataOperation;
using CERA.DataOperation.CeraContext;
using System;

namespace CERA.TestClient
{
    class Program
    {
        static CeraDbContext dbContext = new CeraDbContext();


        static void Main(string[] args)
        {

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
