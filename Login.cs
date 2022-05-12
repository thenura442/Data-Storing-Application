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
    public partial class Login : Form
    {

        // Creating connection and initialising the collection
        string connectionString = "mongodb://localhost:27017";
        public string databaseName = "DataStore";
        public string collectionName = "Users";
        public IMongoCollection<usermodel> userCollection;

        public static string user = "", type = "";

        //Accessing Alert
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }
        public Login()
        {
            InitializeComponent();

            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            userCollection = db.GetCollection<usermodel>(collectionName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(usernametxt.Text != "" & userpasstxt.Text != "")
            {
                var checkuser = Builders<usermodel>.Filter.Eq(a => a.Username, usernametxt.Text);
                var checkpass = Builders<usermodel>.Filter.Eq(a => a.Password, userpasstxt.Text);

                var filterDefinition = checkuser & checkpass;
                var projection = Builders<usermodel>.Projection.Exclude("_id");
                var users = userCollection.Find(filterDefinition).Project<usermodel>(projection).FirstOrDefault();

                if (users != null)
                {
                    type = users.User_Type;
                    user = users.Username;
                    this.Alert("Welcome Back " + user + "!", Form_Alert.enmType.Info);

                    Home home = new Home();

                    home.Show();
                    this.Hide();
                }
                else
                {
                    this.Alert("Please Check Your Credentials!", Form_Alert.enmType.Warning);
                }
            }
            else
            {
                this.Alert("Please Fill All Fields!", Form_Alert.enmType.Warning);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void deleteuser()
        {
            user = null;
            type = null;
        }
    }
}
