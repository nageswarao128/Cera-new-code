using static Microsoft.Azure.Management.Fluent.Azure;

namespace CERA.AuthenticationService
{
    public interface ICeraAuthenticator
    {
        public void Initialize(string tenantId = "73d3d213-f87a-4465-9a7e-67bd625fdf9c", string clientID = "218411ec-50a7-4c7e-b671-e3434f3775d3", string clientSecret = "HX.pn3IxFE.vH1b~xY8u3Sw078LywJO_iU");
        public string GetAuthToken();
        public string GetAuthToken(string TenantId, string ClientID, string ClientSecret);
        public string GetAuthToken(object Certificate);
        IAuthenticated GetAuthenticatedClientUsingAzureCredential();
        IAuthenticated GetAuthenticatedClient();
    }
}
