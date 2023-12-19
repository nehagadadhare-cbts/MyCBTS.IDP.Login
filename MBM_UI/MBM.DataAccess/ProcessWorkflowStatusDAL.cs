using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.Entities;

namespace MBM.DataAccess
{
    public class ProcessWorkflowStatusDAL
    {
        #region Setup
		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
        public ProcessWorkflowStatusDAL(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}
		#endregion

        /// <summary>
        /// Gets Process Workflow Status for Invoice Number.
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public ProcessWorkFlowStatus GetProcessWorkFlowStatusByInvoiceNumber(string InvoiceNumber)
        {
            ProcessWorkFlowStatus objPwfStatus = new ProcessWorkFlowStatus();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    //ISingleResult<get_AssoicateAccountsByInvoiceTypeIdResult> results = db.get_AssoicateAccountsByInvoiceTypeId(InvoiceTypeId);
                    ISingleResult<get_ProcessWorkFlowStatusResult> results = db.get_ProcessWorkFlowStatus(InvoiceNumber);

                    foreach (get_ProcessWorkFlowStatusResult r in results)
                    {
                        objPwfStatus.InvoiceNumber = r.sInvoiceNumber;
                        objPwfStatus.CompareToCRM = r.sCompareToCRM;
                        objPwfStatus.ViewChange = r.sViewChange;
                        objPwfStatus.ApproveChange = r.sApproveChange;
                        objPwfStatus.SyncCRM = r.sSyncCRM;
                        objPwfStatus.ImportCRMData = r.sImportCRMData;
                        objPwfStatus.ProcessInvoice = r.sProcessInvoice;
                        objPwfStatus.ExportInvoiceFile = r.sExportInvoiceFile;                        
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return objPwfStatus;
        }

        /// <summary>
        /// Updates the Process Workflow Status for Invoice Number
        /// </summary>
        /// <param name="crmAccount"></param>
        /// <param name="Username"></param>
        /// <returns></returns>
        public int UpdateProcessWorkFlowStatusByInvoiceNumber(ProcessWorkFlowStatus processWorkflowStatus)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.update_ProcessWorkFlowStatus(processWorkflowStatus.InvoiceNumber, processWorkflowStatus.CompareToCRM, processWorkflowStatus.ViewChange,
                                    processWorkflowStatus.ApproveChange, processWorkflowStatus.SyncCRM, processWorkflowStatus.ImportCRMData,
                                    processWorkflowStatus.ProcessInvoice, processWorkflowStatus.ExportInvoiceFile);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Update Process WorkFlow Status [{0}]", result));
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

        /// <summary>
        /// Updates the Process Workflow Status for Automation Invoices
        /// </summary>
        /// <param name="crmAccount"></param>
        /// <param name="Username"></param>
        /// <returns></returns>
        public int UpdateAutomationWorkFlowStatusByInvoiceId(int invoiceId,bool value,string statusOf)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.update_MBMAutomateStatus(invoiceId,value,statusOf);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Update Automation Process WorkFlow Status [{0}]", result));
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

        /// <summary>
        /// Updates the Process Workflow Sync Status for Invoice Number
        /// </summary>
        /// <param name="crmAccount"></param>
        /// <param name="Username"></param>
        /// <returns></returns>
        public int ResetPreBillingActivityStatus(string strInvoiceNumber)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.mbm_ResetPreBillingActivityStatus(strInvoiceNumber);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to reset Process WorkFlow Status [{0}]", result));
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
