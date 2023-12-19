using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MBM.Entities
{
	public class CanadianTax
	{
		public string Province { get; set; }
		public decimal? GST { get; set; }
		public decimal? QST { get; set; }
		public decimal? PST { get; set; }
		public decimal? HST_Recoverable { get; set; }
		public decimal? HST_NonRecoverable { get; set; }
	}

	/// <summary>
	/// Provides a list of Canadian Taxes
	/// </summary>
	public class CanadianTaxCollection : Collection<CanadianTax>
	{
		/// <summary>
		/// Return the found item matching the results. If no item current exists it will create
		/// a blank item
		/// </summary>
		/// <param name="province">Canadian Province</param>
		/// <returns>file entry</returns>
		public CanadianTax Item(string province)
		{
			CanadianTax fileFound = Items.First<CanadianTax>(
				delegate(CanadianTax u)
				{
					return (u.Province == province);
				});

			return fileFound;
		}

	}
}
