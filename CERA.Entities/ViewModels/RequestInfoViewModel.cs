using System;

namespace CERA.Entities.ViewModels
{
    /// <summary>
    /// This class conatins the request information
    /// </summary>
    public class RequestInfoViewModel:ViewModelBase
    {
        public Guid RequestID { get; set; } = Guid.NewGuid();
        public string Requester { get; set; }
        public DateTime RequestedAt { get; set; }
        
    }
}
