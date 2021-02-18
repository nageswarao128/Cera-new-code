using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Identity.Client;
using Microsoft.Rest;
using System;
using System.Diagnostics;

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
        public string RedirectUri { get; set; }
        public CeraAzureAuthenticator()
        {

        }
        public CeraAzureAuthenticator(string authority, string clientId, string clientSecret, string redirectUri)
        {
            Authority = authority;
            ClientID = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            Initialize();
        }


        void Initialize()
        {
            var x = CreateAuthClient();
            AuthToken = "";
        }

        private IConfidentialClientApplication CreateAuthClient()
        {
            try
            {
                IConfidentialClientApplication confidentialClientApp;
                ConfidentialClientApplicationBuilder clientBuilder = ConfidentialClientApplicationBuilder
                                                                            .Create(ClientID);
                if (!string.IsNullOrWhiteSpace(string.Format(Authority, "")))
                    clientBuilder = clientBuilder.WithAuthority(Authority);
                if (!string.IsNullOrWhiteSpace(ClientSecret))
                    clientBuilder = clientBuilder.WithClientSecret(ClientSecret);
                if (!string.IsNullOrWhiteSpace(RedirectUri))
                    clientBuilder = clientBuilder.WithRedirectUri(RedirectUri);
                confidentialClientApp = clientBuilder.Build();
                return confidentialClientApp;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("CeraAuthenticator", ex.Message, EventLogEntryType.Error);
                return null;
            }

        }
        public AuthenticationResult Authenticate()
        {
            try
            {
                var app = CreateAuthClient();
                //List<string> scopes = new List<string>();
                var scopes = new[] { "https://management.core.windows.net//.default" };
                var AquireTokenClient = app.AcquireTokenForClient(scopes);
                var AuthResult = AquireTokenClient.ExecuteAsync().Result;
                return AuthResult;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("CeraAuthenticator", ex.Message, EventLogEntryType.Error);
                return null;
            }
        }

        public object CreateRestClient()
        {
            var tokenObject = Authenticate();
            TokenCredentials tokenCredentials = new TokenCredentials(tokenObject.AccessToken);
            var azureCredentials = new AzureCredentials(tokenCredentials, tokenCredentials, TenantId, AzureEnvironment.AzureGlobalCloud);
            var restClient = RestClient
            .Configure()
            .WithEnvironment(AzureEnvironment.AzureGlobalCloud)
            .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
            .WithCredentials(azureCredentials)
            .Build();
            var azure = Microsoft.Azure.Management.ResourceManager.Fluent.Azure.Authenticate(restClient, TenantId);
            return azure;
        }

        public string GetAuthToken()
        {
            var authResult = Authenticate();
            return authResult.AccessToken;

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
