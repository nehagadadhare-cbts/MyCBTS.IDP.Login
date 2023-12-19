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
    public class UserBL
    {
        private DataFactory _dal;
        private Logger _logger;
        private string ConnectionString { get; set; }

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
        public UserBL(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

        public Users GetUserDetails(string strUserId)
        {
            try
            {
                return _dal.User.GetUserDetails(strUserId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        public List<Users> GetUserDetailsByInvoiceType(int intInvoiceId)
        {
            try
            {
                return _dal.User.GetUserDetailsByInvoiceId(intInvoiceId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        public void DeleteAssociatedUser(int intAssociatedUserId)
        {
            try
            {
                _dal.User.DeleteAssociatedUser(intAssociatedUserId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        public int InsertAssociatedUser(int intInvoiceId, string strUserId, string strInsertedBy)
        {
            int intReturn;
            try
            {
                intReturn = _dal.User.InsertAssociatedUser(intInvoiceId, strUserId, strInsertedBy);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
            return intReturn;
        }

        public List<Users> SearchUser(string strUser)
        {
            try
            {
                return _dal.User.SearchUser(strUser);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }

        public Users AuthenticateUser(string strLoginUser)
        {
            Users objUser = new Users();
            string strReturnValue = string.Empty;

            objUser = _dal.User.GetUserDetails(strLoginUser);                     

            return objUser;
        }
    }
}
