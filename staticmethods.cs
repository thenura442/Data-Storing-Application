namespace Data_Storing_App
{
    public static class staticmethods
    {

        private static string user = "", type = "";

        static public string connectionString = "mongodb://localhost:27017";
        static public string databaseName = "DataStore";


        //returning connection and database
        public static string getconnection()
        {
            string con = "mongodb://localhost:27017"; ;
            return con;
        }

        public static string getdatabase()
        {
            string db = "DataStore";
            return db;
        }


        //Creating methods for showing forms
        public static void homeshow()
        {
            Home hm = new Home();
            hm.Show();
        }

        public static void formsshow()
        {
            Forms frm = new Forms();
            frm.Show();
        }

        public static void databaseshow()
        {
            Databases dbs = new Databases();
            dbs.Show();
        }

        public static void remindershow()
        {
            Reminders rm = new Reminders();
            rm.Show();
        }

        public static void logoutshow()
        {
            Login lg = new Login();
            lg.Show();
        }

        public static void settingsshow()
        {
            Settings sts = new Settings();
            sts.Show();
        }


        //Databases show methods
        public static void orderdb()
        {
            Order_Database orderdb = new Order_Database();
            orderdb.Show();
        }

        public static void empdb()
        {
            Employee_Database empdb = new Employee_Database();
            empdb.Show();
        }

        public static void salarydb()
        {
            Salary_Database salarydb = new Salary_Database();
            salarydb.Show();
        }

        public static void resourcedb()
        {
            Resources_Database resourcedb = new Resources_Database();
            resourcedb.Show();
        }

        public static void userdb()
        {
            User_Database userdb = new User_Database();
            userdb.Show();
        }

        public static void vehicledb()
        {
            Vehicle_Database vehicledb = new Vehicle_Database();
            vehicledb.Show();
        }


        //Forms show methods

        public static void empform()
        {
            Employee_Form empform = new Employee_Form();
            empform.Show();
        }

        public static void salaryform()
        {
            Salary_Form salaryform = new Salary_Form();
            salaryform.Show();
        }

        public static void resourseform()
        {
            Resources_Form resourseform = new Resources_Form();
            resourseform.Show();
        }

        public static void usersform()
        {
            User_Form usersform = new User_Form();
            usersform.Show();
        }

        public static void vehicleform()
        {
            Vehicle_Form vehicleform = new Vehicle_Form();
            vehicleform.Show();
        }


        //other form methods

        public static void manualshow()
        {
            Manual manualshow = new Manual();
            manualshow.Show();
        }

        public static void helpshow()
        {
            Help helpshow = new Help();
            helpshow.Show();
        }

        public static void accountshow()
        {
            Account accountshow = new Account();
            accountshow.Show();
        }


        //setting username and type  and creating them as null


        public static void setuser(string username)
        {
            user = username;
        }

        public static void settype(string usertype)
        {
            type = usertype;
        }

        public static string getuser()
        {
            return user;
        }

        public static string gettype()
        {
            return type;
        }

        public static void deleteuser()
        {
            user = null;
            type = null;
        }

    }
}
