using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        
           
        {
            string userID = txtUserName.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(userID))
            {
                MessageBox.Show("Please enter the Owner ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter the Password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\userLoginDB.mdf;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM userLoginDB WHERE userID = @user AND password = @password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 1)
                    {
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        CarOwnerInfo form2 = new CarOwnerInfo();
                        form2.Show();
                        this.Hide(); 
                    }
                    else
                    {
                        MessageBox.Show("Invalid User ID or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting to database: " + ex.Message);
                }
            }
        
          

        } 
        
        private void button3_Click(object sender, EventArgs e)
        {
            expertlogin f3 = new expertlogin();
            f3.Show();
            Visible = false;
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            RegistrationOwner f3 = new RegistrationOwner();
            f3.Show();
            Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            adminLogin f3 = new adminLogin();
            f3.Show();
            Visible = false;
        }
    }
}
