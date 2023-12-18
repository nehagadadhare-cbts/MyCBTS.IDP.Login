using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiClient = MyCBTS.Api.Client;

namespace MyCBTS.IDP.Login.Stores
{
    public class CustomClientStore : IClientStore
    {
        private readonly MyCBTS.Api.Client.IIdentityClientClient _identityClientClient;
        private readonly ILogger _logger;
        private readonly ILoggerManager _loggerManager;

        public CustomClientStore(MyCBTS.Api.Client.IIdentityClientClient identityClientClient, ILogger<CustomClientStore> logger, ILoggerManager loggerManager)
        {
            _identityClientClient = identityClientClient;
            _logger = logger;
            _loggerManager = loggerManager;
        }

        /// <summary>
        ///Finds a client by id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns>client</returns>
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            try
            {
                await this._loggerManager.LogInfo(this._logger, null, "CustomClientStore:Find Client by client Id " + clientId, null);
                Client IdenClient = new Client();
                var client = await _identityClientClient.GetClientAsync(clientId);
                IdenClient = MapClient(client);
                if (client == null)
                {
                    await this._loggerManager.LogInfo(this._logger, null, "CustomClientStore:No Client Found  by client Id " + clientId, null);
                    return await Task.FromResult<Client>(null);
                }
                return await Task.FromResult<Client>(IdenClient);
            }
            catch (ApiClient.ApiException ex)
            {
                var code = ex.StatusCode;
                var message = JsonConvert.DeserializeObject<ErrorDescription>(ex.Response);
                await this._loggerManager.LogError(this._logger, ex, "CustomClientStore:No Client Found  by client Id " + clientId, null);
                return await Task.FromResult<Client>(null);
            }
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, ex, "CustomClientStore:No Client Found  by client Id " + clientId, null);
                return await Task.FromResult<Client>(null);
            }
        }

        public Client MapClient(ApiClient.Client apiClient)
        {
            try
            {
                Client idpClient = new Client()
                {
                    Enabled = apiClient.Enabled,
                    ClientId = apiClient.ClientId,
                    ClientName = apiClient.ClientName,
                    ClientUri = apiClient.ClientUri,
                    LogoUri = apiClient.LogoUri,
                    AllowRememberConsent = apiClient.AllowRememberConsent,
                    BackChannelLogoutUri = apiClient.LogoutUri,
                    FrontChannelLogoutUri = apiClient.LogoutUri,
                    RequirePkce = apiClient.RequirePkce,
                    RequireClientSecret = apiClient.RequireClientSecret,
                    RequireConsent = apiClient.RequireConsent,
                    IdentityTokenLifetime = apiClient.IdentityTokenLifetime,
                    AccessTokenLifetime = apiClient.AccessTokenLifetime,
                    AuthorizationCodeLifetime = apiClient.AuthorizationCodeLifetime,
                    AbsoluteRefreshTokenLifetime = apiClient.AbsoluteRefreshTokenLifetime,
                    SlidingRefreshTokenLifetime = apiClient.SlidingRefreshTokenLifetime,
                    RefreshTokenExpiration = (TokenExpiration)apiClient.RefreshTokenExpiration,
                    AccessTokenType = (AccessTokenType)apiClient.AccessTokenType,
                    EnableLocalLogin = apiClient.EnableLocalLogin,
                    IncludeJwtId = apiClient.IncludeJwtId,
                    AlwaysSendClientClaims = apiClient.AlwaysSendClientClaims,
                    ClientClaimsPrefix = apiClient.PrefixClientClaims.ToString(),
                    RedirectUris = apiClient.ClientRedirectUris.Select(p => p.Uri).ToList(),
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RefreshTokenUsage = (TokenUsage)apiClient.RefreshTokenUsage,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = apiClient.UpdateAccessTokenOnRefresh,
                    AllowedScopes = apiClient.ClientScopes.Select(p => p.Scope).ToList(),
                    ClientSecrets = new List<Secret>
                           {
                              new Secret () {Value=apiClient.ClientSecrets.FirstOrDefault().Value.Sha256() }
                           }
                    ,PostLogoutRedirectUris = apiClient.PostLogoutRedirectUris.Count() > 0 ? apiClient.PostLogoutRedirectUris.Select(p => p.Uri).ToList() : new List<string>()
                };
                return AssignGrantTypeToClient(idpClient, apiClient.Flow);
            }
            catch (Exception ex)
            {
                this._loggerManager.LogError(this._logger, ex, "Failed to get clientby Id", null);
                return null;
            }
        }

        public Client AssignGrantTypeToClient(Client client, int grantType)
        {
            switch (grantType)
            {
                case 0:
                    client.AllowedGrantTypes = GrantTypes.ClientCredentials;
                    break;

                case 1:
                    client.AllowedGrantTypes = GrantTypes.Code;
                    break;

                case 2:
                    client.AllowedGrantTypes = GrantTypes.CodeAndClientCredentials;
                    break;

                case 3:
                    client.AllowedGrantTypes = GrantTypes.Implicit;
                    break;

                case 4:
                    client.AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials;
                    break;

                case 5:
                    client.AllowedGrantTypes = GrantTypes.ResourceOwnerPassword;
                    break;

                case 6:
                    client.AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials;
                    break;

                case 7:
                    client.AllowedGrantTypes = GrantTypes.Hybrid;
                    break;

                case 8:
                    client.AllowedGrantTypes = GrantTypes.HybridAndClientCredentials;
                    break;
            }
            return client;
        }
    }
}