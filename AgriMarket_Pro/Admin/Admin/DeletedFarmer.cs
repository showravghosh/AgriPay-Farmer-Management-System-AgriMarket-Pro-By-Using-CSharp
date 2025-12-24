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
    public partial class DeletedFarmer : Form
    {
        SqlConnection con;

        public DeletedFarmer()
        {
            InitializeComponent();
        }

        private void LoadDeletedFarmers()
        {
            try
            {
                ConnectDatabase();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Delf", con);
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

        private void ConnectDatabase()
        {
            try
            {
                if (con == null) // Ensure the connection object is initialized
                {
                    con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30");
                }

                if (con.State == ConnectionState.Closed) // Open the connection only if it's closed
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message);
            }
        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeletedFarmer_Load(object sender, EventArgs e)
        {
            LoadDeletedFarmers();

        }

        private void button2_Click(object sender, EventArgs e)//Search
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Search
        {
            string searchValue = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(searchValue))
            {
                try
                {
                    ConnectDatabase();
                    string query = "SELECT * FROM Delf WHERE [Name] LIKE @Search OR [Phone Number] LIKE @Search";
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
                        MessageBox.Show("No records found.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                // If search value is empty, reload all data
                LoadDeletedFarmers();  // Function to load data for DeletedFarmer
            }
        }

        private void button5_Click(object sender, EventArgs e)//Add
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    ConnectDatabase();
                    int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                    string name = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
                    string birth = dataGridView1.SelectedRows[0].Cells["Birth"].Value.ToString();
                    string address = dataGridView1.SelectedRows[0].Cells["Address"].Value.ToString();
                    string division = dataGridView1.SelectedRows[0].Cells["Division"].Value.ToString();
                    int postalCode = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Postal Code"].Value);
                    string phoneNumber = dataGridView1.SelectedRows[0].Cells["Phone Number"].Value.ToString();
                    string email = dataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
                    string gender = dataGridView1.SelectedRows[0].Cells["Gender"].Value.ToString();
                    string password = dataGridView1.SelectedRows[0].Cells["Password"].Value.ToString();

                    // Insert back to Farmer table
                    SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO Farmer (Name, Birth, Address, Division, [Postal Code], [Phone Number], Email, Gender, Password) " +
                        "VALUES (@Name, @Birth, @Address, @Division, @PostalCode, @PhoneNumber, @Email, @Gender, @Password)", con
                    );
                    insertCmd.Parameters.AddWithValue("@Name", name);
                    insertCmd.Parameters.AddWithValue("@Birth", birth);
                    insertCmd.Parameters.AddWithValue("@Address", address);
                    insertCmd.Parameters.AddWithValue("@Division", division);
                    insertCmd.Parameters.AddWithValue("@PostalCode", postalCode);
                    insertCmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.Parameters.AddWithValue("@Gender", gender);
                    insertCmd.Parameters.AddWithValue("@Password", password);
                    insertCmd.ExecuteNonQuery();

                    // Delete from Delf table
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM Delf WHERE Id = @Id", con);
                    deleteCmd.Parameters.AddWithValue("@Id", selectedID);
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show("Farmer added back to the list.");
                    LoadDeletedFarmers(); // Refresh the DataGridView
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a deleted farmer to restore.");
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
