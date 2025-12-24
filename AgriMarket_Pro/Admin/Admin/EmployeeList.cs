using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin
{
    public partial class EmployeeList : Form
    {
        SqlConnection con;
        public EmployeeList()
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


        private void textBox2_TextChanged(object sender, EventArgs e)//Employee Name
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Salary
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)//Phone Number
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

        private void ResetFields()
        {
            textBox1.Clear(); // Salary
            textBox2.Clear(); // Employee Name
            textBox4.Clear(); // Phone Number
            radioButton1.Checked = false; // Male
            radioButton2.Checked = false; // Female
            radioButton3.Checked = false; // Other
        }

        private bool IsPhoneNumberDuplicate(string phoneNumber, int currentId)
        {
            try
            {
                ConnectDatabase();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Employee List] WHERE [Phone Number] = @PhoneNumber AND Id != @Id", con);
                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@Id", currentId);

                int count = (int)cmd.ExecuteScalar();
                con.Close();
                return count > 0; // Return true if phone number exists in another row
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking duplicate phone number: " + ex.Message);
                con.Close();
                return true; // Return true to prevent accidental duplicate entries
            }
        }


        private void button1_Click(object sender, EventArgs e)//Add
        {
            if (ValidateInputs())
            {
                try
                {
                    ConnectDatabase();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Employee List] ([Employee Name], [Salary], [Phone Number], [Gender]) VALUES (@Name, @Salary, @PhoneNumber, @Gender)", con);
                    cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Salary", textBox1.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Gender", GetSelectedGender());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data added successfully.");
                    LoadData();
                    con.Close();

                    // Reset input fields and gender
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//Update
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    ConnectDatabase(); // Ensure connection is open

                    if (con.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Database connection is not open.");
                        return;
                    }

                    int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value); // Ensure correct column name

                    // Get current values from database if textbox is empty
                    SqlCommand getCurrentValuesCmd = new SqlCommand("SELECT [Employee Name], [Salary], [Phone Number], [Gender] FROM [Employee List] WHERE ID = @ID", con);
                    getCurrentValuesCmd.Parameters.AddWithValue("@ID", selectedID);

                    SqlDataReader reader = getCurrentValuesCmd.ExecuteReader();
                    string currentName = "", currentSalary = "", currentPhone = "", currentGender = "";

                    if (reader.Read())
                    {
                        currentName = reader["Employee Name"].ToString();
                        currentSalary = reader["Salary"].ToString();
                        currentPhone = reader["Phone Number"].ToString();
                        currentGender = reader["Gender"].ToString();
                    }
                    reader.Close();

                    // Use current values if textboxes are empty
                    string updatedName = !string.IsNullOrWhiteSpace(textBox2.Text) ? textBox2.Text : currentName;
                    string updatedSalary = !string.IsNullOrWhiteSpace(textBox1.Text) ? textBox1.Text : currentSalary;
                    string updatedPhone = !string.IsNullOrWhiteSpace(textBox4.Text) ? textBox4.Text : currentPhone;
                    string updatedGender = !string.IsNullOrWhiteSpace(GetSelectedGender()) ? GetSelectedGender() : currentGender;

                    // Validate salary to ensure it's a positive number
                    if (!decimal.TryParse(updatedSalary, out decimal salary) || salary <= 0)
                    {
                        MessageBox.Show("Please enter a valid positive salary.");
                        return;
                    }

                    // Check if the new phone number already exists (except for the selected employee)
                    SqlCommand checkPhoneCmd = new SqlCommand("SELECT COUNT(*) FROM [Employee List] WHERE [Phone Number] = @Phone AND ID <> @ID", con);
                    checkPhoneCmd.Parameters.AddWithValue("@Phone", updatedPhone);
                    checkPhoneCmd.Parameters.AddWithValue("@ID", selectedID);

                    int phoneCount = (int)checkPhoneCmd.ExecuteScalar();
                    if (phoneCount > 0)
                    {
                        MessageBox.Show("This phone number is already in use. Please enter a unique phone number.");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE [Employee List] SET [Employee Name] = @Name, [Salary] = @Salary, [Phone Number] = @PhoneNumber, [Gender] = @Gender WHERE ID = @ID",
                        con
                    );
                    cmd.Parameters.AddWithValue("@ID", selectedID);
                    cmd.Parameters.AddWithValue("@Name", updatedName);
                    cmd.Parameters.AddWithValue("@Salary", updatedSalary);
                    cmd.Parameters.AddWithValue("@PhoneNumber", updatedPhone);
                    cmd.Parameters.AddWithValue("@Gender", updatedGender);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data updated successfully.");
                        LoadData(); // Refresh DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Employee not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    if (con != null && con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                ResetFields();
            }
            else
            {
                MessageBox.Show("Please select an employee to update.");
            }
        }

        private void button3_Click(object sender, EventArgs e)//Delete
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this record?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        ConnectDatabase();

                        int selectedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);
                        string name = dataGridView1.SelectedRows[0].Cells["Employee Name"].Value.ToString();
                        string salary = dataGridView1.SelectedRows[0].Cells["Salary"].Value.ToString();
                        string phoneNumber = dataGridView1.SelectedRows[0].Cells["Phone Number"].Value.ToString();
                        string gender = dataGridView1.SelectedRows[0].Cells["Gender"].Value.ToString();

                        // Insert deleted employee into DelEmp table
                        SqlCommand insertCmd = new SqlCommand(
                            "INSERT INTO DelEmp ([Employee Name], [Salary], [Phone Number], [Gender]) VALUES (@Name, @Salary, @PhoneNumber, @Gender)",
                            con
                        );
                        insertCmd.Parameters.AddWithValue("@Name", name);
                        insertCmd.Parameters.AddWithValue("@Salary", salary);
                        insertCmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        insertCmd.Parameters.AddWithValue("@Gender", gender);
                        insertCmd.ExecuteNonQuery();

                        // Delete from Employee List
                        SqlCommand deleteCmd = new SqlCommand("DELETE FROM [Employee List] WHERE Id = @Id", con);
                        deleteCmd.Parameters.AddWithValue("@Id", selectedID);
                        deleteCmd.ExecuteNonQuery();

                        MessageBox.Show("Employee deleted Successful.");
                        LoadData(); // Refresh Employee List
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void button4_Click(object sender, EventArgs e)//Reset
        {
            ResetFields();

        }

        private void button5_Click(object sender, EventArgs e)//Home
        {
            Login login = new Login();

            // Subscribe to the FormClosed event of Form2
            login.FormClosed += (s, args) => this.Show();

            // Show Form2
            login.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)//Back
        {
            Admin admin = new Admin();

           //Show Form1
            admin.Show();
            //Close Form2
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)//Head Line
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//Table
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Get column name
                string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

                // Clear previous selections
                ResetFields();

                // Fill only the selected field
                if (columnName == "Employee Name")
                {
                    textBox2.Text = row.Cells[e.ColumnIndex].Value.ToString();
                }
                else if (columnName == "Salary")
                {
                    textBox1.Text = row.Cells[e.ColumnIndex].Value.ToString();
                }
                else if (columnName == "Phone Number")
                {
                    textBox4.Text = row.Cells[e.ColumnIndex].Value.ToString();
                }
                else if (columnName == "Gender")
                {
                    string gender = row.Cells[e.ColumnIndex].Value.ToString();
                    radioButton1.Checked = (gender == "Male");
                    radioButton2.Checked = (gender == "Female");
                    radioButton3.Checked = (gender == "Other");
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                ConnectDatabase();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Employee List]", con);
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

        private void label9_Click(object sender, EventArgs e)//Gender
        {

        }
        private string GetSelectedGender()
        {
            if (radioButton1.Checked) return "Male";
            if (radioButton2.Checked) return "Female";
            if (radioButton3.Checked) return "Other";
            return string.Empty;
        }

        private bool ValidateInputs(bool isUpdate = false)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text)) // Employee Name
            {
                MessageBox.Show("Employee Name cannot be empty.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text)) // Salary
            {
                MessageBox.Show("Salary cannot be empty.");
                return false;
            }

            if (!int.TryParse(textBox1.Text, out int salary) || salary < 0)
            {
                MessageBox.Show("Salary must be a positive whole number.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text)) //Phone Number
            {
                MessageBox.Show("Phone Number cannot be empty.");
                return false;
            }

            if (!textBox4.Text.StartsWith("01") || textBox4.Text.Length != 11)
            {
                MessageBox.Show("Phone Number must start with '01' and be exactly 11 digits long.");
                return false;
            }

            if (!long.TryParse(textBox4.Text, out _))
            {
                MessageBox.Show("Phone Number must contain only numeric digits.");
                return false;
            }

            if (IsPhoneNumberDuplicate(textBox4.Text, isUpdate))
            {
                MessageBox.Show("This phone number is already in use by another employee. Please use a unique phone number.");
                return false;
            }

            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            {
                MessageBox.Show("Please select a gender.");
                return false;
            }

            return true;
        }

        private void label3_Click(object sender, EventArgs e)//Phone Number
        {

        }
        private bool IsPhoneNumberDuplicate(string phoneNumber, bool isUpdate = false)
        {
            try
            {
                ConnectDatabase();
                SqlCommand cmd;

                if (isUpdate)
                {
                    cmd = new SqlCommand("SELECT COUNT(*) FROM [Employee List] WHERE [Phone Number] = @PhoneNumber AND Id != @Id", con);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@Id", dataGridView1.SelectedRows[0].Cells[0].Value);
                }
                else
                {
                    cmd = new SqlCommand("SELECT COUNT(*) FROM [Employee List] WHERE [Phone Number] = @PhoneNumber", con);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                }

                int count = (int)cmd.ExecuteScalar();
                con.Close();
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking duplicate phone number: " + ex.Message);
                con.Close();
                return true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)//Search
        {
            string searchTerm = textBox3.Text.Trim(); // Get the search term from the text box

            if (!string.IsNullOrEmpty(searchTerm))
            {
                try
                {
                    ConnectDatabase();

                    // SQL query to search by name or phone number
                    string query = @"
                SELECT * FROM [Employee List]
                WHERE [Employee Name] LIKE @SearchTerm OR [Phone Number] LIKE @SearchTerm";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Set the DataGridView data source to the filtered data
                    dataGridView1.DataSource = dt;

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                // If search term is empty, load all data
                LoadData();
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)//Search
        {
            button7_Click(sender, e);
        }
    }
}
