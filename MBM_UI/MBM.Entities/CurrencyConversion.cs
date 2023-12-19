using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBM.Entities
{
	public class CurrencyConversion
	{
		public int ID { get; set; }

		/// <summary>
		/// ISO 4217 Code
		/// </summary>
		public string Code { get; set; }

		public string Symbol { get; set; }
		public string Description { get; set; }

		/// <summary>
		/// All conversion rates are in relation to USD = 1.0000
		/// </summary>
		public decimal ConversionRate { get; set; }

		/// <summary>
		/// The date this conversion rate was set for the currency
		/// </summary>
		public DateTime ActiveDate { get; set; }

		/// <summary>
		/// Convert all record type entities to a new currency
		/// </summary>
		/// <param name="entities">list of entities</param>
		/// <param name="newCurrency">new currency</param>
		/// <returns>newly converted entities</returns>
		public static void ConvertRecordTypes<T>(IList<T> list, CurrencyConversion newCurrency) where T : ICurrencyConvertor
		{
			foreach (ICurrencyConvertor c in list)
			{
				c.ConvertCurrency(newCurrency);
			}
		}

	}

}
