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
    public partial class UserFish : Form
    {
        SqlConnection con;

        public UserFish()
        {
            InitializeComponent();
        }

        private void dbcon()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
        }

        private void UserFish_Load(object sender, EventArgs e)
        {
            DeleteFishTable();
            LoadFishTable();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            decimal price = 10; // Price per KG
            int quantity = (int)numericUpDown1.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Kural", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            decimal price = 4;
            int quantity = (int)numericUpDown4.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Prawn", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            decimal price = 1.9M;
            int quantity = (int)numericUpDown9.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Pangasius", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            decimal price = 4.5M;
            int quantity = (int)numericUpDown2.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Wallago", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            decimal price = 15;
            int quantity = (int)numericUpDown5.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Hilsa", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            decimal price = 4.5M;
            int quantity = (int)numericUpDown8.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Mrigal Carp", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            decimal price = 3.5M;
            int quantity = (int)numericUpDown3.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("St. Peter's", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            decimal price = 5.5M;
            int quantity = (int)numericUpDown6.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Rui", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            decimal price = 9;
            int quantity = (int)numericUpDown7.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Tuna", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void LoadFishTable()
        {
            dbcon();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Fish", con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            CalculateTotalAmount();

        }

        private void CalculateTotalAmount()
        {
            try
            {
                dbcon();
                SqlCommand cmd = new SqlCommand("SELECT SUM(CAST([Total Price] AS DECIMAL(10,2))) FROM Fish", con);
                object result = cmd.ExecuteScalar();
                con.Close();

                // Display the total amount in the textbox
                textBox1.Text = result != DBNull.Value ? Convert.ToDecimal(result).ToString("0.00") : "0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating total amount: {ex.Message}");
            }
        }

        private void AddProductToDatabase(string productName, decimal price, int quantity)
        {
            try
            {
                dbcon(); // Ensure the database connection is opened
                decimal totalAmount = price * quantity;

                // SQL query to insert the product data
                SqlCommand cmd = new SqlCommand("INSERT INTO Fish ([Product Name], [Product Quantity], [Product Price], [Total Price]) VALUES (@Name, @Quantity, @Price, @Total)", con);
                cmd.Parameters.AddWithValue("@Name", productName);
                cmd.Parameters.AddWithValue("@Quantity", quantity.ToString());
                cmd.Parameters.AddWithValue("@Price", price.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Total", totalAmount);

                // Execute the query
                cmd.ExecuteNonQuery();
                con.Close(); // Close the connection

                // Show a success message
                MessageBox.Show($"{productName} added to the database successfully.");

                // Reload the DataGridView to reflect the new data
                LoadFishTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product to the database: {ex.Message}");
            }
        }

        private void DeleteFishTable()
        {
            try
            {
                dbcon();
                SqlCommand cmd = new SqlCommand("DELETE FROM Fish", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting table data: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            // Subscribe to the FormClosed event of Form2
            login.FormClosed += (s, args) => this.Show();

            // Show Form2
            login.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Create an instance of Form2 and show it
            UserHome uh = new UserHome();
            uh.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Total Amount
        {

        }


        private void button2_Click(object sender, EventArgs e)//Payment
        {
            decimal totalAmount;

            // Try parsing the total amount from textBox1
            if (!decimal.TryParse(textBox1.Text, out totalAmount) || totalAmount < 1)
            {
                MessageBox.Show("Total amount must be at least 1 to proceed with payment.");
                return;
            }

            UserPayHome userPayHome = new UserPayHome(this, totalAmount);
            this.Hide(); // Hide the current form (UserFruit)
            userPayHome.Show();
        }
    }
}
