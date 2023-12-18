using Duende.IdentityServer.Stores;
//using Duende.IdentityServer.Stores.Serialization;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.CustomIdentityServices.Serialization;
using MyCBTS.IDP.Login.Logger;
using Newtonsoft.Json;
using System;
using ApiClient = MyCBTS.Api.Client;

namespace MyCBTS.IDP.Login.Stores
{
    public class CustomBaseTokenStore<T> where T : class
    {
        protected readonly IResourceStore _scopeStore;
        protected IClientStore _clientStore;
        private ILogger _logger;
        private readonly ILoggerManager _loggerManager;
       // private readonly ApiClient.ICBTSServiceClient _client;

        public CustomBaseTokenStore(IResourceStore scopeStore, IClientStore clientStore, ILogger logger, ILoggerManager loggerManager)
        {
            _scopeStore = scopeStore;
            _clientStore = clientStore;
            _logger = logger;
           // _client = client;
            _loggerManager = loggerManager;
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClaimConverter());
            settings.Converters.Add(new ClaimsPrincipalConverter());
            settings.Converters.Add(new ClientConverter(_clientStore));
            settings.Converters.Add(new ScopeConverter(_scopeStore));
            return settings;
        }

        protected string ConvertToJson(T value)
        {
            try
            {
                return JsonConvert.SerializeObject(value, GetJsonSerializerSettings());
            }
            catch (Exception ex) { this._loggerManager.LogError(this._logger, null, "Error in converting to json--" + ex.Message, null); return string.Empty; }
        }

        protected T ConvertFromJson(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, GetJsonSerializerSettings());
        }
    }
}