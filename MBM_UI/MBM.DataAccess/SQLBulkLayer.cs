using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MBM.DataAccess
{
	public class SQLBulkLayer
	{
		public DateTime billingMonth;

		/// <summary>
		/// Bulk load data into RecordType 1
		/// </summary>
		/// <param name="sourceTable">source data</param>
		/// <param name="connectionString">connection to data string</param>
		public static void BulkCopy_CBAD_DataToSqlServer(
			DataTable sourceTable,
			string connectionString
			)
		{
			// column mappings example:
			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();

					using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
					{
						bulkCopy.DestinationTableName = "CBAD_LongDistance";
						bulkCopy.BulkCopyTimeout = 0;
                        
						bulkCopy.ColumnMappings.Add("InvoiceID", "InvoiceID");
						bulkCopy.ColumnMappings.Add("Record Type", "RecordType");
						bulkCopy.ColumnMappings.Add("BAN", "BAN");
						bulkCopy.ColumnMappings.Add("DateOfRecord", "DateOfRecord");
						bulkCopy.ColumnMappings.Add("BillDate", "BillDate");
						bulkCopy.ColumnMappings.Add("BillingNumber", "BillingNumberNorthAmericanStandard");
						bulkCopy.ColumnMappings.Add("SettlementCodeString", "ChargeDescription");
						bulkCopy.ColumnMappings.Add("Provider", "Provider");
						bulkCopy.ColumnMappings.Add("DIDNumber", "DIDNumber");
						bulkCopy.ColumnMappings.Add("Revenue", "Revenue");
						bulkCopy.ColumnMappings.Add("CallDate", "CallDate");
						bulkCopy.ColumnMappings.Add("CallTime", "CallTime");
						bulkCopy.ColumnMappings.Add("Duration", "Duration");
						bulkCopy.ColumnMappings.Add("FromCity", "FromCity");
						bulkCopy.ColumnMappings.Add("FromState", "FromState");
						bulkCopy.ColumnMappings.Add("ToCity", "ToCity");
						bulkCopy.ColumnMappings.Add("ToState", "ToState");
						bulkCopy.ColumnMappings.Add("SettlementCode", "SettlementCode");
						bulkCopy.ColumnMappings.Add("FromNumber", "OriginalFromNumber");

						bulkCopy.WriteToServer(sourceTable);
						bulkCopy.Close();
					}
				}
				catch
				{
					throw;
				}
			}
		}

		/// <summary>
		/// Bulk load a data table into the Tax Table
		/// </summary>
		/// <param name="sourceTable">data</param>
		/// <param name="connectionString">data store string</param>
		public static void BulkCopy_TaxFile_DataToSqlServer(
			DataTable sourceTable,
			string connectionString
			)
		{
			// column mappings example:
			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
					using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
					{
						bulkCopy.DestinationTableName = "CBAD_Taxes";
						bulkCopy.BulkCopyTimeout = 0;

						bulkCopy.ColumnMappings.Add("InvoiceID", "InvoiceID");
						bulkCopy.ColumnMappings.Add("Record Type", "RecordType");
						bulkCopy.ColumnMappings.Add("Customer Number", "CustomerNumber");
						bulkCopy.ColumnMappings.Add(@"Phone Number (Orig/Term)", "PhoneNumber");
						bulkCopy.ColumnMappings.Add(@"Call Type (Toll-Free/Outbound)", "CallType");
						bulkCopy.ColumnMappings.Add("Total Call Count", "TotalCallCount");
                        bulkCopy.ColumnMappings.Add("Total Call Duration", "TotalCallDuration");
						bulkCopy.ColumnMappings.Add("State", "State");
						bulkCopy.ColumnMappings.Add("Local", "Local");
						bulkCopy.ColumnMappings.Add("Federal", "Federal");
						bulkCopy.ColumnMappings.Add("USF", "USF");
						bulkCopy.ColumnMappings.Add("ARF", "ARF");

						bulkCopy.WriteToServer(sourceTable);
						bulkCopy.Close();
					}
				}
				catch
				{
					throw;
				}
			}
		}

		/// <summary>
		/// Insert data into the OneSourceNetworks_LongDistance table. The table will be cleared of all data for an invoice before the load
		/// </summary>
		/// <param name="sourceTable">source data</param>
		/// <param name="connectionString">data store to connect</param>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="userName">login loading the table</param>
		public static void BulkCopy_OsnLongDistance_DataToSqlServer(
			DataTable sourceTable,
			string connectionString,
			int invoiceID,
			string userName
			)
		{
			try
			{
				//Remove any existing entries
				using (MBMDbDataContext db = new MBMDbDataContext(connectionString))
				{
					int results = db.OSN_LongDistance_DeleteByInvoice(invoiceID, userName);

					if (results != 0)
					{
						throw new Exception("Failed to delete existing entries for OsnLongDistance");
					}
				}

				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
					{
						bulkCopy.DestinationTableName = "OneSourceNetworks_LongDistance";

						bulkCopy.ColumnMappings.Add("InvoiceID", "InvoiceID");
						bulkCopy.ColumnMappings.Add("AccountNumber", "AccountNumber");
						bulkCopy.ColumnMappings.Add("ProductID", "ProductID");
						bulkCopy.ColumnMappings.Add("AccountCode", "AccountCode");
						bulkCopy.ColumnMappings.Add("Call_DateTime", "CallDateTime");
						bulkCopy.ColumnMappings.Add("Product_Type", "ProductType");
						bulkCopy.ColumnMappings.Add("Product_Type_Description", "ProductTypeDescription");
						bulkCopy.ColumnMappings.Add("Access_Type", "AccessType");
						bulkCopy.ColumnMappings.Add("Access_Type_Description", "AccessTypeDescription");
						bulkCopy.ColumnMappings.Add("Message_Type", "MessageType");
						bulkCopy.ColumnMappings.Add("Message_Type_Description", "MessageTypeDescription");
						bulkCopy.ColumnMappings.Add("Direction", "Direction");
						bulkCopy.ColumnMappings.Add("Direction_Description", "DirectionDescription");
						bulkCopy.ColumnMappings.Add("Operator_Indicator", "OperatorIndicator");
						bulkCopy.ColumnMappings.Add("Payphone_Indicator", "PayphoneIndicator");
						bulkCopy.ColumnMappings.Add("Orig_lkCountry", "OriginationIkCountry");
						bulkCopy.ColumnMappings.Add("Orig_Number", "OriginationNumber");
						bulkCopy.ColumnMappings.Add("Orig_OCN", "OriginationOCN");
						bulkCopy.ColumnMappings.Add("Orig_Tier", "OriginationTier");
						bulkCopy.ColumnMappings.Add("Term_lkCountry", "TerminatingIkCountry");
						bulkCopy.ColumnMappings.Add("Term_Number", "TerminatingNumber");
						bulkCopy.ColumnMappings.Add("Term_OCN", "TerminatingOCN");
						bulkCopy.ColumnMappings.Add("Term_Tier", "TerminatingTier");
						bulkCopy.ColumnMappings.Add("Customer_Number", "CustomerNumber");
						bulkCopy.ColumnMappings.Add("Customer_City", "CustomerCity");
						bulkCopy.ColumnMappings.Add("Customer_County", "CustomerCounty");
						bulkCopy.ColumnMappings.Add("Customer_State", "CustomerState");
						bulkCopy.ColumnMappings.Add("Customer_Zip", "CustomerZip");
						bulkCopy.ColumnMappings.Add("Customer_Lata", "CustomerLata");
						bulkCopy.ColumnMappings.Add("Customer_OCN", "CustomerOCN");
						bulkCopy.ColumnMappings.Add("Display_Number", "DisplayNumber");
						bulkCopy.ColumnMappings.Add("Display_City", "DisplayCity");
						bulkCopy.ColumnMappings.Add("Display_County", "DisplayCounty");
						bulkCopy.ColumnMappings.Add("Display_State", "DisplayState");
						bulkCopy.ColumnMappings.Add("Display_Zip", "DisplayZip");
						bulkCopy.ColumnMappings.Add("Display_Lata", "DisplayLata");
						bulkCopy.ColumnMappings.Add("Display_OCN", "DisplayOCN");
						bulkCopy.ColumnMappings.Add("TierState", "TierState");
						bulkCopy.ColumnMappings.Add("Tier1", "Tier1");
						bulkCopy.ColumnMappings.Add("Tier2", "Tier2");
						bulkCopy.ColumnMappings.Add("AppliedRate", "AppliedRate");
						bulkCopy.ColumnMappings.Add("CalculatedDuration", "CalculatedDuration");
						bulkCopy.ColumnMappings.Add("DurationForRating", "RatingDuration");
						bulkCopy.ColumnMappings.Add("Surcharge", "Surcharge");
						bulkCopy.ColumnMappings.Add("TotalRevenue", "TotalRevenue");
						bulkCopy.ColumnMappings.Add("PreRated", "PreRated");
						bulkCopy.ColumnMappings.Add("Provider", "Provider");
						bulkCopy.ColumnMappings.Add("CurrencyID", "CurrencyID");
						bulkCopy.ColumnMappings.Add("BillDate", "BillDate");
						bulkCopy.ColumnMappings.Add("DateOfRecord", "DateOfRecord");

						bulkCopy.WriteToServer(sourceTable);
						bulkCopy.Close();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Insert data into the OneSourceNetworks_UsageSummary table. The table will be cleared of all data for an invoice before the load
		/// </summary>
		/// <param name="sourceTable">source data</param>
		/// <param name="connectionString">data store to connect</param>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="userName">login loading the table</param>
		public static void BulkCopy_OsnUsageSummary_DataToSqlServer(
			DataTable sourceTable,
			string connectionString,
			int invoiceID,
			string userName
			)
		{
			try
			{
				//Remove any existing entries
				using (MBMDbDataContext db = new MBMDbDataContext(connectionString))
				{
					int results = db.OSN_UsageSummary_DeleteByInvoice(invoiceID, userName);

					if (results != 0)
					{
						throw new Exception("Failed to delete existing entries for OsnUsageSummary");
					}
				}

				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
					{
						bulkCopy.DestinationTableName = "OneSourceNetworks_UsageSummary";

						bulkCopy.ColumnMappings.Add("InvoiceID", "InvoiceID");
						bulkCopy.ColumnMappings.Add("AccountNumber", "AccountNumber");
						bulkCopy.ColumnMappings.Add("BillingCycleID", "BillingCycle");
						bulkCopy.ColumnMappings.Add("ProductID", "ProductID");
						bulkCopy.ColumnMappings.Add("Calls", "Calls");
						bulkCopy.ColumnMappings.Add("Duration", "Duration");
						bulkCopy.ColumnMappings.Add("Revenue", "Revenue");
						bulkCopy.ColumnMappings.Add("GST", "GST");
						bulkCopy.ColumnMappings.Add("HST", "HST");
						bulkCopy.ColumnMappings.Add("PST", "PST");
						bulkCopy.ColumnMappings.Add("QST", "QST");
						bulkCopy.ColumnMappings.Add("Other", "OtherTax");
						bulkCopy.ColumnMappings.Add("CurrencyID", "CurrencyID");

						bulkCopy.WriteToServer(sourceTable);
						bulkCopy.Close();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Insert data into the OneSourceNetworks_FlatRate table. The table will be cleared of all data for an invoice before the load
		/// </summary>
		/// <param name="sourceTable">source data</param>
		/// <param name="connectionString">data store to connect</param>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="userName">login loading the table</param>
		public static void BulkCopy_OsnFlatRate_DataToSqlServer(
			DataTable sourceTable,
			string connectionString,
			int invoiceID,
			string userName
			)
		{
			try
			{
				//Remove any existing entries
				using (MBMDbDataContext db = new MBMDbDataContext(connectionString))
				{
					int results = db.OSN_FlatRate_DeleteByInvoice(invoiceID, userName);

					if (results != 0)
					{
						throw new Exception("Failed to delete existing entries for OsnFlatRate");
					}
				}

				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
					{
						bulkCopy.DestinationTableName = "OneSourceNetworks_FlatRate";

						bulkCopy.ColumnMappings.Add("InvoiceID", "InvoiceID");
						bulkCopy.ColumnMappings.Add("ProductID", "ProductID");
						bulkCopy.ColumnMappings.Add("GST", "GST");
						bulkCopy.ColumnMappings.Add("HST", "HST");
						bulkCopy.ColumnMappings.Add("PST", "PST");
						bulkCopy.ColumnMappings.Add("QST", "QST");
						bulkCopy.ColumnMappings.Add("Other", "OtherTax");
						bulkCopy.ColumnMappings.Add("CurrentMonth", "CurrentMonth");
						bulkCopy.ColumnMappings.Add("DaysProrated", "DaysProrated");
						bulkCopy.ColumnMappings.Add("AmountProRated", "AmountProrated");
						bulkCopy.ColumnMappings.Add("TotalCharges", "TotalCharges");
						bulkCopy.ColumnMappings.Add("MRC", "MRC");
						bulkCopy.ColumnMappings.Add("NRC", "NRC");
						bulkCopy.ColumnMappings.Add("AmountBilled", "AmountBilled");
						bulkCopy.ColumnMappings.Add("CurrencyID", "CurrencyID");

						bulkCopy.WriteToServer(sourceTable);
						bulkCopy.Close();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Insert data into the OneTimeCharges table. The table will be cleared of all data for an invoice before the load
		/// </summary>
		/// <param name="sourceTable">source data</param>
		/// <param name="connectionString">data store to connect</param>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="userName">login loading the table</param>
		public static void BulkCopy_OneTimeCharges_DataToSqlServer(
			DataTable sourceTable,
			string connectionString,
			int invoiceID,
			string userName
			)
		{
			try
			{
				//Remove any existing entries
				using (MBMDbDataContext db = new MBMDbDataContext(connectionString))
				{
					int results = db.OneTimeCharges_DeleteByInvoice(invoiceID, userName);

					if (results != 0)
					{
						throw new Exception("Failed to delete existing entries for OneTimeCharges");
					}
				}

				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
					{
						bulkCopy.DestinationTableName = "OneTimeCharges";
                        
						bulkCopy.ColumnMappings.Add("InvoiceID", "InvoiceID");
						if (sourceTable.Columns.Contains("BAN"))
						{
							bulkCopy.ColumnMappings.Add("BAN", "BAN");
						}
						bulkCopy.ColumnMappings.Add("10 Digit DID", "10 Digit DID");
						bulkCopy.ColumnMappings.Add("SSO", "SSO");
						bulkCopy.ColumnMappings.Add("Charge Date Parsed", "Charge Date");
						bulkCopy.ColumnMappings.Add("Charge Description", "Charge Description");
						bulkCopy.ColumnMappings.Add("Charge Type", "Charge Type");
						bulkCopy.ColumnMappings.Add("Service Start Date Parsed", "Service Start Date");
						bulkCopy.ColumnMappings.Add("Service End Date Parsed", "Service End Date");
						bulkCopy.ColumnMappings.Add("Invoice Bill Period Start Parsed", "Invoice Bill Period Start");
						bulkCopy.ColumnMappings.Add("Invoice Bill Period End Parsed", "Invoice Bill Period End");
						bulkCopy.ColumnMappings.Add("Total", "Total");
						bulkCopy.ColumnMappings.Add("Gateway Province", "GatewayProvince");

						bulkCopy.WriteToServer(sourceTable);
						bulkCopy.Close();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Insert data into the GE GCOM MLS table. The table will be cleared of all data for an invoice before the load
		/// </summary>
		/// <param name="sourceTable">source data</param>
		/// <param name="connectionString">data store to connect</param>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="userName">login loading the table</param>
		public static void BulkCopy_GEGCOMMultiLocationSummary_DataToSqlServer(
			DataTable sourceTable,
			string connectionString,
			int invoiceID,
			string userName
			)
		{
			try
			{
				//Remove any existing entries
				using (MBMDbDataContext db = new MBMDbDataContext(connectionString))
				{
					int results = db.GEGCOMMultiLocationSummary_Delete(invoiceID, userName);

					if (results != 0)
					{
						throw new Exception("Failed to delete existing entries for GEGCOMMultiLocationSummary");
					}
				}

				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
					{
						bulkCopy.DestinationTableName = "GEGCOMMultiLocationSummary";

						bulkCopy.ColumnMappings.Add("InvoiceID", "InvoiceID");
						bulkCopy.ColumnMappings.Add("ChildAccount", "ChildAccount");
						bulkCopy.ColumnMappings.Add("AcctName", "AcctName");
						bulkCopy.ColumnMappings.Add("Calls", "Calls");
						bulkCopy.ColumnMappings.Add("Minutes", "Minutes");
						bulkCopy.ColumnMappings.Add("CallCharges", "CallCharges");
						bulkCopy.ColumnMappings.Add("USF", "USF");
						bulkCopy.ColumnMappings.Add("ARF", "ARF");
						bulkCopy.ColumnMappings.Add("FederalTax", "FederalTax");
						bulkCopy.ColumnMappings.Add("StateTax", "StateTax");
						bulkCopy.ColumnMappings.Add("CountyTax", "CountyTax");
						bulkCopy.ColumnMappings.Add("CurrentMonth", "CurrentMonth");
						bulkCopy.ColumnMappings.Add("PreviousMonths", "PreviousMonths");
						bulkCopy.ColumnMappings.Add("DaysProrated", "DaysProrated");
						bulkCopy.ColumnMappings.Add("AmountProrated", "AmountProrated");
						bulkCopy.ColumnMappings.Add("TotalCharges", "TotalCharges");
						bulkCopy.ColumnMappings.Add("MRC", "MRC");
						bulkCopy.ColumnMappings.Add("NRC", "NRC");
						bulkCopy.ColumnMappings.Add("AmountBilled", "AmountBilled");

						bulkCopy.WriteToServer(sourceTable);
						bulkCopy.Close();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Bulk insert the EC500 file
		/// </summary>
		/// <param name="sourceTable">source</param>
		/// <param name="connectionString">datastore connection string</param>
		/// <param name="userName">user requesting the load</param>
		public static void BulkCopy_EC500_DataToSqlServer(
			DataTable sourceTable,
			string connectionString,
			string userName
			)
		{
			try
			{
				//Remove any existing entries
				using (MBMDbDataContext db = new MBMDbDataContext(connectionString))
				{
					int results = db.EC500_Delete(userName);

					if (results != 0)
					{
						throw new Exception("Failed to delete existing entries for EC500 table");
					}
				}

				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
					{
						bulkCopy.DestinationTableName = "EC500";

						//There are alternate names for each of the columns because the file is not
						//consistent from month to month
						if (sourceTable.Columns.Contains("10 Digit DID"))
						{
							bulkCopy.ColumnMappings.Add("10 Digit DID", "10 Digit DID");
						}
						else
						{
							bulkCopy.ColumnMappings.Add("GCOM #", "10 Digit DID");
						}

						if (sourceTable.Columns.Contains("Type"))
						{
							bulkCopy.ColumnMappings.Add("Type", "Type");
						}
						else
						{
							//The second column has no heading - this is very bad. Fix in Talend
							bulkCopy.ColumnMappings.Add("NoName", "Type");
						}

						if (sourceTable.Columns.Contains("Cell Phone DID"))
						{
							bulkCopy.ColumnMappings.Add("Cell Phone DID", "Cell Phone DID");
						}
						else if (sourceTable.Columns.Contains("Cell #"))
						{
							bulkCopy.ColumnMappings.Add("Cell #", "Cell Phone DID");
						}
						else
						{
							bulkCopy.ColumnMappings.Add("EC500 #", "Cell Phone DID");
						}

						bulkCopy.WriteToServer(sourceTable);
						bulkCopy.Close();
					}
				}
			}
			catch
			{
				throw;
			}
		}

        public static void BulkCopy_MBM_DataToSqlServer(
            DataTable sourceTable, 
            string connectionString)
        {
            // column mappings example:
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "mbm_FileInput";
                        bulkCopy.BulkCopyTimeout = 0;

                        bulkCopy.ColumnMappings.Add("id", "Id");
                        bulkCopy.ColumnMappings.Add("snapshot_id", "snapshotId");
                        bulkCopy.ColumnMappings.Add("subidentifier", "subIdentifier");
                        bulkCopy.ColumnMappings.Add("first_name", "firstName");
                        bulkCopy.ColumnMappings.Add("last_name", "lastName");
                        bulkCopy.ColumnMappings.Add("asset_search", "assetSearch");
                        bulkCopy.ColumnMappings.Add("service_profile_id", "serviceProfileId");
                        bulkCopy.ColumnMappings.Add("legal_entity", "legalEntity");
                        bulkCopy.ColumnMappings.Add("directory_number", "directoryNumber");
                        bulkCopy.ColumnMappings.Add("service_profile_uid", "sServiceProfileUid");

                        bulkCopy.WriteToServer(sourceTable);
                        bulkCopy.Close();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        
        public static void BulkCopy_ManualCharges_DataToSqlServer(
            DataTable sourceTable,
            string connectionString)
        {
            // column mappings example:
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                   
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "mbm_ManualCharge_Stage";
                        bulkCopy.BulkCopyTimeout = 0;
                        bulkCopy.ColumnMappings.Add("FileId", "iFileId");
                        bulkCopy.ColumnMappings.Add("InvoiceTypeId", "iInvoiceTypeId");
                        //bulkCopy.ColumnMappings.Add("LegalEntity", "sLegalEntity");
                        bulkCopy.ColumnMappings.Add("ProvisioningIdentifier (LegalEntity)*", "sLegalEntity");
                        bulkCopy.ColumnMappings.Add("Subidentifier", "sSubIdentifier");
                        //bulkCopy.ColumnMappings.Add("AssetSearch", "sAssetSearch");
                        bulkCopy.ColumnMappings.Add("AssetSearch (UserID)*", "sAssetSearch");
                        //bulkCopy.ColumnMappings.Add("ServiceProfile", "sServiceProfileId");
                        bulkCopy.ColumnMappings.Add("ServiceProfile ID (ChargeDescription)*", "sServiceProfileId");
                        bulkCopy.ColumnMappings.Add("StartDate*", "dtMainStart");
                        //bulkCopy.ColumnMappings.Add("StartDate", "dtMainStart");
                        bulkCopy.ColumnMappings.Add("EndDate", "dtMainEnd");
                        bulkCopy.ColumnMappings.Add("DirectoryNumber", "sDirectoryNumber");
                        bulkCopy.ColumnMappings.Add("ServiceProfileUid", "sServiceProfileUid");

                        bulkCopy.WriteToServer(sourceTable);
                        bulkCopy.Close();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

	}
}
