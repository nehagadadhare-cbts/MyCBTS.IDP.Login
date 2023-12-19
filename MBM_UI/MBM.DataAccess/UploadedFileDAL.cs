using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.Entities;
using MBM.DataAccess;

namespace MBM.DataAccess
{
    public class UploadedFileDAL
    {
        #region Setup

		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
        public UploadedFileDAL(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}
		#endregion

        /// <summary>
        /// delete fileupload againt fileupload id
        /// </summary>
        /// <param name="intFileupload"></param>
        public void DeleteFileuploadbyFileID(int intFileupload)
        {
            //List<Users> entities = new List<Users>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.delete_FileuploadbyFileID(intFileupload);                    
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }            
        }
    }
}
