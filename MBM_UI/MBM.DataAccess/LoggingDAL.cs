using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.SqlClient;
using MBM.Entities;

namespace MBM.DataAccess
{
	public class LoggingDAL
	{
		#region Setup

		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
		public LoggingDAL(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}

		#endregion

		/// <summary>
		/// Fetch the Process Log
		/// </summary>
		/// <param name="maxRowsToReturn">maximum number of rows to return</param>
		/// <returns>entities found</returns>
        //public List<ProcessLogItemEntity> GetProcessLogItemList(
        //    int? maxRowsToReturn
        //    )
        //{
        //    List<ProcessLogItemEntity> entities = new List<ProcessLogItemEntity>();

        //    try
        //    {
        //        using (MBMDbDataContext db = new MBMDbDataContext(_connection))
        //        {
        //            ISingleResult<ProcessLog_GetResult> results = db.ProcessLog_Get(null, maxRowsToReturn);

        //            foreach (ProcessLog_GetResult r in results)
        //            {
        //                ProcessLogItemEntity entity = new ProcessLogItemEntity()
        //                {
        //                    Comment = r.Comment,
        //                    CreatedBy = r.CreatedBy,
        //                    ID = r.ID,
        //                    InvoiceType = (InvoiceTypeEnum)r.InvoiceType,
        //                    InvoiceNumber = r.InvoiceNumber,
        //                    ProcessDateTime = r.ProcessDateTime,
        //                    ProcessName = r.ProcessName,
        //                    ProcessResult = r.ProcessResult,
        //                };

        //                if (entity != null)
        //                {
        //                    entities.Add(entity);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Exception(ex, -773324);
        //        throw;
        //    }

        //    return entities;
        //}

		/// <summary>
		/// Fetch the Exception Log
		/// </summary>
		/// <param name="maxRowsToReturn">maximum number of rows to return</param>
		/// <returns>entities found</returns>
        //public List<ExceptionLogEntity> GetExceptionLogList(
        //    int? maxRowsToReturn
        //    )
        //{
        //    List<ExceptionLogEntity> entities = new List<ExceptionLogEntity>();

        //    try
        //    {
        //        using (MBMDbDataContext db = new MBMDbDataContext(_connection))
        //        {
        //            ISingleResult<ExceptionLog_GetResult> results = db.ExceptionLog_Get(maxRowsToReturn);

        //            foreach (ExceptionLog_GetResult r in results)
        //            {
        //                ExceptionLogEntity entity = new ExceptionLogEntity()
        //                {
        //                    ID = r.ID,
        //                    InvoiceID = r.InvoiceID,
        //                    Comment = r.Comment,
        //                    CreatedBy = r.CreatedBy,
        //                    InvoiceNumber = r.InvoiceNumber,
        //                    CreatedDate = r.CreatedDate,
        //                    InvoiceType = (InvoiceTypeEnum?)r.InvoiceType,
        //                    StackTrace = r.StackTrace,
        //                    Type = r.Type,
        //                };

        //                if (entity != null)
        //                {
        //                    entities.Add(entity);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Exception(ex, -773324);
        //        throw;
        //    }

        //    return entities;
		//}

		/// <summary>
		/// Get a list of the application log
		/// </summary>
		/// <param name="maxRowsToReturn">maximum number of rows to return</param>
		/// <returns>list of log entries</returns>
        //public List<ApplicationLogEntity> GetApplicationLogList(
        //    int? maxRowsToReturn
        //    )
        //{
        //    List<ApplicationLogEntity> entities = new List<ApplicationLogEntity>();

        //    try
        //    {
        //        using (MBMDbDataContext db = new MBMDbDataContext(_connection))
        //        {
        //            ISingleResult<ApplicationLog_GetResult> results = db.ApplicationLog_Get(maxRowsToReturn);

        //            foreach (ApplicationLog_GetResult r in results)
        //            {
        //                ApplicationLogEntity entity = new ApplicationLogEntity()
        //                {
        //                    InvoiceID = r.InvoiceID,
        //                    InvoiceType = (InvoiceTypeEnum?)r.InvoiceType,
        //                    CodeLocation = r.CodeLocation,
        //                    Comment = r.Comment,
        //                    InvoiceNumber = r.InvoiceNumber,
        //                    LogType = (ApplicationLogType)r.LogType,
        //                    Message = r.Message,
        //                    Source = r.Source,
        //                    StackTrace = r.StackTrace,
        //                    TargetSite = r.TargetSite,
        //                    UserName = r.UserName,
        //                    ExceptionDateTime = r.ExceptionDateTime,
        //                };

        //                if (entity != null)
        //                {
        //                    entities.Add(entity);
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Exception(ex, -729340);
        //        throw;
        //    }

        //    return entities;
        //}

	}
}
