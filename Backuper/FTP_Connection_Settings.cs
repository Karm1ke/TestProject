using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using Globally;

namespace FTP_Connection_Settings
{
    class Settings
    {
        public string ftp_host { get; set; }
        public string ftp_user { get; set; }
        public string ftp_password { get; set; }
        public int ftp_port { get; set; }

        public Settings(string ftp_host, string ftp_user, string ftp_password, int ftp_port)
        {
            this.ftp_host = ftp_host;
            this.ftp_password = ftp_password;
            this.ftp_user = ftp_user;
            this.ftp_port = ftp_port;
        }
    }

    class SettingsManager
    {
        private string filename = Application.StartupPath + "\\ftp_settings.conf";

        public void Save(Settings settings)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create)))
            {
                sw.WriteLine(function.AESSha1Crypter.Encrypt(settings.ftp_host, "qwertyman"));
                sw.WriteLine(function.AESSha1Crypter.Encrypt(settings.ftp_user, "qwertyman"));
                sw.WriteLine(function.AESSha1Crypter.Encrypt(settings.ftp_password, "qwertyman"));
                sw.WriteLine(function.AESSha1Crypter.Encrypt(settings.ftp_port.ToString(), "qwertyman"));
                sw.Close();
            }
        }

        public Settings Load()
        {
            try
            {
                if (File.Exists(filename))
                {
                    Settings rtnSetting = new Settings("", "", "", 21);
                    using (StreamReader sr = new StreamReader(File.Open(filename, FileMode.Open)))
                    {
                        rtnSetting.ftp_host = function.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.ftp_user = function.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.ftp_password = function.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.ftp_port = int.Parse(function.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman"));
                        sr.Close();
                    }
                    return rtnSetting;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
