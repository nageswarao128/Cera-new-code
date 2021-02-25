using System;

namespace CERA.Entities.ViewModel
{
    public class RequestInfoViewModel
    {
        public Guid RequestID { get; set; } = Guid.NewGuid();
        public string Requester { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
