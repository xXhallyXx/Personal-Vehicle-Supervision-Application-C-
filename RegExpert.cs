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
    public partial class RegExpert : Form
    {
        public RegExpert()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            expertlogin back = new expertlogin();
            back.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string expertID = textBox1.Text.Trim();   
            string password = textBox2.Text.Trim();    

            if (string.IsNullOrEmpty(expertID) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both ExpertID and Password.");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.");
                return;
            }
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\Database1.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO carExpert (expertID, password) VALUES (@expertID, @password)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@expertID", expertID);
                        cmd.Parameters.AddWithValue("@password", password);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Expert inserted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Insert failed.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL Error: " + ex.Message);
                }
            }
        }
    }
}
