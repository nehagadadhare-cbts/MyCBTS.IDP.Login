using System;
using System.Diagnostics;

namespace MyCBTS.IDP.Login.Extensions
{
    internal static class DateTimeExtensions
    {
        [DebuggerStepThrough]
        public static bool HasExceeded(this DateTime creationTime, int seconds)
        {
            return (DateTime.Now > creationTime.AddSeconds(seconds));
        }

        [DebuggerStepThrough]
        public static int GetLifetimeInSeconds(this DateTime creationTime)
        {
            return ((int)(DateTime.Now - creationTime).TotalSeconds);
        }

        [DebuggerStepThrough]
        public static bool HasExpired(this DateTime? expirationTime)
        {
            if (expirationTime.HasValue &&
                expirationTime.Value.HasExpired())
            {
                return true;
            }

            return false;
        }

        [DebuggerStepThrough]
        public static bool HasExpired(this DateTime expirationTime)
        {
            if (expirationTime < DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}