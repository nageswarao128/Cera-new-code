using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CERA.Entities.Models
{
    /// <summary>
    /// This class is used as a object for Virtual Machines
    /// </summary>
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