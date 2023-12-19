using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using LumenWorks.Framework.IO.Csv;
using System.Net;

namespace MBM.DataAccess
{
    public class CSVFileAccessLayer
    {
        /// <summary>
        /// Load a CSV file into a data table
        /// </summary>
        /// <param name="filepath">path of file to read</param>
        /// <param name="dataTableName">name to store the data under</param>
        /// <param name="tableMapping">predetermined columns to be assigned date types</param>
        /// <param name="fileHadHeaders">whether the table columns are named from the file or generic</param>
        /// <param name="fileHadHeaders">whether the first row of the file should be ignored or not</param>
        /// <returns>Datatable of the loaded file</returns>
        public static DataTable LoadCSVfile(string file, string dataTableName, Dictionary<string, System.Type> tableMapping, bool fileHasHeaders, bool ignoreFirstLine)
        {
            try
            {
                string fileName = Path.GetFileName(file);
                string filePath = Path.GetDirectoryName(file);
                string totalPath = filePath.Trim() + @"\" + fileName.Trim();

                DataTable dt = new DataTable(dataTableName);

                using (CsvReader csv = new CsvReader(new StreamReader(totalPath), fileHasHeaders))
                {
                    csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;
                    csv.ParseError += new EventHandler<ParseErrorEventArgs>(CSVParseErrorEventHandler);

                    int fieldCount = csv.FieldCount;

                    //-choose column headers--
                    string[] fieldHeaders;
                    if (fileHasHeaders)
                    {
                        fieldHeaders = csv.GetFieldHeaders();
                        for (int k = 1; k <= fieldCount; k++)
                        {
                            if (fieldHeaders[k - 1] == null || fieldHeaders[k - 1].Equals(""))
                            {
                                fieldHeaders[k - 1] = "Column " + k;
                            }
                        }
                    }
                    else
                    {
                        fieldHeaders = new string[fieldCount];
                        for (int k = 1; k <= fieldCount; k++)
                        {
                            fieldHeaders[k - 1] = "Column " + k;                   //make up Column names if the file doesn't have headers
                        }
                    }

                    //-assign column headers and data types--
                    for (int i = 0; i < fieldCount; i++)
                    {
                        if (tableMapping.ContainsKey(fieldHeaders[i].ToUpper()))
                        {
                            dt.Columns.Add(fieldHeaders[i], tableMapping[fieldHeaders[i].ToUpper()]);
                        }
                        else
                        {
                            dt.Columns.Add(fieldHeaders[i], typeof(string));
                        }
                    }

                    if (ignoreFirstLine)
                    {
                        csv.ReadNextRecord();
                    }

                    int lineCount = 0;
                    //-fill table rows--
                    while (csv.ReadNextRecord())
                    {
                        DataRow dr = dt.NewRow();

                        for (int i = 0; i < fieldCount; i++)
                        {
                            string currFieldText = csv[i] == null ? "" : csv[i];

                            if (i == 0 && currFieldText.Length > 0)             //skip lines that start w/ the 'Substitute' character (for Taxes)
                            {
                                char specChar = currFieldText.ToCharArray()[0];

                                int charNum = Convert.ToInt32(specChar);

                                if (charNum == 26)                   //ASCII character '26', aka 'SUBSTITUTE'
                                {
                                    goto SkipLine;
                                }
                            }

                            if (tableMapping.ContainsKey(fieldHeaders[i].ToUpper()))        //load assigned columns as their opropriate datatype
                            {
                                AddFieldValueToRow(ref dr, i, currFieldText, tableMapping[fieldHeaders[i].ToUpper()]);
                            }
                            else                                                            //load all other columns in as Strings
                            {
                                AddFieldValueToRow(ref dr, i, currFieldText, System.Type.GetType("System.String"));
                            }
                        }

                        dt.Rows.Add(dr);

                    SkipLine:
                        lineCount++;
                    }
                }

                return dt;
            }
            catch
            {
                throw;
            }
        }
        public static void CSVParseErrorEventHandler(object src, ParseErrorEventArgs e)
        {
            //Logging.LogMessage(CBTS.cNotify.Common.FixedData.LogSeverity.Severe, "Parse CSV File Error. " + e.Error.Message);
            e.Action = ParseErrorAction.ThrowException;
        }

        private static void AddFieldValueToRow(ref DataRow currRow, int fieldIndx, string fieldText, System.Type castType)
        {
            try
            {
                if (fieldText.Length == 0)
                {
                    currRow[fieldIndx] = DBNull.Value;
                }
                else if (castType == typeof(Int16))
                {
                    currRow[fieldIndx] = (Convert.ToInt16(fieldText));
                }
                else if (castType == typeof(Int32))
                {
                    currRow[fieldIndx] = (Convert.ToInt32(fieldText));
                }
                else if (castType == typeof(Int64))
                {
                    currRow[fieldIndx] = (Convert.ToInt64(fieldText));
                }
                else if (castType == typeof(Double))
                {
                    currRow[fieldIndx] = (Convert.ToDouble(fieldText));
                }
                else if (castType == typeof(DateTime))
                {
                    currRow[fieldIndx] = (Convert.ToDateTime(fieldText));
                }
                else
                {
                    currRow[fieldIndx] = fieldText;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Build a single line of a Delimited String
        /// </summary>
        /// <param name="columns">column entries</param>
        /// <param name="delimiter">delimiter</param>
        /// <param name="numberOfColumnsToReturn">determines if extra columns are added to the end of a line</param>
        /// <returns>returned strings</returns>
        public static string BuildCvsString(
            List<string> columns,
            string delimiter,
            Int32 numberOfColumnsToReturn
            )
        {
            int columnCount = 0;
            StringBuilder sb = new StringBuilder();

            foreach (string s in columns)
            {
                sb.Append("\"");
                sb.Append(s);
                sb.Append("\"");
                sb.Append(delimiter);
                ++columnCount;
            }

            //Add extra commas to the end of the line
            for (int i = columnCount; i < numberOfColumnsToReturn; i++)
            {
                sb.Append(delimiter);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build a Delimited String for all records of a given type that use the IGomPrinter interface
        /// </summary>
        /// <param name="records">list of records</param>
        /// <param name="delimiter">delimiter</param>
        /// <param name="numberOfColumnsToReturn">determines if extra columns are added to the end of a line</param>
        /// <returns>returned strings</returns>
        public static List<string> BuildCvsString<T>(
            IList<T> records,
            string delimiter,
            Int32 numberOfColumnsToReturn
            )
            where T : Entities.IMBMPrinter
        {
            List<string> csv = new List<string>(records.Count + 1);
            int columnCount = 0;
            StringBuilder sb = new StringBuilder();

            OrderedDictionary mbmOut;
            foreach (Entities.IMBMPrinter r in records)
            {
                sb.Clear();
                columnCount = 0;

                mbmOut = r.MBMOutput;
                foreach (string s in mbmOut.Values)
                {
                    sb.Append("\"");
                    sb.Append(s);
                    sb.Append("\"");
                    sb.Append(delimiter);
                    ++columnCount;
                }

                //Add extra commas to the end of the line
                for (int i = columnCount; i < numberOfColumnsToReturn; i++)
                {
                    sb.Append(delimiter);
                }

                if (sb != null)
                {
                    csv.Add(sb.ToString());
                }
            }

            return csv;
        }

        /// <summary>
        /// Export String into a file. Create the file if it does not exist - append if the
        /// file already exists
        /// </summary>
        /// <param name="lines">lines of strings to export</param>
        /// <param name="file">fully qualified filename and path</param>
        /// <param name="appendIfExists">true if to append to an existing fale - false to overwrite any existing data</param>
        public static void SaveStringToFile(
            List<string> lines,
            string file,
            bool appendIfExists
            )
        {
            using (StreamWriter sw = new StreamWriter(@file, appendIfExists, Encoding.ASCII))
            {
                foreach (string s in lines)
                {
                    sw.WriteLine(s);
                }
                sw.Flush();
                sw.Close();
            }
        }


        /// <summary>
        /// Export String into a file. Create the file if it does not exist - append if the
        /// file already exists
        /// </summary>
        /// <param name="lines">lines of strings to export</param>
        /// <param name="file">fully qualified filename and path</param>
        /// <param name="appendIfExists">true if to append to an existing fale - false to overwrite any existing data</param>
        public static void SaveStringToFileFTP(
            List<string> lines,
            string FTPPath,
            string FTPUserName,
            string FTPPassword,
            string filePath,
            string fileName,
            bool appendIfExists
            )
        {

            FtpWebRequest requestFileUpload = (FtpWebRequest)WebRequest.Create(FTPPath + fileName);
            requestFileUpload.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
            requestFileUpload.Method = WebRequestMethods.Ftp.UploadFile;
            //string fileData = @"C:\MyAccStg\DATA.csv";
            // Copy the contents of the file to the request stream.
            StreamReader sourceStream = new StreamReader(filePath);
            //FILEDATA is nothing but string
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            requestFileUpload.ContentLength = fileContents.Length;

            Stream requestStream = requestFileUpload.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)requestFileUpload.GetResponse();
            response.Close();
        }

        /// <summary>
        /// Export String into a file. Create the file if it does not exist - append if the
        /// file already exists
        /// </summary>
        /// <param name="lines">lines of strings to export</param>
        /// <param name="file">fully qualified filename and path</param>
        /// <param name="appendIfExists">true if to append to an existing fale - false to overwrite any existing data</param>
        public static void SaveStringToFile(
            StringBuilder exportText,
            string file,
            bool appendIfExists
            )
        {
            using (StreamWriter sw = new StreamWriter(@file, appendIfExists, Encoding.ASCII))
            {
                sw.WriteLine(exportText.ToString());
                sw.Flush();
                sw.Close();
            }
        }

    }
}
