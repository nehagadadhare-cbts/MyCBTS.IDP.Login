namespace MyCBTS.IDP.Login.Mapping
{
    public class APIConstants
    {
        public const string Application = "Application";
        public const string UserId = "UserID";
        public const string Password = "Password";
        public const string MediaType = "application/json";

        #region SessionConstants

        public const string SessionReturnURL = "_returnurl";
        public const string SessionClient = "_client";

        #endregion SessionConstants

        #region Identity

        public const string GetClient = "identityclients/{0}/";
        public const string GetAllClients = "identityclients";
        public const string GetAllScopes = "identityscopes";
        public const string CreateToken = "identitytokens";
        public const string CreateJsonToken = "identitytokens/jsontoken";
        public const string CreateRefreshToken = "IdentityToken/CreateRefreshToken";
        public const string MarketPlaceRegistration = "/api/IDP/RegisterMarketPlace";
        public const string GetToken = "identitytokens";
        public const string GetRefreshToken = "identitytokens/{tokenKey}/refreshtoken";
        public const string RemoveRefreshTokenAsync = "identitytokens/{tokenKey}/refreshtoken";
        public const string RemoveReferenceTokenAsync = "IdentityToken/RemoveReferenceTokenAsync";
        public const string RemoveReferenceTokenbyClientandSubjectAsync = "IdentityToken/RemoveReferenceTokenbyClientandSubjectAsync";
        public const string RemoveRefreshTokenbyClientandSubjectAsync = "identitytokens/clients/{clientid}/subjects/{subjectid}/refreshtoken";
        public const string UpdatefreshToken = "IdentityToken/UpdateRefreshToken";

        #endregion Identity

        #region Logger

        public const string LogException = "logs";

        #endregion Logger

        #region GetUser

        public const string GetUserByUserName = "users/{0}";
        public const string GetUserByUserId = "users/{0}";
        public const string GetEmailByAccountNumber = "user/getemails/{0}";

        #endregion GetUser

        #region registration

        public const string ValidateAccountNumber = "account/validateAccount/{0}";
        public const string GetCBADBills = "accounts/{0}/cbad/bills";
        public const string GetCBTSBills = "accounts/{0}/cbts/bills";
        public const string GetCBTSCABills = "accounts/{0}/cbtsca/bills";
        public const string GetCBTSUKBills = "accounts/{0}/cbtsuk/bills";
        public const string GetCRISBills = "accounts/{0}/cris/bills";
        public const string GetCABSBills = "accounts/{0}/cabs/bills";
        public const string CreateUser = "users";
        public const string SendVerifyEmailAddressLink = "send/verifyemail/{0}/email";
        public const string VerifyEmailAddress = "verify/verifyemail/{0}/email";
        public const string AccountStatus = "LIVE";

        #endregion registration

        #region Forgot Password

        public const string SendPasswordResetMail = "send/resetemail/{0}/email";
        public const string VerifyPwdResetEmail = "verify/resetemail/{0}/email";
        public const string ResetPassword = "users/change_password";
        public const string Sessiontoken = "_token";

        #endregion Forgot Password

        #region ValidateUser

        public const string ValidateUser = "user/validateuser";

        #endregion ValidateUser

        #region UpdateUser

        public const string UpdateLastLoginDate = "users/{0}/last_login";

        #endregion UpdateUser
    }
}