using Newtonsoft.Json;
using System;
using System.Reflection;

namespace CERA.Converter
{
    public class CeraConverter : ICeraConverter
    {
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
            var assembly = Assembly.LoadFile(DllPath);
            if (assembly != null)
            {
                var objectType = assembly.GetType(TypeName);
                if (objectType != null)
                {
                    var instantiatedObject = Activator.CreateInstance(objectType);
                    return instantiatedObject;
                }
            }
            return null;
        }
    }
}
