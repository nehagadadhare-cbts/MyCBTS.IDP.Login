using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Stores.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.CustomIdentityServices.Constants;

//using MyCBTS.IDP.Login.CustomIdentityServices.Repository;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Mapping;
using MyCBTS.IDP.Login.Logger;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiClient = MyCBTS.Api.Client;

namespace MyCBTS.IDP.Login.Stores
{
    public class CustomAuthorizationCodeStore : IAuthorizationCodeStore
    {
        private readonly IHandleGenerationService _handleGenerationService;

        /// <summary>
        /// Singing service
        /// </summary>

        private readonly MyCBTS.Api.Client.IIdentityTokenClient _identityTokenClient;
        private readonly ILogger _logger;
        private readonly ILoggerManager _loggerManager;
        private readonly IPersistentGrantSerializer _serializer;
        private readonly IHttpContextAccessor _context;

        public CustomAuthorizationCodeStore(
           MyCBTS.Api.Client.IIdentityTokenClient identityTokenClient,
            IPersistentGrantSerializer serializer,
            IHandleGenerationService handleGenerationService,
            IReferenceTokenStore store,
            ILogger<CustomAuthorizationCodeStore> logger,
            ILoggerManager loggerManager,
            IHttpContextAccessor context)
        {
            _identityTokenClient = identityTokenClient;
            _serializer = serializer;
            _handleGenerationService = handleGenerationService;
            _context = context;
            _logger = logger;
            _loggerManager = loggerManager;
        }

        /// <summary>
        /// Stores the authorization code asynchronous.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task<string> StoreAuthorizationCodeAsync(AuthorizationCode code)
        {
            try
            {
                var handle = await _handleGenerationService.GenerateAsync();
                var content = MapTokenFromAuthorizationCode(handle, _serializer.Serialize(code), code);

                await _identityTokenClient.CreateTokenAsync(content);
                var state = _context.HttpContext.Request.Query["state"];
                await Utility.Logger.Audit(new Models.Api.Audit
                {
                    ClientId = code.ClientId,
                    State = state,
                    Step = AuditConstants.Steps.CreateToken,
                    Method = AuditConstants.Methods.CreateToken,
                    Result = AuditConstants.Result.Success,
                    Token = handle,
                    TokenType = IDPConstants.PersistedGrantTypes.AuthorizationCode,
                    TokenStorageType = IDPConstants.PersistedGrantTypes.ReferenceToken,
                }, code.Subject.Claims, _context.HttpContext);
                return handle;
            }
            catch (ApiClient.ApiException ex)
            {
                var code1 = ex.StatusCode;
                var message = JsonConvert.DeserializeObject<ErrorDescription>(ex.Response);
                await this._loggerManager.LogError(this._logger, ex, "Error while storing Authorization code", null);
                return await Task.FromResult<string>(null);
            }
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, ex, "Error while storing Authorization code", null);
                return await Task.FromResult<string>(null);
            }
        }

        /// <summary>
        /// Gets the authorization code asynchronous.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task<AuthorizationCode> GetAuthorizationCodeAsync(string tokenKey)
        {
            try
            {
                var responseString = await _identityTokenClient.GetTokenDetailsAsync(tokenKey);
                AuthorizationCode authCode = _serializer.Deserialize<AuthorizationCode>(responseString.JsonCode);
                var state = _context.HttpContext.Request.Query["state"];
                await Utility.Logger.Audit(new Models.Api.Audit
                {
                    ClientId = authCode.ClientId,
                    State = state,
                    Step = AuditConstants.Steps.RetrieveAuthCode,
                    Method = AuditConstants.Methods.RetrieveAuthCode,
                    Result = AuditConstants.Result.Success,
                    Token = tokenKey,
                    TokenType = IDPConstants.PersistedGrantTypes.AuthorizationCode,
                    TokenStorageType = IDPConstants.PersistedGrantTypes.ReferenceToken,
                }, authCode.Subject.Claims, _context.HttpContext);

                return authCode;
            }
            catch (ApiClient.ApiException ex)
            {
                var code1 = ex.StatusCode;
                var message = JsonConvert.DeserializeObject<ErrorDescription>(ex.Response);
                await this._loggerManager.LogError(this._logger, ex, "Error while getting Authorization code", null);
                return await Task.FromResult<AuthorizationCode>(null);
            }
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, ex, "Error while getting Authorization code", null);
                return await Task.FromResult<AuthorizationCode>(null);
            }
        }

        /// <summary>
        /// Removes the authorization code asynchronous.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public async Task RemoveAuthorizationCodeAsync(string code)
        {
            await _identityTokenClient.RemoveReferenceTokenAsync(code);
        }

        private ApiClient.Token MapTokenFromAuthorizationCode(string key, string jsonCode, AuthorizationCode code)
        {
            ApiClient.Token apiToken = new ApiClient.Token
            {
                Key = key,
                AuthCodeChallenge = "0",
                AuthCodeChallengeMethod = "0",
                ClientId = code.ClientId,
                Expiry = code.CreationTime.AddSeconds(code.Lifetime),
                IsOpenId = code.IsOpenId,
                Nonce = string.Empty,
                TokenType = "code",
                WasConsentShown = false,
                JsonCode = jsonCode,
                RedirectUri = string.Empty,
                SessionId = code.SessionId,
                SubjectId = code.Subject.GetSubjectId(),
                LifeTime = code.Lifetime
            };
            return apiToken;
        }
    }
}