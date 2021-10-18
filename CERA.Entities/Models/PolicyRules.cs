using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    [Table("tbl_PolicyRules")]
    public class PolicyRules
    {
        [Key]
        public Guid id { get; set; }
        public string subscriptionName { get; set; }
        public string policyRule { get; set; }
    }
}
