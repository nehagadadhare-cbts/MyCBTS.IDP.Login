using Duende.IdentityServer;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.CustomIdentityServices.Constants;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Logger;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.CustomIdentityServices.Services
{
    public class CustomRefreshTokenService : IRefreshTokenService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        private readonly ILoggerManager _loggerManager;

        /// <summary>
        /// The refresh token store
        /// </summary>
        protected readonly IRefreshTokenStore RefreshTokenStore;

        /// <summary>
        /// The _events
        /// </summary>
        protected readonly IEventService Events;

        protected readonly IHttpContextAccessor _context;

        private readonly IProfileService _profile;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRefreshTokenService" /> class.
        /// </summary>
        /// <param name="refreshTokenStore">The refresh token store</param>
        /// <param name="events">The events.</param>
        /// <param name="logger">The logger</param>
        public CustomRefreshTokenService(IRefreshTokenStore refreshTokenStore, ILogger<DefaultRefreshTokenService> logger, IHttpContextAccessor context, ILoggerManager loggerManager, IProfileService profile)
        {
            _logger = logger;
            _loggerManager = loggerManager;
            RefreshTokenStore = refreshTokenStore;
            _context = context;
            _profile = profile;
        }

        /// <summary>
        /// Creates the refresh token.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        /// The refresh token handle
        /// </returns>
        async Task<string> CreateRefreshTokenAsync(ClaimsPrincipal subject, Token accessToken, Client client)
        {
            try
            {
                await this._loggerManager.LogDebug(this._logger, null, "Creating refresh token", null);
                int lifetime;
                if (client.RefreshTokenExpiration == TokenExpiration.Absolute)
                {
                    await this._loggerManager.LogDebug(this._logger, null, "Setting an absolute lifetime: " + client.AbsoluteRefreshTokenLifetime, null);
                    lifetime = client.AbsoluteRefreshTokenLifetime;
                }
                else
                {
                    await this._loggerManager.LogDebug(this._logger, null, "Setting a sliding lifetime: " + client.SlidingRefreshTokenLifetime, null);
                    lifetime = client.SlidingRefreshTokenLifetime;
                }
                var refreshToken = new RefreshToken
                {
                    CreationTime = DateTime.Now,
                    Lifetime = lifetime,
                    AccessToken = accessToken,
                };
                var handle = await RefreshTokenStore.StoreRefreshTokenAsync(refreshToken);
                await Utility.Logger.Audit(new Models.Api.Audit
                {
                    SubjectId = Convert.ToInt32(accessToken.SubjectId),
                    ClientId = accessToken.ClientId,
                    Step = string.Format("{0}", AuditConstants.Steps.CreateRefreshToken),
                    Method = string.Format("{0}", AuditConstants.Methods.CreateRefreshToken),
                    Result = AuditConstants.Result.Success,
                    Token = handle,
                    TokenType = "refresh_token",
                    TokenStorageType = Convert.ToString(accessToken.AccessTokenType)
                }, accessToken.Claims, _context.HttpContext);

                return handle;
            }
            catch (Exception ex)
            {
                this._loggerManager.LogError(this._logger, null, "Error in Creating refresh token " + ex.Message, null);
                return null;
            }
        }

        /// <summary>
        /// Updates the refresh token.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        /// The refresh token handle
        /// </returns>
        public virtual async Task<string> UpdateRefreshTokenAsync(string handle, RefreshToken refreshToken, Client client)
        {
            try
            {
                await this._loggerManager.LogDebug(this._logger, null, "Updating refresh token", null);
                var originalHandle = handle;
                bool needsCreate = false;
                bool needsUpdate = false;
                if (client.RefreshTokenUsage == TokenUsage.OneTimeOnly)
                {
                    await this._loggerManager.LogDebug(this._logger, null, "Token usage is one-time only. Generating new handle", null);
                    // delete old one
                    await RefreshTokenStore.RemoveRefreshTokenAsync(handle);
                    // create new one
                    needsCreate = true;
                }
                if (client.RefreshTokenExpiration == TokenExpiration.Sliding)
                {
                    await this._loggerManager.LogDebug(this._logger, null, "Refresh token expiration is sliding - extending lifetime", null);
                    // make sure we don't exceed absolute exp
                    // cap it at absolute exp
                    var currentLifetime = refreshToken.CreationTime.GetLifetimeInSeconds();
                    await this._loggerManager.LogDebug(this._logger, null, "Current lifetime: " + currentLifetime.ToString(), null);
                    var newLifetime = currentLifetime + client.SlidingRefreshTokenLifetime;
                    await this._loggerManager.LogDebug(this._logger, null, "New lifetime: " + newLifetime.ToString(), null);
                    if (newLifetime > client.AbsoluteRefreshTokenLifetime)
                    {
                        newLifetime = client.AbsoluteRefreshTokenLifetime;
                        await this._loggerManager.LogDebug(this._logger, null, "New lifetime exceeds absolute lifetime, capping it to " + newLifetime.ToString(), null);
                    }

                    refreshToken.Lifetime = newLifetime;
                    needsUpdate = true;
                }

                if (needsCreate)
                {
                    handle = await RefreshTokenStore.StoreRefreshTokenAsync(refreshToken);
                    await this._loggerManager.LogDebug(this._logger, null, "Created refresh token in store", null);
                }
                else if (needsUpdate)
                {
                    await RefreshTokenStore.UpdateRefreshTokenAsync(handle, refreshToken);
                    await this._loggerManager.LogDebug(this._logger, null, "Updated refresh token in store", null);
                }
                else
                {
                    await this._loggerManager.LogDebug(this._logger, null, "No updates to refresh token done", null);
                }
                await Utility.Logger.Audit(new Models.Api.Audit
                {
                    SubjectId = Convert.ToInt32(refreshToken.SubjectId),
                    ClientId = refreshToken.ClientId,
                    Step = string.Format("{0}", AuditConstants.Steps.UpdateRefreshToken),
                    Method = string.Format("{0}", AuditConstants.Methods.UpdateRefreshToken),
                    Result = AuditConstants.Result.Success,
                    Token = handle,
                    TokenType = "regenenate_refresh_token",
                    TokenStorageType = Convert.ToString(refreshToken.AccessToken.AccessTokenType)
                }, refreshToken.AccessToken.Claims, _context.HttpContext);

                return handle;
            }
            catch (Exception ex)
            {
                this._loggerManager.LogError(this._logger, null, "Error in updating refresh token " + ex.Message, null);
                return await Task.FromResult<string>(null);
            }
        }

        public async Task<TokenValidationResult> ValidateRefreshTokenAsync(string tokenHandle, Client client = null)
        {
            _logger.LogTrace("Start refresh token validation");

            /////////////////////////////////////////////
            // check if refresh token is valid
            /////////////////////////////////////////////
            var refreshToken = await RefreshTokenStore.GetRefreshTokenAsync(tokenHandle);
            if (refreshToken == null)
            {
                _logger.LogError("Invalid refresh token");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            /////////////////////////////////////////////
            // check if refresh token has expired
            /////////////////////////////////////////////
            if (refreshToken.CreationTime.HasExceeded(refreshToken.Lifetime))
            {

                _logger.LogError(string.Format("Refresh token has expired. Removing from store. REFRESHTK: [{0}]", tokenHandle));
                await RefreshTokenStore.RemoveRefreshTokenAsync(tokenHandle);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            if (client != null)
            {
                /////////////////////////////////////////////
                // check if client belongs to requested refresh token
                /////////////////////////////////////////////
                if (client.ClientId != refreshToken.ClientId)
                {
                    _logger.LogError("{0} tries to refresh token belonging to {1}", client.ClientId, refreshToken.ClientId);
                    return Invalid(OidcConstants.TokenErrors.InvalidGrant);
                }

                /////////////////////////////////////////////
                // check if client still has offline_access scope
                /////////////////////////////////////////////
                if (!client.AllowOfflineAccess)
                {
                    _logger.LogError("{clientId} does not have access to offline_access scope anymore", client.ClientId);
                    return Invalid(OidcConstants.TokenErrors.InvalidGrant);
                }

            }

            /////////////////////////////////////////////
            // make sure user is enabled
            /////////////////////////////////////////////
            var isActiveCtx = new IsActiveContext(
                refreshToken.Subject,
                client,
                IdentityServerConstants.ProfileIsActiveCallers.RefreshTokenValidation);
            await _profile.IsActiveAsync(isActiveCtx);

            if (isActiveCtx.IsActive == false)
            {
                _logger.LogError("{subjectId} has been disabled", refreshToken.Subject.GetSubjectId());
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }


            LogSuccess();

            return new TokenValidationResult
            {
                IsError = false,
                RefreshToken = refreshToken
            };
        }

        private TokenValidationResult Invalid(string error)
        {
            return new TokenValidationResult
            {
                IsError = true,
                Error = error
            };
        }

        private void LogSuccess()
        {
            _logger.LogDebug("Token validation success.");
        }

        public async Task<string> CreateRefreshTokenAsync(RefreshTokenCreationRequest request)
        {
            _logger.LogDebug("Creating refresh token");

            int lifetime;
            if (request.Client.RefreshTokenExpiration == TokenExpiration.Absolute)
            {
                _logger.LogDebug("Setting an absolute lifetime: " + request.Client.AbsoluteRefreshTokenLifetime);
                lifetime = request.Client.AbsoluteRefreshTokenLifetime;
            }
            else
            {
                _logger.LogDebug("Setting a sliding lifetime: " + request.Client.SlidingRefreshTokenLifetime);
                lifetime = request.Client.SlidingRefreshTokenLifetime;
            }

            var refreshToken = new RefreshToken
            {
                CreationTime = DateTime.Now,
                Lifetime = lifetime,
                AccessToken = request?.AccessToken,
                ClientId = request?.Client.ClientId,
                Subject = request?.Subject

            };

            var handle = await RefreshTokenStore.StoreRefreshTokenAsync(refreshToken);


            await Utility.Logger.Audit(new MyCBTS.IDP.Login.Models.Api.Audit
            {
                SubjectId = Convert.ToInt32(request.AccessToken.SubjectId),
                ClientId = request.AccessToken.ClientId,
                Step = string.Format("{0}", AuditConstants.Steps.CreateRefreshToken),
                Method = string.Format("{0}", AuditConstants.Methods.CreateRefreshToken),
                Result = AuditConstants.Result.Success,
                Token = handle,
                TokenType = "refresh_token",
                TokenStorageType = Convert.ToString(request.AccessToken.AccessTokenType)
            }, request.AccessToken.Claims, _context.HttpContext);

            return handle;
        }

        public virtual async Task<string> UpdateRefreshTokenAsync(RefreshTokenUpdateRequest request)
        {
            _logger.LogDebug("Updating refresh token");

            bool needsCreate = false;
            bool needsUpdate = false;

            if (request.Client.RefreshTokenUsage == TokenUsage.OneTimeOnly)
            {
                _logger.LogDebug("Token usage is one-time only. Generating new handle");

                // delete old one
                await RefreshTokenStore.RemoveRefreshTokenAsync(request.Handle);

                // create new one
                needsCreate = true;
            }

            if (request.Client.RefreshTokenExpiration == TokenExpiration.Sliding)
            {
                _logger.LogDebug("Refresh token expiration is sliding - extending lifetime");

                // make sure we don't exceed absolute exp
                // cap it at absolute exp
                var currentLifetime = request.RefreshToken.CreationTime.GetLifetimeInSeconds();
                _logger.LogDebug("Current lifetime: " + currentLifetime.ToString());

                var newLifetime = currentLifetime + request.Client.SlidingRefreshTokenLifetime;
                _logger.LogDebug("New lifetime: " + newLifetime.ToString());

                if (newLifetime > request.Client.AbsoluteRefreshTokenLifetime)
                {
                    newLifetime = request.Client.AbsoluteRefreshTokenLifetime;
                    _logger.LogDebug("New lifetime exceeds absolute lifetime, capping it to " + newLifetime.ToString());
                }

                request.RefreshToken.Lifetime = newLifetime;
                needsUpdate = true;
            }

            if (needsCreate)
            {
                request.Handle = await RefreshTokenStore.StoreRefreshTokenAsync(request.RefreshToken);
                _logger.LogDebug("Created refresh token in store");
            }
            else if (needsUpdate)
            {
                await RefreshTokenStore.UpdateRefreshTokenAsync(request.Handle, request.RefreshToken);
                _logger.LogDebug("Updated refresh token in store");
            }
            else
            {
                _logger.LogDebug("No updates to refresh token done");
            }
            await Utility.Logger.Audit(new MyCBTS.IDP.Login.Models.Api.Audit
            {
                SubjectId = Convert.ToInt32(request.RefreshToken.SubjectId),
                ClientId = request.RefreshToken.ClientId,
                Step = string.Format("{0}", AuditConstants.Steps.UpdateRefreshToken),
                Method = string.Format("{0}", AuditConstants.Methods.UpdateRefreshToken),
                Result = AuditConstants.Result.Success,
                Token = request.Handle,
                TokenType = "regenenate_refresh_token",
                TokenStorageType = Convert.ToString(request.RefreshToken.AccessToken.AccessTokenType)
            }, request.RefreshToken.AccessToken.Claims, _context.HttpContext);

            return request.Handle;
        }
    }
}