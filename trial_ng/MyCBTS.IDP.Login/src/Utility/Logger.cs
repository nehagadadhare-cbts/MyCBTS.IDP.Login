using Microsoft.AspNetCore.Http;
using MyCBTS.IDP.Login.Mapping;
using MyCBTS.IDP.Login.Models.Api;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Utility
{
    public class Logger
    {
        public static string BaseUri;
        public static string ServerName;
        public static string RememberDays;

        public static async Task LogToDB(string level, string message, string username, string action, string controller, string userAgent, string url, string serverAddress
                                     , string client, string userId, string btn, string remoteAddress, string logger, string callSite, string exception)
        {
            LogModel log = new LogModel
            {
                Application = "CincinnatiBell.IDP.Login",
                Level = level,
                Message = message,
                UserName = username,
                ServerName = ServerName,
                Url = url,
                BTN = btn,
                UserId = userId,
                Client = client,
                ServerAddress = serverAddress,
                RemoteAddress = remoteAddress,
                Logger = logger,
                Callsite = callSite,
                Exception = exception,
                UserAgent = userAgent
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(log), Encoding.UTF8, APIConstants.MediaType);
            await PostAsync(BaseUri + "api/logger", BaseUri, content, "post");
        }

        private static async Task PostAsync(string requestPath, string baseUri, StringContent content, string method)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APIConstants.MediaType));

                    HttpResponseMessage response;
                    if (method == "get")
                    {
                        await client.GetAsync(requestPath);
                    }
                    else if (method == "put")
                    {
                        await client.PutAsync(requestPath, content);
                    }
                    else
                    {
                        response = await client.PostAsync(requestPath, content);
                    }
                }
            }
            catch (Exception)
            {
                //eat it
            }
        }

        internal static Task Audit(Audit audit, object claims, HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        internal static Task Audit(Models.Api.Audit audit, object claims, object httpContext)
        {
            throw new NotImplementedException();
        }

        public static bool Log(NLog.LogLevel level, string message, string logger, string client, string userId, string userName)
        {
            NLog.Logger log = LogManager.GetCurrentClassLogger();
            LogEventInfo theEvent = new LogEventInfo(level, logger, message);

            theEvent.Properties["UserID"] = userId;
            theEvent.Properties["Client"] = client;
            theEvent.Properties["UserName"] = userName;

            log.Log(theEvent);
            return true;
        }

        public static async Task Audit(Audit _audit, IEnumerable<Claim> claims, HttpContext context)
        {
            _audit.Application = "CincinnatiBell.IDP.Login";
            string state = string.Empty;
            if (claims != null)
            {
                var stateClaim = claims.FirstOrDefault(c => c.Type.ToLower() == "state");
                var emailClaim = claims.FirstOrDefault(c => c.Type.ToLower() == "email" || c.Type.ToLower() == "name");
                var subClaim = claims.FirstOrDefault(c => c.Type.ToLower() == "sub");
                if (stateClaim != null) { _audit.State = stateClaim.Value; }
                if (emailClaim != null) { _audit.UserName = emailClaim.Value; }
                if (subClaim != null) { _audit.SubjectId = Convert.ToInt32(subClaim.Value); }
            }
            if (context != null)
            {
                context.Request.Cookies.TryGetValue("_state", out state);
                if (string.IsNullOrEmpty(_audit.State) && !string.IsNullOrEmpty(state))
                {
                    _audit.State = state;
                }
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(_audit), Encoding.UTF8, APIConstants.MediaType);
            await PostAsync(BaseUri + "/api/audit", BaseUri, content, "put");
        }
    }
}