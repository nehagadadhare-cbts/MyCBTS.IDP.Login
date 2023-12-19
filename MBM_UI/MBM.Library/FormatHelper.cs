using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBM.Library
{
    public class FormatHelper
    {
		public static string ConvertToDate_YYYYMMDD(string dateToFormat)
		{
			DateTime dateConv;

			if (DateTime.TryParse(dateToFormat, out dateConv))
			{
				return ConvertToDate_YYYYMMDD(dateConv);
			}
			else
			{
				return string.Empty;
			}
		}

        public static string ConvertToDate_YYYYMMDD(DateTime dateToFormat)
        {
            string newDateString = String.Empty;

            // Several standards
            // http://www.csharp-examples.net/string-format-datetime/
            newDateString = String.Format("{0:MMddyyyy}", dateToFormat);  

            return newDateString;
        }

        public static string ConvertToPhoneNumberWithHyphens(string phone)
        {
            return String.Format("{0:###\\.###\\.####}", phone);
        }
    }
}