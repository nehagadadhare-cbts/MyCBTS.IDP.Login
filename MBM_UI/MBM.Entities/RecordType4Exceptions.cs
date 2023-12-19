using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;


namespace MBM.Entities
{
	public class RecordType4Exceptions : IComparable, MBM.Entities.IMBMPrinter, MBM.Entities.ICurrencyConvertor
	{
		#region Data

		/// <summary>
		/// Unique identifier
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Monthly invoice number
		/// </summary>
		public int InvoiceID { get; set; }

		/// <summary>
		/// Invoice Number
		/// </summary>
		public string InvoiceNumber { get; set; }

		/// <summary>
		/// Invoice Type
		/// </summary>
		//public InvoiceTypeEnum InvoiceType { get; set; } //ER#6752

		/// <summary>
		/// RecordType of entry
		/// </summary>
		public int? RecordType { get; set; }

		/// <summary>
		/// Number identifing the customer
		/// </summary>
		public string CustomerNumber { get; set; }

		/// <summary>
		/// Contact Phone Number
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Type of call (outbound, etc.)
		/// </summary>
		public string CallType { get; set; }

		/// <summary>
		/// Total number of calls for this entry
		/// </summary>
		public int? TotalCallCount { get; set; }

		/// <summary>
		/// Total duration of calls
		/// </summary>
		public decimal? TotalCallDuration { get; set; }

		/// <summary>
		/// Total usage
		/// </summary>
		public string UsageTotal { get; set; }

		/// <summary>
		/// International usage
		/// </summary>
		public string InterIntlUsage { get; set; }

		/// <summary>
		/// State fees
		/// </summary>
		public decimal? State { get; set; }

		/// <summary>
		/// Local fees
		/// </summary>
		public decimal? Local { get; set; }

		/// <summary>
		/// Federal fees
		/// </summary>
		public decimal? Federal { get; set; }

		/// <summary>
		/// USF fees
		/// </summary>
		public decimal? USF { get; set; }

		/// <summary>
		/// ARF fees
		/// </summary>
		public decimal? ARF { get; set; }

		/// <summary>
		/// Identifier of the currency conversion rate
		/// </summary>
		public int CurrencyConversionID { get; set; }

		#endregion

		#region Output

		static List<KeyValuePair<string, string>> layout = new List<KeyValuePair<string, string>>()
		{
			new KeyValuePair<string, string>("ID", "ID"),
			new KeyValuePair<string, string>("Invoice Number", "InvoiceNumber"),
			new KeyValuePair<string, string>("RecordType", "RecordType"),
			new KeyValuePair<string, string>("PhoneNumber", "PhoneNumber"),
			new KeyValuePair<string, string>("CallType", "CallType"),
			new KeyValuePair<string, string>("TotalCallCount", "TotalCallCount"),
			new KeyValuePair<string, string>("TotalCallDuration", "TotalCallDuration"),
			new KeyValuePair<string, string>("UsageTotal", "UsageTotal"),
			new KeyValuePair<string, string>("Inter/IntlUsage", "InterIntlUsage"),
			new KeyValuePair<string, string>("State", "State"),
			new KeyValuePair<string, string>("Local", "Local"),
			new KeyValuePair<string, string>("Federal", "Federal"),
			new KeyValuePair<string, string>("USF", "USF"),
			new KeyValuePair<string, string>("ARF", "ARF"),
		};

		static public List<string> MBMHeader
		{
			get
			{
				List<string> header = new List<string>();

				foreach (KeyValuePair<string, string> kvp in layout)
				{
					header.Add(kvp.Key);
				}

				return header;
			}
		}

		public OrderedDictionary MBMOutput
		{
			get
			{
				OrderedDictionary outformat = new OrderedDictionary();

				foreach (KeyValuePair<string, string> kvp in layout)
				{
					Type t = this.GetType();
					PropertyInfo pi = t.GetProperty(kvp.Value, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
					if (pi != null)
					{
						object value = pi.GetValue(this, null);
						outformat.Add(kvp.Key, ((value != null) ? value.ToString() : string.Empty));
					}
					else
					{
						outformat.Add(kvp.Key, "**ERROR**");
					}
				}

				return outformat;
			}
		}

		#endregion

		#region CurrencyConvertor

		/// <summary>
		/// Existing Currency
		/// </summary>
		public CurrencyConversion CurrentCurrency { get; set; }

		/// <summary>
		/// Convert all money values to new currency
		/// </summary>
		/// <param name="conv"></param>
		public void ConvertCurrency(CurrencyConversion conv)
		{
			if (CurrentCurrency == null || conv == null)
			{
				throw new Exception("Cannot convert currency. Missing converter");
			}
			else if (CurrentCurrency.ID == conv.ID)
			{
				return;
			}

			if (State.HasValue)
			{
				State = Decimal.Round(((State.Value / CurrentCurrency.ConversionRate) * conv.ConversionRate), 2);
			}

			if (Local.HasValue)
			{
				Local = Decimal.Round(((Local.Value / CurrentCurrency.ConversionRate) * conv.ConversionRate), 2);
			}

			if (Federal.HasValue)
			{
				Federal = Decimal.Round(((Federal.Value / CurrentCurrency.ConversionRate) * conv.ConversionRate), 2);
			}

			if (USF.HasValue)
			{
				USF = Decimal.Round(((USF.Value / CurrentCurrency.ConversionRate) * conv.ConversionRate), 2);
			}

			if (ARF.HasValue)
			{
				ARF = Decimal.Round(((ARF.Value / CurrentCurrency.ConversionRate) * conv.ConversionRate), 2);
			}

			CurrencyConversionID = conv.ID;
		}

		#endregion

		#region Comparer

		public enum Sort
		{
			/// <summary>
			/// Record Type identifier
			/// </summary>
			RecordType = 1,
			/// <summary>
			/// Invoice Number
			/// </summary>
			InvoiceNumber = 2,
		}

		public static System.Collections.Generic.IComparer<RecordType4Exceptions> GetComparer(Sort field)
		{
			switch (field)
			{
				case Sort.InvoiceNumber:
					return new InvoiceComparer();
				case Sort.RecordType:
					return new RecordTypeComparer();
				default:
					return new RecordTypeComparer();
			}
		}


		protected sealed class RecordTypeComparer : System.Collections.Generic.IComparer<RecordType4Exceptions>
		{
			int System.Collections.Generic.IComparer<RecordType4Exceptions>.Compare(RecordType4Exceptions lhs, RecordType4Exceptions rhs)
			{
				if (lhs.RecordType.HasValue && rhs.RecordType.HasValue)
				{
					return lhs.RecordType.Value.CompareTo(rhs.RecordType.Value);
				}
				else if (lhs.RecordType.HasValue)
				{
					return 1;
				}
				else if (rhs.RecordType.HasValue)
				{
					return -1;
				}
				else
				{
					return 0;
				}
			}
		}
		protected sealed class InvoiceComparer : System.Collections.Generic.IComparer<RecordType4Exceptions>
		{
			int System.Collections.Generic.IComparer<RecordType4Exceptions>.Compare(RecordType4Exceptions lhs, RecordType4Exceptions rhs)
			{
				return lhs.InvoiceID.CompareTo(rhs.InvoiceID);
			}
		}


		/// <summary>
		/// Compare objects of the same type
		/// </summary>
		/// <param name="rhs">right-hand side of compare</param>
		/// <returns>1 on greater; 0 on equal; -1 on lesser</returns>
		int IComparable.CompareTo(Object rhs)
		{
			if (!(rhs is RecordType4Exceptions))
			{
				throw new ArgumentException("Argument is not an RecordType4Exceptions", "rhs");
			}
			RecordType4Exceptions r = (RecordType4Exceptions)rhs;
			return this.CompareTo(r);
		}

		/// <summary>
		/// Compare objects of the same type
		/// </summary>
		/// <param name="rhs">right-hand side of compare</param>
		/// <returns>1 on greater; 0 on equal; -1 on lesser</returns>
		public int CompareTo(RecordType4Exceptions rhs)
		{
			return this.InvoiceID.CompareTo(rhs.InvoiceID);
		}

		/// <summary>
		/// Less than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than the right</returns>
		public static bool operator <(RecordType4Exceptions left, RecordType4Exceptions right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>
		/// Less than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than or equal to the right</returns>
		public static bool operator <=(RecordType4Exceptions left, RecordType4Exceptions right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>
		/// Greater than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than the right</returns>
		public static bool operator >(RecordType4Exceptions left, RecordType4Exceptions right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>
		/// Greater than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than or equal to the right</returns>
		public static bool operator >=(RecordType4Exceptions left, RecordType4Exceptions right)
		{
			return left.CompareTo(right) >= 0;
		}


		#endregion

		#region Serializer

		/// <summary>
		/// Deserializes an object(s) into this class
		/// </summary>
		/// <param name="ms">XML memory stream</param>
		/// <returns>list of objects within the stream</returns>
		/// <remarks>this method is provides to ease transition of the same objects across namespace</remarks>
		public static List<RecordType4Exceptions> DeserializeFromXML(
			MemoryStream ms
			)
		{
			XmlSerializer deserializer = new XmlSerializer(typeof(List<RecordType4Exceptions>));
			List<RecordType4Exceptions> objects;
			objects = (List<RecordType4Exceptions>)deserializer.Deserialize(ms);
			ms.Close();

			return objects;
		}

		#endregion
	}
}
