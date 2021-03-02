using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;

namespace CERA.Logging
{
    public class CERALogger : ICeraLogger
    {
        ILogger<CERALogger> _logger;

        public void CERALogging()
        {
         
            Log.Logger = new LoggerConfiguration()
                               .MinimumLevel.Debug()
                               .Enrich.WithProperty("CorrelationId", Guid.NewGuid())
                               .WriteTo.MSSqlServer
                                 (
                                   connectionString: "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_Cera; Integrated Security= true;",
                                   sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions { TableName = "Logs" }
                                   
                                 )
                               .CreateLogger();
        }
        public CERALogger(ILogger<CERALogger> logger)
        {
            _logger = logger;
            CERALogging();
        }
        public void LogError(string Message)
        {
            Log.Error(Message);
            _logger.LogError(Message);
        }

        public void LogException(Exception exception)
        {
            Log.Logger.Error(exception.Message, "Error Occured");
            _logger.LogError(exception, "Error Occured");
        }

        public void LogInfo(string Message)
        {
            Log.Logger.Information(Message);
            _logger.LogInformation(Message);
        }

        public void LogWarning(string Message)
        {
            Log.Logger.Warning(Message);
            _logger.LogWarning(Message);
        }

    }
}
