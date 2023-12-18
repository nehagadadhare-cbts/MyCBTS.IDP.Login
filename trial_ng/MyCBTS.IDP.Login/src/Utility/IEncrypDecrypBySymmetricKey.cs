using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login.Utility
{
    public interface IEncrypDecrypBySymmetricKey
    {
        public string EncryptString(string key, string plainText);
        public string DecryptString(string key, string cipherText);
    }
}
