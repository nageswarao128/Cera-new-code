using System;

namespace CERA.Entities.Models
{
    /// <summary>
    /// This class contains the useful information which is used by other model classes
    /// </summary>
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
