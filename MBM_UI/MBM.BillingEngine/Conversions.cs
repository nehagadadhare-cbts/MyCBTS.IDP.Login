using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MBM.BillingEngine
{
	static class Conversions
	{
		public static List<string> ConvertToCSV(DataTable loyaltyResults, string delimiter)
		{
			List<string> lines = new List<string>();

			//-Add Headers--
			string headerLine = "";
			foreach (DataColumn c in loyaltyResults.Columns)
			{
				headerLine += c.ColumnName + delimiter + " ";
			}
			lines.Add(headerLine);

			//-Add Data--
			foreach (DataRow r in loyaltyResults.Rows)
			{
				string nextLine = "";

				for (int i = 0; i < r.ItemArray.Count(); i++)
				{
					nextLine += "\"" + r.ItemArray[i] + "\"" + delimiter + " ";
				}

				lines.Add(nextLine);
			}

			return lines;
		}
	}
}
