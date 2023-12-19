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
	class FileImport_EC500
	{
		private static Dictionary<string, System.Type> columnTypeMap_EC500 = new Dictionary<string, System.Type>
		{
			//intentionally blank (all Strings)
		};

		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ConnectionString">Data Access Connection String</param>
		public FileImport_EC500(
			string connection
			)
		{
			ConnectionString = connection;
		}

		/// <summary>
		/// Import the File into the datastore
		/// </summary>
		/// <param name="invoiceID">invoice identifer for import</param>
		/// <param name="filePath">fully qualified path to find the file</param>
		/// the month of the invoice, not the date the bill is issued.</param>
		/// <param name="caller">login of calling account</param>
		/// <returns>true on success or false on failure</returns>
		public bool Import(
			int invoiceID,
			string filePath,
			string caller
			)
		{
			DataTable dt = CSVFileAccessLayer.LoadCSVfile(filePath, "EC500", columnTypeMap_EC500, false, true);

			int colNumber = 0;
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"GCOM #");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Type");
			FileImport.ValidateHeaderRecord(ref dt, colNumber++, @"Cell #");

			SQLBulkLayer.BulkCopy_EC500_DataToSqlServer(dt, ConnectionString, caller);

			return true;
		}

	}
}
