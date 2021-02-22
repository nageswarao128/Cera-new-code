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

        public static I CreateInstance<I>() where I : class
        {
            string assemblyPath = Environment.CurrentDirectory + "\\DynamicCreateInstanceofclass.exe";
            Assembly assembly;
            assembly = Assembly.LoadFrom(assemblyPath);
            Type type = assembly.GetType("DynamicCreateInstanceofclass.UserDetails");
            return Activator.CreateInstance(type) as I;
        }

        public dynamic CreateInstance(string DllPath = @"D:\Personal\My Projects\HealthDomain\BeFit\Jitus.BeFit\Jitus.BeFit.BMICalculator\bin\Debug\netstandard2.1\Jitus.BeFit.BMICalculation.dll", string TypeName = "Jitus.BeFit.BMICalculation.BMICalculator")
        {
            var assembly = Assembly.LoadFile(DllPath);
            var name = assembly.GetName();
            var objectType = assembly.GetType(TypeName);
            var instantiatedObject = Activator.CreateInstance(objectType);
            return instantiatedObject;
        }
    }
}
