using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;


namespace MBM.Entities
{
    public class RT1Summary : MBM.Entities.IMBMPrinter
    {
        #region Data

        /// <summary>
        /// ChargeDescription
        /// </summary>
        public string Zone { get; set; }

        /// <summary>
        /// Total number of calls
        /// </summary>
        public int? NoOfTimes { get; set; }

        /// <summary>
        /// call duration
        /// </summary>
        public decimal? Duration { get; set; }

        /// <summary>
        /// call charges
        /// </summary>
        public decimal? CallCharges { get; set; }

        public int? invoiceID { get; set; }

        #endregion

        #region Output

        static List<KeyValuePair<string, string>> layout = new List<KeyValuePair<string, string>>()
		{
			new KeyValuePair<string, string>("Charge Description", "Zone"),
			new KeyValuePair<string, string>("No Of Calls", "NoOfTimes"),
			new KeyValuePair<string, string>("Duration", "Duration"),
			new KeyValuePair<string, string>("Call Charges", "CallCharges"),			
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
        public static List<RT1Summary> DeserializeFromXML(
            MemoryStream ms
            )
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<RT1Summary>));
            List<RT1Summary> objects;
            objects = (List<RT1Summary>)deserializer.Deserialize(ms);
            ms.Close();

            return objects;
        }

        #endregion
    }
}
