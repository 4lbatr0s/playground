using Contracts;
using NLog;
namespace LoggingService;

public class LoggerManager:ILoggerManager
{
    private static ILogger logger = LogManager.GetCurrentClassLogger(); //INFO: Comes from nlog.

    public LoggerManager()
    {
        
    }

    public void LogInfo(string message)
    {
        logger.Info(message);
    }

    public void LogWarn(string message)
    {
        logger.Warn(message);
    }

    public void LogDebug(string message)
    {
        logger.Debug(message);
    }

    public void LogError(string message)
    {
        logger.Error(message);
    }
}