using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyCBTS.IDP.Login
{
    internal class DuendeHelper
    {
        internal static string ReadFileContent(string path)
        {
            var res = File.ReadAllText(path);

            return res;
        }
    }
}
