using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.CustomIdentityServices.Constants;
using MyCBTS.IDP.Login.Logger;
using System;
using System.Collections.Generic;

namespace MyCBTS.IDP.Login.CustomIdentityServices.EndPoints
{
    public class CustomEndpointRouter : IEndpointRouter
    {
        private readonly IEnumerable<Duende.IdentityServer.Hosting.Endpoint> _endpoints;
        private readonly IdentityServerOptions _options;
        private readonly ILogger _logger;
        private readonly ILoggerManager _loggerManager;

        public CustomEndpointRouter(IEnumerable<Duende.IdentityServer.Hosting.Endpoint> endpoints, IdentityServerOptions options, ILogger<CustomEndpointRouter> logger, ILoggerManager loggerManager)
        {
            _endpoints = endpoints;
            _options = options;
            _logger = logger;
            _loggerManager = loggerManager;
        }

        public IEndpointHandler Find(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            foreach (var endpoint in _endpoints)
            {
                var path = endpoint.Path;

                if (context.Request.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
                {
                    if (path.Equals(IDPConstants.ProtocolRoutePaths.EndSession, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!endpoint.Name.Equals("customendsession", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                    }
                    var endpointName = endpoint.Name;
                    this._loggerManager.LogDebug(this._logger, null, "Request path {path} matched to endpoint type {endpoint}" + context.Request.Path + endpointName, null);

                    return GetEndpointHandler(endpoint, context);
                }
            }
            this._loggerManager.LogInfo(this._logger, null, "No endpoint entry found for request path: {path}" + context.Request.Path, null);
            return null;
        }

        private IEndpointHandler GetEndpointHandler(Duende.IdentityServer.Hosting.Endpoint endpoint, HttpContext context)
        {
            var handler = context.RequestServices.GetService(endpoint.Handler) as IEndpointHandler;
            if (handler != null)
            {
                this._loggerManager.LogDebug(this._logger, null, "Endpoint enabled: {endpoint}, successfully created handler: {endpointHandler}" + endpoint.Name + endpoint.Handler.FullName, null);
                return handler;
            }
            else
            {
                this._loggerManager.LogDebug(this._logger, null, "Endpoint enabled: {endpoint}, failed to create handler: {endpointHandler}" + endpoint.Name + endpoint.Handler.FullName, null);
            }

            return null;
        }
    }
}