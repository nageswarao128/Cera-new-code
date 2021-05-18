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
        /// <summary>
        /// This method will initializes the serilog for storing the logs in database and
        /// Azure app insights
        /// </summary>
        public void CERALogging()
        {

            string InstrumentationKey = "992f17de-b9e2-4f34-91f8-a7f453b3de70";
            
            Log.Logger = new LoggerConfiguration()
                               .MinimumLevel.Debug()
                               .Enrich.WithProperty("SessionId", Guid.NewGuid())
                               .WriteTo.MSSqlServer
                                 (
                                   connectionString: "Server=tcp:cera.database.windows.net,1433;Initial Catalog=db_Cera;Persist Security Info=False;User ID=ceraadmin;Password=Cera@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
                                   sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions { TableName = "Logs" }

                                 )
                                .WriteTo.ApplicationInsights(InstrumentationKey,TelemetryConverter.Traces)
                               .CreateLogger();
        }
        public CERALogger(ILogger<CERALogger> logger)
        {
            _logger = logger;
            CERALogging();
        }
        /// <summary>
        /// This method will logs the errors level logs in database and Azure app insights
        /// </summary>
        /// <param name="Message"></param>
        public void LogError(string Message)
        {
            Log.Error(Message);
            _logger.LogError(Message);
        }
        /// <summary>
        /// This method will inserts the errors level logs in database and Azure app insights
        /// </summary>
        /// <param name="exception"></param>
        public void LogException(Exception exception)
        {
            Log.Logger.Error(exception.Message, "Error Occured");
            _logger.LogError(exception, "Error Occured");
        }
        /// <summary>
        /// This method will inserts the information level logs in database and Azure app insights
        /// </summary>
        /// <param name="Message"></param>
        public void LogInfo(string Message)
        {
            Log.Logger.Information(Message);
            _logger.LogInformation(Message);
        }
        /// <summary>
        /// This method will inserts the warning level logs in database and Azure app insights
        /// </summary>
        /// <param name="Message"></param>
        public void LogWarning(string Message)
        {
            Log.Logger.Warning(Message);
            _logger.LogWarning(Message);
        }

    }
}
