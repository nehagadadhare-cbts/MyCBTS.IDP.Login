using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MBM.Library
{
    public class NullHelper
    {

        /// <summary>
        /// Gets the date from a reader but if the value is null returns the MinValue of the DateTime class.
        /// </summary>
        /// <param name="reader">Record that holds values for evaluation.</param>
        /// <param name="field">String name of the field to evaluate.</param>
        /// <returns></returns>
        public static DateTime? GetDateFromReader(IDataRecord reader, string field, DateTime? returnValueIfNull)
        {
            int index = reader.GetOrdinal(field);

            if (!reader.IsDBNull(index))
            {
                return reader.GetDateTime(index);
            }

            return returnValueIfNull;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader">Record that holds values for evaluation.</param>
        /// <param name="field">String name of the field to evaluate.</param>
        /// <returns></returns>
        public static decimal GetDecimalFromReader(IDataRecord reader, string field)
        {
            int index = reader.GetOrdinal(field);

            if (!reader.IsDBNull(index))
            {
                return reader.GetDecimal(index);
            }

            return 0m;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader">Record that holds values for evaluation.</param>
        /// <param name="field">String name of the field to evaluate.</param>
        /// <returns></returns>
        public static Int16 GetInt16FromReader(IDataRecord reader, string field)
        {
            int index = reader.GetOrdinal(field);

            if (!reader.IsDBNull(index))
            {
                return reader.GetInt16(index);
            }

            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader">Record that holds values for evaluation.</param>
        /// <param name="field">String name of the field to evaluate.</param>
        /// <returns></returns>
        public static Int32 GetInt32FromReader(IDataRecord reader, string field)
        {
            int index = reader.GetOrdinal(field);

            if (!reader.IsDBNull(index))
            {
                return reader.GetInt32(index);
            }

            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader">Record that holds values for evaluation.</param>
        /// <param name="field">String name of the field to evaluate.</param>
        /// <returns></returns>
        public static Int64 GetInt64FromReader(IDataRecord reader, string field)
        {
            int index = reader.GetOrdinal(field);

            if (!reader.IsDBNull(index))
            { return reader.GetInt64(index); }

            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader">Record that holds values for evaluation.</param>
        /// <param name="field">String name of the field to evaluate.</param>
        /// <returns></returns>
        public static string GetStringFromReader(IDataRecord reader, string field)
        {
            int index = reader.GetOrdinal(field);

            if (!reader.IsDBNull(index))
            {
                //return reader.GetString(index).TrimEnd();
                return reader[index].ToString();
            }

            return String.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader">Record that holds values for evaluation.</param>
        /// <param name="field">String name of the field to evaluate.</param>
        /// <returns></returns>
        public static float GetFloatFromReader(IDataRecord reader, string field)
        {
            int index = reader.GetOrdinal(field);

            if (!reader.IsDBNull(index))
            { return reader.GetFloat(index); }

            return 0f;
        }


    }
}
