using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;


namespace MBM.DataAccess
{
    public class ExportExcelToSFTP
    {
        public static void SaveFilesToSFTP
            (
            string Defaultftp,
            string SFTPUserName,
            string SFTPPassword,
            string RT1ServerPath,
            string RT1FileName
            )
        {
            try
            {
                string cleanedUrl = Defaultftp.Substring("sftp://".Length);
                if (cleanedUrl[cleanedUrl.Length - 1] == '/')
                {
                    cleanedUrl = cleanedUrl.Substring(0, cleanedUrl.Length - 1);
                }
                int lastSlashIndex = cleanedUrl.LastIndexOf('/');
                string host = string.Empty;
                if (lastSlashIndex >= 0)
                {
                    // Remove the part after the last '/'
                    host = cleanedUrl.Substring(0, lastSlashIndex);
                }
                Defaultftp = Defaultftp.Replace("sftp://pubftp.itnet.systems", "");

                // Ensure that the path starts with a '/'
                if (!Defaultftp.StartsWith("/"))
                {
                    Defaultftp = "/" + Defaultftp;
                }

                using (var client = new SftpClient(host, SFTPUserName, SFTPPassword))
                {
                    client.Connect();

                    if (client.IsConnected)
                    {
                        using (var fileStream = new FileStream(RT1ServerPath, FileMode.Open))
                        {
                            client.UploadFile(fileStream, Path.Combine(Defaultftp, Path.GetFileName(RT1FileName)));
                        }

                        client.Disconnect();
                    }
                    else
                    {
                        Console.WriteLine("Unable to connect to the SFTP server");
                        Console.ReadLine();
                    }
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

        }
    }
}