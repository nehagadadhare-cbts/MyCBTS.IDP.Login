using IdentityModel;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCBTS.IDP.Login.Configuration;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Mapping;
using MyCBTS.IDP.Login.Logger;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static MyCBTS.IDP.Login.CustomIdentityServices.Constants.UIConstants;

namespace MyCBTS.IDP.Login.CustomIdentityServices.Validators
{
    public class CustomEndsessionRequestValidator : IEndSessionRequestValidator
    {
        private readonly ILogger _logger;
        private readonly IdentityServerOptions _options;
        private readonly ITokenValidator _tokenValidator;
        private readonly IRedirectUriValidator _uriValidator;
        private readonly IUserSession _userSession;
        private readonly IClientStore _clientStore;
        private readonly IHttpContextAccessor _context;
        private readonly MyCBTSDefaultURI _myCBTSDefaultURI;
        private readonly ILoggerManager _loggerManager;

        public CustomEndsessionRequestValidator(
            IHttpContextAccessor context,
            IdentityServerOptions options,
            ITokenValidator tokenValidator,
            IRedirectUriValidator uriValidator,
            IUserSession userSession,
            IClientStore clientStore,
            ILogger<CustomEndsessionRequestValidator> logger,
            IOptions<MyCBTSDefaultURI> myCBTSDefaultURI,
            ILoggerManager loggerManager)
        {
            _context = context;
            _options = options;
            _tokenValidator = tokenValidator;
            _uriValidator = uriValidator;
            _userSession = userSession;
            _clientStore = clientStore;
            _logger = logger;
            _myCBTSDefaultURI = myCBTSDefaultURI.Value;
            _loggerManager = loggerManager;
        }

        public async Task<EndSessionValidationResult> ValidateAsync(NameValueCollection parameters, ClaimsPrincipal subject)
        {
            await this._loggerManager.LogDebug(this._logger, null, "Start end session request validation", null);

            var isAuthenticated = subject.IsAuthenticated();

            if (!isAuthenticated && _options.Authentication.RequireAuthenticatedUserForSignOutMessage)
            {
                return Invalid("User is anonymous. Ignoring end session parameters");
            }

            var validatedRequest = new ValidatedEndSessionRequest
            {
                Raw = parameters
            };

            var idTokenHint = parameters.Get(OidcConstants.EndSessionRequest.IdTokenHint);
            if (!string.IsNullOrEmpty(idTokenHint))
            {
                // validate id_token - no need to validate token life time
                var tokenValidationResult = await _tokenValidator.ValidateIdentityTokenAsync(idTokenHint, null, false);
                if (tokenValidationResult.IsError)
                {
                    return Invalid("Error validating id token hint", validatedRequest);
                }
                validatedRequest.Client = tokenValidationResult.Client;
                // validate sub claim against currently logged on user
                var subClaim = tokenValidationResult.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject);
                if (subClaim != null && isAuthenticated)
                {
                    if (subject.GetSubjectId() != subClaim.Value)
                    {
                        return Invalid("Current user does not match identity token", validatedRequest);
                    }
                    validatedRequest.Subject = subject;
                    validatedRequest.SessionId = await _userSession.GetSessionIdAsync();
                    validatedRequest.ClientIds = await _userSession.GetClientListAsync();
                }
                var redirectUri = parameters.Get(OidcConstants.EndSessionRequest.PostLogoutRedirectUri);
                if (!string.IsNullOrEmpty(redirectUri))
                {
                    if (await _uriValidator.IsPostLogoutRedirectUriValidAsync(redirectUri, validatedRequest.Client) == false)
                    {
                        return Invalid("Invalid post logout URI", validatedRequest);
                    }

                    validatedRequest.PostLogOutUri = redirectUri;
                }
                else if (validatedRequest.Client.PostLogoutRedirectUris.Count == 1)
                {
                    validatedRequest.PostLogOutUri = validatedRequest.Client.PostLogoutRedirectUris.First();
                }

                if (validatedRequest.PostLogOutUri != null)
                {
                    var state = parameters.Get(OidcConstants.EndSessionRequest.State);
                    if (!string.IsNullOrEmpty(state))
                    {
                        validatedRequest.State = state;
                    }
                }
            }
            else
            {
                string redirectUri = parameters[QueryStringParameters.PostLogoutRedirectUri];
                var client = await _clientStore.FindClientByIdAsync(parameters[QueryStringParameters.ClientId]);
                if (client != null)
                {
                    if (!string.IsNullOrEmpty(redirectUri) && client.PostLogoutRedirectUris.Contains(redirectUri.ToLower()))
                    {
                        redirectUri = parameters.Get(OidcConstants.EndSessionRequest.PostLogoutRedirectUri);
                    }
                    else if (client.PostLogoutRedirectUris.Count > 0)
                    {
                        redirectUri = client.PostLogoutRedirectUris.First();
                    }
                    else
                    {
                        redirectUri = _myCBTSDefaultURI.MyAccountUrl;
                    }
                }
                else
                {
                    redirectUri = _myCBTSDefaultURI.MyAccountUrl;
                }

                if (redirectUri.IsPresent())
                {
                    validatedRequest.PostLogOutUri = redirectUri;
                }
                // no id_token to authenticate the client, but we do have a user and a user session
                validatedRequest.Subject = subject;
                validatedRequest.SessionId = await _userSession.GetSessionIdAsync();
                validatedRequest.ClientIds = await _userSession.GetClientListAsync();
            }
            return new EndSessionValidationResult
            {
                ValidatedRequest = validatedRequest,
                IsError = false
            };
        }

        public Task<EndSessionCallbackValidationResult> ValidateCallbackAsync(NameValueCollection parameters)
        {
            throw new NotImplementedException();
        }

        private EndSessionValidationResult Invalid(string message, ValidatedEndSessionRequest request = null)
        {
            message = "End session request validation failure: " + message;
            this._loggerManager.LogInfo(this._logger, null, message, null);
            return new EndSessionValidationResult
            {
                IsError = true,
                Error = "Invalid request",
                ErrorDescription = message
            };
        }
    }
}