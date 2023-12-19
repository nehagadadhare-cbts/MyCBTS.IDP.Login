using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;


namespace MBM.Entities
{
    public class RT4Summary : MBM.Entities.IMBMPrinter
    {
        #region Data

        /// <summary>
        /// ChargeDescription
        /// </summary>
        public string ChargeDescription { get; set; }

        /// <summary>
        /// Total number of times charged
        /// </summary>
        public int? NoOfTimes { get; set; }

        /// <summary>
        /// call duration
        /// </summary>
        public decimal? TaxCharges { get; set; }

        public int? invoiceID { get; set; }

        #endregion

        #region Output

        static List<KeyValuePair<string, string>> layout = new List<KeyValuePair<string, string>>()
		{
			new KeyValuePair<string, string>("Charge Description", "ChargeDescription"),
			new KeyValuePair<string, string>("No Of Times", "NoOfTimes"),
			new KeyValuePair<string, string>("Tax Charges", "TaxCharges"),			
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



        #region Serializer

        /// <summary>
        /// Deserializes an object(s) into this class
        /// </summary>
        /// <param name="ms">XML memory stream</param>
        /// <returns>list of objects within the stream</returns>
        /// <remarks>this method is provides to ease transition of the same objects across namespace</remarks>
        public static List<RT4Summary> DeserializeFromXML(
            MemoryStream ms
            )
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<RT4Summary>));
            List<RT4Summary> objects;
            objects = (List<RT4Summary>)deserializer.Deserialize(ms);
            ms.Close();

            return objects;
        }

        #endregion
    }
}
