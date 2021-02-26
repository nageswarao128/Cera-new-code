using System;

namespace CERA.Entities.ViewModels
{
    public class AddCloudPluginViewModel
    {
        public string CloudProviderName { get; set; }
        public string DllPath { get; set; }
        public string FullyQualifiedClassName { get; set; }
        public DateTime DateEnabled { get; set; }
        public string DevContact { get; set; }
        public string SupportContact { get; set; }
        public string Description { get; set; }
    }
}
