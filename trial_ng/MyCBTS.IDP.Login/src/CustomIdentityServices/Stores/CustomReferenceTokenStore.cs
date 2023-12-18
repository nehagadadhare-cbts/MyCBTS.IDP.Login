using IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Stores.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.CustomIdentityServices.Constants;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiClient = MyCBTS.Api.Client;

namespace MyCBTS.IDP.Login.Stores
{
    public class CustomReferenceTokenStore : CustomBaseTokenStore<Token>, IReferenceTokenStore
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<CustomReferenceTokenStore> _logger;

        private readonly ILoggerManager _loggerManager;

        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        protected readonly IHttpContextAccessor _context;

        private readonly MyCBTS.Api.Client.IIdentityTokenClient _identityTokenClient;
        private readonly IPersistentGrantSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomReferenceTokenStore" /> class. This overloaded constructor is deprecated and will be removed in 3.0.0.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        public CustomReferenceTokenStore(IResourceStore scopeStore, IClientStore clientStore, ILogger<CustomReferenceTokenStore> logger, IHttpContextAccessor context, ILoggerManager loggerManager, MyCBTS.Api.Client.IIdentityTokenClient identityTokenClient, IPersistentGrantSerializer serializer)
            : base(scopeStore, clientStore, logger, loggerManager)
        {
            _serializer = serializer;
            _logger = logger;
            _context = context;
            _loggerManager = loggerManager;
            _identityTokenClient = identityTokenClient;
        }

        //
        // Summary:
        //     Gets the reference token.
        //
        // Parameters:
        //   handle:
        //     The handle.
        public async Task<Token> GetReferenceTokenAsync(string tokenkey)
        {
            try
            {
                if (!string.IsNullOrEmpty(tokenkey))
                {
                    //Token token = ConvertFromJson(json);
                    var token = await _identityTokenClient.GetTokenDetailsAsync(tokenkey);
                    Token apiToken = new Token();
                    apiToken = _serializer.Deserialize<Token>(token.JsonCode);
                    //apiToken = JsonConvert.DeserializeObject<Token>(token.JsonCode);
                    await Utility.Logger.Audit(new Models.Api.Audit
                    {
                        SubjectId = Convert.ToInt32(token.SubjectId),
                        ClientId = token.ClientId,
                        Step = string.Format("{0}.{1}", AuditConstants.Steps.RetrieveReferenceToken, token.TokenType),
                        Method = string.Format("{0}.{1}", AuditConstants.Methods.RetrieveReferenceToken, token.TokenType),
                        Result = AuditConstants.Result.Success,
                        Token = tokenkey,
                        TokenType = token.TokenType,
                        TokenStorageType = Convert.ToString(apiToken.AccessTokenType)
                    },
                        apiToken.Claims, _context.HttpContext);

                    return apiToken;
                }
                else
                {
                    this._loggerManager.LogError(this._logger, null, "Reference Token does not exist or expired. REFERENCETK: [{0}]" + tokenkey, null);
                    return null;
                }
            }
            catch (Exception ex)
            {
                this._loggerManager.LogError(this._logger, null, "Error in Create Security TokenAsync " + ex.Message, null);
                return null;
            }
        }

        //
        // Summary:
        //     Removes the reference token.
        //
        // Parameters:
        //   handle:
        //     The handle.
        public async Task RemoveReferenceTokenAsync(string handle)
        {
            await _identityTokenClient.RemoveReferenceTokenAsync(handle);
        }

        //
        // Summary:
        //     Removes the reference tokens.
        //
        // Parameters:
        //   subjectId:
        //     The subject identifier.
        //
        //   clientId:
        //     The client identifier.
        public async Task RemoveReferenceTokensAsync(string subjectId, string clientId)
        {
            await _identityTokenClient.RemoveReferenceTokenbyClientandSubjectAsync(clientId, subjectId);
        }

        //
        // Summary:
        //     Stores the reference token.
        //
        // Parameters:
        //   token:
        //     The token.
        public Task<string> StoreReferenceTokenAsync(Token token)
        {
            try
            {
                this._loggerManager.LogInfo(this._logger, null, "Store Reference TokenAsync" + token, null);
                string jsonCode = ConvertToJson(token);
                var handle = CryptoRandom.CreateUniqueId();
                ApiClient.Token apiClient = MapToken(handle, jsonCode, token);
                _identityTokenClient.CreateTokenAsync(apiClient);
                return Task.FromResult(handle);
            }
            catch (Exception ex) { this._loggerManager.LogError(this._logger, null, "Error in CustomReferenceTokenStore.StoreReferenceTokenAsync " + ex.Message, null); throw; }
        }

        public Task<string> StoreAccessTokenAsync(Token token)
        {
            try
            {
                this._loggerManager.LogInfo(this._logger, null, "Store Access TokenAsync" + token, null);
                string jsonCode = ConvertToJson(token);
                var handle = CryptoRandom.CreateUniqueId();
                ApiClient.Token apiClient = MapToken(handle, jsonCode, token);
                _identityTokenClient.CreateTokenAsync(apiClient);
                return Task.FromResult(handle);
            }
            catch (Exception ex) { this._loggerManager.LogError(this._logger, null, "Error in CustomReferenceTokenStore.StoreReferenceTokenAsync " + ex.Message, null); throw; }
        }

        public ApiClient.Token MapToken(string key, string jsonCode, Token token)
        {
            try
            {
                Dictionary<string, object> claims = token.Claims.ToClaimsDictionary();
                ApiClient.Token apiToken = new ApiClient.Token
                {
                    Key = key,
                    AuthCodeChallenge = "0",
                    AuthCodeChallengeMethod = "0",
                    ClientId = token.ClientId,
                    Expiry = token.CreationTime.AddSeconds(token.Lifetime),
                    IsOpenId = true,
                    Nonce = claims.ContainsKey("nonce") ? Convert.ToString(claims["nonce"]) : string.Empty,
                    TokenType = token.Type,
                    WasConsentShown = false,
                    JsonCode = jsonCode,
                    RedirectUri = string.Empty,
                    SessionId = claims.ContainsKey("sid") ? Convert.ToString(claims["sid"]) : string.Empty,
                    SubjectId = token.SubjectId,
                    LifeTime = token.Lifetime
                };
                return apiToken;
            }
            catch (Exception ex)
            { this._loggerManager.LogError(this._logger, null, "Error in mapping token--" + ex.Message, null); throw; }
        }
    }
}