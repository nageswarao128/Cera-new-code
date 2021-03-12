using static Microsoft.Azure.Management.Fluent.Azure;

namespace CERA.AuthenticationService
{
    public interface ICeraAuthenticator
    {
        public void Initialize(string tenantId , string clientID , string clientSecret,string authority);
        public string GetAuthToken();
        public string GetAuthToken(string TenantId, string ClientID, string ClientSecret,string Authority);
        public string GetAuthToken(object Certificate);
        IAuthenticated GetAuthenticatedClientUsingAzureCredential();
        IAuthenticated GetAuthenticatedClient();
    }
}
