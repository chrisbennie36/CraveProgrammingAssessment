using Serilog;
using Serilog.Events;

namespace Logging.Interfaces
{
    public interface ILoggerConfiguration
    {
        string FilePath { get; set; }
        RollingInterval FileRollingInterval { get; set; }
        bool FileShared { get; set; }
        LogEventLevel MinimumLogLevel { get; set; }
        string BaseLogFormat { get; set; }
        string DebugLogFormat { get; set; }
        string InformationLogFormat { get; set; }
        string WarningLogFormat { get; set; }
        string ErrorLogFormat { get; set; }
        int RetainedFileCountLimit { get; set; }
    }
}
