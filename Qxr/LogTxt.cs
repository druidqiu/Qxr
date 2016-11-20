using System;
using System.IO;

namespace Qxr
{
    public class LogTxt
    {
        public static void Debug(string message)
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\Logger.log");
            StreamWriter sw = new StreamWriter(logFilePath, true);
            sw.WriteLine(message);
            sw.Close();
        }
    }
}
