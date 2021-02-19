using Microsoft.Extensions.Logging;
using System;

namespace CERA.Logging
{
    public class CERALogger : ICeraLogger
    {
        ILogger _logger;
        public CERALogger(ILogger logger)
        {
            _logger = logger;
        }
        public void LogError(string Message)
        {
            _logger.LogError(Message);
        }

        public void LogException(Exception exception)
        {
            _logger.LogError(exception, "Error Occured");
        }

        public void LogInfo(string Message)
        {
            _logger.LogInformation(Message);
        }

        public void LogWarning(string Message)
        {
            _logger.LogWarning(Message);
        }
    }
}
