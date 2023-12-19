using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;


namespace MBM.Entities
{
	public class RecordType4 : IComparable, MBM.Entities.IMBMPrinter, MBM.Entities.ICurrencyConvertor
	{
		#region Data

		/// <summary>
		/// Unique Identifier
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Type of Record Entry
		/// </summary>
		public int RecordType { get; set; }

		/// <summary>
		/// Billing Account Number (BAN)
		/// </summary>
		public string BAN { get; set; }

		/// <summary>
		/// Monthly invoice number
		/// </summary>
		public int InvoiceID { get; set; }

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
		//public InvoiceTypeEnum InvoiceType { get; set; } //ER#6752

		/// <summary>
		/// Originating number
		/// </summary>
		public string FromNumber { get; set; }

		/// <summary>
		/// GE Single Sign-On
		/// </summary>
		public string SSO { get; set; }

		/// <summary>
		/// Description of the charge
		/// </summary>
		public string ChargeDescription { get; set; }

		/// <summary>
		/// Taxes charged
		/// </summary>
		public decimal? TaxCharge { get; set; }

		/// <summary>
		/// Percentage of the tax
		/// </summary>
		public decimal? TaxPercentage { get; set; }

		/// <summary>
		/// Service Type
		/// </summary>
		public string ServiceType { get; set; }

		/// <summary>
		/// Name of the vendor plus identifying code (CBTS CA, CBTS, etc.)
		/// </summary>
		public string VendorName { get; set; }

		/// <summary>
		/// Name of the State or Province
		/// </summary>
		public string Locality { get; set; }

		/// <summary>
		/// Date of the invoice
		/// </summary>
		public string BillDate { get; set; }

		/// <summary>
		/// Identifier of the currency conversion rate
		/// </summary>
		public int CurrencyConversionID { get; set; }

        /// <summary>
        /// LegalEntity
        /// </summary>
        public string LegalEntity { get; set; }

		#endregion

		#region Output

		static List<KeyValuePair<string, string>> layout = new List<KeyValuePair<string, string>>()
		{
			new KeyValuePair<string, string>("record_type", "RecordType"),
			new KeyValuePair<string, string>("billing_account_number", "BAN"),
			new KeyValuePair<string, string>("invoice_number", "InvoiceDisplay"),
			new KeyValuePair<string, string>("did", "FromNumber"),
			new KeyValuePair<string, string>("employee_id", "SSO"),
			new KeyValuePair<string, string>("charge_description", "ChargeDescription"),
			new KeyValuePair<string, string>("tax_percentage", "TaxPercentage"),
			new KeyValuePair<string, string>("service_type", "ServiceType"),
            new KeyValuePair<string, string>("tax_charge", "TaxCharge"),
			new KeyValuePair<string, string>("vendor_name", "VendorName"),
			new KeyValuePair<string, string>("bill_date", "BillDate"),
			new KeyValuePair<string, string>("state", "Locality"),
            new KeyValuePair<string, string>("legal_entity", "LegalEntity"),
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

			if (TaxCharge.HasValue)
			{
				if (CurrentCurrency.ID != conv.ID)
				{
					TaxCharge = ((TaxCharge.Value / CurrentCurrency.ConversionRate) * conv.ConversionRate);
				}

				TaxCharge = Decimal.Round(TaxCharge.Value, 4);
			}

			CurrencyConversionID = conv.ID;
		}

		#endregion

		#region Comparer

		public enum Sort
		{
			/// <summary>
			/// YUnique Identifier
			/// </summary>
			ID = 1,
			/// <summary>
			/// Record Type identifier
			/// </summary>
			RecordType = 2,
		}

		public static System.Collections.Generic.IComparer<RecordType4> GetComparer(Sort field)
		{
			switch (field)
			{
				case Sort.ID:
					return new IdComparer();
				case Sort.RecordType:
					return new RecordTypeComparer();
				default:
					return new IdComparer();
			}
		}


		protected sealed class IdComparer : System.Collections.Generic.IComparer<RecordType4>
		{
			int System.Collections.Generic.IComparer<RecordType4>.Compare(RecordType4 lhs, RecordType4 rhs)
			{
				return lhs.ID.CompareTo(rhs.ID);
			}
		}
		protected sealed class RecordTypeComparer : System.Collections.Generic.IComparer<RecordType4>
		{
			int System.Collections.Generic.IComparer<RecordType4>.Compare(RecordType4 lhs, RecordType4 rhs)
			{
				return lhs.RecordType.CompareTo(rhs.RecordType);
			}
		}


		/// <summary>
		/// Compare objects of the same type
		/// </summary>
		/// <param name="rhs">right-hand side of compare</param>
		/// <returns>1 on greater; 0 on equal; -1 on lesser</returns>
		int IComparable.CompareTo(Object rhs)
		{
			if (!(rhs is RecordType4))
			{
				throw new ArgumentException("Argument is not an RecordType4", "rhs");
			}
			RecordType4 r = (RecordType4)rhs;
			return this.CompareTo(r);
		}

		/// <summary>
		/// Compare objects of the same type
		/// </summary>
		/// <param name="rhs">right-hand side of compare</param>
		/// <returns>1 on greater; 0 on equal; -1 on lesser</returns>
		public int CompareTo(RecordType4 rhs)
		{
			return this.ID.CompareTo(rhs.ID);
		}

		/// <summary>
		/// Less than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than the right</returns>
		public static bool operator <(RecordType4 left, RecordType4 right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>
		/// Less than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than or equal to the right</returns>
		public static bool operator <=(RecordType4 left, RecordType4 right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>
		/// Greater than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than the right</returns>
		public static bool operator >(RecordType4 left, RecordType4 right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>
		/// Greater than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than or equal to the right</returns>
		public static bool operator >=(RecordType4 left, RecordType4 right)
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
		public static List<RecordType4> DeserializeFromXML(
			MemoryStream ms
			)
		{
			XmlSerializer deserializer = new XmlSerializer(typeof(List<RecordType4>));
			List<RecordType4> objects;
			objects = (List<RecordType4>)deserializer.Deserialize(ms);
			ms.Close();

			return objects;
		}

		#endregion
	}
}
