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
    public partial class Registration : Form
    {
        private SqlConnection con;
        public Registration()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30");
        }


        private void Registration_Load(object sender, EventArgs e)
        {
            comboBox2.Items.AddRange(new string[] { "Dhaka", "Chittagong", "Khulna", "Rajshahi", "Barisal", "Sylhet", "Rangpur", "Mymensingh" });
            comboBox1.Items.AddRange(new string[] { "Farmer", "User" });

            textBox9.PasswordChar = '*';
            textBox8.PasswordChar = '*';

        }

        private void button3_Click_1(object sender, EventArgs e)//Back
        {
            Login login = new Login();

            //Show login
            login.Show();
            //Close this
            this.Hide();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)//First name
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Last name
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)//Birth (m-d-yy)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)//Address
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//Division
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)//Postal Code
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)//Phone Number
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)//Email
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

        private void textBox9_TextChanged(object sender, EventArgs e)//PassWord
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)//Re-enter Password
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//Role
        {

        }

        private void button1_Click(object sender, EventArgs e)//Register
        {

            if (!ValidateInputs())
                return;

            string name = textBox2.Text.Trim() + " " + textBox3.Text.Trim();
            string birth = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            string address = textBox4.Text.Trim();
            string division = comboBox2.SelectedItem.ToString();
            int postalCode = int.Parse(textBox5.Text);
            string phone = textBox7.Text.Trim();
            string email = textBox6.Text.Trim();
            string gender = radioButton1.Checked ? "Male" : radioButton2.Checked ? "Female" : "Other";
            string password = textBox9.Text.Trim();
            string role = comboBox1.SelectedItem.ToString();

            string tableName = role == "Farmer" ? "Farmer" : "UserList";

            // Check if phone number already exists for the role (Farmer or UserList)
            string phoneCheckQuery = $"SELECT COUNT(*) FROM {tableName} WHERE [Phone Number] = @Phone";
            string emailCheckQuery = $"SELECT COUNT(*) FROM {tableName} WHERE [Email] = @Email";

            try
            {
                con.Open();

                // Check for duplicate phone number
                SqlCommand phoneCmd = new SqlCommand(phoneCheckQuery, con);
                phoneCmd.Parameters.AddWithValue("@Phone", phone);
                int phoneCount = (int)phoneCmd.ExecuteScalar();
                if (phoneCount > 0)
                {
                    MessageBox.Show("Phone Number is already registered.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check for duplicate email address
                SqlCommand emailCmd = new SqlCommand(emailCheckQuery, con);
                emailCmd.Parameters.AddWithValue("@Email", email);
                int emailCount = (int)emailCmd.ExecuteScalar();
                if (emailCount > 0)
                {
                    MessageBox.Show("Email is already registered.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Insert into the respective table if no duplicates found
                string query = $"INSERT INTO {tableName} ([Name], [Birth], [Address], [Division], [Postal Code], [Phone Number], [Email], [Gender], [Password]) " +
                               "VALUES (@Name, @Birth, @Address, @Division, @PostalCode, @Phone, @Email, @Gender, @Password)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Birth", birth);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Division", division);
                cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button4_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)//Reset
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//View Password
        {
            if (checkBox1.Checked)
            {
                textBox9.PasswordChar = '\0';
            }
            else
            {
                textBox9.PasswordChar = '*';
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//View Re-enter Password
        {
            if (checkBox2.Checked)
            {
                textBox8.PasswordChar = '\0';
            }
            else
            {
                textBox8.PasswordChar = '*';
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Birth (dd-MM-yyyy)
        {

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)//Birth (DD-MM-YYYY)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox9.Text) || string.IsNullOrWhiteSpace(textBox8.Text) ||
                comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(textBox5.Text, "^\\d{4}$"))
            {
                MessageBox.Show("Postal Code must be exactly 4 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if ((DateTime.Now.Year - dateTimePicker1.Value.Year) < 18)
            {
                MessageBox.Show("You must be at least 18 years old.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(textBox7.Text, "^01\\d{9}$"))
            {
                MessageBox.Show("Phone Number must start with '01' and be exactly 11 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(textBox6.Text, "^[a-zA-Z0-9._%+-]+@gmail\\.com$"))
            {
                MessageBox.Show("Email must be in @gmail.com format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(textBox9.Text, "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$"))
            {
                MessageBox.Show("Password must be at least 6 characters and mixed like (@#%123ABcd).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (textBox9.Text != textBox8.Text)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
