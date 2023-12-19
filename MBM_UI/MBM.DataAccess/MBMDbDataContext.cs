using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using System.Configuration;

namespace MBM.DataAccess
{
    partial class MBMDbDataContext : System.Data.Linq.DataContext
    {
        private static string MBMConnectionString = ConfigurationManager.ConnectionStrings["MBMConnectionString"].ToString();
        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        /// <summary>
        /// Added so that it avoids short timeouts for the long record processes
        /// </summary>
        partial void OnCreated();


        //public MBMDbDataContext() : base(global::MBM.DataAccess.Properties.Settings.Default.MBMDevConnectionString, mappingSource)
        public MBMDbDataContext()
            : base(MBMConnectionString, mappingSource)
        {
            OnCreated();
            CommandTimeout = 0;
        }

        public MBMDbDataContext(string connection)
            : base(connection, mappingSource)
        {
            OnCreated();
            CommandTimeout = 0;
        }

        public MBMDbDataContext(System.Data.IDbConnection connection)
            : base(connection, mappingSource)
        {
            OnCreated();
            CommandTimeout = 0;
        }

        public MBMDbDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource)
            : base(connection, mappingSource)
        {
            OnCreated();
            CommandTimeout = 0;
        }

        public MBMDbDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource)
            : base(connection, mappingSource)
        {
            OnCreated();
            CommandTimeout = 0;
        }

        #region
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.ApplicationLog_Get")]
        public ISingleResult<ApplicationLog_GetResult> ApplicationLog_Get([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MaxRowsReturned", DbType = "Int")] System.Nullable<int> maxRowsReturned)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), maxRowsReturned);
            return ((ISingleResult<ApplicationLog_GetResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_GetServiceDeskData_NoGateways")]
        public ISingleResult<usp_GetServiceDeskData_NoGatewaysResult> usp_GetServiceDeskData_NoGateways()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<usp_GetServiceDeskData_NoGatewaysResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.ApplicationLog_InsertItem")]
        public int ApplicationLog_InsertItem([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExceptionDateTime", DbType = "DateTime")] System.Nullable<System.DateTime> exceptionDateTime, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CodeLocation", DbType = "VarChar(300)")] string codeLocation, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Comment", DbType = "VarChar(1000)")] string comment, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Message", DbType = "VarChar(8000)")] string message, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Source", DbType = "VarChar(8000)")] string source, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TargetSite", DbType = "VarChar(300)")] string targetSite, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "StackTrace", DbType = "VarChar(8000)")] string stackTrace, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LogType", DbType = "Int")] System.Nullable<int> logType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exceptionDateTime, codeLocation, comment, message, source, targetSite, stackTrace, logType);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.BAN_AddCanadianBAN")]
        public int BAN_AddCanadianBAN()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.CanadianTaxes_GetByDate")]
        public ISingleResult<CanadianTaxes_GetByDateResult> CanadianTaxes_GetByDate([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] System.Nullable<System.DateTime> snapDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), snapDate);
            return ((ISingleResult<CanadianTaxes_GetByDateResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.CbadTaxes_DeleteByInvoice")]
        public int CbadTaxes_DeleteByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.CbadTaxes_GetArtifactCountForTaxImport")]
        public ISingleResult<CbadTaxes_GetArtifactCountForTaxImportResult> CbadTaxes_GetArtifactCountForTaxImport([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<CbadTaxes_GetArtifactCountForTaxImportResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Configuration_Get")]
        public ISingleResult<Configuration_GetResult> Configuration_Get()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<Configuration_GetResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Configuration_Update")]
        public int Configuration_Update([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ConfigName", DbType = "VarChar(50)")] string configName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ConfigValue", DbType = "VarChar(8000)")] string configValue, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LastUpdatedDate", DbType = "DateTime")] System.Nullable<System.DateTime> lastUpdatedDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LastUpdatedBy", DbType = "VarChar(50)")] string lastUpdatedBy)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), configName, configValue, lastUpdatedDate, lastUpdatedBy);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Currency_GetAll")]
        public ISingleResult<Currency_GetAllResult> Currency_GetAll()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<Currency_GetAllResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Currency_GetCurrent")]
        public ISingleResult<Currency_GetCurrentResult> Currency_GetCurrent([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] System.Nullable<System.DateTime> snapDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), snapDate);
            return ((ISingleResult<Currency_GetCurrentResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.EC500_Delete")]
        public int EC500_Delete([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.ExceptionLog_Get")]
        public ISingleResult<ExceptionLog_GetResult> ExceptionLog_Get([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> maxRowsReturned)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), maxRowsReturned);
            return ((ISingleResult<ExceptionLog_GetResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.ExceptionLog_InsertItem")]
        public int ExceptionLog_InsertItem([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Type", DbType = "VarChar(50)")] string type, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Comment", DbType = "VarChar(MAX)")] string comment, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "StackTrace", DbType = "VarChar(MAX)")] string stackTrace, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CreatedBy", DbType = "VarChar(50)")] string createdBy)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, type, comment, stackTrace, createdBy);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.GE_LoyaltyCredit_Create")]
        public int GE_LoyaltyCredit_Create([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoyaltyCredit", DbType = "Decimal(18,10)")] System.Nullable<decimal> loyaltyCredit)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, loyaltyCredit);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.GE_LoyaltyCredit_GetByInvoice")]
        public ISingleResult<GE_LoyaltyCredit_GetByInvoiceResult> GE_LoyaltyCredit_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNum", DbType = "Int")] System.Nullable<int> invoiceNum)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceNum);
            return ((ISingleResult<GE_LoyaltyCredit_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.GE_LoyaltyCredit_YearReport")]
        public ISingleResult<GE_LoyaltyCredit_YearReportResult> GE_LoyaltyCredit_YearReport([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "Int")] System.Nullable<int> invoiceNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SummarizeReport", DbType = "Bit")] System.Nullable<bool> summarizeReport)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceNumber, summarizeReport);
            return ((ISingleResult<GE_LoyaltyCredit_YearReportResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.GEGCOMMultiLocationSummary_Delete")]
        public int GEGCOMMultiLocationSummary_Delete([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Invoice_DeleteByInvoice")]
        public int Invoice_DeleteByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Invoice_GetById")]
        public ISingleResult<Invoice_GetByIdResult> Invoice_GetById([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<Invoice_GetByIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Invoice_GetByType")]
        public ISingleResult<Invoice_GetByTypeResult> Invoice_GetByType([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceType", DbType = "Int")] System.Nullable<int> invoiceType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "VarChar(50)")] string invoiceNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GetDeleted", DbType = "Bit")] System.Nullable<bool> getDeleted)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceType, invoiceNumber, getDeleted);
            return ((ISingleResult<Invoice_GetByTypeResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Invoice_Upsert")]
        public int Invoice_Upsert(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ID", DbType = "Int")] System.Nullable<int> iD,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeID", DbType = "Int")] System.Nullable<int> invoiceTypeID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "VarChar(50)")] string invoiceNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingMonth", DbType = "Int")] System.Nullable<int> billingMonth,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingYear", DbType = "Int")] System.Nullable<int> billingYear,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DefaultImportCurrencyID", DbType = "Int")] System.Nullable<int> defaultImportCurrencyID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "VarChar(50)")] string status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LastAction", DbType = "VarChar(50)")] string lastAction,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType1_Status", DbType = "VarChar(50)")] string recordType1_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType1_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> recordType1_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType2_Status", DbType = "VarChar(50)")] string recordType2_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType2_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> recordType2_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType3_Status", DbType = "VarChar(50)")] string recordType3_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType3_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> recordType3_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType4_Status", DbType = "VarChar(50)")] string recordType4_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType4_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> recordType4_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType5_Status", DbType = "VarChar(50)")] string recordType5_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType5_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> recordType5_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingFileExport_Status", DbType = "VarChar(50)")] string billingFileExport_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingFileExport_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> billingFileExport_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingFileExport_Path", DbType = "VarChar(8000)")] string billingFileExport_Path,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceDeskSnapshotID", DbType = "Int")] System.Nullable<int> serviceDeskSnapshotID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD, invoiceTypeID, invoiceNumber, billingMonth, billingYear, defaultImportCurrencyID, userName, status, lastAction, recordType1_Status, recordType1_DateTime, recordType2_Status, recordType2_DateTime, recordType3_Status, recordType3_DateTime, recordType4_Status, recordType4_DateTime, recordType5_Status, recordType5_DateTime, billingFileExport_Status, billingFileExport_DateTime, billingFileExport_Path, exportCurrencyID, serviceDeskSnapshotID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.InvoiceFileUploads_GetByInvoice")]
        public ISingleResult<InvoiceFileUploads_GetByInvoiceResult> InvoiceFileUploads_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<InvoiceFileUploads_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.InvoiceFileUploads_Upsert")]
        public int InvoiceFileUploads_Upsert([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileType", DbType = "Int")] System.Nullable<int> fileType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FilePath", DbType = "VarChar(MAX)")] string filePath, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "VarChar(50)")] string status, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, fileType, filePath, status, userName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.InvoiceType_Get")]
        public ISingleResult<InvoiceType_GetResult> InvoiceType_Get()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<InvoiceType_GetResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.OneTimeCharges_DeleteByInvoice")]
        public int OneTimeCharges_DeleteByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.OSN_FlatRate_DeleteByInvoice")]
        public int OSN_FlatRate_DeleteByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.OSN_LongDistance_DeleteByInvoice")]
        public int OSN_LongDistance_DeleteByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.OSN_UsageSummary_DeleteByInvoice")]
        public int OSN_UsageSummary_DeleteByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.ProcessLog_Get")]
        public ISingleResult<ProcessLog_GetResult> ProcessLog_Get([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MaxRowsToReturn", DbType = "Int")] System.Nullable<int> maxRowsToReturn)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, maxRowsToReturn);
            return ((ISingleResult<ProcessLog_GetResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.ProcessLog_InsertItem")]
        public int ProcessLog_InsertItem([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProcessName", DbType = "VarChar(50)")] string processName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProcessResult", DbType = "VarChar(50)")] string processResult, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProcessDateTime", DbType = "DateTime")] System.Nullable<System.DateTime> processDateTime, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Comment", DbType = "VarChar(8000)")] string comment, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CreatedBy", DbType = "VarChar(50)")] string createdBy)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, processName, processResult, processDateTime, comment, createdBy);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Profiles_GetActiveByDate")]
        public ISingleResult<Profiles_GetActiveByDateResult> Profiles_GetActiveByDate([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceYear", DbType = "Int")] System.Nullable<int> invoiceYear, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceMonth", DbType = "Int")] System.Nullable<int> invoiceMonth)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceYear, invoiceMonth);
            return ((ISingleResult<Profiles_GetActiveByDateResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Profiles_GetActiveByInvoice")]
        public ISingleResult<Profiles_GetActiveByInvoiceResult> Profiles_GetActiveByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<Profiles_GetActiveByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType1_DeleteByInvoice")]
        public int RecordType1_DeleteByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType1_GetArtifactCountForInvoiceImport")]
        public ISingleResult<RecordType1_GetArtifactCountForInvoiceImportResult> RecordType1_GetArtifactCountForInvoiceImport([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<RecordType1_GetArtifactCountForInvoiceImportResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType1_GetByInvoice")]
        public ISingleResult<RecordType1_GetByInvoiceResult> RecordType1_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<RecordType1_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType1_GetExceptionsByInvoice")]
        public ISingleResult<RecordType1_GetExceptionsByInvoiceResult> RecordType1_GetExceptionsByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<RecordType1_GetExceptionsByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType1_Process")]
        public int RecordType1_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType1_ProcessCanada")]
        public int RecordType1_ProcessCanada([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType1_ProcessUS")]
        public int RecordType1_ProcessUS([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType1_ValidateInvoice")]
        public ISingleResult<RecordType1_ValidateInvoiceResult> RecordType1_ValidateInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<RecordType1_ValidateInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType2_GetByInvoice")]
        public ISingleResult<RecordType2_GetByInvoiceResult> RecordType2_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<RecordType2_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType2_GetByInvoiceWithoutGateways")]
        public ISingleResult<RecordType2_GetByInvoiceWithoutGatewaysResult> RecordType2_GetByInvoiceWithoutGateways([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<RecordType2_GetByInvoiceWithoutGatewaysResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType2_GetExceptionsByInvoice")]
        public ISingleResult<RecordType2_GetExceptionsByInvoiceResult> RecordType2_GetExceptionsByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<RecordType2_GetExceptionsByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType2_GetGatewaysByInvoiceID")]
        public ISingleResult<RecordType2_GetGatewaysByInvoiceIDResult> RecordType2_GetGatewaysByInvoiceID([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<RecordType2_GetGatewaysByInvoiceIDResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType2_Process")]
        public int RecordType2_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType2_ValidateInvoice")]
        public ISingleResult<RecordType2_ValidateInvoiceResult> RecordType2_ValidateInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<RecordType2_ValidateInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType3_GetByInvoice")]
        public ISingleResult<RecordType3_GetByInvoiceResult> RecordType3_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<RecordType3_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType3_Process")]
        public int RecordType3_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType4_GetByInvoice")]
        public ISingleResult<RecordType4_GetByInvoiceResult> RecordType4_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<RecordType4_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType4_GetExceptionsByInvoice")]
        public ISingleResult<RecordType4_GetExceptionsByInvoiceResult> RecordType4_GetExceptionsByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<RecordType4_GetExceptionsByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType4_Process")]
        public int RecordType4_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType4_ProcessCanada")]
        public int RecordType4_ProcessCanada([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType4_ProcessUS")]
        public int RecordType4_ProcessUS([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType4_ValidateInvoice")]
        public ISingleResult<RecordType4_ValidateInvoiceResult> RecordType4_ValidateInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<RecordType4_ValidateInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType5_GetByInvoice")]
        public ISingleResult<RecordType5_GetByInvoiceResult> RecordType5_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<RecordType5_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType5_Process")]
        public ISingleResult<RecordType5_ProcessResult> RecordType5_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
            return ((ISingleResult<RecordType5_ProcessResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType6_Get")]
        public ISingleResult<RecordType6_GetResult> RecordType6_Get([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<RecordType6_GetResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType6_GetServiceDesk")]
        public ISingleResult<RecordType6_GetServiceDeskResult> RecordType6_GetServiceDesk()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<RecordType6_GetServiceDeskResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.ServiceDesk_TakeNewSnapshot")]
        public int ServiceDesk_TakeNewSnapshot([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(MAX)")] string notes)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), notes);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_GetServiceDeskData")]
        public ISingleResult<usp_GetServiceDeskDataResult> usp_GetServiceDeskData()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<usp_GetServiceDeskDataResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType1_GetByCustomerNumber")]
        public ISingleResult<RecordType1_GetByCustomerNumberResult> RecordType1_GetByCustomerNumber([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CustomerNumber", DbType = "Int")] System.Nullable<int> customerNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerNumber);
            return ((ISingleResult<RecordType1_GetByCustomerNumberResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType2_GetByCustomerNumber")]
        public ISingleResult<RecordType2_GetByCustomerNumberResult> RecordType2_GetByCustomerNumber([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CustomerNumber", DbType = "Int")] System.Nullable<int> customerNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerNumber);
            return ((ISingleResult<RecordType2_GetByCustomerNumberResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType3_GetByCustomerNumber")]
        public ISingleResult<RecordType3_GetByCustomerNumberResult> RecordType3_GetByCustomerNumber([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CustomerNumber", DbType = "Int")] System.Nullable<int> customerNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerNumber);
            return ((ISingleResult<RecordType3_GetByCustomerNumberResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType4_GetByCustomerNumber")]
        public ISingleResult<RecordType4_GetByCustomerNumberResult> RecordType4_GetByCustomerNumber([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CustomerNumber", DbType = "Int")] System.Nullable<int> customerNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerNumber);
            return ((ISingleResult<RecordType4_GetByCustomerNumberResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.RecordType5_GetByCustomerNumber")]
        public ISingleResult<RecordType5_GetByCustomerNumberResult> RecordType5_GetByCustomerNumber([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CustomerNumber", DbType = "Int")] System.Nullable<int> customerNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerNumber);
            return ((ISingleResult<RecordType5_GetByCustomerNumberResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_GetCustomerNumbers")]
        public ISingleResult<usp_GetCustomerNumbersResult> usp_GetCustomerNumbers()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<usp_GetCustomerNumbersResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.ProcessRecordTypesForLD")]
        public int ProcessRecordTypesForLD([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CustomerNumber", DbType = "VarChar(50)")] string customerNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(50)")] string callerLogin)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerNumber, callerLogin);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.GELD_GetEmailDistributionList")]
        public ISingleResult<GELD_GetEmailDistributionListResult> GELD_GetEmailDistributionList()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<GELD_GetEmailDistributionListResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_CompareAccounts")]
        public int usp_CompareAccounts([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(50)")] string loggedInUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), loggedInUser);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_GetAccountDetailsbyInvoice")]
        public ISingleResult<usp_GetAccountDetailsbyInvoiceResult> usp_GetAccountDetailsbyInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(50)")] string loggedInUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), loggedInUser);
            return ((ISingleResult<usp_GetAccountDetailsbyInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_ProcessUSBillingData")]
        public int usp_ProcessUSBillingData([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(50)")] string loggedInUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), loggedInUser);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_ImportInternationalSnapshotData")]
        public int usp_ImportInternationalSnapshotData([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(MAX)")] string notes)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), notes);
            return ((int)(result.ReturnValue));
        }

        //[global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceById")]
        //public ISingleResult<get_InvoiceByIdResult> get_InvoiceById([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
        //    return ((ISingleResult<get_InvoiceByIdResult>)(result.ReturnValue));
        //}

        //[global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceNumbersByTypeId")]
        //public ISingleResult<get_InvoiceNumbersByTypeIdResult> get_InvoiceNumbersByTypeId([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
        //    return ((ISingleResult<get_InvoiceNumbersByTypeIdResult>)(result.ReturnValue));
        //}

        //[global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Insert_InvoiceType")]
        //public int Insert_InvoiceType([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeName", DbType = "VarChar(50)")] string invoiceTypeName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Prefix", DbType = "VarChar(50)")] string prefix, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "VendorName", DbType = "VarChar(50)")] string vendorName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ImportCurrencyDefault", DbType = "VarChar(3)")] string importCurrencyDefault, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyDefault", DbType = "VarChar(3)")] string exportCurrencyDefault, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DefaultFTP", DbType = "VarChar(100)")] string defaultFTP, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTPUserName", DbType = "VarChar(100)")] string fTPUserName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTPPassword", DbType = "VarChar(100)")] string fTPPassword, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(100)")] string loggedInUser, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsAutoPreBilling", DbType = "Bit")] System.Nullable<bool> isAutoPreBilling, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DaysBeforeBillCycle", DbType = "Int")] System.Nullable<int> daysBeforeBillCycle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsAutoPostBilling", DbType = "Bit")] System.Nullable<bool> isAutoPostBilling, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DaysAfterBillCycle", DbType = "Int")] System.Nullable<int> daysAfterBillCycle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "OutputFileFormat", DbType = "Int")] System.Nullable<int> outputFileFormat, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingSystem", DbType = "VarChar(50)")] string BillingSystem, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AutomationFrequency", DbType = "VarChar(50)")] string AutomationFrequency) ////SERO-1582
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeName, prefix, bAN, vendorName, importCurrencyDefault, exportCurrencyDefault, defaultFTP, fTPUserName, fTPPassword, loggedInUser, isAutoPreBilling, daysBeforeBillCycle, isAutoPostBilling, daysAfterBillCycle, outputFileFormat, BillingSystem, AutomationFrequency);
        //    return ((int)(result.ReturnValue));
        //}

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Insert_InvoiceType")]
        public int Insert_InvoiceType([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeName", DbType = "VarChar(50)")] string invoiceTypeName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Prefix", DbType = "VarChar(50)")] string prefix, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "VendorName", DbType = "VarChar(50)")] string vendorName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ImportCurrencyDefault", DbType = "VarChar(3)")] string importCurrencyDefault, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyDefault", DbType = "VarChar(3)")] string exportCurrencyDefault, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DefaultFTP", DbType = "VarChar(100)")] string defaultFTP, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTPUserName", DbType = "VarChar(100)")] string fTPUserName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTPPassword", DbType = "VarChar(100)")] string fTPPassword, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(100)")] string loggedInUser, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsAutoPreBilling", DbType = "Bit")] System.Nullable<bool> isAutoPreBilling, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DaysBeforeBillCycle", DbType = "Int")] System.Nullable<int> daysBeforeBillCycle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsAutoPostBilling", DbType = "Bit")] System.Nullable<bool> isAutoPostBilling, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DaysAfterBillCycle", DbType = "Int")] System.Nullable<int> daysAfterBillCycle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "OutputFileFormat", DbType = "Int")] System.Nullable<int> outputFileFormat, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsSOO", DbType = "Int")] System.Nullable<int> isSOO, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmailAddress", DbType = "VarChar(100)")] string emailAddress, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EDI", DbType = "Bit")] System.Nullable<bool> eDI, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingSystem", DbType = "VarChar(50)")] string billingSystem, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AutomationFrequency", DbType = "VarChar(50)")] string automationFrequency, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ContractNumber", DbType = "VarChar(50)")] string contractNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ContractStartDate", DbType = "Date")] DateTime contractStartDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ContractEndDate", DbType = "Date")] DateTime contractEndDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IndirectPartnerOrRepCode", DbType = "VarChar(50)")] string indirectPartnerOrRepCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GLDepartmentCode", DbType = "VarChar(50)")] string glDepartmentCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IndirectAgentRegion", DbType = "VarChar(50)")] string indirectAgentRegion)
        {
            IExecuteResult result = this.ExecuteMethodCall(this,
                ((MethodInfo)(MethodInfo.GetCurrentMethod())),
                invoiceTypeName,
                prefix,
                bAN,
                vendorName,
                importCurrencyDefault,
                exportCurrencyDefault, defaultFTP, fTPUserName, fTPPassword, loggedInUser, isAutoPreBilling, daysBeforeBillCycle, isAutoPostBilling, daysAfterBillCycle, outputFileFormat, isSOO, emailAddress, eDI, billingSystem, automationFrequency
               , contractNumber
               , contractStartDate
               , contractEndDate
               , indirectPartnerOrRepCode
               , glDepartmentCode
               , indirectAgentRegion
                );
            return ((int)(result.ReturnValue));
        }


        //[global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Update_InvoiceType")]
        //public int Update_InvoiceType([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeID", DbType = "Int")] System.Nullable<int> invoiceTypeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeName", DbType = "VarChar(50)")] string invoiceTypeName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Prefix", DbType = "VarChar(50)")] string prefix, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "VendorName", DbType = "VarChar(50)")] string vendorName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ImportCurrencyDefault", DbType = "VarChar(3)")] string importCurrencyDefault, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyDefault", DbType = "VarChar(3)")] string exportCurrencyDefault, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DefaultFTP", DbType = "VarChar(100)")] string defaultFTP, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTPUserName", DbType = "VarChar(100)")] string fTPUserName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTPPassword", DbType = "VarChar(100)")] string fTPPassword, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(100)")] string loggedInUser, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsAutoPreBilling", DbType = "Bit")] System.Nullable<bool> isAutoPreBilling, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DaysBeforeBillCycle", DbType = "Int")] System.Nullable<int> daysBeforeBillCycle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsAutoPostBilling", DbType = "Bit")] System.Nullable<bool> isAutoPostBilling, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DaysAfterBillCycle", DbType = "Int")] System.Nullable<int> daysAfterBillCycle, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "OutputFileFormat", DbType = "Int")] System.Nullable<int> outputFileFormat, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingSystem", DbType = "VarChar(50)")] string BillingSystem, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AutomationFrequency", DbType = "VarChar(50)")] string AutomationFrequency) ////SERO-1582
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeID, invoiceTypeName, prefix, bAN, vendorName, importCurrencyDefault, exportCurrencyDefault, defaultFTP, fTPUserName, fTPPassword, loggedInUser, isAutoPreBilling, daysBeforeBillCycle, isAutoPostBilling, daysAfterBillCycle, outputFileFormat, BillingSystem, AutomationFrequency);
        //    return ((int)(result.ReturnValue));
        //}


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Update_InvoiceType")]
        public int Update_InvoiceType(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeID", DbType = "Int")] System.Nullable<int> invoiceTypeID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeName", DbType = "VarChar(50)")] string invoiceTypeName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Prefix", DbType = "VarChar(50)")] string prefix,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "VendorName", DbType = "VarChar(50)")] string vendorName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ImportCurrencyDefault", DbType = "VarChar(3)")] string importCurrencyDefault,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyDefault", DbType = "VarChar(3)")] string exportCurrencyDefault,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DefaultFTP", DbType = "VarChar(100)")] string defaultFTP,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTPUserName", DbType = "VarChar(100)")] string fTPUserName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTPPassword", DbType = "VarChar(100)")] string fTPPassword,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(100)")] string loggedInUser,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsAutoPreBilling", DbType = "Bit")] System.Nullable<bool> isAutoPreBilling,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DaysBeforeBillCycle", DbType = "Int")] System.Nullable<int> daysBeforeBillCycle,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsAutoPostBilling", DbType = "Bit")] System.Nullable<bool> isAutoPostBilling,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DaysAfterBillCycle", DbType = "Int")] System.Nullable<int> daysAfterBillCycle,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "OutputFileFormat", DbType = "Int")] System.Nullable<int> outputFileFormat,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsSOO", DbType = "Int")] System.Nullable<int> isSOO,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmailAddress", DbType = "VarChar(100)")] string emailAddress,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EDI", DbType = "Bit")] System.Nullable<bool> eDI,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingSystem", DbType = "VarChar(50)")] string billingSystem,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AutomationFrequency", DbType = "VarChar(50)")] string automationFrequency,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ContractNumber", DbType = "VarChar(50)")] string contractNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ContractStartDate", DbType = "Date")] DateTime contractStartDate,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ContractEndDate", DbType = "Date")] DateTime contractEndDate,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IndirectPartnerOrRepCode", DbType = "VarChar(50)")] string indirectPartnerOrRepCode,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GLDepartmentCode", DbType = "VarChar(50)")] string glDepartmentCode,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IndirectAgentRegion", DbType = "VarChar(50)")] string indirectAgentRegion)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeID, invoiceTypeName, prefix, bAN, vendorName, importCurrencyDefault, exportCurrencyDefault, defaultFTP, fTPUserName, fTPPassword, loggedInUser, isAutoPreBilling, daysBeforeBillCycle, isAutoPostBilling, daysAfterBillCycle, outputFileFormat, isSOO, emailAddress, eDI, billingSystem, automationFrequency, contractNumber
             , contractStartDate
             , contractEndDate
             , indirectPartnerOrRepCode
             , glDepartmentCode
             , indirectAgentRegion);
            return ((int)(result.ReturnValue));
        }





        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Get_InvoiceFileUploads_ByInvoiceId")]
        public ISingleResult<Get_InvoiceFileUploads_ByInvoiceIdResult> Get_InvoiceFileUploads_ByInvoiceId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<Get_InvoiceFileUploads_ByInvoiceIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceNumbersByTypeId")]
        public ISingleResult<get_InvoiceNumbersByTypeIdResult> get_InvoiceNumbersByTypeId([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<get_InvoiceNumbersByTypeIdResult>)(result.ReturnValue));
        }

        //cbe_11609
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceNumbersByTypeId_All")]
        public ISingleResult<get_InvoiceNumbersByTypeId_AllResult> get_InvoiceNumbersByTypeId_All([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<get_InvoiceNumbersByTypeId_AllResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType1_GetByInvoice_OneTypeFile_SOO_All")]
        public ISingleResult<mbm_RecordType1_GetByInvoice_OneTypeFile_SOO_AllResult> mbm_RecordType1_GetByInvoice_OneTypeFile_SOO_All([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType1_GetByInvoice_OneTypeFile_SOO_AllResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType2_GetByInvoice_OneFile_SOO_All")]
        public ISingleResult<mbm_RecordType2_GetByInvoice_OneFile_SOO_AllResult> mbm_RecordType2_GetByInvoice_OneFile_SOO_All([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType2_GetByInvoice_OneFile_SOO_AllResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RT2Summary_GetByInvoice_SOO_All")]
        public ISingleResult<mbm_RT2Summary_GetByInvoice_SOO_AllResult> mbm_RT2Summary_GetByInvoice_SOO_All([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<mbm_RT2Summary_GetByInvoice_SOO_AllResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType4_GetByInvoice_SOO_All")]
        public ISingleResult<mbm_RecordType4_GetByInvoice_SOO_AllResult> mbm_RecordType4_GetByInvoice_SOO_All([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType4_GetByInvoice_SOO_AllResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType3_GetByInvoice_SOO_All")]
        public ISingleResult<mbm_RecordType3_GetByInvoice_SOO_AllResult> mbm_RecordType3_GetByInvoice_SOO_All([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType3_GetByInvoice_SOO_AllResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceDetailsByNumber")]
        public ISingleResult<get_InvoiceDetailsByNumberResult> get_InvoiceDetailsByNumber([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<get_InvoiceDetailsByNumberResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_UserDetails")]
        public ISingleResult<get_UserDetailsResult> get_UserDetails([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string userId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userId);
            return ((ISingleResult<get_UserDetailsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.delete_InvoiceFile")]
        public int delete_InvoiceFile([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileUploadId", DbType = "Int")] System.Nullable<int> fileUploadId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fileUploadId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.delete_AssociatedUser")]
        public int delete_AssociatedUser([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iAssociatedUserId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iAssociatedUserId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.insert_AssociatedUser")]
        public int insert_AssociatedUser([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iInvoiceId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string sUserId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string sInsertedBy)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iInvoiceId, sUserId, sInsertedBy);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.search_User")]
        public ISingleResult<search_UserResult> search_User([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string sUserId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sUserId);
            return ((ISingleResult<search_UserResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.insert_Invoice")]
        public int insert_Invoice(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeID", DbType = "Int")] System.Nullable<int> invoiceTypeID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "VarChar(50)")] string invoiceNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingMonth", DbType = "Int")] System.Nullable<int> billingMonth,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingYear", DbType = "Int")] System.Nullable<int> billingYear,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DefaultImportCurrencyID", DbType = "Int")] System.Nullable<int> defaultImportCurrencyID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "VarChar(50)")] string status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LastAction", DbType = "VarChar(50)")] string lastAction,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType1_Status", DbType = "VarChar(50)")] string recordType1_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType1_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> recordType1_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType2_Status", DbType = "VarChar(50)")] string recordType2_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType2_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> recordType2_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType3_Status", DbType = "VarChar(50)")] string recordType3_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType3_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> recordType3_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType4_Status", DbType = "VarChar(50)")] string recordType4_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType4_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> recordType4_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType5_Status", DbType = "VarChar(50)")] string recordType5_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType5_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> recordType5_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingFileExport_Status", DbType = "VarChar(50)")] string billingFileExport_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingFileExport_DateTime", DbType = "DateTime")] System.Nullable<System.DateTime> billingFileExport_DateTime,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BillingFileExport_Path", DbType = "VarChar(8000)")] string billingFileExport_Path,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServiceDeskSnapshotID", DbType = "Int")] System.Nullable<int> serviceDeskSnapshotID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeID, invoiceNumber, billingMonth, billingYear, defaultImportCurrencyID, userName, status, lastAction, recordType1_Status, recordType1_DateTime, recordType2_Status, recordType2_DateTime, recordType3_Status, recordType3_DateTime, recordType4_Status, recordType4_DateTime, recordType5_Status, recordType5_DateTime, billingFileExport_Status, billingFileExport_DateTime, billingFileExport_Path, exportCurrencyID, serviceDeskSnapshotID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceFileTypes_ByInvoiceId")]
        public ISingleResult<get_InvoiceFileTypes_ByInvoiceIdResult> get_InvoiceFileTypes_ByInvoiceId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeId);
            return ((ISingleResult<get_InvoiceFileTypes_ByInvoiceIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_AssociatedUsersByInvoiceType")]
        public ISingleResult<get_AssociatedUsersByInvoiceTypeResult> get_AssociatedUsersByInvoiceType([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iInvoiceTypeID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iInvoiceTypeID);
            return ((ISingleResult<get_AssociatedUsersByInvoiceTypeResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_AssoicateFileTypesByInvoiceTypeId")]
        public ISingleResult<get_AssoicateFileTypesByInvoiceTypeIdResult> get_AssoicateFileTypesByInvoiceTypeId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeId);
            return ((ISingleResult<get_AssoicateFileTypesByInvoiceTypeIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.delete_AssoicatedFileType")]
        public int delete_AssoicatedFileType([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AssociatedFileId", DbType = "Int")] System.Nullable<int> associatedFileId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), associatedFileId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_AllFileTypesByInvoiceTypeId")]
        public ISingleResult<get_AllFileTypesByInvoiceTypeIdResult> get_AllFileTypesByInvoiceTypeId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeId);
            return ((ISingleResult<get_AllFileTypesByInvoiceTypeIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.insert_AssocaitedFiles")]
        public int insert_AssocaitedFiles([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileTypeId", DbType = "Int")] System.Nullable<int> fileTypeId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string sCreatedBy)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeId, fileTypeId, sCreatedBy);
            return ((int)(result.ReturnValue));
        }
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.delete_AssoicatedAccount")]
        public int delete_AssoicatedAccount([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AccountNumber", DbType = "bigInt")] System.Nullable<long> accountNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), accountNumber);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.insert_AssocaitedAccounts")]
        public int insert_AssocaitedAccounts(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AccountNumber", DbType = "bigInt")] long accountNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(75)")] string sAccountName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "bigInt")] System.Nullable<long> iParentAccountNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(75)")] string sParentAccountName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(100)")] string sStringIdentifier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Bit")] System.Nullable<bool> bCreateAccount,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string sInsertedBy,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iBillCycle,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] System.Nullable<System.DateTime> dtEffectiveBillDate,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ChildPays", DbType = "Bit")] System.Nullable<bool> childPays,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Password", DbType = "VarChar(50)")] string password,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTP", DbType = "VarChar(50)")] string fTP,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmailId", DbType = "VarChar(50)")] string emailId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EDI", DbType = "Bit")] System.Nullable<bool> eDI)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), accountNumber, invoiceTypeId, sAccountName, iParentAccountNumber, sParentAccountName, sStringIdentifier, bCreateAccount, sInsertedBy, iBillCycle, dtEffectiveBillDate, childPays, userName, password, fTP, emailId, eDI);
            return ((int)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.insert_AssocaitedAccounts_CSG")]
        public int insert_AssocaitedAccounts_CSG(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AccountNumber", DbType = "bigInt")] long accountNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(75)")] string sAccountName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "bigInt")] System.Nullable<long> iParentAccountNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(75)")] string sParentAccountName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(100)")] string sStringIdentifier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Bit")] System.Nullable<bool> bCreateAccount,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string sInsertedBy,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "nVarChar(30)")] string iBillCycle,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] System.Nullable<System.DateTime> dtEffectiveBillDate,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ChildPays", DbType = "Bit")] System.Nullable<bool> childPays,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Password", DbType = "VarChar(50)")] string password,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTP", DbType = "VarChar(50)")] string fTP,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmailId", DbType = "VarChar(50)")] string emailId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EDI", DbType = "Bit")] System.Nullable<bool> eDI,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SubscriberId", DbType = "int")] int csgSubscriberId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DisplayAccountNumber", DbType = "VarChar(50)")] string displayAccountNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())),
                accountNumber, invoiceTypeId, sAccountName, iParentAccountNumber, sParentAccountName, sStringIdentifier, bCreateAccount, sInsertedBy, iBillCycle, dtEffectiveBillDate, childPays, userName, password, fTP, emailId, eDI, csgSubscriberId, displayAccountNumber);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Update_AssociateAccount")]
        public int Update_AssociateAccount(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AccountNumber", DbType = "bigInt")] long accountNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(75)")] string sAccountName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "bigInt")] System.Nullable<long> iParentAccountNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(75)")] string sParentAccountName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(100)")] string sStringIdentifier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Bit")] System.Nullable<bool> bCreateAccount,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string sUpdatedBy,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iBillCycle,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] System.Nullable<System.DateTime> dtEffectiveBillDate,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ChildPays", DbType = "Bit")] System.Nullable<bool> childPays,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Password", DbType = "VarChar(50)")] string password,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTP", DbType = "VarChar(50)")] string fTP,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EMailId", DbType = "VarChar(50)")] string eMailId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EDI", DbType = "Bit")] System.Nullable<bool> eDI)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), accountNumber, invoiceTypeId, sAccountName, iParentAccountNumber, sParentAccountName, sStringIdentifier, bCreateAccount, sUpdatedBy, iBillCycle, dtEffectiveBillDate, childPays, userName, password, fTP, eMailId, eDI);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Update_AssociateAccount_CSG")]
        public int Update_AssociateAccount_CSG(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AccountNumber", DbType = "bigInt")] long accountNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(75)")] string sAccountName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "bigInt")] System.Nullable<long> iParentAccountNumber,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(75)")] string sParentAccountName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(100)")] string sStringIdentifier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Bit")] System.Nullable<bool> bCreateAccount,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string sUpdatedBy,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "nVarChar(30)")] string iBillCycle,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] System.Nullable<System.DateTime> dtEffectiveBillDate,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ChildPays", DbType = "Bit")] System.Nullable<bool> childPays,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Password", DbType = "VarChar(50)")] string password,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FTP", DbType = "VarChar(50)")] string fTP,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EMailId", DbType = "VarChar(50)")] string eMailId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EDI", DbType = "Bit")] System.Nullable<bool> eDI)
        {
            IExecuteResult result = this.ExecuteMethodCall(this
                                                                , ((MethodInfo)(MethodInfo.GetCurrentMethod()))
                                                                , accountNumber
                                                                , invoiceTypeId
                                                                , sAccountName
                                                                , iParentAccountNumber
                                                                , sParentAccountName
                                                                , sStringIdentifier
                                                                , bCreateAccount
                                                                , sUpdatedBy
                                                                , iBillCycle
                                                                , dtEffectiveBillDate
                                                                , childPays
                                                                , userName
                                                                , password
                                                                , fTP
                                                                , eMailId
                                                                , eDI);
            return ((int)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_AssoicateAccountsByInvoiceTypeId")]
        public ISingleResult<get_AssoicateAccountsByInvoiceTypeIdResult> get_AssoicateAccountsByInvoiceTypeId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeId);
            return ((ISingleResult<get_AssoicateAccountsByInvoiceTypeIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_ValidateCRMAccountNumber")]
        public ISingleResult<get_ValidateCRMAccountNumberResult> get_ValidateCRMAccountNumber([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CRMAccountNumber", DbType = "VarChar(50)")] string cRMAccountNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cRMAccountNumber);
            return ((ISingleResult<get_ValidateCRMAccountNumberResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_CSGAccountNumberInformationFromDW")]
        public ISingleResult<get_ValidateCSGAccountNumberResult> get_CSGAccountNumberInformationFromDW([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CSGAccountNumber", DbType = "VarChar(50)")] string CSGAccountNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), CSGAccountNumber);
            return ((ISingleResult<get_ValidateCSGAccountNumberResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_ValidateCSGAssoicateAccounts")]
        public int get_ValidateCSGAssoicateAccounts([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "csgAccountNumber", DbType = "bigInt")] long csgAccountNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "csgStringIdentifier", DbType = "nVarChar(25)")] string csgStringIdentifier, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ActionType", DbType = "int")] int ActionType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), csgAccountNumber, csgStringIdentifier, ActionType);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Get_validatecsgparentaccountnumber")]
        public int get_validateCsgParentAccountNumber([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "csgParentAccountNumber", DbType = "bigInt")] System.Nullable<long> csgParentAccountNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "csgChildAccountBillCycle", DbType = "nVarChar(25)")] string csgChildAccountBillCycle)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), csgParentAccountNumber, csgChildAccountBillCycle);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.insert_MBMdata_ByInvoiceIdInvoiceType")]
        public int insert_MBMdata_ByInvoiceIdInvoiceType([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeID", DbType = "Int")] System.Nullable<int> invoiceTypeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileTypeID", DbType = "Int")] System.Nullable<int> fileTypeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileName", DbType = "VarChar(250)")] string fileName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string uploadedby, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> uploadfileID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, invoiceTypeID, fileTypeID, fileName, uploadedby, uploadfileID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.insert_MBMdata_ByInvoiceIdInvoiceType_CSG")]
        public int insert_MBMdata_ByInvoiceIdInvoiceType_CSG([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeID", DbType = "Int")] System.Nullable<int> invoiceTypeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileTypeID", DbType = "Int")] System.Nullable<int> fileTypeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileName", DbType = "VarChar(250)")] string fileName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string uploadedby, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> uploadfileID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, invoiceTypeID, fileTypeID, fileName, uploadedby, uploadfileID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.delete_FileuploadbyFileID")]
        public int delete_FileuploadbyFileID([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> uploadedFileID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), uploadedFileID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.insert_MBMdatabyFile_ByInvoiceIdInvoiceType")]
        public int insert_MBMdatabyFile_ByInvoiceIdInvoiceType([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeID", DbType = "Int")] System.Nullable<int> invoiceTypeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileTypeID", DbType = "Int")] System.Nullable<int> fileTypeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileName", DbType = "VarChar(250)")] string fileName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(30)")] string uploadedby, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> uploadfileID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, invoiceTypeID, fileTypeID, fileName, uploadedby, uploadfileID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Get_MBM_ComparisonResult")]
        public ISingleResult<Get_MBM_ComparisonResultResult> Get_MBM_ComparisonResult([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId);
            return ((ISingleResult<Get_MBM_ComparisonResultResult>)(result.ReturnValue));
        }

        //SERO-1582
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Get_MBM_ComparisonResultCSG")]
        public ISingleResult<Get_MBM_ComparisonResultResultCSG> Get_MBM_ComparisonResultCSG([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId);
            return ((ISingleResult<Get_MBM_ComparisonResultResultCSG>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Insert_MBM_ComparisonResult")]
        public int Insert_MBM_ComparisonResult([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId, invoiceTypeId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Insert_MBM_ComparisonResult_CSG")]
        public int Insert_MBM_ComparisonResultCSG([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId, invoiceTypeId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.insert_ApplicationException")]
        public int insert_ApplicationException([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MethodName", DbType = "VarChar(MAX)")] string methodName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ErrorMessage", DbType = "VarChar(MAX)")] string errorMessage, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "StackTrace", DbType = "VarChar(MAX)")] string stackTrace, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(50)")] string loggedInUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), methodName, errorMessage, stackTrace, loggedInUser);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType1_GetByInvoice")]
        public ISingleResult<mbm_RecordType1_GetByInvoiceResult> mbm_RecordType1_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType1_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType1_GetExceptionsByInvoice")]
        public ISingleResult<mbm_RecordType1_GetExceptionsByInvoiceResult> mbm_RecordType1_GetExceptionsByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<mbm_RecordType1_GetExceptionsByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType2_GetByInvoice")]
        public ISingleResult<mbm_RecordType2_GetByInvoiceResult> mbm_RecordType2_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType2_GetByInvoiceResult>)(result.ReturnValue));
        }

        //cbe_8967
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType2_GetByInvoice_OneFile")]
        public ISingleResult<mbm_RecordType2_GetByInvoice_OneFileResult> mbm_RecordType2_GetByInvoice_OneFile([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType2_GetByInvoice_OneFileResult>)(result.ReturnValue));
        }

        //cbe_8967        
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType1_GetByInvoice_OneTypeFile")]
        public ISingleResult<mbm_RecordType1_GetByInvoice_OneTypeFileResult> mbm_RecordType1_GetByInvoice_OneTypeFile([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType1_GetByInvoice_OneTypeFileResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType2_GetExceptionsByInvoice")]
        public ISingleResult<mbm_RecordType2_GetExceptionsByInvoiceResult> mbm_RecordType2_GetExceptionsByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<mbm_RecordType2_GetExceptionsByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType3_GetByInvoice")]
        public ISingleResult<mbm_RecordType3_GetByInvoiceResult> mbm_RecordType3_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType3_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType4_GetByInvoice")]
        public ISingleResult<mbm_RecordType4_GetByInvoiceResult> mbm_RecordType4_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType4_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType4_GetExceptionsByInvoice")]
        public ISingleResult<mbm_RecordType4_GetExceptionsByInvoiceResult> mbm_RecordType4_GetExceptionsByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<mbm_RecordType4_GetExceptionsByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType5_GetByInvoice")]
        public ISingleResult<mbm_RecordType5_GetByInvoiceResult> mbm_RecordType5_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BAN", DbType = "VarChar(50)")] string bAN)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, bAN);
            return ((ISingleResult<mbm_RecordType5_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Update_InvoiceRecordTypes")]
        public int Update_InvoiceRecordTypes([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType1Status", DbType = "VarChar(50)")] string recordType1Status, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType2Status", DbType = "VarChar(50)")] string recordType2Status, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType3Status", DbType = "VarChar(50)")] string recordType3Status, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType4Status", DbType = "VarChar(50)")] string recordType4Status, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordType5Status", DbType = "VarChar(50)")] string recordType5Status, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LastUpdatedBy", DbType = "VarChar(50)")] string lastUpdatedBy)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, recordType1Status, recordType2Status, recordType3Status, recordType4Status, recordType5Status, lastUpdatedBy);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType1_Process")]
        public int mbm_RecordType1_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            try
            {
                IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
                return ((int)(result.ReturnValue));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_CRMInputData")]
        public int get_CRMInputData([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "VarChar(50)")] string invoiceNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(50)")] string loggedInUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceNumber, loggedInUser);
            return ((int)(result.ReturnValue));
        }

        //new 1582
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_CSGInputData")]
        public int get_CSGInputData([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "VarChar(50)")] string invoiceNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(50)")] string loggedInUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceNumber, loggedInUser);
            return ((int)(result.ReturnValue));
        }

        //end new 1582

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType2_Process")]
        public int mbm_RecordType2_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            try
            {
                IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
                return ((int)(result.ReturnValue));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType3_Process")]
        public int mbm_RecordType3_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            try
            {
                IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
                return ((int)(result.ReturnValue));
            }
            catch (SystemException ex)
            {
                throw ex;
            }
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType4_Process")]
        public int mbm_RecordType4_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            try
            {
                IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
                return ((int)(result.ReturnValue));
            }
            catch (SystemException ex)
            {
                throw ex;
            }

        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType1_ValidateInvoice")]
        public ISingleResult<mbm_RecordType1_ValidateInvoiceResult> mbm_RecordType1_ValidateInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<mbm_RecordType1_ValidateInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType2_ValidateInvoice")]
        public ISingleResult<mbm_RecordType2_ValidateInvoiceResult> mbm_RecordType2_ValidateInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<mbm_RecordType2_ValidateInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType4_ValidateInvoice")]
        public ISingleResult<mbm_RecordType4_ValidateInvoiceResult> mbm_RecordType4_ValidateInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<mbm_RecordType4_ValidateInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RecordType5_Process")]
        public ISingleResult<mbm_RecordType5_ProcessResult> mbm_RecordType5_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string userName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportCurrencyID", DbType = "Int")] System.Nullable<int> exportCurrencyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID, userName, exportCurrencyID);
            return ((ISingleResult<mbm_RecordType5_ProcessResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceType")]
        public ISingleResult<get_InvoiceTypeResult> get_InvoiceType()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<get_InvoiceTypeResult>)(result.ReturnValue));
        }

        //SERO-1582

        //Sero-3511 start
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_AscendonGLDepartmentCodes")]
        public ISingleResult<Get_AscendonGLDepartmentCodesResult> Get_MBMGetAscendonGLDepartmentCodes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<Get_AscendonGLDepartmentCodesResult>)(result.ReturnValue));
        }
        //Sero-3511 end

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_BillingSystem")]
        public ISingleResult<get_InvoiceTypeResult> GetBillingSystem([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<get_InvoiceTypeResult>)(result.ReturnValue));
        }

        //cbe_9179
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceType_SOO")]
        public ISingleResult<get_InvoiceType_SOOResult> get_InvoiceType_SOO()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<get_InvoiceType_SOOResult>)(result.ReturnValue));
        }

        //cbe_11609
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceType_SOOReport")]
        public ISingleResult<get_InvoiceType_SOOReportResult> get_InvoiceType_SOOReport()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<get_InvoiceType_SOOReportResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_AllAssoicateAccounts")]
        public ISingleResult<get_AllAssoicateAccountsResult> get_AllAssoicateAccounts()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<get_AllAssoicateAccountsResult>)(result.ReturnValue));
        }

        //seratt april
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_AllCSGServiceAttributes")]
        public ISingleResult<get_AllCSGServiceAttributesResult> get_AllCSGServiceAttributes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<get_AllCSGServiceAttributesResult>)(result.ReturnValue));
        }

        //1582
        //seratt april
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_AllCSGServiceFeatures")]
        public ISingleResult<get_AllCSGServiceFeaturesResult> get_AllCSGServiceFeatures([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId);
            return ((ISingleResult<get_AllCSGServiceFeaturesResult>)(result.ReturnValue));
        }

        //end 1582

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_UnextractedCUCDMData")]
        public ISingleResult<get_UnextractedCUCDMDataResult> get_UnextractedCUCDMData()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<get_UnextractedCUCDMDataResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_FetchCRMDataFromUI")]
        public int mbm_FetchCRMDataFromUI([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "VarChar(50)")] string invoiceNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(50)")] string loggedInUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceNumber, loggedInUser);
            return ((int)(result.ReturnValue));
        }

        //1582-2
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_FetchCSGDataFromUI")]
        public int mbm_FetchCSGDataFromUI([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "VarChar(50)")] string invoiceNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "LoggedInUser", DbType = "VarChar(50)")] string loggedInUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceNumber, loggedInUser);
            return ((int)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_ProcessWorkFlowStatus")]
        public ISingleResult<get_ProcessWorkFlowStatusResult> get_ProcessWorkFlowStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "VarChar(50)")] string invoiceNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceNumber);
            return ((ISingleResult<get_ProcessWorkFlowStatusResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.update_ProcessWorkFlowStatus")]
        public int update_ProcessWorkFlowStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "VarChar(50)")] string invoiceNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CompareToCRM", DbType = "Bit")] System.Nullable<bool> compareToCRM, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ViewChange", DbType = "Bit")] System.Nullable<bool> viewChange, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ApproveChange", DbType = "Bit")] System.Nullable<bool> approveChange, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SyncCRM", DbType = "Bit")] System.Nullable<bool> syncCRM, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ImportCRMData", DbType = "Bit")] System.Nullable<bool> importCRMData, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProcessInvoice", DbType = "Bit")] System.Nullable<bool> processInvoice, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ExportInvoiceFile", DbType = "Bit")] System.Nullable<bool> exportInvoiceFile)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceNumber, compareToCRM, viewChange, approveChange, syncCRM, importCRMData, processInvoice, exportInvoiceFile);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceFileExports")]
        public ISingleResult<get_InvoiceFileExportsResult> get_InvoiceFileExports([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<get_InvoiceFileExportsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.insert_InvoiceFileExports")]
        public int insert_InvoiceFileExports([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iInvoiceId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(100)")] string sExportFileName, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(100)")] string sExportFilePath, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(100)")] string sExportedBy)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iInvoiceId, sExportFileName, sExportFilePath, sExportedBy);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Update_MBM_ComparisonResult_CSG")]
        public int Update_MBM_ComparisonResult_CSG([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> InvoiceId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "iSnapshotId", DbType = "bigInt")] long iSnapshotId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "iLoadId", DbType = "Int")] System.Nullable<int> iLoadId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "sSubscriberId", DbType = "VARCHAR(50)")] string sSubscriberId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "iProcessFirst", DbType = "Bit")] bool iProcessFirst)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), InvoiceId, iSnapshotId, iLoadId, sSubscriberId, iProcessFirst);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Update_MBM_ComparisonResult")]
        public int Update_MBM_ComparisonResult([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId);
            return ((int)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Get_UnprocessedMBMComparisonResult")]
        public ISingleResult<Get_UnprocessedMBMComparisonResultResult> Get_UnprocessedMBMComparisonResult([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId);
            return ((ISingleResult<Get_UnprocessedMBMComparisonResultResult>)(result.ReturnValue));
        }

        //1582
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Get_UnprocessedMBMComparisonResultCSG")]
        public ISingleResult<Get_UnprocessedMBMComparisonResultResultCSG> Get_UnprocessedMBMComparisonResultCSG([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId);
            return ((ISingleResult<Get_UnprocessedMBMComparisonResultResultCSG>)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ResetPreBillingActivityStatus")]
        public int mbm_ResetPreBillingActivityStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceNumber", DbType = "VarChar(50)")] string invoiceNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceNumber);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Update_InvoiceExportStatus")]
        public int Update_InvoiceExportStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_Profiles_GetByInvoiceId")]
        public ISingleResult<mbm_Profiles_GetByInvoiceIdResult> mbm_Profiles_GetByInvoiceId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceType", DbType = "Int")] System.Nullable<int> invoiceType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceType);
            return ((ISingleResult<mbm_Profiles_GetByInvoiceIdResult>)(result.ReturnValue));
        }

        //SERO-1582

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ChargeStringIdentifier_GetByInvoiceId")]
        public ISingleResult<mbm_ChargeStringIdentifier_GetByInvoiceIdResult> mbm_ChargeStringIdentifier_GetByInvoiceId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceType", DbType = "Int")] System.Nullable<int> invoiceType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceType);
            return ((ISingleResult<mbm_ChargeStringIdentifier_GetByInvoiceIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ProfileCharges_GetByInvoice")]
        public ISingleResult<mbm_ProfileCharges_GetByInvoiceResult> mbm_ProfileCharges_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceTypeId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(50)")] string invoiceTypeName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeId, invoiceTypeName);
            return ((ISingleResult<mbm_ProfileCharges_GetByInvoiceResult>)(result.ReturnValue));
        }
        //Get profileCharges for CSG
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ProfileCharges_GetByInvoice_CSG")]
        public ISingleResult<mbm_ProfileCharges_GetByInvoice_CSGResult> mbm_ProfileCharges_GetByInvoice_CSG([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceTypeId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(50)")] string invoiceTypeName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeId, invoiceTypeName);
            return ((ISingleResult<mbm_ProfileCharges_GetByInvoice_CSGResult>)(result.ReturnValue));
        }
        //end PC for CSG

        //SERO-1582

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_CSGPricingPlanId_GetByInvoice")]
        public ISingleResult<mbm_CSGPricingPlanId_GetByInvoiceResult> mbm_CSGPricingPlanId_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceTypeId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(50)")] string invoiceTypeName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeId, invoiceTypeName);
            return ((ISingleResult<mbm_CSGPricingPlanId_GetByInvoiceResult>)(result.ReturnValue));
        }



        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_Profiles_Create")]
        public int mbm_Profiles_Create([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileName", DbType = "VarChar(100)")] string profileName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceType", DbType = "Int")] System.Nullable<int> invoiceType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileDescription", DbType = "VarChar(250)")] string profileDescription)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profileName, invoiceType, profileDescription);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_Profiles_DeleteById")]
        public int mbm_Profiles_DeleteById([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileId", DbType = "Int")] System.Nullable<int> profileId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profileId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_Profiles_Edit")]
        public int mbm_Profiles_Edit([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileId", DbType = "Int")] System.Nullable<int> profileId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileName", DbType = "VarChar(100)")] string profileName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileDescription", DbType = "VarChar(250)")] string profileDescription)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profileId, profileName, profileDescription);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_Profiles_VerifyInEnhanceData")]
        public int mbm_Profiles_VerifyInEnhanceData([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> profileId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profileId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ProfileCharges_DeleteByChargeId")]
        public int mbm_ProfileCharges_DeleteByChargeId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileChargeId", DbType = "Int")] System.Nullable<int> profileChargeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profileChargeId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ProfileCharges_CSG_DeleteByChargeId")]
        public int mbm_ProfileCharges_CSG_DeleteByChargeId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileChargeId", DbType = "Int")] System.Nullable<int> profileChargeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profileChargeId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_CSGProfileCharges_VerifyInEnhanceData")]
        public int mbm_CSGProfileCharges_VerifyInEnhanceDataByChargeId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "chargeId", DbType = "Int")] System.Nullable<int> chargeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), chargeId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ProfileCharges_EditByChargeId")]
        public int mbm_ProfileCharges_EditByChargeId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileChargeId", DbType = "Int")] System.Nullable<int> profileChargeId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ChargeAmount", DbType = "Decimal(18,2)")] System.Nullable<decimal> chargeAmount)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profileChargeId, chargeAmount);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ProfileCharges_CSG_EditByChargeId")]
        public int mbm_ProfileCharges_CSG_EditByChargeId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileId", DbType = "Int")] System.Nullable<int> profileId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceTypeId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CatalogItemId", DbType = "Int")] System.Nullable<int> catalogItemId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Charge", DbType = "Decimal(18,2)")] System.Nullable<decimal> Charge)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profileId, invoiceTypeId, catalogItemId, Charge);
            return ((int)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ProfileCharges_VerifyInEnhanceData")]
        public int mbm_ProfileCharges_VerifyInEnhanceData([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> chargeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), chargeId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ManualChargeFile_Create")]
        public int mbm_ManualChargeFile_Create([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeId", DbType = "Int")] System.Nullable<int> invoiceTypeId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileName", DbType = "VarChar(500)")] string fileName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileStatus", DbType = "VarChar(50)")] string fileStatus, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ImportedBy", DbType = "VarChar(50)")] string importedBy, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> outuploadedFileID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeId, fileName, fileStatus, importedBy, outuploadedFileID);
            outuploadedFileID = ((System.Nullable<int>)(result.GetParameterValue(4)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ProfileCharges_ValidateFromCRM")]
        public ISingleResult<mbm_ProfileCharges_ValidateFromCRMResult> mbm_ProfileCharges_ValidateFromCRM([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(50)")] string iGLCode)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iGLCode);
            return ((ISingleResult<mbm_ProfileCharges_ValidateFromCRMResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ProfileCharges_Create")]
        public int mbm_ProfileCharges_Create([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileId", DbType = "Int")] System.Nullable<int> profileId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ChargeCode", DbType = "VarChar(30)")] string chargeCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ChargeAmount", DbType = "Decimal(18,2)")] System.Nullable<decimal> chargeAmount, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ChargeType", DbType = "VarChar(50)")] string chargeType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Feature", DbType = "VarChar(50)")] string feature, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceTypeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profileId, chargeCode, chargeAmount, chargeType, feature, invoiceTypeId);
            return ((int)(result.ReturnValue));
        }

        //PC
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ProfileCharges_Create_CSG")]
        public int mbm_ProfileCharges_Create_CSG([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ProfileId", DbType = "Int")] System.Nullable<int> profileId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceTypeId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CatalogItemId", DbType = "Int")] System.Nullable<int> catalogItemId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Charge", DbType = "Decimal(18,2)")] System.Nullable<decimal> Charge)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), profileId, invoiceTypeId, catalogItemId, Charge);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ManualCharge_GetFilesInfoByInvoiceId")]
        public ISingleResult<mbm_ManualCharge_GetFilesInfoByInvoiceIdResult> mbm_ManualCharge_GetFilesInfoByInvoiceId([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceTypeId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeId);
            return ((ISingleResult<mbm_ManualCharge_GetFilesInfoByInvoiceIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ManualCharge_DeleteByFileId")]
        public int mbm_ManualCharge_DeleteByFileId([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ManualChargeFileId", DbType = "Int")] System.Nullable<int> manualChargeFileId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), manualChargeFileId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ManualCharge_GetChargesByFileId")]
        public ISingleResult<mbm_ManualCharge_GetChargesByFileIdResult> mbm_ManualCharge_GetChargesByFileId([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> fileId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fileId);
            return ((ISingleResult<mbm_ManualCharge_GetChargesByFileIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ManualCharge_ProcessData")]
        public int mbm_ManualCharge_ProcessData([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileId", DbType = "Int")] System.Nullable<int> fileId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fileId);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ManualCharge_getSuccessRecordsByInvoiceId")]
        public ISingleResult<mbm_ManualCharge_getSuccessRecordsByInvoiceIdResult> mbm_ManualCharge_getSuccessRecordsByInvoiceId([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId);
            return ((ISingleResult<mbm_ManualCharge_getSuccessRecordsByInvoiceIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ManualCharge_GetFileProcessSummary")]
        public int mbm_ManualCharge_GetFileProcessSummary([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> iFileId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> successrecords, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] ref System.Nullable<int> failurerecords)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iFileId, successrecords, failurerecords);
            successrecords = ((System.Nullable<int>)(result.GetParameterValue(1)));
            failurerecords = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_ManualCharge_updateFileStatus")]
        public int mbm_ManualCharge_updateFileStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileId", DbType = "Int")] System.Nullable<int> fileId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FileStatus", DbType = "VarChar(50)")] string fileStatus)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fileId, fileStatus);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RT1Summary_GetByInvoice")]
        public ISingleResult<mbm_RT1Summary_GetByInvoiceResult> mbm_RT1Summary_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<mbm_RT1Summary_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RT1Summary_Process")]
        public int mbm_RT1Summary_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RT2Summary_GetByInvoice")]
        public ISingleResult<mbm_RT2Summary_GetByInvoiceResult> mbm_RT2Summary_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<mbm_RT2Summary_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RT2Summary_Process")]
        public int mbm_RT2Summary_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RT4Summary_GetByInvoice")]
        public ISingleResult<mbm_RT4Summary_GetByInvoiceResult> mbm_RT4Summary_GetByInvoice([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<mbm_RT4Summary_GetByInvoiceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.mbm_RT4Summary_Process")]
        public int mbm_RT4Summary_Process([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceID", DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_EHCSRawData")]
        public ISingleResult<get_EHCSRawDataResult> get_EHCSRawData([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "StartDate", DbType = "VarChar(50)")] string startDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EndDate", DbType = "VarChar(50)")] string endDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Customer", DbType = "VarChar(50)")] string customer)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), startDate, endDate, customer);
            return ((ISingleResult<get_EHCSRawDataResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_EHCSEhancedData")]
        public ISingleResult<get_EHCSEhancedDataResult> get_EHCSEhancedData([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId);
            return ((ISingleResult<get_EHCSEhancedDataResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_EHCSUnEhancedData")]
        public ISingleResult<get_EHCSUnEhancedDataResult> get_EHCSUnEhancedData([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId);
            return ((ISingleResult<get_EHCSUnEhancedDataResult>)(result.ReturnValue));
        }

        //cbe9941        
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_SOOFixedChargesData")]
        public ISingleResult<get_SOOFixedChargesDataResult> get_SOOFixedChargesData([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "StartDate", DbType = "VarChar(50)")] string startDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EndDate", DbType = "VarChar(50)")] string endDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Customer", DbType = "VarChar(50)")] string customer)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), startDate, endDate, customer);
            return ((ISingleResult<get_SOOFixedChargesDataResult>)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_EDIReportData")]
        public ISingleResult<get_EDIReportDataResult> get_EDIReportData()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<get_EDIReportDataResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.Get_MBMAutomateStatusByInvoiceId")]
        public ISingleResult<Get_MBMAutomateStatusByInvoiceIdResult> Get_MBMAutomateStatusByInvoiceId([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> invoiceID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceID);
            return ((ISingleResult<Get_MBMAutomateStatusByInvoiceIdResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.update_MBMAutomateStatus")]
        public int update_MBMAutomateStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceId", DbType = "Int")] System.Nullable<int> invoiceId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Bit")] System.Nullable<bool> value, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "StatusOf", DbType = "VarChar(20)")] string statusOf)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceId, value, statusOf);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_ValidateEffectiveDate")]
        public ISingleResult<get_ValidateEffectiveDateResult> get_ValidateEffectiveDate([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeID", DbType = "Int")] System.Nullable<int> invoiceTypeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CRMAccountNumber", DbType = "VarChar(50)")] string cRMAccountNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EffecttiveDate", DbType = "DateTime")] System.Nullable<System.DateTime> effecttiveDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeID, cRMAccountNumber, effecttiveDate);
            return ((ISingleResult<get_ValidateEffectiveDateResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_AccountEffectiveDate_Status")]
        public ISingleResult<get_AccountEffectiveDate_StatusResult> get_AccountEffectiveDate_Status([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InvoiceTypeID", DbType = "Int")] System.Nullable<int> invoiceTypeID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CRMAccountNumber", DbType = "VarChar(50)")] string cRMAccountNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), invoiceTypeID, cRMAccountNumber);
            return ((ISingleResult<get_AccountEffectiveDate_StatusResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceTypesbyAssociatedUserSOO")]
        public ISingleResult<get_InvoiceTypesbyAssociatedUserSOOResult> get_InvoiceTypesbyAssociatedUserSOO([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(100)")] string sAssociatedUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sAssociatedUser);
            return ((ISingleResult<get_InvoiceTypesbyAssociatedUserSOOResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceTypesbyAssociatedUser")]
        public ISingleResult<get_InvoiceTypesbyAssociatedUserResult> get_InvoiceTypesbyAssociatedUser([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(100)")] string sAssociatedUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sAssociatedUser);
            return ((ISingleResult<get_InvoiceTypesbyAssociatedUserResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.get_InvoiceTypesbyAssociatedUser_SOOReport")]
        public ISingleResult<get_InvoiceTypesbyAssociatedUser_SOOReportResult> get_InvoiceTypesbyAssociatedUser_SOOReport([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(100)")] string sAssociatedUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sAssociatedUser);
            return ((ISingleResult<get_InvoiceTypesbyAssociatedUser_SOOReportResult>)(result.ReturnValue));
        }


        #endregion
    }

    #region Start Sero3511
    public partial class Get_AscendonGLDepartmentCodesResult
    {
        private int _iGLDepartmentID;
        private string _sGLDepartmentCode;
        private string _sGLDepartmentName;
        private string _sGLDepartmentValue;


        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iGLDepartmentID", DbType = "int")]
        public int iGLDepartmentID
        {
            get
            {
                return this._iGLDepartmentID;
            }
            set
            {
                if ((this._iGLDepartmentID != value))
                {
                    this._iGLDepartmentID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sGLDepartmentCode", DbType = "nVarchar(50)")]
        public string sGLDepartmentCode
        {
            get
            {
                return this._sGLDepartmentCode;
            }
            set
            {
                if ((this._sGLDepartmentCode != value))
                {
                    this._sGLDepartmentCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sGLDepartmentName", DbType = "nVarchar(50)")]
        public string sGLDepartmentName
        {
            get
            {
                return this._sGLDepartmentName;
            }
            set
            {
                if ((this._sGLDepartmentName != value))
                {
                    this._sGLDepartmentName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sGLDepartmentValue", DbType = "nVarchar(50)")]
        public string sGLDepartmentValue
        {
            get
            {
                return this._sGLDepartmentValue;
            }
            set
            {
                if ((this._sGLDepartmentValue != value))
                {
                    this._sGLDepartmentValue = value;
                }
            }
        }
    }
    #endregion End Sero3511

    public partial class Get_MBM_ComparisonResultResult
    {

        private System.Nullable<int> _iSnapshotId;

        private System.Nullable<int> _iInvoiceId;

        private string _sCRMAccountNumber;

        private string _sAssetSearchCode1;

        private string _sAssetSearchCode2;

        private System.Nullable<decimal> _mCharge;

        private int _iActionType;

        private string _sProfileName;

        private System.Nullable<int> _iGLCode;

        private string _sFeatureCode;

        private System.Nullable<System.DateTime> _dtMainStart;

        private System.Nullable<System.DateTime> _dtMainEnd;

        private string _sItemType;

        private string _sSubType;

        private string _sSwitchId;

        private string _sItemId;

        private string _sSequenceId;

        private System.Nullable<int> _iLoadId;

        private System.Nullable<int> _iProcessed;

        public Get_MBM_ComparisonResultResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iSnapshotId", DbType = "Int")]
        public System.Nullable<int> iSnapshotId
        {
            get
            {
                return this._iSnapshotId;
            }
            set
            {
                if ((this._iSnapshotId != value))
                {
                    this._iSnapshotId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceId", DbType = "Int")]
        public System.Nullable<int> iInvoiceId
        {
            get
            {
                return this._iInvoiceId;
            }
            set
            {
                if ((this._iInvoiceId != value))
                {
                    this._iInvoiceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sCRMAccountNumber", DbType = "VarChar(13)")]
        public string sCRMAccountNumber
        {
            get
            {
                return this._sCRMAccountNumber;
            }
            set
            {
                if ((this._sCRMAccountNumber != value))
                {
                    this._sCRMAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode1", DbType = "VarChar(50)")]
        public string sAssetSearchCode1
        {
            get
            {
                return this._sAssetSearchCode1;
            }
            set
            {
                if ((this._sAssetSearchCode1 != value))
                {
                    this._sAssetSearchCode1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode2", DbType = "VarChar(50)")]
        public string sAssetSearchCode2
        {
            get
            {
                return this._sAssetSearchCode2;
            }
            set
            {
                if ((this._sAssetSearchCode2 != value))
                {
                    this._sAssetSearchCode2 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mCharge", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> mCharge
        {
            get
            {
                return this._mCharge;
            }
            set
            {
                if ((this._mCharge != value))
                {
                    this._mCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iActionType", DbType = "Int NOT NULL")]
        public int iActionType
        {
            get
            {
                return this._iActionType;
            }
            set
            {
                if ((this._iActionType != value))
                {
                    this._iActionType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sProfileName", DbType = "VarChar(100)")]
        public string sProfileName
        {
            get
            {
                return this._sProfileName;
            }
            set
            {
                if ((this._sProfileName != value))
                {
                    this._sProfileName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iGLCode", DbType = "Int")]
        public System.Nullable<int> iGLCode
        {
            get
            {
                return this._iGLCode;
            }
            set
            {
                if ((this._iGLCode != value))
                {
                    this._iGLCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFeatureCode", DbType = "VarChar(50)")]
        public string sFeatureCode
        {
            get
            {
                return this._sFeatureCode;
            }
            set
            {
                if ((this._sFeatureCode != value))
                {
                    this._sFeatureCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainStart
        {
            get
            {
                return this._dtMainStart;
            }
            set
            {
                if ((this._dtMainStart != value))
                {
                    this._dtMainStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainEnd
        {
            get
            {
                return this._dtMainEnd;
            }
            set
            {
                if ((this._dtMainEnd != value))
                {
                    this._dtMainEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sItemType", DbType = "VarChar(10)")]
        public string sItemType
        {
            get
            {
                return this._sItemType;
            }
            set
            {
                if ((this._sItemType != value))
                {
                    this._sItemType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSubType", DbType = "VarChar(10)")]
        public string sSubType
        {
            get
            {
                return this._sSubType;
            }
            set
            {
                if ((this._sSubType != value))
                {
                    this._sSubType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSwitchId", DbType = "VarChar(20)")]
        public string sSwitchId
        {
            get
            {
                return this._sSwitchId;
            }
            set
            {
                if ((this._sSwitchId != value))
                {
                    this._sSwitchId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sItemId", DbType = "VarChar(20)")]
        public string sItemId
        {
            get
            {
                return this._sItemId;
            }
            set
            {
                if ((this._sItemId != value))
                {
                    this._sItemId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSequenceId", DbType = "VarChar(20)")]
        public string sSequenceId
        {
            get
            {
                return this._sSequenceId;
            }
            set
            {
                if ((this._sSequenceId != value))
                {
                    this._sSequenceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iLoadId", DbType = "Int")]
        public System.Nullable<int> iLoadId
        {
            get
            {
                return this._iLoadId;
            }
            set
            {
                if ((this._iLoadId != value))
                {
                    this._iLoadId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iProcessed", DbType = "Int")]
        public System.Nullable<int> iProcessed
        {
            get
            {
                return this._iProcessed;
            }
            set
            {
                if ((this._iProcessed != value))
                {
                    this._iProcessed = value;
                }
            }
        }
    }

    //SERO-1582

    public partial class Get_MBM_ComparisonResultResultCSG
    {

        private System.Nullable<int> _iSnapshotId;

        private System.Nullable<int> _iInvoiceId;

        private string _sCSGAccountNumber;

        private string _sAssetSearchCode1;

        private string _sAssetSearchCode2;

        private System.Nullable<decimal> _mCharge;

        private int _iActionType;

        private string _sProfileName;

        //private System.Nullable<int> _iGLCode;

        //private string _sFeatureCode;
        private string _sOfferExternalRef;
        private string _sProductExternalRef;
        private string _sPricingPlanExternalRef;
        private string _sMBMUniqueID;

        private System.Nullable<System.DateTime> _dtMainStart;

        private System.Nullable<System.DateTime> _dtMainEnd;

        private string _sItemType;

        private string _sSubType;

        private string _sSwitchId;

        private string _sItemId;

        private string _sSequenceId;

        private System.Nullable<int> _iLoadId;

        private System.Nullable<int> _iProcessed;
        private string _sSubscriberId;

        public Get_MBM_ComparisonResultResultCSG()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iSnapshotId", DbType = "Int")]
        public System.Nullable<int> iSnapshotId
        {
            get
            {
                return this._iSnapshotId;
            }
            set
            {
                if ((this._iSnapshotId != value))
                {
                    this._iSnapshotId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceId", DbType = "Int")]
        public System.Nullable<int> iInvoiceId
        {
            get
            {
                return this._iInvoiceId;
            }
            set
            {
                if ((this._iInvoiceId != value))
                {
                    this._iInvoiceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sCSGAccountNumber", DbType = "VarChar(13)")]
        public string sCSGAccountNumber
        {
            get
            {
                return this._sCSGAccountNumber;
            }
            set
            {
                if ((this._sCSGAccountNumber != value))
                {
                    this._sCSGAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSubscriberId", DbType = "VarChar(13)")]
        public string sSubscriberId
        {
            get
            {
                return this._sSubscriberId;
            }
            set
            {
                if ((this._sSubscriberId != value))
                {
                    this._sSubscriberId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode1", DbType = "VarChar(50)")]
        public string sAssetSearchCode1
        {
            get
            {
                return this._sAssetSearchCode1;
            }
            set
            {
                if ((this._sAssetSearchCode1 != value))
                {
                    this._sAssetSearchCode1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode2", DbType = "VarChar(50)")]
        public string sAssetSearchCode2
        {
            get
            {
                return this._sAssetSearchCode2;
            }
            set
            {
                if ((this._sAssetSearchCode2 != value))
                {
                    this._sAssetSearchCode2 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mCharge", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> mCharge
        {
            get
            {
                return this._mCharge;
            }
            set
            {
                if ((this._mCharge != value))
                {
                    this._mCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iActionType", DbType = "Int NOT NULL")]
        public int iActionType
        {
            get
            {
                return this._iActionType;
            }
            set
            {
                if ((this._iActionType != value))
                {
                    this._iActionType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sProfileName", DbType = "VarChar(100)")]
        public string sProfileName
        {
            get
            {
                return this._sProfileName;
            }
            set
            {
                if ((this._sProfileName != value))
                {
                    this._sProfileName = value;
                }
            }
        }

        //23032022
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sOfferExternalRef", DbType = "VarChar(50)")]
        public string sOfferExternalRef
        {
            get
            {
                return this._sOfferExternalRef;
            }
            set
            {
                if ((this._sOfferExternalRef != value))
                {
                    this._sOfferExternalRef = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sProductExternalRef", DbType = "VarChar(50)")]
        public string sProductExternalRef
        {
            get
            {
                return this._sProductExternalRef;
            }
            set
            {
                if ((this._sProductExternalRef != value))
                {
                    this._sProductExternalRef = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPricingPlanExternalRef", DbType = "VarChar(50)")]
        public string sPricingPlanExternalRef
        {
            get
            {
                return this._sPricingPlanExternalRef;
            }
            set
            {
                if ((this._sPricingPlanExternalRef != value))
                {
                    this._sPricingPlanExternalRef = value;
                }
            }
        }


        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sMBMUniqueID", DbType = "VarChar(50)")]
        public string sMBMUniqueID
        {
            get
            {
                return this._sMBMUniqueID;
            }
            set
            {
                if ((this._sMBMUniqueID != value))
                {
                    this._sMBMUniqueID = value;
                }
            }
        }
        //end

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainStart
        {
            get
            {
                return this._dtMainStart;
            }
            set
            {
                if ((this._dtMainStart != value))
                {
                    this._dtMainStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainEnd
        {
            get
            {
                return this._dtMainEnd;
            }
            set
            {
                if ((this._dtMainEnd != value))
                {
                    this._dtMainEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sItemType", DbType = "VarChar(10)")]
        public string sItemType
        {
            get
            {
                return this._sItemType;
            }
            set
            {
                if ((this._sItemType != value))
                {
                    this._sItemType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSubType", DbType = "VarChar(10)")]
        public string sSubType
        {
            get
            {
                return this._sSubType;
            }
            set
            {
                if ((this._sSubType != value))
                {
                    this._sSubType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSwitchId", DbType = "VarChar(20)")]
        public string sSwitchId
        {
            get
            {
                return this._sSwitchId;
            }
            set
            {
                if ((this._sSwitchId != value))
                {
                    this._sSwitchId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sItemId", DbType = "VarChar(20)")]
        public string sItemId
        {
            get
            {
                return this._sItemId;
            }
            set
            {
                if ((this._sItemId != value))
                {
                    this._sItemId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSequenceId", DbType = "VarChar(20)")]
        public string sSequenceId
        {
            get
            {
                return this._sSequenceId;
            }
            set
            {
                if ((this._sSequenceId != value))
                {
                    this._sSequenceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iLoadId", DbType = "Int")]
        public System.Nullable<int> iLoadId
        {
            get
            {
                return this._iLoadId;
            }
            set
            {
                if ((this._iLoadId != value))
                {
                    this._iLoadId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iProcessed", DbType = "Int")]
        public System.Nullable<int> iProcessed
        {
            get
            {
                return this._iProcessed;
            }
            set
            {
                if ((this._iProcessed != value))
                {
                    this._iProcessed = value;
                }
            }
        }
    }

    public partial class get_UnextractedCUCDMDataResult
    {

        private int _id;

        private int _snapshot_id;

        private string _subidentifier;

        private string _first_name;

        private string _last_name;

        private string _asset_search;

        private string _service_profile_id;

        private System.Nullable<System.DateTime> _service_start_date;

        private System.Nullable<System.DateTime> _service_end_date;

        private string _legal_entity;

        private string _state;

        private System.Nullable<int> _postal_code;

        private string _country;

        private string _directory_number;

        private string _service_profile_uid;

        public get_UnextractedCUCDMDataResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_id", DbType = "Int NOT NULL")]
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                if ((this._id != value))
                {
                    this._id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_snapshot_id", DbType = "Int NOT NULL")]
        public int snapshot_id
        {
            get
            {
                return this._snapshot_id;
            }
            set
            {
                if ((this._snapshot_id != value))
                {
                    this._snapshot_id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_subidentifier", DbType = "VarChar(200)")]
        public string subidentifier
        {
            get
            {
                return this._subidentifier;
            }
            set
            {
                if ((this._subidentifier != value))
                {
                    this._subidentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_first_name", DbType = "VarChar(50)")]
        public string first_name
        {
            get
            {
                return this._first_name;
            }
            set
            {
                if ((this._first_name != value))
                {
                    this._first_name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_last_name", DbType = "VarChar(50)")]
        public string last_name
        {
            get
            {
                return this._last_name;
            }
            set
            {
                if ((this._last_name != value))
                {
                    this._last_name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_asset_search", DbType = "VarChar(200)")]
        public string asset_search
        {
            get
            {
                return this._asset_search;
            }
            set
            {
                if ((this._asset_search != value))
                {
                    this._asset_search = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_service_profile_id", DbType = "VarChar(200) NOT NULL", CanBeNull = false)]
        public string service_profile_id
        {
            get
            {
                return this._service_profile_id;
            }
            set
            {
                if ((this._service_profile_id != value))
                {
                    this._service_profile_id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_service_start_date", DbType = "Date")]
        public System.Nullable<System.DateTime> service_start_date
        {
            get
            {
                return this._service_start_date;
            }
            set
            {
                if ((this._service_start_date != value))
                {
                    this._service_start_date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_service_end_date", DbType = "Date")]
        public System.Nullable<System.DateTime> service_end_date
        {
            get
            {
                return this._service_end_date;
            }
            set
            {
                if ((this._service_end_date != value))
                {
                    this._service_end_date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_legal_entity", DbType = "VarChar(300) NOT NULL", CanBeNull = false)]
        public string legal_entity
        {
            get
            {
                return this._legal_entity;
            }
            set
            {
                if ((this._legal_entity != value))
                {
                    this._legal_entity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_state", DbType = "VarChar(10)")]
        public string state
        {
            get
            {
                return this._state;
            }
            set
            {
                if ((this._state != value))
                {
                    this._state = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_postal_code", DbType = "Int")]
        public System.Nullable<int> postal_code
        {
            get
            {
                return this._postal_code;
            }
            set
            {
                if ((this._postal_code != value))
                {
                    this._postal_code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_country", DbType = "VarChar(50)")]
        public string country
        {
            get
            {
                return this._country;
            }
            set
            {
                if ((this._country != value))
                {
                    this._country = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_directory_number", DbType = "VarChar(20)")]
        public string directory_number
        {
            get
            {
                return this._directory_number;
            }
            set
            {
                if ((this._directory_number != value))
                {
                    this._directory_number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_service_profile_uid", DbType = "VarChar(20)")]
        public string mac_address
        {
            get
            {
                return this._service_profile_uid;
            }
            set
            {
                if ((this._service_profile_uid != value))
                {
                    this._service_profile_uid = value;
                }
            }
        }
    }

    public partial class get_AllAssoicateAccountsResult
    {

        private string _sInvoiceTypeName;

        private string _sPrefix;

        private long _iAccountNumber;

        private string _sStringIdentifier;

        public get_AllAssoicateAccountsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceTypeName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sInvoiceTypeName
        {
            get
            {
                return this._sInvoiceTypeName;
            }
            set
            {
                if ((this._sInvoiceTypeName != value))
                {
                    this._sInvoiceTypeName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPrefix", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sPrefix
        {
            get
            {
                return this._sPrefix;
            }
            set
            {
                if ((this._sPrefix != value))
                {
                    this._sPrefix = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iAccountNumber", DbType = "bigInt NOT NULL")]
        public long iAccountNumber
        {
            get
            {
                return this._iAccountNumber;
            }
            set
            {
                if ((this._iAccountNumber != value))
                {
                    this._iAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sStringIdentifier", DbType = "VarChar(100)")]
        public string sStringIdentifier
        {
            get
            {
                return this._sStringIdentifier;
            }
            set
            {
                if ((this._sStringIdentifier != value))
                {
                    this._sStringIdentifier = value;
                }
            }
        }
    }

    //servatt april 1582
    public partial class get_AllCSGServiceAttributesResult
    {
        private string _sServiceAttributeExternalRef;

        private string _sAttributeType;

        private string _sServiceAttributeValue;

        public get_AllCSGServiceAttributesResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sServiceAttributeExternalRef", DbType = "VarChar(100)")]
        public string sServiceAttributeExternalRef
        {
            get
            {
                return this._sServiceAttributeExternalRef;
            }
            set
            {
                if ((this._sServiceAttributeExternalRef != value))
                {
                    this._sServiceAttributeExternalRef = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAttributeType", DbType = "VarChar(100)")]
        public string sAttributeType
        {
            get
            {
                return this._sAttributeType;
            }
            set
            {
                if ((this._sAttributeType != value))
                {
                    this._sAttributeType = value;
                }
            }
        }


        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sServiceAttributeValue", DbType = "VarChar(100)")]
        public string sServiceAttributeValue
        {
            get
            {
                return this._sServiceAttributeValue;
            }
            set
            {
                if ((this._sServiceAttributeValue != value))
                {
                    this._sServiceAttributeValue = value;
                }
            }
        }
    }

    public partial class get_AllCSGServiceFeaturesResult
    {
        private string _sProductExternalRef;

        private string _sPricingPlanExternalRef;

        public get_AllCSGServiceFeaturesResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sProductExternalRef", DbType = "VarChar(100)")]
        public string sProductExternalRef
        {
            get
            {
                return this._sProductExternalRef;
            }
            set
            {
                if ((this._sProductExternalRef != value))
                {
                    this._sProductExternalRef = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPricingPlanExternalRef", DbType = "VarChar(100)")]
        public string sPricingPlanExternalRef
        {
            get
            {
                return this._sPricingPlanExternalRef;
            }
            set
            {
                if ((this._sPricingPlanExternalRef != value))
                {
                    this._sPricingPlanExternalRef = value;
                }
            }
        }
    }
    //end april


    public partial class mbm_RecordType1_ValidateInvoiceResult
    {

        private string _BAN;

        private System.Nullable<decimal> _RecordType1_Sum;

        private System.Nullable<decimal> _RecordType3_Sum;

        private System.Nullable<decimal> _RecordType5_Sum;

        private System.Nullable<bool> _Validated;

        public mbm_RecordType1_ValidateInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(6) NOT NULL", CanBeNull = false)]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType1_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType1_Sum
        {
            get
            {
                return this._RecordType1_Sum;
            }
            set
            {
                if ((this._RecordType1_Sum != value))
                {
                    this._RecordType1_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType3_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType3_Sum
        {
            get
            {
                return this._RecordType3_Sum;
            }
            set
            {
                if ((this._RecordType3_Sum != value))
                {
                    this._RecordType3_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType5_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType5_Sum
        {
            get
            {
                return this._RecordType5_Sum;
            }
            set
            {
                if ((this._RecordType5_Sum != value))
                {
                    this._RecordType5_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Validated", DbType = "Bit")]
        public System.Nullable<bool> Validated
        {
            get
            {
                return this._Validated;
            }
            set
            {
                if ((this._Validated != value))
                {
                    this._Validated = value;
                }
            }
        }
    }

    public partial class mbm_RecordType2_ValidateInvoiceResult
    {

        private string _BAN;

        private System.Nullable<decimal> _RecordType2_Sum;

        private System.Nullable<decimal> _RecordType3_Sum;

        private System.Nullable<decimal> _RecordType5_Sum;

        private System.Nullable<bool> _Validated;

        public mbm_RecordType2_ValidateInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(6) NOT NULL", CanBeNull = false)]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType2_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType2_Sum
        {
            get
            {
                return this._RecordType2_Sum;
            }
            set
            {
                if ((this._RecordType2_Sum != value))
                {
                    this._RecordType2_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType3_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType3_Sum
        {
            get
            {
                return this._RecordType3_Sum;
            }
            set
            {
                if ((this._RecordType3_Sum != value))
                {
                    this._RecordType3_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType5_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType5_Sum
        {
            get
            {
                return this._RecordType5_Sum;
            }
            set
            {
                if ((this._RecordType5_Sum != value))
                {
                    this._RecordType5_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Validated", DbType = "Bit")]
        public System.Nullable<bool> Validated
        {
            get
            {
                return this._Validated;
            }
            set
            {
                if ((this._Validated != value))
                {
                    this._Validated = value;
                }
            }
        }
    }

    public partial class mbm_RecordType4_ValidateInvoiceResult
    {

        private string _BAN;

        private System.Nullable<decimal> _RecordType4_Sum;

        private System.Nullable<decimal> _RecordType3_Sum;

        private System.Nullable<decimal> _RecordType5_Sum;

        private System.Nullable<bool> _Validated;

        public mbm_RecordType4_ValidateInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(6) NOT NULL", CanBeNull = false)]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType4_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType4_Sum
        {
            get
            {
                return this._RecordType4_Sum;
            }
            set
            {
                if ((this._RecordType4_Sum != value))
                {
                    this._RecordType4_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType3_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType3_Sum
        {
            get
            {
                return this._RecordType3_Sum;
            }
            set
            {
                if ((this._RecordType3_Sum != value))
                {
                    this._RecordType3_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType5_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType5_Sum
        {
            get
            {
                return this._RecordType5_Sum;
            }
            set
            {
                if ((this._RecordType5_Sum != value))
                {
                    this._RecordType5_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Validated", DbType = "Bit")]
        public System.Nullable<bool> Validated
        {
            get
            {
                return this._Validated;
            }
            set
            {
                if ((this._Validated != value))
                {
                    this._Validated = value;
                }
            }
        }
    }

    public partial class mbm_RecordType5_ProcessResult
    {

        private int _Column1;

        public mbm_RecordType5_ProcessResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "", Storage = "_Column1", DbType = "Int NOT NULL")]
        public int Column1
        {
            get
            {
                return this._Column1;
            }
            set
            {
                if ((this._Column1 != value))
                {
                    this._Column1 = value;
                }
            }
        }
    }

    public partial class mbm_RecordType1_GetByInvoiceResult
    {

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string _Bill_Date;

        private string _SSO;

        private string _From_Number__DID_;

        private string _From_To_Number;

        private System.Nullable<decimal> _Charge_Amount;

        private string _Date_of_Record;

        private string _Connect_Time;

        private System.Nullable<decimal> _Billable_Time;

        private string _Billing_Number_North_American_Standard;

        private string _From_Place;

        private string _From_State;

        private string _To_Place;

        private string _To_State;

        private System.Nullable<int> _Settlement_Code;

        private string _Charge_Description;

        private string _Provider;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        private string _sLegalEntity;

        //8967
        private int _sCount_Of_Charges;

        private System.Nullable<decimal> _sDuration;

        public mbm_RecordType1_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Bill Date]", Storage = "_Bill_Date", DbType = "VarChar(50)")]
        public string Bill_Date
        {
            get
            {
                return this._Bill_Date;
            }
            set
            {
                if ((this._Bill_Date != value))
                {
                    this._Bill_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From Number (DID)]", Storage = "_From_Number__DID_", DbType = "VarChar(50)")]
        public string From_Number__DID_
        {
            get
            {
                return this._From_Number__DID_;
            }
            set
            {
                if ((this._From_Number__DID_ != value))
                {
                    this._From_Number__DID_ = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From/To Number]", Storage = "_From_To_Number", DbType = "VarChar(50)")]
        public string From_To_Number
        {
            get
            {
                return this._From_To_Number;
            }
            set
            {
                if ((this._From_To_Number != value))
                {
                    this._From_To_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Amount]", Storage = "_Charge_Amount", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Charge_Amount
        {
            get
            {
                return this._Charge_Amount;
            }
            set
            {
                if ((this._Charge_Amount != value))
                {
                    this._Charge_Amount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Date of Record]", Storage = "_Date_of_Record", DbType = "VarChar(50)")]
        public string Date_of_Record
        {
            get
            {
                return this._Date_of_Record;
            }
            set
            {
                if ((this._Date_of_Record != value))
                {
                    this._Date_of_Record = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Connect Time]", Storage = "_Connect_Time", DbType = "VarChar(11)")]
        public string Connect_Time
        {
            get
            {
                return this._Connect_Time;
            }
            set
            {
                if ((this._Connect_Time != value))
                {
                    this._Connect_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Billable Time]", Storage = "_Billable_Time", DbType = "Decimal(18,4)")]
        public System.Nullable<decimal> Billable_Time
        {
            get
            {
                return this._Billable_Time;
            }
            set
            {
                if ((this._Billable_Time != value))
                {
                    this._Billable_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Billing Number North American Standard]", Storage = "_Billing_Number_North_American_Standard", DbType = "VarChar(8000)")]
        public string Billing_Number_North_American_Standard
        {
            get
            {
                return this._Billing_Number_North_American_Standard;
            }
            set
            {
                if ((this._Billing_Number_North_American_Standard != value))
                {
                    this._Billing_Number_North_American_Standard = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From Place]", Storage = "_From_Place", DbType = "VarChar(50)")]
        public string From_Place
        {
            get
            {
                return this._From_Place;
            }
            set
            {
                if ((this._From_Place != value))
                {
                    this._From_Place = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From State]", Storage = "_From_State", DbType = "VarChar(50)")]
        public string From_State
        {
            get
            {
                return this._From_State;
            }
            set
            {
                if ((this._From_State != value))
                {
                    this._From_State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[To Place]", Storage = "_To_Place", DbType = "VarChar(50)")]
        public string To_Place
        {
            get
            {
                return this._To_Place;
            }
            set
            {
                if ((this._To_Place != value))
                {
                    this._To_Place = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[To State]", Storage = "_To_State", DbType = "VarChar(50)")]
        public string To_State
        {
            get
            {
                return this._To_State;
            }
            set
            {
                if ((this._To_State != value))
                {
                    this._To_State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Settlement Code]", Storage = "_Settlement_Code", DbType = "Int")]
        public System.Nullable<int> Settlement_Code
        {
            get
            {
                return this._Settlement_Code;
            }
            set
            {
                if ((this._Settlement_Code != value))
                {
                    this._Settlement_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(50)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Provider", DbType = "VarChar(50)")]
        public string Provider
        {
            get
            {
                return this._Provider;
            }
            set
            {
                if ((this._Provider != value))
                {
                    this._Provider = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._sLegalEntity;
            }
            set
            {
                if ((this._sLegalEntity != value))
                {
                    this._sLegalEntity = value;
                }
            }
        }

        //8967
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sCount_Of_Charges", DbType = "Int NOT NULL")]
        public int Count_Of_Charges
        {
            get
            {
                return this._sCount_Of_Charges;
            }
            set
            {
                if ((this._sCount_Of_Charges != value))
                {
                    this._sCount_Of_Charges = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDuration", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> Duration
        {
            get
            {
                return this._sDuration;
            }
            set
            {
                if ((this._sDuration != value))
                {
                    this._sDuration = value;
                }
            }
        }
    }

    public partial class mbm_RecordType1_GetExceptionsByInvoiceResult
    {

        private string _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string _Bill_Date;

        private string _SSO;

        private string _From_Number__DID_;

        private string _DIDNumber;

        private string _From_To_Number;

        private System.Nullable<decimal> _Charge_Amount;

        private string _Date_of_Record;

        private string _Connect_Time;

        private System.Nullable<decimal> _Billable_Time;

        private string _Billing_Number_North_American_Standard;

        private string _From_Place;

        private string _From_State;

        private string _To_Place;

        private string _To_State;

        private System.Nullable<int> _Settlement_Code;

        private string _Charge_Description;

        private string _Provider;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        public mbm_RecordType1_GetExceptionsByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "VarChar(9) NOT NULL", CanBeNull = false)]
        public string Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Bill Date]", Storage = "_Bill_Date", DbType = "VarChar(50)")]
        public string Bill_Date
        {
            get
            {
                return this._Bill_Date;
            }
            set
            {
                if ((this._Bill_Date != value))
                {
                    this._Bill_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From Number (DID)]", Storage = "_From_Number__DID_", DbType = "VarChar(50)")]
        public string From_Number__DID_
        {
            get
            {
                return this._From_Number__DID_;
            }
            set
            {
                if ((this._From_Number__DID_ != value))
                {
                    this._From_Number__DID_ = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DIDNumber", DbType = "VarChar(50)")]
        public string DIDNumber
        {
            get
            {
                return this._DIDNumber;
            }
            set
            {
                if ((this._DIDNumber != value))
                {
                    this._DIDNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From/To Number]", Storage = "_From_To_Number", DbType = "VarChar(50)")]
        public string From_To_Number
        {
            get
            {
                return this._From_To_Number;
            }
            set
            {
                if ((this._From_To_Number != value))
                {
                    this._From_To_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Amount]", Storage = "_Charge_Amount", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Charge_Amount
        {
            get
            {
                return this._Charge_Amount;
            }
            set
            {
                if ((this._Charge_Amount != value))
                {
                    this._Charge_Amount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Date of Record]", Storage = "_Date_of_Record", DbType = "VarChar(50)")]
        public string Date_of_Record
        {
            get
            {
                return this._Date_of_Record;
            }
            set
            {
                if ((this._Date_of_Record != value))
                {
                    this._Date_of_Record = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Connect Time]", Storage = "_Connect_Time", DbType = "VarChar(11)")]
        public string Connect_Time
        {
            get
            {
                return this._Connect_Time;
            }
            set
            {
                if ((this._Connect_Time != value))
                {
                    this._Connect_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Billable Time]", Storage = "_Billable_Time", DbType = "Decimal(18,4)")]
        public System.Nullable<decimal> Billable_Time
        {
            get
            {
                return this._Billable_Time;
            }
            set
            {
                if ((this._Billable_Time != value))
                {
                    this._Billable_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Billing Number North American Standard]", Storage = "_Billing_Number_North_American_Standard", DbType = "VarChar(8000)")]
        public string Billing_Number_North_American_Standard
        {
            get
            {
                return this._Billing_Number_North_American_Standard;
            }
            set
            {
                if ((this._Billing_Number_North_American_Standard != value))
                {
                    this._Billing_Number_North_American_Standard = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From Place]", Storage = "_From_Place", DbType = "VarChar(50)")]
        public string From_Place
        {
            get
            {
                return this._From_Place;
            }
            set
            {
                if ((this._From_Place != value))
                {
                    this._From_Place = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From State]", Storage = "_From_State", DbType = "VarChar(50)")]
        public string From_State
        {
            get
            {
                return this._From_State;
            }
            set
            {
                if ((this._From_State != value))
                {
                    this._From_State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[To Place]", Storage = "_To_Place", DbType = "VarChar(50)")]
        public string To_Place
        {
            get
            {
                return this._To_Place;
            }
            set
            {
                if ((this._To_Place != value))
                {
                    this._To_Place = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[To State]", Storage = "_To_State", DbType = "VarChar(50)")]
        public string To_State
        {
            get
            {
                return this._To_State;
            }
            set
            {
                if ((this._To_State != value))
                {
                    this._To_State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Settlement Code]", Storage = "_Settlement_Code", DbType = "Int")]
        public System.Nullable<int> Settlement_Code
        {
            get
            {
                return this._Settlement_Code;
            }
            set
            {
                if ((this._Settlement_Code != value))
                {
                    this._Settlement_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(50)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Provider", DbType = "VarChar(50)")]
        public string Provider
        {
            get
            {
                return this._Provider;
            }
            set
            {
                if ((this._Provider != value))
                {
                    this._Provider = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }


    public partial class mbm_RecordType2_GetByInvoiceResult
    {

        private int _ID;

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string @__10_Digit_DID;

        private string _SSO;

        private string _Charge_Date;

        private string _Charge_Description;

        private string _Charge_Type;

        private string _Service_Start_Date;

        private string _Service_End_Date;

        private string _Invoice_Bill_Period_Start;

        private string _Invoice_Bill_Period_End;

        private System.Nullable<decimal> _Total;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        private string _sLegalEntity;

        private string _AssetSearchCode1;

        //8967
        private string _Department;

        private string _Cost_Center;

        private string _ALI_Code;

        private string _Supervisor;

        public mbm_RecordType2_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssetSearchCode1", DbType = "VarChar(100)")]
        public string AssetSearchCode1
        {
            get
            {
                return this._AssetSearchCode1;
            }
            set
            {
                if ((this._AssetSearchCode1 != value))
                {
                    this._AssetSearchCode1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 Digit DID]", Storage = "__10_Digit_DID", DbType = "VarChar(8000)")]
        public string _10_Digit_DID
        {
            get
            {
                return this.@__10_Digit_DID;
            }
            set
            {
                if ((this.@__10_Digit_DID != value))
                {
                    this.@__10_Digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Date]", Storage = "_Charge_Date", DbType = "VarChar(50)")]
        public string Charge_Date
        {
            get
            {
                return this._Charge_Date;
            }
            set
            {
                if ((this._Charge_Date != value))
                {
                    this._Charge_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(255)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Type]", Storage = "_Charge_Type", DbType = "VarChar(50)")]
        public string Charge_Type
        {
            get
            {
                return this._Charge_Type;
            }
            set
            {
                if ((this._Charge_Type != value))
                {
                    this._Charge_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service Start Date]", Storage = "_Service_Start_Date", DbType = "VarChar(8000)")]
        public string Service_Start_Date
        {
            get
            {
                return this._Service_Start_Date;
            }
            set
            {
                if ((this._Service_Start_Date != value))
                {
                    this._Service_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service End Date]", Storage = "_Service_End_Date", DbType = "VarChar(8000)")]
        public string Service_End_Date
        {
            get
            {
                return this._Service_End_Date;
            }
            set
            {
                if ((this._Service_End_Date != value))
                {
                    this._Service_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period Start]", Storage = "_Invoice_Bill_Period_Start", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_Start
        {
            get
            {
                return this._Invoice_Bill_Period_Start;
            }
            set
            {
                if ((this._Invoice_Bill_Period_Start != value))
                {
                    this._Invoice_Bill_Period_Start = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period End]", Storage = "_Invoice_Bill_Period_End", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_End
        {
            get
            {
                return this._Invoice_Bill_Period_End;
            }
            set
            {
                if ((this._Invoice_Bill_Period_End != value))
                {
                    this._Invoice_Bill_Period_End = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }


        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._sLegalEntity;
            }
            set
            {
                if ((this._sLegalEntity != value))
                {
                    this._sLegalEntity = value;
                }
            }
        }


        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Department", DbType = "VarChar(200)")]
        public string Department
        {
            get
            {
                return this._Department;
            }
            set
            {
                if ((this._Department != value))
                {
                    this._Department = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cost_Center", DbType = "VarChar(200)")]
        public string Cost_Center
        {
            get
            {
                return this._Cost_Center;
            }
            set
            {
                if ((this._Cost_Center != value))
                {
                    this._Cost_Center = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ALI_Code", DbType = "VarChar(200)")]
        public string ALI_Code
        {
            get
            {
                return this._ALI_Code;
            }
            set
            {
                if ((this._ALI_Code != value))
                {
                    this._ALI_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Supervisor", DbType = "VarChar(200)")]
        public string Supervisor
        {
            get
            {
                return this._Supervisor;
            }
            set
            {
                if ((this._Supervisor != value))
                {
                    this._Supervisor = value;
                }
            }
        }
    }

    //cbe8967

    public partial class mbm_RecordType2_GetByInvoice_OneFileResult
    {

        private int _ID;

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string @__10_Digit_DID;

        private string _SSO;

        private string _Charge_Date;

        private string _Charge_Description;

        private string _Charge_Type;

        private string _Service_Start_Date;

        private string _Service_End_Date;

        private string _Invoice_Bill_Period_Start;

        private string _Invoice_Bill_Period_End;

        private System.Nullable<decimal> _Total;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        private string _LegalEntity;

        private string _AssetSearchCode1;

        private string _Department;

        private string _Cost_Center;

        private string _ALI_Code;

        private string _Supervisor;

        public mbm_RecordType2_GetByInvoice_OneFileResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 Digit DID]", Storage = "__10_Digit_DID", DbType = "VarChar(8000)")]
        public string _10_Digit_DID
        {
            get
            {
                return this.@__10_Digit_DID;
            }
            set
            {
                if ((this.@__10_Digit_DID != value))
                {
                    this.@__10_Digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Date]", Storage = "_Charge_Date", DbType = "VarChar(50)")]
        public string Charge_Date
        {
            get
            {
                return this._Charge_Date;
            }
            set
            {
                if ((this._Charge_Date != value))
                {
                    this._Charge_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(255)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Type]", Storage = "_Charge_Type", DbType = "VarChar(50)")]
        public string Charge_Type
        {
            get
            {
                return this._Charge_Type;
            }
            set
            {
                if ((this._Charge_Type != value))
                {
                    this._Charge_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service Start Date]", Storage = "_Service_Start_Date", DbType = "VarChar(8000)")]
        public string Service_Start_Date
        {
            get
            {
                return this._Service_Start_Date;
            }
            set
            {
                if ((this._Service_Start_Date != value))
                {
                    this._Service_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service End Date]", Storage = "_Service_End_Date", DbType = "VarChar(8000)")]
        public string Service_End_Date
        {
            get
            {
                return this._Service_End_Date;
            }
            set
            {
                if ((this._Service_End_Date != value))
                {
                    this._Service_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period Start]", Storage = "_Invoice_Bill_Period_Start", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_Start
        {
            get
            {
                return this._Invoice_Bill_Period_Start;
            }
            set
            {
                if ((this._Invoice_Bill_Period_Start != value))
                {
                    this._Invoice_Bill_Period_Start = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period End]", Storage = "_Invoice_Bill_Period_End", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_End
        {
            get
            {
                return this._Invoice_Bill_Period_End;
            }
            set
            {
                if ((this._Invoice_Bill_Period_End != value))
                {
                    this._Invoice_Bill_Period_End = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._LegalEntity;
            }
            set
            {
                if ((this._LegalEntity != value))
                {
                    this._LegalEntity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssetSearchCode1", DbType = "VarChar(100)")]
        public string AssetSearchCode1
        {
            get
            {
                return this._AssetSearchCode1;
            }
            set
            {
                if ((this._AssetSearchCode1 != value))
                {
                    this._AssetSearchCode1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Department", DbType = "VarChar(200)")]
        public string Department
        {
            get
            {
                return this._Department;
            }
            set
            {
                if ((this._Department != value))
                {
                    this._Department = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Cost Center]", Storage = "_Cost_Center", DbType = "VarChar(200)")]
        public string Cost_Center
        {
            get
            {
                return this._Cost_Center;
            }
            set
            {
                if ((this._Cost_Center != value))
                {
                    this._Cost_Center = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[ALI Code]", Storage = "_ALI_Code", DbType = "VarChar(200)")]
        public string ALI_Code
        {
            get
            {
                return this._ALI_Code;
            }
            set
            {
                if ((this._ALI_Code != value))
                {
                    this._ALI_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Supervisor", DbType = "VarChar(200)")]
        public string Supervisor
        {
            get
            {
                return this._Supervisor;
            }
            set
            {
                if ((this._Supervisor != value))
                {
                    this._Supervisor = value;
                }
            }
        }
    }

    public partial class mbm_RecordType1_GetByInvoice_OneTypeFileResult
    {

        private string _LegalEntity;

        private string _NA_Billing_Number;

        private System.Nullable<int> _Count_of_Charges;

        private System.Nullable<decimal> _Duration;

        private System.Nullable<decimal> _Charge_Amount;

        public mbm_RecordType1_GetByInvoice_OneTypeFileResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._LegalEntity;
            }
            set
            {
                if ((this._LegalEntity != value))
                {
                    this._LegalEntity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[NA Billing Number]", Storage = "_NA_Billing_Number", DbType = "VarChar(50)")]
        public string NA_Billing_Number
        {
            get
            {
                return this._NA_Billing_Number;
            }
            set
            {
                if ((this._NA_Billing_Number != value))
                {
                    this._NA_Billing_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Count of Charges]", Storage = "_Count_of_Charges", DbType = "Int")]
        public System.Nullable<int> Count_of_Charges
        {
            get
            {
                return this._Count_of_Charges;
            }
            set
            {
                if ((this._Count_of_Charges != value))
                {
                    this._Count_of_Charges = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Duration", DbType = "Decimal(38,4)")]
        public System.Nullable<decimal> Duration
        {
            get
            {
                return this._Duration;
            }
            set
            {
                if ((this._Duration != value))
                {
                    this._Duration = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Amount]", Storage = "_Charge_Amount", DbType = "Decimal(38,5)")]
        public System.Nullable<decimal> Charge_Amount
        {
            get
            {
                return this._Charge_Amount;
            }
            set
            {
                if ((this._Charge_Amount != value))
                {
                    this._Charge_Amount = value;
                }
            }
        }
    }

    //cbe8967

    public partial class mbm_RecordType2_GetExceptionsByInvoiceResult
    {

        private string _RecordType;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string _p_Asset_Search_Code;

        private string _SSO;

        private string _ChargeDate;

        private string _ChargeDescription;

        private string _ChargeType;

        private System.Nullable<System.DateTime> _ServiceStartDate;

        private System.Nullable<System.DateTime> _ServiceStopDate;

        private System.Nullable<System.DateTime> _InvoiceBillPeriodStart;

        private System.Nullable<System.DateTime> _InvoiceBillPeriodEnd;

        private System.Nullable<decimal> _Total;

        private int _CurrencyID;

        public mbm_RecordType2_GetExceptionsByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType", DbType = "VarChar(1) NOT NULL", CanBeNull = false)]
        public string RecordType
        {
            get
            {
                return this._RecordType;
            }
            set
            {
                if ((this._RecordType != value))
                {
                    this._RecordType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50)")]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[p_Asset Search Code]", Storage = "_p_Asset_Search_Code", DbType = "VarChar(50)")]
        public string p_Asset_Search_Code
        {
            get
            {
                return this._p_Asset_Search_Code;
            }
            set
            {
                if ((this._p_Asset_Search_Code != value))
                {
                    this._p_Asset_Search_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChargeDate", DbType = "VarChar(50)")]
        public string ChargeDate
        {
            get
            {
                return this._ChargeDate;
            }
            set
            {
                if ((this._ChargeDate != value))
                {
                    this._ChargeDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChargeDescription", DbType = "VarChar(50)")]
        public string ChargeDescription
        {
            get
            {
                return this._ChargeDescription;
            }
            set
            {
                if ((this._ChargeDescription != value))
                {
                    this._ChargeDescription = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChargeType", DbType = "VarChar(50)")]
        public string ChargeType
        {
            get
            {
                return this._ChargeType;
            }
            set
            {
                if ((this._ChargeType != value))
                {
                    this._ChargeType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceStartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ServiceStartDate
        {
            get
            {
                return this._ServiceStartDate;
            }
            set
            {
                if ((this._ServiceStartDate != value))
                {
                    this._ServiceStartDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceStopDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ServiceStopDate
        {
            get
            {
                return this._ServiceStopDate;
            }
            set
            {
                if ((this._ServiceStopDate != value))
                {
                    this._ServiceStopDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceBillPeriodStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> InvoiceBillPeriodStart
        {
            get
            {
                return this._InvoiceBillPeriodStart;
            }
            set
            {
                if ((this._InvoiceBillPeriodStart != value))
                {
                    this._InvoiceBillPeriodStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceBillPeriodEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> InvoiceBillPeriodEnd
        {
            get
            {
                return this._InvoiceBillPeriodEnd;
            }
            set
            {
                if ((this._InvoiceBillPeriodEnd != value))
                {
                    this._InvoiceBillPeriodEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyID", DbType = "Int NOT NULL")]
        public int CurrencyID
        {
            get
            {
                return this._CurrencyID;
            }
            set
            {
                if ((this._CurrencyID != value))
                {
                    this._CurrencyID = value;
                }
            }
        }
    }

    public partial class mbm_RecordType3_GetByInvoiceResult
    {

        private int _ID;

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string _Bill_Date;

        private System.Nullable<decimal> _Record_Type_1_Total;

        private System.Nullable<decimal> _Record_Type_2_Total;

        private System.Nullable<decimal> _Record_Type_4_Total;

        private System.Nullable<decimal> _Total;

        private int _CurrencyConversionID;

        private string _sLegalEntity;

        public mbm_RecordType3_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Bill Date]", Storage = "_Bill_Date", DbType = "VarChar(50)")]
        public string Bill_Date
        {
            get
            {
                return this._Bill_Date;
            }
            set
            {
                if ((this._Bill_Date != value))
                {
                    this._Bill_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 1 Total]", Storage = "_Record_Type_1_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_1_Total
        {
            get
            {
                return this._Record_Type_1_Total;
            }
            set
            {
                if ((this._Record_Type_1_Total != value))
                {
                    this._Record_Type_1_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 2 Total]", Storage = "_Record_Type_2_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_2_Total
        {
            get
            {
                return this._Record_Type_2_Total;
            }
            set
            {
                if ((this._Record_Type_2_Total != value))
                {
                    this._Record_Type_2_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 4 Total]", Storage = "_Record_Type_4_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_4_Total
        {
            get
            {
                return this._Record_Type_4_Total;
            }
            set
            {
                if ((this._Record_Type_4_Total != value))
                {
                    this._Record_Type_4_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._sLegalEntity;
            }
            set
            {
                if ((this._sLegalEntity != value))
                {
                    this._sLegalEntity = value;
                }
            }
        }
    }

    public partial class mbm_RecordType4_GetByInvoiceResult
    {

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string @__10_Digit_DID;

        private string _SSO;

        private string _Charge_Description;

        private System.Nullable<decimal> _Tax_Charge;

        private string _Vendor_Name;

        private string _BillDate;

        private System.Nullable<decimal> _Tax_Percentage;

        private string _ServiceType;

        private string _State_Province;

        private int _CurrencyConversionID;

        private string _sLegalEntity;

        public mbm_RecordType4_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 Digit DID]", Storage = "__10_Digit_DID", DbType = "VarChar(50)")]
        public string _10_Digit_DID
        {
            get
            {
                return this.@__10_Digit_DID;
            }
            set
            {
                if ((this.@__10_Digit_DID != value))
                {
                    this.@__10_Digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(50)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Tax Charge]", Storage = "_Tax_Charge", DbType = "Decimal(38,2)")]
        public System.Nullable<decimal> Tax_Charge
        {
            get
            {
                return this._Tax_Charge;
            }
            set
            {
                if ((this._Tax_Charge != value))
                {
                    this._Tax_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillDate", DbType = "VarChar(50)")]
        public string BillDate
        {
            get
            {
                return this._BillDate;
            }
            set
            {
                if ((this._BillDate != value))
                {
                    this._BillDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Tax Percentage]", Storage = "_Tax_Percentage", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Tax_Percentage
        {
            get
            {
                return this._Tax_Percentage;
            }
            set
            {
                if ((this._Tax_Percentage != value))
                {
                    this._Tax_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceType", DbType = "VarChar(50)")]
        public string ServiceType
        {
            get
            {
                return this._ServiceType;
            }
            set
            {
                if ((this._ServiceType != value))
                {
                    this._ServiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._sLegalEntity;
            }
            set
            {
                if ((this._sLegalEntity != value))
                {
                    this._sLegalEntity = value;
                }
            }
        }
    }

    public partial class mbm_RecordType4_GetExceptionsByInvoiceResult
    {

        private int _ID;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private System.Nullable<int> _InvoiceType;

        private System.Nullable<int> _RecordType;

        private string _CustomerNumber;

        private string _PhoneNumber;

        private string _CallType;

        private System.Nullable<int> _TotalCallCount;

        private System.Nullable<decimal> _TotalCallDuration;

        private string _UsageTotal;

        private string _Inter_IntlUsage;

        private System.Nullable<decimal> _State;

        private System.Nullable<decimal> _Local;

        private System.Nullable<decimal> _Federal;

        private System.Nullable<decimal> _USF;

        private System.Nullable<decimal> _ARF;

        private System.Nullable<int> _DefaultImportCurrencyID;

        public mbm_RecordType4_GetExceptionsByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50)")]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int")]
        public System.Nullable<int> InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType", DbType = "Int")]
        public System.Nullable<int> RecordType
        {
            get
            {
                return this._RecordType;
            }
            set
            {
                if ((this._RecordType != value))
                {
                    this._RecordType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerNumber", DbType = "VarChar(50)")]
        public string CustomerNumber
        {
            get
            {
                return this._CustomerNumber;
            }
            set
            {
                if ((this._CustomerNumber != value))
                {
                    this._CustomerNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PhoneNumber", DbType = "VarChar(50)")]
        public string PhoneNumber
        {
            get
            {
                return this._PhoneNumber;
            }
            set
            {
                if ((this._PhoneNumber != value))
                {
                    this._PhoneNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CallType", DbType = "VarChar(50)")]
        public string CallType
        {
            get
            {
                return this._CallType;
            }
            set
            {
                if ((this._CallType != value))
                {
                    this._CallType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalCallCount", DbType = "Int")]
        public System.Nullable<int> TotalCallCount
        {
            get
            {
                return this._TotalCallCount;
            }
            set
            {
                if ((this._TotalCallCount != value))
                {
                    this._TotalCallCount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalCallDuration", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> TotalCallDuration
        {
            get
            {
                return this._TotalCallDuration;
            }
            set
            {
                if ((this._TotalCallDuration != value))
                {
                    this._TotalCallDuration = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UsageTotal", DbType = "VarChar(50)")]
        public string UsageTotal
        {
            get
            {
                return this._UsageTotal;
            }
            set
            {
                if ((this._UsageTotal != value))
                {
                    this._UsageTotal = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Inter/IntlUsage]", Storage = "_Inter_IntlUsage", DbType = "VarChar(50)")]
        public string Inter_IntlUsage
        {
            get
            {
                return this._Inter_IntlUsage;
            }
            set
            {
                if ((this._Inter_IntlUsage != value))
                {
                    this._Inter_IntlUsage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_State", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> State
        {
            get
            {
                return this._State;
            }
            set
            {
                if ((this._State != value))
                {
                    this._State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Local", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> Local
        {
            get
            {
                return this._Local;
            }
            set
            {
                if ((this._Local != value))
                {
                    this._Local = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Federal", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> Federal
        {
            get
            {
                return this._Federal;
            }
            set
            {
                if ((this._Federal != value))
                {
                    this._Federal = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_USF", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> USF
        {
            get
            {
                return this._USF;
            }
            set
            {
                if ((this._USF != value))
                {
                    this._USF = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ARF", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> ARF
        {
            get
            {
                return this._ARF;
            }
            set
            {
                if ((this._ARF != value))
                {
                    this._ARF = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DefaultImportCurrencyID", DbType = "Int")]
        public System.Nullable<int> DefaultImportCurrencyID
        {
            get
            {
                return this._DefaultImportCurrencyID;
            }
            set
            {
                if ((this._DefaultImportCurrencyID != value))
                {
                    this._DefaultImportCurrencyID = value;
                }
            }
        }
    }

    public partial class mbm_RecordType5_GetByInvoiceResult
    {

        private int _Record_Type;

        private int _Check_Record_Type;

        private string _BAN;

        private int _Total_Record_Count;

        private string _SumField_Name;

        private decimal _TotalAmount;

        private int _CurrencyConversionID;

        private string _LegalEntity;

        private System.Nullable<int> _InvoiceID;

        public mbm_RecordType5_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Check Record Type]", Storage = "_Check_Record_Type", DbType = "Int NOT NULL")]
        public int Check_Record_Type
        {
            get
            {
                return this._Check_Record_Type;
            }
            set
            {
                if ((this._Check_Record_Type != value))
                {
                    this._Check_Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Total Record Count]", Storage = "_Total_Record_Count", DbType = "Int NOT NULL")]
        public int Total_Record_Count
        {
            get
            {
                return this._Total_Record_Count;
            }
            set
            {
                if ((this._Total_Record_Count != value))
                {
                    this._Total_Record_Count = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[SumField Name]", Storage = "_SumField_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string SumField_Name
        {
            get
            {
                return this._SumField_Name;
            }
            set
            {
                if ((this._SumField_Name != value))
                {
                    this._SumField_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalAmount", DbType = "Decimal(18,2) NOT NULL")]
        public decimal TotalAmount
        {
            get
            {
                return this._TotalAmount;
            }
            set
            {
                if ((this._TotalAmount != value))
                {
                    this._TotalAmount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._LegalEntity;
            }
            set
            {
                if ((this._LegalEntity != value))
                {
                    this._LegalEntity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int")]
        public System.Nullable<int> InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }
    }


    public partial class get_AssoicateFileTypesByInvoiceTypeIdResult
    {

        private int _iAssociatedFileId;

        private string _sFileType;

        private int _iFileTypeId;

        public get_AssoicateFileTypesByInvoiceTypeIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iAssociatedFileId", DbType = "Int NOT NULL")]
        public int iAssociatedFileId
        {
            get
            {
                return this._iAssociatedFileId;
            }
            set
            {
                if ((this._iAssociatedFileId != value))
                {
                    this._iAssociatedFileId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFileType", DbType = "VarChar(75) NOT NULL", CanBeNull = false)]
        public string sFileType
        {
            get
            {
                return this._sFileType;
            }
            set
            {
                if ((this._sFileType != value))
                {
                    this._sFileType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iFileTypeId", DbType = "Int NOT NULL")]
        public int iFileTypeId
        {
            get
            {
                return this._iFileTypeId;
            }
            set
            {
                if ((this._iFileTypeId != value))
                {
                    this._iFileTypeId = value;
                }
            }
        }
    }

    public partial class get_ValidateCRMAccountNumberResult
    {

        private string _CRMAccountNumber;

        private string _CRMAccountFirstName;

        private string _CRMAccountLastName;

        private string _CRMAccountParentAccountNumber;

        private string _CRMAccountParentFirstName;

        private string _CRMAccountParentLastName;

        private System.Nullable<int> _CRMAccountBillCycle;

        public get_ValidateCRMAccountNumberResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CRMAccountNumber", DbType = "VarChar(50)")]
        public string CRMAccountNumber
        {
            get
            {
                return this._CRMAccountNumber;
            }
            set
            {
                if ((this._CRMAccountNumber != value))
                {
                    this._CRMAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CRMAccountFirstName", DbType = "VarChar(100)")]
        public string CRMAccountFirstName
        {
            get
            {
                return this._CRMAccountFirstName;
            }
            set
            {
                if ((this._CRMAccountFirstName != value))
                {
                    this._CRMAccountFirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CRMAccountLastName", DbType = "VarChar(100)")]
        public string CRMAccountLastName
        {
            get
            {
                return this._CRMAccountLastName;
            }
            set
            {
                if ((this._CRMAccountLastName != value))
                {
                    this._CRMAccountLastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CRMAccountParentAccountNumber", DbType = "VarChar(50)")]
        public string CRMAccountParentAccountNumber
        {
            get
            {
                return this._CRMAccountParentAccountNumber;
            }
            set
            {
                if ((this._CRMAccountParentAccountNumber != value))
                {
                    this._CRMAccountParentAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CRMAccountParentFirstName", DbType = "VarChar(100)")]
        public string CRMAccountParentFirstName
        {
            get
            {
                return this._CRMAccountParentFirstName;
            }
            set
            {
                if ((this._CRMAccountParentFirstName != value))
                {
                    this._CRMAccountParentFirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CRMAccountParentLastName", DbType = "VarChar(100)")]
        public string CRMAccountParentLastName
        {
            get
            {
                return this._CRMAccountParentLastName;
            }
            set
            {
                if ((this._CRMAccountParentLastName != value))
                {
                    this._CRMAccountParentLastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CRMAccountBillCycle", DbType = "Int")]
        public System.Nullable<int> CRMAccountBillCycle
        {
            get
            {
                return this._CRMAccountBillCycle;
            }
            set
            {
                if ((this._CRMAccountBillCycle != value))
                {
                    this._CRMAccountBillCycle = value;
                }
            }
        }
    }

    public partial class get_ValidateCSGAccountNumberResult
    {
        private string _CSGAccountNumber;
        private long _CSGSubcriberNumber;
        private string _CSGSubcriberFirstName;
        private string _CSGSubcriberLastName;
        private System.Nullable<long> _CSGParentSubcriberNumber;
        private string _CSGParentAccountNumber;
        private string _CSGParentFirstName;
        private string _CSGParentLastName;
        private string _CSGbillcyclename;
        private System.Nullable<int> _CSGHierarchylevel;

        public get_ValidateCSGAccountNumberResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CSGAccountNumber", DbType = "nvarchar(250)")]
        public string CSGAccountNumber
        {
            get
            { return this._CSGAccountNumber; }
            set
            {
                if ((this._CSGAccountNumber != value))
                {
                    this._CSGAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CSGSubcriberNumber", DbType = "bigInt NOT NULL")]
        public long CSGSubcriberNumber
        {
            get
            { return this._CSGSubcriberNumber; }
            set
            {
                if ((this._CSGSubcriberNumber != value))
                {
                    this._CSGSubcriberNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CSGSubcriberFirstName", DbType = "nvarChar(250)")]
        public string CSGSubcriberFirstName
        {
            get
            { return this._CSGSubcriberFirstName; }
            set
            {
                if ((this._CSGSubcriberFirstName != value))
                {
                    this._CSGSubcriberFirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CSGSubcriberLastName", DbType = "nvarChar(250)")]
        public string CSGSubcriberLastName
        {
            get
            { return this._CSGSubcriberLastName; }
            set
            {
                if ((this._CSGSubcriberLastName != value))
                {
                    this._CSGSubcriberLastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CSGParentSubcriberNumber", DbType = "bigInt")]
        public System.Nullable<long> CSGParentSubcriberNumber
        {
            get
            { return this._CSGParentSubcriberNumber; }
            set
            {
                if ((this._CSGParentSubcriberNumber != value))
                {
                    this._CSGParentSubcriberNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CSGParentAccountNumber", DbType = "nvarchar(20)")]
        public string CSGParentAccountNumber
        {
            get
            { return this._CSGParentAccountNumber; }
            set
            {
                if ((this._CSGParentAccountNumber != value))
                {
                    this._CSGParentAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CSGParentFirstName", DbType = "nvarChar(250)")]
        public string CSGParentFirstName
        {
            get
            { return this._CSGParentFirstName; }
            set
            {
                if ((this._CSGParentFirstName != value))
                {
                    this._CSGParentFirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CSGParentLastName", DbType = "nvarChar(250)")]
        public string CSGParentLastName
        {
            get
            { return this._CSGParentLastName; }
            set
            {
                if ((this._CSGParentLastName != value))
                {
                    this._CSGParentLastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CSGbillcyclename", DbType = "nvarchar(250)")]
        public string CSGBillcycleName
        {
            get
            { return this._CSGbillcyclename; }
            set
            {
                if ((this._CSGbillcyclename != value))
                {
                    this._CSGbillcyclename = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CSGHierarchylevel", DbType = "Int")]
        public System.Nullable<int> CSGHierarchylevel
        {
            get
            { return this._CSGHierarchylevel; }
            set
            {
                if ((this._CSGHierarchylevel != value))
                {
                    this._CSGHierarchylevel = value;
                }
            }
        }
    }

    public partial class get_AssoicateAccountsByInvoiceTypeIdResult
    {

        private long _iAccountNumber;

        private string _sAccountName;

        private string _sStringIdentifier;

        private System.Nullable<long> _iParentAccountNumber;

        private string _sParentAccountName;

        private System.Nullable<bool> _bCreateAccount;

        private System.Nullable<int> _iBillCycle;

        private System.Nullable<System.DateTime> _EffectiveBillDate;

        private bool _bChildPays;

        private string _sFTP;

        private string _sUserName;

        private string _sPassword;

        private string _sEmailID;

        private bool _bEDI;

        public get_AssoicateAccountsByInvoiceTypeIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iAccountNumber", DbType = "bigInt NOT NULL")]
        public long iAccountNumber
        {
            get
            {
                return this._iAccountNumber;
            }
            set
            {
                if ((this._iAccountNumber != value))
                {
                    this._iAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAccountName", DbType = "VarChar(75)")]
        public string sAccountName
        {
            get
            {
                return this._sAccountName;
            }
            set
            {
                if ((this._sAccountName != value))
                {
                    this._sAccountName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sStringIdentifier", DbType = "VarChar(100)")]
        public string sStringIdentifier
        {
            get
            {
                return this._sStringIdentifier;
            }
            set
            {
                if ((this._sStringIdentifier != value))
                {
                    this._sStringIdentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iParentAccountNumber", DbType = "bigInt")]
        public System.Nullable<long> iParentAccountNumber
        {
            get
            {
                return this._iParentAccountNumber;
            }
            set
            {
                if ((this._iParentAccountNumber != value))
                {
                    this._iParentAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sParentAccountName", DbType = "VarChar(75)")]
        public string sParentAccountName
        {
            get
            {
                return this._sParentAccountName;
            }
            set
            {
                if ((this._sParentAccountName != value))
                {
                    this._sParentAccountName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bCreateAccount", DbType = "Bit")]
        public System.Nullable<bool> bCreateAccount
        {
            get
            {
                return this._bCreateAccount;
            }
            set
            {
                if ((this._bCreateAccount != value))
                {
                    this._bCreateAccount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iBillCycle", DbType = "Int")]
        public System.Nullable<int> iBillCycle
        {
            get
            {
                return this._iBillCycle;
            }
            set
            {
                if ((this._iBillCycle != value))
                {
                    this._iBillCycle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EffectiveBillDate", DbType = "Date")]
        public System.Nullable<System.DateTime> EffectiveBillDate
        {
            get
            {
                return this._EffectiveBillDate;
            }
            set
            {
                if ((this._EffectiveBillDate != value))
                {
                    this._EffectiveBillDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bChildPays", DbType = "Bit NOT NULL")]
        public bool bChildPays
        {
            get
            {
                return this._bChildPays;
            }
            set
            {
                if ((this._bChildPays != value))
                {
                    this._bChildPays = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTP", DbType = "VarChar(50)")]
        public string sFTP
        {
            get
            {
                return this._sFTP;
            }
            set
            {
                if ((this._sFTP != value))
                {
                    this._sFTP = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sUserName", DbType = "VarChar(50)")]
        public string sUserName
        {
            get
            {
                return this._sUserName;
            }
            set
            {
                if ((this._sUserName != value))
                {
                    this._sUserName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPassword", DbType = "VarChar(50)")]
        public string sPassword
        {
            get
            {
                return this._sPassword;
            }
            set
            {
                if ((this._sPassword != value))
                {
                    this._sPassword = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sEmailID", DbType = "VarChar(50)")]
        public string sEmailID
        {
            get
            {
                return this._sEmailID;
            }
            set
            {
                if ((this._sEmailID != value))
                {
                    this._sEmailID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bEDI", DbType = "Bit NOT NULL")]
        public bool bEDI
        {
            get
            {
                return this._bEDI;
            }
            set
            {
                if ((this._bEDI != value))
                {
                    this._bEDI = value;
                }
            }
        }
    }


    public partial class get_AssociatedUsersByInvoiceTypeResult
    {

        private int _iAssociatedUsersId;

        private string _sUserId;

        private string _sFirstName;

        private string _sLastName;

        private System.Nullable<bool> _bIsActive;

        private int _iUserRoleId;

        private string _sRoleCode;

        public get_AssociatedUsersByInvoiceTypeResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iAssociatedUsersId", DbType = "Int NOT NULL")]
        public int iAssociatedUsersId
        {
            get
            {
                return this._iAssociatedUsersId;
            }
            set
            {
                if ((this._iAssociatedUsersId != value))
                {
                    this._iAssociatedUsersId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sUserId", DbType = "VarChar(30) NOT NULL", CanBeNull = false)]
        public string sUserId
        {
            get
            {
                return this._sUserId;
            }
            set
            {
                if ((this._sUserId != value))
                {
                    this._sUserId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFirstName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sFirstName
        {
            get
            {
                return this._sFirstName;
            }
            set
            {
                if ((this._sFirstName != value))
                {
                    this._sFirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastName", DbType = "VarChar(200) NOT NULL", CanBeNull = false)]
        public string sLastName
        {
            get
            {
                return this._sLastName;
            }
            set
            {
                if ((this._sLastName != value))
                {
                    this._sLastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsActive", DbType = "Bit")]
        public System.Nullable<bool> bIsActive
        {
            get
            {
                return this._bIsActive;
            }
            set
            {
                if ((this._bIsActive != value))
                {
                    this._bIsActive = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iUserRoleId", DbType = "Int NOT NULL")]
        public int iUserRoleId
        {
            get
            {
                return this._iUserRoleId;
            }
            set
            {
                if ((this._iUserRoleId != value))
                {
                    this._iUserRoleId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRoleCode", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sRoleCode
        {
            get
            {
                return this._sRoleCode;
            }
            set
            {
                if ((this._sRoleCode != value))
                {
                    this._sRoleCode = value;
                }
            }
        }
    }

    public partial class get_InvoiceFileTypes_ByInvoiceIdResult
    {

        private int _iAssociatedFileId;

        private int _iInvoiceTypeId;

        private int _iFileTypeId;

        private string _sFileType;

        public get_InvoiceFileTypes_ByInvoiceIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iAssociatedFileId", DbType = "Int NOT NULL")]
        public int iAssociatedFileId
        {
            get
            {
                return this._iAssociatedFileId;
            }
            set
            {
                if ((this._iAssociatedFileId != value))
                {
                    this._iAssociatedFileId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iFileTypeId", DbType = "Int NOT NULL")]
        public int iFileTypeId
        {
            get
            {
                return this._iFileTypeId;
            }
            set
            {
                if ((this._iFileTypeId != value))
                {
                    this._iFileTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFileType", DbType = "VarChar(75) NOT NULL", CanBeNull = false)]
        public string sFileType
        {
            get
            {
                return this._sFileType;
            }
            set
            {
                if ((this._sFileType != value))
                {
                    this._sFileType = value;
                }
            }
        }
    }

    public partial class search_UserResult
    {

        private string _sUserId;

        private string _sFirstName;

        private string _sLastName;

        private System.Nullable<bool> _bIsActive;

        private int _iUserRoleId;

        private string _sRoleCode;

        public search_UserResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sUserId", DbType = "VarChar(30) NOT NULL", CanBeNull = false)]
        public string sUserId
        {
            get
            {
                return this._sUserId;
            }
            set
            {
                if ((this._sUserId != value))
                {
                    this._sUserId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFirstName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sFirstName
        {
            get
            {
                return this._sFirstName;
            }
            set
            {
                if ((this._sFirstName != value))
                {
                    this._sFirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastName", DbType = "VarChar(200) NOT NULL", CanBeNull = false)]
        public string sLastName
        {
            get
            {
                return this._sLastName;
            }
            set
            {
                if ((this._sLastName != value))
                {
                    this._sLastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsActive", DbType = "Bit")]
        public System.Nullable<bool> bIsActive
        {
            get
            {
                return this._bIsActive;
            }
            set
            {
                if ((this._bIsActive != value))
                {
                    this._bIsActive = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iUserRoleId", DbType = "Int NOT NULL")]
        public int iUserRoleId
        {
            get
            {
                return this._iUserRoleId;
            }
            set
            {
                if ((this._iUserRoleId != value))
                {
                    this._iUserRoleId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRoleCode", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sRoleCode
        {
            get
            {
                return this._sRoleCode;
            }
            set
            {
                if ((this._sRoleCode != value))
                {
                    this._sRoleCode = value;
                }
            }
        }
    }

    public partial class get_UserDetailsResult
    {

        private string _sUserId;

        private string _sFirstName;

        private string _sLastName;

        private System.Nullable<bool> _bIsActive;

        private string _sRoleCode;

        private int _iUserRoleId;

        public get_UserDetailsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sUserId", DbType = "VarChar(30) NOT NULL", CanBeNull = false)]
        public string sUserId
        {
            get
            {
                return this._sUserId;
            }
            set
            {
                if ((this._sUserId != value))
                {
                    this._sUserId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFirstName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sFirstName
        {
            get
            {
                return this._sFirstName;
            }
            set
            {
                if ((this._sFirstName != value))
                {
                    this._sFirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastName", DbType = "VarChar(200) NOT NULL", CanBeNull = false)]
        public string sLastName
        {
            get
            {
                return this._sLastName;
            }
            set
            {
                if ((this._sLastName != value))
                {
                    this._sLastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsActive", DbType = "Bit")]
        public System.Nullable<bool> bIsActive
        {
            get
            {
                return this._bIsActive;
            }
            set
            {
                if ((this._bIsActive != value))
                {
                    this._bIsActive = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRoleCode", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sRoleCode
        {
            get
            {
                return this._sRoleCode;
            }
            set
            {
                if ((this._sRoleCode != value))
                {
                    this._sRoleCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iUserRoleId", DbType = "Int NOT NULL")]
        public int iUserRoleId
        {
            get
            {
                return this._iUserRoleId;
            }
            set
            {
                if ((this._iUserRoleId != value))
                {
                    this._iUserRoleId = value;
                }
            }
        }
    }

    public partial class ApplicationLog_GetResult
    {

        private int _ID;

        private System.Nullable<int> _InvoiceID;

        private System.Nullable<int> _InvoiceType;

        private string _InvoiceNumber;

        private string _UserName;

        private System.Nullable<System.DateTime> _ExceptionDateTime;

        private string _CodeLocation;

        private string _Comment;

        private string _Message;

        private string _Source;

        private string _TargetSite;

        private string _StackTrace;

        private System.Nullable<int> _LogType;

        public ApplicationLog_GetResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int")]
        public System.Nullable<int> InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int")]
        public System.Nullable<int> InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50)")]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserName", DbType = "VarChar(50)")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExceptionDateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ExceptionDateTime
        {
            get
            {
                return this._ExceptionDateTime;
            }
            set
            {
                if ((this._ExceptionDateTime != value))
                {
                    this._ExceptionDateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CodeLocation", DbType = "VarChar(300)")]
        public string CodeLocation
        {
            get
            {
                return this._CodeLocation;
            }
            set
            {
                if ((this._CodeLocation != value))
                {
                    this._CodeLocation = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Comment", DbType = "VarChar(1000)")]
        public string Comment
        {
            get
            {
                return this._Comment;
            }
            set
            {
                if ((this._Comment != value))
                {
                    this._Comment = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Message", DbType = "VarChar(8000)")]
        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                if ((this._Message != value))
                {
                    this._Message = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Source", DbType = "VarChar(8000)")]
        public string Source
        {
            get
            {
                return this._Source;
            }
            set
            {
                if ((this._Source != value))
                {
                    this._Source = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TargetSite", DbType = "VarChar(300)")]
        public string TargetSite
        {
            get
            {
                return this._TargetSite;
            }
            set
            {
                if ((this._TargetSite != value))
                {
                    this._TargetSite = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StackTrace", DbType = "VarChar(8000)")]
        public string StackTrace
        {
            get
            {
                return this._StackTrace;
            }
            set
            {
                if ((this._StackTrace != value))
                {
                    this._StackTrace = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LogType", DbType = "Int")]
        public System.Nullable<int> LogType
        {
            get
            {
                return this._LogType;
            }
            set
            {
                if ((this._LogType != value))
                {
                    this._LogType = value;
                }
            }
        }
    }

    public partial class usp_GetServiceDeskData_NoGatewaysResult
    {

        private string _p_Asset_Search_Code;

        private string _p_Maint_Start;

        private string _p_Maint_End;

        public usp_GetServiceDeskData_NoGatewaysResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[p_Asset Search Code]", Storage = "_p_Asset_Search_Code", DbType = "VarChar(5) NOT NULL", CanBeNull = false)]
        public string p_Asset_Search_Code
        {
            get
            {
                return this._p_Asset_Search_Code;
            }
            set
            {
                if ((this._p_Asset_Search_Code != value))
                {
                    this._p_Asset_Search_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[p_Maint Start]", Storage = "_p_Maint_Start", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
        public string p_Maint_Start
        {
            get
            {
                return this._p_Maint_Start;
            }
            set
            {
                if ((this._p_Maint_Start != value))
                {
                    this._p_Maint_Start = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[p_Maint End]", Storage = "_p_Maint_End", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
        public string p_Maint_End
        {
            get
            {
                return this._p_Maint_End;
            }
            set
            {
                if ((this._p_Maint_End != value))
                {
                    this._p_Maint_End = value;
                }
            }
        }
    }

    public partial class CanadianTaxes_GetByDateResult
    {

        private string _ProvinceCode;

        private System.Nullable<decimal> _GST;

        private System.Nullable<decimal> _QST;

        private System.Nullable<decimal> _PST;

        private System.Nullable<decimal> _HST_R;

        private System.Nullable<decimal> _HST_NR;

        public CanadianTaxes_GetByDateResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProvinceCode", DbType = "Char(2) NOT NULL", CanBeNull = false)]
        public string ProvinceCode
        {
            get
            {
                return this._ProvinceCode;
            }
            set
            {
                if ((this._ProvinceCode != value))
                {
                    this._ProvinceCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GST", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> GST
        {
            get
            {
                return this._GST;
            }
            set
            {
                if ((this._GST != value))
                {
                    this._GST = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_QST", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> QST
        {
            get
            {
                return this._QST;
            }
            set
            {
                if ((this._QST != value))
                {
                    this._QST = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PST", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> PST
        {
            get
            {
                return this._PST;
            }
            set
            {
                if ((this._PST != value))
                {
                    this._PST = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HST_R", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> HST_R
        {
            get
            {
                return this._HST_R;
            }
            set
            {
                if ((this._HST_R != value))
                {
                    this._HST_R = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HST_NR", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> HST_NR
        {
            get
            {
                return this._HST_NR;
            }
            set
            {
                if ((this._HST_NR != value))
                {
                    this._HST_NR = value;
                }
            }
        }
    }

    public partial class CbadTaxes_GetArtifactCountForTaxImportResult
    {

        private System.Nullable<int> _count;

        public CbadTaxes_GetArtifactCountForTaxImportResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_count", DbType = "Int")]
        public System.Nullable<int> count
        {
            get
            {
                return this._count;
            }
            set
            {
                if ((this._count != value))
                {
                    this._count = value;
                }
            }
        }
    }

    public partial class Configuration_GetResult
    {

        private string _ConfigurationName;

        private string _ConfigurationValue;

        private string _LastUpdatedBy;

        private System.DateTime _LastUpdatedDate;

        public Configuration_GetResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConfigurationName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ConfigurationName
        {
            get
            {
                return this._ConfigurationName;
            }
            set
            {
                if ((this._ConfigurationName != value))
                {
                    this._ConfigurationName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ConfigurationValue", DbType = "VarChar(800) NOT NULL", CanBeNull = false)]
        public string ConfigurationValue
        {
            get
            {
                return this._ConfigurationValue;
            }
            set
            {
                if ((this._ConfigurationValue != value))
                {
                    this._ConfigurationValue = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastUpdatedBy", DbType = "VarChar(50)")]
        public string LastUpdatedBy
        {
            get
            {
                return this._LastUpdatedBy;
            }
            set
            {
                if ((this._LastUpdatedBy != value))
                {
                    this._LastUpdatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastUpdatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime LastUpdatedDate
        {
            get
            {
                return this._LastUpdatedDate;
            }
            set
            {
                if ((this._LastUpdatedDate != value))
                {
                    this._LastUpdatedDate = value;
                }
            }
        }
    }

    public partial class Currency_GetAllResult
    {

        private int _ID;

        private string _CurrencyCode;

        private string _CurrencySymbol;

        private string _Description;

        private decimal _Conversion;

        private System.DateTime _ActiveDate;

        public Currency_GetAllResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyCode", DbType = "Char(3) NOT NULL", CanBeNull = false)]
        public string CurrencyCode
        {
            get
            {
                return this._CurrencyCode;
            }
            set
            {
                if ((this._CurrencyCode != value))
                {
                    this._CurrencyCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencySymbol", DbType = "NVarChar(5) NOT NULL", CanBeNull = false)]
        public string CurrencySymbol
        {
            get
            {
                return this._CurrencySymbol;
            }
            set
            {
                if ((this._CurrencySymbol != value))
                {
                    this._CurrencySymbol = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this._Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Conversion", DbType = "Decimal(18,5) NOT NULL")]
        public decimal Conversion
        {
            get
            {
                return this._Conversion;
            }
            set
            {
                if ((this._Conversion != value))
                {
                    this._Conversion = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActiveDate", DbType = "DateTime NOT NULL")]
        public System.DateTime ActiveDate
        {
            get
            {
                return this._ActiveDate;
            }
            set
            {
                if ((this._ActiveDate != value))
                {
                    this._ActiveDate = value;
                }
            }
        }
    }

    public partial class Currency_GetCurrentResult
    {

        private int _ID;

        private string _CurrencyCode;

        private string _CurrencySymbol;

        private string _Description;

        private decimal _Conversion;

        private System.DateTime _ActiveDate;

        public Currency_GetCurrentResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyCode", DbType = "Char(3) NOT NULL", CanBeNull = false)]
        public string CurrencyCode
        {
            get
            {
                return this._CurrencyCode;
            }
            set
            {
                if ((this._CurrencyCode != value))
                {
                    this._CurrencyCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencySymbol", DbType = "NVarChar(5) NOT NULL", CanBeNull = false)]
        public string CurrencySymbol
        {
            get
            {
                return this._CurrencySymbol;
            }
            set
            {
                if ((this._CurrencySymbol != value))
                {
                    this._CurrencySymbol = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this._Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Conversion", DbType = "Decimal(18,5) NOT NULL")]
        public decimal Conversion
        {
            get
            {
                return this._Conversion;
            }
            set
            {
                if ((this._Conversion != value))
                {
                    this._Conversion = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActiveDate", DbType = "DateTime NOT NULL")]
        public System.DateTime ActiveDate
        {
            get
            {
                return this._ActiveDate;
            }
            set
            {
                if ((this._ActiveDate != value))
                {
                    this._ActiveDate = value;
                }
            }
        }
    }

    public partial class ExceptionLog_GetResult
    {

        private int _ID;

        private System.Nullable<int> _InvoiceID;

        private System.Nullable<int> _InvoiceType;

        private string _InvoiceNumber;

        private string _Type;

        private string _Comment;

        private string _StackTrace;

        private string _CreatedBy;

        private System.Nullable<System.DateTime> _CreatedDate;

        public ExceptionLog_GetResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int")]
        public System.Nullable<int> InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int")]
        public System.Nullable<int> InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50)")]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type", DbType = "VarChar(50)")]
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this._Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Comment", DbType = "VarChar(MAX)")]
        public string Comment
        {
            get
            {
                return this._Comment;
            }
            set
            {
                if ((this._Comment != value))
                {
                    this._Comment = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StackTrace", DbType = "VarChar(MAX)")]
        public string StackTrace
        {
            get
            {
                return this._StackTrace;
            }
            set
            {
                if ((this._StackTrace != value))
                {
                    this._StackTrace = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreatedBy", DbType = "VarChar(50)")]
        public string CreatedBy
        {
            get
            {
                return this._CreatedBy;
            }
            set
            {
                if ((this._CreatedBy != value))
                {
                    this._CreatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreatedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }
    }

    public partial class GE_LoyaltyCredit_GetByInvoiceResult
    {

        private int _ID;

        private int _InvoiceID;

        private System.Nullable<System.DateTime> _ServiceStartDate;

        private string _SSO;

        private string _PhoneNumber;

        private string _ProfileID;

        private string _ChargeDescription;

        private decimal _LoyaltyCredit;

        private int _CurrencyID;

        private bool _ManualEntry;

        private string _Notes;

        public GE_LoyaltyCredit_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceStartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ServiceStartDate
        {
            get
            {
                return this._ServiceStartDate;
            }
            set
            {
                if ((this._ServiceStartDate != value))
                {
                    this._ServiceStartDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PhoneNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string PhoneNumber
        {
            get
            {
                return this._PhoneNumber;
            }
            set
            {
                if ((this._PhoneNumber != value))
                {
                    this._PhoneNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProfileID", DbType = "VarChar(255) NOT NULL", CanBeNull = false)]
        public string ProfileID
        {
            get
            {
                return this._ProfileID;
            }
            set
            {
                if ((this._ProfileID != value))
                {
                    this._ProfileID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChargeDescription", DbType = "VarChar(MAX) NOT NULL", CanBeNull = false)]
        public string ChargeDescription
        {
            get
            {
                return this._ChargeDescription;
            }
            set
            {
                if ((this._ChargeDescription != value))
                {
                    this._ChargeDescription = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LoyaltyCredit", DbType = "Decimal(18,5) NOT NULL")]
        public decimal LoyaltyCredit
        {
            get
            {
                return this._LoyaltyCredit;
            }
            set
            {
                if ((this._LoyaltyCredit != value))
                {
                    this._LoyaltyCredit = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyID", DbType = "Int NOT NULL")]
        public int CurrencyID
        {
            get
            {
                return this._CurrencyID;
            }
            set
            {
                if ((this._CurrencyID != value))
                {
                    this._CurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ManualEntry", DbType = "Bit NOT NULL")]
        public bool ManualEntry
        {
            get
            {
                return this._ManualEntry;
            }
            set
            {
                if ((this._ManualEntry != value))
                {
                    this._ManualEntry = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Notes", DbType = "VarChar(MAX)")]
        public string Notes
        {
            get
            {
                return this._Notes;
            }
            set
            {
                if ((this._Notes != value))
                {
                    this._Notes = value;
                }
            }
        }
    }

    public partial class GE_LoyaltyCredit_YearReportResult
    {

        private int _TempProfileID;

        private string _SSO;

        private string _PhoneNumber;

        private System.Nullable<int> _Occurances;

        private System.Nullable<decimal> _TotalCredits;

        public GE_LoyaltyCredit_YearReportResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TempProfileID", DbType = "Int NOT NULL")]
        public int TempProfileID
        {
            get
            {
                return this._TempProfileID;
            }
            set
            {
                if ((this._TempProfileID != value))
                {
                    this._TempProfileID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PhoneNumber", DbType = "VarChar(50)")]
        public string PhoneNumber
        {
            get
            {
                return this._PhoneNumber;
            }
            set
            {
                if ((this._PhoneNumber != value))
                {
                    this._PhoneNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Occurances", DbType = "Int")]
        public System.Nullable<int> Occurances
        {
            get
            {
                return this._Occurances;
            }
            set
            {
                if ((this._Occurances != value))
                {
                    this._Occurances = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalCredits", DbType = "Decimal(18,10)")]
        public System.Nullable<decimal> TotalCredits
        {
            get
            {
                return this._TotalCredits;
            }
            set
            {
                if ((this._TotalCredits != value))
                {
                    this._TotalCredits = value;
                }
            }
        }
    }

    public partial class Invoice_GetByIdResult
    {

        private int _ID;

        private int _InvoiceType;

        private string _InvoiceNumber;

        private int _BillingMonth;

        private int _BillingYear;

        private int _DefaultImportCurrencyID;

        private string _Status;

        private string _LastAction;

        private string _RecordType1_Status;

        private System.Nullable<System.DateTime> _RecordType1_DateTime;

        private string _RecordType2_Status;

        private System.Nullable<System.DateTime> _RecordType2_DateTime;

        private string _RecordType3_Status;

        private System.Nullable<System.DateTime> _RecordType3_DateTime;

        private string _RecordType4_Status;

        private System.Nullable<System.DateTime> _RecordType4_DateTime;

        private string _RecordType5_Status;

        private System.Nullable<System.DateTime> _RecordType5_DateTime;

        private string _BillingFileExport_Status;

        private System.Nullable<System.DateTime> _BillingFileExport_DateTime;

        private string _BillingFileExport_Path;

        private int _ExportCurrencyID;

        private System.DateTime _CreatedDate;

        private string _CreatedBy;

        private System.DateTime _LastUpdatedDate;

        private string _LastUpdatedBy;

        private System.Nullable<int> _ServiceDeskSnapshot_id;

        public Invoice_GetByIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingMonth", DbType = "Int NOT NULL")]
        public int BillingMonth
        {
            get
            {
                return this._BillingMonth;
            }
            set
            {
                if ((this._BillingMonth != value))
                {
                    this._BillingMonth = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingYear", DbType = "Int NOT NULL")]
        public int BillingYear
        {
            get
            {
                return this._BillingYear;
            }
            set
            {
                if ((this._BillingYear != value))
                {
                    this._BillingYear = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DefaultImportCurrencyID", DbType = "Int NOT NULL")]
        public int DefaultImportCurrencyID
        {
            get
            {
                return this._DefaultImportCurrencyID;
            }
            set
            {
                if ((this._DefaultImportCurrencyID != value))
                {
                    this._DefaultImportCurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this._Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastAction", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string LastAction
        {
            get
            {
                return this._LastAction;
            }
            set
            {
                if ((this._LastAction != value))
                {
                    this._LastAction = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType1_Status", DbType = "VarChar(50)")]
        public string RecordType1_Status
        {
            get
            {
                return this._RecordType1_Status;
            }
            set
            {
                if ((this._RecordType1_Status != value))
                {
                    this._RecordType1_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType1_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecordType1_DateTime
        {
            get
            {
                return this._RecordType1_DateTime;
            }
            set
            {
                if ((this._RecordType1_DateTime != value))
                {
                    this._RecordType1_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType2_Status", DbType = "VarChar(50)")]
        public string RecordType2_Status
        {
            get
            {
                return this._RecordType2_Status;
            }
            set
            {
                if ((this._RecordType2_Status != value))
                {
                    this._RecordType2_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType2_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecordType2_DateTime
        {
            get
            {
                return this._RecordType2_DateTime;
            }
            set
            {
                if ((this._RecordType2_DateTime != value))
                {
                    this._RecordType2_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType3_Status", DbType = "VarChar(50)")]
        public string RecordType3_Status
        {
            get
            {
                return this._RecordType3_Status;
            }
            set
            {
                if ((this._RecordType3_Status != value))
                {
                    this._RecordType3_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType3_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecordType3_DateTime
        {
            get
            {
                return this._RecordType3_DateTime;
            }
            set
            {
                if ((this._RecordType3_DateTime != value))
                {
                    this._RecordType3_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType4_Status", DbType = "VarChar(50)")]
        public string RecordType4_Status
        {
            get
            {
                return this._RecordType4_Status;
            }
            set
            {
                if ((this._RecordType4_Status != value))
                {
                    this._RecordType4_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType4_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecordType4_DateTime
        {
            get
            {
                return this._RecordType4_DateTime;
            }
            set
            {
                if ((this._RecordType4_DateTime != value))
                {
                    this._RecordType4_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType5_Status", DbType = "VarChar(50)")]
        public string RecordType5_Status
        {
            get
            {
                return this._RecordType5_Status;
            }
            set
            {
                if ((this._RecordType5_Status != value))
                {
                    this._RecordType5_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType5_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecordType5_DateTime
        {
            get
            {
                return this._RecordType5_DateTime;
            }
            set
            {
                if ((this._RecordType5_DateTime != value))
                {
                    this._RecordType5_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingFileExport_Status", DbType = "VarChar(50)")]
        public string BillingFileExport_Status
        {
            get
            {
                return this._BillingFileExport_Status;
            }
            set
            {
                if ((this._BillingFileExport_Status != value))
                {
                    this._BillingFileExport_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingFileExport_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> BillingFileExport_DateTime
        {
            get
            {
                return this._BillingFileExport_DateTime;
            }
            set
            {
                if ((this._BillingFileExport_DateTime != value))
                {
                    this._BillingFileExport_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingFileExport_Path", DbType = "VarChar(8000)")]
        public string BillingFileExport_Path
        {
            get
            {
                return this._BillingFileExport_Path;
            }
            set
            {
                if ((this._BillingFileExport_Path != value))
                {
                    this._BillingFileExport_Path = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExportCurrencyID", DbType = "Int NOT NULL")]
        public int ExportCurrencyID
        {
            get
            {
                return this._ExportCurrencyID;
            }
            set
            {
                if ((this._ExportCurrencyID != value))
                {
                    this._ExportCurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreatedBy", DbType = "VarChar(50)")]
        public string CreatedBy
        {
            get
            {
                return this._CreatedBy;
            }
            set
            {
                if ((this._CreatedBy != value))
                {
                    this._CreatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastUpdatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime LastUpdatedDate
        {
            get
            {
                return this._LastUpdatedDate;
            }
            set
            {
                if ((this._LastUpdatedDate != value))
                {
                    this._LastUpdatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastUpdatedBy", DbType = "VarChar(50)")]
        public string LastUpdatedBy
        {
            get
            {
                return this._LastUpdatedBy;
            }
            set
            {
                if ((this._LastUpdatedBy != value))
                {
                    this._LastUpdatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceDeskSnapshot_id", DbType = "Int")]
        public System.Nullable<int> ServiceDeskSnapshot_id
        {
            get
            {
                return this._ServiceDeskSnapshot_id;
            }
            set
            {
                if ((this._ServiceDeskSnapshot_id != value))
                {
                    this._ServiceDeskSnapshot_id = value;
                }
            }
        }
    }

    public partial class Invoice_GetByTypeResult
    {

        private int _ID;

        private int _InvoiceType;

        private string _InvoiceNumber;

        private int _BillingMonth;

        private int _BillingYear;

        private int _DefaultImportCurrencyID;

        private string _Status;

        private string _LastAction;

        private string _RecordType1_Status;

        private System.Nullable<System.DateTime> _RecordType1_DateTime;

        private string _RecordType2_Status;

        private System.Nullable<System.DateTime> _RecordType2_DateTime;

        private string _RecordType3_Status;

        private System.Nullable<System.DateTime> _RecordType3_DateTime;

        private string _RecordType4_Status;

        private System.Nullable<System.DateTime> _RecordType4_DateTime;

        private string _RecordType5_Status;

        private System.Nullable<System.DateTime> _RecordType5_DateTime;

        private string _BillingFileExport_Status;

        private System.Nullable<System.DateTime> _BillingFileExport_DateTime;

        private string _BillingFileExport_Path;

        private int _ExportCurrencyID;

        private System.DateTime _CreatedDate;

        private string _CreatedBy;

        private System.DateTime _LastUpdatedDate;

        private string _LastUpdatedBy;

        private System.Nullable<int> _ServiceDeskSnapshot_id;

        public Invoice_GetByTypeResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingMonth", DbType = "Int NOT NULL")]
        public int BillingMonth
        {
            get
            {
                return this._BillingMonth;
            }
            set
            {
                if ((this._BillingMonth != value))
                {
                    this._BillingMonth = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingYear", DbType = "Int NOT NULL")]
        public int BillingYear
        {
            get
            {
                return this._BillingYear;
            }
            set
            {
                if ((this._BillingYear != value))
                {
                    this._BillingYear = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DefaultImportCurrencyID", DbType = "Int NOT NULL")]
        public int DefaultImportCurrencyID
        {
            get
            {
                return this._DefaultImportCurrencyID;
            }
            set
            {
                if ((this._DefaultImportCurrencyID != value))
                {
                    this._DefaultImportCurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this._Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastAction", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string LastAction
        {
            get
            {
                return this._LastAction;
            }
            set
            {
                if ((this._LastAction != value))
                {
                    this._LastAction = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType1_Status", DbType = "VarChar(50)")]
        public string RecordType1_Status
        {
            get
            {
                return this._RecordType1_Status;
            }
            set
            {
                if ((this._RecordType1_Status != value))
                {
                    this._RecordType1_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType1_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecordType1_DateTime
        {
            get
            {
                return this._RecordType1_DateTime;
            }
            set
            {
                if ((this._RecordType1_DateTime != value))
                {
                    this._RecordType1_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType2_Status", DbType = "VarChar(50)")]
        public string RecordType2_Status
        {
            get
            {
                return this._RecordType2_Status;
            }
            set
            {
                if ((this._RecordType2_Status != value))
                {
                    this._RecordType2_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType2_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecordType2_DateTime
        {
            get
            {
                return this._RecordType2_DateTime;
            }
            set
            {
                if ((this._RecordType2_DateTime != value))
                {
                    this._RecordType2_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType3_Status", DbType = "VarChar(50)")]
        public string RecordType3_Status
        {
            get
            {
                return this._RecordType3_Status;
            }
            set
            {
                if ((this._RecordType3_Status != value))
                {
                    this._RecordType3_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType3_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecordType3_DateTime
        {
            get
            {
                return this._RecordType3_DateTime;
            }
            set
            {
                if ((this._RecordType3_DateTime != value))
                {
                    this._RecordType3_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType4_Status", DbType = "VarChar(50)")]
        public string RecordType4_Status
        {
            get
            {
                return this._RecordType4_Status;
            }
            set
            {
                if ((this._RecordType4_Status != value))
                {
                    this._RecordType4_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType4_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecordType4_DateTime
        {
            get
            {
                return this._RecordType4_DateTime;
            }
            set
            {
                if ((this._RecordType4_DateTime != value))
                {
                    this._RecordType4_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType5_Status", DbType = "VarChar(50)")]
        public string RecordType5_Status
        {
            get
            {
                return this._RecordType5_Status;
            }
            set
            {
                if ((this._RecordType5_Status != value))
                {
                    this._RecordType5_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType5_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecordType5_DateTime
        {
            get
            {
                return this._RecordType5_DateTime;
            }
            set
            {
                if ((this._RecordType5_DateTime != value))
                {
                    this._RecordType5_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingFileExport_Status", DbType = "VarChar(50)")]
        public string BillingFileExport_Status
        {
            get
            {
                return this._BillingFileExport_Status;
            }
            set
            {
                if ((this._BillingFileExport_Status != value))
                {
                    this._BillingFileExport_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingFileExport_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> BillingFileExport_DateTime
        {
            get
            {
                return this._BillingFileExport_DateTime;
            }
            set
            {
                if ((this._BillingFileExport_DateTime != value))
                {
                    this._BillingFileExport_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingFileExport_Path", DbType = "VarChar(8000)")]
        public string BillingFileExport_Path
        {
            get
            {
                return this._BillingFileExport_Path;
            }
            set
            {
                if ((this._BillingFileExport_Path != value))
                {
                    this._BillingFileExport_Path = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExportCurrencyID", DbType = "Int NOT NULL")]
        public int ExportCurrencyID
        {
            get
            {
                return this._ExportCurrencyID;
            }
            set
            {
                if ((this._ExportCurrencyID != value))
                {
                    this._ExportCurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreatedBy", DbType = "VarChar(50)")]
        public string CreatedBy
        {
            get
            {
                return this._CreatedBy;
            }
            set
            {
                if ((this._CreatedBy != value))
                {
                    this._CreatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastUpdatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime LastUpdatedDate
        {
            get
            {
                return this._LastUpdatedDate;
            }
            set
            {
                if ((this._LastUpdatedDate != value))
                {
                    this._LastUpdatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastUpdatedBy", DbType = "VarChar(50)")]
        public string LastUpdatedBy
        {
            get
            {
                return this._LastUpdatedBy;
            }
            set
            {
                if ((this._LastUpdatedBy != value))
                {
                    this._LastUpdatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceDeskSnapshot_id", DbType = "Int")]
        public System.Nullable<int> ServiceDeskSnapshot_id
        {
            get
            {
                return this._ServiceDeskSnapshot_id;
            }
            set
            {
                if ((this._ServiceDeskSnapshot_id != value))
                {
                    this._ServiceDeskSnapshot_id = value;
                }
            }
        }
    }

    public partial class InvoiceFileUploads_GetByInvoiceResult
    {

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _FileType;

        private string _FileDescription;

        private string _FilePath;

        private string _ImportStatus;

        private System.DateTime _CreatedDate;

        private System.DateTime _LastModifiedDate;

        private string _LastUpdatedBy;

        public InvoiceFileUploads_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FileType", DbType = "Int NOT NULL")]
        public int FileType
        {
            get
            {
                return this._FileType;
            }
            set
            {
                if ((this._FileType != value))
                {
                    this._FileType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FileDescription", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string FileDescription
        {
            get
            {
                return this._FileDescription;
            }
            set
            {
                if ((this._FileDescription != value))
                {
                    this._FileDescription = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FilePath", DbType = "VarChar(MAX)")]
        public string FilePath
        {
            get
            {
                return this._FilePath;
            }
            set
            {
                if ((this._FilePath != value))
                {
                    this._FilePath = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ImportStatus", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ImportStatus
        {
            get
            {
                return this._ImportStatus;
            }
            set
            {
                if ((this._ImportStatus != value))
                {
                    this._ImportStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastModifiedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime LastModifiedDate
        {
            get
            {
                return this._LastModifiedDate;
            }
            set
            {
                if ((this._LastModifiedDate != value))
                {
                    this._LastModifiedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastUpdatedBy", DbType = "VarChar(50)")]
        public string LastUpdatedBy
        {
            get
            {
                return this._LastUpdatedBy;
            }
            set
            {
                if ((this._LastUpdatedBy != value))
                {
                    this._LastUpdatedBy = value;
                }
            }
        }
    }

    public partial class InvoiceType_GetResult
    {

        private int _ID;

        private string _InvoiceProfile;

        private string _Prefix;

        private string _VendorName;

        private string _ImportCurrencyDefault;

        private string _ExportCurrencyDefault;

        private string _BAN;

        public InvoiceType_GetResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceProfile", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceProfile
        {
            get
            {
                return this._InvoiceProfile;
            }
            set
            {
                if ((this._InvoiceProfile != value))
                {
                    this._InvoiceProfile = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Prefix", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Prefix
        {
            get
            {
                return this._Prefix;
            }
            set
            {
                if ((this._Prefix != value))
                {
                    this._Prefix = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VendorName", DbType = "VarChar(50)")]
        public string VendorName
        {
            get
            {
                return this._VendorName;
            }
            set
            {
                if ((this._VendorName != value))
                {
                    this._VendorName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ImportCurrencyDefault", DbType = "Char(3) NOT NULL", CanBeNull = false)]
        public string ImportCurrencyDefault
        {
            get
            {
                return this._ImportCurrencyDefault;
            }
            set
            {
                if ((this._ImportCurrencyDefault != value))
                {
                    this._ImportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExportCurrencyDefault", DbType = "Char(3) NOT NULL", CanBeNull = false)]
        public string ExportCurrencyDefault
        {
            get
            {
                return this._ExportCurrencyDefault;
            }
            set
            {
                if ((this._ExportCurrencyDefault != value))
                {
                    this._ExportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }
    }

    public partial class ProcessLog_GetResult
    {

        private int _ID;

        private int _InvoiceID;

        private System.Nullable<int> _InvoiceType;

        private string _InvoiceNumber;

        private string _ProcessName;

        private string _ProcessResult;

        private System.DateTime _ProcessDateTime;

        private string _CreatedBy;

        private string _Comment;

        public ProcessLog_GetResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int")]
        public System.Nullable<int> InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50)")]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProcessName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ProcessName
        {
            get
            {
                return this._ProcessName;
            }
            set
            {
                if ((this._ProcessName != value))
                {
                    this._ProcessName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProcessResult", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ProcessResult
        {
            get
            {
                return this._ProcessResult;
            }
            set
            {
                if ((this._ProcessResult != value))
                {
                    this._ProcessResult = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProcessDateTime", DbType = "DateTime NOT NULL")]
        public System.DateTime ProcessDateTime
        {
            get
            {
                return this._ProcessDateTime;
            }
            set
            {
                if ((this._ProcessDateTime != value))
                {
                    this._ProcessDateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreatedBy", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string CreatedBy
        {
            get
            {
                return this._CreatedBy;
            }
            set
            {
                if ((this._CreatedBy != value))
                {
                    this._CreatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Comment", DbType = "VarChar(8000)")]
        public string Comment
        {
            get
            {
                return this._Comment;
            }
            set
            {
                if ((this._Comment != value))
                {
                    this._Comment = value;
                }
            }
        }
    }

    public partial class Profiles_GetActiveByDateResult
    {

        private string _SD_Bundle_Number;

        private string _Short_Description_EN;

        private string _Long_Description_EN;

        private decimal _Price_to_GIS;

        private decimal _Catalog_Price;

        private System.Nullable<char> _Phone;

        private string _Charge_Type;

        private bool _BaseProfile;

        private int _FirstActiveMonth;

        private int _FirstActiveYear;

        private System.Nullable<System.DateTime> _ActiveDate;

        public Profiles_GetActiveByDateResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[SD Bundle Number]", Storage = "_SD_Bundle_Number", DbType = "VarChar(255) NOT NULL", CanBeNull = false)]
        public string SD_Bundle_Number
        {
            get
            {
                return this._SD_Bundle_Number;
            }
            set
            {
                if ((this._SD_Bundle_Number != value))
                {
                    this._SD_Bundle_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Short_Description_EN", DbType = "VarChar(MAX) NOT NULL", CanBeNull = false)]
        public string Short_Description_EN
        {
            get
            {
                return this._Short_Description_EN;
            }
            set
            {
                if ((this._Short_Description_EN != value))
                {
                    this._Short_Description_EN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Long_Description_EN", DbType = "VarChar(MAX)")]
        public string Long_Description_EN
        {
            get
            {
                return this._Long_Description_EN;
            }
            set
            {
                if ((this._Long_Description_EN != value))
                {
                    this._Long_Description_EN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Price to GIS]", Storage = "_Price_to_GIS", DbType = "Decimal(18,5) NOT NULL")]
        public decimal Price_to_GIS
        {
            get
            {
                return this._Price_to_GIS;
            }
            set
            {
                if ((this._Price_to_GIS != value))
                {
                    this._Price_to_GIS = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Catalog Price]", Storage = "_Catalog_Price", DbType = "Decimal(18,5) NOT NULL")]
        public decimal Catalog_Price
        {
            get
            {
                return this._Catalog_Price;
            }
            set
            {
                if ((this._Catalog_Price != value))
                {
                    this._Catalog_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Phone", DbType = "Char(1)")]
        public System.Nullable<char> Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                if ((this._Phone != value))
                {
                    this._Phone = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Type]", Storage = "_Charge_Type", DbType = "VarChar(50)")]
        public string Charge_Type
        {
            get
            {
                return this._Charge_Type;
            }
            set
            {
                if ((this._Charge_Type != value))
                {
                    this._Charge_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BaseProfile", DbType = "Bit NOT NULL")]
        public bool BaseProfile
        {
            get
            {
                return this._BaseProfile;
            }
            set
            {
                if ((this._BaseProfile != value))
                {
                    this._BaseProfile = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FirstActiveMonth", DbType = "Int NOT NULL")]
        public int FirstActiveMonth
        {
            get
            {
                return this._FirstActiveMonth;
            }
            set
            {
                if ((this._FirstActiveMonth != value))
                {
                    this._FirstActiveMonth = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FirstActiveYear", DbType = "Int NOT NULL")]
        public int FirstActiveYear
        {
            get
            {
                return this._FirstActiveYear;
            }
            set
            {
                if ((this._FirstActiveYear != value))
                {
                    this._FirstActiveYear = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActiveDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ActiveDate
        {
            get
            {
                return this._ActiveDate;
            }
            set
            {
                if ((this._ActiveDate != value))
                {
                    this._ActiveDate = value;
                }
            }
        }

    }

    public partial class Profiles_GetActiveByInvoiceResult
    {

        private string _SD_Bundle_Number;

        private string _Short_Description_EN;

        private string _Long_Description_EN;

        private decimal _Price_to_GIS;

        private decimal _Catalog_Price;

        private System.Nullable<char> _Phone;

        private string _Charge_Type;

        private bool _BaseProfile;

        private int _FirstActiveMonth;

        private int _FirstActiveYear;

        private System.Nullable<System.DateTime> _ActiveDate;

        public Profiles_GetActiveByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[SD Bundle Number]", Storage = "_SD_Bundle_Number", DbType = "VarChar(255) NOT NULL", CanBeNull = false)]
        public string SD_Bundle_Number
        {
            get
            {
                return this._SD_Bundle_Number;
            }
            set
            {
                if ((this._SD_Bundle_Number != value))
                {
                    this._SD_Bundle_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Short_Description_EN", DbType = "VarChar(MAX) NOT NULL", CanBeNull = false)]
        public string Short_Description_EN
        {
            get
            {
                return this._Short_Description_EN;
            }
            set
            {
                if ((this._Short_Description_EN != value))
                {
                    this._Short_Description_EN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Long_Description_EN", DbType = "VarChar(MAX)")]
        public string Long_Description_EN
        {
            get
            {
                return this._Long_Description_EN;
            }
            set
            {
                if ((this._Long_Description_EN != value))
                {
                    this._Long_Description_EN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Price to GIS]", Storage = "_Price_to_GIS", DbType = "Decimal(18,5) NOT NULL")]
        public decimal Price_to_GIS
        {
            get
            {
                return this._Price_to_GIS;
            }
            set
            {
                if ((this._Price_to_GIS != value))
                {
                    this._Price_to_GIS = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Catalog Price]", Storage = "_Catalog_Price", DbType = "Decimal(18,5) NOT NULL")]
        public decimal Catalog_Price
        {
            get
            {
                return this._Catalog_Price;
            }
            set
            {
                if ((this._Catalog_Price != value))
                {
                    this._Catalog_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Phone", DbType = "Char(1)")]
        public System.Nullable<char> Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                if ((this._Phone != value))
                {
                    this._Phone = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Type]", Storage = "_Charge_Type", DbType = "VarChar(50)")]
        public string Charge_Type
        {
            get
            {
                return this._Charge_Type;
            }
            set
            {
                if ((this._Charge_Type != value))
                {
                    this._Charge_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BaseProfile", DbType = "Bit NOT NULL")]
        public bool BaseProfile
        {
            get
            {
                return this._BaseProfile;
            }
            set
            {
                if ((this._BaseProfile != value))
                {
                    this._BaseProfile = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FirstActiveMonth", DbType = "Int NOT NULL")]
        public int FirstActiveMonth
        {
            get
            {
                return this._FirstActiveMonth;
            }
            set
            {
                if ((this._FirstActiveMonth != value))
                {
                    this._FirstActiveMonth = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FirstActiveYear", DbType = "Int NOT NULL")]
        public int FirstActiveYear
        {
            get
            {
                return this._FirstActiveYear;
            }
            set
            {
                if ((this._FirstActiveYear != value))
                {
                    this._FirstActiveYear = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActiveDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ActiveDate
        {
            get
            {
                return this._ActiveDate;
            }
            set
            {
                if ((this._ActiveDate != value))
                {
                    this._ActiveDate = value;
                }
            }
        }
    }

    public partial class RecordType1_GetArtifactCountForInvoiceImportResult
    {

        private System.Nullable<int> _count;

        public RecordType1_GetArtifactCountForInvoiceImportResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_count", DbType = "Int")]
        public System.Nullable<int> count
        {
            get
            {
                return this._count;
            }
            set
            {
                if ((this._count != value))
                {
                    this._count = value;
                }
            }
        }
    }

    public partial class RecordType1_GetByInvoiceResult
    {

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string _Bill_Date;

        private string _SSO;

        private string _From_Number__DID_;

        private string _From_To_Number;

        private System.Nullable<decimal> _Charge_Amount;

        private string _Date_of_Record;

        private string _Connect_Time;

        private System.Nullable<decimal> _Billable_Time;

        private string _Billing_Number_North_American_Standard;

        private string _From_Place;

        private string _From_State;

        private string _To_Place;

        private string _To_State;

        private System.Nullable<int> _Settlement_Code;

        private string _Charge_Description;

        private string _Provider;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        public RecordType1_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Bill Date]", Storage = "_Bill_Date", DbType = "VarChar(50)")]
        public string Bill_Date
        {
            get
            {
                return this._Bill_Date;
            }
            set
            {
                if ((this._Bill_Date != value))
                {
                    this._Bill_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From Number (DID)]", Storage = "_From_Number__DID_", DbType = "VarChar(50)")]
        public string From_Number__DID_
        {
            get
            {
                return this._From_Number__DID_;
            }
            set
            {
                if ((this._From_Number__DID_ != value))
                {
                    this._From_Number__DID_ = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From/To Number]", Storage = "_From_To_Number", DbType = "VarChar(50)")]
        public string From_To_Number
        {
            get
            {
                return this._From_To_Number;
            }
            set
            {
                if ((this._From_To_Number != value))
                {
                    this._From_To_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Amount]", Storage = "_Charge_Amount", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Charge_Amount
        {
            get
            {
                return this._Charge_Amount;
            }
            set
            {
                if ((this._Charge_Amount != value))
                {
                    this._Charge_Amount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Date of Record]", Storage = "_Date_of_Record", DbType = "VarChar(50)")]
        public string Date_of_Record
        {
            get
            {
                return this._Date_of_Record;
            }
            set
            {
                if ((this._Date_of_Record != value))
                {
                    this._Date_of_Record = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Connect Time]", Storage = "_Connect_Time", DbType = "VarChar(11)")]
        public string Connect_Time
        {
            get
            {
                return this._Connect_Time;
            }
            set
            {
                if ((this._Connect_Time != value))
                {
                    this._Connect_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Billable Time]", Storage = "_Billable_Time", DbType = "Decimal(18,4)")]
        public System.Nullable<decimal> Billable_Time
        {
            get
            {
                return this._Billable_Time;
            }
            set
            {
                if ((this._Billable_Time != value))
                {
                    this._Billable_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Billing Number North American Standard]", Storage = "_Billing_Number_North_American_Standard", DbType = "VarChar(8000)")]
        public string Billing_Number_North_American_Standard
        {
            get
            {
                return this._Billing_Number_North_American_Standard;
            }
            set
            {
                if ((this._Billing_Number_North_American_Standard != value))
                {
                    this._Billing_Number_North_American_Standard = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From Place]", Storage = "_From_Place", DbType = "VarChar(50)")]
        public string From_Place
        {
            get
            {
                return this._From_Place;
            }
            set
            {
                if ((this._From_Place != value))
                {
                    this._From_Place = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From State]", Storage = "_From_State", DbType = "VarChar(50)")]
        public string From_State
        {
            get
            {
                return this._From_State;
            }
            set
            {
                if ((this._From_State != value))
                {
                    this._From_State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[To Place]", Storage = "_To_Place", DbType = "VarChar(50)")]
        public string To_Place
        {
            get
            {
                return this._To_Place;
            }
            set
            {
                if ((this._To_Place != value))
                {
                    this._To_Place = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[To State]", Storage = "_To_State", DbType = "VarChar(50)")]
        public string To_State
        {
            get
            {
                return this._To_State;
            }
            set
            {
                if ((this._To_State != value))
                {
                    this._To_State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Settlement Code]", Storage = "_Settlement_Code", DbType = "Int")]
        public System.Nullable<int> Settlement_Code
        {
            get
            {
                return this._Settlement_Code;
            }
            set
            {
                if ((this._Settlement_Code != value))
                {
                    this._Settlement_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(50)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Provider", DbType = "VarChar(50)")]
        public string Provider
        {
            get
            {
                return this._Provider;
            }
            set
            {
                if ((this._Provider != value))
                {
                    this._Provider = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType1_GetExceptionsByInvoiceResult
    {

        private string _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string _Bill_Date;

        private string _SSO;

        private string _From_Number__DID_;

        private string _DIDNumber;

        private string _From_To_Number;

        private System.Nullable<decimal> _Charge_Amount;

        private string _Date_of_Record;

        private string _Connect_Time;

        private System.Nullable<decimal> _Billable_Time;

        private string _Billing_Number_North_American_Standard;

        private string _From_Place;

        private string _From_State;

        private string _To_Place;

        private string _To_State;

        private System.Nullable<int> _Settlement_Code;

        private string _Charge_Description;

        private string _Provider;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        public RecordType1_GetExceptionsByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "VarChar(9) NOT NULL", CanBeNull = false)]
        public string Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Bill Date]", Storage = "_Bill_Date", DbType = "VarChar(50)")]
        public string Bill_Date
        {
            get
            {
                return this._Bill_Date;
            }
            set
            {
                if ((this._Bill_Date != value))
                {
                    this._Bill_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From Number (DID)]", Storage = "_From_Number__DID_", DbType = "VarChar(50)")]
        public string From_Number__DID_
        {
            get
            {
                return this._From_Number__DID_;
            }
            set
            {
                if ((this._From_Number__DID_ != value))
                {
                    this._From_Number__DID_ = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DIDNumber", DbType = "VarChar(50)")]
        public string DIDNumber
        {
            get
            {
                return this._DIDNumber;
            }
            set
            {
                if ((this._DIDNumber != value))
                {
                    this._DIDNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From/To Number]", Storage = "_From_To_Number", DbType = "VarChar(50)")]
        public string From_To_Number
        {
            get
            {
                return this._From_To_Number;
            }
            set
            {
                if ((this._From_To_Number != value))
                {
                    this._From_To_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Amount]", Storage = "_Charge_Amount", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Charge_Amount
        {
            get
            {
                return this._Charge_Amount;
            }
            set
            {
                if ((this._Charge_Amount != value))
                {
                    this._Charge_Amount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Date of Record]", Storage = "_Date_of_Record", DbType = "VarChar(50)")]
        public string Date_of_Record
        {
            get
            {
                return this._Date_of_Record;
            }
            set
            {
                if ((this._Date_of_Record != value))
                {
                    this._Date_of_Record = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Connect Time]", Storage = "_Connect_Time", DbType = "VarChar(11)")]
        public string Connect_Time
        {
            get
            {
                return this._Connect_Time;
            }
            set
            {
                if ((this._Connect_Time != value))
                {
                    this._Connect_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Billable Time]", Storage = "_Billable_Time", DbType = "Decimal(18,4)")]
        public System.Nullable<decimal> Billable_Time
        {
            get
            {
                return this._Billable_Time;
            }
            set
            {
                if ((this._Billable_Time != value))
                {
                    this._Billable_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Billing Number North American Standard]", Storage = "_Billing_Number_North_American_Standard", DbType = "VarChar(8000)")]
        public string Billing_Number_North_American_Standard
        {
            get
            {
                return this._Billing_Number_North_American_Standard;
            }
            set
            {
                if ((this._Billing_Number_North_American_Standard != value))
                {
                    this._Billing_Number_North_American_Standard = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From Place]", Storage = "_From_Place", DbType = "VarChar(50)")]
        public string From_Place
        {
            get
            {
                return this._From_Place;
            }
            set
            {
                if ((this._From_Place != value))
                {
                    this._From_Place = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From State]", Storage = "_From_State", DbType = "VarChar(50)")]
        public string From_State
        {
            get
            {
                return this._From_State;
            }
            set
            {
                if ((this._From_State != value))
                {
                    this._From_State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[To Place]", Storage = "_To_Place", DbType = "VarChar(50)")]
        public string To_Place
        {
            get
            {
                return this._To_Place;
            }
            set
            {
                if ((this._To_Place != value))
                {
                    this._To_Place = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[To State]", Storage = "_To_State", DbType = "VarChar(50)")]
        public string To_State
        {
            get
            {
                return this._To_State;
            }
            set
            {
                if ((this._To_State != value))
                {
                    this._To_State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Settlement Code]", Storage = "_Settlement_Code", DbType = "Int")]
        public System.Nullable<int> Settlement_Code
        {
            get
            {
                return this._Settlement_Code;
            }
            set
            {
                if ((this._Settlement_Code != value))
                {
                    this._Settlement_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(50)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Provider", DbType = "VarChar(50)")]
        public string Provider
        {
            get
            {
                return this._Provider;
            }
            set
            {
                if ((this._Provider != value))
                {
                    this._Provider = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType1_ValidateInvoiceResult
    {

        private string _BAN;

        private System.Nullable<decimal> _RecordType1_Sum;

        private System.Nullable<decimal> _RecordType3_Sum;

        private System.Nullable<decimal> _RecordType5_Sum;

        private System.Nullable<bool> _Validated;

        public RecordType1_ValidateInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(6) NOT NULL", CanBeNull = false)]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType1_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType1_Sum
        {
            get
            {
                return this._RecordType1_Sum;
            }
            set
            {
                if ((this._RecordType1_Sum != value))
                {
                    this._RecordType1_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType3_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType3_Sum
        {
            get
            {
                return this._RecordType3_Sum;
            }
            set
            {
                if ((this._RecordType3_Sum != value))
                {
                    this._RecordType3_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType5_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType5_Sum
        {
            get
            {
                return this._RecordType5_Sum;
            }
            set
            {
                if ((this._RecordType5_Sum != value))
                {
                    this._RecordType5_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Validated", DbType = "Bit")]
        public System.Nullable<bool> Validated
        {
            get
            {
                return this._Validated;
            }
            set
            {
                if ((this._Validated != value))
                {
                    this._Validated = value;
                }
            }
        }
    }

    public partial class RecordType2_GetByInvoiceResult
    {

        private int _ID;

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string @__10_Digit_DID;

        private string _SSO;

        private string _Charge_Date;

        private string _Charge_Description;

        private string _Charge_Type;

        private string _Service_Start_Date;

        private string _Service_End_Date;

        private string _Invoice_Bill_Period_Start;

        private string _Invoice_Bill_Period_End;

        private System.Nullable<decimal> _Total;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        public RecordType2_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 Digit DID]", Storage = "__10_Digit_DID", DbType = "VarChar(8000)")]
        public string _10_Digit_DID
        {
            get
            {
                return this.@__10_Digit_DID;
            }
            set
            {
                if ((this.@__10_Digit_DID != value))
                {
                    this.@__10_Digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Date]", Storage = "_Charge_Date", DbType = "VarChar(50)")]
        public string Charge_Date
        {
            get
            {
                return this._Charge_Date;
            }
            set
            {
                if ((this._Charge_Date != value))
                {
                    this._Charge_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(255)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Type]", Storage = "_Charge_Type", DbType = "VarChar(50)")]
        public string Charge_Type
        {
            get
            {
                return this._Charge_Type;
            }
            set
            {
                if ((this._Charge_Type != value))
                {
                    this._Charge_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service Start Date]", Storage = "_Service_Start_Date", DbType = "VarChar(8000)")]
        public string Service_Start_Date
        {
            get
            {
                return this._Service_Start_Date;
            }
            set
            {
                if ((this._Service_Start_Date != value))
                {
                    this._Service_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service End Date]", Storage = "_Service_End_Date", DbType = "VarChar(8000)")]
        public string Service_End_Date
        {
            get
            {
                return this._Service_End_Date;
            }
            set
            {
                if ((this._Service_End_Date != value))
                {
                    this._Service_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period Start]", Storage = "_Invoice_Bill_Period_Start", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_Start
        {
            get
            {
                return this._Invoice_Bill_Period_Start;
            }
            set
            {
                if ((this._Invoice_Bill_Period_Start != value))
                {
                    this._Invoice_Bill_Period_Start = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period End]", Storage = "_Invoice_Bill_Period_End", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_End
        {
            get
            {
                return this._Invoice_Bill_Period_End;
            }
            set
            {
                if ((this._Invoice_Bill_Period_End != value))
                {
                    this._Invoice_Bill_Period_End = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType2_GetByInvoiceWithoutGatewaysResult
    {

        private int _ID;

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string @__10_Digit_DID;

        private string _SSO;

        private string _Charge_Date;

        private string _Charge_Description;

        private string _Charge_Type;

        private string _Service_Start_Date;

        private string _Service_End_Date;

        private string _Invoice_Bill_Period_Start;

        private string _Invoice_Bill_Period_End;

        private System.Nullable<decimal> _Total;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        public RecordType2_GetByInvoiceWithoutGatewaysResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 Digit DID]", Storage = "__10_Digit_DID", DbType = "VarChar(8000)")]
        public string _10_Digit_DID
        {
            get
            {
                return this.@__10_Digit_DID;
            }
            set
            {
                if ((this.@__10_Digit_DID != value))
                {
                    this.@__10_Digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Date]", Storage = "_Charge_Date", DbType = "VarChar(50)")]
        public string Charge_Date
        {
            get
            {
                return this._Charge_Date;
            }
            set
            {
                if ((this._Charge_Date != value))
                {
                    this._Charge_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(255)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Type]", Storage = "_Charge_Type", DbType = "VarChar(50)")]
        public string Charge_Type
        {
            get
            {
                return this._Charge_Type;
            }
            set
            {
                if ((this._Charge_Type != value))
                {
                    this._Charge_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service Start Date]", Storage = "_Service_Start_Date", DbType = "VarChar(8000)")]
        public string Service_Start_Date
        {
            get
            {
                return this._Service_Start_Date;
            }
            set
            {
                if ((this._Service_Start_Date != value))
                {
                    this._Service_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service End Date]", Storage = "_Service_End_Date", DbType = "VarChar(8000)")]
        public string Service_End_Date
        {
            get
            {
                return this._Service_End_Date;
            }
            set
            {
                if ((this._Service_End_Date != value))
                {
                    this._Service_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period Start]", Storage = "_Invoice_Bill_Period_Start", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_Start
        {
            get
            {
                return this._Invoice_Bill_Period_Start;
            }
            set
            {
                if ((this._Invoice_Bill_Period_Start != value))
                {
                    this._Invoice_Bill_Period_Start = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period End]", Storage = "_Invoice_Bill_Period_End", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_End
        {
            get
            {
                return this._Invoice_Bill_Period_End;
            }
            set
            {
                if ((this._Invoice_Bill_Period_End != value))
                {
                    this._Invoice_Bill_Period_End = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType2_GetExceptionsByInvoiceResult
    {

        private string _RecordType;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string _p_Asset_Search_Code;

        private string _SSO;

        private string _ChargeDate;

        private string _ChargeDescription;

        private string _ChargeType;

        private System.Nullable<System.DateTime> _ServiceStartDate;

        private System.Nullable<System.DateTime> _ServiceStopDate;

        private System.Nullable<System.DateTime> _InvoiceBillPeriodStart;

        private System.Nullable<System.DateTime> _InvoiceBillPeriodEnd;

        private System.Nullable<decimal> _Total;

        private int _CurrencyID;

        public RecordType2_GetExceptionsByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType", DbType = "VarChar(1) NOT NULL", CanBeNull = false)]
        public string RecordType
        {
            get
            {
                return this._RecordType;
            }
            set
            {
                if ((this._RecordType != value))
                {
                    this._RecordType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50)")]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[p_Asset Search Code]", Storage = "_p_Asset_Search_Code", DbType = "VarChar(50)")]
        public string p_Asset_Search_Code
        {
            get
            {
                return this._p_Asset_Search_Code;
            }
            set
            {
                if ((this._p_Asset_Search_Code != value))
                {
                    this._p_Asset_Search_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChargeDate", DbType = "VarChar(50)")]
        public string ChargeDate
        {
            get
            {
                return this._ChargeDate;
            }
            set
            {
                if ((this._ChargeDate != value))
                {
                    this._ChargeDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChargeDescription", DbType = "VarChar(50)")]
        public string ChargeDescription
        {
            get
            {
                return this._ChargeDescription;
            }
            set
            {
                if ((this._ChargeDescription != value))
                {
                    this._ChargeDescription = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChargeType", DbType = "VarChar(50)")]
        public string ChargeType
        {
            get
            {
                return this._ChargeType;
            }
            set
            {
                if ((this._ChargeType != value))
                {
                    this._ChargeType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceStartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ServiceStartDate
        {
            get
            {
                return this._ServiceStartDate;
            }
            set
            {
                if ((this._ServiceStartDate != value))
                {
                    this._ServiceStartDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceStopDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ServiceStopDate
        {
            get
            {
                return this._ServiceStopDate;
            }
            set
            {
                if ((this._ServiceStopDate != value))
                {
                    this._ServiceStopDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceBillPeriodStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> InvoiceBillPeriodStart
        {
            get
            {
                return this._InvoiceBillPeriodStart;
            }
            set
            {
                if ((this._InvoiceBillPeriodStart != value))
                {
                    this._InvoiceBillPeriodStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceBillPeriodEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> InvoiceBillPeriodEnd
        {
            get
            {
                return this._InvoiceBillPeriodEnd;
            }
            set
            {
                if ((this._InvoiceBillPeriodEnd != value))
                {
                    this._InvoiceBillPeriodEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyID", DbType = "Int NOT NULL")]
        public int CurrencyID
        {
            get
            {
                return this._CurrencyID;
            }
            set
            {
                if ((this._CurrencyID != value))
                {
                    this._CurrencyID = value;
                }
            }
        }
    }

    public partial class RecordType2_GetGatewaysByInvoiceIDResult
    {

        private int _ID;

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string @__10_Digit_DID;

        private string _SSO;

        private string _Charge_Date;

        private string _Charge_Description;

        private string _Charge_Type;

        private string _Service_Start_Date;

        private string _Service_End_Date;

        private string _Invoice_Bill_Period_Start;

        private string _Invoice_Bill_Period_End;

        private System.Nullable<decimal> _Total;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        public RecordType2_GetGatewaysByInvoiceIDResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50)")]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 Digit DID]", Storage = "__10_Digit_DID", DbType = "VarChar(8000)")]
        public string _10_Digit_DID
        {
            get
            {
                return this.@__10_Digit_DID;
            }
            set
            {
                if ((this.@__10_Digit_DID != value))
                {
                    this.@__10_Digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Date]", Storage = "_Charge_Date", DbType = "VarChar(50)")]
        public string Charge_Date
        {
            get
            {
                return this._Charge_Date;
            }
            set
            {
                if ((this._Charge_Date != value))
                {
                    this._Charge_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(255)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Type]", Storage = "_Charge_Type", DbType = "VarChar(50)")]
        public string Charge_Type
        {
            get
            {
                return this._Charge_Type;
            }
            set
            {
                if ((this._Charge_Type != value))
                {
                    this._Charge_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service Start Date]", Storage = "_Service_Start_Date", DbType = "VarChar(8000)")]
        public string Service_Start_Date
        {
            get
            {
                return this._Service_Start_Date;
            }
            set
            {
                if ((this._Service_Start_Date != value))
                {
                    this._Service_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service End Date]", Storage = "_Service_End_Date", DbType = "VarChar(8000)")]
        public string Service_End_Date
        {
            get
            {
                return this._Service_End_Date;
            }
            set
            {
                if ((this._Service_End_Date != value))
                {
                    this._Service_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period Start]", Storage = "_Invoice_Bill_Period_Start", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_Start
        {
            get
            {
                return this._Invoice_Bill_Period_Start;
            }
            set
            {
                if ((this._Invoice_Bill_Period_Start != value))
                {
                    this._Invoice_Bill_Period_Start = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period End]", Storage = "_Invoice_Bill_Period_End", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_End
        {
            get
            {
                return this._Invoice_Bill_Period_End;
            }
            set
            {
                if ((this._Invoice_Bill_Period_End != value))
                {
                    this._Invoice_Bill_Period_End = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType2_ValidateInvoiceResult
    {

        private string _BAN;

        private System.Nullable<decimal> _RecordType2_Sum;

        private System.Nullable<decimal> _RecordType3_Sum;

        private System.Nullable<decimal> _RecordType5_Sum;

        private System.Nullable<bool> _Validated;

        public RecordType2_ValidateInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(6) NOT NULL", CanBeNull = false)]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType2_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType2_Sum
        {
            get
            {
                return this._RecordType2_Sum;
            }
            set
            {
                if ((this._RecordType2_Sum != value))
                {
                    this._RecordType2_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType3_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType3_Sum
        {
            get
            {
                return this._RecordType3_Sum;
            }
            set
            {
                if ((this._RecordType3_Sum != value))
                {
                    this._RecordType3_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType5_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType5_Sum
        {
            get
            {
                return this._RecordType5_Sum;
            }
            set
            {
                if ((this._RecordType5_Sum != value))
                {
                    this._RecordType5_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Validated", DbType = "Bit")]
        public System.Nullable<bool> Validated
        {
            get
            {
                return this._Validated;
            }
            set
            {
                if ((this._Validated != value))
                {
                    this._Validated = value;
                }
            }
        }
    }

    public partial class RecordType3_GetByInvoiceResult
    {

        private int _ID;

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string _Bill_Date;

        private System.Nullable<decimal> _Record_Type_1_Total;

        private System.Nullable<decimal> _Record_Type_2_Total;

        private System.Nullable<decimal> _Record_Type_4_Total;

        private System.Nullable<decimal> _Total;

        private int _CurrencyConversionID;

        public RecordType3_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Bill Date]", Storage = "_Bill_Date", DbType = "VarChar(50)")]
        public string Bill_Date
        {
            get
            {
                return this._Bill_Date;
            }
            set
            {
                if ((this._Bill_Date != value))
                {
                    this._Bill_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 1 Total]", Storage = "_Record_Type_1_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_1_Total
        {
            get
            {
                return this._Record_Type_1_Total;
            }
            set
            {
                if ((this._Record_Type_1_Total != value))
                {
                    this._Record_Type_1_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 2 Total]", Storage = "_Record_Type_2_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_2_Total
        {
            get
            {
                return this._Record_Type_2_Total;
            }
            set
            {
                if ((this._Record_Type_2_Total != value))
                {
                    this._Record_Type_2_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 4 Total]", Storage = "_Record_Type_4_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_4_Total
        {
            get
            {
                return this._Record_Type_4_Total;
            }
            set
            {
                if ((this._Record_Type_4_Total != value))
                {
                    this._Record_Type_4_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType4_GetByInvoiceResult
    {

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string @__10_Digit_DID;

        private string _SSO;

        private string _Charge_Description;

        private System.Nullable<decimal> _Tax_Charge;

        private string _Vendor_Name;

        private string _BillDate;

        private System.Nullable<decimal> _Tax_Percentage;

        private string _ServiceType;

        private string _State_Province;

        private int _CurrencyConversionID;

        public RecordType4_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 Digit DID]", Storage = "__10_Digit_DID", DbType = "VarChar(50)")]
        public string _10_Digit_DID
        {
            get
            {
                return this.@__10_Digit_DID;
            }
            set
            {
                if ((this.@__10_Digit_DID != value))
                {
                    this.@__10_Digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(50)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Tax Charge]", Storage = "_Tax_Charge", DbType = "Decimal(38,2)")]
        public System.Nullable<decimal> Tax_Charge
        {
            get
            {
                return this._Tax_Charge;
            }
            set
            {
                if ((this._Tax_Charge != value))
                {
                    this._Tax_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillDate", DbType = "VarChar(50)")]
        public string BillDate
        {
            get
            {
                return this._BillDate;
            }
            set
            {
                if ((this._BillDate != value))
                {
                    this._BillDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Tax Percentage]", Storage = "_Tax_Percentage", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Tax_Percentage
        {
            get
            {
                return this._Tax_Percentage;
            }
            set
            {
                if ((this._Tax_Percentage != value))
                {
                    this._Tax_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceType", DbType = "VarChar(50)")]
        public string ServiceType
        {
            get
            {
                return this._ServiceType;
            }
            set
            {
                if ((this._ServiceType != value))
                {
                    this._ServiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType4_GetExceptionsByInvoiceResult
    {

        private int _ID;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private System.Nullable<int> _InvoiceType;

        private System.Nullable<int> _RecordType;

        private string _CustomerNumber;

        private string _PhoneNumber;

        private string _CallType;

        private System.Nullable<int> _TotalCallCount;

        private System.Nullable<decimal> _TotalCallDuration;

        private string _UsageTotal;

        private string _Inter_IntlUsage;

        private System.Nullable<decimal> _State;

        private System.Nullable<decimal> _Local;

        private System.Nullable<decimal> _Federal;

        private System.Nullable<decimal> _USF;

        private System.Nullable<decimal> _ARF;

        private System.Nullable<int> _DefaultImportCurrencyID;

        public RecordType4_GetExceptionsByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50)")]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int")]
        public System.Nullable<int> InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType", DbType = "Int")]
        public System.Nullable<int> RecordType
        {
            get
            {
                return this._RecordType;
            }
            set
            {
                if ((this._RecordType != value))
                {
                    this._RecordType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerNumber", DbType = "VarChar(50)")]
        public string CustomerNumber
        {
            get
            {
                return this._CustomerNumber;
            }
            set
            {
                if ((this._CustomerNumber != value))
                {
                    this._CustomerNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PhoneNumber", DbType = "VarChar(50)")]
        public string PhoneNumber
        {
            get
            {
                return this._PhoneNumber;
            }
            set
            {
                if ((this._PhoneNumber != value))
                {
                    this._PhoneNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CallType", DbType = "VarChar(50)")]
        public string CallType
        {
            get
            {
                return this._CallType;
            }
            set
            {
                if ((this._CallType != value))
                {
                    this._CallType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalCallCount", DbType = "Int")]
        public System.Nullable<int> TotalCallCount
        {
            get
            {
                return this._TotalCallCount;
            }
            set
            {
                if ((this._TotalCallCount != value))
                {
                    this._TotalCallCount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalCallDuration", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> TotalCallDuration
        {
            get
            {
                return this._TotalCallDuration;
            }
            set
            {
                if ((this._TotalCallDuration != value))
                {
                    this._TotalCallDuration = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UsageTotal", DbType = "VarChar(50)")]
        public string UsageTotal
        {
            get
            {
                return this._UsageTotal;
            }
            set
            {
                if ((this._UsageTotal != value))
                {
                    this._UsageTotal = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Inter/IntlUsage]", Storage = "_Inter_IntlUsage", DbType = "VarChar(50)")]
        public string Inter_IntlUsage
        {
            get
            {
                return this._Inter_IntlUsage;
            }
            set
            {
                if ((this._Inter_IntlUsage != value))
                {
                    this._Inter_IntlUsage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_State", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> State
        {
            get
            {
                return this._State;
            }
            set
            {
                if ((this._State != value))
                {
                    this._State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Local", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> Local
        {
            get
            {
                return this._Local;
            }
            set
            {
                if ((this._Local != value))
                {
                    this._Local = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Federal", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> Federal
        {
            get
            {
                return this._Federal;
            }
            set
            {
                if ((this._Federal != value))
                {
                    this._Federal = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_USF", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> USF
        {
            get
            {
                return this._USF;
            }
            set
            {
                if ((this._USF != value))
                {
                    this._USF = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ARF", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> ARF
        {
            get
            {
                return this._ARF;
            }
            set
            {
                if ((this._ARF != value))
                {
                    this._ARF = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DefaultImportCurrencyID", DbType = "Int")]
        public System.Nullable<int> DefaultImportCurrencyID
        {
            get
            {
                return this._DefaultImportCurrencyID;
            }
            set
            {
                if ((this._DefaultImportCurrencyID != value))
                {
                    this._DefaultImportCurrencyID = value;
                }
            }
        }
    }

    public partial class RecordType4_ValidateInvoiceResult
    {

        private string _BAN;

        private System.Nullable<decimal> _RecordType4_Sum;

        private System.Nullable<decimal> _RecordType3_Sum;

        private System.Nullable<decimal> _RecordType5_Sum;

        private System.Nullable<bool> _Validated;

        public RecordType4_ValidateInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(6) NOT NULL", CanBeNull = false)]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType4_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType4_Sum
        {
            get
            {
                return this._RecordType4_Sum;
            }
            set
            {
                if ((this._RecordType4_Sum != value))
                {
                    this._RecordType4_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType3_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType3_Sum
        {
            get
            {
                return this._RecordType3_Sum;
            }
            set
            {
                if ((this._RecordType3_Sum != value))
                {
                    this._RecordType3_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordType5_Sum", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> RecordType5_Sum
        {
            get
            {
                return this._RecordType5_Sum;
            }
            set
            {
                if ((this._RecordType5_Sum != value))
                {
                    this._RecordType5_Sum = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Validated", DbType = "Bit")]
        public System.Nullable<bool> Validated
        {
            get
            {
                return this._Validated;
            }
            set
            {
                if ((this._Validated != value))
                {
                    this._Validated = value;
                }
            }
        }
    }

    public partial class RecordType5_GetByInvoiceResult
    {

        private int _Record_Type;

        private int _Check_Record_Type;

        private string _BAN;

        private int _Total_Record_Count;

        private string _SumField_Name;

        private decimal _TotalAmount;

        private int _CurrencyConversionID;

        public RecordType5_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Check Record Type]", Storage = "_Check_Record_Type", DbType = "Int NOT NULL")]
        public int Check_Record_Type
        {
            get
            {
                return this._Check_Record_Type;
            }
            set
            {
                if ((this._Check_Record_Type != value))
                {
                    this._Check_Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Total Record Count]", Storage = "_Total_Record_Count", DbType = "Int NOT NULL")]
        public int Total_Record_Count
        {
            get
            {
                return this._Total_Record_Count;
            }
            set
            {
                if ((this._Total_Record_Count != value))
                {
                    this._Total_Record_Count = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[SumField Name]", Storage = "_SumField_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string SumField_Name
        {
            get
            {
                return this._SumField_Name;
            }
            set
            {
                if ((this._SumField_Name != value))
                {
                    this._SumField_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalAmount", DbType = "Decimal(18,2) NOT NULL")]
        public decimal TotalAmount
        {
            get
            {
                return this._TotalAmount;
            }
            set
            {
                if ((this._TotalAmount != value))
                {
                    this._TotalAmount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType5_ProcessResult
    {

        private int _Column1;

        public RecordType5_ProcessResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "", Storage = "_Column1", DbType = "Int NOT NULL")]
        public int Column1
        {
            get
            {
                return this._Column1;
            }
            set
            {
                if ((this._Column1 != value))
                {
                    this._Column1 = value;
                }
            }
        }
    }

    public partial class RecordType6_GetResult
    {

        private int _Record_Type;

        private string _FirstName;

        private string _LastName;

        private string _SSO;

        private string _DID;

        private string _Mobile_Cell;

        private string _Email;

        private string _BusinessUnit;

        private string _Department;

        private string _Location_GCOM;

        private string _Legal_Entity;

        private string _UserAddressType;

        private string _UserStreet1;

        private string _UserStreet2;

        private string _UserCity;

        private string _UserState;

        private string _UserZip_Postal_Code;

        private string _UserCountry;

        private string _UserStatus;

        private string _ServiceProfile_ID;

        private string _StartDate;

        private string _StopDate;

        private string _PSP_Order_ID;

        private string _Gateway;

        private string _Serial_Number;

        private string _MAC_Address;

        private string _IP_Address;

        private string _Brand;

        private string _Model;

        private string _Asset_Tag_Number;

        private string _Asset_Address;

        private string _AssetStatus;

        private string _Site_ID;

        public RecordType6_GetResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FirstName", DbType = "NVarChar(255)")]
        public string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                if ((this._FirstName != value))
                {
                    this._FirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastName", DbType = "NVarChar(255)")]
        public string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                if ((this._LastName != value))
                {
                    this._LastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "NVarChar(255)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DID", DbType = "NVarChar(255)")]
        public string DID
        {
            get
            {
                return this._DID;
            }
            set
            {
                if ((this._DID != value))
                {
                    this._DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Mobile/Cell]", Storage = "_Mobile_Cell", DbType = "NVarChar(255)")]
        public string Mobile_Cell
        {
            get
            {
                return this._Mobile_Cell;
            }
            set
            {
                if ((this._Mobile_Cell != value))
                {
                    this._Mobile_Cell = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Email", DbType = "NVarChar(255)")]
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                if ((this._Email != value))
                {
                    this._Email = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BusinessUnit", DbType = "NVarChar(255)")]
        public string BusinessUnit
        {
            get
            {
                return this._BusinessUnit;
            }
            set
            {
                if ((this._BusinessUnit != value))
                {
                    this._BusinessUnit = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Department", DbType = "NVarChar(255)")]
        public string Department
        {
            get
            {
                return this._Department;
            }
            set
            {
                if ((this._Department != value))
                {
                    this._Department = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Location-GCOM]", Storage = "_Location_GCOM", DbType = "NVarChar(255)")]
        public string Location_GCOM
        {
            get
            {
                return this._Location_GCOM;
            }
            set
            {
                if ((this._Location_GCOM != value))
                {
                    this._Location_GCOM = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Legal Entity]", Storage = "_Legal_Entity", DbType = "NVarChar(255)")]
        public string Legal_Entity
        {
            get
            {
                return this._Legal_Entity;
            }
            set
            {
                if ((this._Legal_Entity != value))
                {
                    this._Legal_Entity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserAddressType", DbType = "NVarChar(255)")]
        public string UserAddressType
        {
            get
            {
                return this._UserAddressType;
            }
            set
            {
                if ((this._UserAddressType != value))
                {
                    this._UserAddressType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserStreet1", DbType = "NVarChar(255)")]
        public string UserStreet1
        {
            get
            {
                return this._UserStreet1;
            }
            set
            {
                if ((this._UserStreet1 != value))
                {
                    this._UserStreet1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserStreet2", DbType = "NVarChar(255)")]
        public string UserStreet2
        {
            get
            {
                return this._UserStreet2;
            }
            set
            {
                if ((this._UserStreet2 != value))
                {
                    this._UserStreet2 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserCity", DbType = "NVarChar(255)")]
        public string UserCity
        {
            get
            {
                return this._UserCity;
            }
            set
            {
                if ((this._UserCity != value))
                {
                    this._UserCity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserState", DbType = "NVarChar(255)")]
        public string UserState
        {
            get
            {
                return this._UserState;
            }
            set
            {
                if ((this._UserState != value))
                {
                    this._UserState = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[UserZip/Postal Code]", Storage = "_UserZip_Postal_Code", DbType = "NVarChar(255)")]
        public string UserZip_Postal_Code
        {
            get
            {
                return this._UserZip_Postal_Code;
            }
            set
            {
                if ((this._UserZip_Postal_Code != value))
                {
                    this._UserZip_Postal_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserCountry", DbType = "NVarChar(255)")]
        public string UserCountry
        {
            get
            {
                return this._UserCountry;
            }
            set
            {
                if ((this._UserCountry != value))
                {
                    this._UserCountry = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserStatus", DbType = "NVarChar(255)")]
        public string UserStatus
        {
            get
            {
                return this._UserStatus;
            }
            set
            {
                if ((this._UserStatus != value))
                {
                    this._UserStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceProfile_ID", DbType = "NVarChar(255)")]
        public string ServiceProfile_ID
        {
            get
            {
                return this._ServiceProfile_ID;
            }
            set
            {
                if ((this._ServiceProfile_ID != value))
                {
                    this._ServiceProfile_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartDate", DbType = "NVarChar(255)")]
        public string StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StopDate", DbType = "NVarChar(255)")]
        public string StopDate
        {
            get
            {
                return this._StopDate;
            }
            set
            {
                if ((this._StopDate != value))
                {
                    this._StopDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[PSP Order ID]", Storage = "_PSP_Order_ID", DbType = "NVarChar(255)")]
        public string PSP_Order_ID
        {
            get
            {
                return this._PSP_Order_ID;
            }
            set
            {
                if ((this._PSP_Order_ID != value))
                {
                    this._PSP_Order_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Gateway", DbType = "NVarChar(255)")]
        public string Gateway
        {
            get
            {
                return this._Gateway;
            }
            set
            {
                if ((this._Gateway != value))
                {
                    this._Gateway = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Serial Number]", Storage = "_Serial_Number", DbType = "NVarChar(255)")]
        public string Serial_Number
        {
            get
            {
                return this._Serial_Number;
            }
            set
            {
                if ((this._Serial_Number != value))
                {
                    this._Serial_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[MAC Address]", Storage = "_MAC_Address", DbType = "NVarChar(255)")]
        public string MAC_Address
        {
            get
            {
                return this._MAC_Address;
            }
            set
            {
                if ((this._MAC_Address != value))
                {
                    this._MAC_Address = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[IP Address]", Storage = "_IP_Address", DbType = "NVarChar(255)")]
        public string IP_Address
        {
            get
            {
                return this._IP_Address;
            }
            set
            {
                if ((this._IP_Address != value))
                {
                    this._IP_Address = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Brand", DbType = "NVarChar(255)")]
        public string Brand
        {
            get
            {
                return this._Brand;
            }
            set
            {
                if ((this._Brand != value))
                {
                    this._Brand = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Model", DbType = "NVarChar(255)")]
        public string Model
        {
            get
            {
                return this._Model;
            }
            set
            {
                if ((this._Model != value))
                {
                    this._Model = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Asset Tag Number]", Storage = "_Asset_Tag_Number", DbType = "NVarChar(255)")]
        public string Asset_Tag_Number
        {
            get
            {
                return this._Asset_Tag_Number;
            }
            set
            {
                if ((this._Asset_Tag_Number != value))
                {
                    this._Asset_Tag_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Asset Address]", Storage = "_Asset_Address", DbType = "NVarChar(255)")]
        public string Asset_Address
        {
            get
            {
                return this._Asset_Address;
            }
            set
            {
                if ((this._Asset_Address != value))
                {
                    this._Asset_Address = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssetStatus", DbType = "NVarChar(255)")]
        public string AssetStatus
        {
            get
            {
                return this._AssetStatus;
            }
            set
            {
                if ((this._AssetStatus != value))
                {
                    this._AssetStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Site ID]", Storage = "_Site_ID", DbType = "NVarChar(255)")]
        public string Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this._Site_ID = value;
                }
            }
        }
    }

    public partial class RecordType6_GetServiceDeskResult
    {

        private int _Record_Type;

        private string _FirstName;

        private string _LastName;

        private string _SSO;

        private string _DID;

        private string _Mobile_Cell;

        private string _Email;

        private string _BusinessUnit;

        private string _Department;

        private string _Location_GCOM;

        private string _Legal_Entity;

        private string _UserAddressType;

        private string _UserStreet1;

        private string _UserStreet2;

        private string _UserCity;

        private string _UserState;

        private string _UserZip_Postal_Code;

        private string _UserCountry;

        private string _UserStatus;

        private string _ServiceProfile_ID;

        private string _StartDate;

        private string _StopDate;

        private string _PSP_Order_ID;

        private string _Gateway;

        private string _Serial_Number;

        private string _MAC_Address;

        private string _IP_Address;

        private string _Brand;

        private string _Model;

        private string _Asset_Tag_Number;

        private string _Asset_Address;

        private string _AssetStatus;

        private string _Site_ID;

        public RecordType6_GetServiceDeskResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FirstName", DbType = "NVarChar(50)")]
        public string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                if ((this._FirstName != value))
                {
                    this._FirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastName", DbType = "NVarChar(50)")]
        public string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                if ((this._LastName != value))
                {
                    this._LastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "NVarChar(4000)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DID", DbType = "NVarChar(4000)")]
        public string DID
        {
            get
            {
                return this._DID;
            }
            set
            {
                if ((this._DID != value))
                {
                    this._DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Mobile/Cell]", Storage = "_Mobile_Cell", DbType = "NVarChar(40)")]
        public string Mobile_Cell
        {
            get
            {
                return this._Mobile_Cell;
            }
            set
            {
                if ((this._Mobile_Cell != value))
                {
                    this._Mobile_Cell = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Email", DbType = "NVarChar(40)")]
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                if ((this._Email != value))
                {
                    this._Email = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BusinessUnit", DbType = "NVarChar(255)")]
        public string BusinessUnit
        {
            get
            {
                return this._BusinessUnit;
            }
            set
            {
                if ((this._BusinessUnit != value))
                {
                    this._BusinessUnit = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Department", DbType = "NVarChar(40)")]
        public string Department
        {
            get
            {
                return this._Department;
            }
            set
            {
                if ((this._Department != value))
                {
                    this._Department = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Location-GCOM]", Storage = "_Location_GCOM", DbType = "NVarChar(80)")]
        public string Location_GCOM
        {
            get
            {
                return this._Location_GCOM;
            }
            set
            {
                if ((this._Location_GCOM != value))
                {
                    this._Location_GCOM = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Legal Entity]", Storage = "_Legal_Entity", DbType = "NVarChar(255)")]
        public string Legal_Entity
        {
            get
            {
                return this._Legal_Entity;
            }
            set
            {
                if ((this._Legal_Entity != value))
                {
                    this._Legal_Entity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserAddressType", DbType = "NVarChar(255)")]
        public string UserAddressType
        {
            get
            {
                return this._UserAddressType;
            }
            set
            {
                if ((this._UserAddressType != value))
                {
                    this._UserAddressType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserStreet1", DbType = "NVarChar(50)")]
        public string UserStreet1
        {
            get
            {
                return this._UserStreet1;
            }
            set
            {
                if ((this._UserStreet1 != value))
                {
                    this._UserStreet1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserStreet2", DbType = "NVarChar(50)")]
        public string UserStreet2
        {
            get
            {
                return this._UserStreet2;
            }
            set
            {
                if ((this._UserStreet2 != value))
                {
                    this._UserStreet2 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserCity", DbType = "NVarChar(50)")]
        public string UserCity
        {
            get
            {
                return this._UserCity;
            }
            set
            {
                if ((this._UserCity != value))
                {
                    this._UserCity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserState", DbType = "NVarChar(50)")]
        public string UserState
        {
            get
            {
                return this._UserState;
            }
            set
            {
                if ((this._UserState != value))
                {
                    this._UserState = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[UserZip/Postal Code]", Storage = "_UserZip_Postal_Code", DbType = "NVarChar(50)")]
        public string UserZip_Postal_Code
        {
            get
            {
                return this._UserZip_Postal_Code;
            }
            set
            {
                if ((this._UserZip_Postal_Code != value))
                {
                    this._UserZip_Postal_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserCountry", DbType = "NVarChar(50)")]
        public string UserCountry
        {
            get
            {
                return this._UserCountry;
            }
            set
            {
                if ((this._UserCountry != value))
                {
                    this._UserCountry = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserStatus", DbType = "VarChar(6) NOT NULL", CanBeNull = false)]
        public string UserStatus
        {
            get
            {
                return this._UserStatus;
            }
            set
            {
                if ((this._UserStatus != value))
                {
                    this._UserStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceProfile_ID", DbType = "NVarChar(255)")]
        public string ServiceProfile_ID
        {
            get
            {
                return this._ServiceProfile_ID;
            }
            set
            {
                if ((this._ServiceProfile_ID != value))
                {
                    this._ServiceProfile_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartDate", DbType = "VarChar(8)")]
        public string StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StopDate", DbType = "VarChar(8)")]
        public string StopDate
        {
            get
            {
                return this._StopDate;
            }
            set
            {
                if ((this._StopDate != value))
                {
                    this._StopDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[PSP Order ID]", Storage = "_PSP_Order_ID", DbType = "NVarChar(50)")]
        public string PSP_Order_ID
        {
            get
            {
                return this._PSP_Order_ID;
            }
            set
            {
                if ((this._PSP_Order_ID != value))
                {
                    this._PSP_Order_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Gateway", DbType = "VarChar(3) NOT NULL", CanBeNull = false)]
        public string Gateway
        {
            get
            {
                return this._Gateway;
            }
            set
            {
                if ((this._Gateway != value))
                {
                    this._Gateway = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Serial Number]", Storage = "_Serial_Number", DbType = "VarChar(7) NOT NULL", CanBeNull = false)]
        public string Serial_Number
        {
            get
            {
                return this._Serial_Number;
            }
            set
            {
                if ((this._Serial_Number != value))
                {
                    this._Serial_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[MAC Address]", Storage = "_MAC_Address", DbType = "VarChar(7) NOT NULL", CanBeNull = false)]
        public string MAC_Address
        {
            get
            {
                return this._MAC_Address;
            }
            set
            {
                if ((this._MAC_Address != value))
                {
                    this._MAC_Address = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[IP Address]", Storage = "_IP_Address", DbType = "VarChar(7) NOT NULL", CanBeNull = false)]
        public string IP_Address
        {
            get
            {
                return this._IP_Address;
            }
            set
            {
                if ((this._IP_Address != value))
                {
                    this._IP_Address = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Brand", DbType = "VarChar(7) NOT NULL", CanBeNull = false)]
        public string Brand
        {
            get
            {
                return this._Brand;
            }
            set
            {
                if ((this._Brand != value))
                {
                    this._Brand = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Model", DbType = "VarChar(7) NOT NULL", CanBeNull = false)]
        public string Model
        {
            get
            {
                return this._Model;
            }
            set
            {
                if ((this._Model != value))
                {
                    this._Model = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Asset Tag Number]", Storage = "_Asset_Tag_Number", DbType = "VarChar(7) NOT NULL", CanBeNull = false)]
        public string Asset_Tag_Number
        {
            get
            {
                return this._Asset_Tag_Number;
            }
            set
            {
                if ((this._Asset_Tag_Number != value))
                {
                    this._Asset_Tag_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Asset Address]", Storage = "_Asset_Address", DbType = "NVarChar(255)")]
        public string Asset_Address
        {
            get
            {
                return this._Asset_Address;
            }
            set
            {
                if ((this._Asset_Address != value))
                {
                    this._Asset_Address = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssetStatus", DbType = "NVarChar(255)")]
        public string AssetStatus
        {
            get
            {
                return this._AssetStatus;
            }
            set
            {
                if ((this._AssetStatus != value))
                {
                    this._AssetStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Site ID]", Storage = "_Site_ID", DbType = "NVarChar(40)")]
        public string Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this._Site_ID = value;
                }
            }
        }
    }

    public partial class usp_GetServiceDeskDataResult
    {

        private string _p_Asset_Search_Code;

        private string _p_Maint_Start;

        private string _p_Maint_End;

        public usp_GetServiceDeskDataResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[p_Asset Search Code]", Storage = "_p_Asset_Search_Code", DbType = "VarChar(5) NOT NULL", CanBeNull = false)]
        public string p_Asset_Search_Code
        {
            get
            {
                return this._p_Asset_Search_Code;
            }
            set
            {
                if ((this._p_Asset_Search_Code != value))
                {
                    this._p_Asset_Search_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[p_Maint Start]", Storage = "_p_Maint_Start", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
        public string p_Maint_Start
        {
            get
            {
                return this._p_Maint_Start;
            }
            set
            {
                if ((this._p_Maint_Start != value))
                {
                    this._p_Maint_Start = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[p_Maint End]", Storage = "_p_Maint_End", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
        public string p_Maint_End
        {
            get
            {
                return this._p_Maint_End;
            }
            set
            {
                if ((this._p_Maint_End != value))
                {
                    this._p_Maint_End = value;
                }
            }
        }
    }

    public partial class RecordType1_GetByCustomerNumberResult
    {

        private System.Nullable<int> _Record_Type;

        private string _BAN;

        private string _Invoice_Number;

        private string _Bill_Date;

        private string _SSO;

        private string _From_Number__DID_;

        private string _From_To_Number;

        private System.Nullable<decimal> _Charge_Amount;

        private string _Date_of_Record;

        private System.Nullable<System.DateTime> _Connect_Time;

        private string _Billable_Time;

        private string _Billing_Number_North_American_Standard;

        private string _From_Place;

        private string _From_State;

        private string _To_Place;

        private string _To_State;

        private string _Settlement_Code;

        private string _Charge_Description;

        private string _Provider;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        public RecordType1_GetByCustomerNumberResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int")]
        public System.Nullable<int> Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(30)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Number]", Storage = "_Invoice_Number", DbType = "VarChar(62)")]
        public string Invoice_Number
        {
            get
            {
                return this._Invoice_Number;
            }
            set
            {
                if ((this._Invoice_Number != value))
                {
                    this._Invoice_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Bill Date]", Storage = "_Bill_Date", DbType = "VarChar(8)")]
        public string Bill_Date
        {
            get
            {
                return this._Bill_Date;
            }
            set
            {
                if ((this._Bill_Date != value))
                {
                    this._Bill_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(9) NOT NULL", CanBeNull = false)]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From Number (DID)]", Storage = "_From_Number__DID_", DbType = "VarChar(8000)")]
        public string From_Number__DID_
        {
            get
            {
                return this._From_Number__DID_;
            }
            set
            {
                if ((this._From_Number__DID_ != value))
                {
                    this._From_Number__DID_ = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From/To Number]", Storage = "_From_To_Number", DbType = "VarChar(64)")]
        public string From_To_Number
        {
            get
            {
                return this._From_To_Number;
            }
            set
            {
                if ((this._From_To_Number != value))
                {
                    this._From_To_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Amount]", Storage = "_Charge_Amount", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Charge_Amount
        {
            get
            {
                return this._Charge_Amount;
            }
            set
            {
                if ((this._Charge_Amount != value))
                {
                    this._Charge_Amount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Date of Record]", Storage = "_Date_of_Record", DbType = "VarChar(8)")]
        public string Date_of_Record
        {
            get
            {
                return this._Date_of_Record;
            }
            set
            {
                if ((this._Date_of_Record != value))
                {
                    this._Date_of_Record = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Connect Time]", Storage = "_Connect_Time", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Connect_Time
        {
            get
            {
                return this._Connect_Time;
            }
            set
            {
                if ((this._Connect_Time != value))
                {
                    this._Connect_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Billable Time]", Storage = "_Billable_Time", DbType = "VarChar(30)")]
        public string Billable_Time
        {
            get
            {
                return this._Billable_Time;
            }
            set
            {
                if ((this._Billable_Time != value))
                {
                    this._Billable_Time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Billing Number North American Standard]", Storage = "_Billing_Number_North_American_Standard", DbType = "VarChar(90)")]
        public string Billing_Number_North_American_Standard
        {
            get
            {
                return this._Billing_Number_North_American_Standard;
            }
            set
            {
                if ((this._Billing_Number_North_American_Standard != value))
                {
                    this._Billing_Number_North_American_Standard = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From Place]", Storage = "_From_Place", DbType = "VarChar(15) NOT NULL", CanBeNull = false)]
        public string From_Place
        {
            get
            {
                return this._From_Place;
            }
            set
            {
                if ((this._From_Place != value))
                {
                    this._From_Place = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[From State]", Storage = "_From_State", DbType = "VarChar(2) NOT NULL", CanBeNull = false)]
        public string From_State
        {
            get
            {
                return this._From_State;
            }
            set
            {
                if ((this._From_State != value))
                {
                    this._From_State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[To Place]", Storage = "_To_Place", DbType = "VarChar(15) NOT NULL", CanBeNull = false)]
        public string To_Place
        {
            get
            {
                return this._To_Place;
            }
            set
            {
                if ((this._To_Place != value))
                {
                    this._To_Place = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[To State]", Storage = "_To_State", DbType = "VarChar(2) NOT NULL", CanBeNull = false)]
        public string To_State
        {
            get
            {
                return this._To_State;
            }
            set
            {
                if ((this._To_State != value))
                {
                    this._To_State = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Settlement Code]", Storage = "_Settlement_Code", DbType = "VarChar(1)")]
        public string Settlement_Code
        {
            get
            {
                return this._Settlement_Code;
            }
            set
            {
                if ((this._Settlement_Code != value))
                {
                    this._Settlement_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(50)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Provider", DbType = "VarChar(4) NOT NULL", CanBeNull = false)]
        public string Provider
        {
            get
            {
                return this._Provider;
            }
            set
            {
                if ((this._Provider != value))
                {
                    this._Provider = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(1) NOT NULL", CanBeNull = false)]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(1) NOT NULL", CanBeNull = false)]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType2_GetByCustomerNumberResult
    {

        private System.Nullable<int> _Record_Type;

        private string _BAN;

        private string _Invoice_Number;

        private string @__10_Digit_DID;

        private string _SSO;

        private string _Charge_Date;

        private string _Charge_Description;

        private string _Charge_Type;

        private string _Service_Start_Date;

        private string _Service_End_Date;

        private string _Invoice_Bill_Period_Start;

        private string _Invoice_Bill_Period_End;

        private System.Nullable<decimal> _Total;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        public RecordType2_GetByCustomerNumberResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int")]
        public System.Nullable<int> Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Number]", Storage = "_Invoice_Number", DbType = "VarChar(50)")]
        public string Invoice_Number
        {
            get
            {
                return this._Invoice_Number;
            }
            set
            {
                if ((this._Invoice_Number != value))
                {
                    this._Invoice_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 Digit DID]", Storage = "__10_Digit_DID", DbType = "VarChar(50)")]
        public string _10_Digit_DID
        {
            get
            {
                return this.@__10_Digit_DID;
            }
            set
            {
                if ((this.@__10_Digit_DID != value))
                {
                    this.@__10_Digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Date]", Storage = "_Charge_Date", DbType = "VarChar(50)")]
        public string Charge_Date
        {
            get
            {
                return this._Charge_Date;
            }
            set
            {
                if ((this._Charge_Date != value))
                {
                    this._Charge_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(25)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Type]", Storage = "_Charge_Type", DbType = "VarChar(255)")]
        public string Charge_Type
        {
            get
            {
                return this._Charge_Type;
            }
            set
            {
                if ((this._Charge_Type != value))
                {
                    this._Charge_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service Start Date]", Storage = "_Service_Start_Date", DbType = "VarChar(8000)")]
        public string Service_Start_Date
        {
            get
            {
                return this._Service_Start_Date;
            }
            set
            {
                if ((this._Service_Start_Date != value))
                {
                    this._Service_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service End Date]", Storage = "_Service_End_Date", DbType = "VarChar(8000)")]
        public string Service_End_Date
        {
            get
            {
                return this._Service_End_Date;
            }
            set
            {
                if ((this._Service_End_Date != value))
                {
                    this._Service_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period Start]", Storage = "_Invoice_Bill_Period_Start", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_Start
        {
            get
            {
                return this._Invoice_Bill_Period_Start;
            }
            set
            {
                if ((this._Invoice_Bill_Period_Start != value))
                {
                    this._Invoice_Bill_Period_Start = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period End]", Storage = "_Invoice_Bill_Period_End", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_End
        {
            get
            {
                return this._Invoice_Bill_Period_End;
            }
            set
            {
                if ((this._Invoice_Bill_Period_End != value))
                {
                    this._Invoice_Bill_Period_End = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType3_GetByCustomerNumberResult
    {

        private System.Nullable<int> _Record_Type;

        private string _BAN;

        private string _Invoice_Number;

        private string _Bill_Date;

        private System.Nullable<decimal> _Record_Type_1_Total;

        private System.Nullable<decimal> _Record_Type_2_Total;

        private System.Nullable<decimal> _Record_Type_4_Total;

        private System.Nullable<decimal> _Total;

        private int _CurrencyConversionID;

        public RecordType3_GetByCustomerNumberResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int")]
        public System.Nullable<int> Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Number]", Storage = "_Invoice_Number", DbType = "VarChar(50)")]
        public string Invoice_Number
        {
            get
            {
                return this._Invoice_Number;
            }
            set
            {
                if ((this._Invoice_Number != value))
                {
                    this._Invoice_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Bill Date]", Storage = "_Bill_Date", DbType = "VarChar(50)")]
        public string Bill_Date
        {
            get
            {
                return this._Bill_Date;
            }
            set
            {
                if ((this._Bill_Date != value))
                {
                    this._Bill_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 1 Total]", Storage = "_Record_Type_1_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_1_Total
        {
            get
            {
                return this._Record_Type_1_Total;
            }
            set
            {
                if ((this._Record_Type_1_Total != value))
                {
                    this._Record_Type_1_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 2 Total]", Storage = "_Record_Type_2_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_2_Total
        {
            get
            {
                return this._Record_Type_2_Total;
            }
            set
            {
                if ((this._Record_Type_2_Total != value))
                {
                    this._Record_Type_2_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 4 Total]", Storage = "_Record_Type_4_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_4_Total
        {
            get
            {
                return this._Record_Type_4_Total;
            }
            set
            {
                if ((this._Record_Type_4_Total != value))
                {
                    this._Record_Type_4_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType4_GetByCustomerNumberResult
    {

        private System.Nullable<int> _Record_Type;

        private string _BAN;

        private string _Invoice_Number;

        private string @__10_digit_DID;

        private string _SSO;

        private string _Charge_Description;

        private System.Nullable<decimal> _Tax_Charge;

        private System.Nullable<decimal> _Tax__;

        private string _Service_Type;

        private string _Vendor_Name;

        private string _Bill_Date;

        private string _State_Province;

        private int _CurrencyConversionID;

        public RecordType4_GetByCustomerNumberResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int")]
        public System.Nullable<int> Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(30)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Number]", Storage = "_Invoice_Number", DbType = "VarChar(50)")]
        public string Invoice_Number
        {
            get
            {
                return this._Invoice_Number;
            }
            set
            {
                if ((this._Invoice_Number != value))
                {
                    this._Invoice_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 digit DID]", Storage = "__10_digit_DID", DbType = "VarChar(50)")]
        public string _10_digit_DID
        {
            get
            {
                return this.@__10_digit_DID;
            }
            set
            {
                if ((this.@__10_digit_DID != value))
                {
                    this.@__10_digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(25) NOT NULL", CanBeNull = false)]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Tax Charge]", Storage = "_Tax_Charge", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Tax_Charge
        {
            get
            {
                return this._Tax_Charge;
            }
            set
            {
                if ((this._Tax_Charge != value))
                {
                    this._Tax_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Tax %]", Storage = "_Tax__", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Tax__
        {
            get
            {
                return this._Tax__;
            }
            set
            {
                if ((this._Tax__ != value))
                {
                    this._Tax__ = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service Type]", Storage = "_Service_Type", DbType = "VarChar(50)")]
        public string Service_Type
        {
            get
            {
                return this._Service_Type;
            }
            set
            {
                if ((this._Service_Type != value))
                {
                    this._Service_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Bill Date]", Storage = "_Bill_Date", DbType = "VarChar(50)")]
        public string Bill_Date
        {
            get
            {
                return this._Bill_Date;
            }
            set
            {
                if ((this._Bill_Date != value))
                {
                    this._Bill_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(1) NOT NULL", CanBeNull = false)]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class RecordType5_GetByCustomerNumberResult
    {

        private string _Invoice_Number;

        private System.Nullable<int> _Record_Type;

        private System.Nullable<int> _Check_Record_Type;

        private System.Nullable<int> _Total_Record_Count;

        private string _SumField_Name;

        private System.Nullable<decimal> _Total_Amount;

        private int _CurrencyConversionID;

        public RecordType5_GetByCustomerNumberResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Number]", Storage = "_Invoice_Number", DbType = "VarChar(62)")]
        public string Invoice_Number
        {
            get
            {
                return this._Invoice_Number;
            }
            set
            {
                if ((this._Invoice_Number != value))
                {
                    this._Invoice_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int")]
        public System.Nullable<int> Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Check Record Type]", Storage = "_Check_Record_Type", DbType = "Int")]
        public System.Nullable<int> Check_Record_Type
        {
            get
            {
                return this._Check_Record_Type;
            }
            set
            {
                if ((this._Check_Record_Type != value))
                {
                    this._Check_Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Total Record Count]", Storage = "_Total_Record_Count", DbType = "Int")]
        public System.Nullable<int> Total_Record_Count
        {
            get
            {
                return this._Total_Record_Count;
            }
            set
            {
                if ((this._Total_Record_Count != value))
                {
                    this._Total_Record_Count = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[SumField Name]", Storage = "_SumField_Name", DbType = "VarChar(30)")]
        public string SumField_Name
        {
            get
            {
                return this._SumField_Name;
            }
            set
            {
                if ((this._SumField_Name != value))
                {
                    this._SumField_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Total Amount]", Storage = "_Total_Amount", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total_Amount
        {
            get
            {
                return this._Total_Amount;
            }
            set
            {
                if ((this._Total_Amount != value))
                {
                    this._Total_Amount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }
    }

    public partial class usp_GetCustomerNumbersResult
    {

        private string _CustomerNbr;

        public usp_GetCustomerNumbersResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerNbr", DbType = "VarChar(50)")]
        public string CustomerNbr
        {
            get
            {
                return this._CustomerNbr;
            }
            set
            {
                if ((this._CustomerNbr != value))
                {
                    this._CustomerNbr = value;
                }
            }
        }
    }

    public partial class GELD_GetEmailDistributionListResult
    {

        private string _EmailDistributionList;

        public GELD_GetEmailDistributionListResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EmailDistributionList", DbType = "NVarChar(MAX)")]
        public string EmailDistributionList
        {
            get
            {
                return this._EmailDistributionList;
            }
            set
            {
                if ((this._EmailDistributionList != value))
                {
                    this._EmailDistributionList = value;
                }
            }
        }
    }

    public partial class usp_GetAccountDetailsbyInvoiceResult
    {

        private System.Nullable<int> _SnapshotID;

        private System.Nullable<int> _AccountNumber;

        private string _TelephoneNumber;

        private System.Nullable<char> _AccountStatusID;

        private string _AccountStatus;

        private System.Nullable<System.DateTime> _InstallationDate;

        private System.Nullable<System.DateTime> _DeactivateDate;

        public usp_GetAccountDetailsbyInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SnapshotID", DbType = "Int")]
        public System.Nullable<int> SnapshotID
        {
            get
            {
                return this._SnapshotID;
            }
            set
            {
                if ((this._SnapshotID != value))
                {
                    this._SnapshotID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AccountNumber", DbType = "Int")]
        public System.Nullable<int> AccountNumber
        {
            get
            {
                return this._AccountNumber;
            }
            set
            {
                if ((this._AccountNumber != value))
                {
                    this._AccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TelephoneNumber", DbType = "VarChar(50)")]
        public string TelephoneNumber
        {
            get
            {
                return this._TelephoneNumber;
            }
            set
            {
                if ((this._TelephoneNumber != value))
                {
                    this._TelephoneNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AccountStatusID", DbType = "Char(1)")]
        public System.Nullable<char> AccountStatusID
        {
            get
            {
                return this._AccountStatusID;
            }
            set
            {
                if ((this._AccountStatusID != value))
                {
                    this._AccountStatusID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AccountStatus", DbType = "VarChar(28)")]
        public string AccountStatus
        {
            get
            {
                return this._AccountStatus;
            }
            set
            {
                if ((this._AccountStatus != value))
                {
                    this._AccountStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InstallationDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> InstallationDate
        {
            get
            {
                return this._InstallationDate;
            }
            set
            {
                if ((this._InstallationDate != value))
                {
                    this._InstallationDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DeactivateDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> DeactivateDate
        {
            get
            {
                return this._DeactivateDate;
            }
            set
            {
                if ((this._DeactivateDate != value))
                {
                    this._DeactivateDate = value;
                }
            }
        }
    }

    public partial class get_InvoiceTypeResult
    {

        private int _iInvoiceTypeId;

        private string _sInvoiceTypeName;

        private string _sPrefix;

        private string _sBAN;

        private string _sVendorName;

        private string _sImportCurrencyDefault;

        private string _sExportCurrencyDefault;

        private string _sDefaultFTP;

        private string _sFTPUsername;

        private string _sFTPPassword;

        private System.Nullable<bool> _bIsAutoPreBill;

        private System.Nullable<int> _iDaysBeforeBillCycle;

        private System.Nullable<bool> _bIsAutoPostBill;

        private System.Nullable<int> _iDaysAfterBillCycle;

        private System.Nullable<int> _iOutputFileFormat;//8967

        private System.Nullable<int> _isSOO;//11609

        private string _sEmailAddress;

        private bool _bEDI;

        private string _sBillingSystem;//SERO-1582
        private string _iAutomationFrequency;//SERO-1582

        //Sero-3511 start
        private string _sContractNumber;
        private DateTime _dContractStartDate;
        private DateTime _dContractEndDate;
        private string _sIndirectPartnerOrRepCode;
        private string _sGLDepartmentCode;
        private string _sIndirectAgentRegion;

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sContractNumber", DbType = "nVarchar(50)")]
        public string sContractNumber
        {
            get
            {
                return this._sContractNumber;
            }
            set
            {
                if ((this._sContractNumber != value))
                {
                    this._sContractNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dContractStartDate", DbType = "DateTime")]
        public DateTime dContractStartDate
        {
            get
            {
                return this._dContractStartDate;
            }
            set
            {
                if ((this._dContractStartDate != value))
                {
                    this._dContractStartDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dContractEndDate", DbType = "DateTime")]
        public DateTime dContractEndDate
        {
            get
            {
                return this._dContractEndDate;
            }
            set
            {
                if ((this._dContractEndDate != value))
                {
                    this._dContractEndDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sIndirectPartnerOrRepCode", DbType = "nVarchar(50)")]
        public string sIndirectPartnerOrRepCode
        {
            get
            {
                return this._sIndirectPartnerOrRepCode;
            }
            set
            {
                if ((this._sIndirectPartnerOrRepCode != value))
                {
                    this._sIndirectPartnerOrRepCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sGLDepartmentCode", DbType = "nVarchar(50)")]
        public string sGLDepartmentCode
        {
            get
            {
                return this._sGLDepartmentCode;
            }
            set
            {
                if ((this._sGLDepartmentCode != value))
                {
                    this._sGLDepartmentCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sIndirectAgentRegion", DbType = "nVarchar(50)")]
        public string sIndirectAgentRegion
        {
            get
            {
                return this._sIndirectAgentRegion;
            }
            set
            {
                if ((this._sIndirectAgentRegion != value))
                {
                    this._sIndirectAgentRegion = value;
                }
            }
        }

        //Sero-3511 END

        public get_InvoiceTypeResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceTypeName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sInvoiceTypeName
        {
            get
            {
                return this._sInvoiceTypeName;
            }
            set
            {
                if ((this._sInvoiceTypeName != value))
                {
                    this._sInvoiceTypeName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPrefix", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sPrefix
        {
            get
            {
                return this._sPrefix;
            }
            set
            {
                if ((this._sPrefix != value))
                {
                    this._sPrefix = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBAN", DbType = "VarChar(50)")]
        public string sBAN
        {
            get
            {
                return this._sBAN;
            }
            set
            {
                if ((this._sBAN != value))
                {
                    this._sBAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sVendorName", DbType = "VarChar(50)")]
        public string sVendorName
        {
            get
            {
                return this._sVendorName;
            }
            set
            {
                if ((this._sVendorName != value))
                {
                    this._sVendorName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sImportCurrencyDefault", DbType = "VarChar(3)")]
        public string sImportCurrencyDefault
        {
            get
            {
                return this._sImportCurrencyDefault;
            }
            set
            {
                if ((this._sImportCurrencyDefault != value))
                {
                    this._sImportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportCurrencyDefault", DbType = "VarChar(3)")]
        public string sExportCurrencyDefault
        {
            get
            {
                return this._sExportCurrencyDefault;
            }
            set
            {
                if ((this._sExportCurrencyDefault != value))
                {
                    this._sExportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDefaultFTP", DbType = "VarChar(100)")]
        public string sDefaultFTP
        {
            get
            {
                return this._sDefaultFTP;
            }
            set
            {
                if ((this._sDefaultFTP != value))
                {
                    this._sDefaultFTP = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPUsername", DbType = "VarChar(100)")]
        public string sFTPUsername
        {
            get
            {
                return this._sFTPUsername;
            }
            set
            {
                if ((this._sFTPUsername != value))
                {
                    this._sFTPUsername = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPPassword", DbType = "VarChar(256)")]
        public string sFTPPassword
        {
            get
            {
                return this._sFTPPassword;
            }
            set
            {
                if ((this._sFTPPassword != value))
                {
                    this._sFTPPassword = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPreBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPreBill
        {
            get
            {
                return this._bIsAutoPreBill;
            }
            set
            {
                if ((this._bIsAutoPreBill != value))
                {
                    this._bIsAutoPreBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysBeforeBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysBeforeBillCycle
        {
            get
            {
                return this._iDaysBeforeBillCycle;
            }
            set
            {
                if ((this._iDaysBeforeBillCycle != value))
                {
                    this._iDaysBeforeBillCycle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPostBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPostBill
        {
            get
            {
                return this._bIsAutoPostBill;
            }
            set
            {
                if ((this._bIsAutoPostBill != value))
                {
                    this._bIsAutoPostBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysAfterBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysAfterBillCycle
        {
            get
            {
                return this._iDaysAfterBillCycle;
            }
            set
            {
                if ((this._iDaysAfterBillCycle != value))
                {
                    this._iDaysAfterBillCycle = value;
                }
            }
        }
        //CBE_8967
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iOutputFileFormat", DbType = "Int")]
        public System.Nullable<int> iOutputFileFormat
        {
            get
            {
                return this._iOutputFileFormat;
            }
            set
            {
                if ((this._iOutputFileFormat != value))
                {
                    this._iOutputFileFormat = value;
                }
            }
        }

        //cbe_11609
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_isSOO", DbType = "Int")]
        public System.Nullable<int> isSOO
        {
            get
            {
                return this._isSOO;
            }
            set
            {
                if ((this._isSOO != value))
                {
                    this._isSOO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sEmailAddress", DbType = "VarChar(100)")]
        public string sEmailAddress
        {
            get
            {
                return this._sEmailAddress;
            }
            set
            {
                if ((this._sEmailAddress != value))
                {
                    this._sEmailAddress = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bEDI", DbType = "Bit NOT NULL")]
        public bool bEDI
        {
            get
            {
                return this._bEDI;
            }
            set
            {
                if ((this._bEDI != value))
                {
                    this._bEDI = value;
                }
            }
        }

        //SERO-1582
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBillingSystem", DbType = "VarChar(50)")]
        public string sBillingSystem
        {
            get
            {
                return this._sBillingSystem;
            }
            set
            {
                if ((this._sBillingSystem != value))
                {
                    this._sBillingSystem = value;
                }
            }
        }

        //SERO-1582
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iAutomationFrequency", DbType = "VarChar(50)")]
        public string iAutomationFrequency
        {
            get
            {
                return this._iAutomationFrequency;
            }
            set
            {
                if ((this._iAutomationFrequency != value))
                {
                    this._iAutomationFrequency = value;
                }
            }
        }
    }

    //cbe9179
    public partial class get_InvoiceType_SOOResult
    {
        private int _iInvoiceTypeId;

        private string _sInvoiceTypeName;

        private string _sPrefix;

        private string _sBAN;

        private string _sVendorName;

        private string _sImportCurrencyDefault;

        private string _sExportCurrencyDefault;

        private string _sDefaultFTP;

        private string _sFTPUsername;

        private string _sFTPPassword;

        private System.Nullable<bool> _bIsAutoPreBill;

        private System.Nullable<int> _iDaysBeforeBillCycle;

        private System.Nullable<bool> _bIsAutoPostBill;

        private System.Nullable<int> _iDaysAfterBillCycle;

        public get_InvoiceType_SOOResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceTypeName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sInvoiceTypeName
        {
            get
            {
                return this._sInvoiceTypeName;
            }
            set
            {
                if ((this._sInvoiceTypeName != value))
                {
                    this._sInvoiceTypeName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPrefix", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sPrefix
        {
            get
            {
                return this._sPrefix;
            }
            set
            {
                if ((this._sPrefix != value))
                {
                    this._sPrefix = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBAN", DbType = "VarChar(50)")]
        public string sBAN
        {
            get
            {
                return this._sBAN;
            }
            set
            {
                if ((this._sBAN != value))
                {
                    this._sBAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sVendorName", DbType = "VarChar(50)")]
        public string sVendorName
        {
            get
            {
                return this._sVendorName;
            }
            set
            {
                if ((this._sVendorName != value))
                {
                    this._sVendorName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sImportCurrencyDefault", DbType = "VarChar(3)")]
        public string sImportCurrencyDefault
        {
            get
            {
                return this._sImportCurrencyDefault;
            }
            set
            {
                if ((this._sImportCurrencyDefault != value))
                {
                    this._sImportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportCurrencyDefault", DbType = "VarChar(3)")]
        public string sExportCurrencyDefault
        {
            get
            {
                return this._sExportCurrencyDefault;
            }
            set
            {
                if ((this._sExportCurrencyDefault != value))
                {
                    this._sExportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDefaultFTP", DbType = "VarChar(100)")]
        public string sDefaultFTP
        {
            get
            {
                return this._sDefaultFTP;
            }
            set
            {
                if ((this._sDefaultFTP != value))
                {
                    this._sDefaultFTP = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPUsername", DbType = "VarChar(100)")]
        public string sFTPUsername
        {
            get
            {
                return this._sFTPUsername;
            }
            set
            {
                if ((this._sFTPUsername != value))
                {
                    this._sFTPUsername = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPPassword", DbType = "VarChar(256)")]
        public string sFTPPassword
        {
            get
            {
                return this._sFTPPassword;
            }
            set
            {
                if ((this._sFTPPassword != value))
                {
                    this._sFTPPassword = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPreBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPreBill
        {
            get
            {
                return this._bIsAutoPreBill;
            }
            set
            {
                if ((this._bIsAutoPreBill != value))
                {
                    this._bIsAutoPreBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysBeforeBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysBeforeBillCycle
        {
            get
            {
                return this._iDaysBeforeBillCycle;
            }
            set
            {
                if ((this._iDaysBeforeBillCycle != value))
                {
                    this._iDaysBeforeBillCycle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPostBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPostBill
        {
            get
            {
                return this._bIsAutoPostBill;
            }
            set
            {
                if ((this._bIsAutoPostBill != value))
                {
                    this._bIsAutoPostBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysAfterBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysAfterBillCycle
        {
            get
            {
                return this._iDaysAfterBillCycle;
            }
            set
            {
                if ((this._iDaysAfterBillCycle != value))
                {
                    this._iDaysAfterBillCycle = value;
                }
            }
        }
    }

    //cbe11609
    public partial class get_InvoiceType_SOOReportResult
    {
        private int _iInvoiceTypeId;

        private string _sInvoiceTypeName;

        private string _sPrefix;

        private string _sBAN;

        private string _sVendorName;

        private string _sImportCurrencyDefault;

        private string _sExportCurrencyDefault;

        private string _sDefaultFTP;

        private string _sFTPUsername;

        private string _sFTPPassword;

        private System.Nullable<bool> _bIsAutoPreBill;

        private System.Nullable<int> _iDaysBeforeBillCycle;

        private System.Nullable<bool> _bIsAutoPostBill;

        private System.Nullable<int> _iDaysAfterBillCycle;

        public get_InvoiceType_SOOReportResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceTypeName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sInvoiceTypeName
        {
            get
            {
                return this._sInvoiceTypeName;
            }
            set
            {
                if ((this._sInvoiceTypeName != value))
                {
                    this._sInvoiceTypeName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPrefix", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sPrefix
        {
            get
            {
                return this._sPrefix;
            }
            set
            {
                if ((this._sPrefix != value))
                {
                    this._sPrefix = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBAN", DbType = "VarChar(50)")]
        public string sBAN
        {
            get
            {
                return this._sBAN;
            }
            set
            {
                if ((this._sBAN != value))
                {
                    this._sBAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sVendorName", DbType = "VarChar(50)")]
        public string sVendorName
        {
            get
            {
                return this._sVendorName;
            }
            set
            {
                if ((this._sVendorName != value))
                {
                    this._sVendorName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sImportCurrencyDefault", DbType = "VarChar(3)")]
        public string sImportCurrencyDefault
        {
            get
            {
                return this._sImportCurrencyDefault;
            }
            set
            {
                if ((this._sImportCurrencyDefault != value))
                {
                    this._sImportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportCurrencyDefault", DbType = "VarChar(3)")]
        public string sExportCurrencyDefault
        {
            get
            {
                return this._sExportCurrencyDefault;
            }
            set
            {
                if ((this._sExportCurrencyDefault != value))
                {
                    this._sExportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDefaultFTP", DbType = "VarChar(100)")]
        public string sDefaultFTP
        {
            get
            {
                return this._sDefaultFTP;
            }
            set
            {
                if ((this._sDefaultFTP != value))
                {
                    this._sDefaultFTP = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPUsername", DbType = "VarChar(100)")]
        public string sFTPUsername
        {
            get
            {
                return this._sFTPUsername;
            }
            set
            {
                if ((this._sFTPUsername != value))
                {
                    this._sFTPUsername = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPPassword", DbType = "VarChar(256)")]
        public string sFTPPassword
        {
            get
            {
                return this._sFTPPassword;
            }
            set
            {
                if ((this._sFTPPassword != value))
                {
                    this._sFTPPassword = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPreBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPreBill
        {
            get
            {
                return this._bIsAutoPreBill;
            }
            set
            {
                if ((this._bIsAutoPreBill != value))
                {
                    this._bIsAutoPreBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysBeforeBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysBeforeBillCycle
        {
            get
            {
                return this._iDaysBeforeBillCycle;
            }
            set
            {
                if ((this._iDaysBeforeBillCycle != value))
                {
                    this._iDaysBeforeBillCycle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPostBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPostBill
        {
            get
            {
                return this._bIsAutoPostBill;
            }
            set
            {
                if ((this._bIsAutoPostBill != value))
                {
                    this._bIsAutoPostBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysAfterBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysAfterBillCycle
        {
            get
            {
                return this._iDaysAfterBillCycle;
            }
            set
            {
                if ((this._iDaysAfterBillCycle != value))
                {
                    this._iDaysAfterBillCycle = value;
                }
            }
        }
    }

    public partial class Get_InvoiceFileUploads_ByInvoiceIdResult
    {

        private int _iUploadedFileId;

        private int _iInvoiceId;

        private int _iFileTypeId;

        private string _sFilePath;

        private System.Nullable<int> _iSnapshotId;

        private string _sUploadedStatus;

        private System.Nullable<System.DateTime> _dtUploadedDate;

        private string _sUploadedBy;

        private string _sFileType;

        public Get_InvoiceFileUploads_ByInvoiceIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iUploadedFileId", DbType = "Int NOT NULL")]
        public int iUploadedFileId
        {
            get
            {
                return this._iUploadedFileId;
            }
            set
            {
                if ((this._iUploadedFileId != value))
                {
                    this._iUploadedFileId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceId", DbType = "Int NOT NULL")]
        public int iInvoiceId
        {
            get
            {
                return this._iInvoiceId;
            }
            set
            {
                if ((this._iInvoiceId != value))
                {
                    this._iInvoiceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iFileTypeId", DbType = "Int NOT NULL")]
        public int iFileTypeId
        {
            get
            {
                return this._iFileTypeId;
            }
            set
            {
                if ((this._iFileTypeId != value))
                {
                    this._iFileTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFilePath", DbType = "VarChar(300) NOT NULL", CanBeNull = false)]
        public string sFilePath
        {
            get
            {
                return this._sFilePath;
            }
            set
            {
                if ((this._sFilePath != value))
                {
                    this._sFilePath = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iSnapshotId", DbType = "Int")]
        public System.Nullable<int> iSnapshotId
        {
            get
            {
                return this._iSnapshotId;
            }
            set
            {
                if ((this._iSnapshotId != value))
                {
                    this._iSnapshotId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sUploadedStatus", DbType = "VarChar(20)")]
        public string sUploadedStatus
        {
            get
            {
                return this._sUploadedStatus;
            }
            set
            {
                if ((this._sUploadedStatus != value))
                {
                    this._sUploadedStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtUploadedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtUploadedDate
        {
            get
            {
                return this._dtUploadedDate;
            }
            set
            {
                if ((this._dtUploadedDate != value))
                {
                    this._dtUploadedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sUploadedBy", DbType = "VarChar(30)")]
        public string sUploadedBy
        {
            get
            {
                return this._sUploadedBy;
            }
            set
            {
                if ((this._sUploadedBy != value))
                {
                    this._sUploadedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFileType", DbType = "VarChar(75) NOT NULL", CanBeNull = false)]
        public string sFileType
        {
            get
            {
                return this._sFileType;
            }
            set
            {
                if ((this._sFileType != value))
                {
                    this._sFileType = value;
                }
            }
        }
    }

    public partial class get_InvoiceNumbersByTypeIdResult
    {

        private int _iInvoiceId;

        private int _iInvoiceTypeId;

        private string _sInvoiceNumber;

        private int _iBillingMonth;

        private int _iBillingYear;

        private int _iDefaultImportCurrencyID;

        private string _sStatus;

        private string _sLastAction;

        private string _sRecordType1_Status;

        private System.Nullable<System.DateTime> _dtRecordType1_DateTime;

        private string _sRecordType2_Status;

        private System.Nullable<System.DateTime> _dtRecordType2_DateTime;

        private string _sRecordType3_Status;

        private System.Nullable<System.DateTime> _dtRecordType3_DateTime;

        private string _sRecordType4_Status;

        private System.Nullable<System.DateTime> _dtRecordType4_DateTime;

        private string _sRecordType5_Status;

        private System.Nullable<System.DateTime> _dtRecordType5_DateTime;

        private string _sBillingFileExport_Status;

        private System.Nullable<System.DateTime> _dtBillingFileExport_DateTime;

        private string _sBillingFileExport_Path;

        private int _sExportCurrencyID;

        private System.DateTime _dtCreatedDate;

        private string _sCreatedBy;

        private System.DateTime _dtLastUpdatedDate;

        private string _sLastUpdatedBy;

        private System.Nullable<int> _iServiceDeskSnapshot_id;

        private System.Nullable<System.DateTime> _dtInvoiceBillPeriodStart;

        private System.Nullable<System.DateTime> _dtInvoiceBillPeriodEnd;

        private bool _IsDeleted;

        private string _sBillingSystem;


        public get_InvoiceNumbersByTypeIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceId", DbType = "Int NOT NULL")]
        public int iInvoiceId
        {
            get
            {
                return this._iInvoiceId;
            }
            set
            {
                if ((this._iInvoiceId != value))
                {
                    this._iInvoiceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sInvoiceNumber
        {
            get
            {
                return this._sInvoiceNumber;
            }
            set
            {
                if ((this._sInvoiceNumber != value))
                {
                    this._sInvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iBillingMonth", DbType = "Int NOT NULL")]
        public int iBillingMonth
        {
            get
            {
                return this._iBillingMonth;
            }
            set
            {
                if ((this._iBillingMonth != value))
                {
                    this._iBillingMonth = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iBillingYear", DbType = "Int NOT NULL")]
        public int iBillingYear
        {
            get
            {
                return this._iBillingYear;
            }
            set
            {
                if ((this._iBillingYear != value))
                {
                    this._iBillingYear = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDefaultImportCurrencyID", DbType = "Int NOT NULL")]
        public int iDefaultImportCurrencyID
        {
            get
            {
                return this._iDefaultImportCurrencyID;
            }
            set
            {
                if ((this._iDefaultImportCurrencyID != value))
                {
                    this._iDefaultImportCurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sStatus", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sStatus
        {
            get
            {
                return this._sStatus;
            }
            set
            {
                if ((this._sStatus != value))
                {
                    this._sStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastAction", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sLastAction
        {
            get
            {
                return this._sLastAction;
            }
            set
            {
                if ((this._sLastAction != value))
                {
                    this._sLastAction = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType1_Status", DbType = "VarChar(50)")]
        public string sRecordType1_Status
        {
            get
            {
                return this._sRecordType1_Status;
            }
            set
            {
                if ((this._sRecordType1_Status != value))
                {
                    this._sRecordType1_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType1_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType1_DateTime
        {
            get
            {
                return this._dtRecordType1_DateTime;
            }
            set
            {
                if ((this._dtRecordType1_DateTime != value))
                {
                    this._dtRecordType1_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType2_Status", DbType = "VarChar(50)")]
        public string sRecordType2_Status
        {
            get
            {
                return this._sRecordType2_Status;
            }
            set
            {
                if ((this._sRecordType2_Status != value))
                {
                    this._sRecordType2_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType2_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType2_DateTime
        {
            get
            {
                return this._dtRecordType2_DateTime;
            }
            set
            {
                if ((this._dtRecordType2_DateTime != value))
                {
                    this._dtRecordType2_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType3_Status", DbType = "VarChar(50)")]
        public string sRecordType3_Status
        {
            get
            {
                return this._sRecordType3_Status;
            }
            set
            {
                if ((this._sRecordType3_Status != value))
                {
                    this._sRecordType3_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType3_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType3_DateTime
        {
            get
            {
                return this._dtRecordType3_DateTime;
            }
            set
            {
                if ((this._dtRecordType3_DateTime != value))
                {
                    this._dtRecordType3_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType4_Status", DbType = "VarChar(50)")]
        public string sRecordType4_Status
        {
            get
            {
                return this._sRecordType4_Status;
            }
            set
            {
                if ((this._sRecordType4_Status != value))
                {
                    this._sRecordType4_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType4_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType4_DateTime
        {
            get
            {
                return this._dtRecordType4_DateTime;
            }
            set
            {
                if ((this._dtRecordType4_DateTime != value))
                {
                    this._dtRecordType4_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType5_Status", DbType = "VarChar(50)")]
        public string sRecordType5_Status
        {
            get
            {
                return this._sRecordType5_Status;
            }
            set
            {
                if ((this._sRecordType5_Status != value))
                {
                    this._sRecordType5_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType5_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType5_DateTime
        {
            get
            {
                return this._dtRecordType5_DateTime;
            }
            set
            {
                if ((this._dtRecordType5_DateTime != value))
                {
                    this._dtRecordType5_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBillingFileExport_Status", DbType = "VarChar(50)")]
        public string sBillingFileExport_Status
        {
            get
            {
                return this._sBillingFileExport_Status;
            }
            set
            {
                if ((this._sBillingFileExport_Status != value))
                {
                    this._sBillingFileExport_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtBillingFileExport_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtBillingFileExport_DateTime
        {
            get
            {
                return this._dtBillingFileExport_DateTime;
            }
            set
            {
                if ((this._dtBillingFileExport_DateTime != value))
                {
                    this._dtBillingFileExport_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBillingFileExport_Path", DbType = "VarChar(8000)")]
        public string sBillingFileExport_Path
        {
            get
            {
                return this._sBillingFileExport_Path;
            }
            set
            {
                if ((this._sBillingFileExport_Path != value))
                {
                    this._sBillingFileExport_Path = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportCurrencyID", DbType = "Int NOT NULL")]
        public int sExportCurrencyID
        {
            get
            {
                return this._sExportCurrencyID;
            }
            set
            {
                if ((this._sExportCurrencyID != value))
                {
                    this._sExportCurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtCreatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime dtCreatedDate
        {
            get
            {
                return this._dtCreatedDate;
            }
            set
            {
                if ((this._dtCreatedDate != value))
                {
                    this._dtCreatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sCreatedBy", DbType = "VarChar(50)")]
        public string sCreatedBy
        {
            get
            {
                return this._sCreatedBy;
            }
            set
            {
                if ((this._sCreatedBy != value))
                {
                    this._sCreatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtLastUpdatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime dtLastUpdatedDate
        {
            get
            {
                return this._dtLastUpdatedDate;
            }
            set
            {
                if ((this._dtLastUpdatedDate != value))
                {
                    this._dtLastUpdatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastUpdatedBy", DbType = "VarChar(50)")]
        public string sLastUpdatedBy
        {
            get
            {
                return this._sLastUpdatedBy;
            }
            set
            {
                if ((this._sLastUpdatedBy != value))
                {
                    this._sLastUpdatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iServiceDeskSnapshot_id", DbType = "Int")]
        public System.Nullable<int> iServiceDeskSnapshot_id
        {
            get
            {
                return this._iServiceDeskSnapshot_id;
            }
            set
            {
                if ((this._iServiceDeskSnapshot_id != value))
                {
                    this._iServiceDeskSnapshot_id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtInvoiceBillPeriodStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtInvoiceBillPeriodStart
        {
            get
            {
                return this._dtInvoiceBillPeriodStart;
            }
            set
            {
                if ((this._dtInvoiceBillPeriodStart != value))
                {
                    this._dtInvoiceBillPeriodStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtInvoiceBillPeriodEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtInvoiceBillPeriodEnd
        {
            get
            {
                return this._dtInvoiceBillPeriodEnd;
            }
            set
            {
                if ((this._dtInvoiceBillPeriodEnd != value))
                {
                    this._dtInvoiceBillPeriodEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this._IsDeleted = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBillingSystem", DbType = "VarChar(10)")]
        public string sBillingSystem
        {
            get
            {
                return this._sBillingSystem;
            }
            set
            {
                if ((this._sBillingSystem != value))
                {
                    this._sBillingSystem = value;
                }
            }
        }
    }

    //cbe_11609
    public partial class get_InvoiceNumbersByTypeId_AllResult
    {

        private int _iInvoiceId;

        private int _iInvoiceTypeId;

        private string _sInvoiceNumber;

        private int _iBillingMonth;

        private int _iBillingYear;

        private int _iDefaultImportCurrencyID;

        private string _sStatus;

        private string _sLastAction;

        private string _sRecordType1_Status;

        private System.Nullable<System.DateTime> _dtRecordType1_DateTime;

        private string _sRecordType2_Status;

        private System.Nullable<System.DateTime> _dtRecordType2_DateTime;

        private string _sRecordType3_Status;

        private System.Nullable<System.DateTime> _dtRecordType3_DateTime;

        private string _sRecordType4_Status;

        private System.Nullable<System.DateTime> _dtRecordType4_DateTime;

        private string _sRecordType5_Status;

        private System.Nullable<System.DateTime> _dtRecordType5_DateTime;

        private string _sBillingFileExport_Status;

        private System.Nullable<System.DateTime> _dtBillingFileExport_DateTime;

        private string _sBillingFileExport_Path;

        private int _sExportCurrencyID;

        private System.DateTime _dtCreatedDate;

        private string _sCreatedBy;

        private System.DateTime _dtLastUpdatedDate;

        private string _sLastUpdatedBy;

        private System.Nullable<int> _iServiceDeskSnapshot_id;

        private System.Nullable<System.DateTime> _dtInvoiceBillPeriodStart;

        private System.Nullable<System.DateTime> _dtInvoiceBillPeriodEnd;

        private bool _IsDeleted;

        public get_InvoiceNumbersByTypeId_AllResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceId", DbType = "Int NOT NULL")]
        public int iInvoiceId
        {
            get
            {
                return this._iInvoiceId;
            }
            set
            {
                if ((this._iInvoiceId != value))
                {
                    this._iInvoiceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sInvoiceNumber
        {
            get
            {
                return this._sInvoiceNumber;
            }
            set
            {
                if ((this._sInvoiceNumber != value))
                {
                    this._sInvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iBillingMonth", DbType = "Int NOT NULL")]
        public int iBillingMonth
        {
            get
            {
                return this._iBillingMonth;
            }
            set
            {
                if ((this._iBillingMonth != value))
                {
                    this._iBillingMonth = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iBillingYear", DbType = "Int NOT NULL")]
        public int iBillingYear
        {
            get
            {
                return this._iBillingYear;
            }
            set
            {
                if ((this._iBillingYear != value))
                {
                    this._iBillingYear = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDefaultImportCurrencyID", DbType = "Int NOT NULL")]
        public int iDefaultImportCurrencyID
        {
            get
            {
                return this._iDefaultImportCurrencyID;
            }
            set
            {
                if ((this._iDefaultImportCurrencyID != value))
                {
                    this._iDefaultImportCurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sStatus", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sStatus
        {
            get
            {
                return this._sStatus;
            }
            set
            {
                if ((this._sStatus != value))
                {
                    this._sStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastAction", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sLastAction
        {
            get
            {
                return this._sLastAction;
            }
            set
            {
                if ((this._sLastAction != value))
                {
                    this._sLastAction = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType1_Status", DbType = "VarChar(50)")]
        public string sRecordType1_Status
        {
            get
            {
                return this._sRecordType1_Status;
            }
            set
            {
                if ((this._sRecordType1_Status != value))
                {
                    this._sRecordType1_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType1_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType1_DateTime
        {
            get
            {
                return this._dtRecordType1_DateTime;
            }
            set
            {
                if ((this._dtRecordType1_DateTime != value))
                {
                    this._dtRecordType1_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType2_Status", DbType = "VarChar(50)")]
        public string sRecordType2_Status
        {
            get
            {
                return this._sRecordType2_Status;
            }
            set
            {
                if ((this._sRecordType2_Status != value))
                {
                    this._sRecordType2_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType2_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType2_DateTime
        {
            get
            {
                return this._dtRecordType2_DateTime;
            }
            set
            {
                if ((this._dtRecordType2_DateTime != value))
                {
                    this._dtRecordType2_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType3_Status", DbType = "VarChar(50)")]
        public string sRecordType3_Status
        {
            get
            {
                return this._sRecordType3_Status;
            }
            set
            {
                if ((this._sRecordType3_Status != value))
                {
                    this._sRecordType3_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType3_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType3_DateTime
        {
            get
            {
                return this._dtRecordType3_DateTime;
            }
            set
            {
                if ((this._dtRecordType3_DateTime != value))
                {
                    this._dtRecordType3_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType4_Status", DbType = "VarChar(50)")]
        public string sRecordType4_Status
        {
            get
            {
                return this._sRecordType4_Status;
            }
            set
            {
                if ((this._sRecordType4_Status != value))
                {
                    this._sRecordType4_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType4_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType4_DateTime
        {
            get
            {
                return this._dtRecordType4_DateTime;
            }
            set
            {
                if ((this._dtRecordType4_DateTime != value))
                {
                    this._dtRecordType4_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType5_Status", DbType = "VarChar(50)")]
        public string sRecordType5_Status
        {
            get
            {
                return this._sRecordType5_Status;
            }
            set
            {
                if ((this._sRecordType5_Status != value))
                {
                    this._sRecordType5_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType5_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType5_DateTime
        {
            get
            {
                return this._dtRecordType5_DateTime;
            }
            set
            {
                if ((this._dtRecordType5_DateTime != value))
                {
                    this._dtRecordType5_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBillingFileExport_Status", DbType = "VarChar(50)")]
        public string sBillingFileExport_Status
        {
            get
            {
                return this._sBillingFileExport_Status;
            }
            set
            {
                if ((this._sBillingFileExport_Status != value))
                {
                    this._sBillingFileExport_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtBillingFileExport_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtBillingFileExport_DateTime
        {
            get
            {
                return this._dtBillingFileExport_DateTime;
            }
            set
            {
                if ((this._dtBillingFileExport_DateTime != value))
                {
                    this._dtBillingFileExport_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBillingFileExport_Path", DbType = "VarChar(8000)")]
        public string sBillingFileExport_Path
        {
            get
            {
                return this._sBillingFileExport_Path;
            }
            set
            {
                if ((this._sBillingFileExport_Path != value))
                {
                    this._sBillingFileExport_Path = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportCurrencyID", DbType = "Int NOT NULL")]
        public int sExportCurrencyID
        {
            get
            {
                return this._sExportCurrencyID;
            }
            set
            {
                if ((this._sExportCurrencyID != value))
                {
                    this._sExportCurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtCreatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime dtCreatedDate
        {
            get
            {
                return this._dtCreatedDate;
            }
            set
            {
                if ((this._dtCreatedDate != value))
                {
                    this._dtCreatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sCreatedBy", DbType = "VarChar(50)")]
        public string sCreatedBy
        {
            get
            {
                return this._sCreatedBy;
            }
            set
            {
                if ((this._sCreatedBy != value))
                {
                    this._sCreatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtLastUpdatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime dtLastUpdatedDate
        {
            get
            {
                return this._dtLastUpdatedDate;
            }
            set
            {
                if ((this._dtLastUpdatedDate != value))
                {
                    this._dtLastUpdatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastUpdatedBy", DbType = "VarChar(50)")]
        public string sLastUpdatedBy
        {
            get
            {
                return this._sLastUpdatedBy;
            }
            set
            {
                if ((this._sLastUpdatedBy != value))
                {
                    this._sLastUpdatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iServiceDeskSnapshot_id", DbType = "Int")]
        public System.Nullable<int> iServiceDeskSnapshot_id
        {
            get
            {
                return this._iServiceDeskSnapshot_id;
            }
            set
            {
                if ((this._iServiceDeskSnapshot_id != value))
                {
                    this._iServiceDeskSnapshot_id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtInvoiceBillPeriodStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtInvoiceBillPeriodStart
        {
            get
            {
                return this._dtInvoiceBillPeriodStart;
            }
            set
            {
                if ((this._dtInvoiceBillPeriodStart != value))
                {
                    this._dtInvoiceBillPeriodStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtInvoiceBillPeriodEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtInvoiceBillPeriodEnd
        {
            get
            {
                return this._dtInvoiceBillPeriodEnd;
            }
            set
            {
                if ((this._dtInvoiceBillPeriodEnd != value))
                {
                    this._dtInvoiceBillPeriodEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this._IsDeleted = value;
                }
            }
        }
    }

    public partial class mbm_RecordType1_GetByInvoice_OneTypeFile_SOO_AllResult
    {

        private string _LegalEntity;

        private string _NA_Billing_Number;

        private System.Nullable<int> _Count_of_Charges;

        private System.Nullable<decimal> _Duration;

        private System.Nullable<decimal> _Charge_Amount;

        public mbm_RecordType1_GetByInvoice_OneTypeFile_SOO_AllResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._LegalEntity;
            }
            set
            {
                if ((this._LegalEntity != value))
                {
                    this._LegalEntity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[NA Billing Number]", Storage = "_NA_Billing_Number", DbType = "VarChar(50)")]
        public string NA_Billing_Number
        {
            get
            {
                return this._NA_Billing_Number;
            }
            set
            {
                if ((this._NA_Billing_Number != value))
                {
                    this._NA_Billing_Number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Count of Charges]", Storage = "_Count_of_Charges", DbType = "Int")]
        public System.Nullable<int> Count_of_Charges
        {
            get
            {
                return this._Count_of_Charges;
            }
            set
            {
                if ((this._Count_of_Charges != value))
                {
                    this._Count_of_Charges = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Duration", DbType = "Decimal(38,4)")]
        public System.Nullable<decimal> Duration
        {
            get
            {
                return this._Duration;
            }
            set
            {
                if ((this._Duration != value))
                {
                    this._Duration = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Amount]", Storage = "_Charge_Amount", DbType = "Decimal(38,5)")]
        public System.Nullable<decimal> Charge_Amount
        {
            get
            {
                return this._Charge_Amount;
            }
            set
            {
                if ((this._Charge_Amount != value))
                {
                    this._Charge_Amount = value;
                }
            }
        }
    }

    public partial class mbm_RecordType2_GetByInvoice_OneFile_SOO_AllResult
    {

        private int _ID;

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string @__10_Digit_DID;

        private string _SSO;

        private string _Charge_Date;

        private string _Charge_Description;

        private string _Charge_Type;

        private string _Service_Start_Date;

        private string _Service_End_Date;

        private string _Invoice_Bill_Period_Start;

        private string _Invoice_Bill_Period_End;

        private System.Nullable<decimal> _Total;

        private string _Vendor_Name;

        private string _State_Province;

        private int _CurrencyConversionID;

        private string _LegalEntity;

        private string _AssetSearchCode1;

        private string _Department;

        private string _Cost_Center;

        private string _ALI_Code;

        private string _Supervisor;

        public mbm_RecordType2_GetByInvoice_OneFile_SOO_AllResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 Digit DID]", Storage = "__10_Digit_DID", DbType = "VarChar(8000)")]
        public string _10_Digit_DID
        {
            get
            {
                return this.@__10_Digit_DID;
            }
            set
            {
                if ((this.@__10_Digit_DID != value))
                {
                    this.@__10_Digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Date]", Storage = "_Charge_Date", DbType = "VarChar(50)")]
        public string Charge_Date
        {
            get
            {
                return this._Charge_Date;
            }
            set
            {
                if ((this._Charge_Date != value))
                {
                    this._Charge_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(255)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Type]", Storage = "_Charge_Type", DbType = "VarChar(50)")]
        public string Charge_Type
        {
            get
            {
                return this._Charge_Type;
            }
            set
            {
                if ((this._Charge_Type != value))
                {
                    this._Charge_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service Start Date]", Storage = "_Service_Start_Date", DbType = "VarChar(8000)")]
        public string Service_Start_Date
        {
            get
            {
                return this._Service_Start_Date;
            }
            set
            {
                if ((this._Service_Start_Date != value))
                {
                    this._Service_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Service End Date]", Storage = "_Service_End_Date", DbType = "VarChar(8000)")]
        public string Service_End_Date
        {
            get
            {
                return this._Service_End_Date;
            }
            set
            {
                if ((this._Service_End_Date != value))
                {
                    this._Service_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period Start]", Storage = "_Invoice_Bill_Period_Start", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_Start
        {
            get
            {
                return this._Invoice_Bill_Period_Start;
            }
            set
            {
                if ((this._Invoice_Bill_Period_Start != value))
                {
                    this._Invoice_Bill_Period_Start = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Invoice Bill Period End]", Storage = "_Invoice_Bill_Period_End", DbType = "VarChar(8000)")]
        public string Invoice_Bill_Period_End
        {
            get
            {
                return this._Invoice_Bill_Period_End;
            }
            set
            {
                if ((this._Invoice_Bill_Period_End != value))
                {
                    this._Invoice_Bill_Period_End = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._LegalEntity;
            }
            set
            {
                if ((this._LegalEntity != value))
                {
                    this._LegalEntity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssetSearchCode1", DbType = "VarChar(100)")]
        public string AssetSearchCode1
        {
            get
            {
                return this._AssetSearchCode1;
            }
            set
            {
                if ((this._AssetSearchCode1 != value))
                {
                    this._AssetSearchCode1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Department", DbType = "VarChar(200)")]
        public string Department
        {
            get
            {
                return this._Department;
            }
            set
            {
                if ((this._Department != value))
                {
                    this._Department = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Cost Center]", Storage = "_Cost_Center", DbType = "VarChar(200)")]
        public string Cost_Center
        {
            get
            {
                return this._Cost_Center;
            }
            set
            {
                if ((this._Cost_Center != value))
                {
                    this._Cost_Center = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[ALI Code]", Storage = "_ALI_Code", DbType = "VarChar(200)")]
        public string ALI_Code
        {
            get
            {
                return this._ALI_Code;
            }
            set
            {
                if ((this._ALI_Code != value))
                {
                    this._ALI_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Supervisor", DbType = "VarChar(200)")]
        public string Supervisor
        {
            get
            {
                return this._Supervisor;
            }
            set
            {
                if ((this._Supervisor != value))
                {
                    this._Supervisor = value;
                }
            }
        }
    }

    public partial class mbm_RT2Summary_GetByInvoice_SOO_AllResult
    {

        private string _Profiles;

        private int _NoOfTimes;

        private decimal _Charges;

        private int _iInvoiceID;

        public mbm_RT2Summary_GetByInvoice_SOO_AllResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Profiles", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Profiles
        {
            get
            {
                return this._Profiles;
            }
            set
            {
                if ((this._Profiles != value))
                {
                    this._Profiles = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoOfTimes", DbType = "Int NOT NULL")]
        public int NoOfTimes
        {
            get
            {
                return this._NoOfTimes;
            }
            set
            {
                if ((this._NoOfTimes != value))
                {
                    this._NoOfTimes = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Charges", DbType = "Decimal(18,5) NOT NULL")]
        public decimal Charges
        {
            get
            {
                return this._Charges;
            }
            set
            {
                if ((this._Charges != value))
                {
                    this._Charges = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceID", DbType = "Int NOT NULL")]
        public int iInvoiceID
        {
            get
            {
                return this._iInvoiceID;
            }
            set
            {
                if ((this._iInvoiceID != value))
                {
                    this._iInvoiceID = value;
                }
            }
        }
    }

    public partial class mbm_RecordType4_GetByInvoice_SOO_AllResult
    {

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string @__10_Digit_DID;

        private string _SSO;

        private string _Charge_Description;

        private System.Nullable<decimal> _Tax_Charge;

        private string _Vendor_Name;

        private string _BillDate;

        private System.Nullable<decimal> _Tax_Percentage;

        private string _ServiceType;

        private string _State_Province;

        private int _CurrencyConversionID;

        private string _LegalEntity;

        public mbm_RecordType4_GetByInvoice_SOO_AllResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[10 Digit DID]", Storage = "__10_Digit_DID", DbType = "VarChar(50)")]
        public string _10_Digit_DID
        {
            get
            {
                return this.@__10_Digit_DID;
            }
            set
            {
                if ((this.@__10_Digit_DID != value))
                {
                    this.@__10_Digit_DID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SSO", DbType = "VarChar(50)")]
        public string SSO
        {
            get
            {
                return this._SSO;
            }
            set
            {
                if ((this._SSO != value))
                {
                    this._SSO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Charge Description]", Storage = "_Charge_Description", DbType = "VarChar(50)")]
        public string Charge_Description
        {
            get
            {
                return this._Charge_Description;
            }
            set
            {
                if ((this._Charge_Description != value))
                {
                    this._Charge_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Tax Charge]", Storage = "_Tax_Charge", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> Tax_Charge
        {
            get
            {
                return this._Tax_Charge;
            }
            set
            {
                if ((this._Tax_Charge != value))
                {
                    this._Tax_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Vendor Name]", Storage = "_Vendor_Name", DbType = "VarChar(50)")]
        public string Vendor_Name
        {
            get
            {
                return this._Vendor_Name;
            }
            set
            {
                if ((this._Vendor_Name != value))
                {
                    this._Vendor_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillDate", DbType = "VarChar(50)")]
        public string BillDate
        {
            get
            {
                return this._BillDate;
            }
            set
            {
                if ((this._BillDate != value))
                {
                    this._BillDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Tax Percentage]", Storage = "_Tax_Percentage", DbType = "Decimal(18,10)")]
        public System.Nullable<decimal> Tax_Percentage
        {
            get
            {
                return this._Tax_Percentage;
            }
            set
            {
                if ((this._Tax_Percentage != value))
                {
                    this._Tax_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServiceType", DbType = "VarChar(50)")]
        public string ServiceType
        {
            get
            {
                return this._ServiceType;
            }
            set
            {
                if ((this._ServiceType != value))
                {
                    this._ServiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[State/Province]", Storage = "_State_Province", DbType = "VarChar(50)")]
        public string State_Province
        {
            get
            {
                return this._State_Province;
            }
            set
            {
                if ((this._State_Province != value))
                {
                    this._State_Province = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._LegalEntity;
            }
            set
            {
                if ((this._LegalEntity != value))
                {
                    this._LegalEntity = value;
                }
            }
        }
    }

    public partial class mbm_RecordType3_GetByInvoice_SOO_AllResult
    {

        private int _ID;

        private int _Record_Type;

        private string _BAN;

        private int _InvoiceID;

        private string _InvoiceNumber;

        private int _InvoiceType;

        private string _Bill_Date;

        private System.Nullable<decimal> _Record_Type_1_Total;

        private System.Nullable<decimal> _Record_Type_2_Total;

        private System.Nullable<decimal> _Record_Type_4_Total;

        private System.Nullable<decimal> _Total;

        private int _CurrencyConversionID;

        private string _LegalEntity;

        public mbm_RecordType3_GetByInvoice_SOO_AllResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type]", Storage = "_Record_Type", DbType = "Int NOT NULL")]
        public int Record_Type
        {
            get
            {
                return this._Record_Type;
            }
            set
            {
                if ((this._Record_Type != value))
                {
                    this._Record_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BAN", DbType = "VarChar(50)")]
        public string BAN
        {
            get
            {
                return this._BAN;
            }
            set
            {
                if ((this._BAN != value))
                {
                    this._BAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceID", DbType = "Int NOT NULL")]
        public int InvoiceID
        {
            get
            {
                return this._InvoiceID;
            }
            set
            {
                if ((this._InvoiceID != value))
                {
                    this._InvoiceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceType", DbType = "Int NOT NULL")]
        public int InvoiceType
        {
            get
            {
                return this._InvoiceType;
            }
            set
            {
                if ((this._InvoiceType != value))
                {
                    this._InvoiceType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Bill Date]", Storage = "_Bill_Date", DbType = "VarChar(50)")]
        public string Bill_Date
        {
            get
            {
                return this._Bill_Date;
            }
            set
            {
                if ((this._Bill_Date != value))
                {
                    this._Bill_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 1 Total]", Storage = "_Record_Type_1_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_1_Total
        {
            get
            {
                return this._Record_Type_1_Total;
            }
            set
            {
                if ((this._Record_Type_1_Total != value))
                {
                    this._Record_Type_1_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 2 Total]", Storage = "_Record_Type_2_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_2_Total
        {
            get
            {
                return this._Record_Type_2_Total;
            }
            set
            {
                if ((this._Record_Type_2_Total != value))
                {
                    this._Record_Type_2_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Record Type 4 Total]", Storage = "_Record_Type_4_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Record_Type_4_Total
        {
            get
            {
                return this._Record_Type_4_Total;
            }
            set
            {
                if ((this._Record_Type_4_Total != value))
                {
                    this._Record_Type_4_Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrencyConversionID", DbType = "Int NOT NULL")]
        public int CurrencyConversionID
        {
            get
            {
                return this._CurrencyConversionID;
            }
            set
            {
                if ((this._CurrencyConversionID != value))
                {
                    this._CurrencyConversionID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LegalEntity", DbType = "VarChar(100)")]
        public string LegalEntity
        {
            get
            {
                return this._LegalEntity;
            }
            set
            {
                if ((this._LegalEntity != value))
                {
                    this._LegalEntity = value;
                }
            }
        }
    }

    public partial class get_InvoiceDetailsByNumberResult
    {

        private int _iInvoiceId;

        private int _iInvoiceTypeId;

        private string _sInvoiceNumber;

        private int _iBillingMonth;

        private int _iBillingYear;

        private int _iDefaultImportCurrencyID;

        private string _sStatus;

        private string _sLastAction;

        private string _sRecordType1_Status;

        private System.Nullable<System.DateTime> _dtRecordType1_DateTime;

        private string _sRecordType2_Status;

        private System.Nullable<System.DateTime> _dtRecordType2_DateTime;

        private string _sRecordType3_Status;

        private System.Nullable<System.DateTime> _dtRecordType3_DateTime;

        private string _sRecordType4_Status;

        private System.Nullable<System.DateTime> _dtRecordType4_DateTime;

        private string _sRecordType5_Status;

        private System.Nullable<System.DateTime> _dtRecordType5_DateTime;

        private string _sBillingFileExport_Status;

        private System.Nullable<System.DateTime> _dtBillingFileExport_DateTime;

        private string _sBillingFileExport_Path;

        private int _sExportCurrencyID;

        private System.DateTime _dtCreatedDate;

        private string _sCreatedBy;

        private System.DateTime _dtLastUpdatedDate;

        private string _sLastUpdatedBy;

        private System.Nullable<int> _iServiceDeskSnapshot_id;

        private System.Nullable<System.DateTime> _dtInvoiceBillPeriodStart;

        private System.Nullable<System.DateTime> _dtInvoiceBillPeriodEnd;

        private bool _IsDeleted;

        private string _sBillingSystem;

        public get_InvoiceDetailsByNumberResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceId", DbType = "Int NOT NULL")]
        public int iInvoiceId
        {
            get
            {
                return this._iInvoiceId;
            }
            set
            {
                if ((this._iInvoiceId != value))
                {
                    this._iInvoiceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sInvoiceNumber
        {
            get
            {
                return this._sInvoiceNumber;
            }
            set
            {
                if ((this._sInvoiceNumber != value))
                {
                    this._sInvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iBillingMonth", DbType = "Int NOT NULL")]
        public int iBillingMonth
        {
            get
            {
                return this._iBillingMonth;
            }
            set
            {
                if ((this._iBillingMonth != value))
                {
                    this._iBillingMonth = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iBillingYear", DbType = "Int NOT NULL")]
        public int iBillingYear
        {
            get
            {
                return this._iBillingYear;
            }
            set
            {
                if ((this._iBillingYear != value))
                {
                    this._iBillingYear = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDefaultImportCurrencyID", DbType = "Int NOT NULL")]
        public int iDefaultImportCurrencyID
        {
            get
            {
                return this._iDefaultImportCurrencyID;
            }
            set
            {
                if ((this._iDefaultImportCurrencyID != value))
                {
                    this._iDefaultImportCurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sStatus", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sStatus
        {
            get
            {
                return this._sStatus;
            }
            set
            {
                if ((this._sStatus != value))
                {
                    this._sStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastAction", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sLastAction
        {
            get
            {
                return this._sLastAction;
            }
            set
            {
                if ((this._sLastAction != value))
                {
                    this._sLastAction = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType1_Status", DbType = "VarChar(50)")]
        public string sRecordType1_Status
        {
            get
            {
                return this._sRecordType1_Status;
            }
            set
            {
                if ((this._sRecordType1_Status != value))
                {
                    this._sRecordType1_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType1_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType1_DateTime
        {
            get
            {
                return this._dtRecordType1_DateTime;
            }
            set
            {
                if ((this._dtRecordType1_DateTime != value))
                {
                    this._dtRecordType1_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType2_Status", DbType = "VarChar(50)")]
        public string sRecordType2_Status
        {
            get
            {
                return this._sRecordType2_Status;
            }
            set
            {
                if ((this._sRecordType2_Status != value))
                {
                    this._sRecordType2_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType2_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType2_DateTime
        {
            get
            {
                return this._dtRecordType2_DateTime;
            }
            set
            {
                if ((this._dtRecordType2_DateTime != value))
                {
                    this._dtRecordType2_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType3_Status", DbType = "VarChar(50)")]
        public string sRecordType3_Status
        {
            get
            {
                return this._sRecordType3_Status;
            }
            set
            {
                if ((this._sRecordType3_Status != value))
                {
                    this._sRecordType3_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType3_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType3_DateTime
        {
            get
            {
                return this._dtRecordType3_DateTime;
            }
            set
            {
                if ((this._dtRecordType3_DateTime != value))
                {
                    this._dtRecordType3_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType4_Status", DbType = "VarChar(50)")]
        public string sRecordType4_Status
        {
            get
            {
                return this._sRecordType4_Status;
            }
            set
            {
                if ((this._sRecordType4_Status != value))
                {
                    this._sRecordType4_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType4_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType4_DateTime
        {
            get
            {
                return this._dtRecordType4_DateTime;
            }
            set
            {
                if ((this._dtRecordType4_DateTime != value))
                {
                    this._dtRecordType4_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sRecordType5_Status", DbType = "VarChar(50)")]
        public string sRecordType5_Status
        {
            get
            {
                return this._sRecordType5_Status;
            }
            set
            {
                if ((this._sRecordType5_Status != value))
                {
                    this._sRecordType5_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtRecordType5_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtRecordType5_DateTime
        {
            get
            {
                return this._dtRecordType5_DateTime;
            }
            set
            {
                if ((this._dtRecordType5_DateTime != value))
                {
                    this._dtRecordType5_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBillingFileExport_Status", DbType = "VarChar(50)")]
        public string sBillingFileExport_Status
        {
            get
            {
                return this._sBillingFileExport_Status;
            }
            set
            {
                if ((this._sBillingFileExport_Status != value))
                {
                    this._sBillingFileExport_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtBillingFileExport_DateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtBillingFileExport_DateTime
        {
            get
            {
                return this._dtBillingFileExport_DateTime;
            }
            set
            {
                if ((this._dtBillingFileExport_DateTime != value))
                {
                    this._dtBillingFileExport_DateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBillingFileExport_Path", DbType = "VarChar(8000)")]
        public string sBillingFileExport_Path
        {
            get
            {
                return this._sBillingFileExport_Path;
            }
            set
            {
                if ((this._sBillingFileExport_Path != value))
                {
                    this._sBillingFileExport_Path = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportCurrencyID", DbType = "Int NOT NULL")]
        public int sExportCurrencyID
        {
            get
            {
                return this._sExportCurrencyID;
            }
            set
            {
                if ((this._sExportCurrencyID != value))
                {
                    this._sExportCurrencyID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtCreatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime dtCreatedDate
        {
            get
            {
                return this._dtCreatedDate;
            }
            set
            {
                if ((this._dtCreatedDate != value))
                {
                    this._dtCreatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sCreatedBy", DbType = "VarChar(50)")]
        public string sCreatedBy
        {
            get
            {
                return this._sCreatedBy;
            }
            set
            {
                if ((this._sCreatedBy != value))
                {
                    this._sCreatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtLastUpdatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime dtLastUpdatedDate
        {
            get
            {
                return this._dtLastUpdatedDate;
            }
            set
            {
                if ((this._dtLastUpdatedDate != value))
                {
                    this._dtLastUpdatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastUpdatedBy", DbType = "VarChar(50)")]
        public string sLastUpdatedBy
        {
            get
            {
                return this._sLastUpdatedBy;
            }
            set
            {
                if ((this._sLastUpdatedBy != value))
                {
                    this._sLastUpdatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iServiceDeskSnapshot_id", DbType = "Int")]
        public System.Nullable<int> iServiceDeskSnapshot_id
        {
            get
            {
                return this._iServiceDeskSnapshot_id;
            }
            set
            {
                if ((this._iServiceDeskSnapshot_id != value))
                {
                    this._iServiceDeskSnapshot_id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtInvoiceBillPeriodStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtInvoiceBillPeriodStart
        {
            get
            {
                return this._dtInvoiceBillPeriodStart;
            }
            set
            {
                if ((this._dtInvoiceBillPeriodStart != value))
                {
                    this._dtInvoiceBillPeriodStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtInvoiceBillPeriodEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtInvoiceBillPeriodEnd
        {
            get
            {
                return this._dtInvoiceBillPeriodEnd;
            }
            set
            {
                if ((this._dtInvoiceBillPeriodEnd != value))
                {
                    this._dtInvoiceBillPeriodEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDeleted", DbType = "Bit NOT NULL")]
        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if ((this._IsDeleted != value))
                {
                    this._IsDeleted = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBillingSystem", DbType = "VarChar(10)")]
        public string sBillingSystem
        {
            get
            {
                return this._sBillingSystem;
            }
            set
            {
                if ((this._sBillingSystem != value))
                {
                    this._sBillingSystem = value;
                }
            }
        }
    }

    public partial class get_ProcessWorkFlowStatusResult
    {

        private int _Id;

        private string _sInvoiceNumber;

        private bool _sCompareToCRM;

        private bool _sViewChange;

        private bool _sApproveChange;

        private bool _sSyncCRM;

        private bool _sImportCRMData;

        private bool _sProcessInvoice;

        private bool _sExportInvoiceFile;

        //private bool _sIsSynced;

        public get_ProcessWorkFlowStatusResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "Int NOT NULL")]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sInvoiceNumber
        {
            get
            {
                return this._sInvoiceNumber;
            }
            set
            {
                if ((this._sInvoiceNumber != value))
                {
                    this._sInvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sCompareToCRM", DbType = "Bit NOT NULL")]
        public bool sCompareToCRM
        {
            get
            {
                return this._sCompareToCRM;
            }
            set
            {
                if ((this._sCompareToCRM != value))
                {
                    this._sCompareToCRM = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sViewChange", DbType = "Bit NOT NULL")]
        public bool sViewChange
        {
            get
            {
                return this._sViewChange;
            }
            set
            {
                if ((this._sViewChange != value))
                {
                    this._sViewChange = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sApproveChange", DbType = "Bit NOT NULL")]
        public bool sApproveChange
        {
            get
            {
                return this._sApproveChange;
            }
            set
            {
                if ((this._sApproveChange != value))
                {
                    this._sApproveChange = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSyncCRM", DbType = "Bit NOT NULL")]
        public bool sSyncCRM
        {
            get
            {
                return this._sSyncCRM;
            }
            set
            {
                if ((this._sSyncCRM != value))
                {
                    this._sSyncCRM = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sImportCRMData", DbType = "Bit NOT NULL")]
        public bool sImportCRMData
        {
            get
            {
                return this._sImportCRMData;
            }
            set
            {
                if ((this._sImportCRMData != value))
                {
                    this._sImportCRMData = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sProcessInvoice", DbType = "Bit NOT NULL")]
        public bool sProcessInvoice
        {
            get
            {
                return this._sProcessInvoice;
            }
            set
            {
                if ((this._sProcessInvoice != value))
                {
                    this._sProcessInvoice = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportInvoiceFile", DbType = "Bit NOT NULL")]
        public bool sExportInvoiceFile
        {
            get
            {
                return this._sExportInvoiceFile;
            }
            set
            {
                if ((this._sExportInvoiceFile != value))
                {
                    this._sExportInvoiceFile = value;
                }
            }
        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sIsSynced", DbType = "Bit NOT NULL")]
        //public bool sIsSynced
        //{
        //    get
        //    {
        //        return this._sIsSynced;
        //    }
        //    set
        //    {
        //        if ((this._sIsSynced != value))
        //        {
        //            this._sIsSynced = value;
        //        }
        //    }
        //}
    }

    public partial class get_InvoiceFileExportsResult
    {

        private string _sInvoiceNumber;

        private string _sExportFileName;

        private string _sExportFilePath;

        private System.Nullable<System.DateTime> _dtExportDate;

        private string _sExportedBy;

        public get_InvoiceFileExportsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sInvoiceNumber
        {
            get
            {
                return this._sInvoiceNumber;
            }
            set
            {
                if ((this._sInvoiceNumber != value))
                {
                    this._sInvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportFileName", DbType = "VarChar(100)")]
        public string sExportFileName
        {
            get
            {
                return this._sExportFileName;
            }
            set
            {
                if ((this._sExportFileName != value))
                {
                    this._sExportFileName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportFilePath", DbType = "VarChar(100)")]
        public string sExportFilePath
        {
            get
            {
                return this._sExportFilePath;
            }
            set
            {
                if ((this._sExportFilePath != value))
                {
                    this._sExportFilePath = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtExportDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtExportDate
        {
            get
            {
                return this._dtExportDate;
            }
            set
            {
                if ((this._dtExportDate != value))
                {
                    this._dtExportDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportedBy", DbType = "VarChar(100)")]
        public string sExportedBy
        {
            get
            {
                return this._sExportedBy;
            }
            set
            {
                if ((this._sExportedBy != value))
                {
                    this._sExportedBy = value;
                }
            }
        }
    }

    public partial class Get_UnprocessedMBMComparisonResultResult
    {

        private System.Nullable<int> _iSnapshotId;

        private System.Nullable<int> _iInvoiceId;

        private string _sCRMAccountNumber;

        private string _sAssetSearchCode1;

        private string _sAssetSearchCode2;

        private System.Nullable<decimal> _mCharge;

        private int _iActionType;

        private string _sProfileName;

        private System.Nullable<int> _iGLCode;

        private string _sFeatureCode;

        private System.Nullable<System.DateTime> _dtMainStart;

        private System.Nullable<System.DateTime> _dtMainEnd;

        private string _sItemType;

        private string _sSubType;

        private string _sSwitchId;

        private string _sItemId;

        private string _sSequenceId;

        private System.Nullable<int> _iLoadId;

        private System.Nullable<int> _iProcessed;

        public Get_UnprocessedMBMComparisonResultResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iSnapshotId", DbType = "Int")]
        public System.Nullable<int> iSnapshotId
        {
            get
            {
                return this._iSnapshotId;
            }
            set
            {
                if ((this._iSnapshotId != value))
                {
                    this._iSnapshotId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceId", DbType = "Int")]
        public System.Nullable<int> iInvoiceId
        {
            get
            {
                return this._iInvoiceId;
            }
            set
            {
                if ((this._iInvoiceId != value))
                {
                    this._iInvoiceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sCRMAccountNumber", DbType = "VarChar(13)")]
        public string sCRMAccountNumber
        {
            get
            {
                return this._sCRMAccountNumber;
            }
            set
            {
                if ((this._sCRMAccountNumber != value))
                {
                    this._sCRMAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode1", DbType = "VarChar(50)")]
        public string sAssetSearchCode1
        {
            get
            {
                return this._sAssetSearchCode1;
            }
            set
            {
                if ((this._sAssetSearchCode1 != value))
                {
                    this._sAssetSearchCode1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode2", DbType = "VarChar(50)")]
        public string sAssetSearchCode2
        {
            get
            {
                return this._sAssetSearchCode2;
            }
            set
            {
                if ((this._sAssetSearchCode2 != value))
                {
                    this._sAssetSearchCode2 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mCharge", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> mCharge
        {
            get
            {
                return this._mCharge;
            }
            set
            {
                if ((this._mCharge != value))
                {
                    this._mCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iActionType", DbType = "Int NOT NULL")]
        public int iActionType
        {
            get
            {
                return this._iActionType;
            }
            set
            {
                if ((this._iActionType != value))
                {
                    this._iActionType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sProfileName", DbType = "VarChar(100)")]
        public string sProfileName
        {
            get
            {
                return this._sProfileName;
            }
            set
            {
                if ((this._sProfileName != value))
                {
                    this._sProfileName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iGLCode", DbType = "Int")]
        public System.Nullable<int> iGLCode
        {
            get
            {
                return this._iGLCode;
            }
            set
            {
                if ((this._iGLCode != value))
                {
                    this._iGLCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFeatureCode", DbType = "VarChar(50)")]
        public string sFeatureCode
        {
            get
            {
                return this._sFeatureCode;
            }
            set
            {
                if ((this._sFeatureCode != value))
                {
                    this._sFeatureCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainStart
        {
            get
            {
                return this._dtMainStart;
            }
            set
            {
                if ((this._dtMainStart != value))
                {
                    this._dtMainStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainEnd
        {
            get
            {
                return this._dtMainEnd;
            }
            set
            {
                if ((this._dtMainEnd != value))
                {
                    this._dtMainEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sItemType", DbType = "VarChar(10)")]
        public string sItemType
        {
            get
            {
                return this._sItemType;
            }
            set
            {
                if ((this._sItemType != value))
                {
                    this._sItemType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSubType", DbType = "VarChar(10)")]
        public string sSubType
        {
            get
            {
                return this._sSubType;
            }
            set
            {
                if ((this._sSubType != value))
                {
                    this._sSubType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSwitchId", DbType = "VarChar(20)")]
        public string sSwitchId
        {
            get
            {
                return this._sSwitchId;
            }
            set
            {
                if ((this._sSwitchId != value))
                {
                    this._sSwitchId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sItemId", DbType = "VarChar(20)")]
        public string sItemId
        {
            get
            {
                return this._sItemId;
            }
            set
            {
                if ((this._sItemId != value))
                {
                    this._sItemId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSequenceId", DbType = "VarChar(20)")]
        public string sSequenceId
        {
            get
            {
                return this._sSequenceId;
            }
            set
            {
                if ((this._sSequenceId != value))
                {
                    this._sSequenceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iLoadId", DbType = "Int")]
        public System.Nullable<int> iLoadId
        {
            get
            {
                return this._iLoadId;
            }
            set
            {
                if ((this._iLoadId != value))
                {
                    this._iLoadId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iProcessed", DbType = "Int")]
        public System.Nullable<int> iProcessed
        {
            get
            {
                return this._iProcessed;
            }
            set
            {
                if ((this._iProcessed != value))
                {
                    this._iProcessed = value;
                }
            }
        }
    }

    //1582
    //Get_UnprocessedMBMComparisonResultResultCSG

    public partial class Get_UnprocessedMBMComparisonResultResultCSG
    {

        private System.Nullable<int> _iSnapshotId;

        private System.Nullable<int> _iInvoiceId;

        private string _sCSGAccountNumber;

        private string _sSubscriberId;

        private string _sAssetSearchCode1;

        private string _sAssetSearchCode2;

        private System.Nullable<decimal> _mCharge;

        private int _iActionType;

        private string _sProfileName;

        //private System.Nullable<int> _iGLCode;

        //private string _sFeatureCode;

        private string _sOfferExternalRef;
        private string _sProductExternalRef;
        private string _sPricingPlanExternalRef;
        private string _sMBMUniqueID;

        private System.Nullable<System.DateTime> _dtMainStart;

        private System.Nullable<System.DateTime> _dtMainEnd;

        private string _sItemType;

        private string _sSubType;

        private string _sSwitchId;

        private string _sItemId;

        private string _sSequenceId;

        private System.Nullable<int> _iLoadId;

        private System.Nullable<int> _iProcessed;


        //Sero-3511 Start 
        private System.Nullable<int> _iProcessFirst;
        private string _sContractNumber;
        private DateTime _dContractStartDate;
        private DateTime _dContractEndDate;
        private string _sGLDepartmentCode;
        private string _sIndirectAgentRegion;
        private string _sIndirectPartnerCode;
        //Sero-3511 End 


        public Get_UnprocessedMBMComparisonResultResultCSG()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iSnapshotId", DbType = "Int")]
        public System.Nullable<int> iSnapshotId
        {
            get
            {
                return this._iSnapshotId;
            }
            set
            {
                if ((this._iSnapshotId != value))
                {
                    this._iSnapshotId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceId", DbType = "Int")]
        public System.Nullable<int> iInvoiceId
        {
            get
            {
                return this._iInvoiceId;
            }
            set
            {
                if ((this._iInvoiceId != value))
                {
                    this._iInvoiceId = value;
                }
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sCSGAccountNumber", DbType = "VarChar(13)")]
        public string sCSGAccountNumber
        {
            get
            {
                return this._sCSGAccountNumber;
            }
            set
            {
                if ((this._sCSGAccountNumber != value))
                {
                    this._sCSGAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode1", DbType = "VarChar(50)")]
        public string sAssetSearchCode1
        {
            get
            {
                return this._sAssetSearchCode1;
            }
            set
            {
                if ((this._sAssetSearchCode1 != value))
                {
                    this._sAssetSearchCode1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode2", DbType = "VarChar(50)")]
        public string sAssetSearchCode2
        {
            get
            {
                return this._sAssetSearchCode2;
            }
            set
            {
                if ((this._sAssetSearchCode2 != value))
                {
                    this._sAssetSearchCode2 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mCharge", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> mCharge
        {
            get
            {
                return this._mCharge;
            }
            set
            {
                if ((this._mCharge != value))
                {
                    this._mCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iActionType", DbType = "Int NOT NULL")]
        public int iActionType
        {
            get
            {
                return this._iActionType;
            }
            set
            {
                if ((this._iActionType != value))
                {
                    this._iActionType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sProfileName", DbType = "VarChar(100)")]
        public string sProfileName
        {
            get
            {
                return this._sProfileName;
            }
            set
            {
                if ((this._sProfileName != value))
                {
                    this._sProfileName = value;
                }
            }
        }

        //23032022
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sOfferExternalRef", DbType = "VarChar(50)")]
        public string sOfferExternalRef
        {
            get
            {
                return this._sOfferExternalRef;
            }
            set
            {
                if ((this._sOfferExternalRef != value))
                {
                    this._sOfferExternalRef = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sProductExternalRef", DbType = "VarChar(50)")]
        public string sProductExternalRef
        {
            get
            {
                return this._sProductExternalRef;
            }
            set
            {
                if ((this._sProductExternalRef != value))
                {
                    this._sProductExternalRef = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPricingPlanExternalRef", DbType = "VarChar(50)")]
        public string sPricingPlanExternalRef
        {
            get
            {
                return this._sPricingPlanExternalRef;
            }
            set
            {
                if ((this._sPricingPlanExternalRef != value))
                {
                    this._sPricingPlanExternalRef = value;
                }
            }
        }


        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sMBMUniqueID", DbType = "VarChar(50)")]
        public string sMBMUniqueID
        {
            get
            {
                return this._sMBMUniqueID;
            }
            set
            {
                if ((this._sMBMUniqueID != value))
                {
                    this._sMBMUniqueID = value;
                }
            }
        }
        //end



        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainStart
        {
            get
            {
                return this._dtMainStart;
            }
            set
            {
                if ((this._dtMainStart != value))
                {
                    this._dtMainStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainEnd
        {
            get
            {
                return this._dtMainEnd;
            }
            set
            {
                if ((this._dtMainEnd != value))
                {
                    this._dtMainEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sItemType", DbType = "VarChar(10)")]
        public string sItemType
        {
            get
            {
                return this._sItemType;
            }
            set
            {
                if ((this._sItemType != value))
                {
                    this._sItemType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSubType", DbType = "VarChar(10)")]
        public string sSubType
        {
            get
            {
                return this._sSubType;
            }
            set
            {
                if ((this._sSubType != value))
                {
                    this._sSubType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSwitchId", DbType = "VarChar(20)")]
        public string sSwitchId
        {
            get
            {
                return this._sSwitchId;
            }
            set
            {
                if ((this._sSwitchId != value))
                {
                    this._sSwitchId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sItemId", DbType = "VarChar(20)")]
        public string sItemId
        {
            get
            {
                return this._sItemId;
            }
            set
            {
                if ((this._sItemId != value))
                {
                    this._sItemId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSequenceId", DbType = "VarChar(20)")]
        public string sSequenceId
        {
            get
            {
                return this._sSequenceId;
            }
            set
            {
                if ((this._sSequenceId != value))
                {
                    this._sSequenceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iLoadId", DbType = "Int")]
        public System.Nullable<int> iLoadId
        {
            get
            {
                return this._iLoadId;
            }
            set
            {
                if ((this._iLoadId != value))
                {
                    this._iLoadId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iProcessed", DbType = "Int")]
        public System.Nullable<int> iProcessed
        {
            get
            {
                return this._iProcessed;
            }
            set
            {
                if ((this._iProcessed != value))
                {
                    this._iProcessed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iProcessFirst", DbType = "Int")]
        public System.Nullable<int> iProcessFirst
        {
            get
            {
                return this._iProcessFirst;
            }
            set
            {
                if ((this._iProcessFirst != value))
                {
                    this._iProcessFirst = value;
                }
            }
        }


        //SERo-3511 start

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sContractNumber", DbType = "VarChar(50)")]
        public string sContractNumber
        {
            get
            {
                return this._sContractNumber;
            }
            set
            {
                if ((this._sContractNumber != value))
                {
                    this._sContractNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dContractStartDate", DbType = "DateTime")]
        public DateTime dContractStartDate
        {
            get
            {
                return this._dContractStartDate;
            }
            set
            {
                if ((this._dContractStartDate != value))
                {
                    this._dContractStartDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dContractEndDate", DbType = "DateTime")]
        public DateTime dContractEndDate
        {
            get
            {
                return this._dContractEndDate;
            }
            set
            {
                if ((this._dContractEndDate != value))
                {
                    this._dContractEndDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sIndirectAgentRegion", DbType = "VarChar(100)")]
        public string sIndirectAgentRegion
        {
            get
            {
                return this._sIndirectAgentRegion;
            }
            set
            {
                if ((this._sIndirectAgentRegion != value))
                {
                    this._sIndirectAgentRegion = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sIndirectPartnerCode", DbType = "nVarChar(50)")]
        public string sIndirectPartnerCode
        {
            get
            {
                return this._sIndirectPartnerCode;
            }
            set
            {
                if ((this._sIndirectPartnerCode != value))
                {
                    this._sIndirectPartnerCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sGLDepartmentCode", DbType = "nVarChar(100)")]
        public string sGLDepartmentCode
        {
            get
            {
                return this._sGLDepartmentCode;
            }
            set
            {
                if ((this._sGLDepartmentCode != value))
                {
                    this._sGLDepartmentCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSubscriberId", DbType = "VarChar(50)")]
        public string sSubscriberId
        {
            get
            {
                return this._sSubscriberId;
            }
            set
            {
                if ((this._sSubscriberId != value))
                {
                    this._sSubscriberId = value;
                }
            }
        }

        //SERO-3511
    }


    public partial class mbm_Profiles_GetByInvoiceIdResult
    {

        private int _iProfileId;

        private string _sProfileName;

        private System.Nullable<int> _iInvoiceTypeId;

        private string _sDescription;

        public mbm_Profiles_GetByInvoiceIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iProfileId", DbType = "Int NOT NULL")]
        public int iProfileId
        {
            get
            {
                return this._iProfileId;
            }
            set
            {
                if ((this._iProfileId != value))
                {
                    this._iProfileId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sProfileName", DbType = "VarChar(100)")]
        public string sProfileName
        {
            get
            {
                return this._sProfileName;
            }
            set
            {
                if ((this._sProfileName != value))
                {
                    this._sProfileName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int")]
        public System.Nullable<int> iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDescription", DbType = "VarChar(250)")]
        public string sDescription
        {
            get
            {
                return this._sDescription;
            }
            set
            {
                if ((this._sDescription != value))
                {
                    this._sDescription = value;
                }
            }
        }
    }

    //SERO-1582

    public partial class mbm_ChargeStringIdentifier_GetByInvoiceIdResult
    {

        private int _iProfileId;

        private string _iChargeStringIdentifier;

        private System.Nullable<int> _iInvoiceTypeId;

        private int _iCatalogItemId;  //PC

        public mbm_ChargeStringIdentifier_GetByInvoiceIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iProfileId", DbType = "Int NOT NULL")]
        public int iProfileId
        {
            get
            {
                return this._iProfileId;
            }
            set
            {
                if ((this._iProfileId != value))
                {
                    this._iProfileId = value;
                }
            }
        }

        //pc
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iCatalogItemId", DbType = "Int NOT NULL")]
        public int iCatalogItemId
        {
            get
            {
                return this._iCatalogItemId;
            }
            set
            {
                if ((this._iCatalogItemId != value))
                {
                    this._iCatalogItemId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iChargeStringIdentifier", DbType = "VarChar(100)")]
        public string iChargeStringIdentifier
        {
            get
            {
                return this._iChargeStringIdentifier;
            }
            set
            {
                if ((this._iChargeStringIdentifier != value))
                {
                    this._iChargeStringIdentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int")]
        public System.Nullable<int> iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }


    }


    public partial class mbm_ProfileCharges_GetByInvoiceResult
    {

        private int _iprofileid;

        private int _ichargeid;

        private string _sprofilename;

        private System.Nullable<int> _iglcode;

        private string _sfeature;

        private string _schargetype;

        private System.Nullable<decimal> _mcharge;

        private string _sChargeIdentifier;//SERO-1582

        public mbm_ProfileCharges_GetByInvoiceResult()
        {
        }

        //SERO-1582 starts
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sChargeIdentifier", DbType = "VarChar(50)")]
        public string sChargeIdentifier
        {
            get
            {
                return this._sChargeIdentifier;
            }
            set
            {
                if ((this._sChargeIdentifier != value))
                {
                    this._sChargeIdentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iprofileid", DbType = "Int NOT NULL")]
        public int iprofileid
        {
            get
            {
                return this._iprofileid;
            }
            set
            {
                if ((this._iprofileid != value))
                {
                    this._iprofileid = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ichargeid", DbType = "Int NOT NULL")]
        public int ichargeid
        {
            get
            {
                return this._ichargeid;
            }
            set
            {
                if ((this._ichargeid != value))
                {
                    this._ichargeid = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sprofilename", DbType = "VarChar(100)")]
        public string sprofilename
        {
            get
            {
                return this._sprofilename;
            }
            set
            {
                if ((this._sprofilename != value))
                {
                    this._sprofilename = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iglcode", DbType = "Int")]
        public System.Nullable<int> iglcode
        {
            get
            {
                return this._iglcode;
            }
            set
            {
                if ((this._iglcode != value))
                {
                    this._iglcode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sfeature", DbType = "VarChar(50)")]
        public string sfeature
        {
            get
            {
                return this._sfeature;
            }
            set
            {
                if ((this._sfeature != value))
                {
                    this._sfeature = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_schargetype", DbType = "VarChar(50)")]
        public string schargetype
        {
            get
            {
                return this._schargetype;
            }
            set
            {
                if ((this._schargetype != value))
                {
                    this._schargetype = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mcharge", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> mcharge
        {
            get
            {
                return this._mcharge;
            }
            set
            {
                if ((this._mcharge != value))
                {
                    this._mcharge = value;
                }
            }
        }
    }

    //SERO-1582
    //Get ProfileCharges for CSG
    public partial class mbm_ProfileCharges_GetByInvoice_CSGResult
    {


        private string _sprofilename;

        private string _sChargeIdentifier;

        private int _iprofileid;

        private int _iinvoicetypeid;

        private int _icatalogitemid;

        private int _ichargeid;

        private System.Nullable<decimal> _mcharge;
        // private System.Nullable<DateTime> _dEffectiveStartDate;
        private System.Nullable<bool> _bIsQuorumSynced;


        public mbm_ProfileCharges_GetByInvoice_CSGResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sChargeIdentifier", DbType = "VarChar(50)")]
        public string sChargeIdentifier
        {
            get
            {
                return this._sChargeIdentifier;
            }
            set
            {
                if ((this._sChargeIdentifier != value))
                {
                    this._sChargeIdentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sprofilename", DbType = "VarChar(100)")]
        public string sprofilename
        {
            get
            {
                return this._sprofilename;
            }
            set
            {
                if ((this._sprofilename != value))
                {
                    this._sprofilename = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iprofileid", DbType = "Int NOT NULL")]
        public int iprofileid
        {
            get
            {
                return this._iprofileid;
            }
            set
            {
                if ((this._iprofileid != value))
                {
                    this._iprofileid = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iinvoicetypeid", DbType = "Int NOT NULL")]
        public int iinvoicetypeid
        {
            get
            {
                return this._iinvoicetypeid;
            }
            set
            {
                if ((this._iinvoicetypeid != value))
                {
                    this._iinvoicetypeid = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_icatalogitemid", DbType = "Int NOT NULL")]
        public int icatalogitemid
        {
            get
            {
                return this._icatalogitemid;
            }
            set
            {
                if ((this._icatalogitemid != value))
                {
                    this._icatalogitemid = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ichargeid", DbType = "Int NOT NULL")]
        public int ichargeid
        {
            get
            {
                return this._ichargeid;
            }
            set
            {
                if ((this._ichargeid != value))
                {
                    this._ichargeid = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mcharge", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> mcharge
        {
            get
            {
                return this._mcharge;
            }
            set
            {
                if ((this._mcharge != value))
                {
                    this._mcharge = value;
                }
            }
        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dEffectiveStartDate", DbType = "DateTime")]
        //public System.Nullable<System.DateTime> dEffectiveStartDate
        //{
        //    get
        //    {
        //        return this._dEffectiveStartDate;
        //    }
        //    set
        //    {
        //        if ((this._dEffectiveStartDate != value))
        //        {
        //            this._dEffectiveStartDate = value;
        //        }
        //    }
        //}

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsQuorumSynced", DbType = "bit")]
        public System.Nullable<bool> bIsQuorumSynced
        {
            get
            {
                return this._bIsQuorumSynced;
            }
            set
            {
                if ((this._bIsQuorumSynced != value))
                {
                    this._bIsQuorumSynced = value;
                }
            }
        }

    }


    //end

    public partial class mbm_CSGPricingPlanId_GetByInvoiceResult
    {

        private string _iPricingPlanId;

        private string _sChargeIdentifier;//SERO-1582

        public mbm_CSGPricingPlanId_GetByInvoiceResult()
        {
        }

        //SERO-1582 starts
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sChargeIdentifier", DbType = "VarChar(50)")]
        public string sChargeIdentifier
        {
            get
            {
                return this._sChargeIdentifier;
            }
            set
            {
                if ((this._sChargeIdentifier != value))
                {
                    this._sChargeIdentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iPricingPlanId", DbType = "VarChar(50)")]
        public string iPricingPlanId
        {
            get
            {
                return this._iPricingPlanId;
            }
            set
            {
                if ((this._iPricingPlanId != value))
                {
                    this._iPricingPlanId = value;
                }
            }
        }
    }
    public partial class mbm_ProfileCharges_ValidateFromCRMResult
    {

        private string _ProfileDesc;

        private string _Feature;

        public mbm_ProfileCharges_ValidateFromCRMResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProfileDesc", DbType = "VarChar(50)")]
        public string ProfileDesc
        {
            get
            {
                return this._ProfileDesc;
            }
            set
            {
                if ((this._ProfileDesc != value))
                {
                    this._ProfileDesc = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Feature", DbType = "VarChar(50)")]
        public string Feature
        {
            get
            {
                return this._Feature;
            }
            set
            {
                if ((this._Feature != value))
                {
                    this._Feature = value;
                }
            }
        }
    }

    public partial class mbm_ManualCharge_GetFilesInfoByInvoiceIdResult
    {

        private int _iFileId;

        private System.Nullable<int> _RecordCount;

        private int _iInvoiceTypeId;

        private string _ManualChargeFileName;

        private string _FilePath;

        private string _sFileStatus;

        private System.Nullable<System.DateTime> _dtLastModified;

        private System.DateTime _dtImported;

        private string _sImportedBy;

        public mbm_ManualCharge_GetFilesInfoByInvoiceIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iFileId", DbType = "Int NOT NULL")]
        public int iFileId
        {
            get
            {
                return this._iFileId;
            }
            set
            {
                if ((this._iFileId != value))
                {
                    this._iFileId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RecordCount", DbType = "Int")]
        public System.Nullable<int> RecordCount
        {
            get
            {
                return this._RecordCount;
            }
            set
            {
                if ((this._RecordCount != value))
                {
                    this._RecordCount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ManualChargeFileName", DbType = "NVarChar(MAX)")]
        public string ManualChargeFileName
        {
            get
            {
                return this._ManualChargeFileName;
            }
            set
            {
                if ((this._ManualChargeFileName != value))
                {
                    this._ManualChargeFileName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FilePath", DbType = "VarChar(500)")]
        public string FilePath
        {
            get
            {
                return this._FilePath;
            }
            set
            {
                if ((this._FilePath != value))
                {
                    this._FilePath = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFileStatus", DbType = "VarChar(50)")]
        public string sFileStatus
        {
            get
            {
                return this._sFileStatus;
            }
            set
            {
                if ((this._sFileStatus != value))
                {
                    this._sFileStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtLastModified", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtLastModified
        {
            get
            {
                return this._dtLastModified;
            }
            set
            {
                if ((this._dtLastModified != value))
                {
                    this._dtLastModified = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtImported", DbType = "DateTime NOT NULL")]
        public System.DateTime dtImported
        {
            get
            {
                return this._dtImported;
            }
            set
            {
                if ((this._dtImported != value))
                {
                    this._dtImported = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sImportedBy", DbType = "VarChar(50)")]
        public string sImportedBy
        {
            get
            {
                return this._sImportedBy;
            }
            set
            {
                if ((this._sImportedBy != value))
                {
                    this._sImportedBy = value;
                }
            }
        }
    }

    public partial class mbm_ManualCharge_GetChargesByFileIdResult
    {

        private int _iInvoiceTypeId;

        private string _sLegalEntity;

        private string _sSubIdentifier;

        private string _sAssetSearch;

        private string _sServiceProfileId;

        private string _dtMainStart;

        private string _dtMainEnd;

        private string _sDirectoryNumber;

        private string _sServiceProfileUid;

        private string _sStatus;

        private System.Nullable<System.DateTime> _dtProcessed;

        private string _sComment;

        public mbm_ManualCharge_GetChargesByFileIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLegalEntity", DbType = "VarChar(300) NOT NULL", CanBeNull = false)]
        public string sLegalEntity
        {
            get
            {
                return this._sLegalEntity;
            }
            set
            {
                if ((this._sLegalEntity != value))
                {
                    this._sLegalEntity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSubIdentifier", DbType = "VarChar(200)")]
        public string sSubIdentifier
        {
            get
            {
                return this._sSubIdentifier;
            }
            set
            {
                if ((this._sSubIdentifier != value))
                {
                    this._sSubIdentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearch", DbType = "VarChar(200)")]
        public string sAssetSearch
        {
            get
            {
                return this._sAssetSearch;
            }
            set
            {
                if ((this._sAssetSearch != value))
                {
                    this._sAssetSearch = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sServiceProfileId", DbType = "VarChar(200) NOT NULL", CanBeNull = false)]
        public string sServiceProfileId
        {
            get
            {
                return this._sServiceProfileId;
            }
            set
            {
                if ((this._sServiceProfileId != value))
                {
                    this._sServiceProfileId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainStart", DbType = "VarChar(30)")]
        public string dtMainStart
        {
            get
            {
                return this._dtMainStart;
            }
            set
            {
                if ((this._dtMainStart != value))
                {
                    this._dtMainStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainEnd", DbType = "VarChar(30)")]
        public string dtMainEnd
        {
            get
            {
                return this._dtMainEnd;
            }
            set
            {
                if ((this._dtMainEnd != value))
                {
                    this._dtMainEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDirectoryNumber", DbType = "VarChar(100)")]
        public string sDirectoryNumber
        {
            get
            {
                return this._sDirectoryNumber;
            }
            set
            {
                if ((this._sDirectoryNumber != value))
                {
                    this._sDirectoryNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sServiceProfileUid", DbType = "VarChar(100)")]
        public string sServiceProfileUid
        {
            get
            {
                return this._sServiceProfileUid;
            }
            set
            {
                if ((this._sServiceProfileUid != value))
                {
                    this._sServiceProfileUid = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sStatus", DbType = "VarChar(50)")]
        public string sStatus
        {
            get
            {
                return this._sStatus;
            }
            set
            {
                if ((this._sStatus != value))
                {
                    this._sStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtProcessed", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtProcessed
        {
            get
            {
                return this._dtProcessed;
            }
            set
            {
                if ((this._dtProcessed != value))
                {
                    this._dtProcessed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sComment", DbType = "VarChar(500)")]
        public string sComment
        {
            get
            {
                return this._sComment;
            }
            set
            {
                if ((this._sComment != value))
                {
                    this._sComment = value;
                }
            }
        }
    }

    public partial class mbm_ManualCharge_getSuccessRecordsByInvoiceIdResult
    {

        private string _sLegalEntity;

        private string _sSubIdentifier;

        private string _sAssetSearch;

        private string _sServiceProfileId;

        private string _dtMainStart;

        private string _dtMainEnd;

        private string _sDirectoryNumber;

        private string _sServiceProfileUid;

        public mbm_ManualCharge_getSuccessRecordsByInvoiceIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLegalEntity", DbType = "VarChar(300) NOT NULL", CanBeNull = false)]
        public string sLegalEntity
        {
            get
            {
                return this._sLegalEntity;
            }
            set
            {
                if ((this._sLegalEntity != value))
                {
                    this._sLegalEntity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSubIdentifier", DbType = "VarChar(200)")]
        public string sSubIdentifier
        {
            get
            {
                return this._sSubIdentifier;
            }
            set
            {
                if ((this._sSubIdentifier != value))
                {
                    this._sSubIdentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearch", DbType = "VarChar(200)")]
        public string sAssetSearch
        {
            get
            {
                return this._sAssetSearch;
            }
            set
            {
                if ((this._sAssetSearch != value))
                {
                    this._sAssetSearch = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sServiceProfileId", DbType = "VarChar(200) NOT NULL", CanBeNull = false)]
        public string sServiceProfileId
        {
            get
            {
                return this._sServiceProfileId;
            }
            set
            {
                if ((this._sServiceProfileId != value))
                {
                    this._sServiceProfileId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainStart", DbType = "VarChar(10)")]
        public string dtMainStart
        {
            get
            {
                return this._dtMainStart;
            }
            set
            {
                if ((this._dtMainStart != value))
                {
                    this._dtMainStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainEnd", DbType = "VarChar(10)")]
        public string dtMainEnd
        {
            get
            {
                return this._dtMainEnd;
            }
            set
            {
                if ((this._dtMainEnd != value))
                {
                    this._dtMainEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDirectoryNumber", DbType = "VarChar(100)")]
        public string sDirectoryNumber
        {
            get
            {
                return this._sDirectoryNumber;
            }
            set
            {
                if ((this._sDirectoryNumber != value))
                {
                    this._sDirectoryNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sServiceProfileUid", DbType = "VarChar(100)")]
        public string sServiceProfileUid
        {
            get
            {
                return this._sServiceProfileUid;
            }
            set
            {
                if ((this._sServiceProfileUid != value))
                {
                    this._sServiceProfileUid = value;
                }
            }
        }
    }

    public partial class mbm_RT1Summary_GetByInvoiceResult
    {

        private string _Zone;

        private int _NoOfTimes;

        private decimal _Duration;

        private decimal _CallCharges;

        private int _invoiceID;

        public mbm_RT1Summary_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Zone", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Zone
        {
            get
            {
                return this._Zone;
            }
            set
            {
                if ((this._Zone != value))
                {
                    this._Zone = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoOfTimes", DbType = "Int NOT NULL")]
        public int NoOfTimes
        {
            get
            {
                return this._NoOfTimes;
            }
            set
            {
                if ((this._NoOfTimes != value))
                {
                    this._NoOfTimes = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Duration", DbType = "Decimal(18,4) NOT NULL")]
        public decimal Duration
        {
            get
            {
                return this._Duration;
            }
            set
            {
                if ((this._Duration != value))
                {
                    this._Duration = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CallCharges", DbType = "Decimal(18,5) NOT NULL")]
        public decimal CallCharges
        {
            get
            {
                return this._CallCharges;
            }
            set
            {
                if ((this._CallCharges != value))
                {
                    this._CallCharges = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_invoiceID", DbType = "Int NOT NULL")]
        public int invoiceID
        {
            get
            {
                return this._invoiceID;
            }
            set
            {
                if ((this._invoiceID != value))
                {
                    this._invoiceID = value;
                }
            }
        }
    }

    public partial class mbm_RT2Summary_GetByInvoiceResult
    {

        private string _Profiles;

        private int _NoOfTimes;

        private decimal _Charges;

        private int _iInvoiceID;

        public mbm_RT2Summary_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Profiles", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Profiles
        {
            get
            {
                return this._Profiles;
            }
            set
            {
                if ((this._Profiles != value))
                {
                    this._Profiles = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoOfTimes", DbType = "Int NOT NULL")]
        public int NoOfTimes
        {
            get
            {
                return this._NoOfTimes;
            }
            set
            {
                if ((this._NoOfTimes != value))
                {
                    this._NoOfTimes = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Charges", DbType = "Decimal(18,5) NOT NULL")]
        public decimal Charges
        {
            get
            {
                return this._Charges;
            }
            set
            {
                if ((this._Charges != value))
                {
                    this._Charges = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceID", DbType = "Int NOT NULL")]
        public int iInvoiceID
        {
            get
            {
                return this._iInvoiceID;
            }
            set
            {
                if ((this._iInvoiceID != value))
                {
                    this._iInvoiceID = value;
                }
            }
        }
    }

    public partial class mbm_RT4Summary_GetByInvoiceResult
    {

        private string _ChargeDescription;

        private int _NoOfTimes;

        private decimal _TaxCharges;

        private int _invoiceID;

        public mbm_RT4Summary_GetByInvoiceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ChargeDescription", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ChargeDescription
        {
            get
            {
                return this._ChargeDescription;
            }
            set
            {
                if ((this._ChargeDescription != value))
                {
                    this._ChargeDescription = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoOfTimes", DbType = "Int NOT NULL")]
        public int NoOfTimes
        {
            get
            {
                return this._NoOfTimes;
            }
            set
            {
                if ((this._NoOfTimes != value))
                {
                    this._NoOfTimes = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TaxCharges", DbType = "Decimal(18,5) NOT NULL")]
        public decimal TaxCharges
        {
            get
            {
                return this._TaxCharges;
            }
            set
            {
                if ((this._TaxCharges != value))
                {
                    this._TaxCharges = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_invoiceID", DbType = "Int NOT NULL")]
        public int invoiceID
        {
            get
            {
                return this._invoiceID;
            }
            set
            {
                if ((this._invoiceID != value))
                {
                    this._invoiceID = value;
                }
            }
        }
    }


    public partial class get_EHCSEhancedDataResult
    {

        private string _Customer;

        private string _InvoiceNumber;

        private System.Nullable<int> _iSnapshotId;

        private System.Nullable<int> _iUploadFileId;

        private string _sSubIdentifier;

        private string _sFirstName;

        private string _sLastName;

        private string _sAssetSearchCode1;

        private string _sAssetSearchCode2;

        private string _sProfileName;

        private string _sLegalEntity;

        private string _sChargeCode;

        private System.Nullable<decimal> _mCharge;

        private System.Nullable<System.DateTime> _dtMainStart;

        private System.Nullable<System.DateTime> _dtMainEnd;

        private System.Nullable<int> _iGLCode;

        private string _sFeatureCode;

        private System.Nullable<System.DateTime> _dtInsertedDate;

        private string _e164_mask;

        private string _ActiveCharge;

        private System.Nullable<System.DateTime> _EffectiveBillingDate;

        public get_EHCSEhancedDataResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Customer", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Customer
        {
            get
            {
                return this._Customer;
            }
            set
            {
                if ((this._Customer != value))
                {
                    this._Customer = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iSnapshotId", DbType = "Int")]
        public System.Nullable<int> iSnapshotId
        {
            get
            {
                return this._iSnapshotId;
            }
            set
            {
                if ((this._iSnapshotId != value))
                {
                    this._iSnapshotId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iUploadFileId", DbType = "Int")]
        public System.Nullable<int> iUploadFileId
        {
            get
            {
                return this._iUploadFileId;
            }
            set
            {
                if ((this._iUploadFileId != value))
                {
                    this._iUploadFileId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSubIdentifier", DbType = "VarChar(100)")]
        public string sSubIdentifier
        {
            get
            {
                return this._sSubIdentifier;
            }
            set
            {
                if ((this._sSubIdentifier != value))
                {
                    this._sSubIdentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFirstName", DbType = "VarChar(30)")]
        public string sFirstName
        {
            get
            {
                return this._sFirstName;
            }
            set
            {
                if ((this._sFirstName != value))
                {
                    this._sFirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastName", DbType = "VarChar(30)")]
        public string sLastName
        {
            get
            {
                return this._sLastName;
            }
            set
            {
                if ((this._sLastName != value))
                {
                    this._sLastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode1", DbType = "VarChar(50)")]
        public string sAssetSearchCode1
        {
            get
            {
                return this._sAssetSearchCode1;
            }
            set
            {
                if ((this._sAssetSearchCode1 != value))
                {
                    this._sAssetSearchCode1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode2", DbType = "VarChar(100)")]
        public string sAssetSearchCode2
        {
            get
            {
                return this._sAssetSearchCode2;
            }
            set
            {
                if ((this._sAssetSearchCode2 != value))
                {
                    this._sAssetSearchCode2 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sProfileName", DbType = "VarChar(100)")]
        public string sProfileName
        {
            get
            {
                return this._sProfileName;
            }
            set
            {
                if ((this._sProfileName != value))
                {
                    this._sProfileName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLegalEntity", DbType = "VarChar(13)")]
        public string sLegalEntity
        {
            get
            {
                return this._sLegalEntity;
            }
            set
            {
                if ((this._sLegalEntity != value))
                {
                    this._sLegalEntity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sChargeCode", DbType = "VarChar(30)")]
        public string sChargeCode
        {
            get
            {
                return this._sChargeCode;
            }
            set
            {
                if ((this._sChargeCode != value))
                {
                    this._sChargeCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mCharge", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> mCharge
        {
            get
            {
                return this._mCharge;
            }
            set
            {
                if ((this._mCharge != value))
                {
                    this._mCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainStart
        {
            get
            {
                return this._dtMainStart;
            }
            set
            {
                if ((this._dtMainStart != value))
                {
                    this._dtMainStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainEnd
        {
            get
            {
                return this._dtMainEnd;
            }
            set
            {
                if ((this._dtMainEnd != value))
                {
                    this._dtMainEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iGLCode", DbType = "Int")]
        public System.Nullable<int> iGLCode
        {
            get
            {
                return this._iGLCode;
            }
            set
            {
                if ((this._iGLCode != value))
                {
                    this._iGLCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFeatureCode", DbType = "VarChar(50)")]
        public string sFeatureCode
        {
            get
            {
                return this._sFeatureCode;
            }
            set
            {
                if ((this._sFeatureCode != value))
                {
                    this._sFeatureCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtInsertedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtInsertedDate
        {
            get
            {
                return this._dtInsertedDate;
            }
            set
            {
                if ((this._dtInsertedDate != value))
                {
                    this._dtInsertedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_e164_mask", DbType = "VarChar(100)")]
        public string e164_mask
        {
            get
            {
                return this._e164_mask;
            }
            set
            {
                if ((this._e164_mask != value))
                {
                    this._e164_mask = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActiveCharge", DbType = "VarChar(3) NOT NULL", CanBeNull = false)]
        public string ActiveCharge
        {
            get
            {
                return this._ActiveCharge;
            }
            set
            {
                if ((this._ActiveCharge != value))
                {
                    this._ActiveCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EffectiveBillingDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EffectiveBillingDate
        {
            get
            {
                return this._EffectiveBillingDate;
            }
            set
            {
                if ((this._EffectiveBillingDate != value))
                {
                    this._EffectiveBillingDate = value;
                }
            }
        }
    }

    //cbe_9941
    public partial class get_SOOFixedChargesDataResult
    {

        private long _CRM_ACCOUNT;

        private string _TN;

        private string _PROFILE_TYPE;

        private System.Nullable<decimal> _mCharge;

        private System.Nullable<System.DateTime> _dtMainStart;

        private System.Nullable<System.DateTime> _dtMainEnd;

        private string _CUSTOMER_DEPARTMENT;

        private string _CUSTOMER_COST_CENTER;

        private string _CUSTOMER_ALI_CODE;

        private string _SUPERVISOR_NAME;

        private string _FIRST_NAME;

        private string _LAST_NAME;

        private string _EMAIL;

        private int _QUANTITY;

        private System.Nullable<decimal> _UNIT_PRICE;

        private string _DESCRIPTION;

        public get_SOOFixedChargesDataResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CRM_ACCOUNT", DbType = "bigInt NOT NULL")]
        public long CRM_ACCOUNT
        {
            get
            {
                return this._CRM_ACCOUNT;
            }
            set
            {
                if ((this._CRM_ACCOUNT != value))
                {
                    this._CRM_ACCOUNT = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TN", DbType = "VarChar(100)")]
        public string TN
        {
            get
            {
                return this._TN;
            }
            set
            {
                if ((this._TN != value))
                {
                    this._TN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PROFILE_TYPE", DbType = "VarChar(100)")]
        public string PROFILE_TYPE
        {
            get
            {
                return this._PROFILE_TYPE;
            }
            set
            {
                if ((this._PROFILE_TYPE != value))
                {
                    this._PROFILE_TYPE = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mCharge", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> mCharge
        {
            get
            {
                return this._mCharge;
            }
            set
            {
                if ((this._mCharge != value))
                {
                    this._mCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainStart
        {
            get
            {
                return this._dtMainStart;
            }
            set
            {
                if ((this._dtMainStart != value))
                {
                    this._dtMainStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainEnd
        {
            get
            {
                return this._dtMainEnd;
            }
            set
            {
                if ((this._dtMainEnd != value))
                {
                    this._dtMainEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CUSTOMER_DEPARTMENT", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
        public string CUSTOMER_DEPARTMENT
        {
            get
            {
                return this._CUSTOMER_DEPARTMENT;
            }
            set
            {
                if ((this._CUSTOMER_DEPARTMENT != value))
                {
                    this._CUSTOMER_DEPARTMENT = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CUSTOMER_COST_CENTER", DbType = "VarChar(11) NOT NULL", CanBeNull = false)]
        public string CUSTOMER_COST_CENTER
        {
            get
            {
                return this._CUSTOMER_COST_CENTER;
            }
            set
            {
                if ((this._CUSTOMER_COST_CENTER != value))
                {
                    this._CUSTOMER_COST_CENTER = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CUSTOMER_ALI_CODE", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
        public string CUSTOMER_ALI_CODE
        {
            get
            {
                return this._CUSTOMER_ALI_CODE;
            }
            set
            {
                if ((this._CUSTOMER_ALI_CODE != value))
                {
                    this._CUSTOMER_ALI_CODE = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SUPERVISOR_NAME", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
        public string SUPERVISOR_NAME
        {
            get
            {
                return this._SUPERVISOR_NAME;
            }
            set
            {
                if ((this._SUPERVISOR_NAME != value))
                {
                    this._SUPERVISOR_NAME = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FIRST_NAME", DbType = "VarChar(100)")]
        public string FIRST_NAME
        {
            get
            {
                return this._FIRST_NAME;
            }
            set
            {
                if ((this._FIRST_NAME != value))
                {
                    this._FIRST_NAME = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LAST_NAME", DbType = "VarChar(100)")]
        public string LAST_NAME
        {
            get
            {
                return this._LAST_NAME;
            }
            set
            {
                if ((this._LAST_NAME != value))
                {
                    this._LAST_NAME = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMAIL", DbType = "VarChar(200)")]
        public string EMAIL
        {
            get
            {
                return this._EMAIL;
            }
            set
            {
                if ((this._EMAIL != value))
                {
                    this._EMAIL = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_QUANTITY", DbType = "Int NOT NULL")]
        public int QUANTITY
        {
            get
            {
                return this._QUANTITY;
            }
            set
            {
                if ((this._QUANTITY != value))
                {
                    this._QUANTITY = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UNIT_PRICE", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> UNIT_PRICE
        {
            get
            {
                return this._UNIT_PRICE;
            }
            set
            {
                if ((this._UNIT_PRICE != value))
                {
                    this._UNIT_PRICE = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DESCRIPTION", DbType = "VarChar(50)")]
        public string DESCRIPTION
        {
            get
            {
                return this._DESCRIPTION;
            }
            set
            {
                if ((this._DESCRIPTION != value))
                {
                    this._DESCRIPTION = value;
                }
            }
        }

    }

    public partial class get_EHCSRawDataResult
    {

        private System.Nullable<int> _snapshot_id;

        private string _subidentifier;

        private string _first_name;

        private string _last_name;

        private string _asset_search;

        private string _service_profile_id;

        private System.Nullable<System.DateTime> _service_start_date;

        private System.Nullable<System.DateTime> _service_end_date;

        private string _legal_entity;

        private string _state;

        private string _postal_code;

        private string _country;

        private string _directory_number;

        private string _service_profile_uid;

        private string _e164_mask;

        private string _ActiveCharge;

        private System.Nullable<System.DateTime> _EffectiveBillingDate;

        public get_EHCSRawDataResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_snapshot_id", DbType = "Int")]
        public System.Nullable<int> snapshot_id
        {
            get
            {
                return this._snapshot_id;
            }
            set
            {
                if ((this._snapshot_id != value))
                {
                    this._snapshot_id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_subidentifier", DbType = "VarChar(200)")]
        public string subidentifier
        {
            get
            {
                return this._subidentifier;
            }
            set
            {
                if ((this._subidentifier != value))
                {
                    this._subidentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_first_name", DbType = "VarChar(100)")]
        public string first_name
        {
            get
            {
                return this._first_name;
            }
            set
            {
                if ((this._first_name != value))
                {
                    this._first_name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_last_name", DbType = "VarChar(100)")]
        public string last_name
        {
            get
            {
                return this._last_name;
            }
            set
            {
                if ((this._last_name != value))
                {
                    this._last_name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_asset_search", DbType = "VarChar(200)")]
        public string asset_search
        {
            get
            {
                return this._asset_search;
            }
            set
            {
                if ((this._asset_search != value))
                {
                    this._asset_search = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_service_profile_id", DbType = "VarChar(200) NOT NULL", CanBeNull = false)]
        public string service_profile_id
        {
            get
            {
                return this._service_profile_id;
            }
            set
            {
                if ((this._service_profile_id != value))
                {
                    this._service_profile_id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_service_start_date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> service_start_date
        {
            get
            {
                return this._service_start_date;
            }
            set
            {
                if ((this._service_start_date != value))
                {
                    this._service_start_date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_service_end_date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> service_end_date
        {
            get
            {
                return this._service_end_date;
            }
            set
            {
                if ((this._service_end_date != value))
                {
                    this._service_end_date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_legal_entity", DbType = "VarChar(300) NOT NULL", CanBeNull = false)]
        public string legal_entity
        {
            get
            {
                return this._legal_entity;
            }
            set
            {
                if ((this._legal_entity != value))
                {
                    this._legal_entity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_state", DbType = "VarChar(60)")]
        public string state
        {
            get
            {
                return this._state;
            }
            set
            {
                if ((this._state != value))
                {
                    this._state = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_postal_code", DbType = "VarChar(50)")]
        public string postal_code
        {
            get
            {
                return this._postal_code;
            }
            set
            {
                if ((this._postal_code != value))
                {
                    this._postal_code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_country", DbType = "VarChar(30)")]
        public string country
        {
            get
            {
                return this._country;
            }
            set
            {
                if ((this._country != value))
                {
                    this._country = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_directory_number", DbType = "VarChar(100)")]
        public string directory_number
        {
            get
            {
                return this._directory_number;
            }
            set
            {
                if ((this._directory_number != value))
                {
                    this._directory_number = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_service_profile_uid", DbType = "VarChar(100)")]
        public string service_profile_uid
        {
            get
            {
                return this._service_profile_uid;
            }
            set
            {
                if ((this._service_profile_uid != value))
                {
                    this._service_profile_uid = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_e164_mask", DbType = "VarChar(100)")]
        public string e164_mask
        {
            get
            {
                return this._e164_mask;
            }
            set
            {
                if ((this._e164_mask != value))
                {
                    this._e164_mask = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActiveCharge", DbType = "VarChar(3) NOT NULL", CanBeNull = false)]
        public string ActiveCharge
        {
            get
            {
                return this._ActiveCharge;
            }
            set
            {
                if ((this._ActiveCharge != value))
                {
                    this._ActiveCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EffectiveBillingDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EffectiveBillingDate
        {
            get
            {
                return this._EffectiveBillingDate;
            }
            set
            {
                if ((this._EffectiveBillingDate != value))
                {
                    this._EffectiveBillingDate = value;
                }
            }
        }
    }

    public partial class get_EHCSUnEhancedDataResult
    {

        private string _Customer;

        private string _InvoiceNumber;

        private int _iDataId;

        private System.Nullable<int> _iSnapshotId;

        private System.Nullable<int> _iUploadFileId;

        private string _sSubIdentifier;

        private string _sFirstName;

        private string _sLastName;

        private string _sAssetSearchCode1;

        private string _sAssetSearchCode2;

        private string _sLegalEntity;

        private string _sServiceProfileId;

        private System.Nullable<System.DateTime> _dtMainStart;

        private System.Nullable<System.DateTime> _dtMainEnd;

        private string _state;

        private string _zipCode;

        private string _country;

        private System.Nullable<System.DateTime> _dtInsertedDate;

        private string _e164_mask;

        private string _ActiveCharge;

        private System.Nullable<System.DateTime> _EffectiveBillingDate;

        public get_EHCSUnEhancedDataResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Customer", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Customer
        {
            get
            {
                return this._Customer;
            }
            set
            {
                if ((this._Customer != value))
                {
                    this._Customer = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDataId", DbType = "Int NOT NULL")]
        public int iDataId
        {
            get
            {
                return this._iDataId;
            }
            set
            {
                if ((this._iDataId != value))
                {
                    this._iDataId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iSnapshotId", DbType = "Int")]
        public System.Nullable<int> iSnapshotId
        {
            get
            {
                return this._iSnapshotId;
            }
            set
            {
                if ((this._iSnapshotId != value))
                {
                    this._iSnapshotId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iUploadFileId", DbType = "Int")]
        public System.Nullable<int> iUploadFileId
        {
            get
            {
                return this._iUploadFileId;
            }
            set
            {
                if ((this._iUploadFileId != value))
                {
                    this._iUploadFileId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sSubIdentifier", DbType = "VarChar(100)")]
        public string sSubIdentifier
        {
            get
            {
                return this._sSubIdentifier;
            }
            set
            {
                if ((this._sSubIdentifier != value))
                {
                    this._sSubIdentifier = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFirstName", DbType = "VarChar(30)")]
        public string sFirstName
        {
            get
            {
                return this._sFirstName;
            }
            set
            {
                if ((this._sFirstName != value))
                {
                    this._sFirstName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLastName", DbType = "VarChar(30)")]
        public string sLastName
        {
            get
            {
                return this._sLastName;
            }
            set
            {
                if ((this._sLastName != value))
                {
                    this._sLastName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode1", DbType = "VarChar(100)")]
        public string sAssetSearchCode1
        {
            get
            {
                return this._sAssetSearchCode1;
            }
            set
            {
                if ((this._sAssetSearchCode1 != value))
                {
                    this._sAssetSearchCode1 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sAssetSearchCode2", DbType = "VarChar(100)")]
        public string sAssetSearchCode2
        {
            get
            {
                return this._sAssetSearchCode2;
            }
            set
            {
                if ((this._sAssetSearchCode2 != value))
                {
                    this._sAssetSearchCode2 = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sLegalEntity", DbType = "VarChar(13)")]
        public string sLegalEntity
        {
            get
            {
                return this._sLegalEntity;
            }
            set
            {
                if ((this._sLegalEntity != value))
                {
                    this._sLegalEntity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sServiceProfileId", DbType = "VarChar(100)")]
        public string sServiceProfileId
        {
            get
            {
                return this._sServiceProfileId;
            }
            set
            {
                if ((this._sServiceProfileId != value))
                {
                    this._sServiceProfileId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainStart", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainStart
        {
            get
            {
                return this._dtMainStart;
            }
            set
            {
                if ((this._dtMainStart != value))
                {
                    this._dtMainStart = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtMainEnd", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMainEnd
        {
            get
            {
                return this._dtMainEnd;
            }
            set
            {
                if ((this._dtMainEnd != value))
                {
                    this._dtMainEnd = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_state", DbType = "VarChar(60)")]
        public string state
        {
            get
            {
                return this._state;
            }
            set
            {
                if ((this._state != value))
                {
                    this._state = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_zipCode", DbType = "VarChar(50)")]
        public string zipCode
        {
            get
            {
                return this._zipCode;
            }
            set
            {
                if ((this._zipCode != value))
                {
                    this._zipCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_country", DbType = "VarChar(30)")]
        public string country
        {
            get
            {
                return this._country;
            }
            set
            {
                if ((this._country != value))
                {
                    this._country = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtInsertedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtInsertedDate
        {
            get
            {
                return this._dtInsertedDate;
            }
            set
            {
                if ((this._dtInsertedDate != value))
                {
                    this._dtInsertedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_e164_mask", DbType = "VarChar(100)")]
        public string e164_mask
        {
            get
            {
                return this._e164_mask;
            }
            set
            {
                if ((this._e164_mask != value))
                {
                    this._e164_mask = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActiveCharge", DbType = "VarChar(3) NOT NULL", CanBeNull = false)]
        public string ActiveCharge
        {
            get
            {
                return this._ActiveCharge;
            }
            set
            {
                if ((this._ActiveCharge != value))
                {
                    this._ActiveCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EffectiveBillingDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EffectiveBillingDate
        {
            get
            {
                return this._EffectiveBillingDate;
            }
            set
            {
                if ((this._EffectiveBillingDate != value))
                {
                    this._EffectiveBillingDate = value;
                }
            }
        }
    }

    public partial class Get_MBMAutomateStatusByInvoiceIdResult
    {

        private int _ID;

        private int _InvoiceTypeId;

        private int _ImportCurrencyId;

        private int _ExportCurrenyId;

        private int _BillingMonth;

        private int _BillingYear;

        private string _InsertedBy;

        private System.Nullable<System.DateTime> _InsertedDate;

        private string _InvoiceNumber;

        private System.Nullable<int> _InvoiceId;

        private bool _IsNewInvoice;

        private bool _IsInvoiceCreated;

        private bool _IsManualFileUpload;

        private bool _IsFileTypeUploaded;

        private bool _IsComparedToCRM;

        private bool _IsViwedChanged;

        private bool _IsApprovalMailSent;

        private bool _IsApprovedChange;

        private bool _IsProcessed;

        private bool _isPreBillingCompleted;

        private bool _IsApprovedByMail;

        private bool _IsSyncedCRM;

        private bool _IsImportCRM;

        private bool _IsProcessInvoice;

        private bool _IsExportInvoice;

        private string _LastAction;

        private System.Nullable<bool> _isPreBillingOverriden;

        public Get_MBMAutomateStatusByInvoiceIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceTypeId", DbType = "Int NOT NULL")]
        public int InvoiceTypeId
        {
            get
            {
                return this._InvoiceTypeId;
            }
            set
            {
                if ((this._InvoiceTypeId != value))
                {
                    this._InvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ImportCurrencyId", DbType = "Int NOT NULL")]
        public int ImportCurrencyId
        {
            get
            {
                return this._ImportCurrencyId;
            }
            set
            {
                if ((this._ImportCurrencyId != value))
                {
                    this._ImportCurrencyId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ExportCurrenyId", DbType = "Int NOT NULL")]
        public int ExportCurrenyId
        {
            get
            {
                return this._ExportCurrenyId;
            }
            set
            {
                if ((this._ExportCurrenyId != value))
                {
                    this._ExportCurrenyId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingMonth", DbType = "Int NOT NULL")]
        public int BillingMonth
        {
            get
            {
                return this._BillingMonth;
            }
            set
            {
                if ((this._BillingMonth != value))
                {
                    this._BillingMonth = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BillingYear", DbType = "Int NOT NULL")]
        public int BillingYear
        {
            get
            {
                return this._BillingYear;
            }
            set
            {
                if ((this._BillingYear != value))
                {
                    this._BillingYear = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InsertedBy", DbType = "VarChar(50)")]
        public string InsertedBy
        {
            get
            {
                return this._InsertedBy;
            }
            set
            {
                if ((this._InsertedBy != value))
                {
                    this._InsertedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InsertedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> InsertedDate
        {
            get
            {
                return this._InsertedDate;
            }
            set
            {
                if ((this._InsertedDate != value))
                {
                    this._InsertedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceNumber", DbType = "VarChar(50)")]
        public string InvoiceNumber
        {
            get
            {
                return this._InvoiceNumber;
            }
            set
            {
                if ((this._InvoiceNumber != value))
                {
                    this._InvoiceNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InvoiceId", DbType = "Int")]
        public System.Nullable<int> InvoiceId
        {
            get
            {
                return this._InvoiceId;
            }
            set
            {
                if ((this._InvoiceId != value))
                {
                    this._InvoiceId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsNewInvoice", DbType = "Bit NOT NULL")]
        public bool IsNewInvoice
        {
            get
            {
                return this._IsNewInvoice;
            }
            set
            {
                if ((this._IsNewInvoice != value))
                {
                    this._IsNewInvoice = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsInvoiceCreated", DbType = "Bit NOT NULL")]
        public bool IsInvoiceCreated
        {
            get
            {
                return this._IsInvoiceCreated;
            }
            set
            {
                if ((this._IsInvoiceCreated != value))
                {
                    this._IsInvoiceCreated = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsManualFileUpload", DbType = "Bit NOT NULL")]
        public bool IsManualFileUpload
        {
            get
            {
                return this._IsManualFileUpload;
            }
            set
            {
                if ((this._IsManualFileUpload != value))
                {
                    this._IsManualFileUpload = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsFileTypeUploaded", DbType = "Bit NOT NULL")]
        public bool IsFileTypeUploaded
        {
            get
            {
                return this._IsFileTypeUploaded;
            }
            set
            {
                if ((this._IsFileTypeUploaded != value))
                {
                    this._IsFileTypeUploaded = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsComparedToCRM", DbType = "Bit NOT NULL")]
        public bool IsComparedToCRM
        {
            get
            {
                return this._IsComparedToCRM;
            }
            set
            {
                if ((this._IsComparedToCRM != value))
                {
                    this._IsComparedToCRM = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsViwedChanged", DbType = "Bit NOT NULL")]
        public bool IsViwedChanged
        {
            get
            {
                return this._IsViwedChanged;
            }
            set
            {
                if ((this._IsViwedChanged != value))
                {
                    this._IsViwedChanged = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsApprovalMailSent", DbType = "Bit NOT NULL")]
        public bool IsApprovalMailSent
        {
            get
            {
                return this._IsApprovalMailSent;
            }
            set
            {
                if ((this._IsApprovalMailSent != value))
                {
                    this._IsApprovalMailSent = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsApprovedChange", DbType = "Bit NOT NULL")]
        public bool IsApprovedChange
        {
            get
            {
                return this._IsApprovedChange;
            }
            set
            {
                if ((this._IsApprovedChange != value))
                {
                    this._IsApprovedChange = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsProcessed", DbType = "Bit NOT NULL")]
        public bool IsProcessed
        {
            get
            {
                return this._IsProcessed;
            }
            set
            {
                if ((this._IsProcessed != value))
                {
                    this._IsProcessed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_isPreBillingCompleted", DbType = "Bit NOT NULL")]
        public bool isPreBillingCompleted
        {
            get
            {
                return this._isPreBillingCompleted;
            }
            set
            {
                if ((this._isPreBillingCompleted != value))
                {
                    this._isPreBillingCompleted = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsApprovedByMail", DbType = "Bit NOT NULL")]
        public bool IsApprovedByMail
        {
            get
            {
                return this._IsApprovedByMail;
            }
            set
            {
                if ((this._IsApprovedByMail != value))
                {
                    this._IsApprovedByMail = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsSyncedCRM", DbType = "Bit NOT NULL")]
        public bool IsSyncedCRM
        {
            get
            {
                return this._IsSyncedCRM;
            }
            set
            {
                if ((this._IsSyncedCRM != value))
                {
                    this._IsSyncedCRM = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsImportCRM", DbType = "Bit NOT NULL")]
        public bool IsImportCRM
        {
            get
            {
                return this._IsImportCRM;
            }
            set
            {
                if ((this._IsImportCRM != value))
                {
                    this._IsImportCRM = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsProcessInvoice", DbType = "Bit NOT NULL")]
        public bool IsProcessInvoice
        {
            get
            {
                return this._IsProcessInvoice;
            }
            set
            {
                if ((this._IsProcessInvoice != value))
                {
                    this._IsProcessInvoice = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsExportInvoice", DbType = "Bit NOT NULL")]
        public bool IsExportInvoice
        {
            get
            {
                return this._IsExportInvoice;
            }
            set
            {
                if ((this._IsExportInvoice != value))
                {
                    this._IsExportInvoice = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastAction", DbType = "VarChar(30)")]
        public string LastAction
        {
            get
            {
                return this._LastAction;
            }
            set
            {
                if ((this._LastAction != value))
                {
                    this._LastAction = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_isPreBillingOverriden", DbType = "Bit")]
        public System.Nullable<bool> isPreBillingOverriden
        {
            get
            {
                return this._isPreBillingOverriden;
            }
            set
            {
                if ((this._isPreBillingOverriden != value))
                {
                    this._isPreBillingOverriden = value;
                }
            }
        }
    }
    public partial class get_AllFileTypesByInvoiceTypeIdResult
    {

        private int _iFileTypeId;

        private string _sFileType;

        public get_AllFileTypesByInvoiceTypeIdResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iFileTypeId", DbType = "Int NOT NULL")]
        public int iFileTypeId
        {
            get
            {
                return this._iFileTypeId;
            }
            set
            {
                if ((this._iFileTypeId != value))
                {
                    this._iFileTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFileType", DbType = "VarChar(75) NOT NULL", CanBeNull = false)]
        public string sFileType
        {
            get
            {
                return this._sFileType;
            }
            set
            {
                if ((this._sFileType != value))
                {
                    this._sFileType = value;
                }
            }
        }
    }

    public partial class get_ValidateEffectiveDateResult
    {

        private string _Response;

        public get_ValidateEffectiveDateResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Response", DbType = "VarChar(7) NOT NULL", CanBeNull = false)]
        public string Response
        {
            get
            {
                return this._Response;
            }
            set
            {
                if ((this._Response != value))
                {
                    this._Response = value;
                }
            }
        }
    }

    public partial class get_AccountEffectiveDate_StatusResult
    {

        private int _Response;

        public get_AccountEffectiveDate_StatusResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Response", DbType = "Int NOT NULL")]
        public int Response
        {
            get
            {
                return this._Response;
            }
            set
            {
                if ((this._Response != value))
                {
                    this._Response = value;
                }
            }
        }
    }

    public partial class get_InvoiceTypesbyAssociatedUserSOOResult
    {

        private int _iInvoiceTypeId;

        private string _sInvoiceTypeName;

        private string _sPrefix;

        private string _sBAN;

        private string _sVendorName;

        private string _sImportCurrencyDefault;

        private string _sExportCurrencyDefault;

        private string _sDefaultFTP;

        private string _sFTPUsername;

        private string _sFTPPassword;

        private System.Nullable<bool> _bIsAutoPreBill;

        private System.Nullable<int> _iDaysBeforeBillCycle;

        private System.Nullable<bool> _bIsAutoPostBill;

        private System.Nullable<int> _iDaysAfterBillCycle;

        private System.Nullable<int> _iOutputFileFormat;

        private System.Nullable<int> _isSOO;

        private string _sEmailAddress;

        private bool _bEDI;

        public get_InvoiceTypesbyAssociatedUserSOOResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceTypeName", DbType = "VarChar(100)")]
        public string sInvoiceTypeName
        {
            get
            {
                return this._sInvoiceTypeName;
            }
            set
            {
                if ((this._sInvoiceTypeName != value))
                {
                    this._sInvoiceTypeName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPrefix", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sPrefix
        {
            get
            {
                return this._sPrefix;
            }
            set
            {
                if ((this._sPrefix != value))
                {
                    this._sPrefix = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBAN", DbType = "VarChar(50)")]
        public string sBAN
        {
            get
            {
                return this._sBAN;
            }
            set
            {
                if ((this._sBAN != value))
                {
                    this._sBAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sVendorName", DbType = "VarChar(50)")]
        public string sVendorName
        {
            get
            {
                return this._sVendorName;
            }
            set
            {
                if ((this._sVendorName != value))
                {
                    this._sVendorName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sImportCurrencyDefault", DbType = "VarChar(3)")]
        public string sImportCurrencyDefault
        {
            get
            {
                return this._sImportCurrencyDefault;
            }
            set
            {
                if ((this._sImportCurrencyDefault != value))
                {
                    this._sImportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportCurrencyDefault", DbType = "VarChar(3)")]
        public string sExportCurrencyDefault
        {
            get
            {
                return this._sExportCurrencyDefault;
            }
            set
            {
                if ((this._sExportCurrencyDefault != value))
                {
                    this._sExportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDefaultFTP", DbType = "VarChar(100)")]
        public string sDefaultFTP
        {
            get
            {
                return this._sDefaultFTP;
            }
            set
            {
                if ((this._sDefaultFTP != value))
                {
                    this._sDefaultFTP = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPUsername", DbType = "VarChar(100)")]
        public string sFTPUsername
        {
            get
            {
                return this._sFTPUsername;
            }
            set
            {
                if ((this._sFTPUsername != value))
                {
                    this._sFTPUsername = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPPassword", DbType = "VarChar(256)")]
        public string sFTPPassword
        {
            get
            {
                return this._sFTPPassword;
            }
            set
            {
                if ((this._sFTPPassword != value))
                {
                    this._sFTPPassword = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPreBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPreBill
        {
            get
            {
                return this._bIsAutoPreBill;
            }
            set
            {
                if ((this._bIsAutoPreBill != value))
                {
                    this._bIsAutoPreBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysBeforeBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysBeforeBillCycle
        {
            get
            {
                return this._iDaysBeforeBillCycle;
            }
            set
            {
                if ((this._iDaysBeforeBillCycle != value))
                {
                    this._iDaysBeforeBillCycle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPostBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPostBill
        {
            get
            {
                return this._bIsAutoPostBill;
            }
            set
            {
                if ((this._bIsAutoPostBill != value))
                {
                    this._bIsAutoPostBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysAfterBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysAfterBillCycle
        {
            get
            {
                return this._iDaysAfterBillCycle;
            }
            set
            {
                if ((this._iDaysAfterBillCycle != value))
                {
                    this._iDaysAfterBillCycle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iOutputFileFormat", DbType = "Int")]
        public System.Nullable<int> iOutputFileFormat
        {
            get
            {
                return this._iOutputFileFormat;
            }
            set
            {
                if ((this._iOutputFileFormat != value))
                {
                    this._iOutputFileFormat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_isSOO", DbType = "Int")]
        public System.Nullable<int> isSOO
        {
            get
            {
                return this._isSOO;
            }
            set
            {
                if ((this._isSOO != value))
                {
                    this._isSOO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sEmailAddress", DbType = "VarChar(100)")]
        public string sEmailAddress
        {
            get
            {
                return this._sEmailAddress;
            }
            set
            {
                if ((this._sEmailAddress != value))
                {
                    this._sEmailAddress = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bEDI", DbType = "Bit NOT NULL")]
        public bool bEDI
        {
            get
            {
                return this._bEDI;
            }
            set
            {
                if ((this._bEDI != value))
                {
                    this._bEDI = value;
                }
            }
        }
    }

    public partial class get_InvoiceTypesbyAssociatedUserResult
    {

        private int _iInvoiceTypeId;

        private string _sInvoiceTypeName;

        private string _sPrefix;

        private string _sBAN;

        private string _sVendorName;

        private string _sImportCurrencyDefault;

        private string _sExportCurrencyDefault;

        private string _sDefaultFTP;

        private string _sFTPUsername;

        private string _sFTPPassword;

        private System.Nullable<bool> _bIsAutoPreBill;

        private System.Nullable<int> _iDaysBeforeBillCycle;

        private System.Nullable<bool> _bIsAutoPostBill;

        private System.Nullable<int> _iDaysAfterBillCycle;

        private System.Nullable<int> _iOutputFileFormat;

        private System.Nullable<int> _isSOO;

        private string _sEmailAddress;

        private bool _bEDI;

        public get_InvoiceTypesbyAssociatedUserResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceTypeName", DbType = "VarChar(100)")]
        public string sInvoiceTypeName
        {
            get
            {
                return this._sInvoiceTypeName;
            }
            set
            {
                if ((this._sInvoiceTypeName != value))
                {
                    this._sInvoiceTypeName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPrefix", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sPrefix
        {
            get
            {
                return this._sPrefix;
            }
            set
            {
                if ((this._sPrefix != value))
                {
                    this._sPrefix = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBAN", DbType = "VarChar(50)")]
        public string sBAN
        {
            get
            {
                return this._sBAN;
            }
            set
            {
                if ((this._sBAN != value))
                {
                    this._sBAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sVendorName", DbType = "VarChar(50)")]
        public string sVendorName
        {
            get
            {
                return this._sVendorName;
            }
            set
            {
                if ((this._sVendorName != value))
                {
                    this._sVendorName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sImportCurrencyDefault", DbType = "VarChar(3)")]
        public string sImportCurrencyDefault
        {
            get
            {
                return this._sImportCurrencyDefault;
            }
            set
            {
                if ((this._sImportCurrencyDefault != value))
                {
                    this._sImportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportCurrencyDefault", DbType = "VarChar(3)")]
        public string sExportCurrencyDefault
        {
            get
            {
                return this._sExportCurrencyDefault;
            }
            set
            {
                if ((this._sExportCurrencyDefault != value))
                {
                    this._sExportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDefaultFTP", DbType = "VarChar(100)")]
        public string sDefaultFTP
        {
            get
            {
                return this._sDefaultFTP;
            }
            set
            {
                if ((this._sDefaultFTP != value))
                {
                    this._sDefaultFTP = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPUsername", DbType = "VarChar(100)")]
        public string sFTPUsername
        {
            get
            {
                return this._sFTPUsername;
            }
            set
            {
                if ((this._sFTPUsername != value))
                {
                    this._sFTPUsername = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPPassword", DbType = "VarChar(256)")]
        public string sFTPPassword
        {
            get
            {
                return this._sFTPPassword;
            }
            set
            {
                if ((this._sFTPPassword != value))
                {
                    this._sFTPPassword = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPreBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPreBill
        {
            get
            {
                return this._bIsAutoPreBill;
            }
            set
            {
                if ((this._bIsAutoPreBill != value))
                {
                    this._bIsAutoPreBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysBeforeBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysBeforeBillCycle
        {
            get
            {
                return this._iDaysBeforeBillCycle;
            }
            set
            {
                if ((this._iDaysBeforeBillCycle != value))
                {
                    this._iDaysBeforeBillCycle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPostBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPostBill
        {
            get
            {
                return this._bIsAutoPostBill;
            }
            set
            {
                if ((this._bIsAutoPostBill != value))
                {
                    this._bIsAutoPostBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysAfterBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysAfterBillCycle
        {
            get
            {
                return this._iDaysAfterBillCycle;
            }
            set
            {
                if ((this._iDaysAfterBillCycle != value))
                {
                    this._iDaysAfterBillCycle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iOutputFileFormat", DbType = "Int")]
        public System.Nullable<int> iOutputFileFormat
        {
            get
            {
                return this._iOutputFileFormat;
            }
            set
            {
                if ((this._iOutputFileFormat != value))
                {
                    this._iOutputFileFormat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_isSOO", DbType = "Int")]
        public System.Nullable<int> isSOO
        {
            get
            {
                return this._isSOO;
            }
            set
            {
                if ((this._isSOO != value))
                {
                    this._isSOO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sEmailAddress", DbType = "VarChar(100)")]
        public string sEmailAddress
        {
            get
            {
                return this._sEmailAddress;
            }
            set
            {
                if ((this._sEmailAddress != value))
                {
                    this._sEmailAddress = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bEDI", DbType = "Bit NOT NULL")]
        public bool bEDI
        {
            get
            {
                return this._bEDI;
            }
            set
            {
                if ((this._bEDI != value))
                {
                    this._bEDI = value;
                }
            }
        }
    }

    public partial class get_EDIReportDataResult
    {

        private long _iAccountNumber;

        private string _sFTPUsername;

        private string _sFTPPassword;

        private string _sDefaultFTP;

        private string _sEmailAddress;

        private bool _bEDI;

        public get_EDIReportDataResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iAccountNumber", DbType = "bigInt NOT NULL")]
        public long iAccountNumber
        {
            get
            {
                return this._iAccountNumber;
            }
            set
            {
                if ((this._iAccountNumber != value))
                {
                    this._iAccountNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPUsername", DbType = "VarChar(100)")]
        public string sFTPUsername
        {
            get
            {
                return this._sFTPUsername;
            }
            set
            {
                if ((this._sFTPUsername != value))
                {
                    this._sFTPUsername = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPPassword", DbType = "VarChar(256)")]
        public string sFTPPassword
        {
            get
            {
                return this._sFTPPassword;
            }
            set
            {
                if ((this._sFTPPassword != value))
                {
                    this._sFTPPassword = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDefaultFTP", DbType = "VarChar(100)")]
        public string sDefaultFTP
        {
            get
            {
                return this._sDefaultFTP;
            }
            set
            {
                if ((this._sDefaultFTP != value))
                {
                    this._sDefaultFTP = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sEmailAddress", DbType = "VarChar(100)")]
        public string sEmailAddress
        {
            get
            {
                return this._sEmailAddress;
            }
            set
            {
                if ((this._sEmailAddress != value))
                {
                    this._sEmailAddress = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bEDI", DbType = "Bit NOT NULL")]
        public bool bEDI
        {
            get
            {
                return this._bEDI;
            }
            set
            {
                if ((this._bEDI != value))
                {
                    this._bEDI = value;
                }
            }
        }
    }

    public partial class get_InvoiceTypesbyAssociatedUser_SOOReportResult
    {

        private int _iInvoiceTypeId;

        private string _sInvoiceTypeName;

        private string _sPrefix;

        private string _sBAN;

        private string _sVendorName;

        private string _sImportCurrencyDefault;

        private string _sExportCurrencyDefault;

        private string _sDefaultFTP;

        private string _sFTPUsername;

        private string _sFTPPassword;

        private System.Nullable<bool> _bIsAutoPreBill;

        private System.Nullable<int> _iDaysBeforeBillCycle;

        private System.Nullable<bool> _bIsAutoPostBill;

        private System.Nullable<int> _iDaysAfterBillCycle;

        private System.Nullable<int> _isSOO;

        private string _sEmailAddress;

        private bool _bEDI;

        public get_InvoiceTypesbyAssociatedUser_SOOReportResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iInvoiceTypeId", DbType = "Int NOT NULL")]
        public int iInvoiceTypeId
        {
            get
            {
                return this._iInvoiceTypeId;
            }
            set
            {
                if ((this._iInvoiceTypeId != value))
                {
                    this._iInvoiceTypeId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sInvoiceTypeName", DbType = "VarChar(100)")]
        public string sInvoiceTypeName
        {
            get
            {
                return this._sInvoiceTypeName;
            }
            set
            {
                if ((this._sInvoiceTypeName != value))
                {
                    this._sInvoiceTypeName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sPrefix", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string sPrefix
        {
            get
            {
                return this._sPrefix;
            }
            set
            {
                if ((this._sPrefix != value))
                {
                    this._sPrefix = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sBAN", DbType = "VarChar(50)")]
        public string sBAN
        {
            get
            {
                return this._sBAN;
            }
            set
            {
                if ((this._sBAN != value))
                {
                    this._sBAN = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sVendorName", DbType = "VarChar(50)")]
        public string sVendorName
        {
            get
            {
                return this._sVendorName;
            }
            set
            {
                if ((this._sVendorName != value))
                {
                    this._sVendorName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sImportCurrencyDefault", DbType = "VarChar(3)")]
        public string sImportCurrencyDefault
        {
            get
            {
                return this._sImportCurrencyDefault;
            }
            set
            {
                if ((this._sImportCurrencyDefault != value))
                {
                    this._sImportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sExportCurrencyDefault", DbType = "VarChar(3)")]
        public string sExportCurrencyDefault
        {
            get
            {
                return this._sExportCurrencyDefault;
            }
            set
            {
                if ((this._sExportCurrencyDefault != value))
                {
                    this._sExportCurrencyDefault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sDefaultFTP", DbType = "VarChar(100)")]
        public string sDefaultFTP
        {
            get
            {
                return this._sDefaultFTP;
            }
            set
            {
                if ((this._sDefaultFTP != value))
                {
                    this._sDefaultFTP = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPUsername", DbType = "VarChar(100)")]
        public string sFTPUsername
        {
            get
            {
                return this._sFTPUsername;
            }
            set
            {
                if ((this._sFTPUsername != value))
                {
                    this._sFTPUsername = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sFTPPassword", DbType = "VarChar(256)")]
        public string sFTPPassword
        {
            get
            {
                return this._sFTPPassword;
            }
            set
            {
                if ((this._sFTPPassword != value))
                {
                    this._sFTPPassword = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPreBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPreBill
        {
            get
            {
                return this._bIsAutoPreBill;
            }
            set
            {
                if ((this._bIsAutoPreBill != value))
                {
                    this._bIsAutoPreBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysBeforeBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysBeforeBillCycle
        {
            get
            {
                return this._iDaysBeforeBillCycle;
            }
            set
            {
                if ((this._iDaysBeforeBillCycle != value))
                {
                    this._iDaysBeforeBillCycle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bIsAutoPostBill", DbType = "Bit")]
        public System.Nullable<bool> bIsAutoPostBill
        {
            get
            {
                return this._bIsAutoPostBill;
            }
            set
            {
                if ((this._bIsAutoPostBill != value))
                {
                    this._bIsAutoPostBill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_iDaysAfterBillCycle", DbType = "Int")]
        public System.Nullable<int> iDaysAfterBillCycle
        {
            get
            {
                return this._iDaysAfterBillCycle;
            }
            set
            {
                if ((this._iDaysAfterBillCycle != value))
                {
                    this._iDaysAfterBillCycle = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_isSOO", DbType = "Int")]
        public System.Nullable<int> isSOO
        {
            get
            {
                return this._isSOO;
            }
            set
            {
                if ((this._isSOO != value))
                {
                    this._isSOO = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sEmailAddress", DbType = "VarChar(100)")]
        public string sEmailAddress
        {
            get
            {
                return this._sEmailAddress;
            }
            set
            {
                if ((this._sEmailAddress != value))
                {
                    this._sEmailAddress = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bEDI", DbType = "Bit NOT NULL")]
        public bool bEDI
        {
            get
            {
                return this._bEDI;
            }
            set
            {
                if ((this._bEDI != value))
                {
                    this._bEDI = value;
                }
            }
        }

    }
}
