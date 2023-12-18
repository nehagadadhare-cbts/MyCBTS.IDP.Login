using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using MyCBTS.Api.Client;
//using MyCBTS.IDP.Login.CustomIdentityServices.Repository;
using System.Threading.Tasks;
using ApiClient = MyCBTS.Api.Client;

namespace MyCBTS.IDP.Login.CustomIdentityServices.Validators
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// The signing service
        /// </summary>

        private readonly ApiClient.IValidateUserClient _validateUserClient;
        public CustomResourceOwnerPasswordValidator(ApiClient.IValidateUserClient validateUserClient)
        {
            _validateUserClient = validateUserClient;
        }

        // Summary:
        //     Validates the resource owner password credential
        //
        // Parameters:
        //   context
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var userLoginRequest = new ApiClient.UserLogin()
            {
                Email = context.UserName,
                Password = context.Password,
                ApplicationName = ""
            };
            var user = _validateUserClient.ValidateUserAsync(userLoginRequest);
            if (user != null)
            {
                context.Result = new GrantValidationResult(user.Result.UserId.ToString(), "password");
                return Task.FromResult(new GrantValidationResult(user.Result.UserId.ToString(), "password"));
            }
            return Task.FromResult(new GrantValidationResult(TokenRequestErrors.InvalidGrant));
        }
    }
}