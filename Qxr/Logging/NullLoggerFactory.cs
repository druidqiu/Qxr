using System;

namespace Qxr.Logging
{
    class NullLoggerFactory : ILoggerFactory
    {
        public void Initialize(ILogger logger)
        {
            throw new NotImplementedException();
        }

        public ILogger GetLogger()
        {
            throw new NotImplementedException();
        }
    }
}