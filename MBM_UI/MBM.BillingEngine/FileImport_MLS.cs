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
	class FileImport_MLS
	{
		private static Dictionary<string, System.Type> columnTypeMap_GEGCOMMultiLocationSummary = new Dictionary<string, System.Type>
		{
			//intentionally blank (all Strings)
		};

		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ConnectionString">Data Access Connection String</param>
		public FileImport_MLS(
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
			DataTable dt = CSVFileAccessLayer.LoadCSVfile(filePath, "GEGCOMMultiLocationSummary", columnTypeMap_GEGCOMMultiLocationSummary, true, false);

			// Add column: InvoiceID
			DataColumn dataCol = new DataColumn("InvoiceID");
			dataCol.DataType = typeof(int);
			dt.Columns.Add(dataCol);

			foreach (DataRow dr in dt.Rows)
			{
				dr["InvoiceID"] = invoiceID;
			}

			SQLBulkLayer.BulkCopy_GEGCOMMultiLocationSummary_DataToSqlServer(dt, ConnectionString, invoiceID, caller);

			return true;
		}
	
	}
}
