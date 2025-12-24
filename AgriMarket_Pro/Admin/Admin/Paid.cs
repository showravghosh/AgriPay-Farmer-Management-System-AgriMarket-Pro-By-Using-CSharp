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
    public partial class Paid : Form
    {
        SqlConnection con;

        public Paid()
        {
            InitializeComponent();
        }

        private void ConnectDatabase()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadPaidData()
        {
            try
            {
                ConnectDatabase();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Paid]", con);
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


        private void Paid_Load(object sender, EventArgs e)
        {
            LoadPaidData();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Search
        {
            string searchValue = textBox3.Text.Trim();

            try
            {
                ConnectDatabase();
                string query = "SELECT * FROM [Paid] WHERE [Phone Number] LIKE @Search";
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

        private void button2_Click(object sender, EventArgs e)//Search
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            UnPaid up = new UnPaid();

            //Show Form1
            up.Show();
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
