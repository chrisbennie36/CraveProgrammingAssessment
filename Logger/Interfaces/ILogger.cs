using System;

namespace Logging.Interfaces
{
    public interface ILogger
    {
        void Debug(string message);
        void Debug(string message, Guid correlationId);
        void Info(string message);
        void Info(string message, Guid correlationId);
        void Warn(string message);
        void Warn(string message, Guid correlationId);
        void Error(Exception exception);
        void Error(Exception exception, string message);
        void Error(Exception exception, Guid correlationId);
        void Error(Exception exception, Guid correlationId, string message);
    }
}
