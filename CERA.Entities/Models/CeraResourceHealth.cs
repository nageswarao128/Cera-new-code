using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    [Table("tbl_ResourceHealth")]
    public class CeraResourceHealth
    {
        [Key]
        public int? Id { get; set; }
        public string ResourceId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string AvailabilityState { get; set; }
        public string Type { get; set; }
    }
}
