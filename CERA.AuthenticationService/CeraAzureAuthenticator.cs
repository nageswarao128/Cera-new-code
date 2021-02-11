namespace CERA.AuthenticationService
{
    public class CeraAzureAuthenticator : ICeraAuthenticator
    {
        public string Authority { get; set; } = "";
        public string TenantId { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public object Certificate { get; set; }
        public string AuthToken { get; set; }
        public CeraAzureAuthenticator()
        {

        }
        public CeraAzureAuthenticator(string TenantId, string ClientID, string ClientSecret)
        {
            Initialize();
        }

        void Initialize()
        {
            var x = CreateAuthClient();
            AuthToken = "";
        }

        private object CreateAuthClient()
        {
            throw new System.NotImplementedException();
        }

        public string GetAuthToken()
        {
            return string.Empty;
        }

        public string GetAuthToken(string TenantId, string ClientID, string ClientSecret)
        {
            throw new System.NotImplementedException();
        }

        public string GetAuthToken(object Certificate)
        {
            throw new System.NotImplementedException();
        }

        public object GetCertificate()
        {
            return new object();
        }

        public string GetJwtToken(string TenantId, string ClientID, string ClientSecret)
        {
            return string.Empty;
        }
    }
}
