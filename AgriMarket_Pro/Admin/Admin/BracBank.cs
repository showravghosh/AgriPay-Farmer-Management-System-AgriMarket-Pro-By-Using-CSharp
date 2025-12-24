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
    public partial class BracBank : Form
    {
        private Form _sourceForm;
        private decimal _totalAmount;
        public BracBank(Form sourceForm, decimal totalAmount)
        {
            InitializeComponent();
            _sourceForm = sourceForm;
            _totalAmount = totalAmount;
            textBox3.Text = _totalAmount.ToString("0.00");

        }

        private void button7_Click(object sender, EventArgs e)
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
            if (_sourceForm != null)
            {
                // Navigate back to the previous form with totalAmount
                UserPayHome uph = new UserPayHome(_sourceForm, _totalAmount);
                uph.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("This action is not allowed from the current form.");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get values from the textboxes
            string userId = textBox1.Text;
            string password = textBox2.Text;
            string amount = textBox3.Text;

            // Check if all textboxes are filled
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(amount))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            // Validate User ID: must be 6 digits
            if (userId.Length != 6 || !userId.All(char.IsDigit))
            {
                MessageBox.Show("User ID should be exactly 6 digits.");
                return;
            }

            // Validate password: must be exactly 4 characters long
            if (password.Length != 4)
            {
                MessageBox.Show("Password should be exactly 4 characters.");
                return;
            }

            // Insert the data into the UpAid table with 'BracBank'
            try
            {
                InsertIntoUpaid(userId, amount);
                MessageBox.Show("Payment Successful");

                // Proceed to UserHome
                UserHome userHome = new UserHome();
                userHome.Show();
                this.Hide();

                // Clear the fields
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void InsertIntoUpaid(string userId, string amount)
        {
            // Connect to the database
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                con.Open();
                // Insert into Upaid table with "From" set to "BracBank"
                string query = "INSERT INTO [Upaid] ([Ph_Num/U_ID/Acc_Num], [Amount], [From]) VALUES (@UserId, @Amount, 'BracBank')";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void BracBank_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Amount
        {

        }
    }
}
