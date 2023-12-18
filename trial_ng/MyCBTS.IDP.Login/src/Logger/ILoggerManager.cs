using MyCBTS.IDP.Login.Models;
using System;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Logger
{
    public interface ILoggerManager
    {
        Task<bool> LogInfo(Microsoft.Extensions.Logging.ILogger logger, Exception ex, string message, UserLogger commonLogger);

        Task<bool> LogWarn(Microsoft.Extensions.Logging.ILogger logger, Exception ex, string message, UserLogger commonLogger);

        Task<bool> LogCritical(Microsoft.Extensions.Logging.ILogger logger, Exception ex, string message, UserLogger commonLogger);

        Task<bool> LogError(Microsoft.Extensions.Logging.ILogger logger, Exception ex, string message, UserLogger commonLogger);

        Task<bool> LogDebug(Microsoft.Extensions.Logging.ILogger logger, Exception ex, string message, UserLogger commonLogger);
    }
}