using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CERA.Entities.Models
{
    [Table("tbl_StorageSize")]
    public class StorageSize
    {
        [Key]
        public int? Id { get; set; }
        public string ResourceID { get; set; }
        public string Name { get; set; }
        public string StorageType { get; set; }
        public float? Size { get; set; }
    }
}
