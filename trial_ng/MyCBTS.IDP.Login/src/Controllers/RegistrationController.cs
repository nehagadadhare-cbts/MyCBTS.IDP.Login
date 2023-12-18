using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCBTS.IDP.Login.Data;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Helpers;
using MyCBTS.IDP.Login.Mapping;
using MyCBTS.IDP.Login.Models;
using MyCBTS.IDP.Login.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static MyCBTS.IDP.Login.Models.EnumList;
using ApiClient = MyCBTS.Api.Client;
using MyCBTS.IDP.Login.Configuration;
using Microsoft.Extensions.Options;
using MyCBTS.IDP.Login.Utility;

namespace MyCBTS.IDP.Login.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserStore _iUserStore;
        private readonly IMapper _iMapper;
        private readonly ILogger<RegistrationController> _logger;
        private readonly ILoggerManager _loggerManager;
        //private readonly ApiClient.ICBTSServiceClient _client;
        private readonly ApiClientHelper _apiClientHelper;
        private readonly ApiClient.IAccountClient _accountClient;
        private readonly AppConfiguration _appConfiguration;
        private readonly ApiClient.IEmailVerificationClient _emailVerificationClient;
        private readonly AllowCBTSConfiguration _allowcbtsconfiguration;
        private readonly IEncrypDecrypBySymmetricKey _symmetricKey;//cbe_16161
        public const string CryptoKey = "fb7ghcf274w242ecb2fa33d257c44212";//cbe_16161
        public RegistrationController(UserStore userStore, 
                                      ILoggerManager loggerManager, 
                                      ApiClientHelper apiClientHelper,
                                      IMapper mapper, ILogger<RegistrationController> logger, 
                                      ApiClient.IAccountClient accountClient, 
                                      ApiClient.IEmailVerificationClient emailVerificationClient,
                                      IOptions<AppConfiguration> appConfiguration, IOptions<AllowCBTSConfiguration> allowcbtsConfiguration, IEncrypDecrypBySymmetricKey symmetricKey)
        {
            _iUserStore = userStore;
            _iMapper = mapper;
            _logger = logger;
            _loggerManager = loggerManager;
            _accountClient = accountClient;
            _apiClientHelper = apiClientHelper;
            _emailVerificationClient = emailVerificationClient;
            _appConfiguration = appConfiguration.Value;
            _allowcbtsconfiguration = allowcbtsConfiguration.Value;
            _symmetricKey = symmetricKey;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            RegisterViewModel register = new RegisterViewModel()
            {
                RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1,
            };
            return View(register);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegistrationStep1(RegisterViewModel usermodel)
        {
            var objUserLogger = new UserLogger();
            string allowcbtsaccounts = _allowcbtsconfiguration.Allowcbtsaccounts;

            //cbe_16161
            #region CaptchaValidation
            var ReCaptcharesponse = Request.Form["g-recaptcha-response"];
            ReCaptchaValidation reCaptcha = new ReCaptchaValidation();
            if (string.IsNullOrEmpty(ReCaptcharesponse) || !reCaptcha.ValidateCaptch(ReCaptcharesponse, _appConfiguration.ReCaptchaSecretKey))
            {
                ViewBag.ReCaptchaMessage = "Check the recaptcha to prove that you are not a robot";
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                return View("../Registration/Register", usermodel);
            }
            #endregion

            var checkBrute = ReadDecryptCookie("_zpinv");
            if (!string.IsNullOrEmpty(checkBrute) && Convert.ToInt32(checkBrute) >= Convert.ToInt32(_appConfiguration.ZipInvCount))
            {
                ViewBag.Title = "Registration";
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                var error = "Sorry, we are not able to create the profile online.  Please contact customer care for assistance.";
                usermodel.CustomErrors.AddError(error);
                await this._loggerManager.LogError(this._logger, null, error, null).ConfigureAwait(false);
                return View("../Registration/Register", usermodel);
            }
            //cbe_16161

            try
            {
                string brandName = HttpContext.Session.GetString(APIConstants.SessionClient);
                ModelState.Remove("InvoiceNumber");
                ModelState.Remove("TotalAmountDue");
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Registration";
                    usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                    await this._loggerManager.LogError(this._logger, null, "Model state is invalid at Registration.RegistrationStep1.HttpPost", null).ConfigureAwait(false);
                    return View("../Registration/Register", usermodel);
                }

                var emailDetials = _iUserStore.FindByEmailAsync(usermodel.EmailAddress?.Trim().ToLower(), default(CancellationToken));
                if (emailDetials?.Result?.Email != null && emailDetials?.Result?.Email.Length > 0)
                {
                    usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                    usermodel.CustomErrors.AddError("EmailAddress Already Exists");
                    objUserLogger.UserName = emailDetials?.Result?.Email;
                    await this._loggerManager.LogInfo(this._logger, null, "EmailAddress Already Exists", objUserLogger).ConfigureAwait(false);
                    return View("../Registration/Register", usermodel);
                }

                var details = await _accountClient.ValidateAccountNumberAsync(usermodel.AccountNumber?.Trim());
                //CBE 15404 start
                var ascendonDetails = await _accountClient.GetAscendonCustomerInfo(usermodel.AccountNumber?.Trim());
                //CBE 15404 End
                var accountDetails = _iMapper.Map<AccountDetails>(details);
                objUserLogger.AccountNumber = usermodel.AccountNumber?.Trim();
			
                if (accountDetails == null || (accountDetails.billingSystem == EnumList.BillingSystem.ASCENDON.ToString() && ascendonDetails == null)) 	//CBE 15404 
                {
                    usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                    usermodel.CustomErrors.AddError("Invalid account number");
                    await this._loggerManager.LogError(this._logger, null, "Account number not found", objUserLogger).ConfigureAwait(false);
                    return View("../Registration/Register", usermodel);
                }

                if (!String.IsNullOrEmpty(accountDetails.billingSystem) &&accountDetails.billingSystem.Contains("CBTS")&& allowcbtsaccounts=="True")
                {
                    string error = _allowcbtsconfiguration.Error;
                    usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                    usermodel.CustomErrors.AddError(error);
                    await this._loggerManager.LogError(this._logger, null, "Account number is CBTS account", objUserLogger).ConfigureAwait(false);
                    return View("../Registration/Register", usermodel);
                }
				//CBE 15404 start
                if (ascendonDetails != null && ascendonDetails.BillingSystem == EnumList.BillingSystem.ASCENDON.ToString()
                                          && ascendonDetails.AccountType == "Child" && ascendonDetails.InvoiceType == "3")
                {
                    string error = "Child accounts without statements are not supported.";
                    usermodel.CustomErrors.AddError(error);
                    await _loggerManager.LogInfo(this._logger, null, error, objUserLogger).ConfigureAwait(false);
                    return View("../Registration/Register", usermodel);
                }
			    //CBE 15404 END
			    
                accountDetails.accountNumber = accountDetails.accountNumber?.Trim();
                accountDetails.billingSystem = accountDetails.billingSystem?.Trim();
                accountDetails.accountStatus = accountDetails.accountStatus?.Trim();
                if ((!System.Enum.IsDefined(typeof(EnumList.BillingSystem), accountDetails?.billingSystem.ToUpper()))
                                    || accountDetails?.accountStatus.ToUpper() != APIConstants.AccountStatus)
                {
                    usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                    usermodel.CustomErrors.AddError("Invalid account number");
                    await this._loggerManager.LogInfo(this._logger, null, "Invalid account number", objUserLogger).ConfigureAwait(false);
                    return View("../Registration/Register", usermodel);
                }

                await this._loggerManager.LogInfo(this._logger, null, "Account Number validated successfully", objUserLogger).ConfigureAwait(false);
                _iMapper.Map<AccountDetails, RegisterViewModel>(accountDetails, usermodel);
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep2;
                return View("../Registration/Register", usermodel);
            }
            catch (ApiClient.ApiException ex)
            {
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                objUserLogger.UserName = usermodel.EmailAddress?.Trim().ToLower();
                objUserLogger.AccountNumber = usermodel.AccountNumber;
                string errorMessage = _apiClientHelper.GetErrorMessage(ex);
                usermodel.CustomErrors.AddError(errorMessage);
                await _loggerManager.LogError(this._logger, null, errorMessage, objUserLogger).ConfigureAwait(false);
                return View("../Registration/Register", usermodel);
            }
            catch (Exception ex)
            {
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                objUserLogger.UserName = usermodel.EmailAddress?.Trim().ToLower();
                objUserLogger.AccountNumber = usermodel.AccountNumber;
                string errorMessage = "Something went wrong";
                usermodel.CustomErrors.AddError(errorMessage);
                await this._loggerManager.LogError(this._logger, ex, "Exception in Registration.RegistrationStep1.HttpPost", objUserLogger).ConfigureAwait(false);
            }
            return View("../Registration/Register", usermodel);
        }

        [HttpPost("register/validateaccount")]
        public async Task<IActionResult> RegistrationStep2(RegisterViewModel usermodel, string back)
        {

            if (!string.IsNullOrEmpty(back))
            {
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                return View("../Registration/Register", usermodel);
            }

            //cbe_16161
            var checkBrute = ReadDecryptCookie("_zpinv");
            if (!string.IsNullOrEmpty(checkBrute) && Convert.ToInt32(checkBrute) >= Convert.ToInt32(_appConfiguration.ZipInvCount))
            {
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep2;
                var error = "Sorry, we are not able to create the profile online.  Please contact customer care for assistance.";
                usermodel.CustomErrors.AddError(error);
                await this._loggerManager.LogError(this._logger, null, error, null).ConfigureAwait(false);
                return View("../Registration/Register", usermodel);
            }

            var objUserLogger = new UserLogger();
            try
            {
                string billingSystem = usermodel.BillingSystem;
                if (!(billingSystem == EnumList.BillingSystem.CBAD.ToString() || billingSystem == EnumList.BillingSystem.CRIS.ToString())
                        && string.IsNullOrEmpty(usermodel.InvoiceNumber))
                {
                    string error = "Please enter invoice number.";
                    usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep2;
                    usermodel.CustomErrors.AddError(error);
                    await this._loggerManager.LogInfo(this._logger, null, error, objUserLogger).ConfigureAwait(false);
                    return View("../Registration/Register", usermodel);
                }                             
                string brandName = HttpContext.Session.GetString(APIConstants.SessionClient);
                var user = _iMapper.Map<RegisterViewModel, User>(usermodel);
                List<BillSummary> validBill = await _apiClientHelper.ValidateBillDetails(usermodel);
                objUserLogger.AccountNumber = usermodel.AccountNumber;
                objUserLogger.UserName = usermodel.EmailAddress;
                if (!(validBill.Count > 0))
                {
                    string error = "";
                    if (billingSystem == EnumList.BillingSystem.CBAD.ToString() || billingSystem == EnumList.BillingSystem.CRIS.ToString())
                    {
                        error = "Please enter valid total amount due and try again";
                    }
                    else
                    {
                        error = "Please enter valid invoice number or total amount due and try again";
                    }
                    //cbe_16161                
                    if (IsBruteForceAttackOnRegister())
                    {
                        error = "Sorry, we are not able to create the profile online.  Please contact customer care for assistance.";
                    }
                    usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep2;
                    usermodel.CustomErrors.AddError(error);
                    await this._loggerManager.LogInfo(this._logger, null, error, objUserLogger).ConfigureAwait(false);
                    return View("../Registration/Register", usermodel);
                }

                usermodel.LastBill = validBill[0]?.BillDate.ToString("dd-MM-yyyy");
                usermodel.BrandName = string.IsNullOrEmpty(brandName) ? _appConfiguration.DefaultBrand.ToString() : brandName;
                await this._loggerManager.LogInfo(this._logger, null, "Bill details verified", objUserLogger).ConfigureAwait(false);
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationContinue;
                return View("../Registration/Register", usermodel);
            }
            catch (ApiClient.ApiException ex)
            {
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                objUserLogger.UserName = usermodel.EmailAddress?.Trim().ToLower();
                objUserLogger.AccountNumber = usermodel.AccountNumber;
                string errorMessage = _apiClientHelper.GetErrorMessage(ex);
                usermodel.CustomErrors.AddError(errorMessage);
                await _loggerManager.LogError(this._logger, null, errorMessage, objUserLogger).ConfigureAwait(false);
                return View("../Registration/Register", usermodel);
            }
            catch (Exception ex)
            {
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                objUserLogger.UserName = usermodel.EmailAddress?.Trim().ToLower();
                objUserLogger.AccountNumber = usermodel.AccountNumber;
                string errorMessage = "Something went wrong";
                usermodel.CustomErrors.AddError(errorMessage);
                await this._loggerManager.LogError(this._logger, ex, "Exception in Registration.RegistrationContinue.HttpPost", objUserLogger).ConfigureAwait(false);
            }
            return View("../Registration/Register", usermodel);
        }
               
        [HttpPost("register/validatedetails")]
        public async Task<IActionResult> RegistrationContinue(RegisterViewModel usermodel, string goback)
        {
            if (!string.IsNullOrEmpty(goback))
            {
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep2;
                return View("../Registration/Register", usermodel);
            }
            var objUserLogger = new UserLogger();
            try
            {
                string brandName = HttpContext.Session.GetString(APIConstants.SessionClient);
                usermodel.BrandName = string.IsNullOrEmpty(brandName) ? _appConfiguration.DefaultBrand.ToString() : brandName;
                var user = _iMapper.Map<RegisterViewModel, User>(usermodel);
                user.Accounts = new List<Account>
                        {
                            new Account
                            {
                              AccountNickName = usermodel.AccountNickName,
                              AccountStatus = usermodel.AccountStatus,
                              AccountNumber = usermodel.AccountNumber,
                              BillingSystem = usermodel.BillingSystem
                            }
                        };

                var response = _iUserStore.CreateAsync(user, default(CancellationToken));
                if (!response.Result.Succeeded)
                {
                    usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                    await this._loggerManager.LogError(this._logger, null, "Registration failed", objUserLogger).ConfigureAwait(false);
                    usermodel.CustomErrors.AddError("Registration failed, Try again");
                    return View("../Registration/Register", usermodel);
                }

                //var userdetails = _client.GetUserByUserNameAsync(usermodel.EmailAddress?.Trim().ToLower());
                //await _client.SendWelcomeEmailAsync(userdetails.Result.UserId);
                await this._loggerManager.LogInfo(this._logger, null, "User created successfully", objUserLogger).ConfigureAwait(false);
                CustomResponse successResponse = new CustomResponse();
                successResponse.SuccessMessage = "Your account was successfully created.";
                return View("Success", successResponse);
            }
            catch (ApiClient.ApiException ex)
            {
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                objUserLogger.UserName = usermodel.EmailAddress?.Trim().ToLower();
                objUserLogger.AccountNumber = usermodel.AccountNumber;
                string errorMessage = _apiClientHelper.GetErrorMessage(ex);
                usermodel.CustomErrors.AddError(errorMessage);
                await _loggerManager.LogError(this._logger, null, errorMessage, objUserLogger).ConfigureAwait(false);
                return View("../Registration/Register", usermodel);
            }
            catch (Exception ex)
            {
                usermodel.RegisterStep = RegisterViewModel.RegisterFlow.RegistrationStep1;
                objUserLogger.UserName = usermodel.EmailAddress?.Trim().ToLower();
                objUserLogger.AccountNumber = usermodel.AccountNumber;
                string errorMessage = "Something went wrong";
                usermodel.CustomErrors.AddError(errorMessage);
                await this._loggerManager.LogError(this._logger, ex, "Exception in Registration.RegistrationContinue.HttpPost", objUserLogger).ConfigureAwait(false);
            }
            return View("../Registration/Register", usermodel);
        }

        [HttpGet("verify/{token}/email")]
        public async Task<IActionResult> VerifyEmailAddress(string token)
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            CustomResponse customResponse = new CustomResponse();
            try
            {
                if (token == null)
                {
                    customResponse.AddError("Invalid Email Verfication Link");
                    await this._loggerManager.LogError(this._logger, null, "Invalid Email Verfication Link", null).ConfigureAwait(false);
                    return View("VerifySuccess", customResponse);
                }
                else
                {
                    var userDetails = await _emailVerificationClient.VerifyEmailAddressAsync(token);
                    if (userDetails == null || userDetails?.UserId <= 0)
                    {
                        customResponse.AddError("Error while verifying Email Address");
                        await this._loggerManager.LogError(this._logger, null, "Error while verifying Email Address", null).ConfigureAwait(false);
                        return View("VerifySuccess", customResponse);
                    }
                    customResponse.BrandName = userDetails.BrandName;
                    await this._loggerManager.LogInfo(this._logger, null, "Email Verified Successfully", null).ConfigureAwait(false);
                    customResponse.SuccessMessage = "Email Verified Successfully";
                    return View("VerifySuccess", customResponse);
                }
            }
            catch (ApiClient.ApiException ex)
            {
                string errorMessage = _apiClientHelper.GetErrorMessage(ex);
                customResponse.AddError(errorMessage);
                await _loggerManager.LogError(this._logger, null, errorMessage, null).ConfigureAwait(false);
                return View("VerifySuccess", customResponse);
            }
            catch (Exception ex)
            {
                string errorMessage = "Something went wrong.";
                customResponse.AddError(errorMessage);
                await this._loggerManager.LogError(this._logger, ex, "Exception in Registration.VerifyEmailAddress.HttpPost", null).ConfigureAwait(false);
            }
            return View("VerifySuccess", customResponse);
        }

        [HttpGet("register/login")]
        public IActionResult RedirectToLogin()
        {
            string sessionReturnURL = HttpContext.Session.GetString(APIConstants.SessionReturnURL);
            if (sessionReturnURL == null)
                return Redirect(_appConfiguration.DefaultURI);
            else
                return Redirect(sessionReturnURL.ToString());

        }

        //cbe16161
        private bool CreateEncryptCookie(string key, string value, DateTime dateTime)
        {
            CookieOptions _cookieOptions = new CookieOptions { Expires = dateTime };
            var encryptedString = _symmetricKey.EncryptString(CryptoKey, value);
            HttpContext.Response.Cookies.Append(key, encryptedString, _cookieOptions);
            return true;
        }

        private string ReadDecryptCookie(string key)
        {
            var res = HttpContext.Request.Cookies[key];
            if (res != null)
                return _symmetricKey.DecryptString(CryptoKey, res);

            return string.Empty;

        }

        private bool IsBruteForceAttackOnRegister()
        {
            var zpinv = ReadDecryptCookie("_zpinv");
            DateTime expiryDate = DateTime.Now.AddMinutes(Convert.ToInt32(_appConfiguration.ZipInvCookieExpiryDays) * 24 * 60);

            if (string.IsNullOrEmpty(zpinv))
            {
                CreateEncryptCookie("_zpinv", "1", expiryDate);
                CookieOptions _cookieOptions = new CookieOptions { Expires = expiryDate };
                HttpContext.Response.Cookies.Append("_kptg", expiryDate.ToString(), _cookieOptions);

            }
            else
            {
                int cnt = Convert.ToInt32(zpinv);
                if (cnt >= Convert.ToInt32(_appConfiguration.ZipInvCount))
                    return true;
                cnt++;
                var value = Request.Cookies["_kptg"];
                expiryDate = string.IsNullOrEmpty(value) ? expiryDate : Convert.ToDateTime(value);
                CreateEncryptCookie("_zpinv", cnt.ToString(), expiryDate);

            }
            return false;
        }
    }
}