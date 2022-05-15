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
    public partial class Salary_Form : Form
    {
        string currentuser, currentusertype;

        public string collectionName = "Salary";
        public IMongoCollection<salarymodel> salaryCollection;


        //Accessing Alert
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        public Salary_Form()
        {
            InitializeComponent();

            //starting the navigation
            pnlNav.Height = formsbtn.Height;
            pnlNav.Top = formsbtn.Top;
            pnlNav.Left = formsbtn.Left;
            formsbtn.BackColor = Color.FromArgb(46, 51, 93);

            //Initializing conncetion to database
            var client = new MongoClient(staticmethods.getconnection());
            var db = client.GetDatabase(staticmethods.getdatabase());
            salaryCollection = db.GetCollection<salarymodel>(collectionName);

            currentuser = staticmethods.getuser();
            currentusertype = staticmethods.gettype();

            usernamelbl.Text = currentuser;
            usertypelbl.Text = currentusertype;

            updtbtn.Visible = false;
        }


        //*********************Setting Navigation of Menu Bar*****************************

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

        //*********************End of Setting Navigation for Menu Bar*****************************

        // Insert and its logic
        private void insertbtn_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = Convert.ToDateTime(datetxt.Text);
                string month = date.Month.ToString();
                string dateset = date.ToString("MM/dd/yyyy");

                if (nametxt.Text != "" & datetxt.Text != "" & basicsalarytxt.Text != "" & bonustxt.Text != "" & deductionstxt.Text != "" & totalsalarytxt.Text != "")
                {
                    var salarymodel = new salarymodel
                    {
                        Name = nametxt.Text,
                        Date = dateset,
                        Month = month,
                        Basic_Salary = Convert.ToDouble(basicsalarytxt.Text),
                        Bonus = Convert.ToDouble(bonustxt.Text),
                        Deductions = Convert.ToDouble(deductionstxt.Text),
                        Total_Salary = Convert.ToDouble(totalsalarytxt.Text),
                    };

                    salaryCollection.InsertOneAsync(salarymodel);
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
                var filterDefinition = Builders<salarymodel>.Filter.Eq(a => a.Name, search.Text);
                var projection = Builders<salarymodel>.Projection.Exclude("_id").Exclude("Month");
                var salary = salaryCollection.Find(filterDefinition).Project<salarymodel>(projection).FirstOrDefault();

                if (salary != null)
                {
                    nametxt.Text = salary.Name;
                    nametxt.Enabled = false;
                    datetxt.Text = salary.Date;
                    basicsalarytxt.Text = salary.Basic_Salary.ToString();
                    bonustxt.Text = salary.Bonus.ToString();
                    deductionstxt.Text = salary.Deductions.ToString();
                    totalsalarytxt.Text = salary.Total_Salary.ToString();

                    this.Alert("Record " + salary.Name + " Found!", Form_Alert.enmType.Info);
                    updtbtn.Visible = true;
                    insertbtn.Visible = false;
                }
                else
                {
                    this.Alert("OrderID " + search.Text + " Not Found!", Form_Alert.enmType.Info);
                }
            }
            catch (Exception ex)
            {
                this.Alert("Critical Error! " + ex, Form_Alert.enmType.Error);
            }
        }

        //update and its logic
        private void updtbtn_Click(object sender, EventArgs e)
        {
            try
            {
                var filterDefinition = Builders<salarymodel>.Filter.Eq(a => a.Name, search.Text);
                var projection = Builders<salarymodel>.Projection.Exclude("_id").Exclude("Month");
                var salaryupdt = salaryCollection.Find(filterDefinition).Project<salarymodel>(projection).FirstOrDefault();

                if (salaryupdt != null)
                {
                    var d = Convert.ToDateTime(datetxt.Text);
                    string month = d.Month.ToString();
                    var filterupdate = Builders<salarymodel>.Filter.Eq(a => a.Name, nametxt.Text);
                    var updateDefinition = Builders<salarymodel>.Update
                        .Set(a => a.Name, nametxt.Text)
                        .Set(a => a.Date, datetxt.Text)
                        .Set(a => a.Month, month)
                        .Set(a => a.Basic_Salary, Convert.ToDouble(basicsalarytxt.Text))
                        .Set(a => a.Bonus, Convert.ToDouble(bonustxt.Text))
                        .Set(a => a.Deductions, Convert.ToDouble(deductionstxt.Text))
                        .Set(a => a.Total_Salary, Convert.ToDouble(totalsalarytxt.Text));

                    salaryCollection.UpdateOneAsync(filterupdate, updateDefinition);

                    this.Alert("Record " + nametxt.Text + " Updated\nSuccessfully!", Form_Alert.enmType.Success);
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

        private void reset_Click(object sender, EventArgs e)
        {
            resetall();
            this.Alert("All Fields Reset!", Form_Alert.enmType.Info);
        }

        //Defining Method to reset all entries
        public void resetall()
        {
            search.Text = "";
            nametxt.Enabled = true;
            nametxt.Text = "";
            datetxt.Text = "";
            basicsalarytxt.Text = "";
            bonustxt.Text = "";
            deductionstxt.Text = "";
            totalsalarytxt.Text = "";

            updtbtn.Visible = false;
            insertbtn.Visible = true;
        }
    }
}
