using Serilog;
using Serilog.Context;
using System;
using SerilogConfiguration = Serilog.LoggerConfiguration;
using ILogger = Logging.Interfaces.ILogger;
using Logging.Interfaces;

namespace Logging
{
    public class Logger : ILogger
    {
        private ILoggerConfiguration _loggerConfiguration;

        private const string DefaultMessage = "No message specified";

        public Logger(ILoggerConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration ?? throw new ArgumentNullException(nameof(loggerConfiguration));

            Initialise();
        }

        private void Initialise()
        {
            Log.Logger = new SerilogConfiguration()
            .MinimumLevel.Is(_loggerConfiguration.MinimumLogLevel)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .WriteTo.File(_loggerConfiguration.FilePath,
            rollingInterval: _loggerConfiguration.FileRollingInterval,
            shared: _loggerConfiguration.FileShared,
            outputTemplate: _loggerConfiguration.BaseLogFormat,
            retainedFileCountLimit: _loggerConfiguration.RetainedFileCountLimit)
            .CreateLogger();
        }

        public void Debug(string message)
        {
            Debug(message, Guid.NewGuid());
        }

        public void Debug(string message, Guid correlationId)
        {
            PushLogProperties(message, correlationId);

            Log.Debug(_loggerConfiguration.DebugLogFormat, message);
        }

        public void Info(string message)
        {
            Info(message, Guid.NewGuid());
        }

        public void Info(string message, Guid correlationId)
        {
            PushLogProperties(message, correlationId);

            Log.Information(_loggerConfiguration.InformationLogFormat, message);
        }

        public void Warn(string message)
        {
            Warn(message, Guid.NewGuid());
        }

        public void Warn(string message, Guid correlationId)
        {
            PushLogProperties(message, correlationId);

            Log.Warning(_loggerConfiguration.WarningLogFormat);
        }

        public void Error(Exception exception)
        {
            Error(exception, Guid.NewGuid(), string.Empty);
        }

        public void Error(Exception exception, string message)
        {
            Error(exception, Guid.NewGuid(), message);
        }

        public void Error(Exception exception, Guid correlationId)
        {
            Error(exception, correlationId, string.Empty);
        }

        public void Error(Exception exception, Guid correlationId, string message)
        {
            PushLogProperties(message, correlationId, exception);

            Log.Error(_loggerConfiguration.ErrorLogFormat, message, exception);
        }

        private void PushLogProperties(string message, Guid correlationId, Exception exception = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = DefaultMessage;
            }

            LogContext.PushProperty("Message", message);
            LogContext.PushProperty("CorrelationId", correlationId);

            if (exception != null)
            {
                LogContext.PushProperty("Exception", exception.ToString());
            }
        }
    }
}
