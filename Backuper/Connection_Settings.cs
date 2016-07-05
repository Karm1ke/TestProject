using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using Globally;

namespace Connection_Settings
{
    class Settings
    {
        public string database { get; set; }
        public string ip_adress { get; set; }
        public string user { get; set; }
        public string password { get; set; }

        public Settings(string database, string ip_adress, string user, string password)
        {
            this.database = database;
            this.ip_adress = ip_adress;
            this.user = user;
            this.password = password;
        }
    }

    class Settings_Manager
    {
        private string filename = Application.StartupPath + "\\mysql_connection_settings.conf";
        
        public void Save(Settings settings)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create)))
            {
                sw.WriteLine(function.AESSha1Crypter.Encrypt(settings.database, "qwertyman"));
                sw.WriteLine(function.AESSha1Crypter.Encrypt(settings.ip_adress, "qwertyman"));
                sw.WriteLine(function.AESSha1Crypter.Encrypt(settings.user, "qwertyman"));
                sw.WriteLine(function.AESSha1Crypter.Encrypt(settings.password, "qwertyman"));
                sw.Close();
            }
        }

        public Settings Load()
        {
            try
            {
                if (File.Exists(filename))
                {
                    Settings rtnSetting = new Settings("", "", "", "");
                    using (StreamReader sr = new StreamReader(File.Open(filename, FileMode.Open)))
                    {
                        rtnSetting.database = function.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.ip_adress = function.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.user = function.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
                        rtnSetting.password = function.AESSha1Crypter.Decrypt(sr.ReadLine(), "qwertyman");
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
