using System;

namespace Qxr.Logging
{
    public interface ILoggerFactory
    {
        void Initialize(ILogger logger);

        ILogger GetLogger();
    }
}