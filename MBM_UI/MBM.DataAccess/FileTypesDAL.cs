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
    public class FileTypesDAL
    {
        #region Setup

		private readonly Logger _logger;
		private readonly string _connection;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbConnection">database connection</param>
        public FileTypesDAL(string dbConnection)
		{
			this._connection = dbConnection;
			_logger = new Logger(dbConnection);
		}
		#endregion

        /// <summary>
        /// Gets the Associated FileTypes for InvoiceType
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<FileTypes> GetAssoicateFileTypesByInvoiceTypeId(int InvoiceTypeId)
        {
            List<FileTypes> lstFileTypes = new List<FileTypes>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_AssoicateFileTypesByInvoiceTypeIdResult> results = db.get_AssoicateFileTypesByInvoiceTypeId(InvoiceTypeId);

                    foreach (get_AssoicateFileTypesByInvoiceTypeIdResult r in results)
                    {
                        FileTypes lstlstFileType = new FileTypes()
                        {
                            FileTypeId = r.iFileTypeId,
                            FileType = r.sFileType,
                            AssociatedFileId = r.iAssociatedFileId
                        };

                        if (lstlstFileType != null)
                        {
                            lstFileTypes.Add(lstlstFileType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -524044);
                throw;
            }

            return lstFileTypes;
        }

        /// <summary>
        /// Gets FileTypes Not associated for InvoiceType
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<FileTypes> GetAllFileTypesByInvoiceTypeId(int InvoiceTypeId)
        {
            List<FileTypes> entities = new List<FileTypes>();

            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    ISingleResult<get_AllFileTypesByInvoiceTypeIdResult> results = db.get_AllFileTypesByInvoiceTypeId(InvoiceTypeId);

                    foreach (get_AllFileTypesByInvoiceTypeIdResult r in results)
                    {
                        FileTypes entity = new FileTypes()
                        {
                            FileTypeId = r.iFileTypeId,                            
                            FileType = r.sFileType,
                                                      
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
        /// Delete Associated FileType
       /// </summary>
       /// <param name="AssociatedFileId"></param>
       /// <returns></returns>
        public void DeleteAssociatedFileType(int AssociatedFileId)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                     db.delete_AssoicatedFileType(AssociatedFileId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        /// <summary>
        /// Associates the Filetype to Invoice Type
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public int InsertAssocoiatedFileType(FileTypes fileType)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.insert_AssocaitedFiles(fileType.InvoiceTypeId, fileType.FileTypeId, fileType.CreatedBy);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to Associate  FileType [{0}]", result));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return result;
        }

       /// <summary>
       /// uploaded DB data againt invoice number
       /// </summary>
       /// <param name="uploadedFile"></param>
        public void UpsertMBMdata_ByInvoiceIdInvoiceType(MBMUploadFile uploadedFile)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.insert_MBMdata_ByInvoiceIdInvoiceType(uploadedFile.InvoiceId, uploadedFile.InvoiceTypeId, uploadedFile.FileTypeId, uploadedFile.FileName, uploadedFile.UploadedBy, uploadedFile.UploadedFileId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        /// <summary>
        /// uploaded DB data againt invoice number
        /// </summary>
        /// <param name="uploadedFile"></param>
        public void UploadMBMdata_ByInvoiceIdInvoiceType_CSG(MBMUploadFile uploadedFile)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.insert_MBMdata_ByInvoiceIdInvoiceType_CSG(uploadedFile.InvoiceId, uploadedFile.InvoiceTypeId, uploadedFile.FileTypeId, uploadedFile.FileName, uploadedFile.UploadedBy, uploadedFile.UploadedFileId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        /// <summary>
        ///  uploaded file againt invoice number
        /// </summary>
        /// <param name="uploadedFile"></param>
        public void UpsertMBMDataFile_ByInvoiceIdInvoiceType(MBMUploadFile uploadedFile)
        {
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    db.insert_MBMdatabyFile_ByInvoiceIdInvoiceType(uploadedFile.InvoiceId, uploadedFile.InvoiceTypeId, uploadedFile.FileTypeId, uploadedFile.FileName, uploadedFile.UploadedBy, uploadedFile.UploadedFileId);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
        }

        /// <summary>
        /// Inert Exception details
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public int InsertApplicationException(AppException appException)
        {
            int result = 0;
            try
            {
                using (MBMDbDataContext db = new MBMDbDataContext(_connection))
                {
                    result = db.insert_ApplicationException(appException.MethodName, appException.ErrorMessage, appException.StackTrace, appException.LoggedInUser);

                    if (result < 0)
                    {
                        throw new Exception(String.Format("Failed to insert exception", result));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -722071);
                throw;
            }
            return result;
        }

    }
}
