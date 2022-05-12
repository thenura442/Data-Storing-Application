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
    public partial class User_Form : Form
    {

        // Creating connection and initialising the collection
        string connectionString = "mongodb://localhost:27017";
        public string databaseName = "DataStore";
        public string collectionName = "Users";
        public IMongoCollection<usermodel> userCollection;

        // creating a list of bridges
        Home hm = new Home();
        Forms fm = new Forms();
        Databases dbs = new Databases();
        Reminders rms = new Reminders();
        Login lg = new Login();
        Settings sts = new Settings();


        //Accessing Alert
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }



        public User_Form()
        {
            InitializeComponent();

            //starting the navigation
            pnlNav.Height = formsbtn.Height;
            pnlNav.Top = formsbtn.Top;
            pnlNav.Left = formsbtn.Left;
            formsbtn.BackColor = Color.FromArgb(46, 51, 93);

            //Initializing conncetion to database
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            userCollection = db.GetCollection<usermodel>(collectionName);

            updtbtn.Visible = false;
        }


        //*********************Setting Navigation of Menu Bar*****************************
        private void homebtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = homebtn.Height;
            pnlNav.Top = homebtn.Top;
            pnlNav.Left = homebtn.Left;
            homebtn.BackColor = Color.FromArgb(46, 51, 93);

            this.Hide();
            hm.Show();
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

            this.Hide();
            dbs.Show();
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

            this.Hide();
            sts.Show();
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

        //*********************End of Setting Navigation for Menu Bar*****************************

        // Insert and its logic
        private void insertbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (username.Text != "" & name.Text != "" & usertype.Text != "" & userpass.Text != "" & useremail.Text != "")
                {
                    var usermodel = new usermodel
                    {
                        Username = username.Text,
                        Name = name.Text,
                        User_Type = usertype.Text,
                        Password = userpass.Text,
                        User_Email = useremail.Text,
                    };

                    userCollection.InsertOneAsync(usermodel);
                    this.Alert("Insert Successful!", Form_Alert.enmType.Success);
                }
                else
                {
                    this.Alert("Please Fill All Fields!", Form_Alert.enmType.Warning);
                }
            }
            catch (Exception ex)
            {
                this.Alert("Critical Error! " + ex, Form_Alert.enmType.Error);
            }
            finally
            {
                resetall();
            }
        }

        //Search and its logic
        private void findbtn_Click(object sender, EventArgs e)
        {
            try
            {
                var filterDefinition = Builders<usermodel>.Filter.Eq(a => a.Username, search.Text);
                var projection = Builders<usermodel>.Projection.Exclude("_id");
                var users = userCollection.Find(filterDefinition).Project<usermodel>(projection).FirstOrDefault();

                if (users != null)
                {
                    username.Text = users.Username;
                    username.Enabled = false;
                    name.Text = users.Name;
                    usertype.Text = users.User_Type;
                    userpass.Text = users.Password;
                    useremail.Text = users.User_Email;

                    this.Alert("Record " + users.Username + " Found!", Form_Alert.enmType.Info);
                    updtbtn.Visible = true;
                    insertbtn.Visible = false;
                }
                else
                {
                    this.Alert("User ID " + search.Text + " Not Found!", Form_Alert.enmType.Info);
                }
            }
            catch (Exception ex)
            {
                this.Alert("Critical Error! " + ex, Form_Alert.enmType.Error);
            }
        }


        //Update and its logic
        private void updtbtn_Click(object sender, EventArgs e)
        {
            try
            {
                var filterDefinition = Builders<usermodel>.Filter.Eq(a => a.Username, search.Text);
                var projection = Builders<usermodel>.Projection.Exclude("_id");
                var usersupdt = userCollection.Find(filterDefinition).Project<usermodel>(projection).FirstOrDefault();

                if (usersupdt != null)
                {
                    var filterupdate = Builders<usermodel>.Filter.Eq(a => a.Username, username.Text);
                    var updateDefinition = Builders<usermodel>.Update
                        .Set(a => a.Username, username.Text)
                        .Set(a => a.Name, name.Text)
                        .Set(a => a.User_Type, usertype.Text)
                        .Set(a => a.Password, userpass.Text)
                        .Set(a => a.User_Email, useremail.Text);

                    userCollection.UpdateOneAsync(filterupdate, updateDefinition);

                    this.Alert("Record " + username.Text + " Updated\nSuccessfully!", Form_Alert.enmType.Success);
                }
                else
                {
                    this.Alert("Record Could Not be\n Updated!", Form_Alert.enmType.Error);
                }
            }
            catch (Exception ex)
            {
                this.Alert("Critical Error! " + ex, Form_Alert.enmType.Error);
            }
            finally
            {
                resetall();
            }
        }

        //reset button
        private void reset_Click(object sender, EventArgs e)
        {
            resetall();
            this.Alert("All Fields Reset!", Form_Alert.enmType.Info);
        }

        //Defining Method to reset all entries
        public void resetall()
        {
            search.Text = "";
            username.Enabled = true;
            username.Text = "";
            name.Text = "";
            usertype.Text = "";
            userpass.Text = "";
            useremail.Text = "";

            updtbtn.Visible = false;
            insertbtn.Visible = true;
        }



    }
}
