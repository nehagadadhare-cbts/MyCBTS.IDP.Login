using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCBTS.IDP.Login.Utility
{
    public class CreateCookie
    {
        protected readonly IHttpContextAccessor _context;

        public CreateCookie(IHttpContextAccessor context)
        {
            this._context = context;
        }

        /// <summary>
        /// Create myss cookie
        /// </summary>
        /// <param name="token"></param>
        /// <param name="createdby"></param>
        public void CreateMyssCookie(string token, string createdby)
        {
            var existingCookie = _context.HttpContext.Request.Cookies["myss"];

            //dont create a cookie if we have a cookie
            if (existingCookie == null)
            {
                CookieOptions option = new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddDays(1),
                    Domain = "cincinnatibell.com",
                };
                var myssCookie = new Dictionary<string, string>()
                {
                    { "token", token },
                    { "createdby", createdby }
                };
                _context.HttpContext.Response.Cookies.Append("myss", myssCookie.ToCookieString(), option);
            }
        }

        /// <summary>
        /// Create Reward Cookie
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <param name="app"></param>
        public void CreateRewardsCookie(string userAccountId, string app)
        {
            var existingCookie = _context.HttpContext.Request.Cookies["_rwds"];

            //dont create a cookie if we have a cookie
            if (existingCookie == null)
            {
                CookieOptions option = new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddDays(1),
                    Domain = "cincinnatibell.com",
                };
                var rwdCookie = new Dictionary<string, string>()
                {
                    { "uaid", userAccountId.ToString() },
                    { "app", app }
                };
                _context.HttpContext.Response.Cookies.Append("_rwds", Base64Encode(rwdCookie.ToCookieString()), option);
            }
        }

        /// <summary>
        /// create MFACookie
        /// </summary>
        /// <param name="token"></param>
        public void MFACookie(string token)
        {
            var existingCookie = _context.HttpContext.Request.Cookies["_mfa"];

            //dont create a cookie if we have a cookie
            CookieOptions option = new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1),
                Domain = "cincinnatibell.com",
            };
            var rwdCookie = token;
            _context.HttpContext.Response.Cookies.Append("_mfa", Base64Encode(rwdCookie), option);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

    public static class MultivalueCookieExtensions
    {
        public static IDictionary<string, string> FromCookieString(this string legacyCookie)
        {
            return legacyCookie.Split('&').Select(s => s.Split('=')).ToDictionary(kvp => kvp[0], kvp => kvp[1]);
        }

        public static string ToCookieString(this IDictionary<string, string> dict)
        {
            var test = System.Uri.UnescapeDataString(string.Join("&", dict.Select(kvp => string.Join("=", kvp.Key, kvp.Value))));
            return test;
        }
    }
}