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

namespace Main
{
    public partial class expertlogin : Form
    {
        public expertlogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 back = new Form1();
            back.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string expertID = txtUserName.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(expertID))
            {
                MessageBox.Show("Please enter the Expert ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter the Password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Use the correct connection string for your local SQL Server
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\Database1.mdf;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT COUNT(*) FROM carExpert WHERE expertID = @expertID AND password = @password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@expertID", expertID);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 1)
                    {
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Open Form2 (carownerinfo)
                        ExpertInfo form2 = new ExpertInfo();
                        form2.Show();
                        this.Hide(); // hide Form1
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
        

        private void button4_Click(object sender, EventArgs e)
        {
           RegExpert f3 = new RegExpert();
            f3.Show();
            Visible = false;
        }
    }
}
