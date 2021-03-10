using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CERA.Entities.Models
{
    /// <summary>
    /// This class is used as a object for client details
    /// </summary>
    [Table("tbl_Clients")]
    public class Client : BaseEntity
    {
        public string ClientName { get; set; }
        public string PrimaryAddress { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryEmail { get; set; }
        public int PrimaryPhone { get; set; }
        public string ClientDescription { get; set; }
    }
}
