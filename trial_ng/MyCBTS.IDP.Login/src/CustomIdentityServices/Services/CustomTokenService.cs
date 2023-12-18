using IdentityModel;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
//using Duende.IdentityServer.Stores.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.CustomIdentityServices.Constants;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Models.Response;
using MyCBTS.IDP.Login.Logger;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApiClient = MyCBTS.Api.Client;
using MyCBTS.IDP.Login.CustomIdentityServices.Serialization;

namespace MyCBTS.IDP.Login.CustomIdentityServices.Services
{
    public class CustomTokenService : ITokenService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        protected readonly IHttpContextAccessor _context;

        /// <summary>
        /// The claims provider
        /// </summary>
        protected readonly IClaimsService _claimsProvider;

        /// <summary>
        /// The reference token store
        /// </summary>
        protected readonly IReferenceTokenStore _referenceTokenStore;

        /// <summary>
        /// The signing service
        /// </summary>
        protected readonly ITokenCreationService _creationService;

        /// <summary>
        /// The signing service
        /// </summary>

        private readonly ApiClient.IUserClient _userClient;

        private readonly Api.Client.IIdentityTokenClient _identityTokenClient;

        /// <summary>
        /// The events service
        /// </summary>
        protected readonly IEventService _events;

        private readonly ILoggerManager _loggerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTokenService" /> class. This overloaded constructor is deprecated and will be removed in 3.0.0.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="claimsProvider">The claims provider.</param>
        /// <param name="referenceTokenStore">The reference token store.</param>
        /// <param name="creationService">The signing service.</param>
        /// <param name="events">The events service.</param>
        /// <param name="logger">The logger.</param>
        public CustomTokenService(IHttpContextAccessor context, ApiClient.IUserClient userClient, IClaimsService claimsProvider, IReferenceTokenStore referenceTokenStore, ITokenCreationService creationService, IEventService events, ILogger<CustomTokenService> logger, ILoggerManager loggerManager, Api.Client.IIdentityTokenClient identityTokenClient)
        {
            _logger = logger;
            _context = context;
            _claimsProvider = claimsProvider;
            _referenceTokenStore = referenceTokenStore;
            _creationService = creationService;
            _events = events;
            _identityTokenClient = identityTokenClient;
            _userClient = userClient;
            _loggerManager = loggerManager;
        }

        /// <summary>
        /// Creates an identity token.
        /// </summary>
        /// <param name="request">The token creation request.</param>
        /// <returns>
        /// An identity token
        /// </returns>
        public virtual async Task<Token> CreateIdentityTokenAsync(TokenCreationRequest request)
        {
            try
            {
                await this._loggerManager.LogInfo(this._logger, null, "Creating identity token" + request, null);
                ICollection requestedIdentitySources = request.ValidatedResources.Resources.IdentityResources.ToList();
                // host provided claims
                var claims = new List<Claim>();
                // if nonce was sent, must be mirrored in id token
                if (request.Nonce != null)
                {
                    claims.Add(new Claim(JwtClaimTypes.Nonce, request.Nonce));
                }
                // add iat claim
                claims.Add(new Claim(JwtClaimTypes.IssuedAt, DateTime.UtcNow.ToEpochTime().ToString(), ClaimValueTypes.Integer));
                // add at_hash claim
                if (request.AccessTokenToHash != null)
                {
                    claims.Add(new Claim(JwtClaimTypes.AccessTokenHash, HashAdditionalData(request.AccessTokenToHash)));
                }
                // add c_hash claim
                if (request.AuthorizationCodeToHash != null)
                {
                    claims.Add(new Claim(JwtClaimTypes.AuthorizationCodeHash, HashAdditionalData(request.AuthorizationCodeToHash)));
                }
                // add sid if present
                if (request.ValidatedRequest.SessionId != null)
                {
                    claims.Add(new Claim(JwtClaimTypes.SessionId, request.ValidatedRequest.SessionId));
                }
                if (request.Subject != null)
                {
                    var user = await _userClient.GetUserDetailsByUserIdAsync(Convert.ToInt32(request.Subject.GetSubjectId()));
                    if (user != null)
                    {
                        var defaultAccount = new DefaultAccount()
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserId = user.UserId,
                            DefaultUserAccountId = user?.Accounts.Count > 0 ? user.Accounts.FirstOrDefault().AccountId : 0,
                        };
                        claims.AddRange(AddClaims(defaultAccount, requestedIdentitySources));
                    }
                }
                claims.AddRange(await _claimsProvider.GetIdentityTokenClaimsAsync(
                    request.Subject,
                    // request.ValidatedRequest.Client,
                    request.ValidatedResources,
                    request.IncludeAllIdentityClaims,
                    request.ValidatedRequest));
                var issuer = _context.HttpContext.GetIdentityServerIssuerUri();
                var token = new Token(OidcConstants.TokenTypes.IdentityToken)
                {
                    //Changed request.Client to request.ValidatedRequest.Client
                    Audiences = { request.ValidatedRequest.Client.ClientId },
                    Issuer = issuer,
                    Lifetime = request.ValidatedRequest.Client.IdentityTokenLifetime,
                    Claims = claims.Distinct(new ClaimComparer()).ToList(),
                    ClientId = request.ValidatedRequest.Client.ClientId,
                    AccessTokenType = request.ValidatedRequest.Client.AccessTokenType
                };
                token.CreationTime = DateTime.Now;
                return token;
            }
            catch (ApiClient.ApiException ex)
            {
                var code = ex.StatusCode;
                var message = JsonConvert.DeserializeObject<ErrorDescription>(ex.Response);
                await this._loggerManager.LogError(this._logger, ex, "Error in Creating identity token ", null); throw;
            }
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, ex, "Error in Creating identity token ", null); throw;
            }
        }

        /// <summary>
        /// Creates an access token.
        /// </summary>
        /// <param name="request">The token creation request.</param>
        /// <returns>
        /// An access token
        /// </returns>
        public virtual async Task<Token> CreateAccessTokenAsync(TokenCreationRequest request)
        {
            try
            {
                await this._loggerManager.LogInfo(this._logger, null, "Creating access token" + request, null);
                //  request.ValidatedRequest.
                ICollection requestedIdentitySources = request.ValidatedResources.Resources.IdentityResources.ToList();
                var claims = new List<Claim>();
                claims.AddRange(await _claimsProvider.GetAccessTokenClaimsAsync(
                    request.Subject,
                    //request.ValidatedRequest.Client,
                    request.ValidatedResources,
                    request.ValidatedRequest));
                if (request.Subject != null)
                {
                    var user = await _userClient.GetUserDetailsByUserIdAsync(Convert.ToInt32(request.Subject.GetSubjectId()));
                    if (user != null)
                    {
                        var defaultAccount = new DefaultAccount()
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserId = user.UserId,
                            DefaultUserAccountId = user?.Accounts.Count > 0 ? user.Accounts.FirstOrDefault().AccountId : 0,
                        };

                        claims.AddRange(AddClaims(defaultAccount, requestedIdentitySources));
                    }
                }
                if (request.ValidatedRequest.Client.IncludeJwtId)
                {
                    claims.Add(new Claim(JwtClaimTypes.JwtId, IdentityModel.CryptoRandom.CreateUniqueId()));
                }
                var issuer = _context.HttpContext.GetIdentityServerIssuerUri();
                var token = new Token(OidcConstants.TokenTypes.AccessToken)
                {
                    Audiences = { string.Format("{0}resources", issuer) },
                    Issuer = issuer,
                    Lifetime = request.ValidatedRequest.Client.AccessTokenLifetime,
                    Claims = claims.Distinct(new ClaimComparer()).ToList(),
                    ClientId = request.ValidatedRequest.Client.ClientId,
                    AccessTokenType = request.ValidatedRequest.Client.AccessTokenType,
                    //SubjectId = request.Subject.GetSubjectId()
                };
                foreach (var api in request.ValidatedRequest.ValidatedResources.Resources.ApiResources)//13920
                {
                    if (api.Name != null)
                    {
                        token.Audiences.Add(api.Name);
                    }
                }
                token.CreationTime = DateTime.Now;
                //token.SubjectId = request.Subject.GetSubjectId();
                return token;
            }
            catch (ApiClient.ApiException ex)
            {
                var code = ex.StatusCode;
                var message = JsonConvert.DeserializeObject<ErrorDescription>(ex.Response);
                await this._loggerManager.LogError(this._logger, ex, "Error in Creating access token " + ex.Message, null); throw;
            }
            catch (Exception ex)
            { await this._loggerManager.LogError(this._logger, ex, "Error in Creating access token " + ex.Message, null); throw; }
        }

        private List<Claim> AddClaims(DefaultAccount user, ICollection requestedIdentitySources)
        {
            List<Claim> claims = new List<Claim>();
            try
            {
                AddClaimtoList(ref claims, JwtClaimTypes.Email, Convert.ToString(user.Email));

                foreach (IdentityResource resource in requestedIdentitySources)
                {
                    switch (Convert.ToString(resource.Name).ToLower())
                    {
                        case CustomScopes.Profile:
                            AddClaimtoList(ref claims, JwtClaimTypes.GivenName, Convert.ToString(user.FirstName));
                            AddClaimtoList(ref claims, JwtClaimTypes.FamilyName, Convert.ToString(user.LastName));
                            AddClaimtoList(ref claims, JwtClaimTypes.Email, Convert.ToString(user.Email));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this._loggerManager.LogError(this._logger, null, "Error in Creating access token " + ex.Message, null);
                return claims;
            }
            return claims;
        }

        private void AddClaimtoList(ref List<Claim> claims, string key, string value)
        {
            var claim = claims.Where(c => c.Type.ToLower() == key.ToLower()).ToList();
            if (claim.Count <= 0)
            {
                claims.Add(new Claim(key, value));
            }
        }

        /// <summary>
        /// Creates a serialized and protected security token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A security token in serialized form
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Invalid token type.</exception>
        public virtual async Task<string> CreateSecurityTokenAsync(Token token)
        {
            string tokenResult;

            if (token.Type == OidcConstants.TokenTypes.AccessToken)
            {
                if (token.AccessTokenType == AccessTokenType.Jwt)
                {
                    try
                    {
                        await this._loggerManager.LogInfo(this._logger, null, "Creating JWT access token", null);
                        //tokenResult = await _creationService.CreateTokenAsync(token);
                        //string jsonCode = JsonConvert.SerializeObject(value, GetJsonSerializerSettings());

                        //await _client.CreateJsonTokenAsync(tokenResult, token);
                        string jsonCode = ConvertToJson(token);
                        tokenResult = await _creationService.CreateTokenAsync(token);
                        ApiClient.Token tokenResponse = MapToken(tokenResult, jsonCode, token);
                        await _identityTokenClient.CreateJsonTokenAsync(tokenResponse);
                    }
                    catch (Exception ex)
                    {
                        await this._loggerManager.LogError(this._logger, ex, "Error in Create Security TokenAsync " + ex.Message, null);
                        return null;
                    }
                }
                else
                {
                    await this._loggerManager.LogInfo(this._logger, null, "Creating reference access token", null);
                    string handle = await _referenceTokenStore.StoreReferenceTokenAsync(token);
                    tokenResult = handle;
                }
            }
            else if (token.Type == OidcConstants.TokenTypes.IdentityToken)
            {
                if (token.AccessTokenType == AccessTokenType.Jwt)
                {
                    await this._loggerManager.LogInfo(this._logger, null, "Creating identity token", null);
                    string jsonCode = ConvertToJson(token);
                    tokenResult = await _creationService.CreateTokenAsync(token);
                    ApiClient.Token tokenResponse = MapToken(tokenResult, jsonCode, token);
                    await _identityTokenClient.CreateJsonTokenAsync(tokenResponse);
                }
                else
                {
                    await this._loggerManager.LogInfo(this._logger, null, "Creating reference identity token", null);
                    string handle = await _referenceTokenStore.StoreReferenceTokenAsync(token);
                    tokenResult = handle;
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid token type.");
            }

            await Utility.Logger.Audit(new Models.Api.Audit
            {
                SubjectId = Convert.ToInt32(token.SubjectId),
                ClientId = token.ClientId,
                Step = string.Format("{0}.{1}", AuditConstants.Steps.CreateToken, token.Type),
                Method = string.Format("{0}.{1}", AuditConstants.Steps.CreateToken, token.Type),
                Result = AuditConstants.Result.Success,
                Token = tokenResult,
                TokenType = token.Type,
                TokenStorageType = Convert.ToString(token.AccessTokenType)
            }, token.Claims, _context.HttpContext);

            return tokenResult;
        }

        /// <summary>
        /// Hashes an additional data.
        /// </summary>
        /// <param name="tokenToHash">The token to hash.</param>
        /// <returns></returns>
        protected virtual string HashAdditionalData(string tokenToHash)
        {
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(tokenToHash));

                var leftPart = new byte[16];
                Array.Copy(hash, leftPart, 16);

                return Base64Url.Encode(leftPart);
            }
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
                    SubjectId = token.SubjectId == null ? "0" : token.SubjectId,
                    LifeTime = token.Lifetime
                };
                return apiToken;
            }
            catch (Exception ex)
            { this._loggerManager.LogError(this._logger, null, "Error in mapping token--" + ex.Message, null); throw; }
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClaimConverter());
            settings.Converters.Add(new ClaimsPrincipalConverter());
            return settings;
        }

        protected string ConvertToJson(Token value)
        {
            try
            {
                return JsonConvert.SerializeObject(value, GetJsonSerializerSettings());
            }
            catch (Exception ex) { this._loggerManager.LogError(this._logger, null, "Error in converting to json--" + ex.Message, null); return string.Empty; }
        }
    }
}