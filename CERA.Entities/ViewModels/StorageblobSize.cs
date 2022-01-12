using System;
using System.Collections.Generic;
using System.Text;

namespace CERA.Entities.ViewModels
{
    public class StorageblobSize
    {
        public string storageAccountId { get; set; }
        public string storageAccountName { get; set; }
        public string name { get; set; }
        public string storageType { get; set; }
        public float? size { get; set; }
    }
}
