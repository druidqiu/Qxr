using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxr.Logging
{
    public class TextLogger : ILogger
    {
        public void Debug(object message)
        {
            WriteLog(message);
        }

        public void Info(object message)
        {
            WriteLog(message);
        }

        public void Warn(object message)
        {
            WriteLog(message);
        }

        public void Error(object message)
        {
            WriteLog(message);
        }

        public void Fatal(object message)
        {
            WriteLog(message);
        }

        public void Debug(object message, Exception ex)
        {
            WriteLog(message, ex);
        }

        public void Info(object message, Exception ex)
        {
            WriteLog(message, ex);
        }

        public void Warn(object message, Exception ex)
        {
            WriteLog(message, ex);
        }

        public void Error(object message, Exception ex)
        {
            WriteLog(message, ex);
        }

        public void Fatal(object message, Exception ex)
        {
            WriteLog(message, ex);
        }


        private void WriteLog(object message, Exception exc = null)
        {
            string fileName = "Qxr_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data\\Log");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
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
    }
}
