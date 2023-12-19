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
    public class UserDAL
    {
        #region Setup

		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
		public UserDAL(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}
		#endregion

        /// <summary>
        /// Get a full list of all configuration settings
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>list of configuration entities found</returns>
        public Users GetUserDetails(string strLoginUser)
        {
            Users entities = new Users();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_UserDetailsResult> results = db.get_UserDetails(strLoginUser.Trim());

                    foreach (get_UserDetailsResult r in results)
                    {
                        Users entity = new Users()
                        {
                            UserId = r.sUserId,
                            UserFirstName = r.sFirstName,
                            UserLastName = r.sLastName,
                            IsActive = (bool)r.bIsActive,
                            UserRole = (UserRoleType)r.iUserRoleId,
                            UserRoleId = r.iUserRoleId,                            
                        };

                        if (entity != null)
                        {
                            entities = entity;                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }

        public List<Users> GetUserDetailsByInvoiceId(int intInvoiceId)
        {
            List<Users> entities = new List<Users>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_AssociatedUsersByInvoiceTypeResult> results = db.get_AssociatedUsersByInvoiceType(intInvoiceId);

                    foreach (get_AssociatedUsersByInvoiceTypeResult r in results)
                    {
                        Users entity = new Users()
                        {
                            UserId = r.sUserId,
                            UserFirstName = r.sFirstName,
                            UserLastName = r.sLastName,                            
                            UserRole = (UserRoleType)r.iUserRoleId,
                            IsActive = (bool)r.bIsActive,
                            UserRoleId = r.iUserRoleId,
                            AssociatedUserId = r.iAssociatedUsersId,
                            
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }

        /// <summary>
        /// Insert a user association into the UserAssociation table
        /// </summary>
        
        public int InsertAssociatedUser(int intInvoiceId, string strUserId, string strInsertedBy)
        {
            int intReturn;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    //ISingleResult<get_UserDetailsResult> results = db.get_UserDetails(strLoginUser.Trim());
                    intReturn = db.insert_AssociatedUser(intInvoiceId, strUserId, strInsertedBy);                    
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }
            return intReturn;
        }

        public void DeleteAssociatedUser(int intAssociatedUserId)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.delete_AssociatedUser(intAssociatedUserId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }
           
        }

        public List<Users> SearchUser(string strUser)
        {
            List<Users> entities = new List<Users>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<search_UserResult> results = db.search_User(strUser.Trim());

                    foreach (search_UserResult r in results)
                    {
                        Users entity = new Users()
                        {
                            UserFirstName = r.sFirstName,
                            UserLastName = r.sLastName,
                            IsActive = (bool)r.bIsActive,
                            UserRole = (UserRoleType)r.iUserRoleId,
                            UserRoleId = r.iUserRoleId,
                            UserId = r.sUserId,
                        };

                        if (entity != null)
                        {
                            entities.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return entities;
        }
    }
}
