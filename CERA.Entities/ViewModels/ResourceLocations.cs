using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class ResourceLocations
    {
        public int radius { get; set; }
        public string locationName { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string fillKey { get; set; }
        public int resourceCount { get; set; }
    }
}
