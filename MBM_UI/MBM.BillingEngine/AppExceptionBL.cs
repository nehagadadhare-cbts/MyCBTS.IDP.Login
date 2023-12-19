using MBM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.Entities;

namespace MBM.BillingEngine
{
    public class AppExceptionBL
    {

        private FileTypesDAL _fileTypesDAL;
        private readonly Logger _logger;
        private string ConnectionString { get; set; }

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
        public AppExceptionBL(string connectionString)
		{
			ConnectionString = connectionString;
            _fileTypesDAL = new FileTypesDAL(connectionString);
            _logger = new Logger(connectionString);
		}

        /// <summary>
        /// Inserts Application Exception in log table
        /// </summary>
        /// <param name="appException"></param>
        /// <returns></returns>
        public int InsertApplicationException(AppException appException)
        {
            int result = 0;
            try
            {
                result = _fileTypesDAL.InsertApplicationException(appException);
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
