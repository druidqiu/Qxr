using System;

namespace Qxr.Logging
{
    public class NullLogger : ILogger
    {
        private static readonly ILogger _instance = new NullLogger();

        public static ILogger Instance
        {
            get { return _instance; }
        }

        public void Debug(object message)
        {
            throw new NotImplementedException();
        }

        public void Info(object message)
        {
            throw new NotImplementedException();
        }

        public void Warn(object message)
        {
            throw new NotImplementedException();
        }

        public void Error(object message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(object message)
        {
            throw new NotImplementedException();
        }

        public void Debug(object message, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Info(object message, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Warn(object message, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Error(object message, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Fatal(object message, Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}