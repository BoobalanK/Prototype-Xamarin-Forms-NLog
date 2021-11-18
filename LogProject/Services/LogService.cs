using System;
using System.Reflection;
using System.Xml;
using NLog;
using NLog.Config;
using NLog.Targets;

using Xamarin.Essentials;

namespace LogProject
{
    /// <summary>
    /// Log all stacktraces depending on their level : Info, Error, Fatal, Info, Warning.
    /// </summary>
    public class LogService : ILogService
    {
        private Logger logger;

        public void Initialize(Assembly assembly, string assemblyName)
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") 
            { 
                FileName = $"{FileSystem.AppDataDirectory}/{DateTime.Now.Date.ToString("yyyy-M-dd")}.txt",
                DeleteOldFileOnStartup=true,
                EnableFileDelete=true,
                FileNameKind=FilePathKind.Relative,
                Name="file",
            };

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;

            //var location = $"{assemblyName}.NLog.config";
            //var stream = assembly.GetManifestResourceStream(location);
            //LogManager.Configuration = new XmlLoggingConfiguration(XmlReader.Create(stream), null);
            
            this.logger = LogManager.GetCurrentClassLogger();
        }

        public void LogDebug(string message)
        {
            this.logger.Info(message);
        }

        public void LogError(string message)
        {
            this.logger.Error(message);
        }

        public void LogFatal(string message)
        {
            this.logger.Fatal(message);
        }

        public void LogInfo(string message)
        {
            this.logger.Info(message);
        }

        public void LogWarning(string message)
        {
            this.logger.Warn(message);
        }
    }
}
