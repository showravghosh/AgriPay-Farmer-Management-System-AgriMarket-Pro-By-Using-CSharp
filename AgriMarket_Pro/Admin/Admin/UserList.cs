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
    public partial class UserList : Form
    {
        SqlConnection con;

        public UserList()
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [UserList]", con);
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

            //Show Admin
            admin.Show();
            //Close this
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)//Delete
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

                    // Insert the deleted user into the Delu table
                    SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO Delu (Name, Birth, Address, Division, [Postal Code], [Phone Number], Email, Gender, Password) " +
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

                    // Delete from UserList table
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM UserList WHERE Id = @Id", con);
                    deleteCmd.Parameters.AddWithValue("@Id", selectedID);
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show("User deleted.");
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
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Search
        {
            string searchValue = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(searchValue))
            {
                try
                {
                    ConnectDatabase();
                    string query = "SELECT * FROM [UserList] WHERE [Name] LIKE @Search OR [Phone Number] LIKE @Search";
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
                LoadData();
            }
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
                string query = "SELECT * FROM [UserList] WHERE [Name] LIKE @Search OR [Phone Number] LIKE @Search";
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

        private void UserList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button5_Click(object sender, EventArgs e)//Reset
        {
            textBox1.Clear();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
