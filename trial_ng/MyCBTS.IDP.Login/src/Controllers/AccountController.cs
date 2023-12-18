using AutoMapper;
using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyCBTS.IDP.Login.Configuration;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Helpers;
using MyCBTS.IDP.Login.Mapping;
using MyCBTS.IDP.Login.Models;
using MyCBTS.IDP.Login.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static MyCBTS.IDP.Login.Models.EnumList;
using ApiClient = MyCBTS.Api.Client;
using IdentityModel;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Events;
using Microsoft.Extensions.Hosting.Internal;

namespace MyCBTS.IDP.Login.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IUserStore<User> _iUserStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly ILogger<AccountController> _logger;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _iMapper;
        private readonly ApiClient.IUserClient _userClient;
        private readonly ApiClient.IValidateUserClient _validateUserClient;
        private readonly ApiClientHelper _apiClientHelper;
        private readonly AppConfiguration _appConfiguration;       
        private readonly IEventService _events;
        private readonly ApiClient.ITokenClient _tokenClient;
        private readonly ApiClient.IAuditClient _auditClient;


        public AccountController(IIdentityServerInteractionService interaction,
                                    IUserStore<User> userStore,
                                    IAuthenticationSchemeProvider schemeProvider,
                                    ILogger<AccountController> logger,
                                    ILoggerManager loggerManager,
                                    IMapper iMapper,
                                    ApiClient.IUserClient userClient,
                                    ApiClient.IValidateUserClient validateUserClient,
                                    ApiClient.ITokenClient tokenClient,
                                    ApiClient.IAuditClient auditClient,
                                    IOptions<AppConfiguration> appConfiguration,
                                    ApiClientHelper apiClientHelper,
                                    IEventService events)
        {
            _interaction = interaction;
            _iUserStore = userStore;
            _schemeProvider = schemeProvider;
            _logger = logger;
            _loggerManager = loggerManager;
            _iMapper = iMapper;
            _validateUserClient = validateUserClient;
            _userClient = userClient;
            _appConfiguration = appConfiguration.Value;
            _apiClientHelper = apiClientHelper;
            _events = events;
            _tokenClient = tokenClient;
            _auditClient = auditClient;
        }

        /// <summary>
        /// Show login page
        /// </summary>
        [HttpGet("~/")]
        [HttpGet("~/account/login")]
        public async Task<IActionResult> Login([FromQuery(Name = "returnurl")] string returnurl)
        {
            string sessionReturnURL = HttpContext.Session.GetString(APIConstants.SessionReturnURL);

            if (string.IsNullOrEmpty(returnurl) && sessionReturnURL == null)
                return Redirect(_appConfiguration.DefaultURI);


            LoginViewModel loginViewModel = new LoginViewModel();

            #region GetSetSession_ReturnURL

            if (!returnurl.IsPresent()) { returnurl = sessionReturnURL; }
            else if (!returnurl.IsPresent() && string.IsNullOrEmpty(sessionReturnURL))
            { return Redirect(_appConfiguration.DefaultURI); }
            else
            {
                HttpContext.Session.Clear();
                await SetClientInSession(returnurl);
                HttpContext.Session.SetString(APIConstants.SessionReturnURL, returnurl);
            }

            #endregion GetSetSession_ReturnURL

            try
            {
                loginViewModel = await BuildLoginViewModelAsync(returnurl);
            }
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, ex, "Exception in Account.Login.HttpGet", null).ConfigureAwait(false);
            }
            return View(loginViewModel);
        }

        /// <summary>
        /// It will take username and password.
        /// checks is it valid user or not. If valid it will redirect to return URL.
        /// </summary>
        [HttpPost("~/account/login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            UserLogger userLogger = new UserLogger();
            try
            {
                userLogger.UserName = loginViewModel.Username?.Trim().ToLower();

                var context = await _interaction.GetAuthorizationContextAsync(loginViewModel.ReturnUrl);
                if (context == null)
                {
                    await this._loggerManager.LogError(this._logger, null, "Context is empty at Account.Login.HttpPost", null).ConfigureAwait(false);
                    throw new Exception("");
                }

                if (!ModelState.IsValid)
                {
                    await this._loggerManager.LogError(this._logger, null, "Model state is invalid at Account.Login.HttpPost", null).ConfigureAwait(false);
                    throw new Exception("");
                }
                var userLogin = new ApiClient.UserLogin()
                {
                    Email = loginViewModel.Username?.Trim().ToLower(),
                    Password = loginViewModel.Password,
                    ApplicationName = loginViewModel.ApplicationName
                };
                var userDetails = await _validateUserClient.ValidateUserAsync(userLogin);
                var user = _iMapper.Map<User>(userDetails);


                #region CreateClaims

                var claims = new List<Claim>{
                                new Claim(ClaimTypes.Name, user.Email),
                                new Claim(ClaimTypes.Sid,(user.UserId).ToString())};
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var isuser = new IdentityServerUser(user.UserId.ToString())
                {
                    DisplayName = user.Email,
                    AdditionalClaims = claims
                };
                Microsoft.AspNetCore.Authentication.AuthenticationProperties props;
                props = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                };

                #endregion CreateClaims

                await HttpContext.SignInAsync(isuser, props);

                bool isLastLoginUpdated = await _userClient.UpdateLoginDateAsync(user.UserId);
                if (isLastLoginUpdated)
                {
                    userLogger.UserID = user.UserId;
                    userLogger.UserName = user.Email;
                    await _loggerManager.LogInfo(this._logger, null, "Updated Last Login Date", userLogger).ConfigureAwait(false);
                }
                else
                {
                    await _loggerManager.LogError(this._logger, null, "Error while updating Last Login Date", userLogger).ConfigureAwait(false);
                }

                if (loginViewModel.AllowRememberLogin)
                    CreateCookie("_sue", loginViewModel.Username, 1);
            }
            catch (ApiClient.ApiException ex)
            {
                userLogger.UserName = loginViewModel.Username.Trim();
                string errorMessage = _apiClientHelper.GetErrorMessage(ex);
                loginViewModel.CustomErrors.AddError(errorMessage);
                await _loggerManager.LogError(this._logger, null, errorMessage, userLogger).ConfigureAwait(false);
                return View(loginViewModel);
            }
            catch (Exception ex)
            {
                string errorMessage = "Something went wrong.";
                loginViewModel.CustomErrors.AddError(errorMessage);
                userLogger.UserName = loginViewModel.Username.Trim();
                await this._loggerManager.LogError(this._logger, ex, "Exception Account.Login.HttpPost", userLogger).ConfigureAwait(false);
            }
            return Redirect(loginViewModel.ReturnUrl);
        }

        [HttpGet("~/account/{tokenId}/login")]
        public async Task<IActionResult> ImpersonateLogin(string tokenId)
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            UserLogger userLogger = new UserLogger();
            try
            {
                if (string.IsNullOrWhiteSpace(tokenId))
                {
                    await this._loggerManager.LogError(this._logger, null, "tokenId is null.", null).ConfigureAwait(false);
                    throw new Exception("tokenId is null");
                }
                var tokenData = await _tokenClient.RetrieveTokenDataAsync(tokenId).ConfigureAwait(false);

                if (tokenData?.TokenAttributes == null || tokenData?.TokenAttributes.UserId <= 0)
                {
                    await this._loggerManager.LogError(this._logger, null, "Login Failed or Incorrect", userLogger).ConfigureAwait(false);
                    loginViewModel.CustomErrors.AddError("Login failed, try again.");
                    return View(loginViewModel);
                }

                var userDetails = await _userClient.GetUserDetailsByUserIdAsync(tokenData.TokenAttributes.UserId).ConfigureAwait(false);

                var user = _iMapper.Map<User>(userDetails);

                if (user == null)
                {
                    await this._loggerManager.LogError(this._logger, null, "Login Failed or Incorrect", userLogger).ConfigureAwait(false);
                    loginViewModel.CustomErrors.AddError("Login failed, try again.");
                    return View(loginViewModel);
                }

                #region CreateClaims

                var claims = new List<Claim>{
                                new Claim(ClaimTypes.Name, user.Email),
                                new Claim(ClaimTypes.Sid,(user.UserId).ToString())};
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var isuser = new IdentityServerUser(user.UserId.ToString())
                {
                    DisplayName = user.Email,
                    AdditionalClaims = claims
                };
                Microsoft.AspNetCore.Authentication.AuthenticationProperties props;
                props = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                };

                #endregion CreateClaims

                await HttpContext.SignInAsync(isuser, props);

                if (loginViewModel.AllowRememberLogin)
                    CreateCookie("_sue", loginViewModel.Username, 1);

                #region Audit
                
                await _auditClient.InsertAuditAsync(0,
                                                    131,
                                                    string.Empty,
                                                    user.UserId,
                                                    string.Empty,
                                                    string.Empty,
                                                    string.Empty,
                                                    tokenData.FromApplication,
                                                    JsonConvert.SerializeObject(tokenData.TokenAttributes),
                                                    "User verified successfully.",
                                                    " ",
                                                    tokenData.TokenAttributes.CustomerRepId,
                                                    DateTime.Now
                                                    );
                #endregion Audit
            }
            //catch (ApiClient.ApiException ex)
            //{
            //    string errorMessage = _apiClientHelper.GetErrorMessage(ex);
            //    loginViewModel.CustomErrors.AddError(errorMessage);
            //    await _loggerManager.LogError(this._logger, null, errorMessage, userLogger).ConfigureAwait(false);
            //    return View(loginViewModel);
            //}
            catch (Exception ex)
            {
                await this._loggerManager.LogError(this._logger, ex, "Exception Account.Login.HttpPost", null).ConfigureAwait(false);
            }

            return Redirect(_appConfiguration.DefaultURI);
        }


        /// <summary>
        /// Show logout page
        /// </summary>
        
        [HttpGet("~/account/identitylogout")]
        public async Task<IActionResult> IdentityLogout(string postReturnurl)
        {
            var tokenId = HttpContext.Request.Query["tokenid"].ToString();
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(IdentityServerConstants.SignoutScheme);
            if (string.IsNullOrEmpty(postReturnurl) && !string.IsNullOrEmpty(tokenId))
            {
                return Redirect(_appConfiguration.DefaultURI + string.Format(_appConfiguration.ImpersonateLogout, tokenId));
                
            }
            return new RedirectResult(postReturnurl);
        }

        [HttpGet("~/account/logout")]
        public async Task<IActionResult> Logout(string redirectUrl)
        {
           // delete local authentication cookie
            await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityServerConstants.SignoutScheme);
            return new RedirectResult(redirectUrl);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();
                await HttpContext.SignOutAsync(IdentityServerConstants.SignoutScheme);

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            
            return View("LoggedOut", vm);
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };


            return vm;
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            if (returnUrl.IsPresent())
            {
                var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
                string userName = string.Empty;
                bool rememberMe = CheckRememberMeCookie(out userName);
                if (context != null && !string.IsNullOrWhiteSpace(context?.Client.ClientId))
                {
                    var vm = new LoginViewModel
                    {
                        ReturnUrl = returnUrl,
                        AllowRememberLogin = AccountOptions.AllowRememberLogin,
                        Username = userName,
                    };

                    return vm;
                }
            }
            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                ReturnUrl = returnUrl,
            };
        }

        private bool CheckRememberMeCookie(out string userName)
        {
            userName = string.Empty;
            var sueCookie = HttpContext.Request.Cookies["_sue"];
            if (sueCookie != null && !string.IsNullOrEmpty(sueCookie))
            {
                userName = Convert.ToString(sueCookie);
                return true;
            }

            return false;
        }

        private bool CreateCookie(string key, string value, int days)
        {
            CookieOptions _cookieOptions = new CookieOptions { Expires = DateTime.Now.AddMinutes(days * 24 * 60) };
            HttpContext.Response.Cookies.Append(key, value, _cookieOptions);
            return true;
        }

        private async Task SetClientInSession(string returnurl)
        {
            try
            {
                if (returnurl.IsPresent())
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString(APIConstants.SessionClient)))
                    {
                        var context = await _interaction.GetAuthorizationContextAsync(returnurl);
                        if (context != null)
                        {
                            var client = context.Client.ClientId.ToUpper().Contains(BrandName.CBTS.ToString()) ? BrandName.CBTS.ToString() :
                                            context.Client.ClientId.ToUpper().Contains(BrandName.ONX.ToString()) ? BrandName.ONX.ToString() : _appConfiguration.DefaultBrand.ToString();

                            HttpContext.Session.SetString(APIConstants.SessionClient, client);
                        }
                    }
                }
            }
            catch(Exception ex)
            { RedirectToAction("Error", "Home", "errorId=404"); }
        }
    }
}