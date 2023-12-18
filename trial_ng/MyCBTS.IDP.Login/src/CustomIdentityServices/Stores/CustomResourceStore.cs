using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Microsoft.Extensions.Logging;


//using MyCBTS.IDP.Login.CustomIdentityServices.Repository;
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
    public class CustomResourceStore : IResourceStore
    { 
        private readonly ApiClient.IIdentityScopeClient _identityScopeClient;
        private readonly ILoggerManager _loggerManager;
        private readonly ILogger _logger;

        public CustomResourceStore(ApiClient.IIdentityScopeClient identityScopeClient, 
                ILogger<CustomResourceStore> logger, ILoggerManager loggerManager)
        {
            _logger = logger;
            _loggerManager = loggerManager;
            _identityScopeClient = identityScopeClient;
        }

        /// <summary>
        /// Finds the API resource by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            try
            {
                var apiResourceList = await _identityScopeClient.GetAllScopesAsync();
                List<ApiResource> apiResource1 = MapApiResources(apiResourceList.Where(p => p.Type == 0 || p.Type == 2).ToList());
                if (apiResource1 == null) { return await Task.FromResult<ApiResource>(null); }
                var apiResource = apiResource1.Where(p => p.Name == name).SingleOrDefault();
                return await Task.FromResult<ApiResource>(apiResource);
            }
            catch (ApiClient.ApiException ex)
            {
                var code = ex.StatusCode;
                var message = JsonConvert.DeserializeObject<ErrorDescription>(ex.Response);
                await this._loggerManager.LogError(this._logger, ex, "Error in getting all scopes ", null); throw;
            }
            catch (Exception ex)
            {
                return await Task.FromResult<ApiResource>(null);
            }
        }

        /// <summary>
        /// Gets API resources by scope name.
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> name)
        {
            try
            {
                var apiResourceList = await _identityScopeClient.GetAllScopesAsync();
                List<ApiResource> apiResource1 = MapApiResources(apiResourceList.Where(p => p.Type == 0 || p.Type == 2).ToList());
                if (apiResource1 == null) { return await Task.FromResult<List<ApiResource>>(null); }
                IEnumerable<ApiResource> list = apiResource1.Where(p => name.Contains(p.Name));
                return await Task.FromResult(list);
            }
            catch (ApiClient.ApiException ex)
            {
                var code = ex.StatusCode;
                var message = JsonConvert.DeserializeObject<ErrorDescription>(ex.Response);
                await this._loggerManager.LogError(this._logger, ex, "Error in getting all scopes ", null); throw;
            }
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, ex, "Error in getting all scopes ", null);
                return await Task.FromResult<IEnumerable<ApiResource>>(null);
            }
        }

        /// <summary>
        /// Gets identity resources by scope name.
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                var list = await _identityScopeClient.GetAllScopesAsync();
                List<IdentityResource> apiScopes = MapIdentityResource(list.Where(p => p.Type == 1 || p.Type == 2).ToList());
                if (apiScopes == null) { return await Task.FromResult<IEnumerable<IdentityResource>>(null); ; }
                IEnumerable<IdentityResource> idenResource = apiScopes.Where(p => scopeNames.Contains(p.Name));
                return await Task.FromResult(idenResource);
            }
            catch (ApiClient.ApiException ex)
            {
                var code = ex.StatusCode;
                var message = JsonConvert.DeserializeObject<ErrorDescription>(ex.Response);
                await this._loggerManager.LogError(this._logger, ex, "Error in getting all scopes ", null); throw;
            }
            catch (Exception ex)
            {
                return await Task.FromResult<IEnumerable<IdentityResource>>(null);
                await this._loggerManager.LogError(this._logger, ex, "Error in getting all scopes ", null); throw;
            }
        }

        /// <summary>
        ///  Gets all resources.
        /// </summary>
        /// <returns></returns>
        public async Task<Resources> GetAllResources()
        {
            var scopes = await _identityScopeClient.GetAllScopesAsync();
            var resources = await _identityScopeClient.GetAllScopesAsync(); 
            var identities = await _identityScopeClient.GetAllScopesAsync();
            
            IEnumerable<ApiScope> apisScope =  MapApiScopes(scopes.Where(p => p.Type == 0 || p.Type == 2).ToList());
            IEnumerable<ApiResource> apisList = MapApiResources(resources.Where(p => p.Type == 0 || p.Type == 2).ToList());
            IEnumerable<IdentityResource> identityResourcesList = MapIdentityResource(identities.Where(p => p.Type == 1 || p.Type == 2).ToList());

            Resources res = new Resources(identityResourcesList, apisList, apisScope);

            return await Task.FromResult(res);

        }

        /// <summary>
        ///  Gets all resources.
        /// </summary>
        /// <returns></returns>
        public async Task<Resources> GetAllResourcesAsync()
        {
            var scopes = await _identityScopeClient.GetAllScopesAsync();
            var resources = await _identityScopeClient.GetAllScopesAsync();
            var identities = await _identityScopeClient.GetAllScopesAsync();

            IEnumerable<ApiScope> apisScope = MapApiScopes(scopes.Where(p => p.Type == 0 || p.Type == 2).ToList());
            IEnumerable<ApiResource> apisList = MapApiResources(resources.Where(p => p.Type == 0 || p.Type == 2).ToList());
            IEnumerable<IdentityResource> identityResourcesList = MapIdentityResource(identities.Where(p => p.Type == 1 || p.Type == 2).ToList());

            Resources res = new Resources(identityResourcesList, apisList, apisScope);
            return await Task.FromResult(res);
        }

        private List<ApiResource> MapApiResources(ICollection<ApiClient.Scope> apiScopes)
        {
            try
            {
                List<ApiResource> apiResources = new List<ApiResource>();
                ApiResource apiResource;
                if (apiScopes != null && apiScopes.Count > 1)
                {
                    foreach (ApiClient.Scope apiScope in apiScopes)
                    {
                        apiResources.Add(apiResource = new ApiResource()
                        {
                            Name = apiScope.Name,
                            DisplayName = apiScope.DisplayName,
                            Description = apiScope.Description,
                            Enabled = apiScope.Enabled,
                            ApiSecrets = new List<Secret>
                           {
                              new Secret () {Value=apiScope.Secret.Sha256() }
                           }
                        });
                    }
                }
                return apiResources;
            }
            catch { return null; }
        }

        private List<IdentityResource> MapIdentityResource(List<ApiClient.Scope> apiScopes)
        {
            try
            {
                List<IdentityResource> scopes = new List<IdentityResource>();
                IdentityResource scope;
                foreach (ApiClient.Scope apiScope in apiScopes)
                {
                    scopes.Add(scope = new IdentityResource()
                    {
                        Name = apiScope.Name,
                        DisplayName = apiScope.DisplayName,
                        Description = apiScope.Description,
                        ShowInDiscoveryDocument = apiScope.ShowInDiscoveryDocument,
                        Enabled = apiScope.Enabled,
                    });
                }
                return scopes;
            }
            catch { return null; }
        }
        private List<ApiScope> MapApiScopes(List<ApiClient.Scope> apiScopes)
        {

            List<ApiScope> apiscopes = new List<ApiScope>();

            ApiScope apiscope;

            foreach (ApiClient.Scope apiScope in apiScopes)
            {
                apiscopes.Add(apiscope = new ApiScope()
                {
                    Name = apiScope.Name,
                    DisplayName = apiScope.DisplayName

                });
            }
            return apiscopes;
        }

        async Task<IEnumerable<ApiResource>> IResourceStore.FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            
            var resources = await _identityScopeClient.GetAllScopesAsync();
            IEnumerable<ApiResource> apisList = MapApiResources(resources.Where(p => p.Type == 0 || p.Type == 2).ToList());
            IEnumerable<ApiResource> list = apisList.Where(p => apiResourceNames.Contains(p.Name));
            return await Task.FromResult(list);
        }

        async Task<IEnumerable<ApiResource>> IResourceStore.FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var resources = await _identityScopeClient.GetAllScopesAsync();
            IEnumerable<ApiResource> apisList = MapApiResources(resources.Where(p => p.Type == 0 || p.Type == 2).ToList());
            IEnumerable<ApiResource> list = apisList.Where(p => scopeNames.Contains(p.Name));
            return await Task.FromResult(list);
        }

        async Task<IEnumerable<ApiScope>> IResourceStore.FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var scopes = await _identityScopeClient.GetAllScopesAsync();

            IEnumerable<ApiScope> apisScope = MapApiScopes(scopes.Where(p => p.Type == 0 || p.Type == 2).ToList());
            IEnumerable<ApiScope> list = apisScope.Where(p => scopeNames.Contains(p.Name));
            return await Task.FromResult(list);
        }

        async Task<IEnumerable<IdentityResource>> IResourceStore.FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            
            var identities = await _identityScopeClient.GetAllScopesAsync();

            IEnumerable<IdentityResource> identityResourcesList = MapIdentityResource(identities.Where(p => p.Type == 1 || p.Type == 2).ToList());
            IEnumerable<IdentityResource> list = identityResourcesList.Where(p => scopeNames.Contains(p.Name));
            return await Task.FromResult(list);
        }
    }
}