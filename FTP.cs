using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Globally;
using MySqlLib;
using System.Net;
using System.IO;

namespace WindowsFormsApplication2
{
    class FTP
    {
        private string FTP_Server_IP = vars.ftp_server_ip;
        private int FTP_Port = 21;
        private string FTP_User = vars.ftp_user;
        private string FTP_Password = vars.ftp_password;

        

        /// <summary>
        /// Загрузка выбранного файла на сервер
        /// </summary>
        /// <param name="PathFile">Путь загружаемого файла</param>
        /// <param name="NameOfProject">Имя проета для которого загружается файл</param>
        public void UploadFile(string PathFile, string NameOfProject)
        {   
            // Проверяет создан ли каталог для данного проекта
            try
            {
                CreateOrCheckFolderFtp(NameOfProject);
            }
            catch
            {
                return;
            }
            FileInfo filePath = new FileInfo(PathFile);
            string uri = "ftp://" + FTP_Server_IP + "/" + NameOfProject + "/" +filePath.Name;

            FtpWebRequest ftpClient;
            ftpClient = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));                
            ftpClient.Credentials = new NetworkCredential(FTP_User, FTP_Password);
            ftpClient.Method = WebRequestMethods.Ftp.UploadFile;
            ftpClient.UsePassive = true;
            ftpClient.ContentLength = filePath.Length;

            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            // Открываем filestream (System.IO.FileStream) для чтения отправляемого файла
            FileStream fs = filePath.OpenRead();

            try
            {
                // Получает поток, используемый для выгрузки данных на FTP-сервер
                Stream strm = ftpClient.GetRequestStream();

                contentLen = fs.Read(buff, 0, buffLength);       

                while (contentLen != 0)
                {
                    // Записываем контент из потока на ftp сервер
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                strm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                fs.Close();
                FtpWebResponse response = (FtpWebResponse)ftpClient.GetResponse();
                MessageBox.Show(response.StatusDescription, "Сообщение от сервера");
                response.Close();
            }

            fs.Close();      

        }

        /// <summary>
        /// Проверяет создана ли папка для данного проекта на сервере, если нет, то создает ее
        /// </summary>
        /// <param name="name_directory">Название папки</param>
        void CreateOrCheckFolderFtp(string name_directory)
        {
            string uri = "ftp://" + FTP_Server_IP + "/" + name_directory + "/";

            try
            {
                FtpWebRequest ftpClient;
                ftpClient = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftpClient.Credentials = new NetworkCredential(FTP_User, FTP_Password);
                ftpClient.KeepAlive = false;
                ftpClient.UseBinary = true;
                ftpClient.Proxy = null;
                ftpClient.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse resp = (FtpWebResponse)ftpClient.GetResponse();
                resp.Close();
            }
            catch
            {
                FtpWebRequest ftpClient;
                ftpClient = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftpClient.Credentials = new NetworkCredential(FTP_User, FTP_Password);
                ftpClient.KeepAlive = false;
                ftpClient.UseBinary = true;
                ftpClient.Proxy = null;
                ftpClient.Method = WebRequestMethods.Ftp.MakeDirectory;

                try
                {
                    FtpWebResponse resp = (FtpWebResponse)ftpClient.GetResponse();
                    resp.Close();
                }
                catch
                {
                    DialogResult result = MessageBox.Show("Пожалуйста проверте настройки подключения" + "\r\n" +
                    "Желаете внести изменения в настройки подключения к серверу? ", "Ошибка подключения", MessageBoxButtons.OKCancel,MessageBoxIcon.Error);

                    if (result == DialogResult.OK)
                    {
                        FTP_Settings newSettings = new FTP_Settings(false);
                        newSettings.ShowDialog();
                    }
                    return;
                }

                
            }       
        }

        /// <summary>
        /// Загрузка файла с FTP-сервера
        /// </summary>
        /// <param name="PathForSaveFile">Путь для сохранения загружаемого файла</param>
        public bool DownloadFile(string PathForSaveFile, int file_id)
        {
             FtpWebRequest ftpRequest = null;
             FtpWebResponse ftpResponse = null;
             Stream ftpStream = null;
             int bufferSize = 2048;

             string FilePathForDownload = QueryForPathFileOnServer(file_id);

             if (FilePathForDownload == "") return false;

             string uri = "ftp://" + FTP_Server_IP + "/" + FilePathForDownload;
            
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftpRequest.Credentials = new NetworkCredential(FTP_User, FTP_Password);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                //Устанавливает обратную связь с FTP Serve
                try
                {
                    ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                }
                catch
                {
                    DialogResult result = MessageBox.Show("Пожалуйста проверте настройки подключения" + "\r\n" +
                    "Желаете внести изменения в настройки подключения к серверу? ", "Ошибка подключения", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    if (result == DialogResult.OK)
                    {
                        FTP_Settings newSettings = new FTP_Settings(false);
                        newSettings.ShowDialog();
                    }
                    return false;
                }
                //Получает поток с сервера
                ftpStream = ftpResponse.GetResponseStream();
                // Открывает поток для записи скаченного файла
                FileStream localFileStream = new FileStream(PathForSaveFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                
                // Буфер для скаченной информации
                byte[] byteBuffer = new byte[bufferSize];
                int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                // Загружает файл записывая скаченные данные покамись зачивание не зкончится
                try
                {
                    while (bytesRead > 0)
                    {
                        localFileStream.Write(byteBuffer, 0, bytesRead);
                        bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                localFileStream.Dispose();
                localFileStream.Close();
                ftpStream.Dispose();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
                
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            return true;
        }

        /// <summary>
        /// Запрос к БД на получение пути файла для последующе загрузки(удаления) с FTP-сервера
        /// </summary>
        /// <param name="file_id">Номер проекта</param>
        /// <returns>Путь файла загружаемоего с FTP-сервера</returns>
        string QueryForPathFileOnServer(int file_id)
        {
            string PathFile = "";

            MySqlData.MySqlExecute.MyResult result = new MySqlData.MySqlExecute.MyResult();
            result = MySqlData.MySqlExecute.SqlScalar(@"
            SELECT file_storage
            FROM project_files
            WHERE file_id = " + file_id + "", vars.connection);

            if (result.HasError == false)
            {
                PathFile = result.ResultText.ToString();
            }
            else
            {
                MessageBox.Show(result.ErrorText);
            }

            return PathFile;
        }


        /// <summary>
        /// Удаление файла с FTP-сервера
        /// </summary>
        /// <param name="file_id">id файла</param>
        public void DeleteFile(int file_id)
        {
            FtpWebRequest ftpRequest = null;
            FtpWebResponse ftpResponse = null;

            string FilePathForDeleteFile = QueryForPathFileOnServer(file_id);
            string uri = "ftp://" + FTP_Server_IP + "/" + FilePathForDeleteFile;

            try
            {
                ftpRequest = (FtpWebRequest)WebRequest.Create(uri);
                // Присоединяемся к FTP серверу при помощи логина и пароля
                ftpRequest.Credentials = new NetworkCredential(FTP_User, FTP_Password);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                try
                {
                    ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                }
                catch
                {
                    DialogResult result = MessageBox.Show("Пожалуйста проверте настройки подключения" + "\r\n" +
                    "Желаете внести изменения в настройки подключения к серверу? ", "Ошибка подключения", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    if (result == DialogResult.OK)
                    {
                        FTP_Settings newSettings = new FTP_Settings(false);
                        newSettings.ShowDialog();
                    }
                    return;
                }
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            QueryForDeleteFile(file_id);
            return;
        }


        /// <summary>
        /// Запрос к БД на удаление заданного файла
        /// </summary>
        /// <param name="file_id">id файла</param>
        void QueryForDeleteFile(int file_id)
        {
            MySqlData.MySqlExecute.MyResult result = new MySqlData.MySqlExecute.MyResult();
            result = MySqlData.MySqlExecute.SqlNoneQuery(@"
            DELETE FROM project_files
            WHERE file_id = " + file_id + "", vars.connection);

            if (result.HasError != false)
            {
                MessageBox.Show(result.ErrorText);
            }
        }

    }
}
