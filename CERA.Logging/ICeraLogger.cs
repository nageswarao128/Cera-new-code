
using Serilog;
using System;

namespace CERA.Logging
{
    public interface ICeraLogger
    {
        public void LogInfo(string Message);
        public void LogError(string Message);
        public void LogException(Exception exception);
        public void LogWarning(string Message);
        //public void CERALogging();
    }
}
