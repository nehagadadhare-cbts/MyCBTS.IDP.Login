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
	class FileImport_OSNUsageSummary
	{
		private static Dictionary<string, System.Type> columnTypeMap_OneSourceUsageSummary = new Dictionary<string, System.Type>
		{
			{"CALLS",  System.Type.GetType("System.Int32")},
			{"DURATION",  System.Type.GetType("System.Double")}, 
			{"REVENUE",  System.Type.GetType("System.Double")},  
			{"GST",  System.Type.GetType("System.Double")},
			{"HST",  System.Type.GetType("System.Double")}, 
			{"PST",  System.Type.GetType("System.Double")}, 
			{"QST",  System.Type.GetType("System.Double")},
			{"OTHER",  System.Type.GetType("System.Double")}
		};

		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ConnectionString">Data Access Connection String</param>
		public FileImport_OSNUsageSummary(
			string connection
			)
		{
			ConnectionString = connection;
		}


		/// <summary>
		/// Import the One-Source Networks long distance File into the datastore
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// <param name="caller">login of calling account</param>
		/// <param name="currencyID">currency identifier</param>
		/// <returns>true on success or false on failure</returns>
		public bool Import(
			int invoiceID,
			string filePath,
			string caller,
			int currencyID
			)
		{
			DataTable dt = CSVFileAccessLayer.LoadCSVfile(filePath, "OneSourceUsageSummary", columnTypeMap_OneSourceUsageSummary, true, false);

			dt.Columns.Add(
				new DataColumn()
				{
					ColumnName = "InvoiceID",
					DataType = typeof(int),
				}
			);

			dt.Columns.Add(
				new DataColumn()
				{
					ColumnName = "CurrencyID",
					DataType = typeof(int),
				}
			);

			foreach (DataRow dr in dt.Rows)
			{
				dr["InvoiceID"] = invoiceID;
				dr["CurrencyID"] = currencyID;
			}

			SQLBulkLayer.BulkCopy_OsnUsageSummary_DataToSqlServer(dt, ConnectionString, invoiceID, caller);

			return true;
		}

	}
}
