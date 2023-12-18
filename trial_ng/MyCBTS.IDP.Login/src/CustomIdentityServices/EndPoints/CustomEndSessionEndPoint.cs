﻿using Duende.IdentityServer.Endpoints.Results;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Hosting;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.Extensions;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.CustomIdentityServices.EndPoints
{
    public class CustomEndSessionEndpoint : IEndpointHandler
    {
        private readonly IEndSessionRequestValidator _endSessionRequestValidator;

        private readonly ILogger _logger;

        private readonly IUserSession _userSession;

        public CustomEndSessionEndpoint(
            IEndSessionRequestValidator endSessionRequestValidator,
            IUserSession userSession,
            ILogger<CustomEndSessionEndpoint> logger)
        {
            _endSessionRequestValidator = endSessionRequestValidator;
            _userSession = userSession;
            _logger = logger;
        }

        public async Task<IEndpointResult> ProcessAsync(HttpContext context)
        {
            NameValueCollection parameters;
            if (HttpMethods.IsGet(context.Request.Method))
            {
                parameters = context.Request.Query.AsNameValueCollection();
            }
            else if (HttpMethods.IsPost(context.Request.Method))
            {
                parameters = (await context.Request.ReadFormAsync()).AsNameValueCollection();
            }
            else
            {
                _logger.LogWarning("Invalid HTTP method for end session endpoint.");
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }

            var user = await _userSession.GetUserAsync();

            _logger.LogDebug("Processing signout request for {subjectId}", user?.GetSubjectId() ?? "anonymous");

            var result = await _endSessionRequestValidator.ValidateAsync(parameters, user);

            if (result.IsError)
            {
                _logger.LogError("Error processing end session request {error}", result.Error);
            }
            else
            {
                context.Response.Redirect(context.GetIdentityServerBaseUrl() + "/account/logout?redirectUrl=" + result.ValidatedRequest.PostLogOutUri);
                _logger.LogDebug("Success validating end session request from {clientId}", result.ValidatedRequest?.Client?.ClientId);
                return null;
            }

            return new EndSessionResult(result);
        }


    }
}
