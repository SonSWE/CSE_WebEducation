//using FluentFTP;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace CommonLib
//{
//    public class FTP_Interface
//    {
//        string c_FTPAddress = "";                       // địa chỉ FTP server ex: 192.168.2.30
//        string c_username = "";                         // username
//        string c_password = "";                         // password
//        bool c_UsePassive = false;                      // nếu thay đổi file trong thư mục gửi thì tự động đẩy file lên FTP
//        bool c_use_SSL = false;

//        public FTP_Interface(string FTPAddress, string username, string password, bool p_use_SSL = false)
//        {
//            c_FTPAddress = FTPAddress;
//            c_username = username;
//            c_password = password;
//            c_UsePassive = true;

//            c_use_SSL = p_use_SSL;

//            // kiểm tra xem đã có thư mục nhận trên FTP server chưa
//            // chưa có thì tạo thư mục đó
//            //Check_And_CreateFTPDirectory(c_FolderReceive_Remote);
//        }

//        /// <summary>
//        /// Tạo folder  
//        /// </summary>
//        /// <param name="p_FolderReceive_Remote"> ex /test/path/that/should/be/created</param>
//        /// <param name="p_isDeleteFolder"></param>
//        void Check_And_CreateFTPDirectory(string p_FolderReceive_Remote)
//        {
//            try
//            {
//                using (var conn = new FtpClient(c_FTPAddress, c_username, c_password))
//                {
//                    conn.Connect();
//                    conn.CreateDirectory(p_FolderReceive_Remote, true);
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.log.Error(ex.ToString());
//            }
//        }

//        /// <summary>
//        /// Download file from server ftp
//        /// </summary>
//        /// <param name="p_remote_path">ex /public_html/temp/README.md</param>
//        /// <param name="p_local_save">ex @"D:\Github\FluentFTP"</param>
//        /// <param name="p_error"></param>
//        /// <returns>Full file save to local</returns>
//        public string Download_File(string p_remote_path, string p_local_save)
//        {
//            try
//            {
//                using (var ftp = new FtpClient(c_FTPAddress, c_username, c_password))
//                {
//                    if (c_use_SSL)
//                    {
//                        ftp.EncryptionMode = FtpEncryptionMode.Explicit;
//                        //ftp.ValidateAnyCertificate = true;
//                        ftp.ValidateCertificate += new FtpSslValidation(OnValidateCertificate);
//                    }
//                    ftp.Connect();

//                    string[] _arr = p_remote_path.Split('/');
//                    string _file_name_download = _arr[_arr.Length - 1];
//                    string _full_file_save = Path.Combine(p_local_save, _file_name_download);

//                    // download a file and ensure the local directory is created
//                    //ftp.DownloadFile(@"D:\Github\FluentFTP\README.md", "/public_html/temp/README.md");

//                    // download a file and ensure the local directory is created, verify the file after download
//                    //ftp.DownloadFile(@"D:\Github\FluentFTP\README.md", "/public_html/temp/README.md", FtpLocalExists.Overwrite, FtpVerify.Retry);

//                    ftp.DownloadFile(_full_file_save, p_remote_path, FtpLocalExists.Overwrite, FtpVerify.Retry);
//                    return _full_file_save;
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.log.Error(ex.ToString());
//                return "";
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="p_Full_FileName_Send">ex @"D:\Github\FluentFTP\README.md"</param>
//        /// <param name="p_folder_remote_path">ex /public_html/temp/ </param>
//        /// <returns></returns>
//        public bool UploadOneFile(string p_Full_FileName_Send, string p_folder_remote_path = "")
//        {
//            try
//            {
//                FileInfo _FileInfo = new FileInfo(p_Full_FileName_Send);

//                // tạo thư mục tạm để copy file sang đó
//                // mục đích là sẽ upload cái file trong thư mục tạm chứ ko phải file gốc
//                // cứ kiểm tra cho chắc
//                string _folder_tem = System.IO.Path.Combine(_FileInfo.Directory.ToString(), "Temp");
//                Logger.log.Info("Folde temp save file before send ftp " + _folder_tem);
//                if (System.IO.Directory.Exists(_folder_tem) == false)
//                    System.IO.Directory.CreateDirectory(_folder_tem);

//                // copy thành 1 file mới đề phòng trường hợp đang ghi file thì mở ra
//                string _fileTem = System.IO.Path.Combine(_folder_tem, _FileInfo.Name);
//                _FileInfo.CopyTo(_fileTem, true);

//                // tạo FTP request  để Upload file
//                FileInfo _FileInfo_Temp = new FileInfo(_fileTem);
//                string _fullname = _FileInfo_Temp.Name;


//                string _file_remote_path = "";
//                _file_remote_path = p_folder_remote_path + _FileInfo_Temp.Name;

//                using (var ftp = new FtpClient(c_FTPAddress, c_username, c_password))
//                {
//                    if (c_use_SSL)
//                    {
//                        ftp.EncryptionMode = FtpEncryptionMode.Explicit;
//                        //ftp.ValidateAnyCertificate = true;
//                        ftp.ValidateCertificate += new FtpSslValidation(OnValidateCertificate);
//                    }
//                    ftp.Connect();

//                    ftp.UploadFile(p_Full_FileName_Send, _file_remote_path, FtpRemoteExists.Overwrite, true, FtpVerify.Retry);

//                    // upload a file to an existing FTP directory
//                    //ftp.UploadFile(@"D:\Github\FluentFTP\README.md", "/public_html/temp/README.md");

//                    // upload a file and ensure the FTP directory is created on the server
//                    //ftp.UploadFile(@"D:\Github\FluentFTP\README.md", "/public_html/temp/README.md", FtpRemoteExists.Overwrite, true);

//                    // upload a file and ensure the FTP directory is created on the server, verify the file after upload
//                    //ftp.UploadFile(@"D:\Github\FluentFTP\README.md", "/public_html/temp/README.md", FtpRemoteExists.Overwrite, true, FtpVerify.Retry);

//                }

//                // delete forder temp
//                if (File.Exists(_fileTem))
//                {
//                    File.Delete(_fileTem);
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {
//                Logger.log.Error(ex.ToString());
//                return false;
//            }
//        }

//        /// <summary>
//        /// Upload file lên FTP server, tạo folder như đường dẫn file
//        /// </summary>
//        /// <param name="p_file_remote_save">đường đẫn đầy đủ của file muốn save ở máy ftp</param>
//        /// <param name="p_file_local">đường đẫn đầy đủ của file down về trước khi gửi đi</param>
//        public bool CreateFileFolder_VNX(string p_file_remote_save, string p_file_local)
//        {
//            try
//            {
//                // tạo folder trước
//                string[] _arr = p_file_remote_save.Split('/');
//                string _file_name_download = _arr[_arr.Length - 1];
//                string _folder_Upload_remote = p_file_remote_save.Replace(_file_name_download, "");
//                Check_And_CreateFTPDirectory(_folder_Upload_remote);

//                // check file download
//                if (File.Exists(p_file_local))
//                {
//                    // thực hiện upload ở đây
//                    UploadOneFile(p_file_local, _folder_Upload_remote);
//                }

//                return true;
//            }
//            catch (Exception ex)
//            {
//                Logger.log.Error(ex.ToString());
//                return false;
//            }
//        }

//        private void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e)
//        {
//            if (e.PolicyErrors != System.Net.Security.SslPolicyErrors.None)
//            {
//                // invalid cert, do you want to accept it?
//                e.Accept = true;
//            }
//            else
//            {
//                e.Accept = true;
//            }
//        }
//    }
//}
