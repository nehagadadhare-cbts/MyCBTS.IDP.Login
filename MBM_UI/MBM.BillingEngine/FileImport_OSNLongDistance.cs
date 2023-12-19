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
	class FileImport_OSNLongDistance
	{
		private static Dictionary<string, System.Type> columnTypeMap_OneSourceLongDistance = new Dictionary<string, System.Type>
		{
			{"CALL_DATETIME",  System.Type.GetType("System.DateTime")},
			{"PRODUCT_TYPE",  System.Type.GetType("System.Int32")},
			{"ACCESS_TYPE",  System.Type.GetType("System.Int32")},
			{"MESSAGE_TYPE",  System.Type.GetType("System.Int32")},
			{"DIRECTION",  System.Type.GetType("System.Int32")},
			{"OPERATION_INDICATOR",  System.Type.GetType("System.Boolean")},
			{"PAYPHONE_INDICATOR",  System.Type.GetType("System.Boolean")},
			{"ORIG_TIER",  System.Type.GetType("System.Int32")},
			{"TERM_TIER",  System.Type.GetType("System.Int32")},
			{"TIERSTATE",  System.Type.GetType("System.Int32")},
			{"TIER1",  System.Type.GetType("System.Int32")},
			{"TIER2",  System.Type.GetType("System.Int32")},
			{"APPLIEDRATE",  System.Type.GetType("System.Double")},
			{"CALCULATEDDURATION",  System.Type.GetType("System.Int32")},
			{"DURATIONFORRATING",  System.Type.GetType("System.Int32")},
			{"SURCHARGE",  System.Type.GetType("System.Double")},
			{"TOTALREVENUE",  System.Type.GetType("System.Double")},
			{"PRERATED",  System.Type.GetType("System.Int32")}
		};

		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ConnectionString">Data Access Connection String</param>
		public FileImport_OSNLongDistance(
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
			DataTable dt = CSVFileAccessLayer.LoadCSVfile(filePath, "OneSourceLongDistance", columnTypeMap_OneSourceLongDistance, true, false);

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

			dt.Columns.Add(
				new DataColumn()
				{
					ColumnName = "Provider",
					DataType = typeof(string),
				}
			);

			dt.Columns.Add(
				new DataColumn()
				{
					ColumnName = "BillDate",
					DataType = typeof(string),
				}
			);

			dt.Columns.Add(
				new DataColumn()
				{
					ColumnName = "DateOfRecord",
					DataType = typeof(string),
					DefaultValue = FormatHelper.ConvertToDate_YYYYMMDD(DateTime.Now),
				}
			);

			DateTime BillDate = DateTime.Now;
			foreach (DataRow dr in dt.Rows)
			{
				dr["InvoiceID"] = invoiceID;
				dr["CurrencyID"] = currencyID;
				dr["Provider"] = "OSN";
				dr["BillDate"] = FormatHelper.ConvertToDate_YYYYMMDD(BillDate);

				// This value is mapped to DateOfRecord in SQLBulkLayer.
				DateTime tempCallDate;
				DateTime.TryParse(dr["Call_DateTime"].ToString(), out tempCallDate);
				dr["DateOfRecord"] = FormatHelper.ConvertToDate_YYYYMMDD(tempCallDate);
			}

			SQLBulkLayer.BulkCopy_OsnLongDistance_DataToSqlServer(dt, ConnectionString, invoiceID, caller);

			return true;
		}

	}
}
