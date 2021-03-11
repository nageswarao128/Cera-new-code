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
        public string Authority { get; private set; } /*= "https://login.microsoftonline.com/{0}/v2.0"*/
        public string TenantId { get; set; }/* = "73d3d213-f87a-4465-9a7e-67bd625fdf9c"*/
        public string ClientID { get; set; } /*= "218411ec-50a7-4c7e-b671-e3434f3775d3";*/
        public string ClientSecret { get; set; } /*= "HX.pn3IxFE.vH1b~xY8u3Sw078LywJO_iU";*/
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
        public CeraAzureAuthenticator(string TenantId, string ClientID, string ClientSecret, string authority,ICeraLogger logger)
        {
            _logger = logger;
            InitializeVariables(TenantId, ClientID, ClientSecret,authority);
            Initialize();
        }

        void Initialize()
        {
            _logger.LogInfo("Initializing Auth Client");
            InitializeAuthClient();
            _logger.LogInfo("Initialization Complete for Auth Client");
        }

        public void Initialize(string tenantId /*= "73d3d213-f87a-4465-9a7e-67bd625fdf9c"*/, string clientID/* = "218411ec-50a7-4c7e-b671-e3434f3775d3"*/, string clientSecret/* = "HX.pn3IxFE.vH1b~xY8u3Sw078LywJO_iU"*/,string authority)
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

        public string GetAuthToken(string TenantId, string ClientID, string ClientSecret,string authority)
        {
            InitializeVariables(TenantId, ClientID, ClientSecret,authority);
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