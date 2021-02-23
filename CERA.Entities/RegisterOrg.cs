using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CERA.Entities
{
   public class RegisterOrg
    {
        
        public int id { get; set; }
        public string orgName { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string contactPersonName { get; set; }
        public string emailId { get; set; }
        public int phoneNo { get; set; }
        public Guid userId { get; set; }
    }
}
