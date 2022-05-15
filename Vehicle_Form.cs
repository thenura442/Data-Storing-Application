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
    public partial class Vehicle_Form : Form
    {
        string currentuser, currentusertype;

        public string collectionName = "Vehicles";
        public IMongoCollection<vehiclemodel> vehicleCollection;


        //Accessing Alert
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }


        public Vehicle_Form()
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
            vehicleCollection = db.GetCollection<vehiclemodel>(collectionName);

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
                if (vehiclenotxt.Text != "" & typetxt.Text != "" & brandtxt.Text != "" & ownershiptxt.Text != "" & amttxt.Text != "" & drivertxt.Text != "" & statustxt.Text != "" & desctxt.Text != "")
                {
                    var vehiclemodel = new vehiclemodel
                    {
                        Vehicle_No = vehiclenotxt.Text,
                        Vehicle_Type = typetxt.Text,
                        Vehicle_Brand = brandtxt.Text,
                        Vehicle_Ownership = ownershiptxt.Text,
                        Amount = Convert.ToDouble(amttxt.Text),
                        Vehicle_Driver = drivertxt.Text,
                        Vehicle_Status = statustxt.Text,
                        Description = desctxt.Text,
                    };

                    vehicleCollection.InsertOneAsync(vehiclemodel);
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
                var filterDefinition = Builders<vehiclemodel>.Filter.Eq(a => a.Vehicle_No, search.Text);
                var projection = Builders<vehiclemodel>.Projection.Exclude("_id");
                var vehicles = vehicleCollection.Find(filterDefinition).Project<vehiclemodel>(projection).FirstOrDefault();

                if (vehicles != null)
                {
                    vehiclenotxt.Text = vehicles.Vehicle_No;
                    vehiclenotxt.Enabled = false;
                    typetxt.Text = vehicles.Vehicle_Type;
                    brandtxt.Text = vehicles.Vehicle_Brand;
                    ownershiptxt.Text = vehicles.Vehicle_Ownership;
                    amttxt.Text = vehicles.Amount.ToString();
                    drivertxt.Text = vehicles.Vehicle_Driver;
                    statustxt.Text = vehicles.Vehicle_Status;
                    desctxt.Text = vehicles.Description;

                    this.Alert("Vehicle No " + vehicles.Vehicle_No + " Found!", Form_Alert.enmType.Info);
                    updtbtn.Visible = true;
                    insertbtn.Visible = false;
                }
                else
                {
                    this.Alert("Vehicle No " + search.Text + " Not Found!", Form_Alert.enmType.Info);
                }
            }
            catch (Exception ex)
            {
                this.Alert("Critical Error! " + ex, Form_Alert.enmType.Error);
            }
        }


        private void updtbtn_Click(object sender, EventArgs e)
        {
            try
            {
                var filterDefinition = Builders<vehiclemodel>.Filter.Eq(a => a.Vehicle_No, search.Text);
                var projection = Builders<vehiclemodel>.Projection.Exclude("_id");
                var vehiclesupdt = vehicleCollection.Find(filterDefinition).Project<vehiclemodel>(projection).FirstOrDefault();

                if (vehiclesupdt != null)
                {
                    var filterupdate = Builders<vehiclemodel>.Filter.Eq(a => a.Vehicle_No, vehiclenotxt.Text);
                    var updateDefinition = Builders<vehiclemodel>.Update
                        .Set(a => a.Vehicle_No, vehiclenotxt.Text)
                        .Set(a => a.Vehicle_Type, typetxt.Text)
                        .Set(a => a.Vehicle_Brand, brandtxt.Text)
                        .Set(a => a.Vehicle_Ownership, ownershiptxt.Text)
                        .Set(a => a.Amount, Convert.ToDouble(amttxt.Text))
                        .Set(a => a.Vehicle_Driver, drivertxt.Text)
                        .Set(a => a.Vehicle_Status, statustxt.Text)
                        .Set(a => a.Description, desctxt.Text);

                    vehicleCollection.UpdateOneAsync(filterupdate, updateDefinition);

                    this.Alert("Record " + vehiclenotxt.Text + " Updated\nSuccessfully!", Form_Alert.enmType.Success);
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

        //Defining Method to reset all entries
        public void resetall()
        {
            search.Text = "";
            vehiclenotxt.Enabled = true;
            vehiclenotxt.Text = "";
            typetxt.Text = "";
            brandtxt.Text = "";
            ownershiptxt.Text = "";
            amttxt.Text = "";
            drivertxt.Text = "";
            statustxt.Text = "";
            desctxt.Text = "";


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
