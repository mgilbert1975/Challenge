using Challenge.Interfaces;
using log4net;
using log4net.Config;
using System.Reflection;

namespace Challenge.Services
{
    public class Log4NetService : Ilog4net
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(Log4NetService));

        public Log4NetService()
        {
            try
            {
                var repository = LogManager.GetRepository(Assembly.GetCallingAssembly());

                var fileInfo = new FileInfo(@"log4net.config");

                XmlConfigurator.Configure(repository, fileInfo);

                _log.Debug("Logger initialized");
            }
            catch (Exception ex)
            {
                _log.Error("Error at init logger", ex);
            }
        }
        public void LogDebug(string msg) => _log.Debug(msg);

        public void LogInfo(string msg) => _log.Info(msg);

        public void LogWarn(string msg) => _log.Warn(msg);

        public void LogError(string msg, Exception ex = null)
        {
            if (ex != null)
                _log.Error(msg, ex);

            _log.Error(msg);
        }
    }
}
