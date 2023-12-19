using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using Automate.DataAccess.Entities;
using Automate.DataAccess.edmx;

namespace AutomateMBM.Entities
{
    class Logging
    {
        #region Setup

        private readonly string _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbConnection">database connection</param>
        public Logging(string dbConnection)
        {
            this._connection = dbConnection;
        }

        public Logging()
        {

        }

        #endregion

        /// <summary>
        /// Log Exception into the data storage
        /// </summary>
        /// <param name="exceptionLogEntity">exception entity</param>
        public void LogExceptionToDatabase(ApplicationLogEntity exceptionLogEntity)
        {
            try
            {
                MethodBase method = new StackTrace().GetFrame(1).GetMethod();

                using (AutomateEntities db = new AutomateEntities())
                {
                    db.ApplicationLog_InsertItem(
                        exceptionLogEntity.InvoiceID,
                        exceptionLogEntity.UserName,
                        exceptionLogEntity.ExceptionDateTime,
                        exceptionLogEntity.CodeLocation,
                        exceptionLogEntity.Comment,
                        exceptionLogEntity.Message,
                        exceptionLogEntity.Source,
                        exceptionLogEntity.TargetSite,
                        exceptionLogEntity.StackTrace,
                        Convert.ToInt32(exceptionLogEntity.LogType)
                        );

                    db.ExceptionLog_InsertItem(
                        exceptionLogEntity.InvoiceID,
                        "Exception",
                        String.Format("{0}::{1} {2} [-772457]", method.ReflectedType.FullName, method.Name, exceptionLogEntity.CodeLocation),
                        exceptionLogEntity.StackTrace,
                        exceptionLogEntity.UserName
                        );
                }
            }
            catch (Exception ex)
            {
                Exception(exceptionLogEntity.InvoiceID, string.Empty, ex, -951565);
            }
        }

        /// <summary>
        /// Insert a single row into the process log
        /// </summary>
        /// <param name="processLogItemEntity">entity to insert</param>
        //public void InsertProcessLogItem(ProcessLogItemEntity processLogItemEntity)
        //{
        //    try
        //    {
        //        MethodBase method = new StackTrace().GetFrame(1).GetMethod();

        //        using (AutomateEntities db = new AutomateEntities())
        //        {
        //            db.ProcessLog_InsertItem(
        //                processLogItemEntity.InvoiceID,
        //                processLogItemEntity.ProcessName,
        //                processLogItemEntity.ProcessResult,
        //                processLogItemEntity.ProcessDateTime,
        //                processLogItemEntity.Comment,
        //                processLogItemEntity.CreatedBy
        //                );
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception(processLogItemEntity.InvoiceID, processLogItemEntity.CreatedBy, ex, -710520);
        //    }
        //}

        /// <summary>
        /// Insert a single row into the process log
        /// </summary>
        /// <param name="invoiceID">invoice identifier</param>
        /// <param name="processName">name of process</param>
        /// <param name="processResult">result or status of the comment</param>
        /// <param name="logdate">date it happened</param>
        /// <param name="comment">description</param>
        /// <param name="createdBy">login creating the entry</param>
        public void InsertProcessLogItem(int invoiceID, string processName, string processResult, DateTime logdate, string comment, string createdBy)
        {
            try
            {
                MethodBase method = new StackTrace().GetFrame(1).GetMethod();

                using (AutomateEntities db = new AutomateEntities())
                {
                    db.ProcessLog_InsertItem(
                        invoiceID,
                        processName,
                        processResult,
                        logdate,
                        comment,
                        createdBy
                        );
                }
            }
            catch (Exception ex)
            {
                Exception(invoiceID, createdBy, ex, -269176);
            }
        }

        /// <summary>
        /// Insert a single entry
        /// </summary>
        /// <param name="invoiceID">number of invoice</param>
        /// <param name="callerLogin">login originating the call</param>
        /// <param name="exception">exception to log</param>
        /// <param name="statusCode">error status code</param>
        public void Exception(int? invoiceID, string callerLogin, Exception exception, int statusCode)
        {
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();

            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    db.ExceptionLog_InsertItem(
                        invoiceID,
                        "Exception",
                        String.Format("{0}::{1} [{2}]", method.ReflectedType.FullName, method.Name, statusCode),
                        exception.StackTrace,
                        callerLogin
                        );
                }
            }
            catch
            {
                //Logging an exception in the logging needs another route - EventLog probably
            }
        }

        /// <summary>
        /// Insert a single entry
        /// </summary>
        /// <param name="callerLogin">login originating the call</param>
        /// <param name="exception">exception to log</param>
        /// <param name="statusCode">error status code</param>
        public void Exception(string callerLogin, Exception exception, int statusCode)
        {
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();

            try
            {
                using (AutomateEntities db = new AutomateEntities())
                {
                    db.ExceptionLog_InsertItem(
                        null,
                        "Exception",
                        String.Format("{0}::{1} [{2}]", method.ReflectedType.FullName, method.Name, statusCode),
                        exception.StackTrace,
                        callerLogin
                        );
                }
            }
            catch
            {
                //Logging an exception in the logging needs another route - EventLog probably
            }
        }

        /// <summary>
        /// Insert a single entry
        /// </summary>
        /// <param name="exception">exception to log</param>
        /// <param name="statusCode">error status code</param>
        /// <param name="callerLogin">login of the user who called this function</param>
        public void Exception(Exception exception, int statusCode, string callerLogin)
        {
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();

            try
            {
                Exception(callerLogin, exception, statusCode);
            }
            catch
            {
                //Logging an exception in the logging needs another route - EventLog probably
            }
        }

        /// <summary>
        /// Insert a single entry
        /// </summary>
        /// <param name="exception">exception to log</param>
        /// <param name="statusCode">error status code</param>
        public void Exception(Exception exception, int statusCode)
        {
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();

            try
            {
                Exception(exception, statusCode, string.Empty);
            }
            catch
            {
                //Logging an exception in the logging needs another route - EventLog probably
            }
        }
    }
}
