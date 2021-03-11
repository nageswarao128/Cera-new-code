using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CERA.Entities.Models
{
    [Table("tbl_VirtualMachines")]
    public class CeraVM
    {
        [Key]
        public int VMId { get; set; }
        public string VMName { get; set; }
        public string RegionName { get; set; }
        public string ResourceGroupName { get; set; }
    }
}