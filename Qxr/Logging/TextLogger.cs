using System;

namespace Qxr.Logging
{
    public class TextLogger : ILogger
    {

        private void WriteLog(object message, Exception exc = null)
        {
            string fileName = "Qxr_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string path = AppConfig.TxtLogFolder;
            string fileFullName = System.IO.Path.Combine(path, fileName);
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(fileFullName, true);
                writer.WriteLine(message);
                if (exc != null)
                {
                    writer.WriteLine(exc.Message);
                }
                writer.Close();
            }
            catch (Exception)
            {

            }
        }

        public bool IsEnabled(LogLevel level)
        {
            return true;
        }

        public void Log(LogLevel level, string message)
        {
            WriteLog(message);
        }

        public void Log(LogLevel level, string message, Exception exception)
        {
            WriteLog(message, exception);
        }
    }
}
