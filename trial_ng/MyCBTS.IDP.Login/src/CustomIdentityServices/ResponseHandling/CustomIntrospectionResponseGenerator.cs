using IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.CustomIdentityServices.ResponseHandling
{
    public class CustomIntrospectionResponseGenerator : IIntrospectionResponseGenerator
    {
        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        protected readonly IEventService Events;

        /// <summary>
        /// The _logger
        /// </summary>
        protected readonly ILogger _logger;

        private readonly ILoggerManager _loggerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntrospectionResponseGenerator" /> class.
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="_logger">The this._loggerManager.</param>
        public CustomIntrospectionResponseGenerator(IEventService events, ILogger<IntrospectionResponseGenerator> logger, ILoggerManager loggerManager)
        {
            Events = events;
            _logger = logger;
            _loggerManager = loggerManager;
        }

        public async Task<Dictionary<string, object>> ProcessAsync(IntrospectionRequestValidationResult validationResult, ApiResource apiResource)
        {
            await this._loggerManager.LogInfo(this._logger, null, "Creating introspection response", null);

            if (validationResult.IsActive == false)
            {
                await this._loggerManager.LogDebug(this._logger, null, "Creating introspection response for inactive token.", null);
                var response = new Dictionary<string, object>();
                response.Add("active", false);
                return await Task.FromResult(response);
            }
            else
            {
                await this._loggerManager.LogDebug(this._logger, null, "Creating introspection response for active token.", null);
                var response = validationResult.Claims.Where(c => c.Type != JwtClaimTypes.Scope).ToClaimsDictionary();
                var allowedScopes = validationResult.Api.Scopes;
                var scopes = validationResult.Claims.Where(c => c.Type == JwtClaimTypes.Scope).Select(x => x.Value);
                //scopes = scopes.Where(x => allowedScopes.Contains(x));
                response.Add("scope", scopes.ToArray());
                response.Add("active", true);
                return await Task.FromResult(response);
            }
        }

        public virtual async Task<Dictionary<string, object>> ProcessAsync(IntrospectionRequestValidationResult validationResult)
        {
            this._loggerManager.LogInfo(this._logger, null, "Creating introspection response", null);
            // standard response
            var response = new Dictionary<string, object>
            {
                { "active", false }
            };
            // token is invalid
            if (validationResult.IsActive == false)
            {
                await this._loggerManager.LogDebug(this._logger, null, "Creating introspection response for inactive token.", null);
                //await Events.RaiseAsync(new Duende.IdentityServer.Events.tok TokenIntrospectionSuccessEvent(validationResult));
                return response;
            }
            // expected scope not present
            if (await AreExpectedScopesPresentAsync(validationResult) == false)
            {
                return response;
            }
            await this._loggerManager.LogDebug(this._logger, null, "Creating introspection response for active token.", null);
            response = validationResult.Claims.Where(c => c.Type != JwtClaimTypes.Scope).ToClaimsDictionary();
            // add active flag
            response.Add("active", true);
            // calculate scopes the caller is allowed to see
            var scopes = validationResult.Claims.Where(c => c.Type == JwtClaimTypes.Scope).Select(x => x.Value);
            response.Add("scope", scopes.ToSpaceSeparatedString());
            return response;
        }

        /// <summary>
        /// Checks if the API resource is allowed to introspect the scopes.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <returns></returns>
        protected virtual async Task<bool> AreExpectedScopesPresentAsync(IntrospectionRequestValidationResult validationResult)
        {
            var apiScopes = validationResult.Api.Scopes;
            var tokenScopes = validationResult.Claims.Where(c => c.Type == JwtClaimTypes.Scope);
            var tokenScopesThatMatchApi = tokenScopes.Where(c => apiScopes.Contains(c.Value));
            var result = false;
            var claim = validationResult.Claims.Where(c => c.Type.ToLower() == "skipcheck").ToList();
            if (claim.Count > 0)
            {
                if (claim.FirstOrDefault().Value == "true")
                {
                    result = true;
                }
            }
            else if (tokenScopesThatMatchApi.Any())
            {
                // at least one of the scopes the API supports is in the token
                result = true;
            }
            else
            {
                // no scopes for this API are found in the token
                this._loggerManager.LogError(this._logger, null, "Expected scope {scopes} is missing in token", null);
            }
            return result;
        }
    }
}