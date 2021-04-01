using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    /// <summary>
    /// This class is used as a object for Tenants
    /// </summary>
    [Table("tbl_Tenants")]
    public class CeraTenants
    {
        [Key]
        public int? Id { get; set; }
        public string Key { get; set; }
        public string TenantId { get; set; }
    }
}
