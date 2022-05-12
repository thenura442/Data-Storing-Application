using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Storing_App
{
    public partial class Settings : Form
    {

        string currentuser, currentusertype;

        Forms fm = new Forms();
        Home hm = new Home();
        Databases dbs = new Databases();
        Reminders rms = new Reminders();
        Login lg = new Login();
        Settings sts = new Settings();
        public Settings()
        {
            InitializeComponent();

            //starting the navigation
            pnlNav.Height = settingsbtn.Height;
            pnlNav.Top = settingsbtn.Top;
            pnlNav.Left = settingsbtn.Left;
            settingsbtn.BackColor = Color.FromArgb(46, 51, 93);


            currentuser = Login.user;
            currentusertype = Login.type;
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = homebtn.Height;
            pnlNav.Top = homebtn.Top;
            pnlNav.Left = homebtn.Left;
            homebtn.BackColor = Color.FromArgb(46, 51, 93);

            hm.Show();
            this.Hide();
        }

        private void formsbtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = formsbtn.Height;
            pnlNav.Top = formsbtn.Top;
            pnlNav.Left = formsbtn.Left;
            formsbtn.BackColor = Color.FromArgb(46, 51, 93);

            this.Hide();
            fm.Show();
        }

        private void databasebtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = databasebtn.Height;
            pnlNav.Top = databasebtn.Top;
            pnlNav.Left = databasebtn.Left;
            databasebtn.BackColor = Color.FromArgb(46, 51, 93);

            dbs.Show();
            this.Hide();

        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = reminderbtn.Height;
            pnlNav.Top = reminderbtn.Top;
            pnlNav.Left = reminderbtn.Left;
            reminderbtn.BackColor = Color.FromArgb(46, 51, 93);

            this.Hide();
            rms.Show();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = logoutbtn.Height;
            pnlNav.Top = logoutbtn.Top;
            pnlNav.Left = logoutbtn.Left;
            logoutbtn.BackColor = Color.FromArgb(46, 51, 93);

            this.Hide();
            lg.Show();
        }

        private void settingsbtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = settingsbtn.Height;
            pnlNav.Top = settingsbtn.Top;
            pnlNav.Left = settingsbtn.Left;
            settingsbtn.BackColor = Color.FromArgb(46, 51, 93);
        }

        private void homebtn_Leave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void formsbtn_Leave(object sender, EventArgs e)
        {
            formsbtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void databasebtn_Leave(object sender, EventArgs e)
        {
            databasebtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void reminderbtn_Leave(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void logoutbtn_Leave(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void settingsbtn_Leave(object sender, EventArgs e)
        {
            settingsbtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void help_Click(object sender, EventArgs e)
        {
            Help hp = new Help();
            hp.Show();
            this.Hide();
        }

        private void manual_Click(object sender, EventArgs e)
        {
            Manual hp = new Manual();
            hp.Show();
            this.Hide();
        }

    }
}
