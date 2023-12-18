using System;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiClient = MyCBTS.Api.Client;
using MyCBTS.IDP.Login.Data;
using MyCBTS.IDP.Login.Extensions;
using MyCBTS.IDP.Login.Models;
using MyCBTS.IDP.Login.Logger;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using MyCBTS.IDP.Login.Helpers;
using System.Text;
using Microsoft.AspNetCore.Http;
using MyCBTS.IDP.Login.Mapping;


namespace MyCBTS.IDP.Login.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly UserStore _iUserStore;
        private readonly IMapper _iMapper;
        private readonly ILogger<ForgotPasswordController> _logger;
        private readonly ILoggerManager _loggerManager;
        private readonly ApiClient.IEmailVerificationClient _emailVerificationClient;
        // private readonly ApiClient.ICBTSServiceClient _client;
        private readonly ApiClientHelper _apiClientHelper;
        private readonly ApiClient.IUserClient _userClient;
        public ForgotPasswordController(UserStore userStore,
                                        ILoggerManager loggerManager,
                                        IMapper mapper,
                                       ApiClient.IEmailVerificationClient emailVerificationClient,
                                       ApiClient.IUserClient userClient,
                                        ILogger<ForgotPasswordController> logger,
                                        ApiClientHelper apiClientHelper)
        {
            _iUserStore = userStore;
            _iMapper = mapper;
            _logger = logger;
            _userClient = userClient;
            _emailVerificationClient = emailVerificationClient;
            _loggerManager = loggerManager;
            _apiClientHelper = apiClientHelper;
        }

        [HttpGet("forget/password")]
        public IActionResult Index()
        {
            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();
            return View(forgotPasswordModel);
        }

        [HttpPost("forget/validateusername")]
        public async Task<IActionResult> ValidateEmail(ForgotPasswordModel forgotPasswordModel)
        {
            var objUserLogger = new UserLogger();
            CustomResponse successResponse = new CustomResponse();
            try
            {
                ModelState.Remove("Password");
                ModelState.Remove("ReEnterPassword");
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Forgot Password";
                    await this._loggerManager.LogError(this._logger, null, "Model state is invalid at ForgotPassword.ValidateEmail.HttpPost", null).ConfigureAwait(false);
                    return View("Index", forgotPasswordModel);
                }
                var emailDetails = _iUserStore.FindByEmailAsync(forgotPasswordModel?.EmailAddress?.Trim().ToLower(), default(CancellationToken));
                if (emailDetails == null || string.IsNullOrEmpty(emailDetails.Result?.Email))
                {
                    await this._loggerManager.LogInfo(this._logger, null, "Email not found while reseting password", null).ConfigureAwait(false);
                    successResponse.SuccessMessage = "Password reset email sent";
                    return View("Success", successResponse);
                }

                var emailSent = await this._emailVerificationClient.SendPasswordResetMailAsync(emailDetails.Result.UserId);
                if (!emailSent)
                {
                    await this._loggerManager.LogError(this._logger, null, "Error while sending password reset email", null).ConfigureAwait(false);
                    forgotPasswordModel?.CustomErrors.AddError("something went wrong");
                    return View("Index", forgotPasswordModel);
                }

                objUserLogger.UserName = emailDetails.Result.Email;
                await this._loggerManager.LogInfo(this._logger, null, "Password reset email sent", objUserLogger).ConfigureAwait(false);
                successResponse.SuccessMessage = "Password reset email sent";
                return View("Success", successResponse);
            }
            catch (ApiClient.ApiException ex)
            {
                objUserLogger.UserName = forgotPasswordModel.EmailAddress?.Trim().ToLower();
                string errorMessage = _apiClientHelper.GetErrorMessage(ex);
                forgotPasswordModel.CustomErrors.AddError(errorMessage);
                await _loggerManager.LogError(this._logger, null, errorMessage, objUserLogger).ConfigureAwait(false);
                return View("Index", forgotPasswordModel);
            }
            catch (Exception ex)
            {
                string errorMessage = "Something went wrong.";
                forgotPasswordModel.CustomErrors.AddError(errorMessage);
                objUserLogger.UserName = forgotPasswordModel.EmailAddress.Trim().ToLower();
                await this._loggerManager.LogError(this._logger, ex, "Exception in ForgotPassword.ValidateEmail.HttpPost", objUserLogger).ConfigureAwait(false);
            }
            return View("Index", forgotPasswordModel);
        }

        [HttpGet("verify/{token}/token")]
        public async Task<IActionResult> VerifyPwdResetEmail(string token)
        {
            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();
            var objUserLogger = new UserLogger();

            
            try
            {
                if (token == null)
                {
                    ViewBag.Title = "Forgot Password";
                    forgotPasswordModel.CustomErrors.AddError("Invalid Reset Email Link");
                    await this._loggerManager.LogError(this._logger, null, "Invalid Reset Email Link", null).ConfigureAwait(false);
                    return View("Index", forgotPasswordModel);
                }
                var userDetails = await _emailVerificationClient.VerifyResetPasswordVerificationCodeAsync(token);

                if (userDetails == null || userDetails?.UserId <= 0)
                {
                    await this._loggerManager.LogError(this._logger, null, "Error while verifying password reset email", null).ConfigureAwait(false);
                    forgotPasswordModel.CustomErrors.AddError("Something went wrong");
                    return View("Index", forgotPasswordModel);
                }

                HttpContext.Session.SetString(APIConstants.Sessiontoken, token);

                if (userDetails.UserId > 0)
                {

                    forgotPasswordModel.EmailAddress = userDetails.Email;
                    forgotPasswordModel.UserId = userDetails.UserId;
                    forgotPasswordModel.VerfiedResetPwdEmail = true;
                    forgotPasswordModel.BrandName = userDetails.BrandName;
                    forgotPasswordModel.token = token;
                    objUserLogger.UserName = userDetails.Email;
                    objUserLogger.UserID = userDetails.UserId;

                    await this._loggerManager.LogInfo(this._logger, null, "Password reset link verified", objUserLogger).ConfigureAwait(false);
                    return View("ResetPassword", forgotPasswordModel);
                }
            }
            catch (ApiClient.ApiException ex)
            {
                string errorMessage = _apiClientHelper.GetErrorMessage(ex);
                forgotPasswordModel.CustomErrors.AddError(errorMessage);
                await _loggerManager.LogError(this._logger, null, errorMessage, null).ConfigureAwait(false);
                return View("Index", forgotPasswordModel);
            }
            catch (Exception ex)
            {
                string errorMessage = "Something went wrong.";
                forgotPasswordModel.CustomErrors.AddError(errorMessage);
                await this._loggerManager.LogError(this._logger, ex, "Exception in ForgotPassword.VerifyPwdResetEmail.HttpPost", null).ConfigureAwait(false);
            }
            return View("Index", forgotPasswordModel);
        }

        [HttpPost("forget/resetpassword")]
        public async Task<IActionResult> ResetPassword(ForgotPasswordModel forgotPasswordModel)
        {
            var objUserLogger = new UserLogger();
            
            try
            {
                if (HttpContext.Session.GetString(APIConstants.Sessiontoken) != forgotPasswordModel.token)
                {
                    await this._loggerManager.LogError(this._logger, null, "This user tried to update the password with wrong token." + " Correct token: " + HttpContext.Session.GetString(APIConstants.Sessiontoken) + " Wrong token: " + forgotPasswordModel.token, objUserLogger).ConfigureAwait(false);
                    forgotPasswordModel.CustomErrors.AddError("Password reset failed");
                    return View("ResetPassword", forgotPasswordModel);
                }

                ModelState.Remove("EmailAddress");
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Forgot Password";
                    await this._loggerManager.LogError(this._logger, null, "Model state is invalid at ForgotPassword.ResetPassword.HttpPost", objUserLogger).ConfigureAwait(false);
                    return View("ResetPassword", forgotPasswordModel);
                }

                var userDetails = await _emailVerificationClient.VerifyResetPasswordVerificationCodeAsync(forgotPasswordModel.token);

                if (userDetails == null || userDetails?.UserId <= 0)
                {
                    await this._loggerManager.LogError(this._logger, null, "Error while retrieving the user details from token", null).ConfigureAwait(false);
                    forgotPasswordModel.CustomErrors.AddError("Something went wrong");
                    return View("ResetPassword", forgotPasswordModel);
                }

                objUserLogger.UserName = userDetails.Email;
                objUserLogger.UserID = userDetails.UserId;

                ApiClient.UserLogin pwdReset = new ApiClient.UserLogin()
                {
                    Email = userDetails.Email,
                    Password = forgotPasswordModel.Password
                };
                bool isPasswordReset = await _userClient.ChangePasswordAsync(userDetails.UserId, pwdReset);

                if (!isPasswordReset)
                {
                    await this._loggerManager.LogError(this._logger, null, "Error while resetting password", objUserLogger).ConfigureAwait(false);
                    forgotPasswordModel.CustomErrors.AddError("something went wrong");
                    return View("ResetPassword", forgotPasswordModel);
                }

                CustomResponse successResponse = new CustomResponse();
                successResponse.BrandName = forgotPasswordModel.BrandName;
                successResponse.SuccessMessage = "Your password reset done successfully.";
                await this._loggerManager.LogInfo(this._logger, null, "password reset successfully", objUserLogger).ConfigureAwait(false);
                return View("VerifySuccess", successResponse);
            }
            catch (ApiClient.ApiException ex)
            {
                string errorMessage = _apiClientHelper.GetErrorMessage(ex);
                forgotPasswordModel.CustomErrors.AddError(errorMessage);
                await _loggerManager.LogError(this._logger, null, errorMessage, objUserLogger).ConfigureAwait(false);
                return View("ResetPassword", forgotPasswordModel);
            }
            catch (Exception ex)
            {
                string errorMessage = "Something went wrong.";
                forgotPasswordModel.CustomErrors.AddError(errorMessage);
                //objUserLogger.UserName = HttpContext.Session.GetString(APIConstants.SEmailAddress).Trim().ToLower();
                await this._loggerManager.LogError(this._logger, ex, "Exception in ForgotPassword.ResetPassword.HttpPost", objUserLogger).ConfigureAwait(false);
            }
            return View("ResetPassword", forgotPasswordModel);
        }

        [HttpGet("forget/username")]
        public IActionResult ForgotUserName()
        {
            ForgotUserNameModel forgotUserNameModel = new ForgotUserNameModel();
            return View("ForgotUserName", forgotUserNameModel);
        }

        [HttpPost("forget/username")]
        public async Task<IActionResult> GetEmailsByAccountNumber(ForgotUserNameModel account)
        {
            try
            {
                var objUserLogger = new UserLogger();
                objUserLogger.AccountNumber = account.AccountNumber;
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Forgot Password";
                    await this._loggerManager.LogError(this._logger, null, "Model state is invalid at ForgotPassword.GetEmailsByAccountNumber.HttpPost", objUserLogger).ConfigureAwait(false);
                    return View("ForgotUserName", account);
                }
                var userNameList = await _userClient.GetEmailsByAccountNumberAsync(account.AccountNumber?.Trim());
                if (userNameList == null || userNameList?.Count == 0)
                {
                    await this._loggerManager.LogInfo(this._logger, null, "Invalid Account Number", objUserLogger).ConfigureAwait(false);
                    account.CustomErrors.AddError("Invalid Account Number");
                    return View("ForgotUserName", account);
                }
                account.UserNameList = new List<string>();
                foreach (var user in userNameList)
                {
                    string[] arr = user.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                    StringBuilder MaskUserName = new StringBuilder();
                    if (arr[0].Length > 4)
                    {
                        MaskUserName.Append(arr[0].Substring(0, 4));
                        for (int i = 0; i < arr[0].Length - 4; i++)
                        {
                            MaskUserName.Append("*");
                        }
                        MaskUserName.Append(string.Format("@{0}", arr[1]));
                    }
                    else
                    {
                        string mask = arr[0];
                        mask = mask.Replace(mask[mask.Length - 1], '*');
                        mask = mask + "@" + arr[1];
                        MaskUserName.Append(mask);
                    }
                    account.UserNameList.Add(MaskUserName.ToString());
                }
                await this._loggerManager.LogInfo(this._logger, null, "Retrieved usernames by account number", objUserLogger).ConfigureAwait(false);
                return View("UserNameDetails", account);
            }
            catch (ApiClient.ApiException ex)
            {
                string errorMessage = string.Empty;
                if (ex.StatusCode == (int)HttpStatusCode.NoContent)
                {
                    errorMessage = "Invalid Account Number";
                }
                else
                {
                    errorMessage = _apiClientHelper.GetErrorMessage(ex);
                }
                account.CustomErrors.AddError(errorMessage);
                await _loggerManager.LogError(this._logger, null, errorMessage, null).ConfigureAwait(false);
                return View("ForgotUserName", account);
            }
            catch (Exception ex)
            {
                string errorMessage = "Something went wrong";
                account.CustomErrors.AddError(errorMessage);
                await this._loggerManager.LogError(this._logger, ex, "Exception in ForgotPassword.GetEmailsByAccountNumber.HttpPost", null).ConfigureAwait(false);
            }
            return View("ForgotUserName", account);
        }
    }
}