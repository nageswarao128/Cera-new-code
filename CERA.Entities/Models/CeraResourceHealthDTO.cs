using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
   
    public class CeraResourceHealthDTO
    {
       
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public Properties Properties { get; set; }
    }
    public class Properties
    {
        public string AvailabilityState { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ReasonType { get; set; }
        public DateTime OccuredTime { get; set; }
        public string ReasonChronicity { get; set; }
        public DateTime ReportedTime { get; set; }
    }
}
