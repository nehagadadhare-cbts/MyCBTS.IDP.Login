using IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyCBTS.IDP.Login.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityModels = Duende.IdentityServer.Models;

namespace MyCBTS.IDP.Login.CustomIdentityServices.ResponseHandling
{
    public class CustomDiscoveryResponseGenerator : IDiscoveryResponseGenerator
    {
        /// <summary>
        /// The options
        /// </summary>
        protected readonly IdentityServerOptions Options;

        /// <summary>
        /// The extension grants validator
        /// </summary>
        protected readonly ExtensionGrantValidator ExtensionGrants;

        /// <summary>
        /// The key material service
        /// </summary>
        protected readonly IKeyMaterialService Keys;

        /// <summary>
        /// The resource owner validator
        /// </summary>
        protected readonly IResourceOwnerPasswordValidator ResourceOwnerValidator;

        /// <summary>
        /// The resource store
        /// </summary>
        protected readonly IResourceStore ResourceStore;

        /// <summary>
        /// The secret parsers
        /// </summary>
        protected readonly ISecretsListParser SecretParsers;

        /// <summary>
        /// The _logger
        /// </summary>
        protected readonly ILogger _logger;

        private readonly ILoggerManager _loggerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDiscoveryResponseGenerator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="resourceStore">The resource store.</param>
        /// <param name="keys">The keys.</param>
        /// <param name="extensionGrants">The extension grants.</param>
        /// <param name="secretParsers">The secret parsers.</param>
        /// <param name="resourceOwnerValidator">The resource owner validator.</param>
        /// <param name="_logger">The this._logger</param>
        public CustomDiscoveryResponseGenerator(
            IdentityServerOptions options,
            IResourceStore resourceStore,
            IKeyMaterialService keys,
            ExtensionGrantValidator extensionGrants,
            ISecretsListParser secretParsers,
            IResourceOwnerPasswordValidator resourceOwnerValidator,
            ILogger<CustomDiscoveryResponseGenerator> logger,
             ILoggerManager loggerManager)
        {
            Options = options;
            ResourceStore = resourceStore;
            Keys = keys;
            ExtensionGrants = extensionGrants;
            SecretParsers = secretParsers;
            ResourceOwnerValidator = resourceOwnerValidator;
            _logger = logger;
            _loggerManager = loggerManager;
        }

        /// <summary>
        /// Creates the discovery document.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="issuerUri">The issuer URI.</param>
        public virtual async Task<Dictionary<string, object>> CreateDiscoveryDocumentAsync(string baseUrl, string issuerUri)
        {
            await this._loggerManager.LogDebug(this._logger, null, "Creating Discovery DocumentAsync", null);
            var entries = new Dictionary<string, object>
            {
                { OidcConstants.Discovery.Issuer, issuerUri }
            };

            // jwks
            if (Options.Discovery.ShowKeySet)
            {
                if ((await Keys.GetValidationKeysAsync()).Any())
                {
                    entries.Add(OidcConstants.Discovery.JwksUri, baseUrl + Constants.Constants.ProtocolRoutePaths.DiscoveryWebKeys);
                }
            }

            // endpoints
            if (Options.Discovery.ShowEndpoints)
            {
                if (Options.Endpoints.EnableAuthorizeEndpoint)
                {
                    entries.Add(OidcConstants.Discovery.AuthorizationEndpoint, baseUrl + Constants.Constants.ProtocolRoutePaths.Authorize);
                }

                if (Options.Endpoints.EnableTokenEndpoint)
                {
                    entries.Add(OidcConstants.Discovery.TokenEndpoint, baseUrl + Constants.Constants.ProtocolRoutePaths.Token);
                }

                if (Options.Endpoints.EnableUserInfoEndpoint)
                {
                    entries.Add(OidcConstants.Discovery.UserInfoEndpoint, baseUrl + Constants.Constants.ProtocolRoutePaths.UserInfo);
                }

                if (Options.Endpoints.EnableEndSessionEndpoint)
                {
                    entries.Add(OidcConstants.Discovery.EndSessionEndpoint, baseUrl + Constants.Constants.ProtocolRoutePaths.EndSession);
                }

                if (Options.Endpoints.EnableCheckSessionEndpoint)
                {
                    entries.Add(OidcConstants.Discovery.CheckSessionIframe, baseUrl + Constants.Constants.ProtocolRoutePaths.CheckSession);
                }

                if (Options.Endpoints.EnableTokenRevocationEndpoint)
                {
                    entries.Add(OidcConstants.Discovery.RevocationEndpoint, baseUrl + Constants.Constants.ProtocolRoutePaths.Revocation);
                }

                if (Options.Endpoints.EnableIntrospectionEndpoint)
                {
                    entries.Add(OidcConstants.Discovery.IntrospectionEndpoint, baseUrl + Constants.Constants.ProtocolRoutePaths.Introspection);
                }
            }

            // logout
            if (Options.Endpoints.EnableEndSessionEndpoint)
            {
                entries.Add(OidcConstants.Discovery.FrontChannelLogoutSupported, true);
                entries.Add(OidcConstants.Discovery.FrontChannelLogoutSessionSupported, true);
                entries.Add(OidcConstants.Discovery.BackChannelLogoutSupported, true);
                entries.Add(OidcConstants.Discovery.BackChannelLogoutSessionSupported, true);
            }

            // scopes and claims
            if (Options.Discovery.ShowIdentityScopes ||
                Options.Discovery.ShowApiScopes ||
                Options.Discovery.ShowClaims)
            {
                var resources = await ResourceStore.GetAllResourcesAsync();
                var scopes = new List<string>();

                // scopes
                if (Options.Discovery.ShowIdentityScopes)
                {
                    scopes.AddRange(resources.IdentityResources.Where(x => x.ShowInDiscoveryDocument).Select(x => x.Name));
                }

                if (Options.Discovery.ShowApiScopes)
                {
                    var apiScopes = from api in resources.ApiScopes
                                    where api.ShowInDiscoveryDocument
                                    select api.Name;

                    scopes.AddRange(apiScopes);
                    scopes.Add(IdentityServerConstants.StandardScopes.OfflineAccess);
                }

                if (scopes.Any())
                {
                    entries.Add(OidcConstants.Discovery.ScopesSupported, scopes.ToArray());
                }

                //CBE-13919 - commented out part of IDP v5 upgrade 
                // claims
                //if (Options.Discovery.ShowClaims)
                //{
                //    var claims = new List<string>();

                //    // add non-hidden identity scopes related claims
                //    claims.AddRange(resources.IdentityResources.Where(x => x.ShowInDiscoveryDocument).SelectMany(x => x.UserClaims));

                //    // add non-hidden api scopes related claims
                //    foreach (var resource in resources.ApiResources)
                //    {
                //        claims.AddRange(resource.UserClaims);

                //        foreach (var scope in resource.Scopes)
                //        {
                //            if (scope.ShowInDiscoveryDocument)
                //            {
                //                claims.AddRange(scope.UserClaims);
                //            }
                //        }
                //    }
                //    entries.Add(OidcConstants.Discovery.ClaimsSupported, claims.Distinct().ToArray());
                //}

                // claims
                if (Options.Discovery.ShowClaims)
                {
                    var claims = new List<string>();

                    // add non-hidden identity scopes related claims
                    claims.AddRange(resources.IdentityResources.Where(x => x.ShowInDiscoveryDocument).SelectMany(x => x.UserClaims));
                    claims.AddRange(resources.ApiResources.Where(x => x.ShowInDiscoveryDocument).SelectMany(x => x.UserClaims));
                    claims.AddRange(resources.ApiScopes.Where(x => x.ShowInDiscoveryDocument).SelectMany(x => x.UserClaims));
                    entries.Add(OidcConstants.Discovery.ClaimsSupported, claims.Distinct().ToArray());
                }

            }
            // grant types
            if (Options.Discovery.ShowGrantTypes)
            {
                var standardGrantTypes = new List<string>
                {
                    OidcConstants.GrantTypes.AuthorizationCode,
                    OidcConstants.GrantTypes.ClientCredentials,
                    OidcConstants.GrantTypes.RefreshToken,
                    OidcConstants.GrantTypes.Implicit
                };

                if (!(ResourceOwnerValidator is NotSupportedResourceOwnerPasswordValidator))
                {
                    standardGrantTypes.Add(OidcConstants.GrantTypes.Password);
                }

                var showGrantTypes = new List<string>(standardGrantTypes);

                if (Options.Discovery.ShowExtensionGrantTypes)
                {
                    showGrantTypes.AddRange(ExtensionGrants.GetAvailableGrantTypes());
                }

                entries.Add(OidcConstants.Discovery.GrantTypesSupported, showGrantTypes.ToArray());
            }
            // response types
            if (Options.Discovery.ShowResponseTypes)
            {
                entries.Add(OidcConstants.Discovery.ResponseTypesSupported, Constants.Constants.SupportedResponseTypes.ToArray());
            }
            // response modes
            if (Options.Discovery.ShowResponseModes)
            {
                entries.Add(OidcConstants.Discovery.ResponseModesSupported, Constants.Constants.SupportedResponseModes.ToArray());
            }
            // misc
            if (Options.Discovery.ShowTokenEndpointAuthenticationMethods)
            {
                entries.Add(OidcConstants.Discovery.TokenEndpointAuthenticationMethodsSupported, SecretParsers.GetAvailableAuthenticationMethods().ToArray());
            }
            entries.Add(OidcConstants.Discovery.SubjectTypesSupported, new[] { "public" });
            entries.Add(OidcConstants.Discovery.IdTokenSigningAlgorithmsSupported, new[] { Constants.Constants.SigningAlgorithms.RSA_SHA_256 });
            entries.Add(OidcConstants.Discovery.CodeChallengeMethodsSupported, new[] { OidcConstants.CodeChallengeMethods.Plain, OidcConstants.CodeChallengeMethods.Sha256 });
            // custom entries
            if (!Options.Discovery.CustomEntries.IsNullOrEmpty())
            {
                foreach (var customEntry in Options.Discovery.CustomEntries)
                {
                    if (entries.ContainsKey(customEntry.Key))
                    {
                        this._loggerManager.LogError(this._logger, null, "Discovery custom entry {key} cannot be added, because it already exists." + customEntry.Key, null);
                    }
                    else
                    {
                        if (customEntry.Value is string customValueString)
                        {
                            if (customValueString.StartsWith("~/") && Options.Discovery.ExpandRelativePathsInCustomEntries)
                            {
                                entries.Add(customEntry.Key, baseUrl + customValueString.Substring(2));
                                continue;
                            }
                        }
                        entries.Add(customEntry.Key, customEntry.Value);
                    }
                }
            }
            return entries;
        }

        /// <summary>
        /// Creates the JWK document.
        /// </summary>
        public virtual async Task<IEnumerable<IdentityModels.JsonWebKey>> CreateJwkDocumentAsync()
        {
            this._loggerManager.LogInfo(this._logger, null, "Creating Jwk DocumentAsync", null);
            var webKeys = new List<IdentityModels.JsonWebKey>();
            var signingCredentials = await Keys.GetSigningCredentialsAsync();
            var algorithm = signingCredentials?.Algorithm ?? Constants.Constants.SigningAlgorithms.RSA_SHA_256;

            foreach (var key in await Keys.GetValidationKeysAsync())
            {
                if (key.Key is X509SecurityKey x509Key)
                {
                    var cert64 = Convert.ToBase64String(x509Key.Certificate.RawData);
                    var thumbprint = Base64Url.Encode(x509Key.Certificate.GetCertHash());
                    var pubKey = x509Key.PublicKey as RSA;
                    var parameters = pubKey.ExportParameters(false);
                    var exponent = Base64Url.Encode(parameters.Exponent);
                    var modulus = Base64Url.Encode(parameters.Modulus);

                    var webKey = new IdentityModels.JsonWebKey
                    {
                        kty = "RSA",
                        use = "sig",
                        kid = x509Key.KeyId,
                        x5t = thumbprint,
                        e = exponent,
                        n = modulus,
                        x5c = new[] { cert64 },
                        alg = algorithm
                    };
                    webKeys.Add(webKey);
                    continue;
                }
                if (key.Key is RsaSecurityKey rsaKey)
                {
                    var parameters = rsaKey.Rsa?.ExportParameters(false) ?? rsaKey.Parameters;
                    var exponent = Base64Url.Encode(parameters.Exponent);
                    var modulus = Base64Url.Encode(parameters.Modulus);
                    var webKey = new IdentityModels.JsonWebKey
                    {
                        kty = "RSA",
                        use = "sig",
                        kid = rsaKey.KeyId,
                        e = exponent,
                        n = modulus,
                        alg = algorithm
                    };
                    webKeys.Add(webKey);
                }
            }
            return webKeys;
        }
    }
}