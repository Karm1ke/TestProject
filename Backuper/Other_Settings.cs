using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Globally;

namespace Other_Settings
{
    class  Settings
    {
        public string adress;
        public bool autoBackup;

        public Settings(string adress, bool autoBackup)
        {
            this.adress = adress;
            this.autoBackup = autoBackup;
        }
    }

    class Settings_Manager
    {
        string filename = Environment.CurrentDirectory + "\\other_settings.conf";
        public Settings Load()
        {
            try
            {
                if (File.Exists(filename))
                {
                    Settings sett = new Settings("", false);
                    using (StreamReader reader = new StreamReader(File.Open(filename, FileMode.Open)))
                    {
                        sett.adress = function.AESSha1Crypter.Decrypt(reader.ReadLine(), "m4fctck2");
                        sett.autoBackup = Boolean.Parse(function.AESSha1Crypter.Decrypt(reader.ReadLine(), "m4fctck2"));
                        reader.Close();
                    }
                    return sett;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void Save(Settings sett)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(File.Open(filename, FileMode.OpenOrCreate)))
                {
                    writer.WriteLine(function.AESSha1Crypter.Encrypt(sett.adress, "m4fctck2"));
                    writer.WriteLine(function.AESSha1Crypter.Encrypt(sett.autoBackup.ToString(), "m4fctck2"));
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
