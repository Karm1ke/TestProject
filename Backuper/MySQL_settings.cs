using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using  Connection_Settings;
using Globally;

namespace Backuper
{
    public partial class MySQL_settings : Form
    {
        public MySQL_settings()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings sett = new Settings(database.Text, ip.Text, user.Text, password.Text);
            Settings_Manager sm = new Settings_Manager();
            sm.Save(sett);

            vars.mysql.host = ip.Text;
            vars.mysql.database = database.Text;
            vars.mysql.login = user.Text;
            vars.mysql.password = password.Text;

            this.Close();
        }

        private void MySQL_settings_Load(object sender, EventArgs e)
        {
            if (File.Exists("mysql_connection_settings.conf"))
            {
                Settings sett = new Settings("", "", "", "");
                Settings_Manager sm = new Settings_Manager();
                sett = sm.Load();
                ip.Text = sett.ip_adress;
                database.Text = sett.database;
                user.Text = sett.user;
                password.Text = sett.password;
            }
        }
    }
}
