using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBM.Entities;

namespace MBM.Entities
{
	public interface ICurrencyConvertor
	{
		/// <summary>
		/// Existing Currency
		/// </summary>
		CurrencyConversion CurrentCurrency { get; set; }

		/// <summary>
		/// Convert all money values to new currency
		/// </summary>
		/// <param name="conv"></param>
		void ConvertCurrency(CurrencyConversion conv);
	}
}
