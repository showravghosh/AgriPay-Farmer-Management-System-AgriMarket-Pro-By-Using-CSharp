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
using System.Text.RegularExpressions;

namespace Admin
{
    public partial class AdminReg : Form
    {
        SqlConnection con;

        public AdminReg()
        {
            InitializeComponent();
        }

        private void ConnectDatabase()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
        }

        private void button7_Click(object sender, EventArgs e)//Back
        {
            Admin admin = new Admin();

            //Show Form1
            admin.Show();
            //Close Form2
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)//Home
        {
            Login login = new Login();

            // Subscribe to the FormClosed event of Form2
            login.FormClosed += (s, args) => this.Show();

            // Show Form2
            login.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Name
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)//Phone Number
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Email Address
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)//Password
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)//Male
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)//Female
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)//Other
        {

        }

        private void button1_Click(object sender, EventArgs e)//Register
        {
            if (ValidateInputs())
            {
                try
                {
                    ConnectDatabase();

                    // Check if the email or phone number already exists in the Admin table
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM [Admin] WHERE [Email Address] = @Email OR [Phone Number] = @Phone", con);
                    checkCmd.Parameters.AddWithValue("@Email", textBox3.Text);
                    checkCmd.Parameters.AddWithValue("@Phone", textBox2.Text);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Phone Number or Email already exists. Please use different credentials.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Insert the new admin record
                        SqlCommand cmd = new SqlCommand("INSERT INTO [Admin] ([Name], [Phone Number], [Email Address], [Password], [Gender]) VALUES (@Name, @Phone, @Email, @Password, @Gender)", con);
                        cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Phone", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Password", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Gender", GetSelectedGender());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Registration successful!");
                        button2_Click(sender, e);
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//Reset
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }

        private bool ValidateInputs()
        {
            // Check if all fields are filled
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                !radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            {
                MessageBox.Show("All fields must be filled.");
                return false;
            }

            // Phone number validation
            if (!textBox2.Text.StartsWith("01") || textBox2.Text.Length != 11 || !long.TryParse(textBox2.Text, out _))
            {
                MessageBox.Show("Phone Number must start with '01' and be exactly 11 digits.");
                return false;
            }

            // Email validation
            if (!Regex.IsMatch(textBox3.Text, @"^[^@\s]+@gmail\.com$"))
            {
                MessageBox.Show("Email Address must be in @gmail.com format.");
                return false;
            }

            // Password validation
            if (textBox4.Text.Length < 6 ||
                !Regex.IsMatch(textBox4.Text, @"[A-Za-z]") ||  // Letters (lowercase and uppercase)
                !Regex.IsMatch(textBox4.Text, @"\d") ||         // Digits
                !Regex.IsMatch(textBox4.Text, @"[\W_]") ||      // Special characters
                !Regex.IsMatch(textBox4.Text, @"[a-z]") ||      // Lowercase letter
                !Regex.IsMatch(textBox4.Text, @"[A-Z]"))        // Uppercase letter
            {
                MessageBox.Show("Password must be at least 6 characters and mixed like (@#%123ABcd).");
                return false;
            }

            return true;
        }
        private string GetSelectedGender()
        {
            if (radioButton1.Checked) return "Male";
            if (radioButton2.Checked) return "Female";
            if (radioButton3.Checked) return "Other";
            return string.Empty;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AdminReg_Load(object sender, EventArgs e)
        {
            textBox4.PasswordChar = '*';

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.PasswordChar = '\0';
            }
            else
            {
                textBox4.PasswordChar = '*';
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
