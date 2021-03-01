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
       
        public dynamic CreateInstance(string DllPath = @"D:\Personal\My Projects\HealthDomain\BeFit\Jitus.BeFit\Jitus.BeFit.BMICalculator\bin\Debug\netstandard2.1\Jitus.BeFit.BMICalculation.dll", string TypeName = "Jitus.BeFit.BMICalculation.BMICalculator")
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
