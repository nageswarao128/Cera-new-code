using static Microsoft.Azure.Management.Fluent.Azure;

namespace CERA.AuthenticationService
{
    public interface ICeraAuthenticator
    {
        void Initialize(string tenantId, string clientID, string clientSecret);
        public string GetAuthToken();
        public string GetAuthToken(string TenantId, string ClientID, string ClientSecret);
        public string GetAuthToken(object Certificate);
        IAuthenticated GetAuthenticatedClientUsingAzureCredential();
        IAuthenticated GetAuthenticatedClient();
    }
}
