using System;

namespace Qxr.Logging
{
    [Flags]
    public enum LogLevel
    {
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    }

    public interface ILogger
    {
        bool IsEnabled(LogLevel level);
        void Log(LogLevel level, string message);
        void Log(LogLevel level,string message, Exception exception);
    }
}
