using IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.Logger;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.CustomIdentityServices.Validators
{
    public class CustomIntrospectionRequestValidator : IIntrospectionRequestValidator
    {
        private readonly ILogger<IIntrospectionRequestValidator> _logger;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenValidator _tokenValidator;

        public CustomIntrospectionRequestValidator(ITokenValidator tokenValidator, ILogger<IIntrospectionRequestValidator> logger, ILoggerManager loggerManager)
        {
            _tokenValidator = tokenValidator;
            _logger = logger;
            _loggerManager = loggerManager;
        }

        public async Task<IntrospectionRequestValidationResult> ValidateAsync(NameValueCollection parameters, ApiResource apiResource)
        {
            await this._loggerManager.LogDebug(this._logger, null, "Introspection request validation started.", null);
            var fail = new IntrospectionRequestValidationResult { IsError = true };
            // retrieve required token
            var token = parameters.Get("token");
            if (token == null)
            {
                this._loggerManager.LogError(this._logger, null, "Token is missing", null);
                fail.IsActive = false;
                fail.ErrorDescription = IntrospectionRequestValidationFailureReason.MissingToken.ToString();
                return fail;
            }
            // validate token
            var tokenValidationResult = await _tokenValidator.ValidateAccessTokenAsync(token);
            // invalid or unknown token
            if (tokenValidationResult.IsError)
            {
                await this._loggerManager.LogDebug(this._logger, null, "Token is invalid.", null);
                fail.IsActive = false;
                fail.ErrorDescription = IntrospectionRequestValidationFailureReason.MissingToken.ToString();
                fail.Token = token;
                return fail;
            }
            //13920
            // check expected scopes
            //var supportedScopes = apiResource.Scopes.Select(x => x.Name);
            //var expectedScopes = tokenValidationResult.Claims.Where(
            //    c => c.Type == JwtClaimTypes.Scope && supportedScopes.Contains(c.Value));

            var claims = tokenValidationResult.Claims.ToList();
            if (tokenValidationResult.Client.ClientId.ToLower().Contains("onx.sandbox.imp"))
            {
                claims.Add(new Claim("skipcheck", "true"));
            }
            // TODO: filter out the scope claims this API is not allowed to see?

            //foreach (var x in supportedScopes)
            //{
            //    var y = tokenValidationResult.Claims.Where(
            //     c => c.Type == JwtClaimTypes.Scope && x.Contains(c.Value));
            //    var scopecollection = new NameValueCollection().Add(x.ToString(),y.ToString());
            //}

            // all is good
            var success = new IntrospectionRequestValidationResult
            {
                Api = apiResource,
                IsActive = true,
                IsError = false,
                Token = token,
                Claims = claims
            };

            await this._loggerManager.LogDebug(this._logger, null, "Introspection request validation successful.", null);
            return success;
        }
    }
}