using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.Helpers;
using MyCBTS.IDP.Login.Mapping;
using MyCBTS.IDP.Login.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using ApiClient = MyCBTS.Api.Client;
using MyCBTS.IDP.Login.Extensions;
using Newtonsoft.Json;
using System.Net;

namespace MyCBTS.IDP.Login.Data
{
    public class UserStore : IUserStore<User>, IUserEmailStore<User>, 
                             IUserPasswordStore<User>
    {
       
        private readonly ILogger<UserStore> _logger;
        private readonly IMapper _iMapper;
        private readonly ApiClient.IUserClient _userClient;
        private readonly ApiClient.ICreateUserClient _createUserClient;
        public UserStore(IConfiguration configuration, ApiClient.IUserClient userClient
                     ,ApiClient.ICreateUserClient createUserClient
            , IMapper mapper
            , ILogger<UserStore> logger)
        {
            _logger = logger;
            _iMapper = mapper;
            _userClient = userClient;
            _createUserClient = createUserClient;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<IdentityError> errors = new List<IdentityError>();
            IdentityResult identityResult;
            try
            {
                user.FirstName = user.FirstName?.Trim();
                user.LastName = user.LastName?.Trim();
                user.UserName = user.UserName?.Trim().ToLower();
                user.Title = user.Title?.Trim();
                user.Email = user.Email?.Trim().ToLower();
                if (user.Accounts.Count > 0)
                {
                    user.Accounts[0].AccountNickName = user.Accounts[0].AccountNickName?.Trim();
                    user.Accounts[0].AccountNumber = user.Accounts[0].AccountNumber?.Trim();
                }
                var clientUser = _iMapper.Map<User, ApiClient.UserProfile>(user);
                var userDetails = _createUserClient.CreateUserAsync(clientUser).Result;

                if (userDetails?.UserId > 0)
                {
                    identityResult = IdentityResult.Success;
                }
                else
                {
                    var error = new IdentityError
                    {
                        Code = "Error",
                        Description = "Something Went Wrong"
                    };
                    identityResult = IdentityResult.Failed(error);
                }
            }
            catch
            {
                throw;
            }
            return Task.FromResult(identityResult);
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            //return Task.FromResult(user.UserId.ToString());
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                var userDetails = this._userClient.GetUserByUserNameAsync(normalizedEmail).Result;
                if (userDetails == null)
                {
                    return Task.FromResult<User>(null);
                }
                var applicationUser = _iMapper.Map<ApiClient.User, User>(userDetails);
                return Task.FromResult(applicationUser);
            }
            catch
            {
                throw;
            }
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }      

        public void Dispose()
        {
            // Nothing to dispose.
        }
    }
}
