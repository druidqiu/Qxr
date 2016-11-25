using System;
using System.Configuration;
using System.IO;

namespace Qxr
{
    public static class AppConfig
    {
        public static string UserSessionKey
        {
            get { return GetAppSettingValue("user_session_key"); }
        }

        public static string PageMessageKey
        {
            get { return GetAppSettingValue("page_message"); }
        }

        public static string EfInterceptorLogDirectoryPath
        {
            get { return GetOrCreateFolder("EfInterceptorLogs"); }
        }

        public static string Log4NetName
        {
            get { return GetAppSettingValue("Log4NetName"); }
        }

        public static string Log4NetConfigUrl
        {
            get { return GetUrl("Log4NetConfigLocation"); }
        }

        public static string TxtLogFolder
        {
            get { return GetOrCreateFolder("LogFolder"); }
        }


        private static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? "";
        }

        private static string GetUrl(string key)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GetAppSettingValue(key));
        }

        private static string GetOrCreateFolder(string key)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GetAppSettingValue(key));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
