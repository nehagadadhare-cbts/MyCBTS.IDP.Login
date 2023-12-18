////using IdentityServer4.Extensions;
////using IdentityServer4.Models;
////using IdentityServer4.Stores.Serialization;
////using Microsoft.Extensions.Logging;
////using MyCBTS.IDP.Login.Extensions;
////using MyCBTS.IDP.Login.Mapping;
////using MyCBTS.IDP.Login.Models;
////using MyCBTS.IDP.Login.Models.Response;
////using Newtonsoft.Json;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Net.Http;
////using System.Net.Http.Headers;
////using System.Text;
////using System.Threading.Tasks;
////using MyCBTS.IDP.Login.Models.Request;
////using MyCBTS.IDP.Login.Logger;
////using Microsoft.Extensions.Options;
////using MyCBTS.IDP.Login.Configuration;

////namespace MyCBTS.IDP.Login.Helpers
////{
////    public class MyCBTSAPIHelper : IMyCBTSAPIHelper
////    {
////        public readonly ApiURIConfiguration _apiURIConfiguration;
////        private readonly ILogger<MyCBTSAPIHelper> _logger;
////        private readonly ILoggerManager _loggerManager;
////        public MyCBTSAPIHelper(IOptions<ApiURIConfiguration> apiURIConfiguration, ILogger<MyCBTSAPIHelper> logger, ILoggerManager loggerManager)
////        {
////            _apiURIConfiguration = apiURIConfiguration.Value;
////            _logger = logger;
////            _loggerManager = loggerManager;
////        }

////        #region CreateClient
////        public HttpClient CreateClient(ref HttpClient httpClient)
////        {
////            try
////            {
////                httpClient.BaseAddress = new Uri(_apiURIConfiguration.BaseURI);
////                httpClient.DefaultRequestHeaders.Accept.Clear();
////                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiNameConstants.MediaType));
////                httpClient.DefaultRequestHeaders.Add(ApiNameConstants.Application, "_apiURIConfiguration.Application");
////                httpClient.DefaultRequestHeaders.Add(ApiNameConstants.UserId, "_apiURIConfiguration.UserId");
////                httpClient.DefaultRequestHeaders.Add(ApiNameConstants.Password, "_apiURIConfiguration.Password");

////            }
////            catch (Exception ex)
////            { }
////            return httpClient;
////        }
////        #endregion

////        #region HTTPResponse
////        public HttpResponseMessage GetHttpResponse(string requestPath, StringContent content, string method)
////        {
////            string baseURI = _apiURIConfiguration.BaseURI;
////            try
////            {
////                using (var client = new HttpClient())
////                {
////                    client.BaseAddress = new Uri(baseURI);
////                    client.DefaultRequestHeaders.Accept.Clear();
////                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiNameConstants.MediaType));
////                    client.DefaultRequestHeaders.Add(ApiNameConstants.Application, "_apiURIConfiguration.Application");
////                    client.DefaultRequestHeaders.Add(ApiNameConstants.UserId, "_apiURIConfiguration.UserId");
////                    client.DefaultRequestHeaders.Add(ApiNameConstants.Password, "_apiURIConfiguration.Password");
////                    HttpResponseMessage response = null;
////                    switch (method)
////                    {
////                        case "get":
////                            response = client.GetAsync(requestPath).Result;
////                            break;
////                        case "put":
////                            response = client.PutAsync(requestPath, content).Result;
////                            break;
////                        case "delete":
////                            response = client.DeleteAsync(requestPath).Result;
////                            break;
////                        case "post":
////                            response = client.PostAsync(requestPath, content).Result;
////                            break;
////                    }
////                    return response;
////                }
////            }
////            catch (Exception ex)
////            {
////                //_logger.LogError(ex.StackTrace);
////                return null;
////            }
////        }
////        public async Task GetHttpResponseAsync(string requestPath, StringContent content, string method)
////        {
////            string baseURI = _apiURIConfiguration.BaseURI;
////            try
////            {
////                using (var client = new HttpClient())
////                {
////                    client.BaseAddress = new Uri(baseURI);
////                    client.DefaultRequestHeaders.Accept.Clear();
////                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiNameConstants.MediaType));
////                    client.DefaultRequestHeaders.Add(ApiNameConstants.Application, "_apiURIConfiguration.Application");
////                    client.DefaultRequestHeaders.Add(ApiNameConstants.UserId, "_apiURIConfiguration.UserId");
////                    client.DefaultRequestHeaders.Add(ApiNameConstants.Password, "_apiURIConfiguration.Password");
////                    switch (method)
////                    {
////                        case "get":
////                            await client.GetAsync(requestPath);
////                            break;
////                        case "put":
////                            await client.PutAsync(requestPath, content);
////                            break;
////                        case "delete":
////                            await client.DeleteAsync(requestPath);
////                            break;
////                        case "post":
////                            await client.PostAsync(requestPath, content);
////                            break;
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                //_logger.LogError(ex.StackTrace);

////            }
////        }

////        #endregion

////        #region IdentityClient
////        public List<Client> GetAllClients()
////        {
////            try
////            {
////                string baseURI = _apiURIConfiguration.BaseURI;
////                string requestPath = baseURI + APIConstants.GetAllClients;
////                HttpResponseMessage response = GetHttpResponse(requestPath, null, "get");
////                List<Client> _clients = new List<Client>();
////                if (response.IsSuccessStatusCode)
////                {
////                    var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////                    List<Models.Api.Client> _apiClients = JsonConvert.DeserializeObject<List<Models.Api.Client>>(responseString);
////                    foreach (Models.Api.Client _apiClient in _apiClients)
////                    {
////                        _clients.Add(MapClient(_apiClient));
////                    }
////                    return _clients;
////                }
////                return null;
////            }
////            catch(Exception ex) {
////                this._loggerManager.LogError(this._logger, ex, "GetClients", null);
////                return null; }
////        }
////        #endregion

////        #region IdentityScope
////        public List<Models.Api.Scope> GetAllScopes()
////        {
////            try
////            {
////                string baseURI = _apiURIConfiguration.BaseURI;
////                string requestPath = string.Format(baseURI + APIConstants.GetAllScopes);
////                HttpResponseMessage response = GetHttpResponse(requestPath, null, "get");
////                if (response.IsSuccessStatusCode)
////                {
////                    var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////                    List<Models.Api.Scope> apiScopes = JsonConvert.DeserializeObject<List<Models.Api.Scope>>(responseString);
////                    return apiScopes;
////                }
////                return null;
////            }
////            catch (Exception ex)
////            { return null; }
////        }
////        #endregion

////        #region IdentityToken
////        public bool CreateToken(string key, string jsonCode, Token token)
////        {
////            try
////            {
////                string baseURI = _apiURIConfiguration.BaseURI;
////                string requestPath = baseURI + APIConstants.CreateToken;
////                StringContent content = new StringContent(JsonConvert.SerializeObject(MapToken(key, jsonCode, token)), Encoding.UTF8, APIConstants.MediaType);
////                HttpResponseMessage response = GetHttpResponse(requestPath, content, "post");
////                if (response != null && response.IsSuccessStatusCode)
////                {
////                    return true;
////                }
////                else
////                {
////                    return false;
////                }
////            }
////            catch (Exception ex)
////            {
////                this._loggerManager.LogError(this._logger,null,"Error in creating token in web respository Create Token" + key + jsonCode + ex.Message,null); return false;
////            }
////        }
////        public async Task<bool> CreateJsonTokenAsync(string key, Token token)
////        {
////            try
////            {
////                string jsonCode = ConvertToJson(token);
////                string baseURI = _apiURIConfiguration.BaseURI;

////                string requestPath = string.Format(baseURI + APIConstants.CreateJsonToken);
////                StringContent content = new StringContent(JsonConvert.SerializeObject(MapToken(key, jsonCode, token)), Encoding.UTF8, APIConstants.MediaType);
////                await GetHttpResponseAsync(requestPath, content, "post");
////                return true;
////            }
////            catch (Exception ex)
////            {
////               this._loggerManager.LogError(this._logger,null,"Error in creating token in web respository Create Token" + key + ex.Message,null);
////                return false;
////            }
////        }
////        public bool CreateRefreshToken(Models.Api.RefreshToken token)
////        {
////            string requestPath = string.Format(_apiURIConfiguration.BaseURI + APIConstants.CreateRefreshToken);
////            StringContent content = new StringContent(JsonConvert.SerializeObject(token), Encoding.UTF8, APIConstants.MediaType);
////            HttpResponseMessage response = GetHttpResponse(requestPath, content, "post");
////            if (response.IsSuccessStatusCode)
////            {
////                return true;
////            }
////            else
////            {
////                return false;
////            }
////        }
////        public bool UpdateRefreshToken(Models.Api.RefreshToken token)
////        {
////            string baseURI = _apiURIConfiguration.BaseURI;
////            string requestPath = string.Format(baseURI + APIConstants.UpdatefreshToken);
////            StringContent content = new StringContent(JsonConvert.SerializeObject(token), Encoding.UTF8, APIConstants.MediaType);
////            HttpResponseMessage response = GetHttpResponse(requestPath, content, "put");
////            if (response.IsSuccessStatusCode)
////            {
////                return true;
////            }
////            else
////            {
////                return false;
////            }
////        }
////        public string GetToken(string tokenKey)
////        {
////            try
////            {
////                string baseURI = _apiURIConfiguration.BaseURI;
////                string requestPath = string.Format("{0}{1}/{2}", baseURI, APIConstants.GetToken, tokenKey);
////                HttpResponseMessage response = GetHttpResponse(requestPath, null, "get");
////                Models.Api.Token apiToken;
////                if (response.IsSuccessStatusCode)
////                {
////                    var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////                    if (!string.IsNullOrEmpty(responseString))
////                    {
////                        apiToken = JsonConvert.DeserializeObject<Models.Api.Token>(responseString);
////                        return apiToken.JsonCode;
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                this._loggerManager.LogError(this._logger, null,string.Format("Reference Token does not exist or expired or Error in converting to json--{0}. REFERENCETK: [{1}]", ex.Message, tokenKey),null);
////            }
////            return null;
////        }
////        public Models.Api.RefreshToken GetRefreshToken(string tokenKey)
////        {
////            try
////            {
////                string baseURI = _apiURIConfiguration.BaseURI;
////                string requestPath = string.Format("{0}{1}/{2}", baseURI, APIConstants.GetRefreshToken, tokenKey);
////                HttpResponseMessage response = GetHttpResponse(requestPath, null, "get");
////                Models.Api.RefreshToken apiToken;
////                if (response.IsSuccessStatusCode)
////                {
////                    var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////                    if (!string.IsNullOrEmpty(responseString))
////                    {
////                        apiToken = JsonConvert.DeserializeObject<Models.Api.RefreshToken>(responseString);
////                        return apiToken;
////                    }
////                }
////            }
////            catch (Exception ex)
////            {
////                this._loggerManager.LogError(this._logger, null,string.Format("Refresh Token does not exist or expired or Error in converting to json--{0}. REFRESHTK: [{1}]", ex.Message, tokenKey),null);
////            }
////            return null;
////        }
////        public async Task RemoveRefreshTokenAsync(string tokenkey)
////        {
////            string baseURI = _apiURIConfiguration.BaseURI;
////            string requestPath = string.Format("{0}{1}/{2}", baseURI, APIConstants.RemoveRefreshTokenAsync, tokenkey);
////            await GetHttpResponseAsync(requestPath, null, "delete");
////        }
////        public async Task RemoveReferenceTokenAsync(string tokenkey)
////        {
////            string baseURI = _apiURIConfiguration.BaseURI;
////            string requestPath = string.Format("{0}{1}/{2}", baseURI, APIConstants.RemoveReferenceTokenAsync, tokenkey);
////            await GetHttpResponseAsync(requestPath, null, "delete");
////        }
////        public async Task RemoveReferenceTokenbyClientandSubjectAsync(string clientId, string subjectId)
////        {
////            string baseURI = _apiURIConfiguration.BaseURI;
////            string requestPath = string.Format("{0}{1}/{2}/{3}", baseURI, APIConstants.RemoveReferenceTokenbyClientandSubjectAsync, clientId, subjectId);
////            await GetHttpResponseAsync(requestPath, null, "delete");
////        }
////        public async Task RemoveRefreshTokenbyClientandSubjectAsync(string clientId, string subjectId)
////        {
////            string requestPath = string.Format("{0}{1}/{2}/{3}", _apiURIConfiguration.BaseURI, APIConstants.RemoveRefreshTokenbyClientandSubjectAsync, clientId, subjectId);
////            await GetHttpResponseAsync(requestPath, null, "delete");
////        }
////        #endregion

////        #region IdentityMappings
////        private List<ApiResource> MapApiResources(List<Models.Api.Scope> apiScopes)
////        {
////            try
////            {
////                List<ApiResource> apiResources = new List<ApiResource>();
////                ApiResource apiResource;
////                if (apiScopes != null && apiScopes.Count > 1)
////                {
////                    foreach (Models.Api.Scope apiScope in apiScopes)
////                    {
////                        apiResources.Add(apiResource = new ApiResource()
////                        {
////                            Name = apiScope.Name,
////                            DisplayName = apiScope.DisplayName,
////                            Description = apiScope.Description,
////                            Enabled = apiScope.Enabled,
////                            Scopes =  {
////                    new Scope()
////                    {
////                        Name = apiScope.Name,
////                        DisplayName = apiScope.DisplayName
////                    }

////                    },
////                            ApiSecrets = new List<Secret>
////                           {
////                              new Secret () {Value=apiScope.Secret.Sha256() }
////                           }
////                        });
////                    }
////                }
////                return apiResources;
////            }
////            catch { return null; }

////        }
////        private List<IdentityResource> MapIdentityResource(List<Models.Api.Scope> apiScopes)
////        {
////            try
////            {
////                List<IdentityResource> scopes = new List<IdentityResource>();
////                IdentityResource scope;
////                foreach (Models.Api.Scope apiScope in apiScopes)
////                {
////                    scopes.Add(scope = new IdentityResource()
////                    {
////                        Name = apiScope.Name,
////                        DisplayName = apiScope.DisplayName,
////                        Description = apiScope.Description,
////                        ShowInDiscoveryDocument = apiScope.ShowInDiscoveryDocument,
////                        Enabled = apiScope.Enabled,
////                    });
////                }
////                return scopes;
////            }
////            catch { return null; }
////        }
////        public Client MapClient(Models.Api.Client apiClient)
////        {
////            try
////            {
////                Client idpClient = new Client()
////                {
////                    Enabled = apiClient.Enabled,
////                    ClientId = apiClient.ClientId,
////                    ClientName = apiClient.ClientName,
////                    ClientUri = apiClient.ClientUri,
////                    LogoUri = apiClient.LogoUri,
////                    AllowRememberConsent = apiClient.AllowRememberConsent,
////                    BackChannelLogoutUri = apiClient.LogoutUri,
////                    FrontChannelLogoutUri = apiClient.LogoutUri,
////                    RequirePkce = apiClient.RequirePkce,
////                    RequireClientSecret = apiClient.RequireClientSecret,
////                    RequireConsent = apiClient.RequireConsent,
////                    IdentityTokenLifetime = apiClient.IdentityTokenLifetime,
////                    AccessTokenLifetime = apiClient.AccessTokenLifetime,
////                    AuthorizationCodeLifetime = apiClient.AuthorizationCodeLifetime,
////                    AbsoluteRefreshTokenLifetime = apiClient.AbsoluteRefreshTokenLifetime,
////                    SlidingRefreshTokenLifetime = apiClient.SlidingRefreshTokenLifetime,
////                    RefreshTokenExpiration = (TokenExpiration)apiClient.RefreshTokenExpiration,
////                    AccessTokenType = (AccessTokenType)apiClient.AccessTokenType,
////                    EnableLocalLogin = apiClient.EnableLocalLogin,
////                    IncludeJwtId = apiClient.IncludeJwtId,
////                    AlwaysSendClientClaims = apiClient.AlwaysSendClientClaims,
////                    ClientClaimsPrefix = apiClient.PrefixClientClaims.ToString(),
////                    RedirectUris = apiClient.ClientRedirectUris.Select(p => p.Uri).ToList(),
////                    AlwaysIncludeUserClaimsInIdToken = true,
////                    RefreshTokenUsage = (TokenUsage)apiClient.RefreshTokenUsage,
////                    AllowOfflineAccess = true,
////                    UpdateAccessTokenClaimsOnRefresh = apiClient.UpdateAccessTokenOnRefresh,
////                    AllowedScopes = apiClient.ClientScopes.Select(p => p.Scope).ToList(),
////                    ClientSecrets = new List<Secret>
////                           {
////                              new Secret () {Value=apiClient.ClientSecrets.FirstOrDefault().Value.Sha256() }
////                           },
////                    PostLogoutRedirectUris = apiClient.PostLogoutRedirectUris.Count() > 0 ? apiClient.PostLogoutRedirectUris.Select(p => p.Uri).ToList() : new List<string>()
////                };
////                return AssignGrantTypeToClient(idpClient, apiClient.Flow);
////            }
////            catch (Exception)
////            { return null; }
////        }
////        public Models.Api.Token MapToken(string key, string jsonCode, Token token)
////        {
////            try
////            {
////                Dictionary<string, object> claims = token.Claims.ToClaimsDictionary();
////                Models.Api.Token apiToken = new Models.Api.Token
////                {
////                    Key = key,
////                    AuthCodeChallenge = "0",
////                    AuthCodeChallengeMethod = "0",
////                    ClientId = token.ClientId,
////                    Expiry = token.CreationTime.AddSeconds(token.Lifetime),
////                    IsOpenId = true,
////                    Nonce = claims.ContainsKey("nonce") ? Convert.ToString(claims["nonce"]) : string.Empty,
////                    TokenType = token.Type,
////                    WasConsentShown = false,
////                    JsonCode = jsonCode,
////                    RedirectUri = string.Empty,
////                    SessionId = claims.ContainsKey("sid") ? Convert.ToString(claims["sid"]) : string.Empty,
////                    SubjectId = token.SubjectId,
////                    LifeTime = token.Lifetime
////                };
////                return apiToken;
////            }
////            catch (Exception ex)
////            { this._loggerManager.LogError(this._logger, null,"Error in mapping token--" + ex.Message,null); throw; }
////        }
////        private Models.Api.Token MapTokenFromAuthorizationCode(string key, string jsonCode, AuthorizationCode code)
////        {
////            Models.Api.Token apiToken = new Models.Api.Token
////            {
////                Key = key,
////                AuthCodeChallenge = "0",
////                AuthCodeChallengeMethod = "0",
////                ClientId = code.ClientId,
////                Expiry = code.CreationTime.AddSeconds(code.Lifetime),
////                IsOpenId = code.IsOpenId,
////                Nonce = string.Empty,
////                TokenType = "code",
////                WasConsentShown = false,
////                JsonCode = jsonCode,
////                RedirectUri = string.Empty,
////                SessionId = code.SessionId,
////                SubjectId = code.Subject.GetSubjectId(),
////                LifeTime = code.Lifetime
////            };
////            return apiToken;
////        }
////        #endregion

////        #region Json
////        JsonSerializerSettings GetJsonSerializerSettings()
////        {
////            var settings = new JsonSerializerSettings();
////            settings.Converters.Add(new ClaimConverter());
////            settings.Converters.Add(new ClaimsPrincipalConverter());
////            return settings;
////        }
////        protected string ConvertToJson(Token value)
////        {
////            try
////            {
////                return JsonConvert.SerializeObject(value, GetJsonSerializerSettings());
////            }
////            catch (Exception ex) {this. _loggerManager.LogError(this._logger, null,"Error in converting to json--" + ex.Message,null); return string.Empty; }
////        }
////        #endregion

////        #region Registration
////        //public AccountDetails ValidateAccountNumber(string accountNumber)
////        //{
////        //    string requestPath = _apiURIConfiguration.BaseURI + string.Format(APIConstants.ValidateAccountNumber, accountNumber);
////        //    HttpResponseMessage response =  GetHttpResponse(requestPath, null, "get");
////        //    if(response.IsSuccessStatusCode)
////        //    {
////        //        var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////        //        var accountDetails = JsonConvert.DeserializeObject<AccountDetails>(responseString);
////        //        accountDetails.accountNumber = accountDetails.accountNumber?.Trim();
////        //        accountDetails.billingSystem = accountDetails.billingSystem?.Trim();
////        //        accountDetails.accountStatus = accountDetails.accountStatus?.Trim();
////        //        return accountDetails;
////        //    }
////        //    else
////        //    {
////        //        this._loggerManager.LogError(this._logger, null, "Error while fetching details from WebApi", null);
////        //    }
////        //    return null;
////        //}

//public User GetUserByUserName(string userName)
//{
//    string requestPath = _apiURIConfiguration.BaseURI + string.Format(APIConstants.GetUserByUserName, userName);
//    HttpResponseMessage response = GetHttpResponse(requestPath, null, "get");
//    if (response.IsSuccessStatusCode)
//    {
//        var responseString = response.Content.ReadAsStringAsync().Result.ToString();
//        var userDetails = JsonConvert.DeserializeObject<User>(responseString);
//        return userDetails;
//    }
//    else
//    {
//        this._loggerManager.LogError(this._logger, null, "Error while fetching details from WebApi", null);
//    }
//    return null;
//}
//public int CreateUser(User user)
//{
//    int userId = 0;
//    string requestPath = _apiURIConfiguration.BaseURI + APIConstants.CreateUser;
//    user.FirstName = user.FirstName?.Trim();
//    user.LastName = user.LastName?.Trim();
//    user.UserName = user.UserName?.Trim().ToLower();
//    user.Title = user.Title?.Trim();
//    user.Email = user.Email?.Trim().ToLower();
//    if (user.AccountsList.Count > 0)
//    {
//        user.AccountsList[0].AccountNickName = user.AccountsList[0].AccountNickName?.Trim();
//        user.AccountsList[0].AccountNumber = user.AccountsList[0].AccountNumber?.Trim();
//    }

//            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
//            HttpResponseMessage response = GetHttpResponse(requestPath, content, "post");
//            if (response.IsSuccessStatusCode)
//            {
//                var responseString = response.Content.ReadAsStringAsync().Result.ToString();
//                var userDetails = JsonConvert.DeserializeObject<User>(responseString);
//                return userDetails;
//            }
//            else
//            {
//                this._loggerManager.LogError(this._logger, null, "Error while fetching details from WebApi", null);
//            }
//            return null;
//        }
//        public int CreateUser(User user)
//        {
//            int userId = 0;
//            string requestPath = _apiURIConfiguration.BaseURI + APIConstants.CreateUser;
//            user.FirstName = user.FirstName?.Trim();
//            user.LastName = user.LastName?.Trim();
//            user.UserName = user.UserName?.Trim().ToLower();
//            user.Title = user.Title?.Trim();
//            user.Email = user.Email?.Trim().ToLower();
//            if (user.AccountsList.Count > 0)
//            {
//                user.AccountsList[0].AccountNickName = user.AccountsList[0].AccountNickName?.Trim();
//                user.AccountsList[0].AccountNumber = user.AccountsList[0].AccountNumber?.Trim();
//            }

////            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
////            HttpResponseMessage response = GetHttpResponse(requestPath, content, "post");
////            if (response.IsSuccessStatusCode)
////            {
////                var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////                userId = JsonConvert.DeserializeObject<int>(responseString);
////                return userId;
////            }
////            else
////            {
////                this._loggerManager.LogError(this._logger, null, "Error while fetching details from WebApi", null);
////            }
////            return userId;
////        }

////        //public bool SendVerifyEmailAddressLink(string emailAddress)
////        //{
////        //    bool isVerifyEmailSent = false;
////        //    string requestPath = _apiURIConfiguration.BaseURI + string.Format(APIConstants.SendVerifyEmailAddressLink, emailAddress);
////        //    HttpResponseMessage response = GetHttpResponse(requestPath, null, "post");
////        //    if (response.IsSuccessStatusCode)
////        //    {
////        //        isVerifyEmailSent = true;
////        //    }
////        //    else
////        //    {
////        //        this._loggerManager.LogError(this._logger, null, "Error from API while sending verification email", null);
////        //    }
////        //    return isVerifyEmailSent;
////        //}

////        //public bool VerifyEmailAddress(string token)
////        //{
////        //    bool isEmailVerified = false;
////        //    string requestPath = _apiURIConfiguration.BaseURI + string.Format(APIConstants.VerifyEmailAddress, token);
////        //    HttpResponseMessage response = GetHttpResponse(requestPath, null, "post");
////        //    if (response.IsSuccessStatusCode)
////        //    {
////        //        isEmailVerified = true;
////        //    }
////        //    {
////        //        this._loggerManager.LogError(this._logger, null, "Error from API while verifying email address", null);
////        //    }
////        //    return isEmailVerified;
////        //}

////        #endregion

////        #region Forgot Password

////        //public bool SendPasswordResetMail(string emailAddress)
////        //{
////        //    bool isEmailSent = false;
////        //    string requestPath =  _apiURIConfiguration.BaseURI + string.Format(APIConstants.SendPasswordResetMail, emailAddress);
////        //    HttpResponseMessage response = GetHttpResponse(requestPath, null, "post");
////        //    if (response.IsSuccessStatusCode)
////        //    {
////        //        isEmailSent = true;
////        //    }
////        //    else
////        //    {
////        //        this._loggerManager.LogError(this._logger, null, "Error while sending password reset email", null);
////        //    }
////        //    return isEmailSent;
////        //}

////        //public ApplicationUser VerifyPwdResetEmail(string token)
////        //{
////        //    bool isResetEmailVerified = false;
////        //    string requestPath = _apiURIConfiguration.BaseURI + string.Format(APIConstants.VerifyPwdResetEmail, token);
////        //    HttpResponseMessage response = GetHttpResponse(requestPath, null, "post");
////        //    if (response.IsSuccessStatusCode)
////        //    {
////        //        var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////        //        var userDetails = JsonConvert.DeserializeObject<ApplicationUser>(responseString);
////        //        if (userDetails == null)
////        //        { return null; }
////        //        else
////        //        { return userDetails; }
////        //    }
////        //    else
////        //    {
////        //        this._loggerManager.LogError(this._logger, null, "Error from API while verifying password reset email", null);
////        //    }
////        //    return null;
////        //}

////        //public bool ResetPassword(ForgotPasswordModel resetPassword)
////        //{
////        //    bool isPasswordReset = false;
////        //    UserLoginRequest pwdReset = new UserLoginRequest()
////        //    {
////        //        Email = resetPassword.EmailAddress,
////        //        UserId = resetPassword.UserId,
////        //        Password = resetPassword.Password
////        //    };
////        //    StringContent content = new StringContent(JsonConvert.SerializeObject(pwdReset), Encoding.UTF8, APIConstants.MediaType);
////        //    string requestPath = _apiURIConfiguration.BaseURI + string.Format(APIConstants.ResetPassword);
////        //    HttpResponseMessage response = GetHttpResponse(requestPath, content, "post");
////        //    if (response.IsSuccessStatusCode)
////        //    {
////        //        var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////        //        isPasswordReset = true;
////        //    }
////        //    else
////        //    {
////        //        this._loggerManager.LogError(this._logger, null, "Error from API while updating password through reset password", null);
////        //    }
////        //    return isPasswordReset;
////        //}
////        #endregion

////        #region GetUser
////        public DefaultAccount GetDefaultAccountByUserId(int userId)
////        {
////            string requestPath = _apiURIConfiguration.BaseURI + string.Format(APIConstants.GetUserByUserId, userId);
////            HttpResponseMessage response = GetHttpResponse(requestPath, null, "get");
////            if (response.IsSuccessStatusCode)
////            {
////                var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////                var userDetails = JsonConvert.DeserializeObject<User>(responseString);
////                if (userDetails == null)
////                { return null; }

//    var defaultAccount = new DefaultAccount()
//    {
//        Email = userDetails.Email,
//        FirstName = userDetails.FirstName,
//        LastName = userDetails.LastName,
//        UserId = userDetails.UserId,
//        DefaultUserAccountId = userDetails?.AccountsList.Count > 0 ? userDetails.AccountsList.FirstOrDefault().AccountId : 0,
//    };
//    return defaultAccount;
//}
//return null;

//        }

//        public User ValidateUser(string userName, string password, string application)
//        {
//            try
//            {
//                string baseURI = _apiURIConfiguration.BaseURI;
//                string requestPath = baseURI + APIConstants.ValidateUser;
//                var userLoginRequest = new UserLoginRequest()
//                {
//                    Email = userDetails.Email,
//                    FirstName = userDetails.FirstName,
//                    LastName = userDetails.LastName,
//                    UserId = userDetails.UserId,
//                    DefaultUserAccountId = userDetails?.AccountsList.Count > 0 ? userDetails.AccountsList.FirstOrDefault().AccountId : 0,
//                };
//                return defaultAccount;
//            }
//            return null;
//}
//}

////        }

////        public User ValidateUser(string userName, string password, string application)
////        {
////            try
////            {
////                string baseURI = _apiURIConfiguration.BaseURI;
////                string requestPath = baseURI + APIConstants.ValidateUser;
////                var userLoginRequest = new UserLoginRequest()
////                {
////                    Email = userName,
////                    Password = password,
////                    ApplicationName = application
////                };

////                StringContent content = new StringContent(JsonConvert.SerializeObject(userLoginRequest), Encoding.UTF8, APIConstants.MediaType);
////                HttpResponseMessage response = GetHttpResponse(requestPath, content, "post");

////                if (response.IsSuccessStatusCode)
////                {
////                    var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////                    var user = JsonConvert.DeserializeObject<User>(responseString);
////                    return user;
////                }
////                else
////                {
////                    this._loggerManager.LogError(this._logger, null, "Error from API while validating the user", null);
////                }
////                return null;
////            }
////            catch (Exception)
////            { return null; }
////        }

////        public List<string> GetEmailsByAccountNumber(string accountNumber)
////        {
////            string requestPath = _apiURIConfiguration.BaseURI + string.Format(APIConstants.GetEmailByAccountNumber, accountNumber);
////            HttpResponseMessage response = GetHttpResponse(requestPath, null, "get");
////            if (response.IsSuccessStatusCode)
////            {
////                var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////                var userNameList = JsonConvert.DeserializeObject<List<string>>(responseString);
////                return userNameList;
////            }
////            else
////            {
////                this._loggerManager.LogError(this._logger, null, "Error from API while getting email details by account number", null);
////            }
////            return null;
////        }

////        #endregion

////        #region UpdateUser
////        //public bool UpdateLastLoginDate(int userId)
////        //{
////        //    bool isLastLoginUpdated = false;
////        //    try
////        //    {
////        //        string requestPath = _apiURIConfiguration.BaseURI + string.Format(APIConstants.UpdateLastLoginDate, userId);
////        //        HttpResponseMessage response = GetHttpResponse(requestPath, null, "post");
////        //        if (response.IsSuccessStatusCode)
////        //        {
////        //            var responseString = response.Content.ReadAsStringAsync().Result.ToString();
////        //            isLastLoginUpdated = JsonConvert.DeserializeObject<bool>(responseString);
////        //            return isLastLoginUpdated;
////        //        }
////        //        else
////        //        {
////        //            this._loggerManager.LogError(this._logger, null, "Error from API while updateing last login date", null);
////        //        }
////        //        return isLastLoginUpdated;
////        //    }
////        //    catch (Exception)
////        //    { return isLastLoginUpdated; }
////        //}
////        #endregion
////        #region billsummary

////        #endregion

////        public List<ApiResource> GetAllApiResources()
////        {
////            List<Models.Api.Scope> apiScopes = GetAllScopes();
////            return MapApiResources(apiScopes.Where(p => p.Type == 0 || p.Type == 2).ToList());
////        }
////        public List<IdentityResource> GetAllIdentityResources()
////        {
////            List<Models.Api.Scope> apiScopes = GetAllScopes();
////            return MapIdentityResource(apiScopes.Where(p => p.Type == 1 || p.Type == 2).ToList());
////        }

////        public Client AssignGrantTypeToClient(Client client, int grantType)
////        {
////            switch (grantType)
////            {
////                case 0:
////                    client.AllowedGrantTypes = GrantTypes.ClientCredentials;
////                    break;
////                case 1:
////                    client.AllowedGrantTypes = GrantTypes.Code;
////                    break;
////                case 2:
////                    client.AllowedGrantTypes = GrantTypes.CodeAndClientCredentials;
////                    break;
////                case 3:
////                    client.AllowedGrantTypes = GrantTypes.Implicit;
////                    break;
////                case 4:
////                    client.AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials;
////                    break;
////                case 5:
////                    client.AllowedGrantTypes = GrantTypes.ResourceOwnerPassword;
////                    break;
////                case 6:
////                    client.AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials;
////                    break;
////                case 7:
////                    client.AllowedGrantTypes = GrantTypes.Hybrid;
////                    break;
////                case 8:
////                    client.AllowedGrantTypes = GrantTypes.HybridAndClientCredentials;
////                    break;
////            }
////            return client;
////        }

////        public bool CreateAuthorzationCode(string key, string jsonCode, AuthorizationCode code)
////        {
////            string requestPath = string.Format(_apiURIConfiguration.BaseURI + APIConstants.CreateToken);

////            StringContent content = new StringContent(JsonConvert.SerializeObject(MapTokenFromAuthorizationCode(key, jsonCode, code)), Encoding.UTF8, APIConstants.MediaType);

////            HttpResponseMessage response = GetHttpResponse(requestPath, content, "post");

////            if (response.IsSuccessStatusCode)
////            {
////                return true;
////            }
////            else
////            {
////                return false;
////            }
////        }
////    }
////}