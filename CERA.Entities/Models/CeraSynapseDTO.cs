using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.Models
{
    public class CeraSynapseDTO
    {

        [JsonProperty(propertyName: "properties")]
        public SynapseProperties properties { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public Identity identity { get; set; }
        public Tags tags { get; set; }
    }

    public class SynapseProperties
    {
        public Encryption encryption { get; set; }
        public string provisioningState { get; set; }
        public Connectivityendpoints connectivityEndpoints { get; set; }
        public string managedResourceGroupName { get; set; }
        public string sqlAdministratorLogin { get; set; }
        public object[] privateEndpointConnections { get; set; }
        public string workspaceUID { get; set; }
        public Extraproperties extraProperties { get; set; }
        public string publicNetworkAccess { get; set; }
        public Cspworkspaceadminproperties cspWorkspaceAdminProperties { get; set; }
        public bool trustedServiceBypassEnabled { get; set; }
    }

    public class Encryption
    {
        public bool doubleEncryptionEnabled { get; set; }
    }

    public class Connectivityendpoints
    {
        public string web { get; set; }
        public string dev { get; set; }
        public string sqlOnDemand { get; set; }
        public string sql { get; set; }
    }

    public class Extraproperties
    {
        public string WorkspaceType { get; set; }
        public bool IsScopeEnabled { get; set; }
    }

    public class Cspworkspaceadminproperties
    {
        public string initialWorkspaceAdminObjectId { get; set; }
    }

    public class Identity
    {
        public string type { get; set; }
        public string tenantId { get; set; }
        public string principalId { get; set; }
    }

    public class Tags
    {
    }

}

