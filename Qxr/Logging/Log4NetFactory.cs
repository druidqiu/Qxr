namespace Qxr.Logging
{
    public class Log4NetFactory : ILoggerFactory
    {
        private  ILogger _logger;

        public  void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        public  ILogger GetLogger()
        {
            return _logger;
        }
    }
}
