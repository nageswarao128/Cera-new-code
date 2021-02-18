using static Microsoft.Azure.Management.Fluent.Azure;

namespace CERA.AuthenticationService
{
    public interface ICeraAuthenticator
    {
        public string GetAuthToken();
        public string GetAuthToken(string TenantId, string ClientID, string ClientSecret);
        public string GetAuthToken(object Certificate);
        IAuthenticated GetAuthenticatedClientUsingAzureCredential();
        IAuthenticated GetAuthenticatedClient();
    }
}
