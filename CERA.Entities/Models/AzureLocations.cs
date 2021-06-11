using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    [Table("tbl_Locations")]
    public class AzureLocations
    {
        [Key]
        public int? id { get; set; }
        public string LocationId { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string regionalDisplayName { get; set; }
        public string regionType { get; set; }
        public string geographyGroup { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string physicalLocation { get; set; }
    }
}
