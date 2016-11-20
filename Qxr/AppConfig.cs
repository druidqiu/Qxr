using System;
using System.IO;

namespace Qxr
{
    public static class AppConfig
    {
        //TODO: Split in different classes
        public static string UserSessionKey = "user_session_key";
        public static string PageMessageKey = "page_message";
        public static string EfInterceptorLogDirectoryPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "EfInterceptorLogs");
            }
        }
    }
}
