using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.DataAccess;
using MBM.Entities;
using MBM.Library;
using System.Data;

namespace MBM.BillingEngine
{
    public class FileTypeBL
    {
        private DataFactory _dal;
        private Logger _logger;
        private string ConnectionString { get; set; }

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="connectionString">database connection string</param>
        public FileTypeBL(string connectionString)
		{
			ConnectionString = connectionString;
			_dal = new DataFactory(connectionString);
			_logger = new Logger(connectionString);
		}

        /// <summary>
        /// Gets the Associated FileTypes for InvoiceType
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<FileTypes> GetAssoicateFileTypesByInvoiceTypeId(int InvoiceTypeId)
        {
            try
            {
                return _dal.FileTypes.GetAssoicateFileTypesByInvoiceTypeId(InvoiceTypeId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
        }


        /// <summary>
        /// Gets FileTypes Not associated for InvoiceType
        /// </summary>
        /// <param name="InvoiceTypeId"></param>
        /// <returns></returns>
        public List<FileTypes> GetAllFileTypesByInvoiceTypeId(int InvoiceTypeId)
        {
            try
            {
                return _dal.FileTypes.GetAllFileTypesByInvoiceTypeId(InvoiceTypeId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -769257);
                throw;
            }
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
                _dal.FileTypes.DeleteAssociatedFileType(AssociatedFileId);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
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
                result = _dal.FileTypes.InsertAssocoiatedFileType(fileType);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
            return result;

        }


        /// <summary>
        /// uploaded DB data againt invoice number
        /// </summary>
        /// <param name="uploadFile"></param>
        public void UpsertMBMdata_ByInvoiceIdInvoiceType(MBMUploadFile uploadFile)
        {
            try
            {
                _dal.FileTypes.UpsertMBMdata_ByInvoiceIdInvoiceType(uploadFile);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
        }

        /// <summary>
        /// uploaded DB data againt invoice number
        /// </summary>
        /// <param name="uploadFile"></param>
        public void UploadMBMdata_ByInvoiceIdInvoiceType_CSG(MBMUploadFile uploadFile)
        {
            try
            {
                _dal.FileTypes.UploadMBMdata_ByInvoiceIdInvoiceType_CSG(uploadFile);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
        }


        /// <summary>
        /// uploaded file againt invoice number
        /// </summary>
        /// <param name="uploadFile"></param>
        public void UpsertMBMDataFile_ByInvoiceIdInvoiceType(MBMUploadFile uploadFile)
        {
            try
            {
                _dal.FileTypes.UpsertMBMDataFile_ByInvoiceIdInvoiceType(uploadFile);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
        }


        /// <summary>
        /// Upload bulk data from upload file 
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="connectionString"></param>
        public void BulkCopy_MBM_DataToSqlServer(DataTable sourceTable, string connectionString)
        {
            try
            {
                SQLBulkLayer.BulkCopy_MBM_DataToSqlServer(sourceTable,connectionString);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
        }

        /// <summary>
        /// Upload bulk data from upload file 
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="connectionString"></param>
        public void BulkCopy_MBM_ManualChargeDataToSqlServer(DataTable sourceTable, string connectionString)
        {
            try
            {
                SQLBulkLayer.BulkCopy_ManualCharges_DataToSqlServer(sourceTable, connectionString);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, -915034);
                throw;
            }
        }
    }
}
