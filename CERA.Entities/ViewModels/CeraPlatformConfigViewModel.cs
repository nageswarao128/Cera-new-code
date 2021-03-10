namespace CERA.Entities.ViewModels
{
    /// <summary>
    /// This class is used as a data transfer objecct for retriving the dll of a platform
    /// </summary>
    public class CeraPlatformConfigViewModel
    {
        public string PlatformName { get; set; }
        public string APIClassName { get; set; }
        public string DllPath { get; set; }
    }
}
