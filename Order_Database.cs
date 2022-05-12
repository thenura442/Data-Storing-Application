using Data_Storing_App.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

        //setting bridges
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

        public Order_Database()
        {
            InitializeComponent();

            //starting the navigation
            pnlNav2.Height = databasebtn.Height;
            pnlNav2.Top = databasebtn.Top;
            pnlNav2.Left = databasebtn.Left;
            databasebtn.BackColor = Color.FromArgb(46, 51, 93);

            //Initializing conncetion to database
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            orderCollection = db.GetCollection<ordermodel>(collectionName);

            //Setting some buttons to be invisible
            invisible();

            LoadUserData();

            checkreset();
        }


        //*********************Setting Navigation of Menu Bar*****************************

        private void homebtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = homebtn.Height;
            pnlNav2.Top = homebtn.Top;
            pnlNav2.Left = homebtn.Left;
            homebtn.BackColor = Color.FromArgb(46, 51, 93);

            this.Hide();
            hm.Show();
        }

        private void formsbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = formsbtn.Height;
            pnlNav2.Top = formsbtn.Top;
            pnlNav2.Left = formsbtn.Left;
            formsbtn.BackColor = Color.FromArgb(46, 51, 93);

            this.Hide();
            fm.Show();
        }

        private void databasebtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = databasebtn.Height;
            pnlNav2.Top = databasebtn.Top;
            pnlNav2.Left = databasebtn.Left;
            databasebtn.BackColor = Color.FromArgb(46, 51, 93);

            this.Hide();
            dbs.Show();
        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = reminderbtn.Height;
            pnlNav2.Top = reminderbtn.Top;
            pnlNav2.Left = reminderbtn.Left;
            reminderbtn.BackColor = Color.FromArgb(46, 51, 93);

            this.Hide();
            rms.Show();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = logoutbtn.Height;
            pnlNav2.Top = logoutbtn.Top;
            pnlNav2.Left = logoutbtn.Left;
            logoutbtn.BackColor = Color.FromArgb(46, 51, 93);

            this.Hide();
            lg.Show();
        }

        private void settingsbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = settingsbtn.Height;
            pnlNav2.Top = settingsbtn.Top;
            pnlNav2.Left = settingsbtn.Left;
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

        private void deletebutton1_Click(object sender, EventArgs e)
        {
            dltbtn.Enabled = false;
            txtbox.Visible = true;
            dltonebtn.Visible = true;
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
                this.Alert("Error! -"+ex, Form_Alert.enmType.Error);
            }
            finally
            {
                invisible();
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime datewanted = Convert.ToDateTime(datechktxt.Text);
                string orderid = searchtxtbox.Text;
                string material = materialchktxt.Text;
                string monthset = monthchktxt.Text;
                string date = datewanted.ToString("MM/dd/yyyy");
                string status = pmtchktxt.Text;
                string month;

                switch (monthset)
                {
                    case "January":
                        month = "1";
                        break;
                    case "February":
                        month = "2";
                        break;
                    case "March":
                        month = "3";
                        break;
                    case "April":
                        month = "4";
                        break;
                    case "May":
                        month = "5";
                        break;
                    case "June":
                        month = "6";
                        break;
                    case "July":
                        month = "7";
                        break;
                    case "August":
                        month = "8";
                        break;
                    case "September":
                        month = "9";
                        break;
                    case "October":
                        month = "10";
                        break;
                    case "November":
                        month = "11";
                        break;
                    case "December":
                        month = "12";
                        break;

                    default:
                        month = "";
                        break;
                }

                var filterid = Builders<ordermodel>.Filter.Eq(a => a.Order_ID, orderid);
                var filterdate = Builders<ordermodel>.Filter.Eq(a => a.Date, date);
                var filtermonth = Builders<ordermodel>.Filter.Eq(a => a.Month, month);
                var filterpmt = Builders<ordermodel>.Filter.Eq(a => a.Status, status);
                var filtermaterial = Builders<ordermodel>.Filter.Eq(a => a.Material, material);

                if (searchtxtbox.Text == "")
                {
                    // Filtering with month only
                    if ((datechk.Checked == false) & (materialchk.Checked == false) & (monthchk.Checked == true) & (pmtchk.Checked == false) & (orderid == ""))
                    {
                        var filterDefinition = filtermonth;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Month Only!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }

                    // Filtering with material only
                    else if ((datechk.Checked == false) & (materialchk.Checked == true) & (monthchk.Checked == false) & (pmtchk.Checked == false) & (orderid == ""))
                    {
                        var filterDefinition = filtermaterial;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Material Only!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }

                    // Filtering with status only
                    else if ((datechk.Checked == false) & (materialchk.Checked == false) & (monthchk.Checked == false) & (pmtchk.Checked == true) & (orderid == ""))
                    {
                        var filterDefinition = filterpmt;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Payment Only!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }


                    // Filtering with date only
                    else if ((datechk.Checked == true) & (materialchk.Checked == false) & (monthchk.Checked == false) & (pmtchk.Checked == false) & (orderid == ""))
                    {
                        var filterDefinition = filterdate;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Date Only!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }

                    // Filtering with Material and month
                    else if ((datechk.Checked == false) & (materialchk.Checked == true) & (monthchk.Checked == true) & (pmtchk.Checked == false) & (orderid == ""))
                    {
                        var filterDefinition = filtermaterial & filtermonth;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Records Found with month!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }

                    // Filtering with Material and date
                    else if ((datechk.Checked == true) & (materialchk.Checked == true) & (monthchk.Checked == false) & (pmtchk.Checked == false) & (orderid == ""))
                    {
                        var filterDefinition = filtermaterial & filterdate;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Records Found with month!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }

                    // Filtering with Material and payment_status
                    else if ((datechk.Checked == false) & (materialchk.Checked == true) & (monthchk.Checked == false) & (pmtchk.Checked == true) & (orderid == ""))
                    {
                        var filterDefinition = filtermaterial & filterpmt;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Records Found with month!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }

                    // Filtering with payment_status and month
                    else if ((datechk.Checked == false) & (materialchk.Checked == false) & (monthchk.Checked == true) & (pmtchk.Checked == true) & (orderid == ""))
                    {
                        var filterDefinition = filtermonth & filterpmt;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Records Found with month!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }


                    // Filtering with payment_status and date
                    else if ((datechk.Checked == true) & (materialchk.Checked == false) & (monthchk.Checked == false) & (pmtchk.Checked == true) & (orderid == ""))
                    {
                        var filterDefinition = filterdate & filterpmt;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Records Found with month!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }

                    // Filtering with payment_status and date and Material
                    else if ((datechk.Checked == true) & (materialchk.Checked == true) & (monthchk.Checked == false) & (pmtchk.Checked == true) & (orderid == ""))
                    {
                        var filterDefinition = filterdate & filterpmt & filtermaterial;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Records Found with month!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }

                    // Filtering with payment_status and month and Material
                    else if ((datechk.Checked == false) & (materialchk.Checked == true) & (monthchk.Checked == true) & (pmtchk.Checked == true) & (orderid == ""))
                    {
                        var filterDefinition = filtermonth & filterpmt & filtermaterial;
                        var projection = Builders<ordermodel>.Projection.Exclude("_id");
                        var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                        if (orders.Count > 0)
                        {
                            datagridview1.DataSource = orders;
                            this.Alert("Records Found with month!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }
                }
                else
                {
                    if(searchtxtbox.Text != "")
                    {
                        // Filtering with OrderID only
                        if ((datechk.Checked == false) & (materialchk.Checked == false) & (monthchk.Checked == false) & (pmtchk.Checked == false) & (orderid != ""))
                        {
                            var filterDefinition = filterid;
                            var projection = Builders<ordermodel>.Projection.Exclude("_id");
                            var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection).ToList();

                            if (orders.Count > 0)
                            {
                                datagridview1.DataSource = orders;
                                this.Alert("Records Found!", Form_Alert.enmType.Info);

                            }
                            else
                            {
                                this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                                LoadUserData();
                            }
                        }
                        else
                        {
                            this.Alert("Please Choose a Meaningful\nFilter Combination!", Form_Alert.enmType.Warning);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                this.Alert("Error - "+ex, Form_Alert.enmType.Error);
            }
        }


        // method to reset datagrid view
        public void LoadUserData()
        {
            var filterDefinition = Builders<ordermodel>.Filter.Empty;
            var projection = Builders<ordermodel>.Projection.Exclude("_id");
            var orders = orderCollection.Find(filterDefinition).Project<ordermodel>(projection)
                .ToList();
            datagridview1.DataSource = orders;
        }

        //method Setting buttons to be invisible
        public void invisible()
        {
            dltbtn.Enabled = true;
            txtbox.Visible = false;
            dltonebtn.Visible = false;
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
            searchtxtbox.Text = "";
            checkreset();
        }

        private void datagridview1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        //resetting filter checkboxes
        public void checkreset()
        {
            datechk.Checked = false;
            monthchk.Checked = false;
            materialchk.Checked = false;
            pmtchk.Checked = false;
        }

        // CheckedChanged Event to enable visibility of respectiv textboxes
        private void datechk_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if(datechk.Checked)
            {
                datechktxt.Visible = true;
                monthchk.Checked = false;
            }
            else
            {
                datechktxt.Visible = false;
            }
        }

        private void monthchk_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (monthchk.Checked)
            {
                monthchktxt.Visible = true;
                datechk.Checked = false;
            }
            else
            {
                monthchktxt.Visible = false;
            }
        }

        private void materialchk_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (materialchk.Checked)
            {
                materialchktxt.Visible = true;
            }
            else
            {
                materialchktxt.Visible = false;
            }
        }

        private void pmtchk_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (pmtchk.Checked)
            {
                pmtchktxt.Visible = true;
            }
            else
            {
                pmtchktxt.Visible = false;
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int i = 0;
                int j = 0;
                var table = "Orders";

                var datenow = DateTime.Now;
                string date = datenow.ToString("MM/dd/yyyy");



                Microsoft.Office.Interop.Excel.Range range = xlWorkSheet.get_Range("A1:I1", Type.Missing);
                range.Merge(Missing.Value);
                //Add Name to Range
                range.Name = "Company_Name_Range";
                range.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //Put data in range cells
                foreach (Microsoft.Office.Interop.Excel.Range cell in range.Cells)
                {
                    cell.set_Value(Missing.Value, "Art Enterprises "+ table +" Table As At "+ date +".");
                    range.Style.Font.Bold = true;
                    cell.Font.Size = 24;
                }

                for (i = 1; i < datagridview1.ColumnCount + 1; i++)
                {
                    xlWorkSheet.Cells[3, i] = datagridview1.Columns[i - 1].HeaderText;
                    xlWorkSheet.get_Range("A3", "P3").Cells.Font.Bold = true;
                    xlWorkSheet.get_Range("A3", "P3").Cells.Font.Size = 18;
                    xlWorkSheet.Columns.AutoFit();
                    xlWorkSheet.Rows.AutoFit();
                }

                for (i = 0; i < datagridview1.RowCount; i++)
                {
                    for (j = 0; j < datagridview1.ColumnCount; j++)
                    {
                        DataGridViewCell cell = datagridview1[j, i];
                        xlWorkSheet.Cells[i + 4, j + 1] = cell.Value;
                        xlWorkSheet.Cells.Style.Font.Size = 14;
                        xlWorkSheet.Cells.Style.Font.Bold = false;
                        xlWorkSheet.Cells.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        xlWorkSheet.Cells.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    }
                }
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                this.Alert("Data Exported Successfully!", Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                this.Alert("Error - " + ex, Form_Alert.enmType.Error);
            }
        }

        private void backbutton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            dbs.Show();
        }
    }
}
