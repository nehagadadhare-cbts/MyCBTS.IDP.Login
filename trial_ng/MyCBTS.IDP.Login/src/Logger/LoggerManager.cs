using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCBTS.IDP.Login.Mapping;
using MyCBTS.IDP.Login.Models.Api;
using MyCBTS.IDP.Login.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyCBTS.IDP.Login.Configuration;
using MyCBTS.IDP.Login.Extensions;

namespace MyCBTS.IDP.Login.Logger
{
    /// <summary>
    /// creates custom Logger class
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        private static CBTSServiceConfiguration _CBTSServiceConfiguration;

        public LoggerManager(IOptions<CBTSServiceConfiguration> CBTSServiceConfiguration)
        {
            _CBTSServiceConfiguration = CBTSServiceConfiguration.Value;
        }

        public async Task<bool> LogInfo(Microsoft.Extensions.Logging.ILogger logger, Exception ex, string message, UserLogger commonLogger)
        {
            object infoProperty = null;
            if (commonLogger != null)
                infoProperty = new { UserId = commonLogger.UserID, UserName = commonLogger.UserName, Application = commonLogger.Application, Client = commonLogger.Client, BTN = commonLogger.AccountNumber };
            logger.LogInformation(ex,
                 message + "{customprop}", infoProperty);
            return true;
        }

        public async Task<bool> LogError(Microsoft.Extensions.Logging.ILogger logger, Exception ex, string message, UserLogger commonLogger)
        {
            object errorProperty = null;
            if (commonLogger != null)
                errorProperty = new { UserId = commonLogger.UserID, UserName = commonLogger.UserName, Application = commonLogger.Application, Client = commonLogger.Client, BTN = commonLogger.AccountNumber };
            logger.LogError(ex,
                message + "{customprop}", errorProperty);
            return true;
        }

        public async Task<bool> LogDebug(Microsoft.Extensions.Logging.ILogger logger, Exception ex, string message, UserLogger commonLogger)
        {
            object debugProperty = null;
            if (commonLogger != null)
                debugProperty = new { UserId = commonLogger.UserID, UserName = commonLogger.UserName, Application = commonLogger.Application, Client = commonLogger.Client, BTN = commonLogger.AccountNumber };
            logger.LogDebug(ex,
                 message + "{customprop}", debugProperty);
            return true;
        }

        public async Task<bool> LogWarn(Microsoft.Extensions.Logging.ILogger logger, Exception ex, string message, UserLogger commonLogger)
        {
            object warnProperty = null;
            if (commonLogger != null)
                warnProperty = new { UserId = commonLogger.UserID, UserName = commonLogger.UserName, Application = commonLogger.Application, Client = commonLogger.Client, BTN = commonLogger.AccountNumber };
            logger.LogDebug(ex,
                message + "{customprop}", warnProperty);
            return true;
        }

        public async Task<bool> LogCritical(Microsoft.Extensions.Logging.ILogger logger, Exception ex, string message, UserLogger commonLogger)
        {
            object criticalProperty = null;
            if (commonLogger != null)
                criticalProperty = new { UserId = commonLogger.UserID, UserName = commonLogger.UserName, Application = commonLogger.Application, Client = commonLogger.Client, BTN = commonLogger.AccountNumber };
            logger.LogDebug(ex,
                message + "{customprop}", criticalProperty);
            return true;
        }

        public static async void LogMethod(string application, string level, string message, string username, string serverName, string action,
           String controller, string userAgent, String serverAddress, String client, String userId, String btn, String remoteAddress,
           String logger, String callSite, String exception, String custom1, String custom2, String custom3, String custom4)
        {
            try
            {
                LogModel logException = new LogModel
                {
                    Application = application,
                    Level = level,
                    Message = message,
                    UserName = username,
                    ServerName = serverName,
                    Action = action,
                    Controller = controller,
                    UserAgent = userAgent,
                    ServerAddress = serverAddress,
                    Client = client,
                    UserId = userId,
                    BTN = btn,
                    RemoteAddress = remoteAddress,
                    Logger = logger,
                    Callsite = callSite,
                    Exception = exception,
                    Custom1 = custom1,
                    Custom2 = custom2,
                    Custom3 = custom3,
                    Custom4 = custom4,
                };

                bool retunStatus = await CreateLogModel(logException);
            }
            catch (Exception ex)
            {
            }
        }

        private static async Task<bool> CreateLogModel(LogModel logException)
        {
            string baseUri = _CBTSServiceConfiguration.BaseURI.DecryptSecret();
            string requestPath = string.Format(baseUri + APIConstants.LogException);
            StringContent content = new StringContent(JsonConvert.SerializeObject(logException), Encoding.UTF8, APIConstants.MediaType);
            HttpResponseMessage response = GetHttpResponse(requestPath, content, baseUri);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static HttpResponseMessage GetHttpResponse(string requestPath, StringContent content, string baseURI)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var authToken = Encoding.ASCII.GetBytes($"{_CBTSServiceConfiguration.AuthUser.DecryptSecret()}:{_CBTSServiceConfiguration.AuthPassword.DecryptSecret()}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                    client.DefaultRequestHeaders.Add("ApplicationName", _CBTSServiceConfiguration.Application);
                    HttpResponseMessage response = null;
                    response = client.PostAsync(requestPath, content).Result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.StackTrace);
                return null;
            }
        }
    }
}