using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{

    public class CeraResourceHealthDTO
    {

        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public Properties properties { get; set; }

    }
        public class Properties
        {
            public string availabilityState { get; set; }
            public string title { get; set; }
            public string summary { get; set; }
            public string reasonType { get; set; }
            public DateTime occuredTime { get; set; }
            public string reasonChronicity { get; set; }
            public DateTime reportedTime { get; set; }
        }
    }

