namespace Qxr.Logging
{
    public static class LoggingFactory
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        public static ILogger GetLogger()
        {
            return _logger;
        }
    }
}
