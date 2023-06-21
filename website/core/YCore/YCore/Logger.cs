using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCore.Data;

namespace YCore
{
    internal static class Logger
    {
        public static void Log(LogSeverity logSeverity, string source, string message, Exception? exception = null)
        {
            Console.WriteLine($"{TimeOnly.FromDateTime(DateTime.Now)} {source} {logSeverity}\n{message}\n{exception}");
            //_ = DatabaseInteractor.Instance().WrileLog(logSeverity, source, message, exception);
        }
    }
}
