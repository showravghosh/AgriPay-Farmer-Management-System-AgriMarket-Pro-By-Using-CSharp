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
    public partial class AdminList : Form
    {
        SqlConnection con;

        public AdminList()
        {
            InitializeComponent();
        }

        private void ConnectDatabase()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
        }

        private void LoadData()
        {
            try
            {
                ConnectDatabase();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Admin]", con);
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

        private void button4_Click(object sender, EventArgs e)//Home
        {
            Login login = new Login();

            // Subscribe to the FormClosed event of Form2
            login.FormClosed += (s, args) => this.Show();

            // Show Form2
            login.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)//Back
        {
            Admin admin = new Admin();

            //Show admin
            admin.Show();
            //Close list
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)//Search
        {
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a Name or Phone Number to search.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ConnectDatabase();

                // Search query to filter by Name or Phone Number
                string query = "SELECT * FROM [Admin] WHERE [Name] LIKE @Search OR [Phone Number] LIKE @Search";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@Search", "%" + searchValue + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No records found matching your search criteria.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Search
        {

        }

        private void button1_Click(object sender, EventArgs e)//Delete
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    ConnectDatabase();

                    // Get selected admin details from DataGridView
                    int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                    string name = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
                    string phoneNumber = dataGridView1.SelectedRows[0].Cells["Phone Number"].Value.ToString();
                    string email = dataGridView1.SelectedRows[0].Cells["Email Address"].Value.ToString(); // Updated to match the correct column name
                    string gender = dataGridView1.SelectedRows[0].Cells["Gender"].Value.ToString();
                    string password = dataGridView1.SelectedRows[0].Cells["Password"].Value.ToString();

                    // Insert the deleted admin into the Dela table
                    SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO Dela (Name, [Phone Number], [Email Address], Password, Gender) " +
                        "VALUES (@Name, @PhoneNumber, @Email, @Password, @Gender)", con
                    );
                    insertCmd.Parameters.AddWithValue("@Name", name);
                    insertCmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    insertCmd.Parameters.AddWithValue("@Email", email); // Corrected parameter for Email Address
                    insertCmd.Parameters.AddWithValue("@Password", password);
                    insertCmd.Parameters.AddWithValue("@Gender", gender);
                    insertCmd.ExecuteNonQuery();

                    // Delete the admin from the Admin table
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM Admin WHERE Id = @Id", con);
                    deleteCmd.Parameters.AddWithValue("@Id", selectedID);
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show("Admin deleted.");
                    LoadData(); // Refresh the DataGridView
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an admin to delete.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//Table
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                MessageBox.Show($"Name: {row.Cells["Name"].Value}\nPhone: {row.Cells["Phone Number"].Value}\nEmail: {row.Cells["Email Address"].Value}\nGender: {row.Cells["Gender"].Value}");
            }
        }

        private void AdminList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            LoadData();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
