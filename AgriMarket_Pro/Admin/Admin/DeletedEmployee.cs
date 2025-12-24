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
    public partial class DeletedEmployee : Form
    {
        SqlConnection con;

        public DeletedEmployee()
        {
            InitializeComponent();
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

        private void LoadDeletedEmployees()
        {
            try
            {
                ConnectDatabase();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DelEmp", con);
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


        private void DeletedEmployee_Load(object sender, EventArgs e)
        {
            LoadDeletedEmployees();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)//Search
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Search
        {
            try
            {
                ConnectDatabase();
                string searchQuery = "SELECT * FROM DelEmp WHERE [Employee Name] LIKE @search";
                SqlDataAdapter da = new SqlDataAdapter(searchQuery, con);
                da.SelectCommand.Parameters.AddWithValue("@search", "%" + textBox3.Text.Trim() + "%");

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

        private void button1_Click(object sender, EventArgs e)//Add
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    ConnectDatabase();

                    int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);
                    string name = dataGridView1.SelectedRows[0].Cells["Employee Name"].Value.ToString();
                    string salary = dataGridView1.SelectedRows[0].Cells["Salary"].Value.ToString();
                    string phoneNumber = dataGridView1.SelectedRows[0].Cells["Phone Number"].Value.ToString();
                    string gender = dataGridView1.SelectedRows[0].Cells["Gender"].Value.ToString();

                    // Insert back to Employee List
                    SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO [Employee List] ([Employee Name], [Salary], [Phone Number], [Gender]) VALUES (@Name, @Salary, @PhoneNumber, @Gender)",
                        con
                    );
                    insertCmd.Parameters.AddWithValue("@Name", name);
                    insertCmd.Parameters.AddWithValue("@Salary", salary);
                    insertCmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    insertCmd.Parameters.AddWithValue("@Gender", gender);
                    insertCmd.ExecuteNonQuery();

                    // Delete from DelEmp table
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM DelEmp WHERE Id = @Id", con);
                    deleteCmd.Parameters.AddWithValue("@Id", selectedID);
                    deleteCmd.ExecuteNonQuery();

                    MessageBox.Show("Employee restored to Employee List.");
                    LoadDeletedEmployees(); // Refresh DeletedEmployee List
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to restore.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            // Subscribe to the FormClosed event of Form2
            login.FormClosed += (s, args) => this.Show();

            // Show Form2
            login.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();

            //Show Form1
            admin.Show();
            //Close Form2
            this.Hide();
        }
    }
}
