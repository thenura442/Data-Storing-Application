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
    public partial class Help : Form
    {

        string currentuser, currentusertype;
        public Help()
        {
            InitializeComponent();

            currentuser = staticmethods.getuser();
            currentusertype = staticmethods.gettype();

            usernamelbl.Text = currentuser;
            usertypelbl.Text = currentusertype;
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = homebtn.Height;
            pnlNav.Top = homebtn.Top;
            pnlNav.Left = homebtn.Left;
            homebtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.homeshow();
            this.Hide();
        }

        private void formsbtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = formsbtn.Height;
            pnlNav.Top = formsbtn.Top;
            pnlNav.Left = formsbtn.Left;
            formsbtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.formsshow();
            this.Hide();
        }

        private void databasebtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = databasebtn.Height;
            pnlNav.Top = databasebtn.Top;
            pnlNav.Left = databasebtn.Left;
            databasebtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.databaseshow();
            this.Hide();
        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = reminderbtn.Height;
            pnlNav.Top = reminderbtn.Top;
            pnlNav.Left = reminderbtn.Left;
            reminderbtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.remindershow();
            this.Hide();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = logoutbtn.Height;
            pnlNav.Top = logoutbtn.Top;
            pnlNav.Left = logoutbtn.Left;
            logoutbtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.logoutshow();
            this.Hide();
        }

        private void settingsbtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = settingsbtn.Height;
            pnlNav.Top = settingsbtn.Top;
            pnlNav.Left = settingsbtn.Left;
            settingsbtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.settingsshow();
            this.Hide();
        }

        private void homebtn_Leave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void formsbtn_Leave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void databasebtn_Leave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void reminderbtn_Leave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void logoutbtn_Leave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void settingsbtn_Leave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.FromArgb(24, 30, 54);
        }
    }
}
