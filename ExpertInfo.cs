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
    public partial class ExpertInfo : Form
    {
        public ExpertInfo()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Feedback f1 = new Feedback();
            f1.Show();
            Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            expertlogin back = new expertlogin();
            back.Show();
        }

        private void extButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TotalQuery f1 = new TotalQuery();
            f1.Show();
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string partsID = textBox1.Text;
            string vehicleName = textBox2.Text;
            string cost = textBox3.Text;

            if (string.IsNullOrWhiteSpace(partsID))
            {
                MessageBox.Show("Vehicle PartsID is required.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\VehicleParts.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "INSERT INTO Product ([Vehicle PartsID], [Vehicle Name], Cost) " +
                                   "VALUES (@partsID, @vehicleName, @cost)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@partsID", partsID);
                    cmd.Parameters.AddWithValue("@vehicleName", string.IsNullOrWhiteSpace(vehicleName) ? (object)DBNull.Value : vehicleName);
                    cmd.Parameters.AddWithValue("@cost", string.IsNullOrWhiteSpace(cost) ? (object)DBNull.Value : cost);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Product inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Insert failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting product: " + ex.Message);
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
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
                    dataGridView1.Height = 200; 
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.RowHeadersVisible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying data: " + ex.Message, "Display Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        

        private void button5_Click(object sender, EventArgs e)
        {
            string partsID = textBox1.Text;

            if (string.IsNullOrWhiteSpace(partsID))
            {
                MessageBox.Show("Please enter a Vehicle PartsID to delete.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Classes\\spring25-26\\C#\\lab\\project\\Main\\Main\\VehicleParts.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    
                    string deleteQuery = "DELETE FROM Product WHERE [Vehicle PartsID] = @partsID";
                    SqlCommand cmd = new SqlCommand(deleteQuery, con);
                    cmd.Parameters.AddWithValue("@partsID", partsID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       
                        string selectQuery = "SELECT [Vehicle PartsID], [Vehicle Name], Cost FROM Product";
                        SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No record found with that Vehicle PartsID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
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

                    dataGridView1.DataSource = table;

                    
                    dataGridView1.Dock = DockStyle.Bottom;
                    dataGridView1.Height = 200; 
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string ownerID = textBox4.Text.Trim();

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
    }
    }
    

