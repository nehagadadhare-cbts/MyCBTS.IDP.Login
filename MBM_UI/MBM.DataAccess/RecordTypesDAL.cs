using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.SqlClient;
using MBM.Entities;
//using GCOM.Library;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace MBM.DataAccess
{
	public class RecordTypesDAL
	{
		#region Setup

		private const string USGATEWAYSBAN = "202202";
		private const string USBAN = "101101";
		private const string CANADIANGATEWAYSBANIDENTIFIER = "GW";
		private const string CABANIDENTIFIER = "CA";
		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// This filter can be applied to reduce the type of entites returned
		/// </summary>
		public enum RECORDS_FILTER
		{
			ALL = 0,
			NO_GATEWAYS = 1,
			GATEWAYS_ONLY = 2,
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
		public RecordTypesDAL(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}

		#endregion

		#region RecordType1: Long Distance

		/// <summary>
		/// Process RecordType 1
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="callerLogin">login of the user calling this function</param>
		/// <param name="currencyID">currency to use</param>
		public void ProcessRecordType1(
			int invoiceID,
			string callerLogin,
			int currencyID
			)
		{
			try
			{

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{

                    int status = (int)db.mbm_RecordType1_Process(invoiceID, callerLogin, currencyID);
					if (status != 0)
					{
						throw new Exception(String.Format("Failed to process data ErrorCode={0}", status));
					}
				}

			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -240173);
				throw ex;
			}
		}

		/// <summary>
		/// Validate RecordType 1 entries
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		public void ValidateRecordType1(
			int invoiceID,
			out List<Entities.RecordValidator> entities
			)
		{
			entities = new List<Entities.RecordValidator>();

			try
			{

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<mbm_RecordType1_ValidateInvoiceResult> results = db.mbm_RecordType1_ValidateInvoice(invoiceID);
                    foreach (mbm_RecordType1_ValidateInvoiceResult r in results)
					{
						RecordValidator rv = new RecordValidator()
						{
							BAN = r.BAN,
							RecordTypeSum = r.RecordType1_Sum ?? (decimal)0.0,
							RecordType3Sum = r.RecordType3_Sum ?? (decimal)0.0,
							RecordType5Sum = r.RecordType5_Sum ?? (decimal)0.0,
							Validated = r.Validated,
						};

						if (rv != null)
						{
							entities.Add(rv);
						}
					}
				}

			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -473230);
				throw;
			}
		}        

		/// <summary>
		/// Fetch the RecordType 1 for a selected invoice
		/// </summary>
		/// <param name="invoiceID">invoice to get</param>
		/// <param name="filter">restriction on the callback</param>
		/// <param name="entities">list of entites returned</param>
		/// <returns>records</returns>
		public void GetRecordType1(int invoiceID, out List<Entities.RecordType1> entities)
		{
			entities = new List<Entities.RecordType1>();

			try
			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<mbm_RecordType1_GetByInvoiceResult> results = db.mbm_RecordType1_GetByInvoice(invoiceID, null);

                    foreach (mbm_RecordType1_GetByInvoiceResult r in results)
					{
						RecordType1 record = new RecordType1()
						{
							RecordType = r.Record_Type,
							BAN = r.BAN,
							InvoiceID = r.InvoiceID,
							InvoiceNumber = r.InvoiceNumber,
							//InvoiceType = (InvoiceTypeEnum)r.InvoiceType, //ER#6752
							CurrencyConversionID = r.CurrencyConversionID,
							BillDate = r.Bill_Date,
							//SSO = r.SSO,
							FromNumber = r.From_Number__DID_,
							ToNumber = r.From_To_Number,
							Revenue = r.Charge_Amount,
							DateOfRecord = r.Date_of_Record,
							ConnectTime = r.Connect_Time,
							Duration = r.Billable_Time,
							BillingNumberNorthAmericanStandard = r.Billing_Number_North_American_Standard,
							FromCity = r.From_Place,
							FromState = r.From_State,
							ToCity = r.To_Place,
							ToState = r.To_State,
							SettlementCode = r.Settlement_Code,
							ChargeDescription = r.Charge_Description,
							Provider = r.Provider,
							VendorName = r.Vendor_Name,
							Locality = r.State_Province,
                            LegalEntity = r.LegalEntity
						};

						if (record != null)
						{
							record.CurrentCurrency = currencyList.Find(delegate(CurrencyConversion c)
								{
									return c.ID == record.CurrencyConversionID;
								});
                            entities.Add(record);
                            
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -745591);
				throw;
			}
		}

		/// <summary>
		/// Get exceptions within the RecordType1 data store
		/// </summary>
		/// <param name="invoiceID">invoice number</param>
		/// <returns>exception entities</returns>
		public void GetRecordType1Exceptions(int invoiceID, out List<Entities.RecordType1Exceptions> entities)
		{
			entities = new List<Entities.RecordType1Exceptions>();

			try
			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<mbm_RecordType1_GetExceptionsByInvoiceResult> results = db.mbm_RecordType1_GetExceptionsByInvoice(invoiceID);

                    foreach (mbm_RecordType1_GetExceptionsByInvoiceResult r in results)
					{
						RecordType1Exceptions record = new RecordType1Exceptions()
						{
							DIDNumber = r.DIDNumber,
							RecordType = r.Record_Type,
							BAN = r.BAN,
							InvoiceID = r.InvoiceID,
							InvoiceNumber = r.InvoiceNumber,
							//InvoiceType = (InvoiceTypeEnum)r.InvoiceType, //ER#6752
							BillDate = r.Bill_Date,
							SSO = r.SSO,
							FromNumber = r.From_Number__DID_,
							ToNumber = r.From_To_Number,
							Revenue = r.Charge_Amount,
							DateOfRecord = r.Date_of_Record,
							ConnectTime = r.Connect_Time,
							Duration = r.Billable_Time,
							BillingNumberNorthAmericanStandard = r.Billing_Number_North_American_Standard,
							FromCity = r.From_Place,
							FromState = r.From_State,
							ToCity = r.To_Place,
							ToState = r.To_State,
							SettlementCode = r.Settlement_Code,
							ChargeDescription = r.Charge_Description,
							Provider = r.Provider,
							VendorName = r.Vendor_Name,
							Locality = r.State_Province,
							CurrencyConversionID = r.CurrencyConversionID,
						};

						if (record != null)
						{
							record.CurrentCurrency = currencyList.Find(
								delegate(CurrencyConversion c)
								{
									return c.ID == record.CurrencyConversionID;
								});

							entities.Add(record);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -862941);
				throw;
			}
		}

		#endregion

		#region RecordType2: Profiles, Monthly-reoccuring (MRC), Non-reoccuring (NRC)
		/// <summary>
		/// Process RecordType2
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="callerLogin">login of the user calling this function</param>
		/// <param name="currencyID">currency to use</param>
		public void ProcessRecordType2(int invoiceID, string callerLogin, int currencyID)
		{
			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    int status = (int)db.mbm_RecordType2_Process(invoiceID, callerLogin, currencyID);
					if (status != 0)
					{
						throw new Exception(String.Format("Failed to process data ErrorCode={0}", status));
					}
				}

			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -338132);
                throw ex;
			}
		}

        //cbe_8967
        /// <summary>
        /// Fetch OneTypeFileFormat for a single invoice
        /// </summary>
        /// <param name="invoiceID">invoice to get</param>
        /// <param name="filter">restriction on the callback</param>
        /// <param name="entities">list of entites returned</param>
        /// <returns>records</returns>
        public void GetOneTypeForSingleInvoice(int invoiceID, out List<Entities.RecordType1> entities)
        {
            entities = new List<Entities.RecordType1>();
            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RecordType1_GetByInvoice_OneTypeFileResult> results = db.mbm_RecordType1_GetByInvoice_OneTypeFile(invoiceID, null);

                    foreach (mbm_RecordType1_GetByInvoice_OneTypeFileResult r in results)
                    {
                        RecordType1 record = new RecordType1()
                        {
                            LegalEntity = r.LegalEntity,
                            BillingNumberNorthAmericanStandard = r.NA_Billing_Number,
                            CountOfCharges = r.Count_of_Charges,
                            Duration = r.Duration,
                            ChargeAmount = r.Charge_Amount
                        };
                        if (record != null)
                        {                            
                            entities.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -745591);
                throw;
            }
        }

        //cbe_11609
        /// <summary>
        /// Fetch OneTypeFileFormat for all invoice
        /// </summary>
        /// <param name="invoiceID">invoice to get</param>
        /// <param name="filter">restriction on the callback</param>
        /// <param name="entities">list of entites returned</param>
        /// <returns>records</returns>
        public void GetOneTypeForSingleInvoice_All(int invoiceID, out List<Entities.RecordType1> entities)
        {
            entities = new List<Entities.RecordType1>();
            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RecordType1_GetByInvoice_OneTypeFile_SOO_AllResult> results = db.mbm_RecordType1_GetByInvoice_OneTypeFile_SOO_All(invoiceID, null);

                    foreach (mbm_RecordType1_GetByInvoice_OneTypeFile_SOO_AllResult r in results)
                    {
                        RecordType1 record = new RecordType1()
                        {
                            LegalEntity = r.LegalEntity,
                            BillingNumberNorthAmericanStandard = r.NA_Billing_Number,
                            CountOfCharges = r.Count_of_Charges,
                            Duration = r.Duration,
                            ChargeAmount = r.Charge_Amount
                        };
                        if (record != null)
                        {
                            entities.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -745591);
                throw;
            }
        }

        /// <summary>
        /// Fetch OneTypeFileFormat for a selected invoice
        /// </summary>
        /// <param name="invoiceID">invoice to get</param>
        /// <param name="filter">restriction on the callback</param>
        /// <param name="entities">list of entites returned</param>
        /// <returns>records</returns>
        public void GetOneFileFormatDetail(int invoiceID, out List<Entities.RecordType2> entities)
        {
            entities = new List<Entities.RecordType2>();
            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RecordType2_GetByInvoice_OneFileResult> results = db.mbm_RecordType2_GetByInvoice_OneFile(invoiceID, null);

                    foreach (mbm_RecordType2_GetByInvoice_OneFileResult r in results)
                    {
                        RecordType2 record = new RecordType2()
                        {
                            ID = r.ID,
                            RecordType = r.Record_Type,
                            BAN = r.BAN,
                            InvoiceID = r.InvoiceID,
                            InvoiceNumber = r.InvoiceNumber,
                            InvoiceType = (InvoiceTypeEnum)r.InvoiceType,
                            CurrencyConversionID = r.CurrencyConversionID,
                            SSO = r.SSO,
                            UserId = r.AssetSearchCode1,
                            ChargeDate = r.Charge_Date,
                            ChargeDescription = r.Charge_Description,
                            ChargeType = r.Charge_Type,
                            ServiceStartDate = r.Service_Start_Date,
                            ServiceEndDate = r.Service_End_Date,
                            InvoiceBillPeriodStart = r.Invoice_Bill_Period_Start,
                            InvoiceBillPeriodEnd = r.Invoice_Bill_Period_End,
                            Total = r.Total,
                            FromNumber = r._10_Digit_DID,
                            VendorName = r.Vendor_Name,
                            Locality = r.State_Province,
                            LegalEntity = r.LegalEntity,
                            AssetSearchCode1 = r.AssetSearchCode1,
                            Department=r.Department,
                            CostCenter=r.Cost_Center,
                            ALI_Code=r.ALI_Code,
                            Supervisor=r.Supervisor
                        };

                        if (record != null)
                        {
                            record.CurrentCurrency = currencyList.Find(
                                delegate(CurrencyConversion c)
                                {
                                    return c.ID == record.CurrencyConversionID;
                                });
                            entities.Add(record);

                        }
                    }
                }

            }
            catch(Exception ex)
            {
                _logger.Exception(ex, -745591);
                throw;
            }
        }

        /// <summary>
        /// Fetch OneTypeFileFormat for a selected invoice, cbe_11609
        /// </summary>
        /// <param name="invoiceID">invoice to get</param>
        /// <param name="filter">restriction on the callback</param>
        /// <param name="entities">list of entites returned</param>
        /// <returns>records</returns>
        public void GetOneFileFormatDetail_All(int invoiceID, out List<Entities.RecordType2> entities)
        {
            entities = new List<Entities.RecordType2>();
            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RecordType2_GetByInvoice_OneFile_SOO_AllResult> results = db.mbm_RecordType2_GetByInvoice_OneFile_SOO_All(invoiceID, null);

                    foreach (mbm_RecordType2_GetByInvoice_OneFile_SOO_AllResult r in results)
                    {
                        RecordType2 record = new RecordType2()
                        {
                            ID = r.ID,
                            RecordType = r.Record_Type,
                            BAN = r.BAN,
                            InvoiceID = r.InvoiceID,
                            InvoiceNumber = r.InvoiceNumber,
                            InvoiceType = (InvoiceTypeEnum)r.InvoiceType,
                            CurrencyConversionID = r.CurrencyConversionID,
                            SSO = r.SSO,
                            UserId = r.AssetSearchCode1,
                            ChargeDate = r.Charge_Date,
                            ChargeDescription = r.Charge_Description,
                            ChargeType = r.Charge_Type,
                            ServiceStartDate = r.Service_Start_Date,
                            ServiceEndDate = r.Service_End_Date,
                            InvoiceBillPeriodStart = r.Invoice_Bill_Period_Start,
                            InvoiceBillPeriodEnd = r.Invoice_Bill_Period_End,
                            Total = r.Total,
                            FromNumber = r._10_Digit_DID,
                            VendorName = r.Vendor_Name,
                            Locality = r.State_Province,
                            LegalEntity = r.LegalEntity,
                            AssetSearchCode1 = r.AssetSearchCode1,
                            Department = r.Department,
                            CostCenter = r.Cost_Center,
                            ALI_Code = r.ALI_Code,
                            Supervisor = r.Supervisor
                        };

                        if (record != null)
                        {
                            record.CurrentCurrency = currencyList.Find(
                                delegate(CurrencyConversion c)
                                {
                                    return c.ID == record.CurrencyConversionID;
                                });
                            entities.Add(record);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -745591);
                throw;
            }
        }

		/// <summary>
		/// Retrieve RecordType2 from the data store
		/// </summary>
		/// <param name="invoiceID">invoice to pull</param>
		/// <returns>list of entities returned</returns>
		public void GetRecordType2(int invoiceID, out List<Entities.RecordType2> entities)
		{
			entities = new List<Entities.RecordType2>();

			try
			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<mbm_RecordType2_GetByInvoiceResult> results = db.mbm_RecordType2_GetByInvoice(invoiceID, null);

                    foreach (mbm_RecordType2_GetByInvoiceResult r in results)
					{
						RecordType2 record = new RecordType2()
						{
							ID = r.ID,
							RecordType = r.Record_Type,
							BAN = r.BAN,
							InvoiceID = r.InvoiceID,
							InvoiceNumber = r.InvoiceNumber,
							//InvoiceType = (InvoiceTypeEnum)r.InvoiceType,
							CurrencyConversionID = r.CurrencyConversionID,
							SSO = r.SSO,
                            UserId = r.AssetSearchCode1,
							ChargeDate = r.Charge_Date,
							ChargeDescription = r.Charge_Description,
							ChargeType = r.Charge_Type,
							ServiceStartDate = r.Service_Start_Date,
							ServiceEndDate = r.Service_End_Date,
							InvoiceBillPeriodStart = r.Invoice_Bill_Period_Start,
							InvoiceBillPeriodEnd = r.Invoice_Bill_Period_End,
							Total = r.Total,
							FromNumber = r._10_Digit_DID,
							VendorName = r.Vendor_Name,
							Locality = r.State_Province,
                            LegalEntity = r.LegalEntity
						};

						if (record != null)
						{
							record.CurrentCurrency = currencyList.Find(
								delegate(CurrencyConversion c)
								{
									return c.ID == record.CurrencyConversionID;
								});
                            entities.Add(record);
                            
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -227644);
				throw;
			}
		}

		/// <summary>
		/// Validate RecordType 2 entries
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		public void ValidateRecordType2(int invoiceID, out List<Entities.RecordValidator> entities)
		{
			entities = new List<Entities.RecordValidator>();

			try
			{

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<mbm_RecordType2_ValidateInvoiceResult> results = db.mbm_RecordType2_ValidateInvoice(invoiceID);
                    foreach (mbm_RecordType2_ValidateInvoiceResult r in results)
					{
						RecordValidator rv = new RecordValidator()
						{
							BAN = r.BAN,
							RecordTypeSum = r.RecordType2_Sum ?? (decimal)0.0,
							RecordType3Sum = r.RecordType3_Sum ?? (decimal)0.0,
							RecordType5Sum = r.RecordType5_Sum ?? (decimal)0.0,
							Validated = r.Validated,
						};

						if (rv != null)
						{
							entities.Add(rv);
						}
					}
				}

			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -946830);
				throw;
			}
		}

		/// <summary>
		/// Exceptions for Record Type 2
		/// </summary>
		/// <param name="invoiceID">invoice</param>
		/// <returns>exceptions found</returns>
		public void GetRecordType2Exceptions(int invoiceID, out List<Entities.RecordType2Exceptions> entities)
		{
			entities = new List<Entities.RecordType2Exceptions>();

			try
			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<mbm_RecordType2_GetExceptionsByInvoiceResult> results = db.mbm_RecordType2_GetExceptionsByInvoice(invoiceID);

                    foreach (mbm_RecordType2_GetExceptionsByInvoiceResult r in results)
					{
						RecordType2Exceptions record = new RecordType2Exceptions()
						{
							RecordType = r.RecordType,
							BAN = r.BAN,
							InvoiceID = r.InvoiceID,
							InvoiceNumber = r.InvoiceNumber,
							//InvoiceType = (InvoiceTypeEnum)r.InvoiceType, //ER#6752
							AssetSearchCode = r.p_Asset_Search_Code,
							SSO = r.SSO,
							ChargeDate = r.ChargeDate,
							ChargeDescription = r.ChargeDescription,
							ChargeType = r.ChargeType,
							ServiceStartDate = r.ServiceStartDate,
							ServiceEndDate = r.ServiceStopDate,
							InvoiceBillPeriodStart = r.InvoiceBillPeriodStart,
							InvoiceBillPeriodEnd = r.InvoiceBillPeriodEnd,
							Total = r.Total,
							CurrencyConversionID = r.CurrencyID,
						};

						if (record != null)
						{
							record.CurrentCurrency = currencyList.Find(
								delegate(CurrencyConversion c)
								{
									return c.ID == record.CurrencyConversionID;
								});

							entities.Add(record);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -189136);
				throw;
			}
		}

		#endregion

		#region RecordType3: Summary
		/// <summary>
		/// Process RecordType3
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="callerLogin">login of the user calling this function</param>
		/// <param name="currencyID">currency to use</param>
		public void ProcessRecordType3(int invoiceID, string callerLogin, int currencyID)
		{
			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    int status = db.mbm_RecordType3_Process(invoiceID, callerLogin, currencyID);
					if (status != 0)
					{
						throw new Exception(String.Format("Failed to process data ErrorCode={0}", status));
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -764695);
				throw ex;
			}
		}

        public void GetRecordType3(int invoiceID, out List<Entities.RecordType3> entities)
		{
			entities = new List<Entities.RecordType3>();

			try
			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<mbm_RecordType3_GetByInvoiceResult> results = db.mbm_RecordType3_GetByInvoice(invoiceID, null);

                    foreach (mbm_RecordType3_GetByInvoiceResult r in results)
					{
						RecordType3 record = new RecordType3()
						{
							ID = r.ID,
							RecordType = r.Record_Type,
							BAN = r.BAN,
							InvoiceID = r.InvoiceID,
							InvoiceNumber = r.InvoiceNumber,
							//InvoiceType = (InvoiceTypeEnum)r.InvoiceType, //ER#6752
							BillDate = r.Bill_Date,
							RecordType1Total = r.Record_Type_1_Total,
							RecordType2Total = r.Record_Type_2_Total,
							RecordType4Total = r.Record_Type_4_Total,
							Total = r.Total,
							CurrencyConversionID = r.CurrencyConversionID,
                            LegalEntity = r.LegalEntity,
						};

						if (record != null)
						{
							record.CurrentCurrency = currencyList.Find(
								 delegate(CurrencyConversion c)
								 {
									 return c.ID == record.CurrencyConversionID;
								 });
                            entities.Add(record);
                            
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -854374);
				throw;
			}
		}

        //cbe_11609
        public void GetRecordType3_All(int invoiceID, out List<Entities.RecordType3> entities)
        {
            entities = new List<Entities.RecordType3>();

            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RecordType3_GetByInvoice_SOO_AllResult> results = db.mbm_RecordType3_GetByInvoice_SOO_All(invoiceID, null);

                    foreach (mbm_RecordType3_GetByInvoice_SOO_AllResult r in results)
                    {
                        RecordType3 record = new RecordType3()
                        {
                            ID = r.ID,
                            RecordType = r.Record_Type,
                            BAN = r.BAN,
                            InvoiceID = r.InvoiceID,
                            InvoiceNumber = r.InvoiceNumber,
                            //InvoiceType = (InvoiceTypeEnum)r.InvoiceType, //ER#6752
                            BillDate = r.Bill_Date,
                            RecordType1Total = r.Record_Type_1_Total,
                            RecordType2Total = r.Record_Type_2_Total,
                            RecordType4Total = r.Record_Type_4_Total,
                            Total = r.Total,
                            CurrencyConversionID = r.CurrencyConversionID,
                            LegalEntity = r.LegalEntity,
                        };

                        if (record != null)
                        {
                            record.CurrentCurrency = currencyList.Find(
                                 delegate(CurrencyConversion c)
                                 {
                                     return c.ID == record.CurrencyConversionID;
                                 });
                            entities.Add(record);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -854374);
                throw;
            }
        }

		#endregion

        #region RT1Summary: Summary
        /// <summary>
        /// Process RT1Summary
        /// </summary>
        /// <param name="invoiceID">invoice identifier</param>
        /// <param name="callerLogin">login of the user calling this function</param>
        public void ProcessRT1Summary(int invoiceID,
            string callerLogin)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    int status = db.mbm_RT1Summary_Process(invoiceID);
                    if (status != 0)
                    {
                        throw new Exception(String.Format("Failed to process data ErrorCode={0}", status));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -764690);
                throw;
            }
        }


        public void GetRT1Summary(int invoiceID, out List<Entities.RT1Summary> entities)
        {
            entities = new List<Entities.RT1Summary>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RT1Summary_GetByInvoiceResult> results = db.mbm_RT1Summary_GetByInvoice(invoiceID);

                    foreach (mbm_RT1Summary_GetByInvoiceResult r in results)
                    {
                        RT1Summary record = new RT1Summary()
                        {
                            Zone = r.Zone,
                            NoOfTimes = r.NoOfTimes,
                            Duration = r.Duration,
                            CallCharges = r.CallCharges,
                            invoiceID = r.invoiceID
                        };

                        if (record != null)
                        {
                            entities.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -854374);
                throw;
            }
        }

        #endregion

        #region RT2Summary: Summary
        /// <summary>
        /// Process RT1Summary
        /// </summary>
        /// <param name="invoiceID">invoice identifier</param>
        /// <param name="callerLogin">login of the user calling this function</param>
        public void ProcessRT2Summary(int invoiceID,
            string callerLogin)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    int status = db.mbm_RT2Summary_Process(invoiceID);
                    if (status != 0)
                    {
                        throw new Exception(String.Format("Failed to process data ErrorCode={0}", status));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -764690);
                throw;
            }
        }


        public void GetRT2Summary(int invoiceID, out List<Entities.RT2Summary> entities)
        {
            entities = new List<Entities.RT2Summary>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RT2Summary_GetByInvoiceResult> results = db.mbm_RT2Summary_GetByInvoice(invoiceID);

                    foreach (mbm_RT2Summary_GetByInvoiceResult r in results)
                    {
                        RT2Summary record = new RT2Summary()
                        {
                            Profiles  = r.Profiles,
                            NoOfTimes = r.NoOfTimes,
                            Charges   = r.Charges,
                            invoiceID = r.iInvoiceID
                        };

                        if (record != null)
                        {
                            entities.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -854374);
                throw;
            }
        }

        //cbe_11609
        public void GetRT2Summary_All(int invoiceID, out List<Entities.RT2Summary> entities)
        {
            entities = new List<Entities.RT2Summary>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RT2Summary_GetByInvoice_SOO_AllResult> results = db.mbm_RT2Summary_GetByInvoice_SOO_All(invoiceID);

                    foreach (mbm_RT2Summary_GetByInvoice_SOO_AllResult r in results)
                    {
                        RT2Summary record = new RT2Summary()
                        {
                            Profiles = r.Profiles,
                            NoOfTimes = r.NoOfTimes,
                            Charges = r.Charges,
                            invoiceID = r.iInvoiceID
                        };

                        if (record != null)
                        {
                            entities.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -854374);
                throw;
            }
        }

        #endregion

		#region RecordType4: Taxes

		/// <summary>
		/// Process RecordType 4
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="callerLogin">login of the user calling this function</param>
		/// <param name="currencyID">currency to use</param>
		public void ProcessRecordType4(int invoiceID, string callerLogin, int currencyID)
		{
			try
			{

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    int status = (int)db.mbm_RecordType4_Process(invoiceID, callerLogin, currencyID);
					if (status != 0)
					{
						throw new Exception(String.Format("Failed to process data ErrorCode={0}", status));
					}
				}

			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -503744);
				throw ex;
			}
		}

		/// <summary>
		/// Retrieve RecordType4 from the data store
		/// </summary>
		/// <param name="invoiceID">invoice to pull</param>
		/// <returns>list of entities returned</returns>
		public void GetRecordType4(int invoiceID, out List<Entities.RecordType4> entities)
		{
			entities = new List<Entities.RecordType4>();

			try
			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<mbm_RecordType4_GetByInvoiceResult> results = db.mbm_RecordType4_GetByInvoice(invoiceID, null);

                    foreach (mbm_RecordType4_GetByInvoiceResult r in results)
					{
						RecordType4 record = new RecordType4()
						{
							RecordType = r.Record_Type,
							BAN = r.BAN,
							InvoiceID = r.InvoiceID,
							InvoiceNumber = r.InvoiceNumber,
							//InvoiceType = (InvoiceTypeEnum)r.InvoiceType, //ER#6752
							FromNumber = r._10_Digit_DID,
							SSO = r.SSO,
							ChargeDescription = r.Charge_Description,
							TaxCharge = r.Tax_Charge,
							TaxPercentage = r.Tax_Percentage,
							ServiceType = r.ServiceType,
							VendorName = r.Vendor_Name,
							BillDate = r.BillDate,
							Locality = r.State_Province,
							CurrencyConversionID = r.CurrencyConversionID,
                            LegalEntity = r.LegalEntity,
						};

						if (record != null)
						{
							record.CurrentCurrency = currencyList.Find(
								delegate(CurrencyConversion c)
								{
									return c.ID == record.CurrencyConversionID;
								});
                            entities.Add(record);
                            
						}

					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -941248);
				throw;
			}
		}


        /// <summary>
        /// Retrieve RecordType4 from the data store, Cbe_11609
        /// </summary>
        /// <param name="invoiceID">invoice to pull</param>
        /// <returns>list of entities returned</returns>
        public void GetRecordType4_All(int invoiceID, out List<Entities.RecordType4> entities)
        {
            entities = new List<Entities.RecordType4>();

            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RecordType4_GetByInvoice_SOO_AllResult> results = db.mbm_RecordType4_GetByInvoice_SOO_All(invoiceID, null);

                    foreach (mbm_RecordType4_GetByInvoice_SOO_AllResult r in results)
                    {
                        RecordType4 record = new RecordType4()
                        {
                            RecordType = r.Record_Type,
                            BAN = r.BAN,
                            InvoiceID = r.InvoiceID,
                            InvoiceNumber = r.InvoiceNumber,
                            //InvoiceType = (InvoiceTypeEnum)r.InvoiceType, //ER#6752
                            FromNumber = r._10_Digit_DID,
                            SSO = r.SSO,
                            ChargeDescription = r.Charge_Description,
                            TaxCharge = r.Tax_Charge,
                            TaxPercentage = r.Tax_Percentage,
                            ServiceType = r.ServiceType,
                            VendorName = r.Vendor_Name,
                            BillDate = r.BillDate,
                            Locality = r.State_Province,
                            CurrencyConversionID = r.CurrencyConversionID,
                            LegalEntity = r.LegalEntity,
                        };

                        if (record != null)
                        {
                            record.CurrentCurrency = currencyList.Find(
                                delegate(CurrencyConversion c)
                                {
                                    return c.ID == record.CurrencyConversionID;
                                });
                            entities.Add(record);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -941248);
                throw;
            }
        }

        /// <summary>
        /// Retrieve RecordType4 from the data store
        /// </summary>
        /// <param name="invoiceID">invoice to pull</param>
        /// <returns>list of entities returned</returns>
        public void GetOneRecordType(int invoiceID, out List<Entities.RecordType4> entities)
        {
            entities = new List<Entities.RecordType4>();

            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RecordType4_GetByInvoiceResult> results = db.mbm_RecordType4_GetByInvoice(invoiceID, null);

                    foreach (mbm_RecordType4_GetByInvoiceResult r in results)
                    {
                        RecordType4 record = new RecordType4()
                        {
                            RecordType = r.Record_Type,
                            BAN = r.BAN,
                            InvoiceID = r.InvoiceID,
                            InvoiceNumber = r.InvoiceNumber,
                            //InvoiceType = (InvoiceTypeEnum)r.InvoiceType, //ER#6752
                            FromNumber = r._10_Digit_DID,
                            SSO = r.SSO,
                            ChargeDescription = r.Charge_Description,
                            TaxCharge = r.Tax_Charge,
                            TaxPercentage = r.Tax_Percentage,
                            ServiceType = r.ServiceType,
                            VendorName = r.Vendor_Name,
                            BillDate = r.BillDate,
                            Locality = r.State_Province,
                            CurrencyConversionID = r.CurrencyConversionID,
                            LegalEntity = r.LegalEntity,
                        };

                        if (record != null)
                        {
                            record.CurrentCurrency = currencyList.Find(
                                delegate(CurrencyConversion c)
                                {
                                    return c.ID == record.CurrencyConversionID;
                                });
                            entities.Add(record);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -941248);
                throw;
            }
        }

		/// <summary>
		/// Validate RecordType 4 entries
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		public void ValidateRecordType4(int invoiceID, out List<Entities.RecordValidator> entities)
		{
			entities = new List<Entities.RecordValidator>();

			try
			{

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<mbm_RecordType4_ValidateInvoiceResult> results = db.mbm_RecordType4_ValidateInvoice(invoiceID);
                    foreach (mbm_RecordType4_ValidateInvoiceResult r in results)
					{
						RecordValidator rv = new RecordValidator()
						{
							BAN = r.BAN,
							RecordTypeSum = r.RecordType4_Sum ?? (decimal)0.0,
							RecordType3Sum = r.RecordType3_Sum ?? (decimal)0.0,
							RecordType5Sum = r.RecordType5_Sum ?? (decimal)0.0,
							Validated = r.Validated,
						};

						if (rv != null)
						{
							entities.Add(rv);
						}
					}
				}

			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -443187);
				throw;
			}
		}

		/// <summary>
		/// Get the exceptions for Record Type 4
		/// </summary>
		/// <param name="invoiceID"></param>
		/// <returns></returns>
		public void GetRecordType4Exceptions(int invoiceID, out List<Entities.RecordType4Exceptions> entities)
		{
			entities = new List<Entities.RecordType4Exceptions>();

			try
			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<mbm_RecordType4_GetExceptionsByInvoiceResult> results = db.mbm_RecordType4_GetExceptionsByInvoice(invoiceID);

                    foreach (mbm_RecordType4_GetExceptionsByInvoiceResult r in results)
					{
						RecordType4Exceptions record = new RecordType4Exceptions()
						{
							ID = r.ID,
							InvoiceID = r.InvoiceID,
							InvoiceNumber = r.InvoiceNumber,
							//InvoiceType = (InvoiceTypeEnum)r.InvoiceType,
							RecordType = r.RecordType,
							CustomerNumber = r.CustomerNumber,
							PhoneNumber = r.PhoneNumber,
							CallType = r.CallType,
							TotalCallCount = r.TotalCallCount,
							TotalCallDuration = r.TotalCallDuration,
							UsageTotal = r.UsageTotal,
							InterIntlUsage = r.Inter_IntlUsage,
							State = r.State,
							Local = r.Local,
							Federal = r.Federal,
							USF = r.USF,
							ARF = r.ARF,
							CurrencyConversionID = r.DefaultImportCurrencyID ?? 0,
						};

						if (record != null)
						{
							record.CurrentCurrency = currencyList.Find(
								delegate(CurrencyConversion c)
								{
									return c.ID == record.CurrencyConversionID;
								});

							entities.Add(record);
						}

					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -681834);
				throw;
			}
		}

		#endregion

        #region RT2Summary: Summary
        /// <summary>
        /// Process RT1Summary
        /// </summary>
        /// <param name="invoiceID">invoice identifier</param>
        /// <param name="callerLogin">login of the user calling this function</param>
        public void ProcessRT4Summary(int invoiceID,
            string callerLogin)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    int status = db.mbm_RT4Summary_Process(invoiceID);
                    if (status != 0)
                    {
                        throw new Exception(String.Format("Failed to process data ErrorCode={0}", status));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -764690);
                throw;
            }
        }


        public void GetRT4Summary(int invoiceID, out List<Entities.RT4Summary> entities)
        {
            entities = new List<Entities.RT4Summary>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<mbm_RT4Summary_GetByInvoiceResult> results = db.mbm_RT4Summary_GetByInvoice(invoiceID);

                    foreach (mbm_RT4Summary_GetByInvoiceResult r in results)
                    {
                        RT4Summary record = new RT4Summary()
                        {
                            ChargeDescription = r.ChargeDescription,
                            NoOfTimes = r.NoOfTimes,
                            TaxCharges = r.TaxCharges,
                            invoiceID = r.invoiceID
                        };

                        if (record != null)
                        {
                            entities.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -854374);
                throw;
            }
        }

        #endregion

		#region RecordType5: Totals
		/// <summary>
		/// Process RecordType 5
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="currencyID">currency to use</param>
		public void ProcessRecordType5(int invoiceID, string callerLogin, int currencyID)
		{
			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    db.mbm_RecordType5_Process(invoiceID, callerLogin, currencyID);
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -627781);
				throw ex;
			}
		}

		/// <summary>
		/// Get a list of the entries for Record Type 5
		/// </summary>
		/// <param name="invoiceID">invoice to fetch</param>
		/// <param name="filter">what entries to filter out</param>
		/// <param name="entities">list of entries</param>
		public void GetRecordType5(int invoiceID, out List<Entities.RecordType5> entities)
		{
			entities = new List<Entities.RecordType5>();

			try

			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
                    ISingleResult<mbm_RecordType5_GetByInvoiceResult> results = db.mbm_RecordType5_GetByInvoice(invoiceID, null);

                    foreach (mbm_RecordType5_GetByInvoiceResult r in results)
					{
						RecordType5 record = new RecordType5()
						{
							RecordType = r.Record_Type,
							BAN = r.BAN,
							CheckRecordType = r.Check_Record_Type,
							TotalRecordCount = r.Total_Record_Count,
							SumFieldName = r.SumField_Name,
							TotalAmount = r.TotalAmount,
							CurrencyConversionID = r.CurrencyConversionID,
                            LegalEntity = r.LegalEntity,
                            InvoiceID = (int)r.InvoiceID
						};

						if (record != null)
						{
							record.CurrentCurrency = currencyList.Find(
							delegate(CurrencyConversion c)
							{
								return c.ID == record.CurrencyConversionID;
							});
						}
                        entities.Add(record);
                        
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -854374);
				throw;
			}
		}


		[Obsolete]
		public void GetRecordType5_GatewaysOnly(int invoiceID, int currencyId, string callerLogin, out List<Entities.RecordType5> entities)
		{
			entities = new List<Entities.RecordType5>();

			try
			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<RecordType5_GetByInvoiceResult> results = db.RecordType5_GetByInvoice(invoiceID, null);

					foreach (RecordType5_GetByInvoiceResult r in results)
					{
						if (r.BAN == USGATEWAYSBAN || r.BAN == CANADIANGATEWAYSBANIDENTIFIER)
						{
							RecordType5 record = new RecordType5()
							{
								RecordType = r.Record_Type,
								CheckRecordType = r.Check_Record_Type,
								TotalRecordCount = r.Total_Record_Count,
								SumFieldName = r.SumField_Name,
								TotalAmount = r.TotalAmount,
								CurrencyConversionID = r.CurrencyConversionID,
							};

							if (record != null)
							{
								record.CurrentCurrency = currencyList.Find(
									 delegate(CurrencyConversion c)
									 {
										 return c.ID == record.CurrencyConversionID;
									 });

								entities.Add(record);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -854374);
				throw;
			}
		}
		
        /// <summary>
		/// Retrieve RecordType5 from the data store
		/// </summary>
		/// <param name="invoiceID">invoice to pull</param>
		/// <returns>list of entities returned</returns>
		[Obsolete]
		public void GetRecordType5_ByInvoice(int invoiceID, RECORDS_FILTER filter, out List<Entities.RecordType5> entities)
		{
			entities = new List<Entities.RecordType5>();

			try
			{
				bool added = false;
				RecordType5 temp = null;

				decimal recordType2Sum = 0;
				int recordType2Count = 0;

				decimal recordType3Sum = 0;
				int recordType3Count = 0;

				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<RecordType5_GetByInvoiceResult> results = db.RecordType5_GetByInvoice(invoiceID, null);

					foreach (RecordType5_GetByInvoiceResult r in results)
					{
						switch (r.Check_Record_Type)
						{
							case 1:

								AddRecordType5(ref entities, r.Record_Type, r.Check_Record_Type, r.Total_Record_Count, r.SumField_Name, r.TotalAmount, r.CurrencyConversionID);
								break;
							case 2:
								if (recordType2Sum == 0)
								{
									recordType2Sum += r.TotalAmount;
									recordType2Count += r.Total_Record_Count;
									temp = new RecordType5()
									{
										RecordType = r.Record_Type,
										CheckRecordType = r.Check_Record_Type,
										TotalRecordCount = r.Total_Record_Count,
										SumFieldName = r.SumField_Name,
										TotalAmount = r.TotalAmount,
										CurrencyConversionID = r.CurrencyConversionID,
									};

									added = false;
								}
								else
								{
									recordType2Sum += r.TotalAmount;
									recordType2Count += r.Total_Record_Count;
									AddRecordType5(ref entities, r.Record_Type, r.Check_Record_Type, recordType2Count, r.SumField_Name, recordType2Sum, r.CurrencyConversionID);
									added = true;
								}
								break;
							case 3:
								if (added == false && temp != null)
								{

									AddRecordType5(ref entities, temp);
									added = true;
									temp = null;
								}

								if (recordType3Sum == 0)
								{
									recordType3Sum += r.TotalAmount;
									recordType3Count += r.Total_Record_Count;
									temp = new RecordType5()
									{
										RecordType = r.Record_Type,
										CheckRecordType = r.Check_Record_Type,
										TotalRecordCount = r.Total_Record_Count,
										SumFieldName = r.SumField_Name,
										TotalAmount = r.TotalAmount,
										CurrencyConversionID = r.CurrencyConversionID,
									};

									added = false;
								}
								else
								{
									recordType3Sum += r.TotalAmount;
									recordType3Count += r.Total_Record_Count;
									AddRecordType5(ref entities, r.Record_Type, r.Check_Record_Type, recordType3Count, r.SumField_Name, recordType3Sum, r.CurrencyConversionID);
								}
								break;
							case 4:
								if (added == false && temp != null)
								{

									AddRecordType5(ref entities, temp);
									added = true;
									temp = null;
								}
								AddRecordType5(ref entities, r.Record_Type, r.Check_Record_Type, r.Total_Record_Count, r.SumField_Name, r.TotalAmount, r.CurrencyConversionID);
								break;

						}

					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -365704);
				throw;
			}
		}

		public void AddRecordType5(ref List<Entities.RecordType5> entities, int record_Type, int check_Record_Type, int total_Record_Count, string sumField_Name, decimal totalAmount, int currencyConversionID)
		{
			try
			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);
				RecordType5 record = new RecordType5()
			{
				RecordType = record_Type,
				CheckRecordType = check_Record_Type,
				TotalRecordCount = total_Record_Count,
				SumFieldName = sumField_Name,
				TotalAmount = totalAmount,
				CurrencyConversionID = currencyConversionID,
			};

				if (record != null)
				{
					record.CurrentCurrency = currencyList.Find(
						delegate(CurrencyConversion c)
						{
							return c.ID == record.CurrencyConversionID;
						});

					entities.Add(record);
				}

			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -365704);
				throw;
			}
		}
		
        public void AddRecordType5(ref List<Entities.RecordType5> entities, RecordType5 record)
		{
			try
			{
				List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

				if (record != null)
				{
					record.CurrentCurrency = currencyList.Find(
						 delegate(CurrencyConversion c)
						 {
							 return c.ID == record.CurrencyConversionID;
						 });

					entities.Add(record);
				}

			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -365704);
				throw;
			}
		}
		#endregion

		#region RecordType6: Accounts
		/// <summary>
		/// Retrieve list of record type 6 for select invoice
		/// </summary>
		/// <param name="invoiceID">invoice identifier to generate</param>
		/// <param name="entities">list of entities</param>
		/// <returns>positive on success or negative on failure</returns>
		public void GetRecordType6(int invoiceID, out List<Entities.RecordType6> entities)
		{
			entities = new List<Entities.RecordType6>();

			try
			{
				using (MBMDbDataContext db = new MBMDbDataContext(_connection))
				{
					ISingleResult<RecordType6_GetResult> results = db.RecordType6_Get(invoiceID);

					foreach (RecordType6_GetResult r in results)
					{
						RecordType6 record = new RecordType6()
						{
							RecordType = r.Record_Type,
							AddressType = r.UserAddressType,
							AssetAddress = r.Asset_Address,
							AssetStatus = r.AssetStatus,
							AssetTagNumber = r.Asset_Tag_Number,
							Brand = r.Brand,
							BusinessUnit = r.BusinessUnit,
							City = r.UserCity,
							Country = r.UserCountry,
							Department = r.Department,
							DID = r.DID,
							Email = r.Email,
							FirstName = r.FirstName,
							LastName = r.LastName,
							IpAddress = r.IP_Address,
							MacAddress = r.MAC_Address,
							Gateway = r.Gateway,
							LegalEntity = r.Legal_Entity,
							Location = r.Location_GCOM,
							Mobile = r.Mobile_Cell,
							Model = r.Model,
							PostalCode = r.UserZip_Postal_Code,
							PspOrderID = r.PSP_Order_ID,
							SerialNumber = r.Serial_Number,
							ServiceProfileID = r.ServiceProfile_ID,
							SiteID = r.Site_ID,
							SSO = r.SSO,
							StartDate = r.StartDate,
							StopDate = r.StopDate,
							State = r.UserState,
							Status = r.UserStatus,
							Street1 = r.UserStreet1,
							Street2 = r.UserStreet2,
						};

						if (record != null)
						{
							entities.Add(record);
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, -566686);
				throw;
			}
		}

		#endregion

        //Lokesh
        /// <summary>
        /// Fetch the LDRecordType 1 for a selected customer
        /// </summary>
        /// <param name="invoiceID">customer to get</param>
        /// <param name="filter">restriction on the callback</param>
        /// <param name="entities">list of entites returned</param>
        /// <returns>records</returns>
        public void GetLDRecordType1(int customerNumber, RECORDS_FILTER filter, out List<Entities.RecordType1> entities)
        {
            entities = new List<Entities.RecordType1>();

            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<RecordType1_GetByCustomerNumberResult> results = db.RecordType1_GetByCustomerNumber(customerNumber);

                    foreach (RecordType1_GetByCustomerNumberResult r in results)
                    {
                        RecordType1 record = new RecordType1()
                        {
                            RecordType = r.Record_Type,
                            BAN = r.BAN,
                            InvoiceNumber = r.Invoice_Number,
                            CurrencyConversionID = r.CurrencyConversionID,
                            CallTime = r.Billable_Time,
                            BillDate = r.Bill_Date,
                            Duration = (r.Billable_Time!=string.Empty)? Convert.ToDecimal(r.Billable_Time):0,
                            SSO = r.SSO,
                            FromNumber = r.From_Number__DID_,
                            ToNumber = r.From_To_Number,
                            Revenue = r.Charge_Amount,
                            DateOfRecord = r.Date_of_Record,
                            ConnectTime = r.Connect_Time.ToString(),
                            BillingNumberNorthAmericanStandard = r.Billing_Number_North_American_Standard,
                            FromCity = r.From_Place,
                            FromState = r.From_State,
                            ToCity = r.To_Place,
                            ToState = r.To_State,
                            SettlementCode =Convert.ToInt32(r.Settlement_Code),
                            ChargeDescription = r.Charge_Description,
                            Provider = r.Provider,
                            VendorName = r.Vendor_Name,
                            Locality = r.State_Province,
                        };

                        if (record != null)
                        {
                            record.CurrentCurrency = currencyList.Find(
                                delegate(CurrencyConversion c)
                                {
                                    return c.ID == record.CurrencyConversionID;
                                });

                            entities.Add(record);

                            //switch (filter)
                            //{
                            //    case RECORDS_FILTER.NO_GATEWAYS:
                            //        if (r.BAN == USBAN
                            //            || (!r.BAN.Contains(CANADIANGATEWAYSBANIDENTIFIER) && r.BAN != USGATEWAYSBAN)
                            //            )
                            //        {
                            //            entities.Add(record);
                            //        }
                            //        break;

                            //    case RECORDS_FILTER.GATEWAYS_ONLY:
                            //        if (r.BAN == USGATEWAYSBAN || r.BAN.Contains(CANADIANGATEWAYSBANIDENTIFIER))
                            //        {
                            //            record.InvoiceNumber = "GW" + record.InvoiceNumber.Substring(2); //Need to replace the MT or CA in the invoice number
                            //            entities.Add(record);
                            //        }
                            //        break;

                            //    default:
                            //        entities.Add(record);
                            //        break;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -745591);
                throw;
            }
        }

        /// <summary>
        /// Retrieve RecordType2 from the data store
        /// </summary>
        /// <param name="invoiceID">Customer number to pull</param>
        /// <returns>list of entities returned</returns>
        public void GetLDRecordType2(int CustomerNumber, RECORDS_FILTER filter, out List<Entities.RecordType2> entities)
        {
            entities = new List<Entities.RecordType2>();

            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<RecordType2_GetByCustomerNumberResult> results = db.RecordType2_GetByCustomerNumber(CustomerNumber);

                    foreach (RecordType2_GetByCustomerNumberResult r in results)
                    {
                        RecordType2 record = new RecordType2()
                        {
                           
                            RecordType =Convert.ToInt32(r.Record_Type),
                            BAN = r.BAN,
                            InvoiceNumber = r.Invoice_Number,
                            CurrencyConversionID = r.CurrencyConversionID,
                            SSO = r.SSO,
                            ChargeDate = r.Charge_Date,
                            ChargeDescription = r.Charge_Description,
                            ChargeType = r.Charge_Type,
                            ServiceStartDate = r.Service_Start_Date,
                            ServiceEndDate = r.Service_End_Date,
                            InvoiceBillPeriodStart = r.Invoice_Bill_Period_Start,
                            InvoiceBillPeriodEnd = r.Invoice_Bill_Period_End,
                            Total = r.Total,
                            FromNumber = r._10_Digit_DID,
                            VendorName = r.Vendor_Name,
                            Locality = r.State_Province,
                        };

                        if (record != null)
                        {
                            record.CurrentCurrency = currencyList.Find(
                                delegate(CurrencyConversion c)
                                {
                                    return c.ID == record.CurrencyConversionID;
                                });

                            entities.Add(record);
                            //switch (filter)
                            //{
                            //    case RECORDS_FILTER.NO_GATEWAYS:
                            //        if (r.BAN == USBAN
                            //            || (!r.BAN.Contains(CANADIANGATEWAYSBANIDENTIFIER) && r.BAN != USGATEWAYSBAN)
                            //            )
                            //        {
                            //            entities.Add(record);
                            //        }
                            //        break;

                            //    case RECORDS_FILTER.GATEWAYS_ONLY:
                            //        if (r.BAN == USGATEWAYSBAN || r.BAN.Contains(CANADIANGATEWAYSBANIDENTIFIER))
                            //        {
                            //            record.InvoiceNumber = "GW" + record.InvoiceNumber.Substring(2); //Need to replace the MT or CA in the invoice number
                            //            entities.Add(record);
                            //        }
                            //        break;

                            //    default:
                            //        entities.Add(record);
                            //        break;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -227644);
                throw;
            }
        }

        /// <summary>
        /// Retrieve RecordType3 from the data store
        /// </summary>
        /// <param name="CustomerNumber">Customer number to pull</param>
        /// <returns>list of entities returned</returns>
        public void GetLDRecordType3(int CustomerNumber, RECORDS_FILTER filter, out List<Entities.RecordType3> entities)
        {
            entities = new List<Entities.RecordType3>();

            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<RecordType3_GetByCustomerNumberResult> results = db.RecordType3_GetByCustomerNumber(CustomerNumber);

                    foreach (RecordType3_GetByCustomerNumberResult r in results)
                    {
                        RecordType3 record = new RecordType3()
                        {

                            RecordType =Convert.ToInt32(r.Record_Type),
                            BAN = r.BAN,
                            InvoiceNumber = r.Invoice_Number,
                            BillDate = r.Bill_Date,
                            RecordType1Total = r.Record_Type_1_Total,
                            RecordType2Total = r.Record_Type_2_Total,
                            RecordType4Total = r.Record_Type_4_Total,
                            Total = r.Total,
                            CurrencyConversionID = r.CurrencyConversionID,
                        };

                        if (record != null)
                        {
                            record.CurrentCurrency = currencyList.Find(
                                 delegate(CurrencyConversion c)
                                 {
                                     return c.ID == record.CurrencyConversionID;
                                 });

                            entities.Add(record);

                            //switch (filter)
                            //{
                            //    case RECORDS_FILTER.NO_GATEWAYS:
                            //        if (r.BAN == USBAN
                            //            || (!r.BAN.Contains(CANADIANGATEWAYSBANIDENTIFIER) && r.BAN != USGATEWAYSBAN)
                            //            )
                            //        {
                            //            entities.Add(record);
                            //        }
                            //        break;

                            //    case RECORDS_FILTER.GATEWAYS_ONLY:
                            //        if (r.BAN == USGATEWAYSBAN || r.BAN.Contains(CANADIANGATEWAYSBANIDENTIFIER))
                            //        {
                            //            record.InvoiceNumber = "GW" + record.InvoiceNumber.Substring(2); //Need to replace the MT or CA in the invoice number
                            //            entities.Add(record);
                            //        }
                            //        break;

                            //    default:
                            //        entities.Add(record);
                            //        break;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -854374);
                throw;
            }
        }

        /// <summary>
        /// Retrieve RecordType4 from the data store
        /// </summary>
        /// <param name="CustomerNumber">Customer number to pull</param>
        /// <returns>list of entities returned</returns>
        public void GetLDRecordType4(int CustomerNumber, RECORDS_FILTER filter, out List<Entities.RecordType4> entities)
        {
            entities = new List<Entities.RecordType4>();

            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<RecordType4_GetByCustomerNumberResult> results = db.RecordType4_GetByCustomerNumber(CustomerNumber);

                    foreach (RecordType4_GetByCustomerNumberResult r in results)
                    {
                        RecordType4 record = new RecordType4()
                        {

                            RecordType =Convert.ToInt32(r.Record_Type),
                            BAN = r.BAN,
                            InvoiceNumber = r.Invoice_Number,
                            FromNumber=r._10_digit_DID,
                            SSO=r.SSO,
                            ChargeDescription=r.Charge_Description,
                            TaxCharge=r.Tax_Charge,
                            TaxPercentage=r.Tax__,
                            ServiceType=r.Service_Type,
                            VendorName=r.Vendor_Name,
                            BillDate = r.Bill_Date,
                            Locality=r.State_Province,
                            CurrencyConversionID = r.CurrencyConversionID,
                        };

                        if (record != null)
                        {
                            record.CurrentCurrency = currencyList.Find(
                                 delegate(CurrencyConversion c)
                                 {
                                     return c.ID == record.CurrencyConversionID;
                                 });

                            entities.Add(record);
                            //switch (filter)
                            //{
                            //    case RECORDS_FILTER.NO_GATEWAYS:
                            //        if (r.BAN == USBAN
                            //            || (!r.BAN.Contains(CANADIANGATEWAYSBANIDENTIFIER) && r.BAN != USGATEWAYSBAN)
                            //            )
                            //        {
                            //            entities.Add(record);
                            //        }
                            //        break;

                            //    case RECORDS_FILTER.GATEWAYS_ONLY:
                            //        if (r.BAN == USGATEWAYSBAN || r.BAN.Contains(CANADIANGATEWAYSBANIDENTIFIER))
                            //        {
                            //            record.InvoiceNumber = "GW" + record.InvoiceNumber.Substring(2); //Need to replace the MT or CA in the invoice number
                            //            entities.Add(record);
                            //        }
                            //        break;

                            //    default:
                            //        entities.Add(record);
                            //        break;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -854374);
                throw;
            }
        }

        /// <summary>
        /// Retrieve RecordType5 from the data store
        /// </summary>
        /// <param name="CustomerNumber">Customer number to pull</param>
        /// <returns>list of entities returned</returns>
        public void GetLDRecordType5(int CustomerNumber, RECORDS_FILTER filter, out List<Entities.RecordType5> entities)
        {
            entities = new List<Entities.RecordType5>();

            try
            {
                List<CurrencyConversion> currencyList = new DataFactory(_connection).Configurations.GetCurrencyConversions(null);

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<RecordType5_GetByCustomerNumberResult> results = db.RecordType5_GetByCustomerNumber(CustomerNumber);

                    foreach (RecordType5_GetByCustomerNumberResult r in results)
                    {
                        RecordType5 record = new RecordType5()
                        {
                            BAN=r.Invoice_Number,
                            RecordType = Convert.ToInt32(r.Record_Type),
                            CheckRecordType=Convert.ToInt32(r.Check_Record_Type),
                            TotalRecordCount=r.Total_Record_Count,
                            SumFieldName=r.SumField_Name,
                            TotalAmount=r.Total_Amount,
                            CurrencyConversionID = r.CurrencyConversionID,
                        };

                        if (record != null)
                        {
                            record.CurrentCurrency = currencyList.Find(
                                 delegate(CurrencyConversion c)
                                 {
                                     return c.ID == record.CurrencyConversionID;
                                 });

                            entities.Add(record);
                            //switch (filter)
                            //{
                            //    case RECORDS_FILTER.NO_GATEWAYS:
                            //        //if (r.BAN == USBAN
                            //        //    || (!r.BAN.Contains(CANADIANGATEWAYSBANIDENTIFIER) && r.BAN != USGATEWAYSBAN)
                            //        //    )
                            //        //{
                            //        //    entities.Add(record);
                            //        //}
                            //        break;

                            //    case RECORDS_FILTER.GATEWAYS_ONLY:
                            //        //if (r.BAN == USGATEWAYSBAN || r.BAN.Contains(CANADIANGATEWAYSBANIDENTIFIER))
                            //        //{
                            //        //    record.InvoiceID = "GW" + record.InvoiceID.ToString().Substring(2); //Need to replace the MT or CA in the invoice number
                            //        //    entities.Add(record);
                            //        //}
                            //        break;

                            //    default:
                            //        entities.Add(record);
                            //        break;
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -854374);
                throw;
            }
        }

        /// <summary>
        /// Process  Long Distance RecordTypes
        /// </summary>
        /// <param name="CustomerNumner">Customer number</param>
        /// <param name="callerLogin">login of the user calling this function</param>
        public void ProcessLDRecordTypes(string CustomerNumner, string callerLogin)
        {
            try
            {

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {

                    int status = (int)db.ProcessRecordTypesForLD(CustomerNumner, callerLogin);
                    if (status != 0)
                    {
                        throw new Exception(String.Format("Failed to process data ErrorCode={0}", status));
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -240173);
                throw;
            }
        }

        /// <summary>
        /// Get email distribution list for GE LD
        /// </summary>
        /// <param name="CustomerNumner">Customer number</param>
        /// <param name="callerLogin">login of the user calling this function</param>

        public string GetGELDEmailDistributionList()
        {
            string EmailDistributionList = string.Empty;
            try
            {

                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<GELD_GetEmailDistributionListResult> results = db.GELD_GetEmailDistributionList();


                    foreach (GELD_GetEmailDistributionListResult r in results)
                    {
                        EmailDistributionList = r.EmailDistributionList;
                        break;
                    }
                    if (EmailDistributionList.Trim() == string.Empty)
                    {
                        throw new Exception(String.Format("Failed to process data ErrorCode={0}", EmailDistributionList));
                    }
                }
                return EmailDistributionList;

            }
            catch (Exception ex)
            {

                _logger.Exception(ex, -240173);
                return EmailDistributionList;
                throw;
            }
        }

        /// <summary>
        /// CRQ CRQ700001314707
        /// Import International video Snapshot data
        /// </summary>
        /// <param name="callerLogin">login of the user calling this function</param>
        /// <returns>true on success or false on failure</returns>
        public bool ImportIntenationalSnapshotData(string Notes)
        {
            bool result = false;
            int Status = 1;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    Status = db.usp_ImportInternationalSnapshotData(Notes);
                    // Status = 0;
                }

                if (Status == 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {

                _logger.Exception(ex, -240173);
                return result;
                throw;
            }
        }

        /// <summary>
        /// CRQ CRQ700001314707
        /// Import US video Snapshot data
        /// </summary>
        /// <param name="callerLogin">login of the user calling this function</param>
        /// <returns>true on success or false on failure</returns>
        public bool ImportUSSnapshotData(string Notes)
        {
            bool result = false;
            int Status = 1;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    Status = db.ServiceDesk_TakeNewSnapshot(Notes);
                }

                if (Status == 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {

                _logger.Exception(ex, -240173);
                return result;
                throw;
            }
        }

        /// <summary>
        /// CRQ CRQ700001314707
        /// Comapre Accounts
        /// </summary>
        /// <param name="callerLogin">login of the user calling this function</param>
        /// <returns>true on success or false on failure</returns>
        public bool CompareAccounts(string UserName)
        {
            bool result = false;
            int Status =0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.usp_CompareAccounts( UserName);
                   
                }

                if (Status == 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {

                _logger.Exception(ex, -240173);
                return result;
                throw;
            }
        }

        /// <summary>
        /// START: ERO 1037
        /// Process Loyalty Credit Data
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <param name="callerLogin"></param>
        /// <returns></returns>
        public bool ProcessLoyaltyCreditData(int invoiceID, string callerLogin)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {

                    //int status = (int)db.GE_LoyaltyCredit_Create(invoiceID, callerLogin);
                    //if (status != 0)
                    //{
                    //    throw new Exception(String.Format("Failed to process data ErrorCode={0}", status));
                    //}

                }
                return true;
            }
            catch (Exception e)
            {
                ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
                appLogEntity.LogType = ApplicationLogType.SystemRaised;
                appLogEntity.InvoiceID = invoiceID;
                appLogEntity.ExceptionDateTime = DateTime.Now;
                appLogEntity.Exception = e;
                appLogEntity.CodeLocation = "ProcessLoyaltyCreditData()";
                appLogEntity.Comment = "Failed to ProcessLoyaltyCreditData for Invoice# " + invoiceID;

                _logger.LogExceptionToDatabase(appLogEntity);
                return false;
            }
        }

        /// <summary>
        /// Updates the recordtype status after proessing
        /// </summary>
        /// <param name="invoiceid"></param>
        /// <param name="recordType1status"></param>
        /// <param name="recordType2status"></param>
        /// <param name="recordType3status"></param>
        /// <param name="recordType4status"></param>
        /// <param name="recordType5status"></param>
        /// <param name="callerLogin"></param>
        /// <returns></returns>
        public int UpdateInvoiceRecordTypes(int invoiceid,string recordType1status, string recordType2status, string recordType3status, string recordType4status, string recordType5status, string callerLogin)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.Update_InvoiceRecordTypes(invoiceid, recordType1status, recordType2status, recordType3status, recordType4status, recordType5status, callerLogin);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Update  InvoiceRecordTypes [{0}]", result));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return result;
        } 

    }
}
