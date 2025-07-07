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
    public partial class ownerQuery : Form
    {
        public ownerQuery()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            OwnerInfo back = new OwnerInfo();
            back.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ownerID = textBox1.Text.Trim();
            string vehicleID = textBox2.Text.Trim();
            string ownerQuery = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(vehicleID))
            {
                MessageBox.Show("Please enter the Vehicle ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\QueryTable.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "INSERT INTO QueryTable (OwnerID, VehicleID, [Owner Query]) " +
                                   "VALUES (@ownerID, @vehicleID, @ownerQuery)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ownerID", ownerID);
                    cmd.Parameters.AddWithValue("@vehicleID", vehicleID);
                    cmd.Parameters.AddWithValue("@ownerQuery", ownerQuery);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Query sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Send failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending data: " + ex.Message);
                }
            }

        }
    

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\QueryTable.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT * FROM QueryTable"; 

                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    
                    dataGridView1.DataSource = dt;

                    
                    dataGridView1.Dock = DockStyle.Bottom;
                    dataGridView1.Height = 180;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying data: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string vehicleID = textBox2.Text;

            if (string.IsNullOrWhiteSpace(vehicleID))
            {
                MessageBox.Show("Please enter a VehicleID to delete.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\QueryTable.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "DELETE FROM QueryTable WHERE VehicleID = @vehicleID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@vehicleID", vehicleID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No matching record found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting data: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\VehicleParts.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT [Vehicle PartsID], [Vehicle Name], Cost, [Total Amount to Purchase] FROM Product";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    
                    dataGridView1.Dock = DockStyle.Bottom;
                    dataGridView1.Height = 180; 
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.RowHeadersVisible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying data: " + ex.Message, "Display Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        

        

        private void button7_Click(object sender, EventArgs e)
        {
            string partsID = textBox4.Text;
            string amountText = textBox5.Text;

            if (string.IsNullOrWhiteSpace(partsID))
            {
                MessageBox.Show("Vehicle PartsID is required.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(amountText))
                 {
                     MessageBox.Show("Enter the amount of the product.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                 }

                object amountValue;
            if (string.IsNullOrWhiteSpace(amountText))
            {
                amountValue = DBNull.Value;
            }
            else if (int.TryParse(amountText, out int amount))
            {
                amountValue = amount;
            }
            else
            {
                MessageBox.Show("Total Amount to Purchase must be a valid number or empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\VehicleParts.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "UPDATE Product SET [Total Amount to Purchase] = @amount WHERE [Vehicle PartsID] = @partsID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@amount", amountValue);
                    cmd.Parameters.AddWithValue("@partsID", partsID);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Total Amount updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No matching Vehicle PartsID found. Update failed.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating data: " + ex.Message);
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
