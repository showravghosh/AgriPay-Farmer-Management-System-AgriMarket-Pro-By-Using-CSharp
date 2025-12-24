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
    public partial class DeleteAdmin : Form
    {
        SqlConnection con;

        public DeleteAdmin()
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Dela", con);
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



        private void textBox1_TextChanged(object sender, EventArgs e)//Search
        {
            string searchValue = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(searchValue))
            {
                try
                {
                    ConnectDatabase();
                    string query = "SELECT * FROM Dela WHERE Name LIKE @Search OR [Phone Number] LIKE @Search";
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
                LoadData();
            }
        }

        private void button5_Click(object sender, EventArgs e)//Add
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    ConnectDatabase();

                    // Get selected admin details
                    int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                    string name = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
                    string phoneNumber = dataGridView1.SelectedRows[0].Cells["Phone Number"].Value.ToString();
                    string email = dataGridView1.SelectedRows[0].Cells["Email Address"].Value.ToString();
                    string gender = dataGridView1.SelectedRows[0].Cells["Gender"].Value.ToString();
                    string password = dataGridView1.SelectedRows[0].Cells["Password"].Value.ToString();

                    // Insert the restored admin back into the Admin table
                    SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO Admin (Name, [Phone Number], [Email Address], Password, Gender) " +
                        "VALUES (@Name, @PhoneNumber, @Email, @Password, @Gender)", con
                    );
                    insertCmd.Parameters.AddWithValue("@Name", name);
                    insertCmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.Parameters.AddWithValue("@Password", password);
                    insertCmd.Parameters.AddWithValue("@Gender", gender);
                    insertCmd.ExecuteNonQuery();

                    // Delete the admin from Dela table (restoring)
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM Dela WHERE Id = @Id", con);
                    deleteCmd.Parameters.AddWithValue("@Id", selectedID);
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show("Admin restored.");
                    LoadData(); // Refresh the DataGridView to reflect the changes
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an admin to restore.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeleteAdmin_Load(object sender, EventArgs e)
        {
            LoadData();

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
