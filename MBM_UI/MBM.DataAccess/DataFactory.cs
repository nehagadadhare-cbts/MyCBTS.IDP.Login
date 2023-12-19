using MBM.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBM.DataAccess
{
	public class DataFactory
	{

		#region Setup

		private readonly string _connection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection string</param>
		public DataFactory(string dbConnection)
		{
			this._connection = dbConnection;
		}

		#endregion

		#region Members

		public ConfigurationsDAL Configurations
		{
			get { return new MBM.DataAccess.ConfigurationsDAL(_connection); }
		}

		public InvoiceDAL Invoice
		{
			get { return new MBM.DataAccess.InvoiceDAL(_connection); }
		}

		public LoggingDAL Logging
		{
			get { return new MBM.DataAccess.LoggingDAL(_connection); }
		}

		public RecordTypesDAL RecordTypes
		{
			get { return new MBM.DataAccess.RecordTypesDAL(_connection); }
		}

		public GELoyaltyCredit GELoyaltyCredit
		{
			get { return new MBM.DataAccess.GELoyaltyCredit(_connection); }
		}

        public UserDAL User
        {
            get { return new MBM.DataAccess.UserDAL(_connection); }
        }

        public UploadedFileDAL UploadedFile
        {
            get { return new MBM.DataAccess.UploadedFileDAL(_connection); }
        }

        public FileTypesDAL FileTypes
        {
            get { return new MBM.DataAccess.FileTypesDAL(_connection); }
        }

        public CRMAccountDAL CRMAccount
        {
            get { return new MBM.DataAccess.CRMAccountDAL(_connection); }
        }

        public CSGAccountDAL CSGAccount
        {
            get { return new MBM.DataAccess.CSGAccountDAL(_connection); }
        }

        public CUCDMDataDAL CUCDMData
        {
            get { return new MBM.DataAccess.CUCDMDataDAL(_connection); }
        }

        public ProcessWorkflowStatusDAL ProcessWorkflowStatus
        {
            get { return new MBM.DataAccess.ProcessWorkflowStatusDAL(_connection); }
        }

        public ProfileDAL ProfileDetails
        {
            get { return new MBM.DataAccess.ProfileDAL(_connection); }
        }
		#endregion

	}
}
