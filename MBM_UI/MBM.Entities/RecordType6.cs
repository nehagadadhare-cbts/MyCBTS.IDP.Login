using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;


namespace MBM.Entities
{
	public class RecordType6 : IComparable, MBM.Entities.IMBMPrinter
	{
		#region Data

		/// <summary>
		/// Type of Record Entry
		/// </summary>
		public int RecordType { get; set; }

		/// <summary>
		/// Given Name
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Surname
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// GE Single Sign-On
		/// </summary>
		public string SSO { get; set; }

		/// <summary>
		/// DID Number
		/// </summary>
		public string DID { get; set; }

		/// <summary>
		/// Cell/Mobile Number
		/// </summary>
		public string Mobile { get; set; }

		/// <summary>
		/// Email address
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Business Unit
		/// </summary>
		public string BusinessUnit { get; set; }

		/// <summary>
		/// Business department
		/// </summary>
		public string Department { get; set; }

		/// <summary>
		/// Physical location
		/// </summary>
		public string Location { get; set; }

		/// <summary>
		/// Business Entity
		/// </summary>
		public string LegalEntity { get; set; }

		/// <summary>
		/// Type of address (home, business, etc.)
		/// </summary>
		public string AddressType { get; set; }

		/// <summary>
		/// First part of street address
		/// </summary>
		public string Street1 { get; set; }

		/// <summary>
		/// Second part of street address
		/// </summary>
		public string Street2 { get; set; }

		/// <summary>
		/// City
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// State
		/// </summary>
		public string State { get; set; }

		/// <summary>
		/// Zip or Postal Code
		/// </summary>
		public string PostalCode { get; set; }

		/// <summary>
		/// Country
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// Account status
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// Unique id
		/// </summary>
		public string ServiceProfileID { get; set; }

		/// <summary>
		/// Starting date
		/// </summary>
		public string StartDate { get; set; }

		/// <summary>
		/// Stopped date - if any
		/// </summary>
		public string StopDate { get; set; }

		/// <summary>
		/// Unknown
		/// </summary>
		public string PspOrderID { get; set; }

		/// <summary>
		/// Gateway connection
		/// </summary>
		public string Gateway { get; set; }

		/// <summary>
		/// Equipment serial number
		/// </summary>
		public string SerialNumber { get; set; }

		/// <summary>
		/// MAC Address of equipment
		/// </summary>
		public string MacAddress { get; set; }

		/// <summary>
		/// IP Address of equipment
		/// </summary>
		public string IpAddress { get; set; }

		/// <summary>
		/// Brand of equipment
		/// </summary>
		public string Brand { get; set; }

		/// <summary>
		/// Specific model of equipment
		/// </summary>
		public string Model { get; set; }

		/// <summary>
		/// Asset tag of the equipment
		/// </summary>
		public string AssetTagNumber { get; set; }

		/// <summary>
		/// Address the asset belongs to
		/// </summary>
		public string AssetAddress { get; set; }

		/// <summary>
		/// Status of the asset
		/// </summary>
		public string AssetStatus { get; set; }

		/// <summary>
		/// Site identifier
		/// </summary>
		public string SiteID { get; set; }

		#endregion

		#region Output

		static List<KeyValuePair<string, string>> layout = new List<KeyValuePair<string, string>>()
		{
			new KeyValuePair<string, string>("Record Type", "RecordType"),
			new KeyValuePair<string, string>("FirstName", "FirstName"),
			new KeyValuePair<string, string>("LastName", "LastName"),
			new KeyValuePair<string, string>("SSO", "SSO"),
			new KeyValuePair<string, string>("DID", "DID"),
			new KeyValuePair<string, string>("Mobile/Cell", "Mobile"),
			new KeyValuePair<string, string>("Email", "Email"),
			new KeyValuePair<string, string>("BusinessUnit", "BusinessUnit"),
			new KeyValuePair<string, string>("Department", "Department"),
			new KeyValuePair<string, string>("Location-GCOM", "Location"),
			new KeyValuePair<string, string>("Legal Entity", "LegalEntity"),
			new KeyValuePair<string, string>("UserAddressType", "AddressType"),
			new KeyValuePair<string, string>("UserStreet1", "Street1"),
			new KeyValuePair<string, string>("UserStreet2", "Street2"),
			new KeyValuePair<string, string>("UserCity", "City"),
			new KeyValuePair<string, string>("UserState", "State"),
			new KeyValuePair<string, string>("UserZip/Postal Code", "PostalCode"),
			new KeyValuePair<string, string>("UserCountry", "Country"),
			new KeyValuePair<string, string>("UserStatus", "Status"),
			new KeyValuePair<string, string>("ServiceProfile_ID", "ServiceProfileID"),
			new KeyValuePair<string, string>("StartDate", "StartDate"),
			new KeyValuePair<string, string>("StopDate", "StopDate"),
			new KeyValuePair<string, string>("PSP Order ID", "PspOrderID"),
			new KeyValuePair<string, string>("Gateway", "Gateway"),
			new KeyValuePair<string, string>("Serial Number", "SerialNumber"),
			new KeyValuePair<string, string>("MAC Address", "MacAddress"),
			new KeyValuePair<string, string>("IP Address", "IpAddress"),
			new KeyValuePair<string, string>("Brand", "Brand"),
			new KeyValuePair<string, string>("Model", "Model"),
			new KeyValuePair<string, string>("Asset Tag Number", "AssetTagNumber"),
			new KeyValuePair<string, string>("Asset Address", "AssetAddress"),
			new KeyValuePair<string, string>("AssetStatus", "AssetStatus"),
			new KeyValuePair<string, string>("Site ID", "SiteID"),
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

		#region Comparer

		public enum Sort
		{
			/// <summary>
			/// YUnique Identifier
			/// </summary>
			FirstName = 1,
			/// <summary>
			/// Record Type identifier
			/// </summary>
			RecordType = 2,
			/// <summary>
			/// Surname
			/// </summary>
			LastName = 3,
		}

		public static System.Collections.Generic.IComparer<RecordType6> GetComparer(Sort field)
		{
			switch (field)
			{
				case Sort.FirstName:
					return new FirstNameComparer();
				case Sort.LastName:
					return new LastNameComparer();
				case Sort.RecordType:
					return new RecordTypeComparer();
				default:
					return new RecordTypeComparer();
			}
		}


		protected sealed class FirstNameComparer : System.Collections.Generic.IComparer<RecordType6>
		{
			int System.Collections.Generic.IComparer<RecordType6>.Compare(RecordType6 lhs, RecordType6 rhs)
			{
				return lhs.FirstName.CompareTo(rhs.FirstName);
			}
		}
		protected sealed class LastNameComparer : System.Collections.Generic.IComparer<RecordType6>
		{
			int System.Collections.Generic.IComparer<RecordType6>.Compare(RecordType6 lhs, RecordType6 rhs)
			{
				return lhs.LastName.CompareTo(rhs.LastName);
			}
		}
		protected sealed class RecordTypeComparer : System.Collections.Generic.IComparer<RecordType6>
		{
			int System.Collections.Generic.IComparer<RecordType6>.Compare(RecordType6 lhs, RecordType6 rhs)
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
			if (!(rhs is RecordType6))
			{
				throw new ArgumentException("Argument is not an RecordType6", "rhs");
			}
			RecordType6 r = (RecordType6)rhs;
			return this.CompareTo(r);
		}

		/// <summary>
		/// Compare objects of the same type
		/// </summary>
		/// <param name="rhs">right-hand side of compare</param>
		/// <returns>1 on greater; 0 on equal; -1 on lesser</returns>
		public int CompareTo(RecordType6 rhs)
		{
			return this.RecordType.CompareTo(rhs.RecordType);
		}

		/// <summary>
		/// Less than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than the right</returns>
		public static bool operator <(RecordType6 left, RecordType6 right)
		{
			return left.CompareTo(right) < 0;
		}

		/// <summary>
		/// Less than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is less than or equal to the right</returns>
		public static bool operator <=(RecordType6 left, RecordType6 right)
		{
			return left.CompareTo(right) <= 0;
		}

		/// <summary>
		/// Greater than operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than the right</returns>
		public static bool operator >(RecordType6 left, RecordType6 right)
		{
			return left.CompareTo(right) > 0;
		}

		/// <summary>
		/// Greater than or equal operator
		/// </summary>
		/// <param name="left">left-hand side</param>
		/// <param name="right">right-hand side</param>
		/// <returns>true if left-hand side is greater than or equal to the right</returns>
		public static bool operator >=(RecordType6 left, RecordType6 right)
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
		public static List<RecordType6> DeserializeFromXML(
			MemoryStream ms
			)
		{
			XmlSerializer deserializer = new XmlSerializer(typeof(List<RecordType6>));
			List<RecordType6> objects;
			objects = (List<RecordType6>)deserializer.Deserialize(ms);
			ms.Close();

			return objects;
		}

		#endregion
	}
}
