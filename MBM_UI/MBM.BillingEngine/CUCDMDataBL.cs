using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.DataAccess;
using MBM.Entities;
using MBM.Library;
using System.Data;

namespace MBM.BillingEngine
{
    public class CUCDMDataBL
    {
        private DataFactory _dal;
        private Logger _logger;
        private string ConnectionString { get; set; }

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
        public CUCDMDataBL(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

        /// <summary>
        /// Gets Unextracted CUCDMData
        /// </summary>
        /// <returns></returns>
        public List<CUCDMData> GetUnextractedCUCDMData()
        {
            try
            {
                return _dal.CUCDMData.GetUnextractedCUCDMData();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        /// <summary>
        /// Gets Enhanced Data
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<EnhancedData> GetEnhancedData(int invoiceId)
        {
            List<EnhancedData> lstEnhancedData = new List<EnhancedData>();

            try
            {
                lstEnhancedData = _dal.CUCDMData.GetEnhancedData(invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstEnhancedData;
        }

        /// <summary>
        /// Gets UnEnhanced Data
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<UnEnhancedData> GetUnEnhancedData(int invoiceId)
        {
            List<UnEnhancedData> lstUnEnhancedData = new List<UnEnhancedData>();

            try
            {
                lstUnEnhancedData = _dal.CUCDMData.GetUnEnhancedData(invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstUnEnhancedData;
        }

        /// <summary>
        /// Gets EHCS Raw Data
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<EHCSRawData> GetEHCSRawData(string customer, string startDate, string endDate)
        {
            List<EHCSRawData> lstEHCSRawData = new List<EHCSRawData>();

            try
            {
                lstEHCSRawData = _dal.CUCDMData.GetEHCSRawData(customer, startDate, endDate);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstEHCSRawData;
        }

        /// <summary>
        /// Gets SOO Accural Revenue Report Data, cbe_9941
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SOOReportData> GetSOOReportData(string customer, string startDate, string endDate)
        {
            List<SOOReportData> SOOReportData = new List<SOOReportData>();

            try
            {
                SOOReportData = _dal.CUCDMData.GetSOOReportData(customer, startDate, endDate);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return SOOReportData;
        }

        /// <summary>
        /// Gets SOO Account Report Data, cbe_11609
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetSOOAccountData(int invoiceId)
        {
            DataSet ds = new DataSet();
            List<RecordType1> recordTypes1 = new List<RecordType1>();
            List<RecordType2> recordTypes2 = new List<RecordType2>();
            List<RT2Summary> rT2Summary = new List<RT2Summary>();
            List<RecordType4> recordTypes4 = new List<RecordType4>();
            List<RecordType3> recordTypes3 = new List<RecordType3>();

            try
            {
                #region Sheet1
                _dal.RecordTypes.GetOneTypeForSingleInvoice(invoiceId, out recordTypes1);
                string tableName1 = "Call Usage Summary";
                DataTable dt1 = new DataTable(tableName1);
                dt1.Columns.Add("Legal Entity", typeof(string));
                dt1.Columns.Add("na_billing_number", typeof(string));
                dt1.Columns.Add("count_of_charges", typeof(string));
                dt1.Columns.Add("Duration", typeof(decimal));
                dt1.Columns.Add("charge_amount", typeof(decimal));
                dt1.Columns[dt1.Columns.Count - 1].Caption = "Currency";
                foreach (var items in recordTypes1)
                {
                    var row = dt1.NewRow();
                    row["Legal Entity"] = items.LegalEntity;
                    row["na_billing_number"] = items.BillingNumberNorthAmericanStandard;
                    row["count_of_charges"] = items.CountOfCharges;
                    row["Duration"] = items.Duration;
                    row["charge_amount"] = items.ChargeAmount;

                    dt1.Rows.Add(row);
                }
                ds.Tables.Add(dt1);
                #endregion

                #region Sheet2
                _dal.RecordTypes.GetOneFileFormatDetail(invoiceId, out recordTypes2);
                string tableName2 = "User Detail";
                DataTable dt2 = new DataTable(tableName2);
                dt2.Columns.Add("record_type", typeof(string));
                dt2.Columns.Add("billing_account_number", typeof(string));
                dt2.Columns.Add("invoice_number", typeof(string));
                dt2.Columns.Add("did", typeof(string));
                dt2.Columns.Add("subidentifier", typeof(string));
                dt2.Columns.Add("user_id", typeof(string));
                dt2.Columns.Add("bill_date", typeof(string));
                dt2.Columns.Add("charge_description", typeof(string));
                dt2.Columns.Add("charge_type", typeof(string));
                dt2.Columns.Add("service_start_date", typeof(string));
                dt2.Columns.Add("service_end_date", typeof(string));
                dt2.Columns.Add("invoice_bill_period_start", typeof(string));
                dt2.Columns.Add("invoice_bill_period_end", typeof(string));
                dt2.Columns.Add("total", typeof(string));
                dt2.Columns.Add("vendor_name", typeof(string));
                dt2.Columns.Add("state", typeof(string));
                dt2.Columns.Add("legal_entity", typeof(string));
                dt2.Columns.Add("assetsearchcode1", typeof(string));
                dt2.Columns.Add("department", typeof(string));
                dt2.Columns.Add("cost_center", typeof(string));
                dt2.Columns.Add("ali_code", typeof(string));
                dt2.Columns.Add("supervisor", typeof(string));
                foreach (var items in recordTypes2)
                {
                    var row = dt2.NewRow();
                    row["record_type"] = items.RecordType;
                    row["billing_account_number"] = items.BAN;
                    row["invoice_number"] = items.InvoiceDisplay;
                    row["did"] = items.FromNumber;
                    row["subidentifier"] = items.SSO;
                    row["user_id"] = items.UserId;
                    row["bill_date"] = items.ChargeDate;
                    row["charge_description"] = items.ChargeDescription;
                    row["charge_type"] = items.ChargeType;
                    row["service_start_date"] = items.ServiceStartDate;
                    row["service_end_date"] = items.ServiceEndDate;
                    row["invoice_bill_period_start"] = items.InvoiceBillPeriodStart;
                    row["invoice_bill_period_end"] = items.InvoiceBillPeriodEnd;
                    row["total"] = items.Total;
                    row["vendor_name"] = items.VendorName;
                    row["state"] = items.Locality;
                    row["legal_entity"] = items.LegalEntity;
                    row["assetsearchcode1"] = items.AssetSearchCode1;
                    row["department"] = items.Department;
                    row["cost_center"] = items.CostCenter;
                    row["ali_code"] = items.ALI_Code;
                    row["supervisor"] = items.Supervisor;
                    dt2.Rows.Add(row);
                }
                ds.Tables.Add(dt2);
                #endregion

                #region Sheet3
                //Sheet 4
                _dal.RecordTypes.GetRT2Summary(invoiceId, out rT2Summary);
                string tableName3 = "User Summary";
                DataTable dt3 = new DataTable(tableName3);
                dt3.Columns.Add("Profiles", typeof(string));
                dt3.Columns.Add("Count of Charges", typeof(string));
                dt3.Columns.Add("Charges", typeof(string));
                dt3.Columns[dt3.Columns.Count - 1].Caption = "Currency";
                foreach (var items in rT2Summary)
                {
                    var row = dt3.NewRow();
                    row["Profiles"] = items.Profiles;
                    row["Count of Charges"] = items.NoOfTimes;
                    row["Charges"] = items.Charges;

                    dt3.Rows.Add(row);
                }
                ds.Tables.Add(dt3);
                #endregion

                #region Sheet4
                //Sheet5
                _dal.RecordTypes.GetRecordType4(invoiceId, out recordTypes4);

                string tableName4 = "Detailed File";
                DataTable dt4 = new DataTable(tableName4);
                dt4.Columns.Add("record_type", typeof(string));
                dt4.Columns.Add("billing_account_number", typeof(string));
                dt4.Columns.Add("invoice_number", typeof(string));
                dt4.Columns.Add("did", typeof(string));
                dt4.Columns.Add("employee_id", typeof(string));
                dt4.Columns.Add("charge_description", typeof(string));
                dt4.Columns.Add("tax_percentage", typeof(string));
                dt4.Columns.Add("service_type", typeof(string));
                dt4.Columns.Add("tax_charge", typeof(string));
                dt4.Columns.Add("vendor_name", typeof(string));
                dt4.Columns.Add("bill_date", typeof(string));
                dt4.Columns.Add("state", typeof(string));
                dt4.Columns.Add("legal_entity", typeof(string));

                foreach (var items in recordTypes4)
                {
                    var row = dt4.NewRow();
                    row["record_type"] = items.RecordType;
                    row["billing_account_number"] = items.BAN;
                    row["invoice_number"] = items.InvoiceDisplay;
                    row["did"] = items.FromNumber;
                    row["employee_id"] = items.SSO;
                    row["charge_description"] = items.ChargeDescription;
                    row["tax_percentage"] = items.TaxCharge;
                    row["service_type"] = items.ServiceType;
                    row["tax_charge"] = items.TaxCharge;
                    row["vendor_name"] = items.VendorName;
                    row["bill_date"] = items.BillDate;
                    row["state"] = items.Locality;
                    row["legal_entity"] = items.LegalEntity;

                    dt4.Rows.Add(row);
                }
                ds.Tables.Add(dt4);
                #endregion

                #region Sheet5
                //Sheet6
                _dal.RecordTypes.GetRecordType3(invoiceId, out recordTypes3);
                string tabName = "Billing Account Summary";
                DataTable dt5 = new DataTable(tabName);
                dt5.Columns.Add("record_type", typeof(string));
                dt5.Columns.Add("billing_account_number", typeof(string));
                dt5.Columns.Add("invoice_number", typeof(string));
                dt5.Columns.Add("bill_date", typeof(string));
                dt5.Columns.Add("record_type_1_total", typeof(string));
                dt5.Columns.Add("record_type_2_total", typeof(string));
                dt5.Columns.Add("record_type_4_total", typeof(string));
                dt5.Columns.Add("grand_total", typeof(string));
                dt5.Columns.Add("legal_entity", typeof(string));

                foreach (var items in recordTypes3)
                {
                    var row = dt5.NewRow();
                    row["record_type"] = items.RecordType;
                    row["billing_account_number"] = items.BAN;
                    row["invoice_number"] = items.InvoiceDisplay;
                    row["bill_date"] = items.BillDate;
                    row["record_type_1_total"] = items.RecordType1Total;
                    row["record_type_2_total"] = items.RecordType2Total;
                    row["record_type_4_total"] = items.RecordType4Total;
                    row["grand_total"] = items.Total;
                    row["legal_entity"] = items.LegalEntity;

                    dt5.Rows.Add(row);
                }
                ds.Tables.Add(dt5);
                #endregion

                return ds;

            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

        }

        /// <summary>
        /// Gets SOO Account Report Data for All, cbe_11609
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetSOOAccountData_All(int invoiceId)
        {
            DataSet ds = new DataSet();
            List<RecordType1> recordTypes1 = new List<RecordType1>();
            List<RecordType2> recordTypes2 = new List<RecordType2>();
            List<RT2Summary> rT2Summary = new List<RT2Summary>();
            List<RecordType4> recordTypes4 = new List<RecordType4>();
            List<RecordType3> recordTypes3 = new List<RecordType3>();

            try
            {
                #region Sheet1
                _dal.RecordTypes.GetOneTypeForSingleInvoice_All(invoiceId, out recordTypes1);
                string tableName1 = "Call Usage Summary";
                DataTable dt1 = new DataTable(tableName1);
                dt1.Columns.Add("Legal Entity", typeof(string));
                dt1.Columns.Add("na_billing_number", typeof(string));
                dt1.Columns.Add("count_of_charges", typeof(string));
                dt1.Columns.Add("Duration", typeof(decimal));
                dt1.Columns.Add("charge_amount", typeof(decimal));
                dt1.Columns[dt1.Columns.Count - 1].Caption = "Currency";
                foreach (var items in recordTypes1)
                {
                    var row = dt1.NewRow();
                    row["Legal Entity"] = items.LegalEntity;
                    row["na_billing_number"] = items.BillingNumberNorthAmericanStandard;
                    row["count_of_charges"] = items.CountOfCharges;
                    row["Duration"] = items.Duration;
                    row["charge_amount"] = items.ChargeAmount;

                    dt1.Rows.Add(row);
                }
                ds.Tables.Add(dt1);
                #endregion

                #region Sheet2
                _dal.RecordTypes.GetOneFileFormatDetail_All(invoiceId, out recordTypes2);
                string tableName2 = "User Detail";
                DataTable dt2 = new DataTable(tableName2);
                dt2.Columns.Add("record_type", typeof(string));
                dt2.Columns.Add("billing_account_number", typeof(string));
                dt2.Columns.Add("invoice_number", typeof(string));
                dt2.Columns.Add("did", typeof(string));
                dt2.Columns.Add("subidentifier", typeof(string));
                dt2.Columns.Add("user_id", typeof(string));
                dt2.Columns.Add("bill_date", typeof(string));
                dt2.Columns.Add("charge_description", typeof(string));
                dt2.Columns.Add("charge_type", typeof(string));
                dt2.Columns.Add("service_start_date", typeof(string));
                dt2.Columns.Add("service_end_date", typeof(string));
                dt2.Columns.Add("invoice_bill_period_start", typeof(string));
                dt2.Columns.Add("invoice_bill_period_end", typeof(string));
                dt2.Columns.Add("total", typeof(string));
                dt2.Columns.Add("vendor_name", typeof(string));
                dt2.Columns.Add("state", typeof(string));
                dt2.Columns.Add("legal_entity", typeof(string));
                dt2.Columns.Add("assetsearchcode1", typeof(string));
                dt2.Columns.Add("department", typeof(string));
                dt2.Columns.Add("cost_center", typeof(string));
                dt2.Columns.Add("ali_code", typeof(string));
                dt2.Columns.Add("supervisor", typeof(string));
                foreach (var items in recordTypes2)
                {
                    var row = dt2.NewRow();
                    row["record_type"] = items.RecordType;
                    row["billing_account_number"] = items.BAN;
                    row["invoice_number"] = items.InvoiceDisplay;
                    row["did"] = items.FromNumber;
                    row["subidentifier"] = items.SSO;
                    row["user_id"] = items.UserId;
                    row["bill_date"] = items.ChargeDate;
                    row["charge_description"] = items.ChargeDescription;
                    row["charge_type"] = items.ChargeType;
                    row["service_start_date"] = items.ServiceStartDate;
                    row["service_end_date"] = items.ServiceEndDate;
                    row["invoice_bill_period_start"] = items.InvoiceBillPeriodStart;
                    row["invoice_bill_period_end"] = items.InvoiceBillPeriodEnd;
                    row["total"] = items.Total;
                    row["vendor_name"] = items.VendorName;
                    row["state"] = items.Locality;
                    row["legal_entity"] = items.LegalEntity;
                    row["assetsearchcode1"] = items.AssetSearchCode1;
                    row["department"] = items.Department;
                    row["cost_center"] = items.CostCenter;
                    row["ali_code"] = items.ALI_Code;
                    row["supervisor"] = items.Supervisor;
                    dt2.Rows.Add(row);
                }
                ds.Tables.Add(dt2);
                #endregion

                #region Sheet3
                //Sheet 4
                _dal.RecordTypes.GetRT2Summary_All(invoiceId, out rT2Summary);
                string tableName3 = "User Summary";
                DataTable dt3 = new DataTable(tableName3);
                dt3.Columns.Add("Profiles", typeof(string));
                dt3.Columns.Add("Count of Charges", typeof(string));
                dt3.Columns.Add("Charges", typeof(string));
                dt3.Columns[dt3.Columns.Count - 1].Caption = "Currency";
                foreach (var items in rT2Summary)
                {
                    var row = dt3.NewRow();
                    row["Profiles"] = items.Profiles;
                    row["Count of Charges"] = items.NoOfTimes;
                    row["Charges"] = items.Charges;

                    dt3.Rows.Add(row);
                }
                ds.Tables.Add(dt3);
                #endregion

                #region Sheet4
                //Sheet5
                _dal.RecordTypes.GetRecordType4_All(invoiceId, out recordTypes4);

                string tableName4 = "Detailed File";
                DataTable dt4 = new DataTable(tableName4);
                dt4.Columns.Add("record_type", typeof(string));
                dt4.Columns.Add("billing_account_number", typeof(string));
                dt4.Columns.Add("invoice_number", typeof(string));
                dt4.Columns.Add("did", typeof(string));
                dt4.Columns.Add("employee_id", typeof(string));
                dt4.Columns.Add("charge_description", typeof(string));
                dt4.Columns.Add("tax_percentage", typeof(string));
                dt4.Columns.Add("service_type", typeof(string));
                dt4.Columns.Add("tax_charge", typeof(string));
                dt4.Columns.Add("vendor_name", typeof(string));
                dt4.Columns.Add("bill_date", typeof(string));
                dt4.Columns.Add("state", typeof(string));
                dt4.Columns.Add("legal_entity", typeof(string));

                foreach (var items in recordTypes4)
                {
                    var row = dt4.NewRow();
                    row["record_type"] = items.RecordType;
                    row["billing_account_number"] = items.BAN;
                    row["invoice_number"] = items.InvoiceDisplay;
                    row["did"] = items.FromNumber;
                    row["employee_id"] = items.SSO;
                    row["charge_description"] = items.ChargeDescription;
                    row["tax_percentage"] = items.TaxCharge;
                    row["service_type"] = items.ServiceType;
                    row["tax_charge"] = items.TaxCharge;
                    row["vendor_name"] = items.VendorName;
                    row["bill_date"] = items.BillDate;
                    row["state"] = items.Locality;
                    row["legal_entity"] = items.LegalEntity;

                    dt4.Rows.Add(row);
                }
                ds.Tables.Add(dt4);
                #endregion

                #region Sheet5
                //Sheet6
                _dal.RecordTypes.GetRecordType3_All(invoiceId, out recordTypes3);
                string tabName = "Billing Account Summary";
                DataTable dt5 = new DataTable(tabName);
                dt5.Columns.Add("record_type", typeof(string));
                dt5.Columns.Add("billing_account_number", typeof(string));
                dt5.Columns.Add("invoice_number", typeof(string));
                dt5.Columns.Add("bill_date", typeof(string));
                dt5.Columns.Add("record_type_1_total", typeof(string));
                dt5.Columns.Add("record_type_2_total", typeof(string));
                dt5.Columns.Add("record_type_4_total", typeof(string));
                dt5.Columns.Add("grand_total", typeof(string));
                dt5.Columns.Add("legal_entity", typeof(string));

                foreach (var items in recordTypes3)
                {
                    var row = dt5.NewRow();
                    row["record_type"] = items.RecordType;
                    row["billing_account_number"] = items.BAN;
                    row["invoice_number"] = items.InvoiceDisplay;
                    row["bill_date"] = items.BillDate;
                    row["record_type_1_total"] = items.RecordType1Total;
                    row["record_type_2_total"] = items.RecordType2Total;
                    row["record_type_4_total"] = items.RecordType4Total;
                    row["grand_total"] = items.Total;
                    row["legal_entity"] = items.LegalEntity;

                    dt5.Rows.Add(row);
                }
                ds.Tables.Add(dt5);
                #endregion

                return ds;

            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

        }
        /// <summary>
        /// Gets EDI Report Data
        /// </summary>
        public List<EDIReportData> GetEDIReportData()
        {
            List<EDIReportData> EDIReportData = new List<EDIReportData>();

            try
            {
                EDIReportData = _dal.CUCDMData.GetEDIReportData();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return EDIReportData;
        }
    }
}
