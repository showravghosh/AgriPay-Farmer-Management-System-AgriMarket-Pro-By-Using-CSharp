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
    public partial class Rocket : Form
    {
        private Form _sourceForm;
        private decimal _totalAmount;

        public Rocket(Form sourceForm, decimal totalAmount)
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

        private void textBox1_TextChanged(object sender, EventArgs e)//Number
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)//PassWord
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Amount
        {

        }

        private void button1_Click(object sender, EventArgs e)//Payment
        {
            // Get values from the textboxes
            string phoneNumber = textBox1.Text;
            string password = textBox2.Text;
            string amount = textBox3.Text;

            // Check if all textboxes are filled
            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(amount))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            // Validate phone number: must start with '01' and be 11 digits long
            if (!phoneNumber.StartsWith("01") || phoneNumber.Length != 11 || !phoneNumber.All(char.IsDigit))
            {
                MessageBox.Show("Your Phone Number is Wrong");
                return;
            }

            // Validate password: must be exactly 4 characters long
            if (password.Length != 4)
            {
                MessageBox.Show("Your Password Don't Match");
                return;
            }

            // If all conditions are met
            try
            {
                InsertIntoUpaid(phoneNumber, amount);
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

        private void InsertIntoUpaid(string phoneNumber, string amount)
        {
            // Connect to the database
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                con.Open();
                // Insert into Upaid table
                string query = "INSERT INTO [Upaid] ([Ph_Num/U_ID/Acc_Num], [Amount], [From]) VALUES (@Phone, @Amount, 'Rocket')";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Phone", phoneNumber);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Rocket_Load(object sender, EventArgs e)
        {

        }
    }
}
