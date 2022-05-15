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
    public partial class Employee_Database : Form
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

        public Employee_Database()
        {
            InitializeComponent();

            //starting the navigation
            pnlNav2.Height = databasebtn.Height;
            pnlNav2.Top = databasebtn.Top;
            pnlNav2.Left = databasebtn.Left;
            databasebtn.BackColor = Color.FromArgb(46, 51, 93);

            //Initializing conncetion to database
            var client = new MongoClient(staticmethods.getconnection());
            var db = client.GetDatabase(staticmethods.getdatabase());
            employeeCollection = db.GetCollection<employeemodel>(collectionName);


            currentuser = staticmethods.getuser();
            currentusertype = staticmethods.gettype();

            usernamelbl.Text = currentuser;
            usertypelbl.Text = currentusertype;


            //Setting some buttons to be invisible
            invisible();

            LoadUserData();

            checkreset();
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = homebtn.Height;
            pnlNav2.Top = homebtn.Top;
            pnlNav2.Left = homebtn.Left;
            homebtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.homeshow();
            this.Hide();
        }

        private void formsbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = formsbtn.Height;
            pnlNav2.Top = formsbtn.Top;
            pnlNav2.Left = formsbtn.Left;
            formsbtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.formsshow();
            this.Hide();
        }

        private void databasebtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = databasebtn.Height;
            pnlNav2.Top = databasebtn.Top;
            pnlNav2.Left = databasebtn.Left;
            databasebtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.databaseshow();
            this.Hide();
        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = reminderbtn.Height;
            pnlNav2.Top = reminderbtn.Top;
            pnlNav2.Left = reminderbtn.Left;
            reminderbtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.remindershow();
            this.Hide();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = logoutbtn.Height;
            pnlNav2.Top = logoutbtn.Top;
            pnlNav2.Left = logoutbtn.Left;
            logoutbtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.logoutshow();
            this.Hide();
        }

        private void settingsbtn_Click(object sender, EventArgs e)
        {
            pnlNav2.Height = settingsbtn.Height;
            pnlNav2.Top = settingsbtn.Top;
            pnlNav2.Left = settingsbtn.Left;
            settingsbtn.BackColor = Color.FromArgb(46, 51, 93);

            staticmethods.settingsshow();
            this.Hide();
        }


        //*********************Setting Navigation of Menu Bar*****************************

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
        private void dltbtn_Click(object sender, EventArgs e)
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

        //delet a record with a id
        private void dltonebtn_Click(object sender, EventArgs e)
        {
            try
            {
                var filterDefinition = Builders<employeemodel>.Filter.Eq(a => a.NIC, txtbox.Text);
                var projection = Builders<employeemodel>.Projection.Exclude("_id");
                var employees = employeeCollection.Find(filterDefinition).Project<employeemodel>(projection).FirstOrDefault();

                if (employees != null)
                {
                    employeeCollection.DeleteOneAsync(filterDefinition);

                    this.Alert("Record " + employees.NIC + " Deleted!", Form_Alert.enmType.Success);
                    LoadUserData();
                }
                else
                {
                    this.Alert("Record " + txtbox.Text + " Not Found!", Form_Alert.enmType.Warning);
                }
            }
            catch (Exception ex)
            {
                this.Alert("Error! -" + ex, Form_Alert.enmType.Error);
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
                string nic = searchtxtbox.Text;
                string type = typechktxt.Text;
                string job = jobchktxt.Text;

                var filternic = Builders<employeemodel>.Filter.Eq(a => a.NIC, nic);
                var filtertype = Builders<employeemodel>.Filter.Eq(a => a.Type , type);
                var filterjob = Builders<employeemodel>.Filter.Eq(a => a.Job_Specific, job);

                if (searchtxtbox.Text == "")
                {
                    // Filtering with job only
                    if ((jobchk.Checked == true) & (typechk.Checked == false) & (nic == ""))
                    {
                        var filterDefinition = filterjob;
                        var projection = Builders<employeemodel>.Projection.Exclude("_id");
                        var employees = employeeCollection.Find(filterDefinition).Project<employeemodel>(projection).ToList();

                        if (employees.Count > 0)
                        {
                            datagridview1.DataSource = employees;
                            this.Alert("Records Found!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }

                    // Filtering with Type only
                    else if ((jobchk.Checked == false) & (typechk.Checked == true) & (nic == ""))
                    {
                        var filterDefinition = filtertype;
                        var projection = Builders<employeemodel>.Projection.Exclude("_id");
                        var employees = employeeCollection.Find(filterDefinition).Project<employeemodel>(projection).ToList();

                        if (employees.Count > 0)
                        {
                            datagridview1.DataSource = employees;
                            this.Alert("Records Found!", Form_Alert.enmType.Info);

                        }
                        else
                        {
                            this.Alert("No Records Found!", Form_Alert.enmType.Warning);
                            LoadUserData();
                        }
                    }

                    //Filtering with Type and job
                    else if ((jobchk.Checked == true) & (typechk.Checked == true) & (nic == ""))
                    {
                        var filterDefinition = filtertype & filterjob;
                        var projection = Builders<employeemodel>.Projection.Exclude("_id");
                        var employees = employeeCollection.Find(filterDefinition).Project<employeemodel>(projection).ToList();

                        if (employees.Count > 0)
                        {
                            datagridview1.DataSource = employees;
                            this.Alert("Records Found!", Form_Alert.enmType.Info);

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
                    if (searchtxtbox.Text != "")
                    {
                        //Filtering with NIC
                        if ((jobchk.Checked == false) & (typechk.Checked == false) & (nic != ""))
                        {
                            var filterDefinition = filternic;
                            var projection = Builders<employeemodel>.Projection.Exclude("_id");
                            var employees = employeeCollection.Find(filterDefinition).Project<employeemodel>(projection).ToList();

                            if (employees.Count > 0)
                            {
                                datagridview1.DataSource = employees;
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
                this.Alert("Error - " + ex, Form_Alert.enmType.Error);
            }
        }


        // method to reset datagrid view
        public void LoadUserData()
        {
            var filterDefinition = Builders<employeemodel>.Filter.Empty;
            var projection = Builders<employeemodel>.Projection.Exclude("_id");
            var orders = employeeCollection.Find(filterDefinition).Project<employeemodel>(projection)
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

        private void resetbtn_Click(object sender, EventArgs e)
        {
            LoadUserData();
            searchtxtbox.Text = "";
            checkreset();
        }

        //resetting filter checkboxes
        public void checkreset()
        {
            typechk.Checked = false;
            jobchk.Checked = false;
        }

        private void typechk_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (typechk.Checked)
            {
                typechktxt.Visible = true;
            }
            else
            {
                typechktxt.Visible = false;
            }
        }

        private void jobchk_CheckStateChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (jobchk.Checked)
            {
                jobchktxt.Visible = true;
            }
            else
            {
                jobchktxt.Visible = false;
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
                var table = "Employees";

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
                    cell.set_Value(Missing.Value, "Art Enterprises " + table + " Table As At " + date + ".");
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
            staticmethods.databaseshow();
            this.Hide();
        }
    }
}
