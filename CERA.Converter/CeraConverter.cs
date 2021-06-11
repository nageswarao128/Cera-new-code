using CERA.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace CERA.Converter
{
    public class CeraConverter : ICeraConverter
    {
        public ICeraLogger Logger { get; set; }
        public CeraConverter(ICeraLogger logger )
        {
            Logger = logger;
        }
        public string GenerateJson(object model)
        {
            var jsonData = JsonConvert.SerializeObject(model);
            return jsonData;
        }
        /// <summary>
        /// This method will takes the Dll path and class name and loads the files from the assembly
        /// </summary>
        /// <param name="DllPath"></param>
        /// <param name="TypeName"></param>
        /// <returns>returns a object which contains instance of the dll path </returns>
        public dynamic CreateInstance(string DllPath , string TypeName )
        {
            try
            {
                string fullPath = Path.GetFullPath(DllPath);
                var assembly = Assembly.LoadFile(fullPath);
                if (assembly != null)
                {
                    Logger.LogInfo($"dll: {assembly.CodeBase}; loaded successfully");
                    var objectType = assembly.GetType(TypeName);
                    if (objectType != null)
                    {
                        var instantiatedObject = Activator.CreateInstance(objectType);
                        return instantiatedObject;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw;
            }
            
        }
    }
}
