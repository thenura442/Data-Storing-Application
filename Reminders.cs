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
    public partial class Reminders : Form
    {

        string currentuser, currentusertype;

        public string collectionName = "Reminders";
        public IMongoCollection<remindermodel> reminderCollection;


        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        public Reminders()
        {
            InitializeComponent();

            pnlNav.Height = reminderbtn.Height;
            pnlNav.Top = reminderbtn.Top;
            pnlNav.Left = reminderbtn.Left;
            reminderbtn.BackColor = Color.FromArgb(46, 51, 93);

            timepicker.Format = DateTimePickerFormat.Time;
            timepicker.ShowUpDown = true;

            datepicker.Format = DateTimePickerFormat.Custom;
            datepicker.CustomFormat = "MM/dd/yyyy";

            var client = new MongoClient(staticmethods.getconnection());
            var db = client.GetDatabase(staticmethods.getdatabase());
            reminderCollection = db.GetCollection<remindermodel>(collectionName);


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

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datenow = DateTime.Now;
            var timenow = datenow.TimeOfDay;

            var filterDefinition = Builders<remindermodel>.Filter.Lte(b => b.reminderdate, datenow);
            var projection = Builders<remindermodel>.Projection.Exclude("_id").Include("reminderdate").Include("remindername");
            var reminders = reminderCollection.Find(filterDefinition).Project<remindermodel>(projection)
                .FirstOrDefault();
            
            if (reminders != null)
            {
                var name = reminders.remindername;
                this.Alert("Reminder "+ name, Form_Alert.enmType.Info);
                reminderCollection.DeleteOneAsync(filterDefinition);
            }
        }

        private void setreminder_Click(object sender, EventArgs e)
        {
            DateTime dateset = datepicker.Value;
            var timeOfDay =  this.timepicker.Value.TimeOfDay;
            var setdate = dateset.Date.Add(timeOfDay);
            DateTime datetoday = DateTime.Now;

            if (datetoday <= setdate)
            {
                this.Alert("Reminder Set!", Form_Alert.enmType.Info);
                var remindermodel = new remindermodel
                {
                    remindername = aname.Text,
                    reminderdate = setdate,
                    reminderdescription = reminderdesc.Text,
                };


                reminderCollection.InsertOneAsync(remindermodel);
            }
            else
            {
                this.Alert("Reminder has Alredy Passed!", Form_Alert.enmType.Warning);
            }
        }

    }
}
