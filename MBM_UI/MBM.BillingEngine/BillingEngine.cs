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
	public static class Constants
	{
		public static string UNKNOWN { get { return "none"; } }
		public static string COMPLETED { get { return "Completed"; } }
		public static string FAILED { get { return "Failed"; } }
		public static string PROCESSING { get { return "In-Process"; } }
	}

	public class BillingEngine
	{
		#region Setup

		private DataFactory _dal;
		private Logger _logger;

		private string ConnectionString { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
		public BillingEngine(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}
       

		#endregion


		#region Singleton Factory

		private RecordTypes _recordTypes;
		public RecordTypes RecordTypesBL
		{
			get
			{
				if (_recordTypes == null)
				{
					_recordTypes = new MBM.BillingEngine.RecordTypes(ConnectionString);
				}
				return _recordTypes;
			}
		}

		private FileImport _fileImport;
		public FileImport FileImportBL
		{
			get
			{
				if (_fileImport == null)
				{
					_fileImport = new MBM.BillingEngine.FileImport(ConnectionString);
				}
				return _fileImport;
			}
		}

		private Logs _logs;
		public Logs LogsBL
		{
			get
			{
				if (_logs == null)
				{
					_logs = new MBM.BillingEngine.Logs(ConnectionString);
				}
				return _logs;
			}
		}

		private InvoiceBL _invoice;
		public InvoiceBL InvoiceBL
		{
			get
			{
				if (_invoice == null)
				{
					_invoice = new MBM.BillingEngine.InvoiceBL(ConnectionString);
				}
				return _invoice;
			}
		}

        private UserBL _user;
        public UserBL UserBL
        {
            get
            {
                if (_user == null)
                {
                    _user = new MBM.BillingEngine.UserBL(ConnectionString);
                }
                return _user;
            }
        }

        private UploadedFileBL _uploadedFile;
        public UploadedFileBL UploadedFileBL
        {
            get
            {
                if (_uploadedFile == null)
                {
                    _uploadedFile = new MBM.BillingEngine.UploadedFileBL(ConnectionString);
                }
                return _uploadedFile;
            }
        }

        private FileTypeBL _fileType;
        public FileTypeBL FileTypeBL
        {
            get
            {
                if (_fileType == null)
                {
                    _fileType = new MBM.BillingEngine.FileTypeBL(ConnectionString);
                }
                return _fileType;
            }
        }

        private CRMAccountBL _crmAccount;
        public CRMAccountBL CRMAccountBL
        {
            get
            {
                if (_crmAccount == null)
                {
                    _crmAccount = new MBM.BillingEngine.CRMAccountBL(ConnectionString);
                }
                return _crmAccount;
            }
        }      

		private Configurations _config;
		public Configurations Configurations
		{
			get
			{
				if (_config == null)
				{
					_config = new MBM.BillingEngine.Configurations(ConnectionString);
				}
				return _config;
			}
		}

        private ProcessWorkflowStatusBL _processWorkFlowStatus;
        public ProcessWorkflowStatusBL ProcessWorkFlowStatusBL
        {
            get
            {
                if (_processWorkFlowStatus == null)
                {
                    _processWorkFlowStatus = new MBM.BillingEngine.ProcessWorkflowStatusBL(ConnectionString);
                }
                return _processWorkFlowStatus;
            }
        }

        private ProfileBL _profile;
        public ProfileBL ProfileBL
        {
            get
            {
                if(_profile == null)
                {
                    _profile = new MBM.BillingEngine.ProfileBL(ConnectionString);
                }
                return _profile;
            }
        }
		#endregion

	}
}
