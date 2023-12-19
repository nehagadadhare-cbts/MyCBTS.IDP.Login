using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DataAccess
{
    public class ExportExcelToFTP
    {
        public static void SaveFilesToFTP(
           string FTPPath,
           string FTPUserName,
           string FTPPassword,
           string filePath,
           string fileName
           )
        {

            FtpWebRequest requestFileUpload = (FtpWebRequest)WebRequest.Create(FTPPath + fileName);
            requestFileUpload.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
            requestFileUpload.Method = WebRequestMethods.Ftp.UploadFile;

            FileInfo fileInfo = new FileInfo(filePath);
            FileStream fileStream = fileInfo.OpenRead();

            int bufferLength = 2048;
            byte[] buffer = new byte[bufferLength];

            Stream uploadStream = requestFileUpload.GetRequestStream();
            int contentLength = fileStream.Read(buffer, 0, bufferLength);

            while (contentLength != 0)
            {
                uploadStream.Write(buffer, 0, contentLength);
                contentLength = fileStream.Read(buffer, 0, bufferLength);
            }

            uploadStream.Close();
            fileStream.Close();

            requestFileUpload = null;
        }
    }
}
