using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Logger
{
    public class Logger : ILogger
    {
        private readonly ILog _logger;

        public Logger(string applicationName)
        {
            _logger = LogManager.GetLogger(applicationName);

            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var logsFolder = Path.Combine(documentsFolder, "SteamDeckTools", "Logs");

            BasicConfigurator.Configure();
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders();

            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date [%thread] %-5level %logger - %message%newline"
            };
            patternLayout.ActivateOptions();

            var roller = new RollingFileAppender
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
        
        public void Trace(object subject)
        {
            _logger.Logger.Log(_logger.GetType(), Level.Trace, subject, null);
        }

        public void Info(string format, params object?[] arg)
        {
            String line = String.Format(format, arg);
            _logger.Info(line);
        }

        public void Debug(string format, params object?[] arg)
        {
            String line = String.Format(format, arg);
            _logger.Debug(line);
        }

        public void Error(string format, params object?[] arg)
        {
            String line = String.Format(format, arg);
            _logger.Error(line);
        }

        public void Fatal(string type, Object? name, Exception e)
        {
            var message = $"{type}: {name}: Exception: {e.Message}";
            _logger.Fatal(message, e);
        }

        public void Fatal(string type, Exception e)
        {
            var message = $"{type}: Exception: {e.Message}";
            _logger.Fatal(message, e);
        }
    }
}