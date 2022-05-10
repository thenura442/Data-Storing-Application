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
    public partial class Order_Database : Form
    {

        // Creating connection and initialising the collection
        string connectionString = "mongodb://localhost:27017";
        public string databaseName = "DataStore";
        public string collectionName = "Orders";
        public IMongoCollection<ordermodel> orderCollection;


        //Accessing Alert
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        public Order_Database()
        {
            InitializeComponent();

            //starting the navigation
            pnlNav.Height = databasebtn.Height;
            pnlNav.Top = databasebtn.Top;
            pnlNav.Left = databasebtn.Left;
            databasebtn.BackColor = Color.FromArgb(46, 51, 93);

            //Initializing conncetion to database
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            orderCollection = db.GetCollection<ordermodel>(collectionName);

            //Setting some buttons to be invisible
            invisible();

            LoadUserData(); 
        }


        //*********************Setting Navigation of Menu Bar*****************************

        private void homebtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = homebtn.Height;
            pnlNav2.Top = homebtn.Top;
            pnlNav2.Left = homebtn.Left;
            homebtn.BackColor = Color.FromArgb(46, 51, 93);
        }

        private void formsbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = formsbtn.Height;
            pnlNav2.Top = formsbtn.Top;
            pnlNav2.Left = formsbtn.Left;
            formsbtn.BackColor = Color.FromArgb(46, 51, 93);
        }

        private void databasebtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = databasebtn.Height;
            pnlNav2.Top = databasebtn.Top;
            pnlNav2.Left = databasebtn.Left;
            databasebtn.BackColor = Color.FromArgb(46, 51, 93);
        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = reminderbtn.Height;
            pnlNav2.Top = reminderbtn.Top;
            pnlNav2.Left = reminderbtn.Left;
            reminderbtn.BackColor = Color.FromArgb(46, 51, 93);
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = logoutbtn.Height;
            pnlNav2.Top = logoutbtn.Top;
            pnlNav2.Left = logoutbtn.Left;
            logoutbtn.BackColor = Color.FromArgb(46, 51, 93);
        }

        private void settingsbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = settingsbtn.Height;
            pnlNav2.Top = settingsbtn.Top;
            pnlNav2.Left = settingsbtn.Left;
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
        //*********************End of Setting Navigation for Menu Bar*****************************

        private void deletebutton1_Click(object sender, EventArgs e)
        {
            dltbtn.Enabled = false;
            txtbox.Visible = true;
            dltonebtn.Visible = true;
            dltmanybtn.Visible = true;
            cancel.Visible = true;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            invisible();
        }

        //delete a record with that id
        private void dltonebtn_Click(object sender, EventArgs e)
        {
            try
            {
                var filterDefinition = Builders<ordermodel>.Filter.Eq(a => a.Order_ID, txtbox.Text);
                var projection = Builders<ordermodel>.Projection.Exclude("_id");
                var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).FirstOrDefault();

                if (orders != null)
                {
                    orderCollection.DeleteOneAsync(filterDefinition);

                    this.Alert("Record " + orders.Order_ID + " Deleted!", Form_Alert.enmType.Success);
                    LoadUserData();
                }
                else
                {
                    this.Alert("Record " + txtbox.Text + " Not Found!", Form_Alert.enmType.Warning);
                }
            }
            catch (Exception ex)
            {
                this.Alert("Error!", Form_Alert.enmType.Error);
            }
            finally
            {
                invisible();
            }
        }

        private void dltmanybtn_Click(object sender, EventArgs e)
        {
            try
            {
                var filterDefinition = Builders<ordermodel>.Filter.Eq(a => a.Order_ID, searchtxtbox.Text);
                var projection = Builders<ordermodel>.Projection.Exclude("_id");
                var ordersmany = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).FirstOrDefault();

                if(ordersmany != null)
                {
                    orderCollection.DeleteManyAsync(filterDefinition);
                    this.Alert("Records with " + ordersmany.Order_ID + " Deleted!", Form_Alert.enmType.Success);
                    LoadUserData();
                }
                else
                {
                    this.Alert("Record " + txtbox.Text + " Not Found!", Form_Alert.enmType.Warning);
                }
            }
            catch(Exception ex)
            {
                this.Alert("Error!", Form_Alert.enmType.Error);
            }
            invisible();
        }

        public void LoadUserData()
        {
            var filterDefinition = Builders<ordermodel>.Filter.Empty;
            var projection = Builders<ordermodel>.Projection.Exclude("_id");
            var users = orderCollection.Find(filterDefinition).Project<ordermodel>(projection)
                //.SortByDescending(x => x.Date)
                .ToList();
            datagridview1.DataSource = users;
        }

        //method Setting buttons to be invisible
        public void invisible()
        {
            dltbtn.Enabled = true;
            txtbox.Visible = false;
            dltonebtn.Visible = false;
            dltmanybtn.Visible = false;
            cancel.Visible = false;
            txtbox.Text = "";
        }

        private void cancel_Click_1(object sender, EventArgs e)
        {
            invisible();
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void datagridview1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
