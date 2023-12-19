﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;


namespace MBM.Entities
{
	public class RecordType1Exceptions : IComparable, MBM.Entities.IMBMPrinter, MBM.Entities.ICurrencyConvertor
	{
		#region Data

		/// <summary>
		/// Record Type
		/// </summary>
		public string RecordType { get; set; }

		/// <summary>
		/// Billing Account Number (BAN)
		/// </summary>
		public string BAN { get; set; }

		/// <summary>
		/// Invoice identifier
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
		//public InvoiceTypeEnum InvoiceType { get; set; } //6752

		/// <summary>
		/// Name of the Vendor + identifying code (CBTS CA, CBTS, etc.)
		/// </summary>
		public string VendorName { get; set; }

		/// <summary>
		/// Name of the State or Province
		/// </summary>
		public string Locality { get; set; }

		/// <summary>
		/// Identifier of the currency conversion rate
		/// </summary>
		public int CurrencyConversionID { get; set; }

		/// <summary>
		/// Date call was made
		/// </summary>
		public DateTime CallDate { get; set; }

		/// <summary>
		/// Time of the call
		/// </summary>
		public string CallTime { get; set; }

		/// <summary>
		/// when the call was recorded
		/// </summary>
		public string DateOfRecord { get; set; }

		/// <summary>
		/// Date of the Bill
		/// </summary>
		public string BillDate { get; set; }

		/// <summary>
		/// GE Single Sign-On
		/// </summary>
		public string SSO { get; set; }

		/// <summary>
		/// Originating number
		/// </summary>
		public string FromNumber { get; set; }

		/// <summary>
		/// Destination number
		/// </summary>
		public string ToNumber { get; set; }

		/// <summary>
		/// DID Phone number
		/// </summary>
		public string DIDNumber { get; set; }

		/// <summary>
		/// Unknown
		/// </summary>
		public string FromToNumber { get; set; }

		/// <summary>
		/// Amount of revenue generated by call
		/// </summary>
		public decimal? Revenue { get; set; }

		/// <summary>
		/// when the call was connected
		/// </summary>
		public string ConnectTime { get; set; }

		/// <summary>
		/// Duration of the call
		/// </summary>
		public decimal? Duration { get; set; }

		/// <summary>
		/// Billing number in NAP standard
		/// </summary>
		public string BillingNumberNorthAmericanStandard { get; set; }

		/// <summary>
		/// Originating city
		/// </summary>
		public string FromCity { get; set; }

		/// <summary>
		/// Originating State/Providence
		/// </summary>
		public string FromState { get; set; }

		/// <summary>
		/// Destination City
		/// </summary>
		public string ToCity { get; set; }

		/// <summary>
		/// Destination State/Providence
		/// </summary>
		public string ToState { get; set; }

		/// <summary>
		/// Billing settlement code
		/// </summary>
		public int? SettlementCode { get; set; }

		/// <summary>
		/// Type of charge (call, equipment, service, etc.)
		/// </summary>
		public string ChargeDescription { get; set; }

		/// <summary>
		/// Provider of the service
		/// </summary>
		public string Provider { get; set; }

		/// <summary>
		/// Unknown
		/// </summary>
		public string ChargeOrientation { get; set; }

		/// <summary>
		/// Unknown
		/// </summary>
		public string OriginalFromNumber { get; set; }

		#endregion

		#region Output

		static List<KeyValuePair<string, string>> layout = new List<KeyValuePair<string, string>>()
		{
			new KeyValuePair<string, string>("Record Type", "RecordType"),
			new KeyValuePair<string, string>("BAN", "BAN"),
			new KeyValuePair<string, string>("Invoice Number", "InvoiceDisplay"),
			new KeyValuePair<string, string>("Bill Date", "BillDate"),
			new KeyValuePair<string, string>("SSO", "SSO"),
			new KeyValuePair<string, string>("From Number (DID)", "FromNumber"),
			new KeyValuePair<string, string>("DIDNumber", "DIDNumber"),
			new KeyValuePair<string, string>("From/To Number", "ToNumber"),
			new KeyValuePair<string, string>("Charge Amount", "Revenue"),
			new KeyValuePair<string, string>("Date of Record", "DateOfRecord"),
			new KeyValuePair<string, string>("Connect Time", "ConnectTime"),
			new KeyValuePair<string, string>("Billable Time", "Duration"),
			new KeyValuePair<string, string>("Billing Number North American Standard", "BillingNumberNorthAmericanStandard"),
			new KeyValuePair<string, string>("From Place", "FromCity"),
			new KeyValuePair<string, string>("From State", "FromState"),
			new KeyValuePair<string, string>("To Place", "ToCity"),
			new KeyValuePair<string, string>("To State", "ToState"),
			new KeyValuePair<string, string>("Settlement Code", "SettlementCode"),
			new KeyValuePair<string, string>("Charge Description", "ChargeDescription"),
			new KeyValuePair<string, string>("Provider", "Provider"),
			new KeyValuePair<string, string>("Vendor Name", "VendorName"),
			new KeyValuePair<string, string>("State/Province", "Locality"),
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

			if (Revenue.HasValue)
			{
				if (CurrentCurrency.ID != conv.ID)
				{
					Revenue = ((Revenue.Value / CurrentCurrency.ConversionRate) * conv.ConversionRate);
				}

				Revenue = Decimal.Round(Revenue.Value, 2);
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
		}

		public static System.Collections.Generic.IComparer<RecordType1Exceptions> GetComparer(Sort field)
		{
			switch (field)
			{
				case Sort.RecordType:
					return new RecordTypeComparer();
				default:
					return new RecordTypeComparer();
			}
		}
		protected sealed class RecordTypeComparer : System.Collections.Generic.IComparer<RecordType1Exceptions>
		{
			int System.Collections.Generic.IComparer<RecordType1Exceptions>.Compare(RecordType1Exceptions lhs, RecordType1Exceptions rhs)
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
			if (!(rhs is RecordType1Exceptions))
			{
				throw new ArgumentException("Argument is not an RecordType1Exceptions", "rhs");
			}
			RecordType1Exceptions r = (RecordType1Exceptions)rhs;
			return this.CompareTo(r);
		}

		/// <summary>
		/// Compare objects of the same type
		/// </summary>
		/// <param name="rhs">right-hand side of compare</param>
		/// <returns>1 on greater; 0 on equal; -1 on lesser</returns>
		public int CompareTo(RecordType1Exceptions rhs)
		{
			return this.RecordType.CompareTo(rhs.RecordType);
		}

		/// <summary>
		/// Less than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than the right</returns>
		public static bool operator <(RecordType1Exceptions left, RecordType1Exceptions right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>
		/// Less than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than or equal to the right</returns>
		public static bool operator <=(RecordType1Exceptions left, RecordType1Exceptions right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>
		/// Greater than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than the right</returns>
		public static bool operator >(RecordType1Exceptions left, RecordType1Exceptions right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>
		/// Greater than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than or equal to the right</returns>
		public static bool operator >=(RecordType1Exceptions left, RecordType1Exceptions right)
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
		public static List<RecordType1Exceptions> DeserializeFromXML(
			MemoryStream ms
			)
		{
			XmlSerializer deserializer = new XmlSerializer(typeof(List<RecordType1Exceptions>));
			List<RecordType1Exceptions> objects;
			objects = (List<RecordType1Exceptions>)deserializer.Deserialize(ms);
			ms.Close();

			return objects;
		}

		#endregion
	}
}