using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class StorageContainersDTO
    {

        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string etag { get; set; }
        [JsonProperty(propertyName: "properties")]
        public ContainerProperties properties { get; set; }
    }

        public class ContainerProperties
        {
            public Immutablestoragewithversioning immutableStorageWithVersioning { get; set; }
            public bool deleted { get; set; }
            public int remainingRetentionDays { get; set; }
            public string defaultEncryptionScope { get; set; }
            public bool denyEncryptionScopeOverride { get; set; }
            public string publicAccess { get; set; }
            public string leaseStatus { get; set; }
            public string leaseState { get; set; }
            public DateTime lastModifiedTime { get; set; }
            public bool hasImmutabilityPolicy { get; set; }
            public bool hasLegalHold { get; set; }
        }

        public class Immutablestoragewithversioning
        {
            public bool enabled { get; set; }
        }

    }

