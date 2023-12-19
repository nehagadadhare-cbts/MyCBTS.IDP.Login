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

	class FileImport_CBADLongDistance
	{
		private static Dictionary<string, System.Type> columnTypeMap_CBAD = new Dictionary<string, System.Type>
		  {
				{"REVENUE",  System.Type.GetType("System.Double")},
				{"CALLDATE",  System.Type.GetType("System.DateTime")},
				{"DURATION",  System.Type.GetType("System.Double")},
				{"SETTLEMENTCODE",  System.Type.GetType("System.Int32")}
		  };

		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ConnectionString">Data Access Connection String</param>
		public FileImport_CBADLongDistance(
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
		/// <param name="BAN">Billing account number to use</param>
		/// <param name="billDate">Invoice date of the bill in MMDDYYYY standard. This is the first date of
		/// the month of the invoice, not the date the bill is issued.</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool Import(
			int invoiceID,
			string filePath,
			string BAN,
			string billDate,
			string caller
			)
		{
			DataTable dt = CSVFileAccessLayer.LoadCSVfile(filePath, "CBAD", columnTypeMap_CBAD, true, false);

			DataColumn newRecordTypeCol = new DataColumn("Record Type");
			newRecordTypeCol.DataType = typeof(string);
			dt.Columns.Add(newRecordTypeCol);

			DataColumn newInvoiceIdCol = new DataColumn("InvoiceID");
			newInvoiceIdCol.DataType = typeof(string);
			dt.Columns.Add(newInvoiceIdCol);

			DataColumn newBANCol = new DataColumn("BAN");
			newBANCol.DataType = typeof(string);
			dt.Columns.Add(newBANCol);

			DataColumn newBillDateCol = new DataColumn("BillDate");
			newBillDateCol.DataType = typeof(string);
			dt.Columns.Add(newBillDateCol);

			DataColumn newDateOfRecordCol = new DataColumn("DateOfRecord");
			newBillDateCol.DataType = typeof(string);
			dt.Columns.Add(newDateOfRecordCol);

			DataColumn newProviderCol = new DataColumn("Provider");
			newProviderCol.DataType = typeof(string);
			dt.Columns.Add(newProviderCol);

			DataColumn newBillingNumberCol = new DataColumn("BillingNumber");
			newBillingNumberCol.DataType = typeof(string);
			dt.Columns.Add(newBillingNumberCol);

			DataColumn newSettlementCodeCol = new DataColumn("SettlementCodeString");
			newSettlementCodeCol.DataType = typeof(string);
			dt.Columns.Add(newSettlementCodeCol);

			foreach (DataRow dr in dt.Rows)
			{
				dr["Record Type"] = "1";
				dr["InvoiceID"] = invoiceID;
				dr["BAN"] = BAN;
				dr["BillDate"] = billDate;
				dr["Provider"] = "CBAD";

				// This value is mapped to DateOfRecord in SQLBulkLayer.
				DateTime tempCallDate;
				DateTime.TryParse(dr["CallDate"].ToString(), out tempCallDate);
				dr["DateOfRecord"] = FormatHelper.ConvertToDate_YYYYMMDD(tempCallDate);

				// Add hyphens to the Billing Number, using DIDNumber field as the source
				dr["BillingNumber"] = FormatHelper.ConvertToPhoneNumberWithHyphens(dr["BillingNumber"].ToString());

				// use cross reference to switch the SettlementCode value
				switch (dr["SettlementCode"].ToString())
				{
					case "1":
						dr["SettlementCodeString"] = "Intralata";
						break;
					case "2":
						dr["SettlementCodeString"] = "Intrastate";
						break;
					case "3":
						dr["SettlementCodeString"] = "Interstate";
						break;
					case "4":
						dr["SettlementCodeString"] = "Canada";
						break;
					case "5":
						dr["SettlementCodeString"] = "Intl/Mexico";
						break;
				}
			}

			SQLBulkLayer.BulkCopy_CBAD_DataToSqlServer(dt, ConnectionString);

			return true;
		}

	}
}
