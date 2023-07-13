using YCore.Data;

namespace YCore;

internal static class Logger
{
    public static void Log(LogSeverity logSeverity, string source, string message, Exception? exception = null)
    {
        Console.WriteLine($"{TimeOnly.FromDateTime(DateTime.Now)} {source} {logSeverity}\n{message}\n{exception}");
        _ = DatabaseInteractor.Instance().WrileLogAsync(logSeverity, source, message, exception);
    }
}
