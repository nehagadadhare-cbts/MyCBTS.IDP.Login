using IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Logger;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.CustomIdentityServices.Validators
{
    public class CustomTokenRequestValidator : ITokenRequestValidator
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The Server Options
        /// </summary>
        private readonly IdentityServerOptions _options;

        /// <summary>
        /// The authorization Code Store
        /// </summary>
        private readonly IAuthorizationCodeStore _authorizationCodeStore;

        /// <summary>
        /// The Token Store
        /// </summary>
        private readonly IRefreshTokenStore _refreshTokenStore;

        /// <summary>
        /// Grant Validator
        /// </summary>
        private readonly ExtensionGrantValidator _extensionGrantValidator;

        /// <summary>
        /// The custom Request Validator
        /// </summary>
        private readonly ICustomTokenRequestValidator _customRequestValidator;

        /// <summary>
        /// The Scope
        /// </summary>
        private readonly IScopeParser _scopeParser;

        /// <summary>
        /// The events
        /// </summary>
        private readonly IEventService _events;

        /// <summary>
        /// Resource Owner Password Validator
        /// </summary>
        private readonly IResourceOwnerPasswordValidator _resourceOwnerValidator;

        /// <summary>
        /// Profile Service
        /// </summary>
        private readonly IProfileService _profile;

        private readonly IResourceValidator _resourceValidator;

        private ValidatedTokenRequest _validatedRequest;
        private readonly ILoggerManager _loggerManager;

        public CustomTokenRequestValidator(IdentityServerOptions options, IAuthorizationCodeStore authorizationCodeStore, IRefreshTokenStore refreshTokenStore, IResourceOwnerPasswordValidator resourceOwnerValidator, IProfileService profile, ExtensionGrantValidator extensionGrantValidator, ICustomTokenRequestValidator customRequestValidator, IScopeParser scopeParser, IEventService events, ILogger<ITokenRequestValidator> logger, ILoggerManager loggerManager, IResourceValidator resourceValidator)
        {
            _logger = logger;
            _options = options;
            _authorizationCodeStore = authorizationCodeStore;
            _refreshTokenStore = refreshTokenStore;
            _resourceOwnerValidator = resourceOwnerValidator;
            _profile = profile;
            _extensionGrantValidator = extensionGrantValidator;
            _customRequestValidator = customRequestValidator;
            _scopeParser = scopeParser;
            _events = events;
            _loggerManager = loggerManager;
            _resourceValidator = resourceValidator;
        }

        //
        // Summary:
        //     Validates the request.
        //
        // Parameters:
        //   parameters:
        //     The parameters.
        //
        //   clientValidationResult:
        //     The client validation result.
        public async Task<TokenRequestValidationResult> ValidateRequestAsync(NameValueCollection parameters, Client client)
        {
            await this._loggerManager.LogDebug(this._logger, null, "Start token request validation", null);

            if (client == null) throw new ArgumentNullException(nameof(client));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            _validatedRequest = new ValidatedTokenRequest
            {
                Raw = parameters,
                Client = client,
                Options = _options
            };

            /////////////////////////////////////////////
            // check client protocol type
            /////////////////////////////////////////////
            if (client.ProtocolType != IdentityServerConstants.ProtocolTypes.OpenIdConnect)
            {
                //  await LogError("Client {clientId} has invalid protocol type for token endpoint: {protocolType}", client.ClientId, client.ProtocolType);
                return Invalid(OidcConstants.TokenErrors.InvalidClient);
            }

            /////////////////////////////////////////////
            // check grant type
            /////////////////////////////////////////////
            var grantType = parameters.Get(OidcConstants.TokenRequest.GrantType);
            if (grantType.IsMissing())
            {
                await this._loggerManager.LogError(this._logger, null, "Grant type is missing", null);
                return Invalid(OidcConstants.TokenErrors.UnsupportedGrantType);
            }

            if (grantType.Length > _options.InputLengthRestrictions.GrantType)
            {
                //  await LogError("Grant type is too long");
                return Invalid(OidcConstants.TokenErrors.UnsupportedGrantType);
            }

            _validatedRequest.GrantType = grantType;

            switch (grantType)
            {
                case OidcConstants.GrantTypes.AuthorizationCode:
                    return await RunValidationAsync(ValidateAuthorizationCodeRequestAsync, parameters);

                case OidcConstants.GrantTypes.ClientCredentials:
                    return await RunValidationAsync(ValidateClientCredentialsRequestAsync, parameters);

                case OidcConstants.GrantTypes.Password:
                    return await RunValidationAsync(ValidateResourceOwnerCredentialRequestAsync, parameters);

                case OidcConstants.GrantTypes.RefreshToken:
                    return await RunValidationAsync(ValidateRefreshTokenRequestAsync, parameters);

                default:
                    return await RunValidationAsync(ValidateExtensionGrantRequestAsync, parameters);
            }
        }

        private async Task<TokenRequestValidationResult> RunValidationAsync(Func<NameValueCollection, Task<TokenRequestValidationResult>> validationFunc, NameValueCollection parameters)
        {
            // run standard validation
            var result = await validationFunc(parameters);
            if (result.IsError)
            {
                return result;
            }

            // run custom validation

            await this._loggerManager.LogInfo(this._logger, null, "Calling into custom request validator", null);

            var customValidationContext = new CustomTokenRequestValidationContext { Result = result };
            await _customRequestValidator.ValidateAsync(customValidationContext);

            if (customValidationContext.Result.IsError)
            {
                if (customValidationContext.Result.Error.IsPresent())
                {
                    // await LogError("Custom token request validator error {error}", customValidationContext.Result.Error);
                }
                else
                {
                    // await LogError("Custom token request validator error");
                }

                return customValidationContext.Result;
            }

            //LogSuccess();
            return customValidationContext.Result;
        }

        private async Task<TokenRequestValidationResult> ValidateAuthorizationCodeRequestAsync(NameValueCollection parameters)
        {
            await this._loggerManager.LogInfo(this._logger, null, "Start validation of authorization code token request" + parameters, null);

            /////////////////////////////////////////////
            // check if client is authorized for grant type
            /////////////////////////////////////////////
            if (!_validatedRequest.Client.AllowedGrantTypes.ToList().Contains(GrantType.AuthorizationCode) &&
                !_validatedRequest.Client.AllowedGrantTypes.ToList().Contains(GrantType.Hybrid))
            {
                // await LogError("Client not authorized for code flow");
                return Invalid(OidcConstants.TokenErrors.UnauthorizedClient);
            }

            /////////////////////////////////////////////
            // validate authorization code
            /////////////////////////////////////////////
            var code = parameters.Get(OidcConstants.TokenRequest.Code);
            if (code.IsMissing())
            {
                var error = "Authorization code is missing";
                ////await LogError(error);
                RaiseFailedAuthorizationCodeRedeemedEventAsync(null, error);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            if (code.Length > _options.InputLengthRestrictions.AuthorizationCode)
            {
                var error = "Authorization code is too long";
                //await LogError(error);
                RaiseFailedAuthorizationCodeRedeemedEventAsync(null, error);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            _validatedRequest.AuthorizationCodeHandle = code;

            var authZcode = await _authorizationCodeStore.GetAuthorizationCodeAsync(code);
            if (authZcode == null)
            {
                //await LogError("Authorization code cannot be found in the store. CODE: [{code}]", code);
                RaiseFailedAuthorizationCodeRedeemedEventAsync(code, "Invalid handle");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            await _authorizationCodeStore.RemoveAuthorizationCodeAsync(code);

            /////////////////////////////////////////////
            // populate session id
            /////////////////////////////////////////////
            if (authZcode.SessionId.IsPresent())
            {
                _validatedRequest.SessionId = authZcode.SessionId;
            }

            /////////////////////////////////////////////
            // validate client binding
            /////////////////////////////////////////////
            if (authZcode.ClientId != _validatedRequest.Client.ClientId)
            {
                await this._loggerManager.LogError(this._logger, null, "Client  is trying to use a code from another client ", null);
                RaiseFailedAuthorizationCodeRedeemedEventAsync(code, "Invalid client binding");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            /////////////////////////////////////////////
            // validate code expiration
            /////////////////////////////////////////////
            if (authZcode.CreationTime.HasExceeded(_validatedRequest.Client.AuthorizationCodeLifetime))
            {
                var error = "Authorization code is expired";
                //await LogError(error);
                RaiseFailedAuthorizationCodeRedeemedEventAsync(code, error);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            _validatedRequest.AuthorizationCode = authZcode;

            /////////////////////////////////////////////
            // validate redirect_uri
            /////////////////////////////////////////////
            var redirectUri = parameters.Get(OidcConstants.TokenRequest.RedirectUri);
            if (redirectUri.IsMissing())
            {
                var error = "Redirect URI is missing";
                await this._loggerManager.LogError(this._logger, null, error, null);
                RaiseFailedAuthorizationCodeRedeemedEventAsync(code, error);
                return Invalid(OidcConstants.TokenErrors.UnauthorizedClient);
            }

            if (redirectUri.Equals(_validatedRequest.AuthorizationCode.RedirectUri, StringComparison.Ordinal) == false)
            {
                //await LogError("Invalid redirect_uri: {redirectUri}", redirectUri);
                var error = "Invalid redirect_uri: " + redirectUri;
                RaiseFailedAuthorizationCodeRedeemedEventAsync(code, error);
                return Invalid(OidcConstants.TokenErrors.UnauthorizedClient);
            }

            /////////////////////////////////////////////
            // validate scopes are present
            /////////////////////////////////////////////
            if (_validatedRequest.AuthorizationCode.RequestedScopes == null ||
                !_validatedRequest.AuthorizationCode.RequestedScopes.Any())
            {
                var error = "Authorization code has no associated scopes";
                //await LogError(error);
                await RaiseFailedAuthorizationCodeRedeemedEventAsync(code, error);
                return Invalid(OidcConstants.TokenErrors.InvalidRequest);
            }

            /////////////////////////////////////////////
            // validate PKCE parameters
            /////////////////////////////////////////////
            var codeVerifier = parameters.Get(OidcConstants.TokenRequest.CodeVerifier);
            if (_validatedRequest.Client.RequirePkce)
            {
                await this._loggerManager.LogDebug(this._logger, null, "Client required a proof key for code exchange. Starting PKCE validation", null);

                var proofKeyResult = await ValidateAuthorizationCodeWithProofKeyParameters(codeVerifier, _validatedRequest.AuthorizationCode);
                if (proofKeyResult.IsError)
                {
                    return proofKeyResult;
                }

                _validatedRequest.CodeVerifier = codeVerifier;
            }
            else
            {
                if (codeVerifier.IsPresent())
                {
                    await this._loggerManager.LogError(this._logger, null, "Unexpected code_verifier: {codeVerifier}" + codeVerifier, null);
                    return Invalid(OidcConstants.TokenErrors.InvalidGrant);
                }
            }
            /////////////////////////////////////////////
            // make sure user is enabled
            /////////////////////////////////////////////
            var isActiveCtx = new IsActiveContext(_validatedRequest.AuthorizationCode.Subject, _validatedRequest.Client, IdentityServerConstants.ProfileIsActiveCallers.AuthorizationCodeValidation);
            await _profile.IsActiveAsync(isActiveCtx);
            //CBE-13920 - commented out part of IDP v5 upgrade
            //await _scopeValidator.AreScopesValidAsync(_validatedRequest.AuthorizationCode.RequestedScopes);
            //_validatedRequest.ValidatedScopes = _scopeValidator;
            var scopeResponse = _scopeParser.ParseScopeValues(_validatedRequest.AuthorizationCode.RequestedScopes);
            _validatedRequest.ValidatedResources.ParsedScopes = scopeResponse.ParsedScopes;
            if (isActiveCtx.IsActive == false)
            {
                // await LogError("User has been disabled: {subjectId}", _validatedRequest.AuthorizationCode.Subject.GetSubjectId());
                var error = "User has been disabled: " + _validatedRequest.AuthorizationCode.Subject.GetSubjectId();
                RaiseFailedAuthorizationCodeRedeemedEventAsync(code, error);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            await this._loggerManager.LogDebug(this._logger, null, "Validation of authorization code token request success", null);
            RaiseSuccessfulAuthorizationCodeRedeemedEventAsync();

            return Valid();
        }

        private async Task<TokenRequestValidationResult> ValidateClientCredentialsRequestAsync(NameValueCollection parameters)
        {
            await this._loggerManager.LogInfo(this._logger, null, "Start client credentials token request validation", null);

            /////////////////////////////////////////////
            // check if client is authorized for grant type
            /////////////////////////////////////////////
            if (!_validatedRequest.Client.AllowedGrantTypes.ToList().Contains(GrantType.ClientCredentials))
            {
                await this._loggerManager.LogError(this._logger, null, "{clientId} not authorized for client credentials flow" + _validatedRequest.Client.ClientId, null);
                return Invalid(OidcConstants.TokenErrors.UnauthorizedClient);
            }

            /////////////////////////////////////////////
            // check if client is allowed to request scopes
            /////////////////////////////////////////////
            if (!(await ValidateRequestedScopesAsync(parameters)))
            {
                return Invalid(OidcConstants.TokenErrors.InvalidScope);
            }

            //CBE-13920 - commented out part of IDP v5 upgrade
            //if (_validatedRequest.ValidatedScopes.ContainsOfflineAccessScope)
            if (_validatedRequest.ValidatedResources.Resources.OfflineAccess)
            {
                // await LogError("{clientId} cannot request a refresh token in client credentials flow", _validatedRequest.Client.ClientId);
                return Invalid(OidcConstants.TokenErrors.InvalidScope);
            }

            await this._loggerManager.LogDebug(this._logger, null, "{clientId} credentials token request validation success" + _validatedRequest.Client.ClientId, null);
            return Valid();
        }

        private async Task<TokenRequestValidationResult> ValidateResourceOwnerCredentialRequestAsync(NameValueCollection parameters)
        {
            await this._loggerManager.LogInfo(this._logger, null, "Start resource owner password token request validation", null);

            /////////////////////////////////////////////
            // check if client is authorized for grant type
            /////////////////////////////////////////////
            if (!_validatedRequest.Client.AllowedGrantTypes.Contains(GrantType.ResourceOwnerPassword))
            {
                await this._loggerManager.LogError(this._logger, null, "{clientId} not authorized for resource owner flow" + _validatedRequest.Client.ClientId, null);
                return Invalid(OidcConstants.TokenErrors.UnauthorizedClient);
            }

            /////////////////////////////////////////////
            // check if client is allowed to request scopes
            /////////////////////////////////////////////
            if (!(await ValidateRequestedScopesAsync(parameters)))
            {
                return Invalid(OidcConstants.TokenErrors.InvalidScope);
            }

            /////////////////////////////////////////////
            // check resource owner credentials
            /////////////////////////////////////////////
            var userName = parameters.Get(OidcConstants.TokenRequest.UserName);
            var password = parameters.Get(OidcConstants.TokenRequest.Password);

            if (userName.IsMissing() || password.IsMissing())
            {
                //  await LogError("Username or password missing");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            if (userName.Length > _options.InputLengthRestrictions.UserName ||
                password.Length > _options.InputLengthRestrictions.Password)
            {
                // await LogError("Username or password too long");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            _validatedRequest.UserName = userName;

            /////////////////////////////////////////////
            // authenticate user
            /////////////////////////////////////////////
            var resourceOwnerContext = new ResourceOwnerPasswordValidationContext
            {
                UserName = userName,
                Password = password,
                Request = _validatedRequest
            };
            await _resourceOwnerValidator.ValidateAsync(resourceOwnerContext);

            if (resourceOwnerContext.Result.IsError)
            {
                if (resourceOwnerContext.Result.Error == OidcConstants.TokenErrors.UnsupportedGrantType)
                {
                    await this._loggerManager.LogError(this._logger, null, "Resource owner password credential grant type not supported", null);
                    RaiseFailedResourceOwnerAuthenticationEventAsync(userName, "password grant type not supported");
                    return Invalid(OidcConstants.TokenErrors.UnsupportedGrantType, customResponse: resourceOwnerContext.Result.CustomResponse);
                }

                var errorDescription = "invalid_username_or_password";

                if (resourceOwnerContext.Result.ErrorDescription.IsPresent())
                {
                    errorDescription = resourceOwnerContext.Result.ErrorDescription;
                }

                await this._loggerManager.LogError(this._logger, null, "User authentication failed: {error}" + errorDescription ?? resourceOwnerContext.Result.Error, null);
                RaiseFailedResourceOwnerAuthenticationEventAsync(userName, errorDescription);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant, errorDescription, resourceOwnerContext.Result.CustomResponse);
            }

            if (resourceOwnerContext.Result.Subject == null)
            {
                var error = "User authentication failed: no principal returned";
                //await LogError(error);
                RaiseFailedResourceOwnerAuthenticationEventAsync(userName, error);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            /////////////////////////////////////////////
            // make sure user is enabled
            /////////////////////////////////////////////
            var isActiveCtx = new IsActiveContext(resourceOwnerContext.Result.Subject, _validatedRequest.Client, IdentityServerConstants.ProfileIsActiveCallers.ResourceOwnerValidation);
            await _profile.IsActiveAsync(isActiveCtx);
            if (isActiveCtx.IsActive == false)
            {
                //await LogError("User has been disabled: {subjectId}", resourceOwnerContext.Result.Subject.GetSubjectId());
                RaiseFailedResourceOwnerAuthenticationEventAsync(userName, "user is inactive");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }
            _validatedRequest.UserName = userName;
            _validatedRequest.Subject = resourceOwnerContext.Result.Subject;
            RaiseSuccessfulResourceOwnerAuthenticationEventAsync(userName, resourceOwnerContext.Result.Subject.GetSubjectId());
            await this._loggerManager.LogDebug(this._logger, null, "Resource owner password token request validation success.", null);
            return Valid(resourceOwnerContext.Result.CustomResponse);
        }

        private async Task<TokenRequestValidationResult> ValidateRefreshTokenRequestAsync(NameValueCollection parameters)
        {
            await this._loggerManager.LogInfo(this._logger, null, "Start validation of refresh token request", null);
            var refreshTokenHandle = parameters.Get(OidcConstants.TokenRequest.RefreshToken);
            if (refreshTokenHandle.IsMissing())
            {
                var error = "Refresh token is missing";
                //await LogError(error);
                RaiseRefreshTokenRefreshFailureEventAsync(null, error);
                return Invalid(OidcConstants.TokenErrors.InvalidRequest);
            }

            if (refreshTokenHandle.Length > _options.InputLengthRestrictions.RefreshToken)
            {
                var error = "Refresh token too long";
                //await LogError(error);
                RaiseRefreshTokenRefreshFailureEventAsync(null, error);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }
            _validatedRequest.RefreshTokenHandle = refreshTokenHandle;
            /////////////////////////////////////////////
            // check if refresh token is valid
            /////////////////////////////////////////////
            var refreshToken = await _refreshTokenStore.GetRefreshTokenAsync(refreshTokenHandle);
            if (refreshToken == null)
            {
                // await LogError("Refresh token cannot be found in store.  REFRESHTK: [{refreshToken}]", refreshTokenHandle);
                var error = "Refresh token cannot be found in store: " + refreshTokenHandle;
                RaiseRefreshTokenRefreshFailureEventAsync(refreshTokenHandle, error);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }
            /////////////////////////////////////////////
            // check if refresh token has expired
            /////////////////////////////////////////////
            if (refreshToken.CreationTime.HasExceeded(refreshToken.Lifetime))
            {
                var error = "Refresh token has expired";
                //await LogError(error);
                RaiseRefreshTokenRefreshFailureEventAsync(refreshTokenHandle, error);
                await _refreshTokenStore.RemoveRefreshTokenAsync(refreshTokenHandle);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }
            /////////////////////////////////////////////
            // check if client belongs to requested refresh token
            /////////////////////////////////////////////
            if (_validatedRequest.Client.ClientId != refreshToken.ClientId)
            {
                // await LogError("{clientId} tries to refresh token belonging to {clientId}", _validatedRequest.Client.ClientId, refreshToken.ClientId);
                RaiseRefreshTokenRefreshFailureEventAsync(refreshTokenHandle, "Invalid client binding");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }
            /////////////////////////////////////////////
            // check if client still has offline_access scope
            /////////////////////////////////////////////
            if (!_validatedRequest.Client.AllowOfflineAccess)
            {
                // await LogError("{clientId} does not have access to offline_access scope anymore", _validatedRequest.Client.ClientId);
                var error = "Client does not have access to offline_access scope anymore";
                await RaiseRefreshTokenRefreshFailureEventAsync(refreshTokenHandle, error);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            _validatedRequest.RefreshToken = refreshToken;
            /////////////////////////////////////////////
            // make sure user is enabled
            /////////////////////////////////////////////
            var subject = _validatedRequest.RefreshToken.Subject;
            var isActiveCtx = new IsActiveContext(subject, _validatedRequest.Client, IdentityServerConstants.ProfileIsActiveCallers.RefreshTokenValidation);
            await _profile.IsActiveAsync(isActiveCtx);
            if (isActiveCtx.IsActive == false)
            {
                // await LogError("{subjectId} has been disabled", _validatedRequest.RefreshToken.SubjectId);
                var error = "User has been disabled: " + _validatedRequest.RefreshToken.SubjectId;
                RaiseRefreshTokenRefreshFailureEventAsync(refreshTokenHandle, error);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }
            _validatedRequest.Subject = subject;
            //CBE-13920 - commented out part of IDP v5 upgrade
            //await _scopeValidator.AreScopesValidAsync(_validatedRequest.RefreshToken.Scopes);
            //_validatedRequest.ValidatedScopes = _scopeValidator;
            await this._loggerManager.LogDebug(this._logger, null, "Validation of refresh token request success", null);
            return Valid();
        }

        private async Task<TokenRequestValidationResult> ValidateExtensionGrantRequestAsync(NameValueCollection parameters)
        {
            await this._loggerManager.LogDebug(this._logger, null, "Start validation of custom grant token request", null);
            /////////////////////////////////////////////
            // check if client is allowed to use grant type
            /////////////////////////////////////////////
            if (!_validatedRequest.Client.AllowedGrantTypes.Contains(_validatedRequest.GrantType))
            {
                // await LogError("{clientId} does not have the custom grant type in the allowed list, therefore requested grant is not allowed", _validatedRequest.Client.ClientId);
                return Invalid(OidcConstants.TokenErrors.UnsupportedGrantType);
            }
            /////////////////////////////////////////////
            // check if a validator is registered for the grant type
            /////////////////////////////////////////////
            if (!_extensionGrantValidator.GetAvailableGrantTypes().Contains(_validatedRequest.GrantType, StringComparer.Ordinal))
            {
                // await LogError("No validator is registered for the grant type: {grantType}", _validatedRequest.GrantType);
                return Invalid(OidcConstants.TokenErrors.UnsupportedGrantType);
            }
            /////////////////////////////////////////////
            // check if client is allowed to request scopes
            /////////////////////////////////////////////
            if (!(await ValidateRequestedScopesAsync(parameters)))
            {
                return Invalid(OidcConstants.TokenErrors.InvalidScope);
            }
            /////////////////////////////////////////////
            // validate custom grant type
            /////////////////////////////////////////////
            var result = await _extensionGrantValidator.ValidateAsync(_validatedRequest);

            if (result == null)
            {
                // await LogError("Invalid extension grant");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }
            if (result.IsError)
            {
                if (result.Error.IsPresent())
                {
                    // await LogError("Invalid extension grant: {error}", result.Error);
                    return Invalid(result.Error, result.ErrorDescription, result.CustomResponse);
                }
                else
                {
                    // await LogError("Invalid extension grant");
                    return Invalid(OidcConstants.TokenErrors.InvalidGrant, customResponse: result.CustomResponse);
                }
            }

            if (result.Subject != null)
            {
                _validatedRequest.Subject = result.Subject;
            }

            await this._loggerManager.LogDebug(this._logger, null, "Validation of extension grant token request success", null);
            return Valid(result.CustomResponse);
        }

        private async Task<bool> ValidateRequestedScopesAsync(NameValueCollection parameters)
        {
            var scopes = parameters?.Get(OidcConstants.TokenRequest.Scope);

            if (scopes.IsMissing())
            {
                await this._loggerManager.LogInfo(this._logger, null, "Client provided no scopes - checking allowed scopes list", null);

                var clientAllowedScopes = _validatedRequest.Client.AllowedScopes;
                if (!clientAllowedScopes.IsNullOrEmpty())
                {
                    if (_validatedRequest.Client.AllowOfflineAccess)
                    {
                        clientAllowedScopes.Add(IdentityServerConstants.StandardScopes.OfflineAccess);
                    }
                    scopes = clientAllowedScopes.ToSpaceSeparatedString();
                    await this._loggerManager.LogInfo(this._logger, null, "Defaulting to: {scopes}" + scopes, null);
                }
                else
                {
                    // await LogError("No allowed scopes configured for {clientId}", _validatedRequest.Client.ClientId);
                    return false;
                }
            }

            if (scopes?.Length > _options?.InputLengthRestrictions?.Scope)
            {
                await LogError("Scope parameter exceeds max allowed length");
                return false;
            }

            var requestedScopes = scopes.ParseScopesString();

            if (requestedScopes == null)
            {
                await LogError("No scopes found in request");
                return false;
            }

            //CBE-13920 - commented out part of IDP v5 upgrade
            //if (!(await _scopeValidator.AreScopesAllowedAsync(_validatedRequest.Client, requestedScopes)))
            //{
            //    // await LogError();
            //    return false;
            //}

            //if (!(await _scopeValidator.AreScopesValidAsync(requestedScopes)))
            //{
            //    //  await LogError();
            //    return false;
            //}
            var resourceValidationResult = await _resourceValidator.ValidateRequestedResourcesAsync(new ResourceValidationRequest
            {
                Client = _validatedRequest.Client,
                Scopes = requestedScopes
            });

            _validatedRequest.RequestedScopes = requestedScopes;
            _validatedRequest.ValidatedResources = resourceValidationResult;
            return true;
        }

        private async Task<TokenRequestValidationResult> ValidateAuthorizationCodeWithProofKeyParameters(string codeVerifier, AuthorizationCode authZcode)
        {
            if (authZcode.CodeChallenge.IsMissing() || authZcode.CodeChallengeMethod.IsMissing())
            {
                //await LogError("{clientId} is missing code challenge or code challenge method", _validatedRequest.Client.ClientId);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            if (codeVerifier.IsMissing())
            {
                // await LogError("Missing code_verifier");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }
            if (codeVerifier.Length < _options.InputLengthRestrictions.CodeVerifierMinLength ||
                codeVerifier.Length > _options.InputLengthRestrictions.CodeVerifierMaxLength)
            {
                // await LogError("code_verifier is too short or too long");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            if (SupportedCodeChallengeMethods.Contains(authZcode.CodeChallengeMethod) == false)
            {
                // await LogError("Unsupported code challenge method: {codeChallengeMethod}", authZcode.CodeChallengeMethod);
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }

            if (ValidateCodeVerifierAgainstCodeChallenge(codeVerifier, authZcode.CodeChallenge, authZcode.CodeChallengeMethod) == false)
            {
                // await LogError("Transformed code verifier does not match code challenge");
                return Invalid(OidcConstants.TokenErrors.InvalidGrant);
            }
            return Valid();
        }

        private bool ValidateCodeVerifierAgainstCodeChallenge(string codeVerifier, string codeChallenge, string codeChallengeMethod)
        {
            if (codeChallengeMethod == OidcConstants.CodeChallengeMethods.Plain)
            {
                return TimeConstantComparer.IsEqual(codeVerifier?.Sha256(), codeChallenge);
            }
            var codeVerifierBytes = Encoding.ASCII.GetBytes(codeVerifier);
            var hashedBytes = codeVerifierBytes?.Sha256();
            var transformedCodeVerifier = Base64Url.Encode(hashedBytes);
            return TimeConstantComparer.IsEqual(transformedCodeVerifier?.Sha256(), codeChallenge);
        }

        private TokenRequestValidationResult Valid(Dictionary<string, object> customResponse = null)
        {
            return new TokenRequestValidationResult(_validatedRequest, customResponse);
        }

        private TokenRequestValidationResult Invalid(string error, string errorDescription = null, Dictionary<string, object> customResponse = null)
        {
            return new TokenRequestValidationResult(_validatedRequest, error, errorDescription, customResponse);
        }

        private async Task LogError(string message = null, params object[] values)
        {
            if (message.IsPresent())
            {
                try
                {
                    await this._loggerManager.LogError(this._logger, null, message + values, null);
                }
                catch (Exception ex)
                {
                    await this._loggerManager.LogError(this._logger, null, "Error logging {exception}" + ex.Message, null);
                }
            }
        }

        private async Task LogSuccess()
        {
            try
            {
                await this._loggerManager.LogInfo(this._logger, null, "Token request validation success\n{details}" + _validatedRequest.Client + _validatedRequest.Subject.GetSubjectId(), null);
            }
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, null, "Error in Creating refresh token " + ex.Message, null);
                // return null;
            }
        }

        private async Task RaiseSuccessfulResourceOwnerAuthenticationEventAsync(string userName, string subjectId)
        {
            await this._loggerManager.LogInfo(this._logger, null, "Resource owner authentication successful/n username:{0},subjectid:{1}" + userName + subjectId, null);
        }

        private async Task RaiseFailedResourceOwnerAuthenticationEventAsync(string userName, string error)
        {
            await this._loggerManager.LogError(this._logger, null, "Resource owner authentication failed/n username:{0},error:{1}" + userName + error, null);
        }

        private async Task RaiseFailedAuthorizationCodeRedeemedEventAsync(string handle, string error)
        {
            await this._loggerManager.LogError(this._logger, null, "Authorization code redemption failed/n code:{0},error:{1}" + handle + error, null);
        }

        private async Task RaiseSuccessfulAuthorizationCodeRedeemedEventAsync()
        {
            await this._loggerManager.LogInfo(this._logger, null, "Authorization code redeeemed", null);
        }

        private async Task RaiseRefreshTokenRefreshFailureEventAsync(string handle, string error)
        {
            await this._loggerManager.LogError(this._logger, null, "Refresh token refresh failed/n code:{0},error:{1}" + handle + error, null);
        }

        public async Task<TokenRequestValidationResult> ValidateRequestAsync(NameValueCollection parameters, ClientSecretValidationResult clientValidationResult)
        {
            await this._loggerManager.LogDebug(this._logger, null, "Start token request validation", null);
            _validatedRequest = new ValidatedTokenRequest
            {
                Raw = parameters ?? throw new ArgumentNullException(nameof(parameters)),
                Options = _options
            };

            if (clientValidationResult == null) throw new ArgumentNullException(nameof(clientValidationResult));
            _validatedRequest.SetClient(clientValidationResult.Client, clientValidationResult.Secret);
            /////////////////////////////////////////////
            // check client protocol type
            /////////////////////////////////////////////
            if (_validatedRequest.Client.ProtocolType != IdentityServerConstants.ProtocolTypes.OpenIdConnect)
            {
                // await LogError("Client {clientId} has invalid protocol type for token endpoint: expected {expectedProtocolType} but found {protocolType}",
                //_validatedRequest.Client.ClientId,
                //IdentityServerConstants.ProtocolTypes.OpenIdConnect,
                //_validatedRequest.Client.ProtocolType);
                return Invalid(OidcConstants.TokenErrors.InvalidClient);
            }

            /////////////////////////////////////////////
            // check grant type
            /////////////////////////////////////////////
            var grantType = parameters.Get(OidcConstants.TokenRequest.GrantType);
            if (grantType.IsMissing())
            {
                //await LogError("Grant type is missing");
                return Invalid(OidcConstants.TokenErrors.UnsupportedGrantType);
            }

            if (grantType.Length > _options.InputLengthRestrictions.GrantType)
            {
                // await LogError("Grant type is too long");
                return Invalid(OidcConstants.TokenErrors.UnsupportedGrantType);
            }

            _validatedRequest.GrantType = grantType;

            switch (grantType)
            {
                case OidcConstants.GrantTypes.AuthorizationCode:
                    return await RunValidationAsync(ValidateAuthorizationCodeRequestAsync, parameters);

                case OidcConstants.GrantTypes.ClientCredentials:
                    return await RunValidationAsync(ValidateClientCredentialsRequestAsync, parameters);

                case OidcConstants.GrantTypes.Password:
                    return await RunValidationAsync(ValidateResourceOwnerCredentialRequestAsync, parameters);

                case OidcConstants.GrantTypes.RefreshToken:
                    return await RunValidationAsync(ValidateRefreshTokenRequestAsync, parameters);

                default:
                    return await RunValidationAsync(ValidateExtensionGrantRequestAsync, parameters);
            }
        }

        public static readonly List<string> SupportedCodeChallengeMethods = new List<string>
        {
            OidcConstants.CodeChallengeMethods.Plain,
            OidcConstants.CodeChallengeMethods.Sha256
        };
    }
}