using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    [Table("tbl_Compliances")]
    public class CeraCompliances
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string AssessmentType { get; set; }
    }
}
