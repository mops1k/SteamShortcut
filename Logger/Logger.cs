using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Logger;

public class Logger : ILogger
{
    private readonly ILog _logger;

    public Logger(string applicationName, string baseDirectoryName)
    {
        _logger = LogManager.GetLogger(applicationName);

        string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string logsFolder = Path.Combine(documentsFolder, baseDirectoryName, "Logs");

        BasicConfigurator.Configure();
        Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
        hierarchy.Root.RemoveAllAppenders();

        PatternLayout patternLayout = new()
        {
            ConversionPattern = "%date [%thread] %-5level %logger - %message%newline"
        };
        patternLayout.ActivateOptions();

        RollingFileAppender roller = new()
        {
            AppendToFile = true,
            File = logsFolder + Path.DirectorySeparatorChar,
            DatePattern = $"'{applicationName}_'dd.MM.yyyy'.log'",
            Layout = patternLayout,
            MaxSizeRollBackups = 5,
            MaximumFileSize = "5MB",
            RollingStyle = RollingFileAppender.RollingMode.Composite,
            StaticLogFileName = false
        };
        roller.ActivateOptions();
        hierarchy.Root.AddAppender(roller);

#if DEBUG
        hierarchy.Root.Level = Level.All;
#else
            hierarchy.Root.Level = Level.Info;
#endif
        hierarchy.Configured = true;
        BasicConfigurator.Configure(hierarchy);
    }

    public void Trace(object subject) => _logger.Logger.Log(_logger.GetType(), Level.Trace, subject, null);

    public void Info(string format, params object?[] arg)
    {
        string line = string.Format(format, arg);
        _logger.Info(line);
    }

    public void Debug(string format, params object?[] arg)
    {
        string line = string.Format(format, arg);
        _logger.Debug(line);
    }

    public void Error(string format, params object?[] arg)
    {
        string line = string.Format(format, arg);
        _logger.Error(line);
    }

    public void Fatal(string type, object? name, Exception e)
    {
        string? message = $"{type}: {name}: Exception: {e.Message}";
        _logger.Fatal(message, e);
    }

    public void Fatal(string type, Exception? e)
    {
        string? message = $"{type}: Exception: {e.Message}";
        _logger.Fatal(message, e);
    }
}
