using Serilog;
using Serilog.Events;
using Logging.Interfaces;

namespace Logging
{
    public class LoggerConfiguration : ILoggerConfiguration
    {
        public string FilePath { get; set; } = @"C:\Logs\Log.txt";
        public LogEventLevel MinimumLogLevel { get; set; } = LogEventLevel.Error;
        public string BaseLogFormat { get; set; } = "{Timestamp:yyyy-MM-dd HH:mm:sszzz} [{Level}] - {CorrelationId} - {ThreadId} - {Message} {Exception} {NewLine}";
        public string InformationLogFormat { get; set; } = "{Message}";
        public string DebugLogFormat { get; set; } = "{Message}";
        public string WarningLogFormat { get; set; } = "{Message}";
        public string ErrorLogFormat { get; set; } = "{Message} - {Exception}";
        public RollingInterval FileRollingInterval { get; set; } = RollingInterval.Day;
        public bool FileShared { get; set; } = true;
        public int RetainedFileCountLimit { get; set; } = 14;
    }
}
