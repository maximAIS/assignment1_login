using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login.Main;
using Login.Register;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Debug.WriteLine("constructor fired");
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (checkUser(this.textBoxUsername.Text, this.textBoxPassword.Text))
            {
                MainForm mf = new MainForm();
                mf.Show();
            }
            else
            {
                MessageBox.Show("wrong username or password");
            }
        }

        private bool checkUser(string user, string paswd)
        {
            try
            {
                //"Default": "Server=localhost; Database=<insert db name>; User Id=sa; Password=your_password123"
                SqlConnection myConnection = new SqlConnection("server=localhost;" + 
                                                               "Trusted_Connection=yes;" + 
                                                               "database=LoginAppDB; " + 
                                                               "connection timeout=30");
            
                SqlCommand myCommand = new SqlCommand("SELECT * FROM loginDB.tblUsers", myConnection);
            
            
                SqlDataReader myReader = null;
                myConnection.Open();
                myReader = myCommand.ExecuteReader();

                Dictionary<string, string> users = new Dictionary<string, string>();
                
                while(myReader.Read())
                {
                    var username = myReader["username"].ToString();
                    var password = myReader["password"].ToString();
                    users.Add(username,password);
                }
                myConnection.Close();

                if (!(users[user] is null) && users[user] == paswd) return true;

            }
            catch (Exception es)
            {
                //MessageBox.Show(es.Message);
            }

            return false;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            RegisterForm f2 = new RegisterForm();
            f2.Show();
        }
    }
}