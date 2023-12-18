using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.Extensions.Logging;

//using MyCBTS.IDP.Login.CustomIdentityServices.Repository;
using MyCBTS.IDP.Login.Logger;
using System;
using System.Threading.Tasks;
using ApiClient = MyCBTS.Api.Client;

namespace MyCBTS.IDP.Login.Stores
{
    public class CustomRefreshTokenStore : CustomBaseTokenStore<Token>, IRefreshTokenStore
    {
        /// <summary>
        /// Generation Service
        /// </summary>
        private readonly IHandleGenerationService _handleGenerationService;

        /// <summary>
        /// The Logger
        /// </summary>
        private readonly ILogger<CustomRefreshTokenStore> _logger;

        private readonly ILoggerManager _loggerManager;
        private readonly MyCBTS.Api.Client.IIdentityTokenClient _identityTokenClient;

        public CustomRefreshTokenStore(IResourceStore scopeStore, IClientStore clientStore, IHandleGenerationService handleGenerationService, ILogger<CustomRefreshTokenStore> logger, ILoggerManager loggerManager, MyCBTS.Api.Client.IIdentityTokenClient identityTokenClient)
            : base(scopeStore, clientStore, logger, loggerManager)
        {
            _handleGenerationService = handleGenerationService;
            _logger = logger;
            _identityTokenClient = identityTokenClient;
            _loggerManager = loggerManager;
        }

        //
        // Summary:
        //     Gets the refresh token.
        //
        // Parameters:
        //   refreshTokenHandle:
        //     The refresh token handle.
        async Task<RefreshToken> IRefreshTokenStore.GetRefreshTokenAsync(string refreshTokenHandle)
        {
            ApiClient.RefreshToken apiRefreshToken = await _identityTokenClient.GetRefreshTokenDetailsAsync(refreshTokenHandle);
            if (apiRefreshToken != null && !string.IsNullOrEmpty(apiRefreshToken.JsonCode))
            {
                RefreshToken refreshToken = new RefreshToken()
                {
                    AccessToken = ConvertFromJson(apiRefreshToken?.JsonCode),
                    CreationTime = Convert.ToDateTime(apiRefreshToken.CreationTime),
                    Lifetime = apiRefreshToken.Lifetime
                };
                return refreshToken;
            }
            else
            {
                this._loggerManager.LogError(this._logger, null, "Refresh Token does not exist or expired. REFRESHTK: [{0}]" + refreshTokenHandle, null);
                return null;
            }
        }

        //
        // Summary:
        //     Removes the refresh token.
        //
        // Parameters:
        //   refreshTokenHandle:
        //     The refresh token handle.
        async Task IRefreshTokenStore.RemoveRefreshTokenAsync(string refreshTokenHandle)
        {
            await _identityTokenClient.RemoveRefreshTokenAsync(refreshTokenHandle);
        }

        //
        // Summary:
        //     Removes the refresh tokens.
        //
        // Parameters:
        //   subjectId:
        //     The subject identifier.
        //
        //   clientId:
        //     The client identifier.
        async Task IRefreshTokenStore.RemoveRefreshTokensAsync(string subjectId, string clientId)
        {
            await _identityTokenClient.RemoveReferenceTokenbyClientandSubjectAsync(clientId, subjectId);
        }

        //
        // Summary:
        //     Stores the refresh token.
        //
        // Parameters:
        //   refreshToken:
        //     The refresh token.
        async Task<string> IRefreshTokenStore.StoreRefreshTokenAsync(RefreshToken refreshToken)
        {
            try
            {
                this._loggerManager.LogInfo(this._logger, null, "Store Refresh TokenAsync" + refreshToken, null);
                var handle = await _handleGenerationService.GenerateAsync();
                ApiClient.RefreshToken apiToken = new ApiClient.RefreshToken
                {
                    Key = handle,
                    ClientId = refreshToken.ClientId,
                    CreationTime = refreshToken.CreationTime,
                    GrantType = "refresh_token",
                    Lifetime = refreshToken.Lifetime,
                    SubjectId = refreshToken.SubjectId,
                    TokenType = 1,
                    JsonCode = ConvertToJson(refreshToken.AccessToken)
                };
                _ = this._loggerManager.LogInfo(this._logger, null, "Creating Refresh Token" + apiToken, null);
                if (await _identityTokenClient.CreateRefreshTokenAsync(apiToken))
                {
                    return handle;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                _ = this._loggerManager.LogError(this._logger, null, "Error in Store Refresh TokenAsync  " + ex.Message, null);
                return null;
            }
        }

        //
        // Summary:
        //     Updates the refresh token.
        //
        // Parameters:
        //   handle:
        //     The handle.
        //
        //   refreshToken:
        //     The refresh token.
        Task IRefreshTokenStore.UpdateRefreshTokenAsync(string handle, RefreshToken refreshToken)
        {
            try
            {
                this._loggerManager.LogDebug(this._logger, null, "Updating refresh token", null);
                ApiClient.RefreshToken apiToken = new ApiClient.RefreshToken
                {
                    Key = handle,
                    ClientId = refreshToken.ClientId,
                    CreationTime = refreshToken.CreationTime,
                    GrantType = "refresh_token",
                    Lifetime = refreshToken.Lifetime,
                    SubjectId = refreshToken.SubjectId,
                    TokenType = 1,
                    JsonCode = ConvertToJson(refreshToken.AccessToken)
                };
                return Task.FromResult(_identityTokenClient.UpdateRefreshTokenAsync(apiToken,handle));
            }
            catch (Exception ex)
            {
                this._loggerManager.LogError(this._logger, null, "Error in updating refresh token " + ex.Message, null);
                return Task.FromResult<CustomRefreshTokenStore>(null);
            }
        }
    }
}