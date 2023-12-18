//using IdentityServer4.Models;
//using MyCBTS.IDP.Login.Models;
//using MyCBTS.IDP.Login.Models.Response;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace MyCBTS.IDP.Login.CustomIdentityServices.Repository
//{
//    public interface IIdentityRepository
//    {
//        #region IdentityClient

//        Client GetClient(string clientId);

//        #endregion IdentityClient

//        #region Identity Token

//        Task CreateJsonToken(string key, Token token);

//        bool CreateToken(string key, string jsonCode, Token token);

//        bool CreateRefreshToken(Models.Api.RefreshToken token);

//        Models.Api.RefreshToken GetRefreshToken(string handle);

//        string GetToken(string handle);

//        bool UpdateRefreshToken(Models.Api.RefreshToken token);

//        Task RemoveReferenceTokenAsync(string tokenkey);

//        Task RemoveReferenceTokenbyClientandSubjectAsync(string clientId, string subjectId);

//        Task RemoveRefreshTokenAsync(string tokenkey);

//        Task RemoveRefreshTokenbyClientandSubjectAsync(string clientId, string subjectId);

//        #endregion Identity Token

//        #region Authorization

//        bool CreateAuthorizationCode(string key, string jsonCode, AuthorizationCode code);

//        string GetAuthorizationCode(string code);

//        #endregion Authorization

//        List<ApiResource> GetAllApiResourcesAsyc();

//        List<IdentityResource> GetAllIdentityResourcesAsyc();

//        DefaultAccount GetDefaultAccountByUserId(int userId);

//        User ValidatePassword(string username, string plainTextPassword, string application);
//    }
//}