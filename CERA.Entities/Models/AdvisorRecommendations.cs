using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    [Table("tbl_advisor")]
    public class AdvisorRecommendations : UtilityModel
    {
        [Key]
        public int? id { get; set; }
        public string recommendationId { get; set; }
        public string category { get; set; }
        public string impact { get; set; }
        public string resourceId { get; set; }
        public string location { get; set; }
    }
}
