using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using Globally;
using Ionic.Zip;
using Ionic.Zlib;
using Other_Settings;
using MySql.Data.MySqlClient;

namespace Backuper
{
    public partial class MainForm : Form
    {
        private BackgroundWorker createBackupBw;
        private BackgroundWorker uploadBackupBw;
        private string directoryForSaveBackup = "";
        private string directoryForWork = Application.StartupPath + "\\Temp";

        public MainForm()
        {
            InitializeComponent();

            createBackupBw = new BackgroundWorker();
            createBackupBw.DoWork += new DoWorkEventHandler(DoWork);
            createBackupBw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            createBackupBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWWine);
            createBackupBw.WorkerSupportsCancellation = true;

            uploadBackupBw = new BackgroundWorker();
            uploadBackupBw.DoWork += new DoWorkEventHandler(upDoWork);
            uploadBackupBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(upWine);
            uploadBackupBw.WorkerSupportsCancellation = true;
        }

        #region BackgroundWorker для создания бэкапа
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate(){
                    button1.Enabled = false;
                    button2.Enabled = false;
                });
            }
            BackUP_Action(directoryForSaveBackup);
            if (createBackupBw.CancellationPending == true)
            {
                e.Cancel = true;
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void BWWine(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                button1.Enabled = true;
                button2.Enabled = true;
            });
        }
        #endregion

        #region BackgroundWorker для загрузки бэкапа на сервер

        private void upDoWork(object sender, DoWorkEventArgs e)
        {
            if (sender is string)
            {
                string filename = (string)sender;
                restoreBackup(filename);
            }
        }

        private void upWine(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Бэкап залит на сервер");
        }

        #endregion

        private bool mysqlbackup(string file)
        {
            try
            {
                string mysql_conn = @"Database=" + vars.mysql.database + @"; 
                                      Data Source=" + vars.mysql.host + @"; 
                                      User Id=" + vars.mysql.login + @"; 
                                      Password=" + vars.mysql.password + @"; 
                                      charset=utf8; port=3306;Allow Zero Datetime = true";
                //MessageBox.Show(mysql_conn);
                using (MySqlConnection conn = new MySqlConnection(mysql_conn))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(file);
                            conn.Close();
                            MessageBox.Show("Бэкап базы данных сделан");
                            return true;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                MessageBox.Show("База по данному адресу отсутствует или к ней нет доступа");
                return false;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Данная папка или устройство отсутствует");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
                return false;
            }
        }

        private void mysqlimport(string file)
        {
            try
            {
                string mysql_conn = @"Database=" + vars.mysql.database + @"; 
                                      Data Source=" + vars.mysql.host + @"; 
                                      User Id=" + vars.mysql.login + @"; 
                                      Password=" + vars.mysql.password + @"; 
                                      charset=utf8; port=3306; Allow Zero Datetime = true";

                using (MySqlConnection conn = new MySqlConnection(mysql_conn))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ImportFromFile(file);
                            conn.Close();
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                MessageBox.Show("База по данному адресу отсутствует или к ней нет доступа");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Данная папка или устройство отсутствует");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MySQL_settings settings = new MySQL_settings();
            settings.ShowDialog();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            FTP_Settings ftp_s = new FTP_Settings();
            ftp_s.ShowDialog();
        }

        private void linkLabel3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (label3.Text != "")
            {
                fbd.SelectedPath = label3.Text;
            }
            DialogResult res = fbd.ShowDialog();
            if (res == DialogResult.OK)
            {
                label3.Text = fbd.SelectedPath;
            }

            vars.path_for_backup = label3.Text;
            OtherSettingsSave(vars.path_for_backup, vars.autoBackup);
        }

        private void OtherSettingsSave(string pathForBackup, bool autoBackup)
        {
            Settings sett = new Settings(pathForBackup, autoBackup);
            Settings_Manager sm = new Settings_Manager();
            sm.Save(sett);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(directoryForWork))
            {
                Directory.CreateDirectory(directoryForWork);
            }

            #region Path_for_Backup
            // Загрузка информации о пути для сохранения бэкапов и автобэкапе (true/false)
            Other_Settings.Settings path_sett = new Other_Settings.Settings("", false);
            Other_Settings.Settings_Manager sm = new Other_Settings.Settings_Manager();
            path_sett = sm.Load();
            if (path_sett != null)
            {
                label3.Text = path_sett.adress;
                reserve_backup.Checked = path_sett.autoBackup;
                vars.path_for_backup = path_sett.adress;
                vars.autoBackup = path_sett.autoBackup;
            }
            else
            {
                string defaultPath = Application.StartupPath + "\\dump";
                vars.path_for_backup = defaultPath;
                vars.autoBackup = false;
                reserve_backup.Checked = vars.autoBackup;
                label3.Text = defaultPath;
                if (!Directory.Exists(defaultPath))
                {
                    Directory.CreateDirectory(defaultPath);
                }
                path_sett = new Settings(defaultPath, false);
                sm.Save(path_sett);
            }
            #endregion

            #region FTP
            // загрузка настроек ftp:
            FTP_Connection_Settings.Settings sett = new FTP_Connection_Settings.Settings("", "", "", -1);
            FTP_Connection_Settings.SettingsManager ftp_sm = new FTP_Connection_Settings.SettingsManager();
            if (File.Exists("ftp_settings.conf"))
            {
                sett = ftp_sm.Load();

                vars.ftp.host = sett.ftp_host;
                vars.ftp.login = sett.ftp_user;
                vars.ftp.password = sett.ftp_password;
                vars.ftp.port = sett.ftp_port;
            }
            #endregion

            #region MySQL
            // загрузка настроек mysql:
            Connection_Settings.Settings mysql_sett = new Connection_Settings.Settings("", "", "", "");
            Connection_Settings.Settings_Manager mysql_sm = new Connection_Settings.Settings_Manager();
            if (File.Exists("mysql_connection_settings.conf"))
            {
                mysql_sett = mysql_sm.Load();

                vars.mysql.host = mysql_sett.ip_adress;
                vars.mysql.database = mysql_sett.database;
                vars.mysql.login = mysql_sett.user;
                vars.mysql.password = mysql_sett.password;
            }
            #endregion


            #region Таблица файлов
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = "filename";
            column.HeaderText = "Имя файла";
            backuper_dg.Columns.Add(column);

            DataGridViewTextBoxColumn date_create = new DataGridViewTextBoxColumn();
            date_create.Name = "date_create";
            date_create.HeaderText = "Дата создания";
            backuper_dg.Columns.Add(date_create);

            DataGridViewTextBoxColumn full_filename = new DataGridViewTextBoxColumn();
            full_filename.Name = "full_filename";
            full_filename.Visible = false;
            backuper_dg.Columns.Add(full_filename);
            #endregion

            #region АвтоБэкап
            if (vars.autoBackup == true)
            {
                directoryForSaveBackup = vars.path_for_backup;
                if (Directory.Exists(directoryForSaveBackup))
                {
                    createBackupBw.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Данная папка или устройство отсутствует");
                }
            }
            #endregion
            Load_Filetable();
        }

        private void Load_Filetable()
        {
            try
            {
                backuper_dg.Rows.Clear();
                if (!String.IsNullOrEmpty(vars.path_for_backup))
                {
                    DirectoryInfo dinfo = new DirectoryInfo(vars.path_for_backup);
                    if (dinfo.Exists)
                    {
                        FileInfo[] filelist = dinfo.GetFiles("*.zip");
                        for (int i = 0; i < filelist.Length; i++)
                        {
                            backuper_dg.Rows.Add();
                            backuper_dg[0, i].Value = filelist[i].Name;
                            backuper_dg[1, i].Value = filelist[i].CreationTime;
                            backuper_dg[2, i].Value = filelist[i].FullName;
                        }
                    }
                }

                file_count.Text = backuper_dg.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            directoryForSaveBackup = vars.path_for_backup;
            if (Directory.Exists(directoryForSaveBackup))
            {
                createBackupBw.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Данная папка или устройство отсутствует");
            }
        }

        /// <summary>
        /// Непосредственное действо бэкапа
        /// </summary>
        /// <param name="directoryPath">Директория, куда бэкап закинут будет</param>
        private void BackUP_Action(string directoryPath)
        {
            try
            {
                // генерация списка файлов для архивирования
                List<FileName> fileNames = new List<FileName>();
                List<DirectoryInfo> directories = new List<DirectoryInfo>();
                // генерация имени файла дампа БД:
                string sql_namefile =
                    directoryForWork + "\\" +
                    DateTime.Now.ToShortDateString().Replace(":", "-") + "_" +
                    DateTime.Now.ToShortTimeString().Replace(":", "-") + "_backup.sql";
                // бэкап БД:
                try
                {
                    bool result = mysqlbackup(sql_namefile);
                    if (result)
                    {
                        fileNames.Add(new FileName(sql_namefile, DateTime.Now.ToShortDateString().Replace(":", "-") + "_" +
                                                                 DateTime.Now.ToShortTimeString().Replace(":", "-") +
                                                                 "_backup.sql"));
                    }
                }
                catch (Exception)
                {
                }
                if (checkBox1.Checked == true)
                {
                    try
                    {
                        // если выбрано - в список закидываются файлы с ftp:
                        FtpWebRequest ftpclient;
                        List<FtpFile> rootFilesInfo = new List<FtpFile>();
                        List<FtpDirectory> rootDirInfo = new List<FtpDirectory>();
                        string ftpDirectoryPath = directoryForWork + "\\ftp";
                        bool prost = true;
                        directories.Add(new DirectoryInfo(ftpDirectoryPath));
                        if (!directories[0].Exists)
                        {
                            directories[0].Create();
                        }
                        GetDirectoryList("", ref rootFilesInfo, ref rootDirInfo);

                        foreach (FtpFile f in rootFilesInfo)
                        {
                            DownloadFile(f.name, directories[0].FullName);
                            string pathForDownloadedFile = directories[0].FullName + "\\" + f.name;
                            ////MessageBox.Show(pathForDownloadedFile);
                            fileNames.Add(new FileName(pathForDownloadedFile, f.name));
                        }
                        foreach (var directory in rootDirInfo)
                        {
                            if (!Directory.Exists(directory.fullName))
                            {
                                Directory.CreateDirectory(directory.fullName);
                            }
                            List<FtpFile> f_info = new List<FtpFile>();
                            foreach (var ftpFile in f_info)
                            {
                                DownloadFile(ftpFile.name, directories[0].FullName + "\\" + directory.Name);
                                string pathForDownloadedFile = directories[0].FullName + "\\" + directory.Name + "\\" +
                                                               ftpFile.name;
                                ////MessageBox.Show(pathForDownloadedFile);
                                fileNames.Add(new FileName(pathForDownloadedFile, ftpFile.name));
                            }
                            List<FtpDirectory> d_info = new List<FtpDirectory>();
                            GetDirectoryList(directory.Name, ref f_info, ref d_info);
                            //MessageBox.Show("Файлов:" + f_info.Count + ", Папок:" + d_info.Count);
                        }
                        //MessageBox.Show("" + d_info.Count);
                        //ftpclient.ContentLength = fi.Length;
                    }
                    catch (Exception)
                    {
                    }
                }
                string DefaultFolderForSaveFiles = @"\dump";
                string currentDirectory = Environment.CurrentDirectory;
                string dir = directoryPath;
                //Console.WriteLine(filenames[0].short_filename);
                string newPath = "";
                newPath = dir + "\\" + DateTime.Now.ToShortDateString().Replace(":", "-") + "_" +
                          DateTime.Now.ToShortTimeString().Replace(":", "-") + "_backup" + ".zip";
                //newPath = dir + "\\" + fileNames[0].short_filename + ".zip";

                //Console.WriteLine("newPath:" + newPath);
                // Добавление файлов в архив:

                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.Always;
                    zip.AlternateEncoding = Encoding.GetEncoding(866);
                    foreach (FileName f in fileNames)
                    {
                        zip.AddFile(f.full_filename, "");
                    }
                    foreach (var directory in directories)
                    {
                        zip.AddDirectory(directory.FullName, directory.Name);
                    }
                    zip.CompressionLevel = CompressionLevel.Level5;
                    zip.Save(newPath);
                }

                foreach (var fileName in fileNames)
                {
                    File.Delete(fileName.full_filename);
                }
                foreach (var directory in directories)
                {
                    List<string> files = Directory.GetFiles(directory.FullName).ToList();
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                    Directory.Delete(directory.FullName);
                }
                File.Delete(sql_namefile);
                if (backuper_dg.InvokeRequired)
                {
                    backuper_dg.Invoke((MethodInvoker)
                        delegate
                        {
                            Load_Filetable();
                        }
                        );
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void GetDirectoryList(string directoryPath, ref List<FtpFile> files, ref List<FtpDirectory> dirs)
        {
            try
            {
                files.Clear();
                dirs.Clear();

                FtpWebRequest ftpclient;
                ftpclient = (FtpWebRequest) FtpWebRequest.Create(vars.ftp.host + directoryPath);
                ftpclient.Credentials = new NetworkCredential(vars.ftp.login, vars.ftp.password);
                //FileInfo fi = new FileInfo(filenames[i, 0]);

                ftpclient.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse response = (FtpWebResponse) ftpclient.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    Regex regex =
                        new Regex(
                            @"^([d-])([rwxt-]{3}){3}\s+\d{1,}\s+.*?(\d{1,})\s+(\w+\s+\d{1,2}\s+(?:\d{4})?)(\d{1,2}:\d{2})?\s+(.+?)\s?$",
                            RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase |
                            RegexOptions.IgnorePatternWhitespace);
                    var matches = regex.Match(line);
                    List<string> match_group = new List<string>();
                    int match_number = -1;
                    int size = 0;
                    string type = "";
                    foreach (var match in matches.Groups)
                    {
                        match_number++;

                        //MessageBox.Show(match.ToString() + " " + match_number);
                        switch (match_number)
                        {
                            case 1:
                                //MessageBox.Show(match.ToString());
                                if (match.ToString() == "-")
                                {
                                    type = "file";
                                }
                                else if (match.ToString() == "d")
                                {
                                    type = "directory";
                                }
                                //MessageBox.Show(type);
                                break;
                            case 2:
                                break;
                            case 3:
                                size = int.Parse(match.ToString());
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                            case 6:
                                //MessageBox.Show(type);
                                if (type == "file")
                                {
                                    //MessageBox.Show(match.ToString());
                                    files.Add(new FtpFile(match.ToString(), size));
                                }
                                else if (type == "directory")
                                {
                                    //MessageBox.Show(match.ToString());
                                    dirs.Add(new FtpDirectory(vars.path_for_backup + "\\" + match.ToString(),
                                        match.ToString(), directoryPath));
                                }
                                break;
                        }
                        match_group.Add(match.ToString());
                    }
                }
            }
            catch (WebException)
            {
                MessageBox.Show("FTP-сервер недоступен");
            }
        }


        private void DownloadFile(string fileName, string directory)
        {
            FtpWebRequest ftpclient;
            ////MessageBox.Show(f.name);
            ftpclient = (FtpWebRequest)FtpWebRequest.Create(vars.ftp.host + fileName);
            ftpclient.Credentials = new NetworkCredential(vars.ftp.login, vars.ftp.password);
            //FileInfo fi = new FileInfo(filenames[i, 0]);

            ftpclient.Method = WebRequestMethods.Ftp.DownloadFile;
            ftpclient.UseBinary = true;
            ftpclient.UsePassive = true;

            FtpWebResponse resp = (FtpWebResponse)ftpclient.GetResponse();
            Stream ftpStream = resp.GetResponseStream();

            string pathForDownloadedFile = directory + "\\" + fileName;
            //MessageBox.Show(pathForDownloadedFile);
            FileStream f_stream = new FileStream(pathForDownloadedFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            using (BinaryWriter sw = new BinaryWriter(f_stream, Encoding.GetEncoding(1251)))
            {
                byte[] buffer = new byte[1024];
                int bytesRead = ftpStream.Read(buffer, 0, 1024);
                while (bytesRead > 0)
                {
                    //f_stream.Write(buffer, 0, 1024);
                    sw.Write(buffer, 0, 1024);
                    bytesRead = ftpStream.Read(buffer, 0, 1024);
                }
            }
        }

        private void backuper_dg_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
            {
                file_menu.Show(Cursor.Position);
                backuper_dg[e.ColumnIndex, e.RowIndex].Selected = true;
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteFile();
        }

        /// <summary>
        /// Удаление файла по выбору вашему
        /// </summary>
        private void deleteFile()
        {
            int y = backuper_dg.CurrentCellAddress.Y;
            if (y >= 0)
            {
                string filename = backuper_dg[2, y].Value.ToString();
                DialogResult res = MessageBox.Show("Удалить данный файл с бэкапом?", "Удаление файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                        backuper_dg.Rows.RemoveAt(y);
                        file_count.Text = backuper_dg.Rows.Count.ToString();
                    }
                }
            }
        }

        private void backuper_dg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                deleteFile();
            }
            if (e.KeyCode == Keys.Z && e.Shift)
            {
                int y = backuper_dg.CurrentCellAddress.Y;
                if (y >= 0)
                {
                    string filename = backuper_dg[2, y].Value.ToString();
                    restoreBackup(filename);
                }
            }
        }

        private void восстановитьИзБэкапаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int y = backuper_dg.CurrentCellAddress.Y;
            if (y >= 0)
            {
                string filename = backuper_dg[2, y].Value.ToString();
                //restoreBackup(filename);
                uploadBackupBw.RunWorkerAsync(filename);
            }
        }

        /// <summary>
        /// Восстановление информации из бэкапа
        /// </summary>
        /// <param name="filename">Путь к файлу бэкапа</param>
        private void restoreBackup(string filename)
        {
            try
            {
                DialogResult res = MessageBox.Show("Восстановить информацию из бэкапа?", "Восстановление информации", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    if (File.Exists(filename))
                    {
                        string dump_filename = function.ArchiveOperations.SendRFilename(filename);
                        //MessageBox.Show(vars.path_for_backup + "\\" + filename);
                        function.ArchiveOperations.Extract(filename);
                        string full_filename = directoryForWork + "\\" + dump_filename;
                        mysqlimport(full_filename);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void reserve_backup_Click(object sender, EventArgs e)
        {
            bool autoBackup = reserve_backup.Checked;
            vars.autoBackup = autoBackup;
            OtherSettingsSave(vars.path_for_backup, autoBackup);
        }

        private void updateLbl_Click(object sender, EventArgs e)
        {
            Load_Filetable();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            try
            {
                string[] files = Directory.GetFiles(vars.path_for_backup);
                foreach (string filepath in files)
                {
                    File.Delete(filepath);
                }
                Load_Filetable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult res = fbd.ShowDialog();
            if (res == DialogResult.OK)
            {
                directoryForSaveBackup = fbd.SelectedPath;
                createBackupBw.RunWorkerAsync();
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                string filename = ofd.FileName;
                //restoreBackup(filename);
                uploadBackupBw.RunWorkerAsync(filename);
            }
        }
    }
}
