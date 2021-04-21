using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    [Table("tbl_RateCard")]
    public class CeraRateCard
    {
        public string Currency { get; set; }
        public DateTime EffectiveDate { get; set; }
        public float IncludedQuantity { get; set; }
        public string MeterCategory { get; set; }
        [Key]
        public string MeterId { get; set; }
        public string MeterName { get; set; }
        public string MeterRegion { get; set; }
        public string MeterStatus { get; set; }
        public string MeterSubCategory { get; set; }
        public string Unit { get; set; }
    }
}
