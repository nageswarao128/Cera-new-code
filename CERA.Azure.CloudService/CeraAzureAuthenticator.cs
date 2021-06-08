using CERA.CloudService;
using CERA.Logging;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Identity.Client;
using Microsoft.Rest;
using System;
using static Microsoft.Azure.Management.Fluent.Azure;

namespace CERA.Azure.CloudService
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
        /// <summary>
        /// This method will intialize the client authentication
        /// </summary>
        void Initialize()
        {
            _logger.LogInfo("Initializing Auth Client");
            InitializeAuthClient();
            _logger.LogInfo("Initialization Complete for Auth Client");
        }

        public void Initialize(string tenantId, string clientID , string clientSecret ,string authority)
        {
            //TODO: get client id , tenet id and client secret from DB
            InitializeVariables(tenantId, clientID, clientSecret,authority);
            Initialize();
        }
        /// <summary>
        /// This method will intializes the variables for the client authentication
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="authority"></param>
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
        /// <summary>
        /// This method will authenticate the user and gets the access token from the cloud
        /// </summary>
        /// <returns>returns the access token from Azure</returns>
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
            var azure = Microsoft.Azure.Management.Fluent.Azure.Authenticate(restClient, TenantId);
            //var azure = Azure.Authenticate(restClient, TenantId);
            return azure;
        }
        AzureCredentials GetAzureCredentials(string token,string tenantId)
        {
            TokenCredentials tokenCredentials = new TokenCredentials(token);
            var azureCredentials = new AzureCredentials(tokenCredentials, tokenCredentials, tenantId, AzureEnvironment.AzureGlobalCloud);
            return azureCredentials;
        }
        /// <summary>
        /// This method will comunicate the cloud and retrives the client details
        /// </summary>
        /// <returns>returns the client details</returns>
        public IAuthenticated GetAuthenticatedClientUsingAzureCredential()
        {
            var azureCredentials = GetAzureCredentials();
            var azure = Microsoft.Azure.Management.Fluent.Azure.Authenticate(azureCredentials);
            //var azure = Azure.Authenticate(azureCredentials);
            return azure;
        }
        public IAuthenticated GetAuthenticatedClientUsingAzureCredential(string token,string tenantId)
        {
            var azureCredentials = GetAzureCredentials(token,tenantId);
            var azure = Microsoft.Azure.Management.Fluent.Azure.Authenticate(azureCredentials);
            return azure;
        }
    }
}