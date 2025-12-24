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
    public partial class UserVegetable : Form
    {
        SqlConnection con;

        public UserVegetable()
        {
            InitializeComponent();
        }

        private void dbcon()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
        }

        // Method to load the Vegetable table data into DataGridView
        private void LoadVegetableTable()
        {
            try
            {
                dbcon();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Vegetable", con);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                CalculateTotalAmount();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading table: {ex.Message}");
            }
        }

        private void CalculateTotalAmount()
        {
            try
            {
                dbcon();
                SqlCommand cmd = new SqlCommand("SELECT SUM(CAST([Total Price] AS DECIMAL(10,2))) FROM Vegetable", con);
                object result = cmd.ExecuteScalar();
                con.Close();

                textBox1.Text = result != DBNull.Value ? Convert.ToDecimal(result).ToString("0.00") : "0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating total amount: {ex.Message}");
            }
        }


        // Method to add products to the Vegetable table
        private void AddProductToDatabase(string productName, decimal price, int quantity)
        {
            try
            {
                dbcon();
                decimal totalAmount = price * quantity;

                SqlCommand cmd = new SqlCommand("INSERT INTO Vegetable ([Product Name], [Product Quantity], [Product Price], [Total Price]) VALUES (@Name, @Quantity, @Price, @Total)", con);
                cmd.Parameters.AddWithValue("@Name", productName);
                cmd.Parameters.AddWithValue("@Quantity", quantity.ToString());
                cmd.Parameters.AddWithValue("@Price", price.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Total", totalAmount);

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show($"{productName} added to the database successfully.");
                LoadVegetableTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product to the database: {ex.Message}");
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
            this.Hide();

            // Create an instance of Form2 and show it
            UserHome uh = new UserHome();
            uh.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal price = 3;
            int quantity = (int)numericUpDown1.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Mushroom", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            decimal price = 1.5M;
            int quantity = (int)numericUpDown4.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Papaya", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            decimal price = 2;
            int quantity = (int)numericUpDown9.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Pointed Gourd", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            decimal price = 2.5M;
            int quantity = (int)numericUpDown2.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Cabbage", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            decimal price = 3;
            int quantity = (int)numericUpDown5.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Bottle Gourd", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            decimal price = 2.2M;
            int quantity = (int)numericUpDown8.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Cucumber", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            decimal price = 1.8M;
            int quantity = (int)numericUpDown3.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Radish", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            decimal price = 3.5M;
            int quantity = (int)numericUpDown6.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Sweet Gourd", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            decimal price = 4;
            int quantity = (int)numericUpDown7.Value;
            if (quantity > 0)
            {
                AddProductToDatabase("Capsicum", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {

        }

        private void DeleteVegetableTable()
        {
            try
            {
                dbcon();
                SqlCommand cmd = new SqlCommand("DELETE FROM Vegetable", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting table data: {ex.Message}");
            }
        }

        private void UserVegetable_Load(object sender, EventArgs e)
        {
            DeleteVegetableTable();
            LoadVegetableTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Total Amount
        {

        }
    }
    
}
