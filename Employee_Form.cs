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
    public partial class Employee_Form : Form
    {

        string currentuser, currentusertype;


        public string collectionName = "Employees";
        public IMongoCollection<employeemodel> employeeCollection;



        //Accessing Alert
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }


        public Employee_Form()
        {
            InitializeComponent();

            //starting the navigation
            pnlNav.Height = formsbtn.Height;
            pnlNav.Top = formsbtn.Top;
            pnlNav.Left = formsbtn.Left;
            formsbtn.BackColor = Color.FromArgb(46, 51, 93);

            currentuser = staticmethods.getuser();
            currentusertype = staticmethods.gettype();

            usernamelbl.Text = currentuser;
            usertypelbl.Text = currentusertype;

            //Initializing conncetion to database
            var client = new MongoClient(staticmethods.getconnection());
            var db = client.GetDatabase(staticmethods.getdatabase());
            employeeCollection = db.GetCollection<employeemodel>(collectionName);

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

                if (nictxt.Text != "" & fullnametxt.Text != "" & typetxt.Text != "" & jobtxt.Text != "" & addresstxt.Text != "" & contacttxt.Text != "")
                {
                    var employeemodel = new employeemodel
                    {
                        NIC = nictxt.Text,
                        Full_Name = fullnametxt.Text,
                        Type =  typetxt.Text,
                        Job_Specific = jobtxt.Text,
                        Address = addresstxt.Text,
                        Contact_Number = contacttxt.Text,
                    };

                    employeeCollection.InsertOneAsync(employeemodel);
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
                var filterDefinition = Builders<employeemodel>.Filter.Eq(a => a.NIC, search.Text);
                var projection = Builders<employeemodel>.Projection.Exclude("_id");
                var employees = employeeCollection.Find(filterDefinition).Project<employeemodel>(projection).FirstOrDefault();

                if (employees != null)
                {
                    nictxt.Text = employees.NIC;
                    nictxt.Enabled = false;
                    fullnametxt.Text = employees.Full_Name;
                    typetxt.Text = employees.Type;
                    jobtxt.Text = employees.Job_Specific;
                    addresstxt.Text = employees.Address;
                    contacttxt.Text = employees.Contact_Number;

                    this.Alert("Record " + employees.NIC + " Found!", Form_Alert.enmType.Info);
                    updtbtn.Visible = true;
                    insertbtn.Visible = false;
                }
                else
                {
                    this.Alert("NIC " + search.Text + " Not Found!", Form_Alert.enmType.Info);
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
                var filterDefinition = Builders<employeemodel>.Filter.Eq(a => a.NIC, search.Text);
                var projection = Builders<employeemodel>.Projection.Exclude("_id");
                var employeesupdt = employeeCollection.Find(filterDefinition).Project<employeemodel>(projection).FirstOrDefault();

                if (employeesupdt != null)
                {
                    var filterupdate = Builders<employeemodel>.Filter.Eq(a => a.NIC, nictxt.Text);
                    var updateDefinition = Builders<employeemodel>.Update
                        .Set(a => a.NIC, nictxt.Text)
                        .Set(a => a.Full_Name, fullnametxt.Text)
                        .Set(a => a.Type, typetxt.Text)
                        .Set(a => a.Job_Specific, jobtxt.Text)
                        .Set(a => a.Address, addresstxt.Text)
                        .Set(a => a.Contact_Number, contacttxt.Text);

                    employeeCollection.UpdateOneAsync(filterupdate, updateDefinition);

                    this.Alert("Record " + nictxt.Text + " Updated\nSuccessfully!", Form_Alert.enmType.Success);
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


        //defining method to reset all
        public void resetall()
        {
            search.Text = "";
            nictxt.Enabled = true;
            nictxt.Text = "";
            fullnametxt.Text = "";
            typetxt.Text = "";
            jobtxt.Text = "";
            addresstxt.Text = "";
            contacttxt.Text = "";

            updtbtn.Visible = false;
            insertbtn.Visible = true;
        }

        private void reset_Click(object sender, EventArgs e)
        {
            resetall();
            this.Alert("All Fields Reset!", Form_Alert.enmType.Info);
        }
    }
}
