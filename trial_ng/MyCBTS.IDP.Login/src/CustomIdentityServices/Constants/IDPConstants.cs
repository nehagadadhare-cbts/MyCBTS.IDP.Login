using IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using Duende.IdentityServer.Hosting;

namespace MyCBTS.IDP.Login.CustomIdentityServices.Constants
{
    public static class IDPConstants
    {
        public const string IdentityServerName = "Duende.IdentityServer";
        public const string IdentityServerAuthenticationType = IdentityServerName;
        public const string ExternalAuthenticationMethod = "external";
        public const string AccessTokenAudience = "{0}resources";
        public const string DefaultHashAlgorithm = "SHA256";

        public static readonly TimeSpan DefaultCookieTimeSpan = TimeSpan.FromHours(10);
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromMinutes(5);

        public static readonly List<string> SupportedResponseTypes = new List<string>
        {
            OidcConstants.ResponseTypes.Code,
            OidcConstants.ResponseTypes.Token,
            OidcConstants.ResponseTypes.IdToken,
            OidcConstants.ResponseTypes.IdTokenToken,
            OidcConstants.ResponseTypes.CodeIdToken,
            OidcConstants.ResponseTypes.CodeToken,
            OidcConstants.ResponseTypes.CodeIdTokenToken
        };

        public static readonly Dictionary<string, string> ResponseTypeToGrantTypeMapping = new Dictionary<string, string>
        {
            { OidcConstants.ResponseTypes.Code, GrantType.AuthorizationCode },
            { OidcConstants.ResponseTypes.Token, GrantType.Implicit },
            { OidcConstants.ResponseTypes.IdToken, GrantType.Implicit },
            { OidcConstants.ResponseTypes.IdTokenToken, GrantType.Implicit },
            { OidcConstants.ResponseTypes.CodeIdToken, GrantType.Hybrid },
            { OidcConstants.ResponseTypes.CodeToken, GrantType.Hybrid },
            { OidcConstants.ResponseTypes.CodeIdTokenToken, GrantType.Hybrid }
        };

        public static readonly List<string> AllowedGrantTypesForAuthorizeEndpoint = new List<string>
        {
            GrantType.AuthorizationCode,
            GrantType.Implicit,
            GrantType.Hybrid
        };

        public static readonly List<string> SupportedCodeChallengeMethods = new List<string>
        {
            OidcConstants.CodeChallengeMethods.Plain,
            OidcConstants.CodeChallengeMethods.Sha256
        };

        public enum ScopeRequirement
        {
            None,
            ResourceOnly,
            IdentityOnly,
            Identity
        }

        public static readonly Dictionary<string, ScopeRequirement> ResponseTypeToScopeRequirement = new Dictionary<string, ScopeRequirement>
        {
            { OidcConstants.ResponseTypes.Code, ScopeRequirement.None },
            { OidcConstants.ResponseTypes.Token, ScopeRequirement.ResourceOnly },
            { OidcConstants.ResponseTypes.IdToken, ScopeRequirement.IdentityOnly },
            { OidcConstants.ResponseTypes.IdTokenToken, ScopeRequirement.Identity },
            { OidcConstants.ResponseTypes.CodeIdToken, ScopeRequirement.Identity },
            { OidcConstants.ResponseTypes.CodeToken, ScopeRequirement.Identity },
            { OidcConstants.ResponseTypes.CodeIdTokenToken, ScopeRequirement.Identity }
        };

        public static readonly Dictionary<string, IEnumerable<string>> AllowedResponseModesForGrantType = new Dictionary<string, IEnumerable<string>>
        {
            { GrantType.AuthorizationCode, new[] { OidcConstants.ResponseModes.Query, OidcConstants.ResponseModes.FormPost } },
            { GrantType.Hybrid, new[] { OidcConstants.ResponseModes.Fragment, OidcConstants.ResponseModes.FormPost }},
            { GrantType.Implicit, new[] { OidcConstants.ResponseModes.Fragment, OidcConstants.ResponseModes.FormPost }},
        };

        public static readonly List<string> SupportedResponseModes = new List<string>
        {
            OidcConstants.ResponseModes.FormPost,
            OidcConstants.ResponseModes.Query,
            OidcConstants.ResponseModes.Fragment,
        };

        public static string[] SupportedSubjectTypes =
        {
            "pairwise", "public"
        };

        public static class SigningAlgorithms
        {
            public const string RSA_SHA_256 = "RS256";
        }

        public static readonly List<string> SupportedDisplayModes = new List<string>
        {
            OidcConstants.DisplayModes.Page,
            OidcConstants.DisplayModes.Popup,
            OidcConstants.DisplayModes.Touch,
            OidcConstants.DisplayModes.Wap,
        };

        public static readonly List<string> SupportedPromptModes = new List<string>
        {
            OidcConstants.PromptModes.None,
            OidcConstants.PromptModes.Login,
            OidcConstants.PromptModes.Consent,
            OidcConstants.PromptModes.SelectAccount,
        };

        public static class KnownAcrValues
        {
            public const string HomeRealm = "idp:";
            public const string Tenant = "tenant:";

            public static readonly string[] All = { HomeRealm, Tenant };
        }

        public static Dictionary<string, int> ProtectedResourceErrorStatusCodes = new Dictionary<string, int>
        {
            { OidcConstants.ProtectedResourceErrors.InvalidToken,      401 },
            { OidcConstants.ProtectedResourceErrors.ExpiredToken,      401 },
            { OidcConstants.ProtectedResourceErrors.InvalidRequest,    400 },
            { OidcConstants.ProtectedResourceErrors.InsufficientScope, 403 },
        };

        public static readonly Dictionary<string, IEnumerable<string>> ScopeToClaimsMapping = new Dictionary<string, IEnumerable<string>>
        {
            { IdentityServerConstants.StandardScopes.Profile, new[]
                            {
                                JwtClaimTypes.Name,
                                JwtClaimTypes.FamilyName,
                                JwtClaimTypes.GivenName,
                                JwtClaimTypes.MiddleName,
                                JwtClaimTypes.NickName,
                                JwtClaimTypes.PreferredUserName,
                                JwtClaimTypes.Profile,
                                JwtClaimTypes.Picture,
                                JwtClaimTypes.WebSite,
                                JwtClaimTypes.Gender,
                                JwtClaimTypes.BirthDate,
                                JwtClaimTypes.ZoneInfo,
                                JwtClaimTypes.Locale,
                                JwtClaimTypes.UpdatedAt
                            }},
            { IdentityServerConstants.StandardScopes.Email, new[]
                            {
                                JwtClaimTypes.Email,
                                JwtClaimTypes.EmailVerified
                            }},
            { IdentityServerConstants.StandardScopes.Address, new[]
                            {
                                JwtClaimTypes.Address
                            }},
            { IdentityServerConstants.StandardScopes.Phone, new[]
                            {
                                JwtClaimTypes.PhoneNumber,
                                JwtClaimTypes.PhoneNumberVerified
                            }},
            { IdentityServerConstants.StandardScopes.OpenId, new[]
                            {
                                JwtClaimTypes.Subject
                            }},
        };

        public static class ProtocolRoutePaths
        {
            public const string Authorize = "connect/authorize";
            public const string AuthorizeAfterConsent = Authorize + "/consent/Index";
            public const string AuthorizeAfterLogin = Authorize + "/login";
            public const string DiscoveryConfiguration = ".well-known/openid-configuration";
            public const string DiscoveryWebKeys = DiscoveryConfiguration + "/jwks";
            public const string Token = "connect/token";
            public const string Revocation = "connect/revocation";
            public const string UserInfo = "connect/userinfo";
            public const string Introspection = "connect/introspect";
            public const string EndSession = "/connect/endsession";
            public const string EndSessionCallback = EndSession + "/callback";
            public const string CheckSession = "connect/checksession";

            public static readonly string[] CorsPaths =
            {
                DiscoveryConfiguration,
                DiscoveryWebKeys,
                Token,
                UserInfo,
                Revocation
            };
        }

        public static readonly Dictionary<string, Endpoint> EndpointPathToNameMap = new Dictionary<string, Endpoint>
        {
            {ProtocolRoutePaths.EndSession, new Endpoint("Endsession", "/connect/endsession/callback",typeof(Duende.IdentityServer.Endpoints.Results.EndSessionResult)
                //, typeof(CustomEndSessionEndpoint)
                ) },
          
        };

        public static class EnvironmentKeys
        {
            public const string IdentityServerBasePath = "idsvr:IdentityServerBasePath";
            public const string IdentityServerOrigin = "idsvr:IdentityServerOrigin";
        }

        public static class TokenTypeHints
        {
            public const string RefreshToken = "refresh_token";
            public const string AccessToken = "access_token";
        }

        public static class PersistedGrantTypes
        {
            public const string AuthorizationCode = "authorization_code";
            public const string ReferenceToken = "reference_token";
            public const string RefreshToken = "refresh_token";
            public const string UserConsent = "user_consent";
        }

        public static List<string> SupportedTokenTypeHints = new List<string>
        {
            TokenTypeHints.RefreshToken,
            TokenTypeHints.AccessToken
        };

        public static class RevocationErrors
        {
            public const string UnsupportedTokenType = "unsupported_token_type";
        }

        public class Filters
        {
            // filter for claims from an incoming access token (e.g. used at the user profile endpoint)
            public static readonly string[] ProtocolClaimsFilter = new string[]
            {
                JwtClaimTypes.AccessTokenHash,
                JwtClaimTypes.Audience,
                JwtClaimTypes.AuthorizedParty,
                JwtClaimTypes.AuthorizationCodeHash,
                JwtClaimTypes.ClientId,
                JwtClaimTypes.Expiration,
                JwtClaimTypes.IssuedAt,
                JwtClaimTypes.Issuer,
                JwtClaimTypes.JwtId,
                JwtClaimTypes.Nonce,
                JwtClaimTypes.NotBefore,
                JwtClaimTypes.ReferenceTokenId,
                JwtClaimTypes.SessionId,
                JwtClaimTypes.Scope,
            };

            // filter list for claims returned from profile service prior to creating tokens
            public static readonly string[] ClaimsServiceFilterClaimTypes = new string[]
            {
                // TODO: consider JwtClaimTypes.AuthenticationContextClassReference,
                JwtClaimTypes.AccessTokenHash,
                JwtClaimTypes.Audience,
                JwtClaimTypes.AuthenticationMethod,
                JwtClaimTypes.AuthenticationTime,
                JwtClaimTypes.AuthorizedParty,
                JwtClaimTypes.AuthorizationCodeHash,
                JwtClaimTypes.ClientId,
                JwtClaimTypes.Expiration,
                JwtClaimTypes.IdentityProvider,
                JwtClaimTypes.IssuedAt,
                JwtClaimTypes.Issuer,
                JwtClaimTypes.JwtId,
                JwtClaimTypes.Nonce,
                JwtClaimTypes.NotBefore,
                JwtClaimTypes.ReferenceTokenId,
                JwtClaimTypes.SessionId,
                JwtClaimTypes.Subject,
                JwtClaimTypes.Scope,
                JwtClaimTypes.Confirmation
            };
        }
    }

    public static class CustomJwtClaimTypes
    {
        public const string Company = "company_name";

        public const string CompanyID = "external_company_id";

        public const string BillCycle = "billing_day";

        public const string PurchaseEligible = "purchase_eligible";
    }

    public static class CustomScopes
    {
        public const string Profile = "profile";

        public const string Email = "email";

        public const string Company = "company";

        public const string ApiCinbell = "api_cinbell";
    }

    public static class UIConstants
    {
        // the limit after which old messages are purged
        public const int CookieMessageThreshold = 2;

        public static class DefaultRoutePathParams
        {
            public const string Error = "errorId";
            public const string Login = "returnUrl";
            public const string Consent = "returnUrl";
            public const string Logout = "logoutId";
            public const string Custom = "returnUrl";
        }

        public static class DefaultRoutePaths
        {
            public const string MyLogin = "/web/account/login";
            public const string Login = "/account/login";

            //Place it back Mouni public const string Logout = "/account/logout";
            public const string Logout = "/account/logout";

            public const string Consent = "/consent/Index";
            //public const string Error = "/home/error";
        }

        public static class QueryStringParameters
        {
            public const string ClientId = "client_id";
            public const string PostLogoutRedirectUri = "post_logout_redirect_uri";
        }
    }

    public static class AuditConstants
    {
        public static class Steps
        {
            public const string Login = "UserLogin";
            public const string CreateToken = "CreateToken";
            public const string GenerateAuthCode = "GenerateAuthorizationCode";
            public const string RetrieveAuthCode = "RetrieveAuthorizationCode";
            public const string CreateRefreshToken = "CreateRefreshToken";
            public const string UpdateRefreshToken = "UpdateRefreshToken";
            public const string RetrieveReferenceToken = "RetrieveReferenceToken";
        }

        public static class Methods
        {
            public const string AccountLogin = "AccountController.Login";
            public const string GenerateAuthCode = "CustomAuthorizationCodeStore.StoreAuthorizationCodeAsync";
            public const string RetrieveAuthCode = "CustomAuthorizationCodeStore.GetAuthorizationCodeAsync";
            public const string CreateToken = "CustomTokenService.CreateSecurityTokenAsync";
            public const string CreateRefreshToken = "CustomRefreshTokenService.CreateRefreshTokenAsync";
            public const string UpdateRefreshToken = "CustomRefreshTokenService.UpdateRefreshTokenAsync";
            public const string RetrieveReferenceToken = "CustomReferenceTokenStoreGetReferenceTokenAsync";
        }

        public static class Result
        {
            public const string Success = "Success";
            public const string Error = "Error";
        }
    }
}