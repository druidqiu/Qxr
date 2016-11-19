using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Text;

namespace Qxr.EntityFramework.Infrastructures
{
    public class CommandInterceptor : IDbCommandInterceptor
    {
        private static readonly ConcurrentDictionary<DbCommand, DateTime> MStartTime = new ConcurrentDictionary<DbCommand, DateTime>();

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            OnStart(command);
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            OnStart(command);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            OnStart(command);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        private static void OnStart(DbCommand command)
        {
            MStartTime.TryAdd(command, DateTime.Now);
        }

        private static void Log<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            DateTime startTime;
            TimeSpan duration;
            MStartTime.TryRemove(command, out startTime);
            if (startTime != default(DateTime))
            {
                duration = DateTime.Now - startTime;
            }
            else
            {
                duration = TimeSpan.Zero;
            }

            var parameters = new StringBuilder();
            foreach (DbParameter param in command.Parameters)
            {
                parameters.AppendLine("-- @" + param.ParameterName + ": '" + param.Value + "' (Type = " + param.DbType + ")");
            }

            var sb = new StringBuilder();
            sb.AppendLine("-- " + command.CommandText);
            sb.Append(parameters);
            if (duration.TotalSeconds > 1 && interceptionContext.Exception != null)
            {
                sb.AppendLine("-- " + interceptionContext.Exception.Message);
            }

            sb.AppendLine("-- Excuting at " + startTime);
            sb.AppendLine("-- Completed in " + duration.Milliseconds + " ms with result: " + interceptionContext.Result);

            SaveToFile(sb.ToString());
        }

        private static void SaveToFile(string text)
        {
            string path = AppConfig.EfInterceptorLogDirectoryPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string file = Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            var sw = new StreamWriter(file, true);
            sw.WriteLine(text);
            sw.Close();
        }
    }
}
