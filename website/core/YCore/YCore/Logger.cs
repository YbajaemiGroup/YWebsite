using YCore.Data;

namespace YCore
{
    internal static class Logger
    {
        private static List<Task> logTasks = new List<Task>();

        public static void Log(LogSeverity logSeverity, string source, string message, Exception? exception = null)
        {
            //_ = Task.Run(() =>
            //{
            //    logTasks.RemoveAll(t => t.IsCompleted);
            //});
            Console.WriteLine($"{TimeOnly.FromDateTime(DateTime.Now)} {source} {logSeverity}\n{message}\n{exception}");
            // logTasks.Add(DatabaseInteractor.Instance().WrileLog(logSeverity, source, message, exception));
        }
    }
}
