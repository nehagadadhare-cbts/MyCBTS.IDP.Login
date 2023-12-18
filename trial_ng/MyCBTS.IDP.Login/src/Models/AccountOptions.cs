using System;

namespace MyCBTS.IDP.Login.Models
{
    public class AccountOptions
    {
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(7);
        public static bool ShowLogoutPrompt = true;
        public static bool AutomaticRedirectAfterSignOut = true;
    }
}