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
using System.Threading;
using System.Threading.Tasks;

namespace MBM.BillingEngine
{
	public class RecordTypes
	{
		private DataFactory _dal;
		private Logger _logger;
		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
		public RecordTypes(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

		/// <summary>
		/// Process the RecordType for a selected invoice
		/// </summary>
		/// <param name="recordTypeId">Record Type to process</param>
		/// <param name="invoiceID">Invoice to Process</param>
		/// <param name="callerLogin">Login of the user requesting the change</param>
		/// <param name="currencyID">final currency</param>
		/// <returns>Output status</returns>
		public string ProcessRecordType(
			int recordTypeId,
			int invoiceID,
			string callerLogin,
			int currencyID
			)
		{
			string returnResult = Constants.UNKNOWN;

			switch (recordTypeId)
			{
				case 1:
					returnResult = ProcessRecordType1(invoiceID, callerLogin, currencyID);
					break;
				case 2:
					returnResult = ProcessRecordType2(invoiceID, callerLogin, currencyID);
					break;
				case 3:
					returnResult = ProcessRecordType3(invoiceID, callerLogin, currencyID);
					break;
				case 4:
					returnResult = ProcessRecordType4(invoiceID, callerLogin, currencyID);
					break;
				case 5:
					returnResult = ProcessRecordType5(invoiceID, callerLogin, currencyID);
					break;
			}

			return returnResult;
		}

		/// <summary>
		/// Process RecordType 1 (Long Distance)
		/// </summary>
		/// <param name="invoiceID">invoice to processes</param>
		/// <param name="callerLogin">login information</param>
		/// <param name="currencyID">currency identifier of the resulting information</param>
		/// <returns>Output status</returns>
		public string ProcessRecordType1(
			int invoiceID,
			string callerLogin,
			int currencyID
			)
		{
			DateTime date = DateTime.Now;
			var processLogItemEntity = new ProcessLogItemEntity();

			try
			{
                _dal.RecordTypes.ProcessRecordType1(invoiceID, callerLogin, currencyID);
                //Task t = Task.Factory.StartNew(() =>
                //    _dal.RecordTypes.ProcessRecordType1(invoiceID, callerLogin, currencyID)
                //);
                //t.Wait();
				processLogItemEntity.ProcessResult = Constants.COMPLETED;
			}
			catch (Exception e)
			{
				ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
				appLogEntity.LogType = ApplicationLogType.SystemRaised;
				appLogEntity.InvoiceID = invoiceID;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.Exception = e;
				appLogEntity.CodeLocation = "ProcessRecordType1()";
				appLogEntity.Comment = "Failed to ProcessRecordType1 for Invoice# " + invoiceID;

				_logger.LogExceptionToDatabase(appLogEntity);

				processLogItemEntity.ProcessResult = Constants.FAILED;
			}

			processLogItemEntity.InvoiceID = invoiceID;
			processLogItemEntity.ProcessDateTime = DateTime.Now;
			processLogItemEntity.ProcessName = "Step 2: ProcessRecordType1()";
			processLogItemEntity.CreatedBy = callerLogin;
			_logger.InsertProcessLogItem(processLogItemEntity);

			return processLogItemEntity.ProcessResult;
		}

		/// <summary>
		/// Process RecordType2 (Monthly Reoccuring, Non-reoccuring) for a single invoice
		/// </summary>
		/// <param name="invoiceID">invoice number</param>
		/// <param name="callerLogin">login of caller</param>
		/// <param name="currencyID">currency identifier of final result</param>
		/// <returns>Output status</returns>
		public string ProcessRecordType2(
			int invoiceID,
			string callerLogin,
			int currencyID
			)
		{
			DateTime date = DateTime.Now;
			var processLogItemEntity = new ProcessLogItemEntity();

			try
			{
                _dal.RecordTypes.ProcessRecordType2(invoiceID, callerLogin, currencyID);
                //Task t = Task.Factory.StartNew(() =>
                //    _dal.RecordTypes.ProcessRecordType2(invoiceID, callerLogin, currencyID)
                //);
                //t.Wait();

				processLogItemEntity.ProcessResult = Constants.COMPLETED;
			}
			catch (Exception e)
			{
				ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
				appLogEntity.LogType = ApplicationLogType.SystemRaised;
				appLogEntity.InvoiceID = invoiceID;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.Exception = e;
				appLogEntity.CodeLocation = "ProcessRecordType2()";
				appLogEntity.Comment = "Failed to ProcessRecordType2 for Invoice# " + invoiceID;

				_logger.LogExceptionToDatabase(appLogEntity);

				processLogItemEntity.ProcessResult = Constants.FAILED;
			}

			processLogItemEntity.InvoiceID = invoiceID;
			processLogItemEntity.ProcessDateTime = DateTime.Now;
			processLogItemEntity.ProcessName = "Step 2: ProcessRecordType2()";
			processLogItemEntity.CreatedBy = callerLogin;
			_logger.InsertProcessLogItem(processLogItemEntity);

			return processLogItemEntity.ProcessResult;
		}

		/// <summary>
		/// Process RecordType3 (Summary) for a single invoice
		/// </summary>
		/// <param name="invoiceID">invoice number</param>
		/// <param name="callerLogin">login of caller</param>
		/// <param name="currencyID">currency identifier of output</param>
		/// <returns>Output status</returns>
		public string ProcessRecordType3(
			int invoiceID,
			string callerLogin,
			int currencyID
			)
		{
			DateTime date = DateTime.Now;
			var processLogItemEntity = new ProcessLogItemEntity();

			try
			{
				_dal.RecordTypes.ProcessRecordType3(invoiceID, callerLogin, currencyID);
				processLogItemEntity.ProcessResult = Constants.COMPLETED;
			}
			catch (Exception e)
			{
				ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
				appLogEntity.LogType = ApplicationLogType.SystemRaised;
				appLogEntity.InvoiceID = invoiceID;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.Exception = e;
				appLogEntity.CodeLocation = "ProcessRecordType3()";
				appLogEntity.Comment = "Failed to ProcessRecordType3 for Invoice# " + invoiceID;

				_logger.LogExceptionToDatabase(appLogEntity);

				processLogItemEntity.ProcessResult = Constants.FAILED;
			}

			processLogItemEntity.InvoiceID = invoiceID;
			processLogItemEntity.ProcessDateTime = DateTime.Now;
			processLogItemEntity.ProcessName = "Step 2: ProcessRecordType3()";
			processLogItemEntity.CreatedBy = callerLogin;
			_logger.InsertProcessLogItem(processLogItemEntity);

			return processLogItemEntity.ProcessResult;
		}

        /// <summary>
        /// Process RT1Summary for a single invoice
        /// </summary>
        /// <param name="invoiceID">invoice number</param>
        /// <param name="callerLogin">login of caller</param>
        /// <returns>Output status</returns>
        public string ProcessRT1Summary(
            int invoiceID,
            string callerLogin
            )
        {
            DateTime date = DateTime.Now;
            var processLogItemEntity = new ProcessLogItemEntity();

            try
            {
                _dal.RecordTypes.ProcessRT1Summary(invoiceID, callerLogin);
                processLogItemEntity.ProcessResult = Constants.COMPLETED;
            }
            catch (Exception e)
            {
                ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
                appLogEntity.LogType = ApplicationLogType.SystemRaised;
                appLogEntity.InvoiceID = invoiceID;
                appLogEntity.ExceptionDateTime = DateTime.Now;
                appLogEntity.Exception = e;
                appLogEntity.CodeLocation = "ProcessRT1Summary()";
                appLogEntity.Comment = "Failed to ProcessRT1Summary for Invoice# " + invoiceID;

                _logger.LogExceptionToDatabase(appLogEntity);

                processLogItemEntity.ProcessResult = Constants.FAILED;
            }

            processLogItemEntity.InvoiceID = invoiceID;
            processLogItemEntity.ProcessDateTime = DateTime.Now;
            processLogItemEntity.ProcessName = "Step 2: ProcessRT1Summary()";
            processLogItemEntity.CreatedBy = callerLogin;
            _logger.InsertProcessLogItem(processLogItemEntity);

            return processLogItemEntity.ProcessResult;
        }

        /// <summary>
        /// Process RT2Summary for a single invoice
        /// </summary>
        /// <param name="invoiceID">invoice number</param>
        /// <param name="callerLogin">login of caller</param>
        /// <returns>Output status</returns>
        public string ProcessRT2Summary(
            int invoiceID,
            string callerLogin
            )
        {
            DateTime date = DateTime.Now;
            var processLogItemEntity = new ProcessLogItemEntity();

            try
            {
                _dal.RecordTypes.ProcessRT2Summary(invoiceID, callerLogin);
                processLogItemEntity.ProcessResult = Constants.COMPLETED;
            }
            catch (Exception e)
            {
                ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
                appLogEntity.LogType = ApplicationLogType.SystemRaised;
                appLogEntity.InvoiceID = invoiceID;
                appLogEntity.ExceptionDateTime = DateTime.Now;
                appLogEntity.Exception = e;
                appLogEntity.CodeLocation = "ProcessRT2Summary()";
                appLogEntity.Comment = "Failed to ProcessRT2Summary for Invoice# " + invoiceID;

                _logger.LogExceptionToDatabase(appLogEntity);

                processLogItemEntity.ProcessResult = Constants.FAILED;
            }

            processLogItemEntity.InvoiceID = invoiceID;
            processLogItemEntity.ProcessDateTime = DateTime.Now;
            processLogItemEntity.ProcessName = "Step 2: ProcessRT2Summary()";
            processLogItemEntity.CreatedBy = callerLogin;
            _logger.InsertProcessLogItem(processLogItemEntity);

            return processLogItemEntity.ProcessResult;
        }

		/// <summary>
		/// Process RecordType4 (Taxes) for the invoice number
		/// </summary>
		/// <param name="invoiceID">invoice number</param>
		/// <param name="callerLogin">login of person calling this function</param>
		/// <param name="currencyID">currency identifier of the output</param>
		/// <returns>Output status</returns>
		public string ProcessRecordType4(
			int invoiceID,
			string callerLogin,
			int currencyID
			)
		{
			DateTime date = DateTime.Now;
			var processLogItemEntity = new ProcessLogItemEntity();

			try
			{
                 _dal.RecordTypes.ProcessRecordType4(invoiceID, callerLogin, currencyID);
                //Task t = Task.Factory.StartNew(() =>
                //    _dal.RecordTypes.ProcessRecordType4(invoiceID, callerLogin, currencyID));
                //t.Wait();
				processLogItemEntity.ProcessResult = Constants.COMPLETED;
			}
			catch (Exception e)
			{
				ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
				appLogEntity.LogType = ApplicationLogType.SystemRaised;
				appLogEntity.InvoiceID = invoiceID;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.Exception = e;
				appLogEntity.CodeLocation = "ProcessRecordType4()";
				appLogEntity.Comment = "Failed to ProcessRecordType4 for Invoice# " + invoiceID;

				_logger.LogExceptionToDatabase(appLogEntity);

				processLogItemEntity.ProcessResult = Constants.FAILED;
			}

			processLogItemEntity.InvoiceID = invoiceID;
			processLogItemEntity.ProcessDateTime = DateTime.Now;
			processLogItemEntity.ProcessName = "Step 2: ProcessRecordType4()";
			processLogItemEntity.CreatedBy = callerLogin;
			_logger.InsertProcessLogItem(processLogItemEntity);

			return processLogItemEntity.ProcessResult;
		}

        /// <summary>
        /// Process RT4Summary for a single invoice
        /// </summary>
        /// <param name="invoiceID">invoice number</param>
        /// <param name="callerLogin">login of caller</param>
        /// <returns>Output status</returns>
        public string ProcessRT4Summary(
            int invoiceID,
            string callerLogin
            )
        {
            DateTime date = DateTime.Now;
            var processLogItemEntity = new ProcessLogItemEntity();

            try
            {
                _dal.RecordTypes.ProcessRT4Summary(invoiceID, callerLogin);
                processLogItemEntity.ProcessResult = Constants.COMPLETED;
            }
            catch (Exception e)
            {
                ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
                appLogEntity.LogType = ApplicationLogType.SystemRaised;
                appLogEntity.InvoiceID = invoiceID;
                appLogEntity.ExceptionDateTime = DateTime.Now;
                appLogEntity.Exception = e;
                appLogEntity.CodeLocation = "ProcessRT4Summary()";
                appLogEntity.Comment = "Failed to ProcessRT4Summary for Invoice# " + invoiceID;

                _logger.LogExceptionToDatabase(appLogEntity);

                processLogItemEntity.ProcessResult = Constants.FAILED;
            }

            processLogItemEntity.InvoiceID = invoiceID;
            processLogItemEntity.ProcessDateTime = DateTime.Now;
            processLogItemEntity.ProcessName = "Step 2: ProcessRT4Summary()";
            processLogItemEntity.CreatedBy = callerLogin;
            _logger.InsertProcessLogItem(processLogItemEntity);

            return processLogItemEntity.ProcessResult;
        }

		/// <summary>
		/// Process RecordType5 (Totals) for a single invoice
		/// </summary>
		/// <param name="invoiceID">invoice to process</param>
		/// <param name="callerLogin">login of person calling this function</param>
		/// <param name="currencyID">currency identifier of output</param>
		/// <returns>Output status</returns>
		public string ProcessRecordType5(
			int invoiceID,
			string callerLogin,
			int currencyID
			)
		{
			DateTime date = DateTime.Now;
			var processLogItemEntity = new ProcessLogItemEntity();

			try
			{
				_dal.RecordTypes.ProcessRecordType5(invoiceID, callerLogin, currencyID);
				processLogItemEntity.ProcessResult = Constants.COMPLETED;
			}
			catch (Exception e)
			{
				ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
				appLogEntity.LogType = ApplicationLogType.SystemRaised;
				appLogEntity.InvoiceID = invoiceID;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.Exception = e;
				appLogEntity.CodeLocation = "ProcessRecordType5()";
				appLogEntity.Comment = "Failed to ProcessRecordType5 for Invoice# " + invoiceID;

				_logger.LogExceptionToDatabase(appLogEntity);

				processLogItemEntity.ProcessResult = Constants.FAILED;
			}

			processLogItemEntity.InvoiceID = invoiceID;
			processLogItemEntity.ProcessDateTime = DateTime.Now;
			processLogItemEntity.ProcessName = "Step 2: ProcessRecordType5()";
			processLogItemEntity.CreatedBy = callerLogin;
			_logger.InsertProcessLogItem(processLogItemEntity);

			return processLogItemEntity.ProcessResult;
		}


		/// <summary>
		/// Validate the RecordType for a selected invoice
		/// </summary>
		/// <param name="recordTypeId">Record Type to process</param>
		/// <param name="invoiceID">Invoice to Process</param>
		/// <returns>Output status</returns>
		public bool ValidateRecordType(
			int recordTypeId,
			int invoiceID
			)
		{
			bool returnResult = false;

			switch (recordTypeId)
			{
				case 1:
					returnResult = ValidateRecordType1(invoiceID);
					break;
				case 2:
					returnResult = ValidateRecordType2(invoiceID);
					break;
				case 3:
					returnResult = ValidateRecordType3(invoiceID);
					break;
				case 4:
					returnResult = ValidateRecordType4(invoiceID);
					break;
				case 5:
					returnResult = ValidateRecordType5(invoiceID);
					break;
				default:
					returnResult = false;
					break;
			}

			return returnResult;
		}

		/// <summary>
		/// Validate the entries of Record Type 1
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <returns>true on validation, false on any error</returns>
		public bool ValidateRecordType1(
			int invoiceID
			)
		{
			try
			{
				List<RecordValidator> entities = new List<RecordValidator>();
                _dal.RecordTypes.ValidateRecordType1(invoiceID, out entities);
                //Task t = Task.Factory.StartNew(() =>
                //    _dal.RecordTypes.ValidateRecordType1(invoiceID, out entities)
                //);
                //t.Wait();

				foreach (RecordValidator r in entities)
				{
					if (r.Validated == false
						|| !r.RecordTypeSum.Equals(r.RecordType3Sum)
						|| !r.RecordTypeSum.Equals(r.RecordType5Sum)
						)
					{
						return false;
					}
				}
			}
			catch (Exception e)
			{
				ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
				appLogEntity.LogType = ApplicationLogType.SystemRaised;
				appLogEntity.InvoiceID = invoiceID;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.Exception = e;
				appLogEntity.CodeLocation = "ValidateRecordType1()";
				appLogEntity.Comment = "Failed to ValidateRecordType1 for Invoice# " + invoiceID;

				_logger.LogExceptionToDatabase(appLogEntity);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Validate the entries of Record Type 2
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <returns>true on validation, false on any error</returns>
		public bool ValidateRecordType2(
			int invoiceID
			)
		{
			try
			{
				List<RecordValidator> entities = new List<RecordValidator>();
                _dal.RecordTypes.ValidateRecordType2(invoiceID, out entities);
                //Task t = Task.Factory.StartNew(() =>
                //    _dal.RecordTypes.ValidateRecordType2(invoiceID, out entities)
                //);
                //t.Wait();

				foreach (RecordValidator r in entities)
				{
					if (r.Validated == false
						|| !r.RecordTypeSum.Equals(r.RecordType3Sum)
						|| !r.RecordTypeSum.Equals(r.RecordType5Sum)
						)
					{
						return false;
					}
				}
			}
			catch (Exception e)
			{
				ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
				appLogEntity.LogType = ApplicationLogType.SystemRaised;
				appLogEntity.InvoiceID = invoiceID;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.Exception = e;
				appLogEntity.CodeLocation = "ValidateRecordType2()";
				appLogEntity.Comment = "Failed to ValidateRecordType2 for Invoice# " + invoiceID;

				_logger.LogExceptionToDatabase(appLogEntity);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Validate the entries of Record Type 3
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <returns>true on validation, false on any error</returns>
		public bool ValidateRecordType3(
			int invoiceID
			)
		{
			//return false;
            return true;
		}

		/// <summary>
		/// Validate the entries of Record Type 4
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <returns>true on validation, false on any error</returns>
		public bool ValidateRecordType4(
			int invoiceID
			)
		{
			try
			{
				List<RecordValidator> entities = new List<RecordValidator>();
                _dal.RecordTypes.ValidateRecordType4(invoiceID, out entities);
                //Task t = Task.Factory.StartNew(() =>
                //    _dal.RecordTypes.ValidateRecordType4(invoiceID, out entities)
                //);
                //t.Wait();

				foreach (RecordValidator r in entities)
				{
					if (r.Validated == false
						|| !r.RecordTypeSum.Equals(r.RecordType3Sum)
						|| !r.RecordTypeSum.Equals(r.RecordType5Sum)
						)
					{
						return false;
					}
				}
			}
			catch (Exception e)
			{
				ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
				appLogEntity.LogType = ApplicationLogType.SystemRaised;
				appLogEntity.InvoiceID = invoiceID;
				appLogEntity.ExceptionDateTime = DateTime.Now;
				appLogEntity.Exception = e;
				appLogEntity.CodeLocation = "ValidateRecordType4()";
				appLogEntity.Comment = "Failed to ValidateRecordType4 for Invoice# " + invoiceID;

				_logger.LogExceptionToDatabase(appLogEntity);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Validate the entries of Record Type 5
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <returns>true on validation, false on any error</returns>
		public bool ValidateRecordType5(
			int invoiceID
			)
		{
			//return false;
            return true;
		}

        /// <summary>
        /// Process Long distance record types into the data storage
        /// <param name="CustomerNumner">Customer number</param>
        /// <param name="callerLogin">login of caller</param>
        /// <returns>Output status</returns> 
        /// </summary>
        public void ProcessLDRecordTypes(
            string CustomerNumner,
            string callerLogin
            )
        {
            DateTime date = DateTime.Now;
            var processLogItemEntity = new ProcessLogItemEntity();
            try
            {
                _dal.RecordTypes.ProcessLDRecordTypes(CustomerNumner, callerLogin);
                processLogItemEntity.ProcessResult = Constants.COMPLETED;
            }
            catch (Exception e)
            {
                ApplicationLogEntity appLogEntity = new ApplicationLogEntity();
                appLogEntity.LogType = ApplicationLogType.SystemRaised;
                appLogEntity.InvoiceID =Convert.ToInt32(CustomerNumner);
                appLogEntity.ExceptionDateTime = DateTime.Now;
                appLogEntity.Exception = e;
                appLogEntity.CodeLocation = "ProcessLDRecordTypes()";
                appLogEntity.Comment = "Failed to ProcessLDRecordTypes for CustomerNumner# " + CustomerNumner;

                _logger.LogExceptionToDatabase(appLogEntity);

                processLogItemEntity.ProcessResult = Constants.FAILED;
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
        public int UpdateInvoiceRecordTypes(int invoiceid, string recordType1status, string recordType2status, string recordType3status, string recordType4status, string recordType5status, string callerLogin)
        {
            int result = 0;
            try
            {
                _dal.RecordTypes.UpdateInvoiceRecordTypes(invoiceid, recordType1status, recordType2status, recordType3status, recordType4status, recordType5status, callerLogin);
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
