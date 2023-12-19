using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.DataAccess;
using MBM.Entities;
using MBM.Library;

namespace MBM.BillingEngine
{
    public class ProcessWorkflowStatusBL
    {
        private DataFactory _dal;
        private Logger _logger;
        private string ConnectionString { get; set; }

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
        public ProcessWorkflowStatusBL(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

        /// <summary>
        /// Gets Process Workflow Status for Invoice Number.
        /// </summary>
        /// <param name="strInvoiceNumber"></param>
        /// <returns></returns>
        public ProcessWorkFlowStatus GetProcessWorkFlowStatusByInvoiceNumber(string strInvoiceNumber)
        {
            try
            {
                //return _dal.CRMAccount.GetAssoicateAccountsByInvoiceTypeId(InvoiceTypeId);
                return _dal.ProcessWorkflowStatus.GetProcessWorkFlowStatusByInvoiceNumber(strInvoiceNumber);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        /// <summary>
        /// UpUpdates the Process Workflow Status for Invoice Number
        /// </summary>
        /// <param name="objProcessWorkflowStatus"></param>        
        /// <returns></returns>
        public int UpdateProcessWorkFlowStatusByInvoiceNumber(ProcessWorkFlowStatus objProcessWorkflowStatus)
        {
            int result = 0;
            try
            {
                result = _dal.ProcessWorkflowStatus.UpdateProcessWorkFlowStatusByInvoiceNumber(objProcessWorkflowStatus);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }

        
        /// <summary>
        /// Updates the Process Workflow Status for Automation
        /// </summary>
        /// <param name="objProcessWorkflowStatus"></param>        
        /// <returns></returns>
        public int UpdateAutomationWorkFlowStatusByInvoiceId(int invoiceId,bool value,string statusOf)
        {
            int result = 0;
            try
            {
                result = _dal.ProcessWorkflowStatus.UpdateAutomationWorkFlowStatusByInvoiceId(invoiceId,value,statusOf);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }

        /// <summary>
        /// UpUpdates the Process Workflow Status for Invoice Number
        /// </summary>
        /// <param name="objProcessWorkflowStatus"></param>        
        /// <returns></returns>
        public int ResetPreBillingActivityStatus(string strInvoiceNumber)
        {
            int result = 0;
            try
            {
                result = _dal.ProcessWorkflowStatus.ResetPreBillingActivityStatus(strInvoiceNumber);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;
        }
    }
}
