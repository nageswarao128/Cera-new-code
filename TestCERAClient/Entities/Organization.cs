using System;
namespace CERAAPI.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string PrimaryAddress { get; set; }
        public string Description { get; set; }
        public string ContactPersonName { get; set; }
        public string EmailId { get; set; }
        public int PhoneNo { get; set; }
        public Guid UserId { get; set; }
    }
}
