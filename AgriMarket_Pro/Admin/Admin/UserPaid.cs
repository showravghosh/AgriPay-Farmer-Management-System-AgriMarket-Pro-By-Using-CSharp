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
    public partial class UserPaid : Form
    {
        public UserPaid()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadDataIntoDataGridView(string searchTerm = "")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    con.Open();
                    string query = "SELECT [Serial], [Ph_Num/U_ID/Acc_Num], [Amount], [From] FROM [Upaid]";

                    // If there is a search term, modify the query to filter results
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " WHERE [Ph_Num/U_ID/Acc_Num] LIKE @SearchTerm OR [Amount] LIKE @SearchTerm";
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                    {
                        // Add parameter for search term to prevent SQL injection
                        da.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UserPaid_Load(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Search
        {
            string searchTerm = textBox3.Text;
            LoadDataIntoDataGridView(searchTerm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();

            //Show Admin
            admin.Show();
            //Close this
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
