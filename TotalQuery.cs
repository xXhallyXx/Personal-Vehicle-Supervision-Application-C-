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
    public partial class TotalQuery : Form
    {
        public TotalQuery()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            ExpertInfo back = new ExpertInfo();
            back.Show();
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
                    dataGridView1.Height = 300;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying table: " + ex.Message);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string vehicleID = textBox1.Text;
            string expertQuery = textBox2.Text;
            string cost = textBox3.Text;

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

                   
                    string updateQuery = "UPDATE QueryTable " +
                                         "SET [Expert Query] = @expertQuery, Cost = @cost " +
                                         "WHERE VehicleID = @vehicleID";

                    SqlCommand cmd = new SqlCommand(updateQuery, con);
                    cmd.Parameters.AddWithValue("@vehicleID", vehicleID);
                    cmd.Parameters.AddWithValue("@expertQuery", expertQuery);
                    cmd.Parameters.AddWithValue("@cost", cost);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Expert query and cost updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No matching VehicleID found. Update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating data: " + ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string vehicleID = textBox1.Text;

            if (string.IsNullOrWhiteSpace(vehicleID))
            {
                MessageBox.Show("Please enter a VehicleID to delete.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\QueryTable.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string deleteQuery = "DELETE FROM QueryTable WHERE VehicleID = @vehicleID";

                    SqlCommand cmd = new SqlCommand(deleteQuery, con);
                    cmd.Parameters.AddWithValue("@vehicleID", vehicleID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No record found with that VehicleID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            
        }
    }
    }

