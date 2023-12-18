//using IdentityServer4.Models;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using MyCBTS.IDP.Login.Helpers;
//using MyCBTS.IDP.Login.Mapping;
//using MyCBTS.IDP.Login.Models;
//using MyCBTS.IDP.Login.Models.Response;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace MyCBTS.IDP.Login.CustomIdentityServices.Repository
//{
//    public class IdentityRepository : IIdentityRepository
//    {
//        public List<ApiResource> _apiResourcesList;
//        public List<IdentityResource> _identityResourcesList;
//        public List<Client> _clients;
//        private readonly IMyCBTSAPIHelper _MyCBTSAPIHelpher;
//        private readonly ILogger _logger;
//        public IdentityRepository(ILogger<IdentityRepository> logger, IMyCBTSAPIHelper myCBTSAPIHelper)
//        {
//            _logger = logger;
//            _MyCBTSAPIHelpher = myCBTSAPIHelper;


//        }
//        public List<ApiResource> GetAllApiResourcesAsyc()
//        {
//            if (_apiResourcesList == null)
//                _apiResourcesList = _MyCBTSAPIHelpher.GetAllApiResources();
//            return _apiResourcesList;
//        }

//        public List<IdentityResource> GetAllIdentityResourcesAsyc()
//        {
//            if (_identityResourcesList == null)
//                _identityResourcesList = _MyCBTSAPIHelpher.GetAllIdentityResources();
//            return _identityResourcesList;
//        }

//        public Client GetClient(string clientId)
//        {
//            if (_clients == null)
//            {
//                _clients = _MyCBTSAPIHelpher.GetAllClients();
//            }
//            if (_clients != null && _clients.Count > 0)
//            {
//                return _clients.Find(p => p.ClientId == clientId);
//            }
//            else
//            {
//                return null;
//            }
//        }

//        public string GetToken(string handle)
//        {
//            return _MyCBTSAPIHelpher.GetToken(handle);

//        }
//        public bool CreateToken(string key, string jsonCode, Token token)
//        {
//            return _MyCBTSAPIHelpher.CreateToken(key, jsonCode, token);
//        }
//        public async Task RemoveRefreshTokenAsync(string tokenkey)
//        {
//            await _MyCBTSAPIHelpher.RemoveRefreshTokenAsync(tokenkey);
//        }
//        public async Task RemoveReferenceTokenbyClientandSubjectAsync(string clientId, string subjectId)
//        {
//            await _MyCBTSAPIHelpher.RemoveReferenceTokenbyClientandSubjectAsync(clientId, subjectId);
//        }

//        public async Task RemoveReferenceTokenAsync(string tokenkey)
//        {
//            await _MyCBTSAPIHelpher.RemoveReferenceTokenAsync(tokenkey);
//        }


//        public User ValidatePassword(string username, string plainTextPassword, string application)
//        {
//            return _MyCBTSAPIHelpher.ValidateUser(username, plainTextPassword, application);
//        }
//        public DefaultAccount GetDefaultAccountByUserId(int userId)
//        {

//            return _MyCBTSAPIHelpher.GetDefaultAccountByUserId(userId);
//        }

//        public async Task CreateJsonToken(string key, Token token)
//        {
//            await _MyCBTSAPIHelpher.CreateJsonTokenAsync(key, token);
//        }
//        public bool CreateRefreshToken(Models.Api.RefreshToken token)
//        {
//            return _MyCBTSAPIHelpher.CreateRefreshToken(token);
//        }
//        public Models.Api.RefreshToken GetRefreshToken(string handle)
//        {
//            return _MyCBTSAPIHelpher.GetRefreshToken(handle);

//        }
//        public bool UpdateRefreshToken(Models.Api.RefreshToken token)
//        {
//            return _MyCBTSAPIHelpher.UpdateRefreshToken(token);
//        }

//        public async Task RemoveRefreshTokenbyClientandSubjectAsync(string clientId, string subjectId)
//        {
//            await _MyCBTSAPIHelpher.RemoveRefreshTokenbyClientandSubjectAsync(clientId, subjectId);
//        }
//        public bool CreateAuthorizationCode(string key, string jsonCode, AuthorizationCode code)
//        {

//            return _MyCBTSAPIHelpher.CreateAuthorzationCode(key, jsonCode, code);
//        }
//        public string GetAuthorizationCode(string code)
//        {
//            return _MyCBTSAPIHelpher.GetToken(code);
//        }
//    }
//}