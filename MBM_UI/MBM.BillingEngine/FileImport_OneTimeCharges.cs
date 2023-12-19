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
	class FileImport_OneTimeCharges
	{
		/// <summary>
		/// Maps what columns require which data types for the CSV file importer (default is 'string')
		/// </summary>
		private static Dictionary<string, System.Type> columnTypeMap_OneTimeCharges = new Dictionary<string, System.Type>
		{
			{"TOTAL",  System.Type.GetType("System.Double")}
		};

		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ConnectionString">Data Access Connection String</param>
		public FileImport_OneTimeCharges(
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
			DataTable dt = CSVFileAccessLayer.LoadCSVfile(filePath, "OneTimeCharges", columnTypeMap_OneTimeCharges, true, false);

			DataColumn newInvoiceIdCol = new DataColumn("InvoiceID");
			newInvoiceIdCol.DataType = typeof(string);
			dt.Columns.Add(newInvoiceIdCol);

			DataColumn chargeDateCol = new DataColumn("Charge Date Parsed");
			chargeDateCol.DataType = typeof(string);
			dt.Columns.Add(chargeDateCol);

			DataColumn serviceStartDateCol = new DataColumn("Service Start Date Parsed");
			serviceStartDateCol.DataType = typeof(string);
			dt.Columns.Add(serviceStartDateCol);

			DataColumn serviceStopDateCol = new DataColumn("Service End Date Parsed");
			serviceStopDateCol.DataType = typeof(string);
			dt.Columns.Add(serviceStopDateCol);

			DataColumn invoiceBillPeriodStartCol = new DataColumn("Invoice Bill Period Start Parsed");
			invoiceBillPeriodStartCol.DataType = typeof(string);
			dt.Columns.Add(invoiceBillPeriodStartCol);

			DataColumn invoiceBillPeriodEndCol = new DataColumn("Invoice Bill Period End Parsed");
			invoiceBillPeriodEndCol.DataType = typeof(string);
			dt.Columns.Add(invoiceBillPeriodEndCol);

			foreach (DataRow dr in dt.Rows)
			{
				dr["InvoiceID"] = invoiceID;

				DateTime tempCallDate;
				DateTime.TryParse(dr["Charge Date"].ToString(), out tempCallDate);
				if (!tempCallDate.Equals(DateTime.MinValue))
				{
					dr["Charge Date Parsed"] = FormatHelper.ConvertToDate_YYYYMMDD(tempCallDate);
				}

				DateTime.TryParse(dr["Service Start Date"].ToString(), out tempCallDate);
				if (!tempCallDate.Equals(DateTime.MinValue))
				{
					dr["Service Start Date Parsed"] = FormatHelper.ConvertToDate_YYYYMMDD(tempCallDate);
				}

				DateTime.TryParse(dr["Service End Date"].ToString(), out tempCallDate);
				if (!tempCallDate.Equals(DateTime.MinValue))
				{
					dr["Service End Date Parsed"] = FormatHelper.ConvertToDate_YYYYMMDD(tempCallDate);
				}

				DateTime.TryParse(dr["Invoice Bill Period Start"].ToString(), out tempCallDate);
				if (!tempCallDate.Equals(DateTime.MinValue))
				{
					dr["Invoice Bill Period Start Parsed"] = FormatHelper.ConvertToDate_YYYYMMDD(tempCallDate);
				}

				DateTime.TryParse(dr["Invoice Bill Period End"].ToString(), out tempCallDate);
				if (!tempCallDate.Equals(DateTime.MinValue))
				{
					dr["Invoice Bill Period End Parsed"] = FormatHelper.ConvertToDate_YYYYMMDD(tempCallDate);
				}
			}

			SQLBulkLayer.BulkCopy_OneTimeCharges_DataToSqlServer(dt, ConnectionString, invoiceID, caller);

			return true;
		}
	
	}
}
