using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTP_Connection_Settings;
using Globally;

namespace Backuper
{
    public partial class FTP_Settings : Form
    {
        public FTP_Settings()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // сохранение настроек в файл:
            Settings sett = new Settings(ip.Text, login.Text, password.Text, Convert.ToInt32(port.Text));
            SettingsManager sm = new SettingsManager();
            sm.Save(sett);

            vars.ftp.host = ip.Text;
            vars.ftp.login = login.Text;
            vars.ftp.password = password.Text;
            vars.ftp.port = Convert.ToInt32(port.Text);

            this.Close();
        }

        private void MySQL_Settings_Load(object sender, EventArgs e)
        {
            // загрузка настроек из файла:
            Settings sett = new Settings("", "", "", -1);
            SettingsManager sm = new SettingsManager();
            sett = sm.Load();

            // добавление в текстовые поля значений:
            ip.Text = sett.ftp_host;
            login.Text = sett.ftp_user;
            password.Text = sett.ftp_password;
            port.Text = sett.ftp_port.ToString();
        }
    }
}
