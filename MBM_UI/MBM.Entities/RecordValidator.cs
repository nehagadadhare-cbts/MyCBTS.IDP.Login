using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBM.Entities
{
	public class RecordValidator
	{
		#region Data

		/// <summary>
		/// Unique Identifier
		/// </summary>
		public string BAN { get; set; }

		/// <summary>
		/// Type of Record Entry
		/// </summary>
		public decimal RecordTypeSum { get; set; }

		/// <summary>
		/// Type of Record Entry
		/// </summary>
		public decimal RecordType3Sum { get; set; }

		/// <summary>
		/// Type of Record Entry
		/// </summary>
		public decimal RecordType5Sum { get; set; }

		/// <summary>
		/// Has this BAN been successfully validated
		/// </summary>
		public bool? Validated { get; set; }

		#endregion

	}
}
