using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBM.Entities
{
	public class Invoice
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public Invoice()
		{
			UploadedFiles = new UploadedFileCollection();
		}

		public int ID { set; get; }
		public int TypeOfBill { get; set; }
		public string InvoiceNumber { set; get; }
		public string Status { set; get; }
		public string LastAction { set; get; }

		public int BillingMonth { set; get; }
		public int BillingYear { set; get; }

		public int DefaultImportCurrencyID { set; get; }

		public string RecordType1Status { set; get; }
		public DateTime? RecordType1DateTime { set; get; }
		public string RecordType2Status { set; get; }
		public DateTime? RecordType2DateTime { set; get; }
		public string RecordType3Status { set; get; }
		public DateTime? RecordType3DateTime { set; get; }
		public string RecordType4Status { set; get; }
		public DateTime? RecordType4DateTime { set; get; }
		public string RecordType5Status { set; get; }
		public DateTime? RecordType5DateTime { set; get; }

		public string BillingFileExportStatus { get; set; }
		public DateTime? BillingFileExportDateTime { get; set; }
		public string BillingFileExportPath { get; set; }
		public int ExportCurrencyID { get; set; }

		public DateTime? CreatedDate { set; get; }
		public string CreatedBy { set; get; }
		public DateTime? LastUpdatedDate { set; get; }
		public string LastUpdatedBy { set; get; }

		public UploadedFileCollection UploadedFiles { get; set; }

        public string CustomerNum { set; get; }

        public string TypeofMode { set; get; }

        public DateTime? BillStartDate { get; set; }
        public DateTime? BillEndDate { get; set; }

        public string BillingSystem { get; set; } //SERO_1582

        public string invoiceId { get; set; } //SERO_1582
	}

    /// <summary>
    /// Start:17-12-2014
    ///  Returns list of customer numbers
    /// </summary>
    public class Customers
    {
        public string CustomerNum { set; get; }
    }

}
