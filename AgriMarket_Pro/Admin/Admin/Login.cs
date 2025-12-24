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

    public partial class Login : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30";

        public Login()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)//Phone Number/Email Address
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Password
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//Show Password
        {
            if (checkBox1.Checked)
            {
                textBox1.PasswordChar = '\0';
            }
            else
            {
                textBox1.PasswordChar = '*';
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//Role
        {

        }

        private bool CheckCredentials(string tableName, string phoneColumn, string emailColumn, string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $@"
            SELECT COUNT(*) 
            FROM {tableName} 
            WHERE ([{phoneColumn}] = @Username OR [{emailColumn}] = @Username) 
            AND [Password] = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();
                        int count = (result != null) ? Convert.ToInt32(result) : 0;
                        return count > 0; // Returns true if a matching user is found
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)//Login
        {
            string enteredUsername = textBox5.Text.Trim(); // Phone Number or Email
            string enteredPassword = textBox1.Text.Trim(); // Password
            string selectedRole = comboBox1.SelectedItem?.ToString(); // Selected Role

            if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword) || string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Please fill in all fields and select a role.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isValidUser = false;

            switch (selectedRole)
            {
                case "Admin":
                    isValidUser = CheckCredentials("Admin", "Phone Number", "Email Address", enteredUsername, enteredPassword);
                    if (isValidUser)
                    {
                        Admin adminForm = new Admin();
                        adminForm.Show();
                        this.Hide();
                    }
                    break;

                case "Farmer":
                    isValidUser = CheckCredentials("Farmer", "Phone Number", "Email", enteredUsername, enteredPassword);
                    if (isValidUser)
                    {
                        FarmerHome farmerHome = new FarmerHome();
                        farmerHome.Show();
                        this.Hide();
                    }
                    break;

                case "User":
                    isValidUser = CheckCredentials("UserList", "Phone Number", "Email", enteredUsername, enteredPassword);
                    if (isValidUser)
                    {
                        UserHome userHome = new UserHome();
                        userHome.Show();
                        this.Hide();
                    }
                    break;

                default:
                    MessageBox.Show("Invalid role selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            if (!isValidUser)
            {
                MessageBox.Show("Invalid credentials. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)//Don't Have Account
        {
            Registration registration = new Registration();

            //Show Registration
            registration.Show();
            //Close Form2
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            About about = new About();

            //Show About
            about.Show();
            //Close this
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] {"Admin", "Farmer", "User"});
            textBox1.PasswordChar = '*';

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
