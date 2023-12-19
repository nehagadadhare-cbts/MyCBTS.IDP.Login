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

namespace MBM.BillingEngine
{
	public class Logs
	{
		private DataFactory _dal;
		private Logger _logger;
		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
		public Logs(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

		/// <summary>
		/// Get list of process logs in the data store
		/// </summary>
		/// <param name="maxRowsToFetch">maximum numbers of rows to fetch or null to fetch all</param>
		/// <returns>logs</returns>
        //public List<ProcessLogItemEntity> GetProcessLogs(int? maxRowsToFetch)
        //{
        //    List<ProcessLogItemEntity> entities = new List<ProcessLogItemEntity>();

        //    try
        //    {
        //        entities = _dal.Logging.GetProcessLogItemList(maxRowsToFetch);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Exception(ex, -310557);
        //        throw;
        //    }

        //    return entities;
        //}

		/// <summary>
		/// Get list of exceptions in the data store
		/// </summary>
		/// <param name="maxRowsToFetch">maximum numbers of rows to fetch or null to fetch all</param>
		/// <returns>list of exceptions</returns>
        //public List<ApplicationLogEntity> GetApplicationLogs(int? maxRowsToFetch)
        //{
        //    List<ApplicationLogEntity> entities = new List<ApplicationLogEntity>();

        //    try
        //    {
        //        entities = _dal.Logging.GetApplicationLogList(maxRowsToFetch);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Exception(ex, -454980);
        //        throw;
        //    }

        //    return entities;
        //}

		/// <summary>
		/// Get list of exceptions in the data store
		/// </summary>
		/// <param name="maxRowsToFetch">maximum numbers of rows to fetch or null to fetch all</param>
		/// <returns>list of exceptions</returns>
        //public List<ExceptionLogEntity> GetExceptionLogs(int? maxRowsToFetch)
        //{
        //    List<ExceptionLogEntity> entities = new List<ExceptionLogEntity>();

        //    try
        //    {
        //        entities = _dal.Logging.GetExceptionLogList(maxRowsToFetch);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Exception(ex, -282157);
        //        throw;
        //    }

        //    return entities;
        //}

	}
}
