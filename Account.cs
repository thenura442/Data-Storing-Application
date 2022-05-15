using Data_Storing_App.Models;
using MongoDB.Driver;
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
    public partial class Account : Form
    {

        string currentuser, currentusertype;

        // Creating connection and initialising the collection
        public string collectionName = "Users";
        public IMongoCollection<usermodel> userCollection;

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        public Account()
        {
            InitializeComponent();

            //starting the navigation

            pnlNav.Visible = false;

            currentuser = staticmethods.getuser();
            currentusertype = staticmethods.gettype();

            usernamelbl.Text = currentuser;
            usertypelbl.Text = currentusertype;

            var client = new MongoClient(staticmethods.getconnection());
            var db = client.GetDatabase(staticmethods.getdatabase());
            userCollection = db.GetCollection<usermodel>(collectionName);

            showuserdetail();
            resetall();
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

        private void changepass_Click(object sender, EventArgs e)
        {
            passtxt.Visible = true;
            repasstxt.Visible = true;
            updtpassbtn.Visible = true;
            cancelbtn.Visible = true;
            changepass.Visible = false;
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            resetall();
        }

        public void showuserdetail()
        {
            var usern = staticmethods.getuser();

            var filterDefinition = Builders<usermodel>.Filter.Eq(a => a.Username,usern);
            var projection = Builders<usermodel>.Projection.Exclude("_id");
            var users = userCollection.Find(filterDefinition).Project<usermodel>(projection).FirstOrDefault();

            if(users != null)
            {
                user_namelbl.Text = users.Username;
                u_typelbl.Text = users.User_Type;
                u_emaillbl.Text = users.User_Email;
                u_name.Text = users.Name;
            }

        }

        private void updtpassbtn_Click(object sender, EventArgs e)
        {
            if((passtxt.Text == repasstxt.Text) & (passtxt.Text != ""))
            {
                var usern = staticmethods.getuser();
                var filterupdate = Builders<usermodel>.Filter.Eq(a => a.Username, usern);
                var updateDefinition = Builders<usermodel>.Update
                    .Set(a => a.Password, passtxt.Text);

                userCollection.UpdateOneAsync(filterupdate, updateDefinition);

                this.Alert("Password of " + usern + " Updated\nSuccessfully!", Form_Alert.enmType.Success);
                resetall();
            }
            else
            {
                this.Alert("Please Enter Matching Passwords!", Form_Alert.enmType.Warning);
            }
        }

        public void resetall()
        {
            passtxt.Visible = false;
            repasstxt.Visible = false;
            updtpassbtn.Visible = false;
            cancelbtn.Visible = false;
            changepass.Visible = true;
        }
    }
}
