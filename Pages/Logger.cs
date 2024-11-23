using Serilog;

namespace Pages;

public static class Logger
{
    public static Serilog.Core.Logger GetLogger()
    {
        var logDirectory = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        var logFilePath = Path.Combine(logDirectory, "log.txt");

        var _logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        return _logger;
    }
}
