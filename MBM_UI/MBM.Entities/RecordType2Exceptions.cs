using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;


namespace MBM.Entities
{
	public class RecordType2Exceptions : IComparable, MBM.Entities.IMBMPrinter, MBM.Entities.ICurrencyConvertor
	{
		#region Data

		/// <summary>
		/// Record Type
		/// </summary>
		public string RecordType { get; set; }

		/// <summary>
		/// Billing Account Number
		/// </summary>
		public string BAN { get; set; }

		/// <summary>
		/// Monthly invoice number
		/// </summary>
		public int? InvoiceID { get; set; }

		/// <summary>
		/// Invoice Number
		/// </summary>
		public string InvoiceNumber { get; set; }

		/// <summary>
		/// Invoice Number displayed in output friendly format
		/// </summary>
		/// <remarks>
		/// Invoice Display was created because the Invoice Number needed for the Canadian bill
		/// is a compound of the BAN + trailing edge of the InvoiceNumber. If the BAN is CA202002
		/// and the Invoice is CA10001 then the InvoiceDisplay for Canada is CA20200210001.
		/// </remarks>
		public string InvoiceDisplay
		{
			get
			{
                return InvoiceNumber;

                //if (InvoiceType == InvoiceTypeEnum.Canada)
                //{
                //    return BAN + InvoiceNumber.Substring(2);
                //}
                //else
                //{
                //    return InvoiceNumber;
                //}
			}
		}

		/// <summary>
		/// Invoice Type
		/// </summary>
		//public InvoiceTypeEnum InvoiceType { get; set; }  //ER#6752

		/// <summary>
		/// Asset Identifier
		/// </summary>
		public string AssetSearchCode { get; set; }

		/// <summary>
		/// GE Single Sign-On
		/// </summary>
		public string SSO { get; set; }

		/// <summary>
		/// Date of the charge
		/// </summary>
		public string ChargeDate { get; set; }

		/// <summary>
		/// Description of the charge
		/// </summary>
		public string ChargeDescription { get; set; }

		/// <summary>
		/// Category/Type of the charge
		/// </summary>
		public string ChargeType { get; set; }

		/// <summary>
		/// When did the service start
		/// </summary>
		public DateTime? ServiceStartDate { get; set; }

		/// <summary>
		/// When did the service end (if any)
		/// </summary>
		public DateTime? ServiceEndDate { get; set; }

		/// <summary>
		/// Start period of the invoice
		/// </summary>
		public DateTime? InvoiceBillPeriodStart { get; set; }

		/// <summary>
		/// Ending period of the invoice
		/// </summary>
		public DateTime? InvoiceBillPeriodEnd { get; set; }

		/// <summary>
		/// Total cost of the line
		/// </summary>
		public decimal? Total { get; set; }

		/// <summary>
		/// Identifier of the currency conversion rate
		/// </summary>
		public int CurrencyConversionID { get; set; }

		#endregion

		#region Output

		static List<KeyValuePair<string, string>> layout = new List<KeyValuePair<string, string>>()
		{
			new KeyValuePair<string, string>("Record Type", "RecordType"),
			new KeyValuePair<string, string>("BAN", "BAN"),
			new KeyValuePair<string, string>("Invoice Number", "InvoiceDisplay"),
			new KeyValuePair<string, string>("p_Asset Search Code", "AssetSearchCode"),
			new KeyValuePair<string, string>("SSO", "SSO"),
			new KeyValuePair<string, string>("Charge Date", "ChargeDate"),
			new KeyValuePair<string, string>("Charge Description", "ChargeDescription"),
			new KeyValuePair<string, string>("Charge Type", "ChargeType"),
			new KeyValuePair<string, string>("ServiceStartDate", "ServiceEndDate"),
			new KeyValuePair<string, string>("ServiceStopDate", "ServiceEndDate"),
			new KeyValuePair<string, string>("InvoiceBillPeriodStart", "InvoiceBillPeriodStart"),
			new KeyValuePair<string, string>("InvoiceBillPeriodEnd", "InvoiceBillPeriodEnd"),
			new KeyValuePair<string, string>("Total", "Total"),
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

			if (Total.HasValue)
			{
				if (CurrentCurrency.ID != conv.ID)
				{
					Total = ((Total.Value / CurrentCurrency.ConversionRate) * conv.ConversionRate);
				}

				Total = Decimal.Round(Total.Value, 2);
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
			/// GE Single Sign-On
			/// </summary>
			SSO = 2,
		}

		public static System.Collections.Generic.IComparer<RecordType2Exceptions> GetComparer(Sort field)
		{
			switch (field)
			{
				case Sort.SSO:
					return new SSOComparer();
				case Sort.RecordType:
					return new RecordTypeComparer();
				default:
					return new SSOComparer();
			}
		}


		protected sealed class RecordTypeComparer : System.Collections.Generic.IComparer<RecordType2Exceptions>
		{
			int System.Collections.Generic.IComparer<RecordType2Exceptions>.Compare(RecordType2Exceptions lhs, RecordType2Exceptions rhs)
			{
				return lhs.RecordType.CompareTo(rhs.RecordType);
			}
		}
		protected sealed class SSOComparer : System.Collections.Generic.IComparer<RecordType2Exceptions>
		{
			int System.Collections.Generic.IComparer<RecordType2Exceptions>.Compare(RecordType2Exceptions lhs, RecordType2Exceptions rhs)
			{
				return lhs.SSO.CompareTo(rhs.SSO);
			}
		}


		/// <summary>
		/// Compare objects of the same type
		/// </summary>
		/// <param name="rhs">right-hand side of compare</param>
		/// <returns>1 on greater; 0 on equal; -1 on lesser</returns>
		int IComparable.CompareTo(Object rhs)
		{
			if (!(rhs is RecordType2Exceptions))
			{
				throw new ArgumentException("Argument is not an RecordType2Exceptions", "rhs");
			}
			RecordType2Exceptions r = (RecordType2Exceptions)rhs;
			return this.CompareTo(r);
		}

		/// <summary>
		/// Compare objects of the same type
		/// </summary>
		/// <param name="rhs">right-hand side of compare</param>
		/// <returns>1 on greater; 0 on equal; -1 on lesser</returns>
		public int CompareTo(RecordType2Exceptions rhs)
		{
			return this.RecordType.CompareTo(rhs.RecordType);
		}

		/// <summary>
		/// Less than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than the right</returns>
		public static bool operator <(RecordType2Exceptions left, RecordType2Exceptions right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>
		/// Less than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than or equal to the right</returns>
		public static bool operator <=(RecordType2Exceptions left, RecordType2Exceptions right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>
		/// Greater than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than the right</returns>
		public static bool operator >(RecordType2Exceptions left, RecordType2Exceptions right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>
		/// Greater than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than or equal to the right</returns>
		public static bool operator >=(RecordType2Exceptions left, RecordType2Exceptions right)
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
		public static List<RecordType2Exceptions> DeserializeFromXML(
			MemoryStream ms
			)
		{
			XmlSerializer deserializer = new XmlSerializer(typeof(List<RecordType2Exceptions>));
			List<RecordType2Exceptions> objects;
			objects = (List<RecordType2Exceptions>)deserializer.Deserialize(ms);
			ms.Close();

			return objects;
		}

		#endregion
	}
}
