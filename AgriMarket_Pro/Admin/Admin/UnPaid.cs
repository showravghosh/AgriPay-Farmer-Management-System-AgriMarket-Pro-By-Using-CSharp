using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Admin
{
    public partial class UnPaid : Form
    {
        SqlConnection con;
        public UnPaid()
        {
            InitializeComponent();
        }

        private void ConnectDatabase()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
        }

        // Load Data into DataGridView
        private void LoadData()
        {
            try
            {
                ConnectDatabase();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Unpaid]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is selected
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["Phone Number"].Value.ToString(); // Assign Phone Number
                textBox2.Text = row.Cells["Total Amount"].Value.ToString(); // Assign Total Amount
            }
        }

        private void UnPaid_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Phone Number
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)//Total Amount
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Search
        {

        }

        private void button1_Click(object sender, EventArgs e)//Payment
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please select a record before making a payment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ConnectDatabase();

                string phoneNumber = textBox1.Text;
                string totalAmount = textBox2.Text;

                // Step 1: Insert data into Paid table
                string insertQuery = "INSERT INTO [Paid] ([Phone Number], [Total Amount]) VALUES (@Phone, @Amount)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@Phone", phoneNumber);
                insertCmd.Parameters.AddWithValue("@Amount", totalAmount);
                insertCmd.ExecuteNonQuery();

                // Step 2: Delete data from UnPaid table
                string deleteQuery = "DELETE FROM [Unpaid] WHERE [Phone Number] = @Phone AND [Total Amount] = @Amount";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                deleteCmd.Parameters.AddWithValue("@Phone", phoneNumber);
                deleteCmd.Parameters.AddWithValue("@Amount", totalAmount);
                deleteCmd.ExecuteNonQuery();

                con.Close();

                // Step 3: Refresh DataGridView in UnPaid Form
                LoadData(); // Refresh the UnPaid DataGridView

                // Step 4: Clear TextBoxes
                textBox1.Clear();
                textBox2.Clear();

                MessageBox.Show("Payment processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)//Search
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)//Search
        {
            string searchValue = textBox3.Text.Trim();

            try
            {
                ConnectDatabase();
                string query = "SELECT * FROM [Unpaid] WHERE [Phone Number] LIKE @Search";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@Search", "%" + searchValue + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)//Delete
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        ConnectDatabase();
                        SqlCommand cmd = new SqlCommand("DELETE FROM [Unpaid] WHERE Serial = @Id", con);
                        cmd.Parameters.AddWithValue("@Id", dataGridView1.SelectedRows[0].Cells["Serial"].Value);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record deleted successfully.");
                        LoadData();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();

            //Show Form1
            admin.Show();
            //Close Form2
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Paid pd = new Paid();

            // Subscribe to the FormClosed event of Form2
            pd.FormClosed += (s, args) => this.Show();

            // Show Form2
            pd.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            // Subscribe to the FormClosed event of Form2
            login.FormClosed += (s, args) => this.Show();

            // Show Form2
            login.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }
    }
}
