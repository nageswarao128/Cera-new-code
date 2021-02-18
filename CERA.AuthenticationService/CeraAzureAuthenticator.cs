using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Identity.Client;
using Microsoft.Rest;
using System;
using static Microsoft.Azure.Management.Fluent.Azure;

namespace CERA.AuthenticationService
{
    public class CeraAzureAuthenticator : ICeraAuthenticator
    {
        public string Authority { get; private set; } = "https://login.microsoftonline.com/{0}/v2.0";
        public string TenantId { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public object Certificate { get; set; }
        public string AuthToken { get; private set; }
        public string RedirectUri { get; set; }
        IConfidentialClientApplication confidentialClientApp;
        public CeraAzureAuthenticator()
        {
            Initialize();
        }
        public CeraAzureAuthenticator(string TenantId, string ClientID, string ClientSecret)
        {
            InitializeVariables(TenantId, ClientID, ClientSecret);
            Initialize();
        }

        void Initialize()
        {
            InitializeAuthClient();
        }
        void InitializeVariables(string tenantId, string clientID, string clientSecret)
        {
            Authority = string.Format(Authority, tenantId);
            ClientID = clientID;
            ClientSecret = clientSecret;
        }

        private void InitializeAuthClient()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ClientID))
                {
                    ConfidentialClientApplicationBuilder clientBuilder = ConfidentialClientApplicationBuilder
                                                                                .Create(ClientID);
                    if (!string.IsNullOrWhiteSpace(Authority))
                        clientBuilder = clientBuilder.WithAuthority(Authority);
                    if (!string.IsNullOrWhiteSpace(ClientSecret))
                        clientBuilder = clientBuilder.WithClientSecret(ClientSecret);
                    if (!string.IsNullOrWhiteSpace(RedirectUri))
                        clientBuilder = clientBuilder.WithRedirectUri(RedirectUri);
                    confidentialClientApp = clientBuilder.Build();
                }
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("CeraAuthenticator", ex.Message, EventLogEntryType.Error);
            }
        }

        public string GetAuthToken()
        {
            var scopes = new[] { "https://management.core.windows.net//.default" };
            var AquireTokenClient = confidentialClientApp.AcquireTokenForClient(scopes);
            var AuthResult = AquireTokenClient.ExecuteAsync().Result;
            AuthToken = AuthResult.AccessToken;
            return AuthToken;
        }

        public string GetAuthToken(string TenantId, string ClientID, string ClientSecret)
        {
            InitializeVariables(TenantId, ClientID, ClientSecret);
            GetAuthToken();
            return AuthToken;
        }

        public string GetAuthToken(object Certificate)
        {
            AuthToken = "";
            return AuthToken;
        }

        public object GetCertificate()
        {
            return new object();
        }

        public RestClient CreateRestClient()
        {
            var azureCredentials = GetAzureCredentials();
            var restClient = RestClient
            .Configure()
            .WithEnvironment(AzureEnvironment.AzureGlobalCloud)
            .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
            .WithCredentials(azureCredentials)
            .Build();
            return restClient;
        }
        AzureCredentials GetAzureCredentials()
        {
            GetAuthToken();
            TokenCredentials tokenCredentials = new TokenCredentials(AuthToken);
            var azureCredentials = new AzureCredentials(tokenCredentials, tokenCredentials, TenantId, AzureEnvironment.AzureGlobalCloud);
            return azureCredentials;
        }
        public IAuthenticated GetAuthenticatedClient()
        {
            var restClient = CreateRestClient();
            var azure = Azure.Authenticate(restClient, TenantId);
            return azure;
        }
        public IAuthenticated GetAuthenticatedClientUsingAzureCredential()
        {
            var azureCredentials = GetAzureCredentials();
            var azure = Azure.Authenticate(azureCredentials);
            return azure;
        }
    }
}