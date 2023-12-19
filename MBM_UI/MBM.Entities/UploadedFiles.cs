using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MBM.Entities
{
	/// <summary>
	/// Matches the FileUploadType table in the data store
	/// </summary>
	public enum UploadedFileType
	{


        GEInternationalVideoSnapshot = 1,
        OneTimeChargesFile = 2,
        BroadVox = 3,

        CBAD = 0
       //CBADTaxFile = 2,
       // BroadVox = 3,
       // OneTimeCharges = 4,
       // EC500 = 5,
       // TestAccounts = 6,
       // GEGCOMMultiLocationSummary = 7,
       // OneSourceLongDistance = 8,
       // OneSourceUsageSummary = 9,
       // OneSourceFlatRate = 10,

       //// CRQ CRQ700001314707
       ////Add new profiles for US and International Video billing
       // USVideoNewMonthly=11,
       // InternationalVideoNewMonthly=12,
       // InternationalVideoNewAnnual=13,

	};

   public class UploadedFile
	{
		public int InvoiceID { get; set; }
		public string InvoiceNumber { get; set; }
        public string FileType { get; set; }
		public string FilePath { get; set; }
		public string UploadedStatus { get; set; }
		public string LastUpdatedDate { get; set; }
		public string LastUpdatedBy { get; set; }
        public int UploadedFileId { get; set; }
        public string SnapshotId { get; set; }
	}


   public class ExportedFile
   {
       public int InvoiceID { get; set; }
       public string InvoiceNumber { get; set; }
       public string ExportedFileName { get; set; }
       public string ExportedFilePath { get; set; }
       public DateTime ExportedFileDate { get; set; }
       public string ExportedFileBy { get; set; }
   }

	/// <summary>
	/// Provides a list of uploaded files for invoices
	/// </summary>
	public class UploadedFileCollection : Collection<UploadedFile>
	{
		/// <summary>
		/// Return the found item matching the results. If no item current exists it will create
		/// a blank item
		/// </summary>
		/// <param name="invoiceID">invoice identifier</param>
		/// <param name="fileType">type of uploaded file (UploadedFileType enum)</param>
		/// <returns>file entry</returns>
		public UploadedFile Item(int invoiceID, string fileType)
		{
			UploadedFile fileFound = Items.FirstOrDefault<UploadedFile>(
				delegate(UploadedFile u)
				{
					return (u.FileType == fileType && u.InvoiceID == invoiceID);
				});

			if (fileFound == null)
			{
				fileFound = new UploadedFile()
				{
					InvoiceID = invoiceID,
					FileType = fileType,
				};
			}

			return fileFound;
		}

	}
}
