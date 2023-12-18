﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.CustomIdentityServices.Secret
{
    public class DefaultSecretParser : ISecretsListParser
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<ISecretParser> _parsers;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretParser"/> class.
        /// </summary>
        /// <param name="parsers">The parsers.</param>
        /// <param name="logger">The logger.</param>
        public DefaultSecretParser(IEnumerable<ISecretParser> parsers, ILogger<ISecretsListParser> logger)
        {
            _parsers = parsers;
            _logger = logger;
        }

        /// <summary>
        /// Checks the context to find a secret.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns></returns>
        public async Task<ParsedSecret> ParseAsync(HttpContext context)
        {
            // see if a registered parser finds a secret on the request
            ParsedSecret bestSecret = null;
            foreach (var parser in _parsers)
            {
                var parsedSecret = await parser.ParseAsync(context);
                if (parsedSecret != null)
                {
                    _logger.LogDebug("Parser found secret: {type}", parser.GetType().Name);

                    bestSecret = parsedSecret;

                    if (parsedSecret.Type != IdentityServerConstants.ParsedSecretTypes.NoSecret)
                    {
                        break;
                    }
                }
            }

            if (bestSecret != null)
            {
                _logger.LogDebug("Secret id found: {id}", bestSecret.Id);
                return bestSecret;
            }

            _logger.LogDebug("Parser found no secret");
            return null;
        }

        /// <summary>
        /// Gets all available authentication methods.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAvailableAuthenticationMethods()
        {
            return _parsers.Select(p => p.AuthenticationMethod).Where(p => !String.IsNullOrWhiteSpace(p));
        }
    }    
}
