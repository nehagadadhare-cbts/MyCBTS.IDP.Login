﻿using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace MyCBTS.IDP.Login.Extensions
{
    internal static class StringExtensions
    {
        [DebuggerStepThrough]
        public static string ToSpaceSeparatedString(this IEnumerable<string> list)
        {
            if (list == null)
            {
                return "";
            }

            var sb = new StringBuilder(100);

            foreach (var element in list)
            {
                sb.Append(element + " ");
            }

            return sb.ToString().Trim();
        }

        [DebuggerStepThrough]
        public static IEnumerable<string> FromSpaceSeparatedString(this string input)
        {
            input = input.Trim();
            return input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static List<string> ParseScopesString(this string scopes)
        {
            if (scopes.IsMissing())
            {
                return null;
            }

            scopes = scopes.Trim();
            var parsedScopes = scopes.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();

            if (parsedScopes.Any())
            {
                parsedScopes.Sort();
                return parsedScopes;
            }

            return null;
        }

        [DebuggerStepThrough]
        public static bool IsMissing(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        [DebuggerStepThrough]
        public static bool IsMissingOrTooLong(this string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            if (value.Length > maxLength)
            {
                return true;
            }

            return false;
        }

        [DebuggerStepThrough]
        public static bool IsPresent(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        [DebuggerStepThrough]
        public static string EnsureLeadingSlash(this string url)
        {
            if (!url.StartsWith("/"))
            {
                return "/" + url;
            }

            return url;
        }

        [DebuggerStepThrough]
        public static string EnsureTrailingSlash(this string url)
        {
            if (!url.EndsWith("/"))
            {
                return url + "/";
            }

            return url;
        }

        [DebuggerStepThrough]
        public static string RemoveLeadingSlash(this string url)
        {
            if (url != null && url.StartsWith("/"))
            {
                url = url.Substring(1);
            }

            return url;
        }

        [DebuggerStepThrough]
        public static string RemoveTrailingSlash(this string url)
        {
            if (url != null && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        [DebuggerStepThrough]
        public static string CleanUrlPath(this string url)
        {
            if (String.IsNullOrWhiteSpace(url)) url = "/";

            if (url != "/" && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        [DebuggerStepThrough]
        public static bool IsLocalUrl(this string url)
        {
            return
                !String.IsNullOrEmpty(url) &&

                // Allows "/" or "/foo" but not "//" or "/\".
                ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) ||

                // Allows "~/" or "~/foo".
                (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        [DebuggerStepThrough]
        public static string AddQueryString(this string url, string query)
        {
            if (!url.Contains("?"))
            {
                url += "?";
            }
            else if (!url.EndsWith("&"))
            {
                url += "&";
            }

            return url + query;
        }

        [DebuggerStepThrough]
        public static string AddQueryString(this string url, string name, string value)
        {
            return url.AddQueryString(name + "=" + UrlEncoder.Default.Encode(value));
        }

        [DebuggerStepThrough]
        public static string AddHashFragment(this string url, string query)
        {
            if (!url.Contains("#"))
            {
                url += "#";
            }

            return url + query;
        }

        [DebuggerStepThrough]
        public static NameValueCollection ReadQueryStringAsNameValueCollection(this string url)
        {
            if (url != null)
            {
                var idx = url.IndexOf('?');
                if (idx >= 0)
                {
                    url = url.Substring(idx + 1);
                }
                var query = QueryHelpers.ParseNullableQuery(url);
                if (query != null)
                {
                    return query.AsNameValueCollection();
                }
            }

            return new NameValueCollection();
        }

        public static string GetOrigin(this string url)
        {
            if (url != null && (url.StartsWith("http://") || url.StartsWith("https://")))
            {
                var idx = url.IndexOf("//", StringComparison.Ordinal);
                if (idx > 0)
                {
                    idx = url.IndexOf("/", idx + 2, StringComparison.Ordinal);
                    if (idx >= 0)
                    {
                        url = url.Substring(0, idx);
                    }
                    return url;
                }
            }

            return null;
        }

        public static string DecryptSecret(this string cipherText)
        {
            var keys = GetKeys();

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(keys[0]);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            var password = new PasswordDeriveBytes
            (
                keys[1], null
            );

            byte[] keyBytes = password.GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor
            (
                keyBytes,
                initVectorBytes
            );

            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read
            (
                plainTextBytes, 0, plainTextBytes.Length
            );

            memoryStream.Close();
            cryptoStream.Close();

            string plainText = Encoding.UTF8.GetString
            (
                plainTextBytes, 0, decryptedByteCount
            );

            return plainText;
        }

        private static List<string> GetKeys()
        {
            var keys = new List<string>();

            var path = Path.Combine(AppContext.BaseDirectory, "ConfigKey.txt");
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    keys.Add(reader.ReadLine());
                }
            }

            return keys;
        }
    }
}