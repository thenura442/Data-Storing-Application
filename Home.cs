using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Data_Storing_App.Models;
using MongoDB.Driver;

namespace Data_Storing_App
{
    public partial class Home : Form
    {
        string currentuser, currentusertype;

        // Creating connection and initialising the collection
        public string collectionName = "Orders";
        public IMongoCollection<ordermodel> orderCollection;

        
        //Accessing Alert
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }



        public Home()
        {
            InitializeComponent();

            //starting the navigation
            pnlNav.Height = homebtn.Height;
            pnlNav.Top = homebtn.Top;
            pnlNav.Left = homebtn.Left;
            homebtn.BackColor = Color.FromArgb(46, 51, 93);

            //Initializing conncetion to database
            var client = new MongoClient(staticmethods.getconnection());
            var db = client.GetDatabase(staticmethods.getdatabase());
            orderCollection = db.GetCollection<ordermodel>(collectionName);

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
                DateTime datenow = DateTime.Now;
                string date = datenow.ToString("MM/dd/yyyy");
                string month = datenow.Month.ToString();


                if (orderid.Text != "" & material.Text != "" & initialweight.Text != "" & loadedweight.Text != "" & quantity.Text != "" & lorryno.Text != "" & desc.Text != "" & priceton.Text != "" & pmttype.Text != "" & pmtstatus.Text != "" & pendingamt.Text != "" & totalamt.Text != "")
                {
                    var ordermodel = new ordermodel
                    {
                        Order_ID = orderid.Text,
                        Material = material.Text,
                        Intial_Weight = Convert.ToDouble(initialweight.Text),
                        Loaded_Weight = Convert.ToDouble(loadedweight.Text),
                        Quantity = Convert.ToDouble(quantity.Text),
                        Lorry_No = lorryno.Text,
                        Description = desc.Text,
                        Date = date,
                        Month = month,
                        Perton = Convert.ToDouble(priceton.Text),
                        Type = pmttype.Text,
                        Status = pmtstatus.Text,
                        Pending_Amt = Convert.ToDouble(pendingamt.Text),
                        Total_Amt = Convert.ToDouble(totalamt.Text),
                    };

                    orderCollection.InsertOneAsync(ordermodel);
                    this.Alert("Insert Successful!", Form_Alert.enmType.Success);
                }
                else
                {
                    this.Alert("Please Fill All Fields!", Form_Alert.enmType.Warning);
                }
            }
            catch (Exception ex)
            {
                this.Alert("Critical Error! "+ex, Form_Alert.enmType.Error);
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
                var filterDefinition = Builders<ordermodel>.Filter.Eq(a => a.Order_ID, search.Text);
                var projection = Builders<ordermodel>.Projection.Exclude("_id").Exclude("Date");
                var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).FirstOrDefault();

                if (orders != null)
                {
                    orderid.Text = orders.Order_ID;
                    orderid.Enabled = false;
                    material.Text = orders.Material;
                    initialweight.Text = orders.Intial_Weight.ToString();
                    loadedweight.Text = orders.Loaded_Weight.ToString();
                    quantity.Text = orders.Quantity.ToString();
                    lorryno.Text = orders.Lorry_No;
                    desc.Text = orders.Description;
                    priceton.Text = orders.Perton.ToString();
                    pmttype.Text = orders.Type;
                    pmtstatus.Text = orders.Status;
                    pendingamt.Text = orders.Pending_Amt.ToString();
                    totalamt.Text = orders.Total_Amt.ToString();

                    this.Alert("Record "+orders.Order_ID+" Found!", Form_Alert.enmType.Info);
                    updtbtn.Visible = true;
                    insertbtn.Visible = false;
                }
                else
                {
                    this.Alert("OrderID "+search.Text+" Not Found!", Form_Alert.enmType.Info);
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
                var filterDefinition = Builders<ordermodel>.Filter.Eq(a => a.Order_ID, search.Text);
                var projection = Builders<ordermodel>.Projection.Exclude("_id").Exclude("Date");
                var ordersupdt = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).FirstOrDefault();

                if (ordersupdt != null)
                {
                    var filterupdate = Builders<ordermodel>.Filter.Eq(a => a.Order_ID, orderid.Text);
                    var updateDefinition = Builders<ordermodel>.Update
                        .Set(a => a.Material, material.Text)
                        .Set(a => a.Intial_Weight, Convert.ToDouble(initialweight.Text))
                        .Set(a => a.Loaded_Weight, Convert.ToDouble(loadedweight.Text))
                        .Set(a => a.Quantity, Convert.ToDouble(quantity.Text))
                        .Set(a => a.Lorry_No, lorryno.Text)
                        .Set(a => a.Description, desc.Text)
                        .Set(a => a.Perton, Convert.ToDouble(priceton.Text))
                        .Set(a => a.Type, pmttype.Text)
                        .Set(a => a.Status, pmtstatus.Text)
                        .Set(a => a.Pending_Amt, Convert.ToDouble(pendingamt.Text))
                        .Set(a => a.Total_Amt, Convert.ToDouble(totalamt.Text));

                    orderCollection.UpdateOneAsync(filterupdate, updateDefinition);

                    this.Alert("Record "+orderid.Text+" Updated\nSuccessfully!", Form_Alert.enmType.Success);
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
            orderid.Enabled = true;
            orderid.Text = "";
            material.Text = "";
            initialweight.Text = "";
            loadedweight.Text = "";
            quantity.Text = "";
            lorryno.Text = "";
            desc.Text = "";
            priceton.Text = "";
            pmttype.Text = "";
            pmtstatus.Text = "";
            pendingamt.Text = "";
            totalamt.Text = "";

            updtbtn.Visible=false;
            insertbtn.Visible = true;
        }
    }
}
