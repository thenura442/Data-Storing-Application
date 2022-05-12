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
    public partial class Resources_Form : Form
    {

        // Creating connection and initialising the collection
        string connectionString = "mongodb://localhost:27017";
        public string databaseName = "DataStore";
        public string collectionName = "Resources";
        public IMongoCollection<resourcesmodel> resourcesCollection;

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
        public Resources_Form()
        {
            InitializeComponent();

            //starting the navigation
            pnlNav.Height = formsbtn.Height;
            pnlNav.Top = formsbtn.Top;
            pnlNav.Left = formsbtn.Left;
            formsbtn.BackColor = Color.FromArgb(46, 51, 93);

            ////Setting username and type on the menu
            //usernamelbl.Text = Login.username;
            //usertypelbl.Text = Login.usertype;

            //Initializing conncetion to database
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            resourcesCollection = db.GetCollection<resourcesmodel>(collectionName);

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

        // Insert and its login

        private void insertbtn_Click(object sender, EventArgs e)
        {
            try
            {

                if (invoicenotxt.Text != "" & itemnametxt.Text != "" & typetxt.Text != "" & priceper.Text != "" & quantitytxt.Text != "" & pmttype.Text != "" & pmtstatus.Text != "" & pendingamt.Text != "" & totalamt.Text != "")
                {
                    var resourcesmodel = new resourcesmodel
                    {
                        Invoice_No = invoicenotxt.Text,
                        Item_Name = itemnametxt.Text,
                        Type = typetxt.Text,
                        Priceper = Convert.ToDouble(priceper.Text),
                        Quantity = Convert.ToDouble(quantitytxt.Text),
                        Payment_Type = pmttype.Text,
                        Status = pmtstatus.Text,
                        Pending_Amount = Convert.ToDouble(pendingamt.Text),
                        Total_Amt = Convert.ToDouble(totalamt.Text),
                    };

                    resourcesCollection.InsertOneAsync(resourcesmodel);
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
                var filterDefinition = Builders<resourcesmodel>.Filter.Eq(a => a.Invoice_No, search.Text);
                var projection = Builders<resourcesmodel>.Projection.Exclude("_id");
                var resources = resourcesCollection.Find(filterDefinition).Project<resourcesmodel>(projection).FirstOrDefault();

                if (resources != null)
                {
                    invoicenotxt.Text = resources.Invoice_No;
                    invoicenotxt.Enabled = false;
                    itemnametxt.Text = resources.Item_Name;
                    typetxt.Text = resources.Type;
                    priceper.Text = resources.Priceper.ToString();
                    quantitytxt.Text = resources.Quantity.ToString();
                    pmttype.Text = resources.Payment_Type;
                    pmtstatus.Text = resources.Status;
                    pendingamt.Text = resources.Pending_Amount.ToString();
                    totalamt.Text = resources.Total_Amt.ToString();

                    this.Alert("Record " + resources.Invoice_No + " Found!", Form_Alert.enmType.Info);
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

        //Update and its logic
        private void updtbtn_Click(object sender, EventArgs e)
        {
            try
            {
                var filterDefinition = Builders<resourcesmodel>.Filter.Eq(a => a.Invoice_No, search.Text);
                var projection = Builders<resourcesmodel>.Projection.Exclude("_id");
                var resourcesupdt = resourcesCollection.Find(filterDefinition).Project<resourcesmodel>(projection).FirstOrDefault();

                if (resourcesupdt != null)
                {
                    var filterupdate = Builders<resourcesmodel>.Filter.Eq(a => a.Invoice_No, invoicenotxt.Text);
                    var updateDefinition = Builders<resourcesmodel>.Update
                        .Set(a => a.Invoice_No, invoicenotxt.Text)
                        .Set(a => a.Item_Name, itemnametxt.Text)
                        .Set(a => a.Type, typetxt.Text)
                        .Set(a => a.Priceper, Convert.ToDouble(priceper.Text))
                        .Set(a => a.Quantity, Convert.ToDouble(quantitytxt.Text))
                        .Set(a => a.Payment_Type, pmttype.Text)
                        .Set(a => a.Status, pmtstatus.Text)
                        .Set(a => a.Pending_Amount, Convert.ToDouble(pendingamt.Text))
                        .Set(a => a.Total_Amt, Convert.ToDouble(totalamt.Text));

                    resourcesCollection.UpdateOneAsync(filterupdate, updateDefinition);

                    this.Alert("Record " + invoicenotxt.Text + " Updated\nSuccessfully!", Form_Alert.enmType.Success);
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
            invoicenotxt.Enabled = true;
            invoicenotxt.Text = "";
            itemnametxt.Text = "";
            typetxt.Text = "";
            priceper.Text = "";
            quantitytxt.Text = "";
            pmttype.Text = "";
            pmtstatus.Text = "";
            pendingamt.Text = "";
            totalamt.Text = "";

            updtbtn.Visible = false;
            insertbtn.Visible = true;
        }
    }
}
