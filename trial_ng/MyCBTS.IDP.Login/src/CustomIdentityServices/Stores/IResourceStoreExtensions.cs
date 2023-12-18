using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;

namespace MyCBTS.IDP.Login.CustomIdentityServices.Stores
{
    public class IResourceStoreExtensions
    {
        private readonly IResourceStore _resourceStore;

        public IResourceStoreExtensions(IResourceStore resourceStore)
        {
            _resourceStore = resourceStore;

        }



        public async Task<Resources> FindEnabledResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var resources = await _resourceStore.FindResourcesByScopeAsync(scopeNames);

            if (resources == null) return new Resources();

            return new Resources(
                resources.IdentityResources.Where(x => x.Enabled),
                resources.ApiResources.Where(x => x.Enabled),
                resources.ApiScopes.Where(x => x.Enabled))
            {
                OfflineAccess = resources.OfflineAccess
            };
        }

        /// <summary>
        /// Creates a resource validation result.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="parsedScopesResult">The parsed scopes.</param>
        /// <returns></returns>
        public async Task<ResourceValidationResult> CreateResourceValidationResult(ParsedScopesResult parsedScopesResult)
        {
            var validScopeValues = parsedScopesResult.ParsedScopes;
            var scopes = validScopeValues.Select(x => x.ParsedName).ToArray();
            var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(scopes);
            return new ResourceValidationResult(resources, validScopeValues);
        }
    }
}
