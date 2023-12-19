using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MBM.DataAccess;
using MBM.Entities;
using MBM.Library;
using System.Net.Mail;
using System.Configuration;
using System.Globalization;

namespace MBM.BillingEngine
{
    public class FileExport
    {
        private DataAccess.DataFactory _dal;
        int _typeofMode;
        private Logger _logger;

        public FileExport(string connectionString)
        {
            _dal = new DataFactory(connectionString);
        }

        #region CreateBillingFile

        /// <summary>
        /// Build the billing file format
        /// </summary>
        /// <param name="invoiceID">invoice identifier</param>
        /// <param name="delimiter">delimiter character</param>
        /// <param name="columnCount">number of columns per line</param>
        /// <param name="exportCurrency">conversion currency</param>
        /// <param name="fileName">full path and file to save</param>
        public void CreateBillingFile(int invoiceID, string delimiter, int columnCount, CurrencyConversion exportCurrency, string serverPath, InvoiceType objInvoiceType, string fileName, string userName, InvoiceBL objInvoiceBL)
        {
            string dateTime = DateTime.Now.ToString("MMddyyyyHHmmss");

            //cbe_8967
            if (objInvoiceType.OutputFileFormat.ToString() == "1")
            {
                OneSummaryFile(invoiceID, exportCurrency, fileName, dateTime, serverPath, objInvoiceType, userName, objInvoiceBL);
            }
            else
            {
                RT1SummaryFile(invoiceID, exportCurrency, fileName, dateTime, serverPath, objInvoiceType, userName, objInvoiceBL);

                RT2SummaryFile(invoiceID, exportCurrency, fileName, dateTime, serverPath, objInvoiceType, userName, objInvoiceBL);

                RecordType3DetailFile(invoiceID, exportCurrency, fileName, dateTime, serverPath, objInvoiceType, userName, objInvoiceBL);

                RT4SummaryFile(invoiceID, exportCurrency, fileName, dateTime, serverPath, objInvoiceType, userName, objInvoiceBL);

                RecordType5DetailFile(invoiceID, exportCurrency, fileName, dateTime, serverPath, objInvoiceType, userName, objInvoiceBL);
            }
        }

        #endregion

        #region InsertInvoiceFileExportsByRecordType
        /// <summary>
        /// Inserts the Exported File details based on RecordType or RecordTypeandLegalEntity
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <param name="fileName"></param>
        /// <param name="DefaultFTP"></param>
        /// <param name="userName"></param>
        /// <param name="objInvoiceBL"></param>
        private void InsertInvoiceFileExportsByRecordType(int invoiceID, string fileName, string DefaultFTP, string userName, InvoiceBL objInvoiceBL)
        {
            ExportedFile exportedFile = new ExportedFile();
            exportedFile.InvoiceID = invoiceID;
            exportedFile.ExportedFileName = fileName;
            exportedFile.ExportedFilePath = DefaultFTP;
            exportedFile.ExportedFileBy = userName;
            objInvoiceBL.InsertInvoiceFileExports(exportedFile);
        }
        #endregion

        #region CreateBillingFileByLegalEntity
        /// <summary>
        /// Build the billing file format
        /// </summary>
        /// <param name="invoiceID">invoice identifier</param>
        /// <param name="delimiter">delimiter character</param>
        /// <param name="columnCount">number of columns per line</param>
        /// <param name="exportCurrency">conversion currency</param>
        /// <param name="fileName">full path and file to save</param>
        public void CreateBillingFileByLegalEntity(int invoiceID, string delimiter, int columnCount, CurrencyConversion exportCurrency, string serverPath, InvoiceType objInvoiceType, string fileName, List<string> legalEntites, string userName, InvoiceBL objInvoiceBL)
        {
            // Get DataSet for RecordType1
            List<RecordType1> recordTypes1 = new List<RecordType1>();
            _dal.RecordTypes.GetRecordType1(invoiceID, out recordTypes1);
            CurrencyConversion.ConvertRecordTypes(recordTypes1, exportCurrency);

            // Get DataSet for RecordType2
            List<RecordType2> recordTypes2 = new List<RecordType2>();
            _dal.RecordTypes.GetRecordType2(invoiceID, out recordTypes2);
            CurrencyConversion.ConvertRecordTypes(recordTypes2, exportCurrency);

            // Get DataSet for RecordType3
            List<RecordType3> recordTypes3 = new List<RecordType3>();
            _dal.RecordTypes.GetRecordType3(invoiceID, out recordTypes3);
            CurrencyConversion.ConvertRecordTypes(recordTypes3, exportCurrency);

            // Get DataSet for RecordType4
            List<RecordType4> recordTypes4 = new List<RecordType4>();
            _dal.RecordTypes.GetRecordType4(invoiceID, out recordTypes4);
            CurrencyConversion.ConvertRecordTypes(recordTypes4, exportCurrency);

            // Get DataSet for RecordType5
            List<RecordType5> recordTypes5 = new List<RecordType5>();
            _dal.RecordTypes.GetRecordType5(invoiceID, out recordTypes5);
            CurrencyConversion.ConvertRecordTypes(recordTypes5, exportCurrency);

            string dateTime = DateTime.Now.ToString("MMddyyyyHHmmss");
            if (legalEntites.Count > 0)
            {
                foreach (var legalEntity in legalEntites)
                {
                    #region RT1 Export File
                    //RecordType1 Export File
                    // Get DataSet for RecordType1
                    DataSet ds1 = new DataSet();
                    if (recordTypes1 != null && recordTypes1.Count > 0)
                    {

                        List<RecordType1> recordType1BylegalEntity = recordTypes1.Where(x => (x.LegalEntity == legalEntity && x.InvoiceID == invoiceID)).ToList();

                        string tableName = "Detailed File";
                        DataTable dt = new DataTable(tableName);
                        dt.Columns.Add("record_type", typeof(Int32));
                        dt.Columns.Add("billing_account_number", typeof(string));
                        dt.Columns.Add("invoice_number", typeof(string));
                        dt.Columns.Add("bill_date", typeof(Int32));
                        dt.Columns.Add("originating_number", typeof(string));
                        dt.Columns.Add("terminating_number", typeof(string));
                        dt.Columns.Add("charge_amount", typeof(decimal));
                        dt.Columns.Add("record_date", typeof(Int32));
                        dt.Columns.Add("record_time", typeof(string));
                        dt.Columns.Add("billable_minutes", typeof(decimal));
                        dt.Columns.Add("na_billing_number", typeof(string));
                        dt.Columns.Add("originating_city", typeof(string));
                        dt.Columns.Add("originating_state", typeof(string));
                        dt.Columns.Add("terminating_city", typeof(string));
                        dt.Columns.Add("terminating_state", typeof(string));
                        dt.Columns.Add("settlement_code", typeof(int));
                        dt.Columns.Add("charge_description", typeof(string));
                        dt.Columns.Add("provider", typeof(string));
                        dt.Columns.Add("vendor_name", typeof(string));
                        dt.Columns.Add("state", typeof(string));
                        dt.Columns.Add("legal_entity", typeof(Int32));

                        foreach (var items in recordType1BylegalEntity)
                        {
                            var row = dt.NewRow();
                            row["record_type"] = items.RecordType;
                            row["billing_account_number"] = items.BAN;
                            row["invoice_number"] = items.InvoiceDisplay;
                            row["bill_date"] = items.BillDate;
                            row["originating_number"] = items.FromNumber;
                            row["terminating_number"] = items.ToNumber;
                            row["charge_amount"] = items.Revenue;
                            row["record_date"] = items.DateOfRecord;
                            row["record_time"] = items.ConnectTime;
                            row["billable_minutes"] = items.Duration;
                            row["na_billing_number"] = items.BillingNumberNorthAmericanStandard;
                            row["originating_city"] = items.FromCity;
                            row["originating_state"] = items.FromState;
                            row["terminating_city"] = items.ToCity;
                            row["terminating_state"] = items.ToState;
                            row["settlement_code"] = items.SettlementCode;
                            row["charge_description"] = items.ChargeDescription;
                            row["provider"] = items.Provider;
                            row["vendor_name"] = items.VendorName;
                            row["state"] = items.Locality;
                            row["legal_entity"] = items.LegalEntity;

                            dt.Rows.Add(row);
                        }


                        ds1.Tables.Add(dt);

                    }

                    string RT1FileName = fileName + "_" + legalEntity + "_RT1_" + dateTime + ".xlsx";
                    string RT1ServerPath = serverPath + RT1FileName;
                    CreateExcelFile.CreateExcelDocument(ds1, RT1ServerPath);

                    if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                    {
                        try
                        {
                            // Code for SFTP
                            ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT1ServerPath, RT1FileName);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                            {
                                LogType = ApplicationLogType.SystemRaised,
                                ExceptionDateTime = DateTime.Now,
                                CodeLocation = "SaveFilesToSFTPRT1()",
                                Comment = "System Exception",
                                Exception = ex,
                            });
                        }
                    }
                    else
                    {
                        // Code for FTP
                        ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT1ServerPath, RT1FileName);
                    }
                    InsertInvoiceFileExportsByRecordType(invoiceID, RT1FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL);


                    #endregion

                    #region RT2 Export File
                    // RecordType2 Export File
                    // Get DataSet for RecordType2
                    DataSet ds2 = new DataSet();
                    if (recordTypes2 != null && recordTypes2.Count > 0)
                    {
                        List<RecordType2> recordType2BylegalEntity = recordTypes2.Where(x => (x.LegalEntity == legalEntity && x.InvoiceID == invoiceID)).ToList();

                        string tableName = "Detailed File";
                        DataTable dt2 = new DataTable(tableName);
                        dt2.Columns.Add("record_type", typeof(Int32));
                        dt2.Columns.Add("billing_account_number", typeof(Int32));
                        dt2.Columns.Add("invoice_number", typeof(string));
                        dt2.Columns.Add("did", typeof(string));
                        dt2.Columns.Add("subidentifier", typeof(string));
                        dt2.Columns.Add("user_id", typeof(string));
                        dt2.Columns.Add("bill_date", typeof(Int32));
                        dt2.Columns.Add("charge_description", typeof(string));
                        dt2.Columns.Add("charge_type", typeof(string));
                        dt2.Columns.Add("service_start_date", typeof(string));
                        dt2.Columns.Add("service_end_date", typeof(string));
                        dt2.Columns.Add("invoice_bill_period_start", typeof(Int32));
                        dt2.Columns.Add("invoice_bill_period_end", typeof(Int32));
                        dt2.Columns.Add("total", typeof(decimal));
                        dt2.Columns.Add("vendor_name", typeof(string));
                        dt2.Columns.Add("state", typeof(string));
                        dt2.Columns.Add("legal_entity", typeof(Int32));

                        foreach (var items in recordType2BylegalEntity)
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

                            dt2.Rows.Add(row);
                        }
                        ds2.Tables.Add(dt2);
                    }

                    string RT2FileName = fileName + "_" + legalEntity + "_RT2_" + dateTime + ".xlsx";
                    string RT2ServerPath = serverPath + RT2FileName;
                    CreateExcelFile.CreateExcelDocument(ds2, RT2ServerPath);

                    if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                    {
                        // Code for SFTP
                        try
                        {
                            ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT2ServerPath, RT2FileName);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                            {
                                LogType = ApplicationLogType.SystemRaised,
                                ExceptionDateTime = DateTime.Now,
                                CodeLocation = "SaveFilesToSFTPRT2()",
                                Comment = "System Exception",
                                Exception = ex,
                            });
                        }
                    }
                    else
                    {
                        // Code for FTP
                        ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT2ServerPath, RT2FileName);
                    }
                    InsertInvoiceFileExportsByRecordType(invoiceID, RT2FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL); 
                   
                    #endregion

                    #region RT3 Export File
                    // RecordType3 Export File
                    // Get DataSet for RecordType3
                    DataSet ds3 = new DataSet();
                    if (recordTypes3 != null && recordTypes3.Count > 0)
                    {
                        List<RecordType3> recordType3BylegalEntity = recordTypes3.Where(x => (x.LegalEntity == legalEntity && x.InvoiceID == invoiceID)).ToList();

                        string tabName = "Detailed File";
                        DataTable dt3 = new DataTable(tabName);
                        dt3.Columns.Add("record_type", typeof(Int32));
                        dt3.Columns.Add("billing_account_number", typeof(string));
                        dt3.Columns.Add("invoice_number", typeof(string));
                        dt3.Columns.Add("bill_date", typeof(Int32));
                        dt3.Columns.Add("record_type_1_total", typeof(decimal));
                        dt3.Columns.Add("record_type_2_total", typeof(decimal));
                        dt3.Columns.Add("record_type_4_total", typeof(decimal));
                        dt3.Columns.Add("grand_total", typeof(decimal));
                        dt3.Columns.Add("legal_entity", typeof(Int32));

                        foreach (var items in recordType3BylegalEntity)
                        {
                            var row = dt3.NewRow();
                            row["record_type"] = items.RecordType;
                            row["billing_account_number"] = items.BAN;
                            row["invoice_number"] = items.InvoiceDisplay;
                            row["bill_date"] = items.BillDate;
                            row["record_type_1_total"] = items.RecordType1Total;
                            row["record_type_2_total"] = items.RecordType2Total;
                            row["record_type_4_total"] = items.RecordType4Total;
                            row["grand_total"] = items.Total;
                            row["legal_entity"] = items.LegalEntity;

                            dt3.Rows.Add(row);
                        }
                        ds3.Tables.Add(dt3);
                    }

                    string RT3FileName = fileName + "_" + legalEntity + "_RT3_" + dateTime + ".xlsx";
                    string RT3ServerPath = serverPath + RT3FileName;
                    CreateExcelFile.CreateExcelDocument(ds3, RT3ServerPath);

                    if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                    {
                        // Code for SFTP
                        try
                        {
                            ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT3ServerPath, RT3FileName);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                            {
                                LogType = ApplicationLogType.SystemRaised,
                                ExceptionDateTime = DateTime.Now,
                                CodeLocation = "SaveFilesToSFTPRT3()",
                                Comment = "System Exception",
                                Exception = ex,
                            });
                        }
                    }
                    else
                    {
                        // Code for FTP
                        ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT3ServerPath, RT3FileName);
                    }
                    InsertInvoiceFileExportsByRecordType(invoiceID, RT3FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL);
                    #endregion

                    #region RT4 Export File
                    // RecordType4 Export File
                    // Get DataSet for RecordType4
                    DataSet ds4 = new DataSet();
                    if (recordTypes4 != null && recordTypes4.Count > 0)
                    {
                        List<RecordType4> recordType4BylegalEntity = recordTypes4.Where(x => (x.LegalEntity == legalEntity && x.InvoiceID == invoiceID)).ToList();

                        string tableName = "Detailed File";
                        DataTable dt4 = new DataTable(tableName);
                        dt4.Columns.Add("record_type", typeof(Int32));
                        dt4.Columns.Add("billing_account_number", typeof(string));
                        dt4.Columns.Add("invoice_number", typeof(string));
                        dt4.Columns.Add("did", typeof(string));
                        dt4.Columns.Add("employee_id", typeof(string));
                        dt4.Columns.Add("charge_description", typeof(string));
                        dt4.Columns.Add("tax_percentage", typeof(string));
                        dt4.Columns.Add("service_type", typeof(string));
                        dt4.Columns.Add("tax_charge", typeof(decimal));
                        dt4.Columns.Add("vendor_name", typeof(string));
                        dt4.Columns.Add("bill_date", typeof(string));
                        dt4.Columns.Add("state", typeof(string));
                        dt4.Columns.Add("legal_entity", typeof(Int32));

                        foreach (var items in recordType4BylegalEntity)
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
                        ds4.Tables.Add(dt4);
                    }

                    string RT4FileName = fileName + "_" + legalEntity + "_RT4_" + dateTime + ".xlsx";
                    string RT4ServerPath = serverPath + RT4FileName;
                    CreateExcelFile.CreateExcelDocument(ds4, RT4ServerPath);

                    if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                    {
                        // Code for SFTP
                        try
                        {
                            ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT4ServerPath, RT4FileName);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                            {
                                LogType = ApplicationLogType.SystemRaised,
                                ExceptionDateTime = DateTime.Now,
                                CodeLocation = "SaveFilesToSFTPRT4()",
                                Comment = "System Exception",
                                Exception = ex,
                            });
                        }
                    }
                    else
                    {
                        // Code for FTP
                        ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT4ServerPath, RT4FileName);
                    }
                    InsertInvoiceFileExportsByRecordType(invoiceID, RT4FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL);
                    #endregion

                    #region RT5 Export File
                    // RecordType5 Export File
                    // Get DataSet for RecordType5
                    DataSet ds5 = new DataSet();
                    if (recordTypes5 != null && recordTypes5.Count > 0)
                    {
                        List<RecordType5> recordType5BylegalEntity = recordTypes5.Where(x => (x.LegalEntity == legalEntity && x.InvoiceID == invoiceID)).ToList();

                        string tabName = "Detailed File";
                        DataTable dt5 = new DataTable(tabName);
                        dt5.Columns.Add("record_type", typeof(Int32));
                        dt5.Columns.Add("invoice_number", typeof(string));
                        dt5.Columns.Add("checked_record_type", typeof(string));
                        dt5.Columns.Add("record_count", typeof(Int32));
                        dt5.Columns.Add("summed_field_name", typeof(string));
                        dt5.Columns.Add("grand_total", typeof(decimal));
                        dt5.Columns.Add("legal_entity", typeof(Int32));

                        foreach (var items in recordType5BylegalEntity)
                        {
                            var row = dt5.NewRow();
                            row["record_type"] = items.RecordType;
                            row["invoice_number"] = items.BAN;
                            row["checked_record_type"] = items.CheckRecordType;
                            row["record_count"] = items.TotalRecordCount;
                            row["summed_field_name"] = items.SumFieldName;
                            row["grand_total"] = items.TotalAmount;
                            row["legal_entity"] = items.LegalEntity;

                            dt5.Rows.Add(row);
                        }
                        ds5.Tables.Add(dt5);
                    }

                    string RT5FileName = fileName + "_" + legalEntity + "_RT5_" + dateTime + ".xlsx";
                    string RT5ServerPath = serverPath + RT5FileName;
                    CreateExcelFile.CreateExcelDocument(ds5, RT5ServerPath);
                    if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                    {
                        try
                        {
                            // Code for SFTP
                            ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT5ServerPath, RT5FileName);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                            {
                                LogType = ApplicationLogType.SystemRaised,
                                ExceptionDateTime = DateTime.Now,
                                CodeLocation = "SaveFilesToSFTPRT5()",
                                Comment = "System Exception",
                                Exception = ex,
                            });
                        }
                    }
                    else
                    {
                        // Code for FTP
                        ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT5ServerPath, RT5FileName);
                    }
                    InsertInvoiceFileExportsByRecordType(invoiceID, RT5FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL);
                    #endregion
                }
            }
        }
        #endregion

        #region OneEntryPerRecordType
        /// <summary>
        /// Combine all entries with duplicate RecordTypes
        /// </summary>
        /// <param name="recordTypes5">list of record types</param>
        /// <returns>compressed list</returns>
        private List<RecordType5> OneEntryPerRecordType(List<RecordType5> recordTypes5)
        {
            List<RecordType5> rt5 = new List<RecordType5>();

            foreach (RecordType5 r in recordTypes5)
            {
                if (rt5.Exists(x => x.CheckRecordType == r.CheckRecordType))
                {
                    RecordType5 z = rt5.Find(y => y.CheckRecordType == r.CheckRecordType);
                    z.TotalRecordCount += r.TotalRecordCount;
                    z.TotalAmount += r.TotalAmount;
                }
                else
                {
                    rt5.Add(r);
                }
            }

            return rt5;
        }
        #endregion

        #region SaveLoyaltCreditReportFile
        /// <summary>
        /// Write out the Loyalty Credit File in delimited text file
        /// </summary>
        /// <param name="invoiceID">invoice identifier</param>
        /// <param name="delimiter">field delimiter</param>
        /// <param name="fileName">full location and name of the file</param>
        public void SaveLoyaltCreditReportFile(int invoiceID, string delimiter, string fileName)
        {
            //DataTable loyaltyResults = _dal.GE_LoyaltyCredit.Get_LoyaltyCredit(invoiceID);
            //List<string> lines = Conversions.ConvertToCSV(loyaltyResults, delimiter);
            //CSVFileAccessLayer.SaveStringToFile(lines, fileName, false);
        }
        #endregion

        #region SaveAssetFile
        /// <summary>
        /// Create the RecordType6 Asset file
        /// </summary>
        /// <param name="fileName">full path and filename to save</param>
        public void SaveAssetFile(int invoiceID, string delimiter, string fileName)
        {
            List<RecordType6> recordTypes;
            _dal.RecordTypes.GetRecordType6(invoiceID, out recordTypes);

            int numberOfColumnsToReturn = recordTypes[0].MBMOutput.Count;

            List<string> records = new List<string>();
            records.Add(CSVFileAccessLayer.BuildCvsString(RecordType6.MBMHeader, delimiter, numberOfColumnsToReturn));
            records.AddRange(CSVFileAccessLayer.BuildCvsString(recordTypes, delimiter, numberOfColumnsToReturn));

            CSVFileAccessLayer.SaveStringToFile(records, fileName, false);
        }
        #endregion

        #region SaveRecordType1_Exceptions
        /// <summary>
        /// Create the RecordType1 Exception file
        /// </summary>
        /// <param name="fileName">full path and filename to save</param>
        public void SaveRecordType1_Exceptions(int invoiceID, string delimiter, CurrencyConversion exportCurrency, string fileName)
        {
            List<RecordType1Exceptions> recordTypes;
            _dal.RecordTypes.GetRecordType1Exceptions(invoiceID, out recordTypes);

            CurrencyConversion.ConvertRecordTypes(recordTypes, exportCurrency);
            int numberOfColumnsToReturn = (recordTypes != null && recordTypes.Count > 0) ? recordTypes[0].MBMOutput.Count : 0;

            List<string> records = new List<string>();
            records.Add(CSVFileAccessLayer.BuildCvsString(RecordType1Exceptions.MBMHeader, delimiter, numberOfColumnsToReturn));

            if (recordTypes != null && recordTypes.Count > 0)
            {
                records.AddRange(CSVFileAccessLayer.BuildCvsString(recordTypes, delimiter, numberOfColumnsToReturn));
            }

            CSVFileAccessLayer.SaveStringToFile(records, fileName, false);
        }
        #endregion

        #region SaveRecordType2_Exceptions
        /// <summary>
        /// Create the RecordType2 Exceptions
        /// </summary>
        /// <param name="fileName">full path and filename to save</param>
        public void SaveRecordType2_Exceptions(int invoiceID, string delimiter, CurrencyConversion exportCurrency, string fileName)
        {
            List<RecordType2Exceptions> recordTypes;
            _dal.RecordTypes.GetRecordType2Exceptions(invoiceID, out recordTypes);

            CurrencyConversion.ConvertRecordTypes(recordTypes, exportCurrency);
            int numberOfColumnsToReturn = (recordTypes != null && recordTypes.Count > 0) ? recordTypes[0].MBMOutput.Count : 0;

            List<string> records = new List<string>();
            records.Add(CSVFileAccessLayer.BuildCvsString(RecordType2Exceptions.MBMHeader, delimiter, numberOfColumnsToReturn));

            if (recordTypes != null && recordTypes.Count > 0)
            {
                records.AddRange(CSVFileAccessLayer.BuildCvsString(recordTypes, delimiter, numberOfColumnsToReturn));
            }

            CSVFileAccessLayer.SaveStringToFile(records, fileName, false);
        }
        #endregion

        #region SaveRecordType4_Exceptions
        /// <summary>
        /// Create the RecordType4 Exception delimited file
        /// </summary>
        /// <param name="fileName">full path and filename to save</param>
        public void SaveRecordType4_Exceptions(int invoiceID, string delimiter, CurrencyConversion exportCurrency, string fileName)
        {
            List<RecordType4Exceptions> recordTypes;
            _dal.RecordTypes.GetRecordType4Exceptions(invoiceID, out recordTypes);

            CurrencyConversion.ConvertRecordTypes(recordTypes, exportCurrency);
            int numberOfColumnsToReturn = (recordTypes != null && recordTypes.Count > 0) ? recordTypes[0].MBMOutput.Count : 0;

            List<string> records = new List<string>();
            records.Add(CSVFileAccessLayer.BuildCvsString(RecordType4Exceptions.MBMHeader, delimiter, numberOfColumnsToReturn));

            if (recordTypes != null && recordTypes.Count > 0)
            {
                records.AddRange(CSVFileAccessLayer.BuildCvsString(recordTypes, delimiter, numberOfColumnsToReturn));
            }

            CSVFileAccessLayer.SaveStringToFile(records, fileName, false);
        }
        #endregion

        #region CreateBillingDataFile
        /// <summary>
        /// START CRQ CRQ700001260001
        /// Export the selected customer billing data
        /// Build the billing file format
        /// </summary>
        /// <param name="customerNumber">Customer identifier</param>
        /// <param name="delimiter">delimiter character</param>
        /// <param name="columnCount">number of columns per line</param>
        /// <param name="exportCurrency">conversion currency</param>
        /// <param name="fileName">full path and file to save</param>
        public void CreateBillingDataFile(int customerNumber, string delimiter, int columnCount, CurrencyConversion exportCurrency, string fileName, RecordTypesDAL.RECORDS_FILTER filter, string typeofMode)
        {
            List<string> lines = new List<string>();
            List<string> ToltalRecords = new List<string>();
            List<string> records;

            string SharedfilePath = string.Empty;
            string FilePathFormat = string.Empty;

            string FileFormat = string.Empty;
            int index = 0;
            index = fileName.LastIndexOf('.');
            FileFormat = fileName.Substring(index);

            FilePathFormat = customerNumber.ToString() + "_" + DateTime.Now.ToString("MMddyyyy") + FileFormat;

            fileName = fileName.Remove(fileName.Length - 4, 4);

            if (typeofMode.ToUpper() == "PRODUCTION")
                SharedfilePath = ConfigurationManager.AppSettings["ProdSharedLocPath"].ToString();
            else
                SharedfilePath = ConfigurationManager.AppSettings["TestSharedLocPath"].ToString();

            // Get DataSet for RecordType1
            lines.Add(CSVFileAccessLayer.BuildCvsString(RecordType1.MBMHeader, delimiter, columnCount));
            ToltalRecords.Add(CSVFileAccessLayer.BuildCvsString(RecordType1.MBMHeader, delimiter, columnCount));

            List<RecordType1> recordTypes1 = new List<RecordType1>();
            _dal.RecordTypes.GetLDRecordType1(customerNumber, filter, out recordTypes1);
            CurrencyConversion.ConvertRecordTypes(recordTypes1, exportCurrency);
            records = CSVFileAccessLayer.BuildCvsString(recordTypes1, delimiter, columnCount);
            if (records != null && records.Count > 0)
            {
                lines.AddRange(records);
                ToltalRecords.AddRange(records);
            }

            try
            {
                CSVFileAccessLayer.SaveStringToFile(lines, fileName + "_RecordType1.csv", false);
                CSVFileAccessLayer.SaveStringToFile(lines, SharedfilePath + "RecordType1_" + FilePathFormat, false);
            }
            catch (Exception ex)
            {
            }
            lines.Clear(); records.Clear();


            // Get DataSet for RecordType2
            lines.Add(CSVFileAccessLayer.BuildCvsString(RecordType2.MBMHeader, delimiter, columnCount));
            ToltalRecords.Add(CSVFileAccessLayer.BuildCvsString(RecordType2.MBMHeader, delimiter, columnCount));

            List<RecordType2> recordTypes2 = new List<RecordType2>();
            _dal.RecordTypes.GetLDRecordType2(customerNumber, filter, out recordTypes2);
            CurrencyConversion.ConvertRecordTypes(recordTypes2, exportCurrency);
            records = CSVFileAccessLayer.BuildCvsString(recordTypes2, delimiter, columnCount);
            if (records != null && records.Count > 0)
            {
                lines.AddRange(records);
                ToltalRecords.AddRange(records);
            }

            try
            {
                CSVFileAccessLayer.SaveStringToFile(lines, fileName + "_RecordType2.csv", false);
                CSVFileAccessLayer.SaveStringToFile(lines, SharedfilePath + "RecordType2_" + FilePathFormat, false);
                lines.Clear(); records.Clear();
            }
            catch (Exception ex)
            {
            }

            // Get DataSet for RecordType3
            lines.Add(CSVFileAccessLayer.BuildCvsString(RecordType3.MBMHeader, delimiter, columnCount));
            ToltalRecords.Add(CSVFileAccessLayer.BuildCvsString(RecordType3.MBMHeader, delimiter, columnCount));

            List<RecordType3> recordTypes3 = new List<RecordType3>();
            _dal.RecordTypes.GetLDRecordType3(customerNumber, filter, out recordTypes3);
            CurrencyConversion.ConvertRecordTypes(recordTypes3, exportCurrency);
            records = CSVFileAccessLayer.BuildCvsString(recordTypes3, delimiter, columnCount);
            if (records != null && records.Count > 0)
            {
                lines.AddRange(records);
                ToltalRecords.AddRange(records);
            }

            try
            {
                CSVFileAccessLayer.SaveStringToFile(lines, fileName + "_RecordType3.csv", false);
                CSVFileAccessLayer.SaveStringToFile(lines, SharedfilePath + "RecordType3_" + FilePathFormat, false);
                lines.Clear(); records.Clear();

            }
            catch (Exception ex)
            {
            }
            // Get DataSet for RecordType4
            lines.Add(CSVFileAccessLayer.BuildCvsString(RecordType4.MBMHeader, delimiter, columnCount));
            ToltalRecords.Add(CSVFileAccessLayer.BuildCvsString(RecordType4.MBMHeader, delimiter, columnCount));

            List<RecordType4> recordTypes4 = new List<RecordType4>();
            _dal.RecordTypes.GetLDRecordType4(customerNumber, filter, out recordTypes4);
            CurrencyConversion.ConvertRecordTypes(recordTypes4, exportCurrency);
            records = CSVFileAccessLayer.BuildCvsString(recordTypes4, delimiter, columnCount);
            if (records != null && records.Count > 0)
            {
                lines.AddRange(records);
                ToltalRecords.AddRange(records);
            }

            try
            {
                CSVFileAccessLayer.SaveStringToFile(lines, fileName + "_RecordType4.csv", false);
                CSVFileAccessLayer.SaveStringToFile(lines, SharedfilePath + "RecordType4_" + FilePathFormat, false);
                lines.Clear(); records.Clear();
            }
            catch (Exception ex)
            {
            }

            // Get DataSet for RecordType5

            lines.Add(CSVFileAccessLayer.BuildCvsString(RecordType5.MBMHeader, delimiter, columnCount));
            ToltalRecords.Add(CSVFileAccessLayer.BuildCvsString(RecordType5.MBMHeader, delimiter, columnCount));

            List<RecordType5> recordTypes5 = new List<RecordType5>();
            _dal.RecordTypes.GetLDRecordType5(customerNumber, filter, out recordTypes5);
            CurrencyConversion.ConvertRecordTypes(recordTypes5, exportCurrency);

            //Only one entry per record type allowed in RecordType 5
            recordTypes5 = OneEntryPerRecordType(recordTypes5);

            records = CSVFileAccessLayer.BuildCvsString(recordTypes5, delimiter, columnCount);

            if (records != null && records.Count > 0)
            {
                lines.AddRange(records);
                ToltalRecords.AddRange(records);
            }

            try
            {
                CSVFileAccessLayer.SaveStringToFile(lines, fileName + "_RecordType5.csv", false);
                CSVFileAccessLayer.SaveStringToFile(lines, SharedfilePath + "RecordType5_" + FilePathFormat, false);
                lines.Clear(); records.Clear();
                CSVFileAccessLayer.SaveStringToFile(ToltalRecords, fileName + "_GE_Tangoe_LD_Export" + FilePathFormat, false);
                CSVFileAccessLayer.SaveStringToFile(ToltalRecords, SharedfilePath + "GE_Tangoe_LD_Export" + FilePathFormat, false);

            }
            catch (Exception ex)
            {
            }
            string GetGELDEmailDistributionList = _dal.RecordTypes.GetGELDEmailDistributionList();
            if (GetGELDEmailDistributionList.Trim() != string.Empty)
            {
                string Subject = "GE LD File Processing";
                SendMailWithAttachment(SharedfilePath, FilePathFormat, GetGELDEmailDistributionList, Subject, "");
            }
        }
        #endregion

        #region SendMailWithAttachment
        /// <summary>
        /// START CRQ CRQ700001260001
        /// Send the mail with customer billiong data files to distributed list of users
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="FilePathFormat"></param>
        /// <param name="customerNumber"></param>
        /// <param name="GetGELDEmailDistributionList"></param>
        /// <returns></returns>
        public bool SendMailWithAttachment(string fileName, string FilePathFormat, string GetGELDEmailDistributionList, string Subject, string UserName)
        {
            bool flag = false;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["MailHostAddress"].ToString());
                mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString());

                mail.To.Add(GetGELDEmailDistributionList);
                mail.Subject = Subject;
                StringBuilder builder = new StringBuilder();
                builder.Append("<html>");
                builder.Append("<body>");

                if (FilePathFormat != string.Empty && fileName != string.Empty)
                {
                    builder.Append(@"Please verify the contents of the GE LD File process.");
                    mail.Attachments.Add(new System.Net.Mail.Attachment(fileName + "RecordType1_" + FilePathFormat));
                    mail.Attachments.Add(new System.Net.Mail.Attachment(fileName + "RecordType2_" + FilePathFormat));
                    mail.Attachments.Add(new System.Net.Mail.Attachment(fileName + "RecordType3_" + FilePathFormat));
                    mail.Attachments.Add(new System.Net.Mail.Attachment(fileName + "RecordType4_" + FilePathFormat));
                    mail.Attachments.Add(new System.Net.Mail.Attachment(fileName + "RecordType5_" + FilePathFormat));
                    mail.Attachments.Add(new System.Net.Mail.Attachment(fileName + "GE_Tangoe_LD_Export" + FilePathFormat));
                }
                else
                {
                    List<AccountDetails> Accountdet = new List<AccountDetails>();
                    Accountdet = _dal.Invoice.SendAccountDetails(UserName);
                    // string InvoiceNumber = fileName;//Invoice Number

                    // mail.Subject = "International Video billing processing for Invoice number: " + InvoiceNumber; 
                    // mail.Subject = "Video billing processing"; 
                    builder.Append("<table border = '1'>");
                    builder.Append("<tr>");
                    builder.Append("<td><b>Account Number</b></td>");
                    builder.Append("<td><b>Telephone Number</b></td>");
                    builder.Append("<td><b>Account Status</b></td>");
                    builder.Append("</tr>");
                    for (int row = 0; row < Accountdet.Count; row++)
                    {
                        builder.Append("<tr>");
                        builder.Append("<td>");
                        builder.Append(Accountdet[row].AccountNumber.ToString());
                        builder.Append("</td>");
                        builder.Append("<td>");
                        builder.Append(Accountdet[row].TelephoneNumber.ToString());
                        builder.Append("</td>");
                        builder.Append("<td>");
                        builder.Append(Accountdet[row].AccountStatus.ToString());
                        builder.Append("</td>");
                        builder.Append("</tr>");
                    }
                }

                builder.Append("</body>");
                builder.Append("</html>");
                mail.Body = builder.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"].ToString());// 25;
                SmtpServer.Send(mail);
                flag = true;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                flag = false;
            }
            return flag;
        }
        #endregion

        #region ProcessSnapshotData
        /// <summary>
        /// CRQ #CRQ700001314707  
        /// Compare VIPR Accounts against CRM Snapshot data
        /// </summary>
        /// <param name="caller">login of calling account</param>
        /// <returns>true on success or false on failure</returns>
        public bool ProcessSnapshotData(string UserName)
        {
            bool result = false;
            try
            {
                result = _dal.RecordTypes.CompareAccounts(UserName);
            }
            catch (Exception ex)
            {
                _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                {
                    UserName = UserName,
                    LogType = ApplicationLogType.SystemRaised,
                    ExceptionDateTime = DateTime.Now,
                    CodeLocation = "CompareAccounts()",
                    Comment = "System Exception",
                    Exception = ex,
                });
            }
            return result;
        }
        #endregion

        #region SendAccountDetails
        /// <summary>
        /// CRQ #CRQ700001314707  
        /// Get Account Details
        /// </summary>
        ///  /// <param name="caller">Invoice number</param>
        /// <param name="caller">login of calling account</param>
        /// <returns>list of Accounts</returns>
        public bool SendAccountDetails(string UserName)
        {
            try
            {
                string VIPRTeamEmails = string.Empty;
                string Subject = "Active/InActive CRM data";

                VIPRTeamEmails = ConfigurationManager.AppSettings["VIPRTeamEmails"].ToString();

                SendMailWithAttachment("", "", VIPRTeamEmails, Subject, UserName);

                //List<string> lines = new List<string>();
                //List<string> ToltalRecords = new List<string>();
                //List<string> records;
                //string delimiter = ",";
                //int columnCount = 7;
                // lines.Add(CSVFileAccessLayer.BuildCvsString(AccountDetails.GcomHeader, delimiter, columnCount));
                //  records = CSVFileAccessLayer.BuildCvsString(Accountdet, delimiter, columnCount);
                //CSVFileAccessLayer.SaveStringToFile(lines, fileName, false);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                {
                    UserName = UserName,
                    LogType = ApplicationLogType.SystemRaised,
                    ExceptionDateTime = DateTime.Now,
                    CodeLocation = "GetAccountDetails()",
                    Comment = "System Exception",
                    Exception = ex,
                });
            }
            return true;
        }
        #endregion

        #region GetAccountandTelePhonenoDetails
        /// <summary>
        /// CRQ #CRQ700001314707  
        /// Get Account and Telephone number details and consume service for Quorum process
        /// </summary>
        /// <param name="caller">Invoice Number</param>
        /// <param name="caller">login of calling account</param>
        /// <returns>true on success or false on failure</returns>
        public int GetAccountandTelePhonenoDetails(string UserName, bool debugMode)
        {
            int result = 0;
            try
            {
                List<AccountDetails> Accountdet = new List<AccountDetails>();

                System.Nullable<int> SnapshotID = 0;
                Dictionary<string, System.Nullable<DateTime>> TelephoneNumberdet = new Dictionary<string, System.Nullable<DateTime>>();
                Dictionary<System.Nullable<int>, System.Nullable<DateTime>> AccountNumbersdet = new Dictionary<System.Nullable<int>, System.Nullable<DateTime>>();

                //Get all Active and Deactive Accounts
                Accountdet = _dal.Invoice.SendAccountDetails(UserName);

                if (Accountdet != null)
                {
                    if (Accountdet.Count > 0)
                    {
                        SnapshotID = Accountdet[0].SnapshotID;

                        //Get all "Telephone Numbers" which "AccountStatusID" is "I" from Accountdet list.
                        TelephoneNumberdet = Accountdet.Where(x => x.AccountStatusID.ToString().ToUpper().Contains("I")).ToDictionary(x => x.TelephoneNumber, x => x.InstallationDate);

                        //Get all "Acount Numbers" which "AccountStatusID" is "D" from Accountdet list.
                        AccountNumbersdet = Accountdet.Where(x => x.AccountStatusID.ToString().ToUpper().Contains("D")).ToDictionary(x => x.AccountNumber, x => x.DeactivateDate);

                        //Calling CBTSService service from Quorum application


                        if (debugMode)
                        {
                            //CBTSService.CBTSServiceClient cbtsService = new CBTSService.CBTSServiceClient();
                            //List<CBTSService.GCOMAccountRequest> AccountRequest = new List<CBTSService.GCOMAccountRequest>();
                            //List<CBTSService.GCOMDeactivateAccount> DeActiveAccount = new List<CBTSService.GCOMDeactivateAccount>();
                            foreach (KeyValuePair<string, System.Nullable<DateTime>> TelphDet in TelephoneNumberdet)
                            {
                                //AccountRequest.Add(new CBTSService.GCOMAccountRequest { TelephoneNumber = TelphDet.Key, InstallationFee = true, InstallationDate = Convert.ToDateTime(TelphDet.Value) });
                            }

                            foreach (KeyValuePair<System.Nullable<int>, System.Nullable<DateTime>> AccDet in AccountNumbersdet)
                            {
                                //DeActiveAccount.Add(new CBTSService.GCOMDeactivateAccount { AccountID = Convert.ToInt32(AccDet.Key), DeactivateDate = Convert.ToDateTime(AccDet.Value) });
                            }

                            //Sending Account ID's and Telphone numbers to quorum application using "CBTSService" to activate and deactivate the accounts.

                            //cbtsService.GCOMAccountConfiguration(AccountRequest.ToArray(), DeActiveAccount.ToArray(), Convert.ToInt64(SnapshotID));
                        }
                        else
                        {
                            //CBTSServiceProd.CBTSServiceClient cbtsServiceProd = new CBTSServiceProd.CBTSServiceClient();
                            //List<CBTSServiceProd.GCOMAccountRequest> AccountRequestProd = new List<CBTSServiceProd.GCOMAccountRequest>();
                            //List<CBTSServiceProd.GCOMDeactivateAccount> DeActiveAccountProd = new List<CBTSServiceProd.GCOMDeactivateAccount>();
                            foreach (KeyValuePair<string, System.Nullable<DateTime>> TelphDet in TelephoneNumberdet)
                            {
                                //AccountRequestProd.Add(new CBTSServiceProd.GCOMAccountRequest { TelephoneNumber = TelphDet.Key, InstallationFee = true, InstallationDate = Convert.ToDateTime(TelphDet.Value) });
                            }

                            foreach (KeyValuePair<System.Nullable<int>, System.Nullable<DateTime>> AccDet in AccountNumbersdet)
                            {
                                //DeActiveAccountProd.Add(new CBTSServiceProd.GCOMDeactivateAccount { AccountID = Convert.ToInt32(AccDet.Key), DeactivateDate = Convert.ToDateTime(AccDet.Value) });
                            }

                            //Sending Account ID's and Telphone numbers to quorum application using "CBTSService" to activate and deactivate the accounts.

                            //cbtsServiceProd.GCOMAccountConfiguration(AccountRequestProd.ToArray(), DeActiveAccountProd.ToArray(), Convert.ToInt64(SnapshotID));
                        }
                        result = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                result = 2;
                _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                {
                    UserName = UserName,
                    LogType = ApplicationLogType.SystemRaised,
                    ExceptionDateTime = DateTime.Now,
                    CodeLocation = "GetAccountandTelePhonenoDetails()",
                    Comment = "System Exception",
                    Exception = ex,
                });
            }
            return result;
        }
        #endregion

        #region RT1 Detailed File Data
        public DataTable RecordType1DetailFile(int invoiceID, CurrencyConversion exportCurrency)
        {
            List<RecordType1> recordTypes1 = new List<RecordType1>();
            _dal.RecordTypes.GetRecordType1(invoiceID, out recordTypes1);
            CurrencyConversion.ConvertRecordTypes(recordTypes1, exportCurrency);

            string tableName = "Detailed File";
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add("record_type", typeof(string));
            dt.Columns.Add("billing_account_number", typeof(string));
            dt.Columns.Add("invoice_number", typeof(string));
            dt.Columns.Add("bill_date", typeof(string));
            dt.Columns.Add("originating_number", typeof(string));
            dt.Columns.Add("terminating_number", typeof(string));
            dt.Columns.Add("charge_amount", typeof(string));
            dt.Columns.Add("record_date", typeof(string));
            dt.Columns.Add("record_time", typeof(string));
            dt.Columns.Add("billable_minutes", typeof(string));
            dt.Columns.Add("na_billing_number", typeof(string));
            dt.Columns.Add("originating_city", typeof(string));
            dt.Columns.Add("originating_state", typeof(string));
            dt.Columns.Add("terminating_city", typeof(string));
            dt.Columns.Add("terminating_state", typeof(string));
            dt.Columns.Add("settlement_code", typeof(string));
            dt.Columns.Add("charge_description", typeof(string));
            dt.Columns.Add("provider", typeof(string));
            dt.Columns.Add("vendor_name", typeof(string));
            dt.Columns.Add("state", typeof(string));
            dt.Columns.Add("legal_entity", typeof(string));

            foreach (var items in recordTypes1)
            {
                var row = dt.NewRow();
                row["record_type"] = items.RecordType;
                row["billing_account_number"] = items.BAN;
                row["invoice_number"] = items.InvoiceDisplay;
                row["bill_date"] = items.BillDate;
                row["originating_number"] = items.FromNumber;
                row["terminating_number"] = items.ToNumber;
                row["charge_amount"] = items.Revenue;
                row["record_date"] = items.DateOfRecord;
                row["record_time"] = items.ConnectTime;
                row["billable_minutes"] = items.Duration;
                row["na_billing_number"] = items.BillingNumberNorthAmericanStandard;
                row["originating_city"] = items.FromCity;
                row["originating_state"] = items.FromState;
                row["terminating_city"] = items.ToCity;
                row["terminating_state"] = items.ToState;
                row["settlement_code"] = items.SettlementCode;
                row["charge_description"] = items.ChargeDescription;
                row["provider"] = items.Provider;
                row["vendor_name"] = items.VendorName;
                row["state"] = items.Locality;
                row["legal_entity"] = items.LegalEntity;

                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion

        #region RT1 Detailed and Summary File Export
        public void RT1SummaryFile(int invoiceID, CurrencyConversion exportCurrency, string fileName, string dateTime, string serverPath, InvoiceType objInvoiceType, string userName, InvoiceBL objInvoiceBL)
        {
            DataTable dtRecordType1 = new DataTable();
            dtRecordType1 = RecordType1DetailFile(invoiceID, exportCurrency);
            DataSet ds = new DataSet();
            ds.Tables.Add(dtRecordType1);

            List<RT1Summary> rT1Summary = new List<RT1Summary>();
            _dal.RecordTypes.GetRT1Summary(invoiceID, out rT1Summary);

            string tableName = "Summary File";
            DataTable dt1 = new DataTable(tableName);
            dt1.Columns.Add("Zone", typeof(string));
            dt1.Columns.Add("Count of Charges", typeof(string));
            dt1.Columns.Add("Duration", typeof(string));
            dt1.Columns.Add("Call Charges", typeof(decimal));
            dt1.Columns[dt1.Columns.Count - 1].Caption = "Currency";

            foreach (var items in rT1Summary)
            {
                var row = dt1.NewRow();
                row["Zone"] = items.Zone;
                row["Count of Charges"] = items.NoOfTimes;
                row["Duration"] = items.Duration;
                row["Call Charges"] = items.CallCharges;

                dt1.Rows.Add(row);
            }

            ds.Tables.Add(dt1);

            string RT1FileName = fileName + "_RT1_" + dateTime + ".xlsx";
            string RT1ServerPath = serverPath + RT1FileName;
            //string RT1Remotedirectory = ConfigurationManager.AppSettings["remoteDirectory"] + "/" + fileName; 
            bool success = CreateExcelFile.CreateExcelDocument(ds, RT1ServerPath);

            if (success)
            {
                if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                {
                    try
                    {
                        // Code for SFTP
                        ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT1ServerPath, RT1FileName);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                        {
                            LogType = ApplicationLogType.SystemRaised,
                            ExceptionDateTime = DateTime.Now,
                            CodeLocation = "SaveFilesToSFTPRT1()",
                            Comment = "System Exception",
                            Exception = ex,
                        });
                    }
                }
                else
                {
                    // Code for FTP
                    ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT1ServerPath, RT1FileName);
                }
                InsertInvoiceFileExportsByRecordType(invoiceID, RT1FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL);
            }

        }
        #endregion

        #region RT2 Detailed File Data
        public DataTable RecordType2DetailFile(int invoiceID, CurrencyConversion exportCurrency, InvoiceType objInvoice)
        {
            List<RecordType2> recordTypes2 = new List<RecordType2>();
            _dal.RecordTypes.GetRecordType2(invoiceID, out recordTypes2);
            CurrencyConversion.ConvertRecordTypes(recordTypes2, exportCurrency);

            string tableName = "Detailed File";
            DataTable dt2 = new DataTable(tableName);
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
                dt2.Rows.Add(row);
            }
            return dt2;
        }
        #endregion

        #region RT2 Detailed and Summary File Export
        public void RT2SummaryFile(int invoiceID, CurrencyConversion exportCurrency, string fileName, string dateTime, string serverPath, InvoiceType objInvoiceType, string userName, InvoiceBL objInvoiceBL)
        {
            DataTable dtRT2 = new DataTable();
            dtRT2 = RecordType2DetailFile(invoiceID, exportCurrency, objInvoiceType);
            DataSet ds = new DataSet();
            ds.Tables.Add(dtRT2);

            List<RT2Summary> rT2Summary = new List<RT2Summary>();
            _dal.RecordTypes.GetRT2Summary(invoiceID, out rT2Summary);

            string tableName = "Summary File";
            DataTable dt2 = new DataTable(tableName);
            dt2.Columns.Add("Profiles", typeof(string));
            dt2.Columns.Add("Count of Charges", typeof(string));
            dt2.Columns.Add("Charges", typeof(string));
            dt2.Columns[dt2.Columns.Count - 1].Caption = "Currency";

            foreach (var items in rT2Summary)
            {
                var row = dt2.NewRow();
                row["Profiles"] = items.Profiles;
                row["Count of Charges"] = items.NoOfTimes;
                row["Charges"] = items.Charges;

                dt2.Rows.Add(row);
            }
            ds.Tables.Add(dt2);

            string RT2FileName = fileName + "_RT2_" + dateTime + ".xlsx";
            string RT2ServerPath = serverPath + RT2FileName;
            bool success = CreateExcelFile.CreateExcelDocument(ds, RT2ServerPath);
            if (success)
            {
                if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                {
                    try
                    {
                        // Code for SFTP
                        ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT2ServerPath, RT2FileName);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                        {
                            LogType = ApplicationLogType.SystemRaised,
                            ExceptionDateTime = DateTime.Now,
                            CodeLocation = "SaveFilesToSFTPRT2()",
                            Comment = "System Exception",
                            Exception = ex,
                        });
                    }
                }
                else
                {
                    // Code for FTP
                    ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT2ServerPath, RT2FileName);
                }
                InsertInvoiceFileExportsByRecordType(invoiceID, RT2FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL);
            }
        }
        #endregion

        #region RT3 Detailed File Export
        public void RecordType3DetailFile(int invoiceID, CurrencyConversion exportCurrency, string fileName, string dateTime, string serverPath, InvoiceType objInvoiceType, string userName, InvoiceBL objInvoiceBL)
        {
            List<RecordType3> recordTypes3 = new List<RecordType3>();
            _dal.RecordTypes.GetRecordType3(invoiceID, out recordTypes3);
            CurrencyConversion.ConvertRecordTypes(recordTypes3, exportCurrency);

            string tabName = "Detailed File";
            DataTable dt3 = new DataTable(tabName);
            dt3.Columns.Add("record_type", typeof(string));
            dt3.Columns.Add("billing_account_number", typeof(string));
            dt3.Columns.Add("invoice_number", typeof(string));
            dt3.Columns.Add("bill_date", typeof(string));
            dt3.Columns.Add("record_type_1_total", typeof(string));
            dt3.Columns.Add("record_type_2_total", typeof(string));
            dt3.Columns.Add("record_type_4_total", typeof(string));
            dt3.Columns.Add("grand_total", typeof(string));
            dt3.Columns.Add("legal_entity", typeof(string));

            foreach (var items in recordTypes3)
            {
                var row = dt3.NewRow();
                row["record_type"] = items.RecordType;
                row["billing_account_number"] = items.BAN;
                row["invoice_number"] = items.InvoiceDisplay;
                row["bill_date"] = items.BillDate;
                row["record_type_1_total"] = items.RecordType1Total;
                row["record_type_2_total"] = items.RecordType2Total;
                row["record_type_4_total"] = items.RecordType4Total;
                row["grand_total"] = items.Total;
                row["legal_entity"] = items.LegalEntity;

                dt3.Rows.Add(row);
            }
            DataSet ds3 = new DataSet();
            ds3.Tables.Add(dt3);

            string RT3FileName = fileName + "_RT3_" + dateTime + ".xlsx";
            string RT3ServerPath = serverPath + RT3FileName;
            bool success = CreateExcelFile.CreateExcelDocument(ds3, RT3ServerPath);
            if (success)
            {
                if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                {
                    try
                    {
                        // Code for SFTP
                        ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT3ServerPath, RT3FileName);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                        {
                            LogType = ApplicationLogType.SystemRaised,
                            ExceptionDateTime = DateTime.Now,
                            CodeLocation = "SaveFilesToSFTPRT3()",
                            Comment = "System Exception",
                            Exception = ex,
                        });
                    }
                }
                else
                {
                    // Code for FTP
                    ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT3ServerPath, RT3FileName);
                }
                InsertInvoiceFileExportsByRecordType(invoiceID, RT3FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL);
            }
        }
        #endregion

        #region RT4 Detailed File Data
        public DataTable RecordType4DetailFile(int invoiceID, CurrencyConversion exportCurrency)
        {
            List<RecordType4> recordTypes4 = new List<RecordType4>();
            _dal.RecordTypes.GetRecordType4(invoiceID, out recordTypes4);
            CurrencyConversion.ConvertRecordTypes(recordTypes4, exportCurrency);

            string tableName = "Detailed File";
            DataTable dt4 = new DataTable(tableName);
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
            return dt4;
        }
        #endregion

        #region RT4 Deatiled and Summary File export
        public void RT4SummaryFile(int invoiceID, CurrencyConversion exportCurrency, string fileName, string dateTime, string serverPath, InvoiceType objInvoiceType, string userName, InvoiceBL objInvoiceBL)
        {
            DataTable dtRT4 = new DataTable();
            dtRT4 = RecordType4DetailFile(invoiceID, exportCurrency);
            DataSet ds4 = new DataSet();
            ds4.Tables.Add(dtRT4);

            List<RT4Summary> rT4Summary = new List<RT4Summary>();
            _dal.RecordTypes.GetRT4Summary(invoiceID, out rT4Summary);

            string tableName = "Summary File";
            DataTable dt4 = new DataTable(tableName);
            dt4.Columns.Add("ChargeDescription", typeof(string));
            dt4.Columns.Add("Count of Charges", typeof(string));
            dt4.Columns.Add("Charges", typeof(string));
            dt4.Columns[dt4.Columns.Count - 1].Caption = "Currency";

            foreach (var items in rT4Summary)
            {
                var row = dt4.NewRow();
                row["ChargeDescription"] = items.ChargeDescription;
                row["Count of Charges"] = items.NoOfTimes;
                row["Charges"] = items.TaxCharges;

                dt4.Rows.Add(row);
            }
            ds4.Tables.Add(dt4);

            string RT4FileName = fileName + "_RT4_" + dateTime + ".xlsx";
            string RT4ServerPath = serverPath + RT4FileName;
            bool success = CreateExcelFile.CreateExcelDocument(ds4, RT4ServerPath);
            if (success)
            {
                if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                {
                    try
                    {
                        // Code for SFTP
                        ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT4ServerPath, RT4FileName);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                        {
                            LogType = ApplicationLogType.SystemRaised,
                            ExceptionDateTime = DateTime.Now,
                            CodeLocation = "SaveFilesToSFTPRT4()",
                            Comment = "System Exception",
                            Exception = ex,
                        });
                    }
                }
                else
                {
                    // Code for FTP
                    ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT4ServerPath, RT4FileName);
                }
                InsertInvoiceFileExportsByRecordType(invoiceID, RT4FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL);
            }
        }
        #endregion

        #region RT5 Detailed File Export
        public void RecordType5DetailFile(int invoiceID, CurrencyConversion exportCurrency, string fileName, string dateTime, string serverPath, InvoiceType objInvoiceType, string userName, InvoiceBL objInvoiceBL)
        {
            List<RecordType5> recordTypes5 = new List<RecordType5>();
            _dal.RecordTypes.GetRecordType5(invoiceID, out recordTypes5);
            CurrencyConversion.ConvertRecordTypes(recordTypes5, exportCurrency);

            string tabName = "Detailed File";
            DataTable dt5 = new DataTable(tabName);
            dt5.Columns.Add("record_type", typeof(string));
            dt5.Columns.Add("invoice_number", typeof(string));
            dt5.Columns.Add("checked_record_type", typeof(string));
            dt5.Columns.Add("record_count", typeof(string));
            dt5.Columns.Add("summed_field_name", typeof(string));
            dt5.Columns.Add("grand_total", typeof(string));
            dt5.Columns.Add("legal_entity", typeof(string));

            foreach (var items in recordTypes5)
            {
                var row = dt5.NewRow();
                row["record_type"] = items.RecordType;
                row["invoice_number"] = items.BAN;
                row["checked_record_type"] = items.CheckRecordType;
                row["record_count"] = items.TotalRecordCount;
                row["summed_field_name"] = items.SumFieldName;
                row["grand_total"] = items.TotalAmount;
                row["legal_entity"] = items.LegalEntity;

                dt5.Rows.Add(row);
            }
            DataSet ds5 = new DataSet();
            ds5.Tables.Add(dt5);

            string RT5FileName = fileName + "_RT5_" + dateTime + ".xlsx";
            string RT5ServerPath = serverPath + RT5FileName;
            bool success = CreateExcelFile.CreateExcelDocument(ds5, RT5ServerPath);
            if (success)
            {
                if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                {
                    try
                    {
                        // Code for SFTP
                        ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT5ServerPath, RT5FileName);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                        {
                            LogType = ApplicationLogType.SystemRaised,
                            ExceptionDateTime = DateTime.Now,
                            CodeLocation = "SaveFilesToSFTPRT5()",
                            Comment = "System Exception",
                            Exception = ex,
                        });
                    }
                }
                else
                {
                    // Code for FTP
                    ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT5ServerPath, RT5FileName);
                }
                InsertInvoiceFileExportsByRecordType(invoiceID, RT5FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL);
            }
        }
        #endregion

        //8967
        #region OneType Detailed File Data
        public DataTable OneTypeDetailFile(int invoiceID, CurrencyConversion exportCurrency)
        {
            List<RecordType2> recordTypes2 = new List<RecordType2>();
            _dal.RecordTypes.GetOneFileFormatDetail(invoiceID, out recordTypes2);
            CurrencyConversion.ConvertRecordTypes(recordTypes2, exportCurrency);

            string tableName = "User Detail";
            DataTable dt2 = new DataTable(tableName);
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
            return dt2;
        }
        #endregion

        //6798
        #region One Detailed and Summary File Export
        public void OneSummaryFile(int invoiceID, CurrencyConversion exportCurrency, string fileName, string dateTime, string serverPath, InvoiceType objInvoiceType, string userName, InvoiceBL objInvoiceBL)
        {
            DataSet ds = new DataSet();

            //Sheet 1
            //DataTable dtRT1 = new DataTable();
            //dtRT1 = RecordType1DetailFile(invoiceID, exportCurrency);
            //dtRT1.TableName = "LD Usage Detail";//cbe_8967

            //ds.Tables.Add(dtRT1);//commenting sheet1 tab as a part of change request bugid#21988

            //Sheet 2
            List<RecordType1> recordTypes1 = new List<RecordType1>();
            _dal.RecordTypes.GetOneTypeForSingleInvoice(invoiceID, out recordTypes1);
            string tableName = "Call Usage Summary";
            DataTable dt1 = new DataTable(tableName);
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

            //Sheet 3
            DataTable dtRT2 = new DataTable();
            dtRT2 = OneTypeDetailFile(invoiceID, exportCurrency);
            ds.Tables.Add(dtRT2);

            //Sheet 4
            List<RT2Summary> rT2Summary = new List<RT2Summary>();
            _dal.RecordTypes.GetRT2Summary(invoiceID, out rT2Summary);
            string tableName2 = "User Summary";
            DataTable dt2 = new DataTable(tableName2);
            dt2.Columns.Add("Profiles", typeof(string));
            dt2.Columns.Add("Count of Charges", typeof(string));
            dt2.Columns.Add("Charges", typeof(string));
            dt2.Columns[dt2.Columns.Count - 1].Caption = "Currency";
            foreach (var items in rT2Summary)
            {
                var row = dt2.NewRow();
                row["Profiles"] = items.Profiles;
                row["Count of Charges"] = items.NoOfTimes;
                row["Charges"] = items.Charges;

                dt2.Rows.Add(row);
            }
            ds.Tables.Add(dt2);

            //Sheet5
            DataTable dtRT4 = new DataTable();
            dtRT4 = RecordType4DetailFile(invoiceID, exportCurrency);
            dtRT4.TableName = "Tax Detail";
            DataSet ds4 = new DataSet();
            ds.Tables.Add(dtRT4);

            //Sheet6
            List<RecordType3> recordTypes3 = new List<RecordType3>();
            _dal.RecordTypes.GetRecordType3(invoiceID, out recordTypes3);
            CurrencyConversion.ConvertRecordTypes(recordTypes3, exportCurrency);
            string tabName = "Billing Account Summary";
            DataTable dt3 = new DataTable(tabName);
            dt3.Columns.Add("record_type", typeof(string));
            dt3.Columns.Add("billing_account_number", typeof(string));
            dt3.Columns.Add("invoice_number", typeof(string));
            dt3.Columns.Add("bill_date", typeof(string));
            dt3.Columns.Add("record_type_1_total", typeof(string));
            dt3.Columns.Add("record_type_2_total", typeof(string));
            dt3.Columns.Add("record_type_4_total", typeof(string));
            dt3.Columns.Add("grand_total", typeof(string));
            dt3.Columns.Add("legal_entity", typeof(string));

            foreach (var items in recordTypes3)
            {
                var row = dt3.NewRow();
                row["record_type"] = items.RecordType;
                row["billing_account_number"] = items.BAN;
                row["invoice_number"] = items.InvoiceDisplay;
                row["bill_date"] = items.BillDate;
                row["record_type_1_total"] = items.RecordType1Total;
                row["record_type_2_total"] = items.RecordType2Total;
                row["record_type_4_total"] = items.RecordType4Total;
                row["grand_total"] = items.Total;
                row["legal_entity"] = items.LegalEntity;

                dt3.Rows.Add(row);
            }
            DataSet ds3 = new DataSet();
            ds.Tables.Add(dt3);

            string RT2FileName = fileName + "_OneType_" + dateTime + ".xlsx";
            string RT2ServerPath = serverPath + RT2FileName;
            bool success = CreateExcelFile.CreateExcelDocument(ds, RT2ServerPath);
            if (success)
            {
                if (objInvoiceType.DefaultFTP.StartsWith("sftp://"))
                {
                    try
                    {
                        // Code for SFTP
                        ExportExcelToSFTP.SaveFilesToSFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT2ServerPath, RT2FileName);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogExceptionToDatabase(new ApplicationLogEntity()
                        {
                            LogType = ApplicationLogType.SystemRaised,
                            ExceptionDateTime = DateTime.Now,
                            CodeLocation = "SaveFilesToSFTPRT2()",
                            Comment = "System Exception",
                            Exception = ex,
                        });
                    }
                }
                else
                {
                    // Code for FTP
                    ExportExcelToFTP.SaveFilesToFTP(objInvoiceType.DefaultFTP, objInvoiceType.FTPUserName, objInvoiceType.FTPPassword, RT2ServerPath, RT2FileName);
                }
                InsertInvoiceFileExportsByRecordType(invoiceID, RT2FileName, objInvoiceType.DefaultFTP, userName, objInvoiceBL);
            }
        }
        #endregion

    }
}

