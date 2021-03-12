using CERA.Logging;
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
        public string Authority { get; private set; }
        public string TenantId { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public object Certificate { get; set; }
        public string AuthToken { get; private set; }
        public string RedirectUri { get; set; }
        IConfidentialClientApplication confidentialClientApp;
        private ICeraLogger _logger;

        public CeraAzureAuthenticator(ICeraLogger logger)
        {
            _logger = logger;
            Initialize();
        }
        public CeraAzureAuthenticator(string TenantId, string ClientID, string ClientSecret,string Authority, ICeraLogger logger)
        {
            _logger = logger;
            InitializeVariables(TenantId, ClientID, ClientSecret,Authority);
            Initialize();
        }

        void Initialize()
        {
            _logger.LogInfo("Initializing Auth Client");
            InitializeAuthClient();
            _logger.LogInfo("Initialization Complete for Auth Client");
        }

        public void Initialize(string tenantId , string clientID, string clientSecret,string authority)
        {
            
            InitializeVariables(tenantId, clientID, clientSecret,authority);
            Initialize();
        }
        void InitializeVariables(string tenantId, string clientID, string clientSecret,string authority)
        {
            _logger.LogInfo("Initializing Variable for Auth Client");
            if (!string.IsNullOrWhiteSpace(tenantId))
                Authority = string.Format(authority, tenantId);
            if (!string.IsNullOrWhiteSpace(clientID))
                ClientID = clientID;
            if (!string.IsNullOrWhiteSpace(clientSecret))
                ClientSecret = clientSecret;
            _logger.LogInfo("Initialization Complete  Variable for Auth Client");
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

        public string GetAuthToken(string TenantId, string ClientID, string ClientSecret,string Authority)
        {
            InitializeVariables(TenantId, ClientID, ClientSecret,Authority);
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