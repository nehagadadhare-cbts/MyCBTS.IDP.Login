using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
//using MyCBTS.IDP.Login.CustomIdentityServices.Repository;
using System.Threading.Tasks;
using ApiClient = MyCBTS.Api.Client;

namespace MyCBTS.IDP.Login.Stores
{
    public class CustomProfileService : IProfileService
    {
        /// <summary>
        /// The signing service
        /// </summary>
       
       // private readonly ApiClient.ICBTSServiceClient _client;
        public CustomProfileService()
        {
            //_client = client;
        }

        //
        // Summary:
        //     This method is called whenever claims about the user are requested (e.g. during
        //     token creation or via the userinfo endpoint)
        //
        // Parameters:
        //   context:
        //     The context.
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.AddRequestedClaims(context.Subject.Claims);
            return Task.FromResult(0);
        }

        //
        // Summary:
        //     This method gets called whenever identity server needs to determine if the user
        //     is valid or active (e.g. if the user's account has been deactivated since they
        //     logged in). (e.g. during token issuance or validation).
        //
        // Parameters:
        //   context:
        //     The context.
        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;

            return Task.FromResult(0);
        }
    }
}