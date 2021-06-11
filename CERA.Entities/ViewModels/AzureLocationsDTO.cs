using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class AzureLocationsDTO
    {
      
            public string id { get; set; }
            public string name { get; set; }
            public string displayName { get; set; }
            public string regionalDisplayName { get; set; }
            public Metadata metadata { get; set; }
        

        public class Metadata
        {
            public string regionType { get; set; }
            public string regionCategory { get; set; }
            public string geographyGroup { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }
            public string physicalLocation { get; set; }
            public Pairedregion[] pairedRegion { get; set; }
        }

        public class Pairedregion
        {
            public string name { get; set; }
            public string id { get; set; }
        }

    }
}
