using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;


namespace MBM.Entities
{
	public class RecordType5 : IComparable, MBM.Entities.IMBMPrinter, MBM.Entities.ICurrencyConvertor
	{
		#region Data

		/// <summary>
		/// Unique Identifier
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Monthly invoice number
		/// </summary>
		public int InvoiceID { get; set; }

		/// <summary>
		/// Type of Record Entry
		/// </summary>
		public int RecordType { get; set; }

		/// <summary>
		/// BAN (Billing Account Number) assigned to each row
		/// </summary>
		public string BAN { get; set; }

		/// <summary>
		/// Record Type sanity
		/// </summary>
		public int CheckRecordType { get; set; }

		/// <summary>
		/// Total number of records
		/// </summary>
		public int? TotalRecordCount { get; set; }

		/// <summary>
		/// Sum of the Records
		/// </summary>
		public string SumFieldName { get; set; }

		/// <summary>
		/// Total amount
		/// </summary>
		public decimal? TotalAmount { get; set; }

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
            new KeyValuePair<string, string>("invoice_number", "BAN"),
			new KeyValuePair<string, string>("checked_record_type", "CheckRecordType"),
			new KeyValuePair<string, string>("record_count", "TotalRecordCount"),
			new KeyValuePair<string, string>("summed_field_name", "SumFieldName"),
			new KeyValuePair<string, string>("grand_total", "TotalAmount"),
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

			if (TotalAmount.HasValue)
			{
				if (CurrentCurrency.ID != conv.ID)
				{
					TotalAmount = ((TotalAmount.Value / CurrentCurrency.ConversionRate) * conv.ConversionRate);
				}

				TotalAmount = Decimal.Round(TotalAmount.Value, 4);
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

		public static System.Collections.Generic.IComparer<RecordType5> GetComparer(Sort field)
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


		protected sealed class IdComparer : System.Collections.Generic.IComparer<RecordType5>
		{
			int System.Collections.Generic.IComparer<RecordType5>.Compare(RecordType5 lhs, RecordType5 rhs)
			{
				return lhs.ID.CompareTo(rhs.ID);
			}
		}
		protected sealed class RecordTypeComparer : System.Collections.Generic.IComparer<RecordType5>
		{
			int System.Collections.Generic.IComparer<RecordType5>.Compare(RecordType5 lhs, RecordType5 rhs)
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
			if (!(rhs is RecordType5))
			{
				throw new ArgumentException("Argument is not an RecordType5", "rhs");
			}
			RecordType5 r = (RecordType5)rhs;
			return this.CompareTo(r);
		}

		/// <summary>
		/// Compare objects of the same type
		/// </summary>
		/// <param name="rhs">right-hand side of compare</param>
		/// <returns>1 on greater; 0 on equal; -1 on lesser</returns>
		public int CompareTo(RecordType5 rhs)
		{
			return this.ID.CompareTo(rhs.ID);
		}

		/// <summary>
		/// Less than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than the right</returns>
		public static bool operator <(RecordType5 left, RecordType5 right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>
		/// Less than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than or equal to the right</returns>
		public static bool operator <=(RecordType5 left, RecordType5 right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>
		/// Greater than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than the right</returns>
		public static bool operator >(RecordType5 left, RecordType5 right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>
		/// Greater than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than or equal to the right</returns>
		public static bool operator >=(RecordType5 left, RecordType5 right)
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
		public static List<RecordType5> DeserializeFromXML(
			MemoryStream ms
			)
		{
			XmlSerializer deserializer = new XmlSerializer(typeof(List<RecordType5>));
			List<RecordType5> objects;
			objects = (List<RecordType5>)deserializer.Deserialize(ms);
			ms.Close();

			return objects;
		}

		#endregion
	}

    public class AccountDetails
    {
        #region Data

        /// <summary>
        /// Snapshot ID
        /// </summary>
        public System.Nullable<int> SnapshotID { get; set; }

     
        /// <summary>
        /// Account Number
        /// </summary>
        public System.Nullable<int> AccountNumber { get; set; }

        /// <summary>
        /// Telephone Number
        /// </summary>
        public string TelephoneNumber { get; set; }

       /// <summary>
        /// Account Status
        /// </summary>
        public string AccountStatus { get; set; }

        /// <summary>
        /// Account Status TYPE
        /// </summary>
        public System.Nullable<char> AccountStatusID { get; set; }

        /// <summary>
        /// Account Installation Date
        /// </summary>
        public System.Nullable<System.DateTime> InstallationDate { get; set; }

        /// <summary>
        /// Account Deactivate Date
        /// </summary>
        public System.Nullable<System.DateTime> DeactivateDate { get; set; }

        #endregion

        #region Output

        //static List<KeyValuePair<string, string>> layout = new List<KeyValuePair<string, string>>()
        //{
        //    new KeyValuePair<string, string>("Invoice Number", "InvoiceNumber"),
        //    new KeyValuePair<string, string>("Account Number", "AccountNumber"),
        //    new KeyValuePair<string, string>("Account Status", "AccountStatus"),
        //};


        //static public List<string> GcomHeader
        //{
        //    get
        //    {
        //        List<string> header = new List<string>();

        //        foreach (KeyValuePair<string, string> kvp in layout)
        //        {
        //            header.Add(kvp.Key);
        //        }

        //        return header;
        //    }
        //}

        //public OrderedDictionary GcomOutput
        //{
        //    get
        //    {
        //        OrderedDictionary outformat = new OrderedDictionary();

        //        foreach (KeyValuePair<string, string> kvp in layout)
        //        {
        //            Type t = this.GetType();
        //            PropertyInfo pi = t.GetProperty(kvp.Value, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        //            if (pi != null)
        //            {
        //                object value = pi.GetValue(this, null);

        //                outformat.Add(kvp.Key, ((value != null) ? value.ToString() : string.Empty));

        //            }
        //            else
        //            {
        //                outformat.Add(kvp.Key, "**ERROR**");
        //            }
        //        }

        //        return outformat;
        //    }
        //}

        #endregion
    }
  
}
