//using IdentityServer4.Models;
//using MyCBTS.IDP.Login.Models;
//using MyCBTS.IDP.Login.Models.Response;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace MyCBTS.IDP.Login.Helpers
//{
//    public interface IMyCBTSAPIHelper
//    {
//        IdentityServer4.Models.Client AssignGrantTypeToClient(IdentityServer4.Models.Client client, int grantType);

//        bool CreateAuthorzationCode(string key, string jsonCode, AuthorizationCode code);

//        HttpClient CreateClient(ref HttpClient httpClient);

//        Task<bool> CreateJsonTokenAsync(string key, IdentityServer4.Models.Token token);

//        bool CreateRefreshToken(Models.Api.RefreshToken token);

//        bool CreateToken(string key, string jsonCode, IdentityServer4.Models.Token token);

//        int CreateUser(User user);

//        List<ApiResource> GetAllApiResources();

//        List<IdentityServer4.Models.Client> GetAllClients();

//        List<IdentityResource> GetAllIdentityResources();

//        List<Models.Api.Scope> GetAllScopes();

//        DefaultAccount GetDefaultAccountByUserId(int userId);

//        HttpResponseMessage GetHttpResponse(string requestPath, StringContent content, string method);

//        Task GetHttpResponseAsync(string requestPath, StringContent content, string method);

//        Models.Api.RefreshToken GetRefreshToken(string tokenKey);

//        string GetToken(string tokenKey);

//        User GetUserByUserName(string userName);

//        IdentityServer4.Models.Client MapClient(Models.Api.Client apiClient);

//        Models.Api.Token MapToken(string key, string jsonCode, IdentityServer4.Models.Token token);

//        Task RemoveReferenceTokenAsync(string tokenkey);

//        Task RemoveReferenceTokenbyClientandSubjectAsync(string clientId, string subjectId);

//        Task RemoveRefreshTokenAsync(string tokenkey);

//        Task RemoveRefreshTokenbyClientandSubjectAsync(string clientId, string subjectId);

//        bool UpdateRefreshToken(Models.Api.RefreshToken token);

//        User ValidateUser(string userName, string password, string application);
//    }
//}