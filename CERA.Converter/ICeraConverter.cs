namespace CERA.Converter
{
    public interface ICeraConverter
    {
        public string GenerateJson(object model);
        public dynamic CreateInstance(string DllPath = @"D:\Personal\My Projects\HealthDomain\BeFit\Jitus.BeFit\Jitus.BeFit.BMICalculator\bin\Debug\netstandard2.1\Jitus.BeFit.BMICalculation.dll", string TypeName = "Jitus.BeFit.BMICalculation.BMICalculator");
    }
}