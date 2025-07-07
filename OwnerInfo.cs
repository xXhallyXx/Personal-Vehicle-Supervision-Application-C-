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
    public partial class OwnerInfo : Form
    {
        public OwnerInfo()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            CarOwnerInfo back = new CarOwnerInfo();
            back.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ownerName = textBox1.Text.Trim();
            string ownerID = textBox2.Text.Trim();
            string phoneNumber = textBox3.Text.Trim();
            string vehicleModel = textBox4.Text.Trim();
            string vehicleID = textBox5.Text.Trim();

            
            if (string.IsNullOrEmpty(ownerID))
            {
                MessageBox.Show("Please enter the Owner ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\VehInfo.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "INSERT INTO VehInfo (OwnerID, OwnerName, PhoneNumber, VehicleModel, VehicleID) " +
                                   "VALUES (@id, @name, @phone, @model, @vid)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", ownerName);
                    cmd.Parameters.AddWithValue("@id", ownerID);
                    cmd.Parameters.AddWithValue("@phone", phoneNumber);
                    cmd.Parameters.AddWithValue("@model", vehicleModel);
                    cmd.Parameters.AddWithValue("@vid", vehicleID);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Data inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Insert failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting data: " + ex.Message);
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\VehInfo.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT * FROM VehInfo";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView2.DataSource = table;

                    
                    dataGridView2.Dock = DockStyle.Bottom;
                    dataGridView2.Height = 200; 
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ownerName = textBox2.Text.Trim();
            string ownerID = textBox1.Text.Trim();
            string phoneNumber = textBox3.Text.Trim();
            string vehicleModel = textBox4.Text.Trim();
            string vehicleID = textBox5.Text.Trim();

            if (string.IsNullOrEmpty(ownerID))
            {
                MessageBox.Show("Please enter the Owner ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Classes\spring25-26\C#\lab\project\Main\Main\VehInfo.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "UPDATE VehInfo SET OwnerName = @name, PhoneNumber = @phone, VehicleModel = @model, VehicleID = @vid WHERE OwnerID = @id";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", ownerID);      
                    cmd.Parameters.AddWithValue("@name", ownerName);  
                    cmd.Parameters.AddWithValue("@phone", phoneNumber);
                    cmd.Parameters.AddWithValue("@model", vehicleModel);
                    cmd.Parameters.AddWithValue("@vid", vehicleID);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Update failed! OwnerID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating data: " + ex.Message);
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ownerID = textBox1.Text.Trim(); 

            if (string.IsNullOrEmpty(ownerID))
            {
                MessageBox.Show("Please enter OwnerID to delete the record.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Classes\spring25-26\C#\lab\project\Main\Main\VehInfo.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    
                    DialogResult confirm = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirm == DialogResult.No)
                        return;

                    string query = "DELETE FROM VehInfo WHERE OwnerID = @id";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", ownerID);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Record deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No record found with this OwnerID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            ownerQuery f1 = new ownerQuery();
            f1.Show();
            Visible = false;
        }
    }
    }
