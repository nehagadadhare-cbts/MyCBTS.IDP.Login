using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Principal;
using System.IO;
using MBM.DataAccess;
using MBM.Entities;
using MBM.Library;

namespace MBM.BillingEngine
{
	class FileImport_CBADTaxFile
	{
		private static Dictionary<string, System.Type> columnTypeMap_Tax = new Dictionary<string, System.Type>
		  {
				{"COLUMN 5",  System.Type.GetType("System.Int32")},     //TotalCallCount
				{"COLUMN 6",  System.Type.GetType("System.Double")},    //TotalCallDuration
				{"COLUMN 10",  System.Type.GetType("System.Double")},   //Federal
				{"COLUMN 11",  System.Type.GetType("System.Double")},   //State
				{"COLUMN 12",  System.Type.GetType("System.Double")},   //Local
				{"COLUMN 13",  System.Type.GetType("System.Double")},   //USF
				{"COLUMN 14",  System.Type.GetType("System.Double")}    //ARF
		  };

		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ConnectionString">Data Access Connection String</param>
		public FileImport_CBADTaxFile(
			string connection
			)
		{
			ConnectionString = connection;
		}

		/// <summary>
		/// Import the CBAD long distance File into the datastore
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool Import(
			int invoiceID,
			string filePath,
			string caller
			)
		{
			DataTable dt = CSVFileAccessLayer.LoadCSVfile(filePath, "Tax", columnTypeMap_Tax, false, false);

			int colNumber = 0;
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Customer Number");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Billing Period");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Phone Number (Orig/Term)");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Call Type (Toll-Free/Outbound)");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Total Call Count");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Total Call Duration");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Interstate/Int'l Duration");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Total Call Revenue");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Interstate/Int'l");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Federal");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"State");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Local");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"USF");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"ARF");

			DataColumn newInvoiceNumberCol = new DataColumn("InvoiceID");
			newInvoiceNumberCol.DataType = typeof(string);
			dt.Columns.Add(newInvoiceNumberCol);

			DataColumn newRecordTypeCol = new DataColumn("Record Type");
			newRecordTypeCol.DataType = typeof(string);
			dt.Columns.Add(newRecordTypeCol);

			foreach (DataRow dr in dt.Rows)
			{
				dr["InvoiceID"] = invoiceID;
				dr["Record Type"] = "4";
			}

			SQLBulkLayer.BulkCopy_TaxFile_DataToSqlServer(dt, ConnectionString);

			return true;
		}
	
	}
}
