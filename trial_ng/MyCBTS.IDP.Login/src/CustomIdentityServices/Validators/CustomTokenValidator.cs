using IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Logger;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.CustomIdentityServices.Validators
{
    public class CustomTokenValidator : ITokenValidator
    {
        private readonly ILogger _logger;
        private readonly ILoggerManager _loggerManager;
        private readonly IdentityServerOptions _options;
        private readonly IHttpContextAccessor _context;
        private readonly IReferenceTokenStore _referenceTokenStore;
        private readonly ICustomTokenValidator _customValidator;
        private readonly IClientStore _clients;
        private readonly IKeyMaterialService _keys;
        private readonly IProfileService _profile;
        private readonly ISystemClock _clock;
        private readonly IRefreshTokenStore _refreshTokenStore;

        public CustomTokenValidator(IdentityServerOptions options, IHttpContextAccessor context, IClientStore clients, IReferenceTokenStore referenceTokenStore, ICustomTokenValidator customValidator, IKeyMaterialService keys, ILogger<ITokenValidator> logger, IProfileService profile, ILoggerManager loggerManager)
        {
            _options = options;
            _context = context;
            _clients = clients;
            _referenceTokenStore = referenceTokenStore;
            _customValidator = customValidator;
            _keys = keys;
            _logger = logger;
            _profile = profile;
            _loggerManager = loggerManager;
        }

        //
        // Summary:
        //     Validates an identity token.
        //
        // Parameters:
        //   token:
        //     The token.
        //
        //   clientId:
        //     The client identifier.
        //
        //   validateLifetime:
        //     if set to true the lifetime gets validated. Otherwise not.
        public virtual async Task<TokenValidationResult> ValidateIdentityTokenAsync(string token, string clientId = null, bool validateLifetime = true)
        {
            await this._loggerManager.LogDebug(this._logger, null, "Start identity token validation", null);

            if (token.Length > _options.InputLengthRestrictions.Jwt)
            {
                await this._loggerManager.LogError(this._logger, null, "JWT too long", null);
                return Invalid(OidcConstants.ProtectedResourceErrors.InvalidToken);
            }

            if (string.IsNullOrEmpty(clientId))
            {
                clientId = await GetClientIdFromJwt(token);

                if (string.IsNullOrEmpty(clientId))
                {
                    await this._loggerManager.LogError(this._logger, null, "No clientId supplied, can't find id in identity token.", null);
                    return Invalid(OidcConstants.ProtectedResourceErrors.InvalidToken);
                }
            }
            var client = await _clients.FindEnabledClientByIdAsync(clientId);
            if (client == null)
            {
                await this._loggerManager.LogError(this._logger, null, "Unknown or diabled client: {clientId}." + clientId, null);
                return Invalid(OidcConstants.ProtectedResourceErrors.InvalidToken);
            }
            await this._loggerManager.LogDebug(this._logger, null, "Client found: {clientId} / {clientName}" + client.ClientId + client.ClientName, null);

            //13920 
            //var keys = await _keys.GetValidationKeysAsync();
            //var result = await ValidateJwtAsync(token, clientId, keys, validateLifetime);
            var keyinfo = await _keys.GetValidationKeysAsync();
            var keys = keyinfo.Select(s => s.Key);

            var result = await ValidateJwtAsync(
                token,
                string.Format("{0}resources", _context.HttpContext.GetIdentityServerIssuerUri()),
                keys);

            result.Client = client;
            if (result.IsError)
            {
                await LogError("Error validating JWT");
                return result;
            }
            await this._loggerManager.LogDebug(this._logger, null, "Calling into custom token validator: {type}" + _customValidator.GetType().FullName, null);
            var customResult = await _customValidator.ValidateIdentityTokenAsync(result);
            if (customResult.IsError)
            {
                await LogError("Custom validator failed: " + (customResult?.Error ?? "unknown"));
                return customResult;
            }
            LogSuccess();
            return customResult;
        }

        //
        // Summary:
        //     Validates an access token.
        //
        // Parameters:
        //   token:
        //     The access token.
        //
        //   expectedScope:
        //     The expected scope.
        public virtual async Task<TokenValidationResult> ValidateAccessTokenAsync(string token, string expectedScope = null)
        {
            this._loggerManager.LogInfo(this._logger, null, "Start access token validation", null);
            TokenValidationResult result;
            if (token.Contains("."))
            {
                if (token.Length > _options.InputLengthRestrictions.Jwt)
                {
                    await this._loggerManager.LogError(this._logger, null, "JWT too long", null);

                    return new TokenValidationResult
                    {
                        IsError = true,
                        Error = OidcConstants.ProtectedResourceErrors.InvalidToken,
                        ErrorDescription = "Token too long"
                    };
                }
                //13920
                //result = await ValidateJwtAsync(
                //    token,
                //    string.Format("{0}resources", _context.HttpContext.GetIdentityServerIssuerUri()),
                //    await _keys.GetValidationKeysAsync());
                var keyinfo = await _keys.GetValidationKeysAsync();
                var keys = keyinfo.Select(s => s.Key);

                result = await ValidateJwtAsync(
                    token,
                    string.Format("{0}resources", _context.HttpContext.GetIdentityServerIssuerUri()),
                    keys);
            }
            else
            {
                if (token.Length > _options.InputLengthRestrictions.TokenHandle)
                {
                    await this._loggerManager.LogError(this._logger, null, "token handle too long", null);

                    return new TokenValidationResult
                    {
                        IsError = true,
                        Error = OidcConstants.ProtectedResourceErrors.InvalidToken,
                        ErrorDescription = "Token too long"
                    };
                }
                result = await ValidateReferenceAccessTokenAsync(token);
            }
            if (result.IsError)
            {
                return result;
            }

            if (!string.IsNullOrWhiteSpace(expectedScope))
            {
                var scope = result.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Scope && c.Value == expectedScope);
                if (scope == null)
                {
                    await LogError(string.Format("Checking for expected scope {0} failed", expectedScope));
                    return Invalid(OidcConstants.ProtectedResourceErrors.InsufficientScope);
                }
            }

            var customResult = await _customValidator.ValidateAccessTokenAsync(result);

            if (customResult.IsError)
            {
                await LogError("Custom validator failed: " + (customResult?.Error ?? "unknown"));
                return customResult;
            }

            // add claims again after custom validation
            await LogSuccess();
            return customResult;
        }

        //
        // Summary:
        //     Validates a refresh token.
        //
        // Parameters:
        //   token:
        //     The refresh token.
        //
        //   client:
        //     The client.
        public async Task<TokenValidationResult> ValidateRefreshTokenAsync(string tokenHandle, Client client = null)
        {
            try
            {
                await this._loggerManager.LogInfo(this._logger, null, "Start refresh token validation", null);

                /////////////////////////////////////////////
                // check if refresh token is valid
                /////////////////////////////////////////////
                var refreshToken = await _refreshTokenStore.GetRefreshTokenAsync(tokenHandle);
                if (refreshToken == null)
                {
                    await this._loggerManager.LogError(this._logger, null, "Invalid refresh token", null);
                    return Invalid(OidcConstants.TokenErrors.InvalidGrant);
                }

                /////////////////////////////////////////////
                // check if refresh token has expired
                /////////////////////////////////////////////
                if (refreshToken.CreationTime.HasExceeded(refreshToken.Lifetime))
                {
                    await this._loggerManager.LogError(this._logger, null, string.Format("Refresh token has expired. Removing from store. REFRESHTK: [{0}]", tokenHandle), null);
                    await _refreshTokenStore.RemoveRefreshTokenAsync(tokenHandle);
                    return Invalid(OidcConstants.TokenErrors.InvalidGrant);
                }

                if (client != null)
                {
                    /////////////////////////////////////////////
                    // check if client belongs to requested refresh token
                    /////////////////////////////////////////////
                    if (client.ClientId != refreshToken.ClientId)
                    {
                        await this._loggerManager.LogError(this._logger, null, "{0} tries to refresh token belonging to {1}" + client.ClientId + refreshToken.ClientId, null);
                        return Invalid(OidcConstants.TokenErrors.InvalidGrant);
                    }

                    /////////////////////////////////////////////
                    // check if client still has offline_access scope
                    /////////////////////////////////////////////
                    if (!client.AllowOfflineAccess)
                    {
                        await this._loggerManager.LogError(this._logger, null, "{clientId} does not have access to offline_access scope anymore" + client.ClientId, null);
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
                    await this._loggerManager.LogError(this._logger, null, "{subjectId} has been disabled" + refreshToken.Subject.GetSubjectId(), null);
                    return Invalid(OidcConstants.TokenErrors.InvalidGrant);
                }

                LogSuccess();

                return new TokenValidationResult
                {
                    IsError = false,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, ex, "Error in validating refresh token " + ex.Message, null);
                return new TokenValidationResult
                {
                    IsError = true,
                    RefreshToken = null
                };
            }
        }

        private async Task<TokenValidationResult> ValidateJwtAsync(string jwt, string audience, IEnumerable<SecurityKey> validationKeys, bool validateLifetime = true)
        {
            var handler = new JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();

            var parameters = new TokenValidationParameters
            {
                ValidIssuer = _context.HttpContext.GetIdentityServerIssuerUri(),
                IssuerSigningKeys = validationKeys,
                ValidateLifetime = validateLifetime,
                ValidAudience = audience
            };

            try
            {
                SecurityToken jwtToken;
                var id = handler.ValidateToken(jwt, parameters, out jwtToken);

                // if access token contains an ID, log it
                var jwtId = id.FindFirst(JwtClaimTypes.JwtId);
                if (jwtId != null)
                {
                }

                // load the client that belongs to the client_id claim
                Client client = null;
                var clientId = id.FindFirst(JwtClaimTypes.ClientId);
                if (clientId != null)
                {
                    client = await _clients.FindEnabledClientByIdAsync(clientId.Value);
                    if (client == null)
                    {
                        throw new InvalidOperationException("Client does not exist anymore.");
                    }
                }

                return new TokenValidationResult
                {
                    IsError = false,

                    Claims = id.Claims,
                    Client = client,
                    Jwt = jwt
                };
            }
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, ex, "JWT token validation error: {exception}" + ex.ToString(), null);
                return Invalid(OidcConstants.ProtectedResourceErrors.InvalidToken);
            }
        }

        private async Task<TokenValidationResult> ValidateReferenceAccessTokenAsync(string tokenHandle)
        {
            var token = await _referenceTokenStore.GetReferenceTokenAsync(tokenHandle);
            if (token == null)
            {
                await this._loggerManager.LogError(this._logger, null, "Token handle not found in token handle store", null);
                return Invalid(OidcConstants.ProtectedResourceErrors.InvalidToken);
            }
            if (DateTime.Now >= token.CreationTime.AddSeconds(token.Lifetime))
            {
                DateTime expiredTime = token.CreationTime.AddSeconds(token.Lifetime);

                await LogError(string.Format("Token expired on {0}. REFERENCEACCESSTK: [{1}]", expiredTime.ToString(), tokenHandle));

                await _referenceTokenStore.RemoveReferenceTokenAsync(tokenHandle);
                return Invalid(OidcConstants.ProtectedResourceErrors.ExpiredToken);
            }

            // load the client that is defined in the token
            Client client = null;
            if (token.ClientId != null)
            {
                client = await _clients.FindEnabledClientByIdAsync(token.ClientId);
            }

            if (client == null)
            {
                await LogError($"Client deleted or disabled: {token.ClientId}");
                return Invalid(OidcConstants.ProtectedResourceErrors.InvalidToken);
            }

            return new TokenValidationResult
            {
                IsError = false,

                Client = client,
                Claims = ReferenceTokenToClaims(token),
                ReferenceToken = token,
                ReferenceTokenId = tokenHandle
            };
        }

        private IEnumerable<Claim> ReferenceTokenToClaims(Token token)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Issuer, token.Issuer),
                new Claim(JwtClaimTypes.NotBefore, token.CreationTime.ToEpochTime().ToString(), ClaimValueTypes.Integer),
                new Claim(JwtClaimTypes.Expiration, token.CreationTime.AddSeconds(token.Lifetime).ToEpochTime().ToString(), ClaimValueTypes.Integer)
            };

            foreach (var aud in token.Audiences)
            {
                claims.Add(new Claim(JwtClaimTypes.Audience, aud));
            }

            claims.AddRange(token.Claims);
            return claims;
        }

        private async Task<string> GetClientIdFromJwt(string token)
        {
            try
            {
                var jwt = new JwtSecurityToken(token);
                var clientId = jwt.Audiences.FirstOrDefault();
                return clientId;
            }
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, ex, "Malformed JWT token: {exception}" + ex.ToString(), null);
                return null;
            }
        }

        private TokenValidationResult Invalid(string error)
        {
            return new TokenValidationResult
            {
                IsError = true,
                Error = error
            };
        }

        private async Task LogError(string message)
        {
            await this._loggerManager.LogError(this._logger, null, message, null);
        }

        private async Task LogSuccess()
        {
            await this._loggerManager.LogDebug(this._logger, null, "Token validation success.", null);
        }
    }
}