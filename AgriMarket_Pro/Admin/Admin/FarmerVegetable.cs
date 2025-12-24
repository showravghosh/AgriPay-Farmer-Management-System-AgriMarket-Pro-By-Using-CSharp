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
    public partial class FarmerVegetable : Form
    {
        SqlConnection con;

        public FarmerVegetable()
        {
            InitializeComponent();
        }

        private void dbcon()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employee.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
        }

        // Method to load the UVegetabl table data into DataGridView
        private void LoadUVegetableTable()
        {
            try
            {
                dbcon();
                SqlCommand cmd = new SqlCommand("SELECT * FROM UVegetabl", con);
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
                SqlCommand cmd = new SqlCommand("SELECT SUM(CAST([Total Price] AS DECIMAL(10,2))) FROM UVegetabl", con);
                object result = cmd.ExecuteScalar();
                con.Close();

                textBox1.Text = result != DBNull.Value ? Convert.ToDecimal(result).ToString("0.00") : "0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating total amount: {ex.Message}");
            }
        }


        // Method to sell products and add them to the UVegetabl table
        private void SellProduct(string productName, decimal price, int quantity)
        {
            try
            {
                dbcon();
                decimal totalAmount = price * quantity;

                SqlCommand cmd = new SqlCommand("INSERT INTO UVegetabl ([Product Name], [Product Quantity], [Product Price ], [Total Price]) VALUES (@Name, @Quantity, @Price, @Total)", con);
                cmd.Parameters.AddWithValue("@Name", productName);
                cmd.Parameters.AddWithValue("@Quantity", quantity.ToString());
                cmd.Parameters.AddWithValue("@Price", price.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Total", totalAmount);

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show($"{productName} sold successfully.");
                LoadUVegetableTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selling product: {ex.Message}");
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
            FarmerHome fh = new FarmerHome();
            fh.Show();
        }

        private void DeleteUVegetableTable()
        {
            try
            {
                dbcon();
                SqlCommand cmd = new SqlCommand("DELETE FROM UVegetabl", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting table data: {ex.Message}");
            }
        }

        private void FarmerVegetable_Load(object sender, EventArgs e)
        {
            DeleteUVegetableTable();
            LoadUVegetableTable();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal price = 3;
            int quantity = (int)numericUpDown1.Value;
            if (quantity > 0)
            {
                SellProduct("Mushroom", price, quantity);
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
            decimal price = 1.5M;
            int quantity = (int)numericUpDown4.Value;
            if (quantity > 0)
            {
                SellProduct("Papaya", price, quantity);
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
            decimal price = 2;
            int quantity = (int)numericUpDown9.Value;
            if (quantity > 0)
            {
                SellProduct("Pointed Gourd", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            decimal price = 2.5M;
            int quantity = (int)numericUpDown2.Value;
            if (quantity > 0)
            {
                SellProduct("Cabbage", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            decimal price = 3;
            int quantity = (int)numericUpDown5.Value;
            if (quantity > 0)
            {
                SellProduct("Bottle Gourd", price, quantity);
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
            decimal price = 2.2M;
            int quantity = (int)numericUpDown8.Value;
            if (quantity > 0)
            {
                SellProduct("Cucumber", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            decimal price = 1.8M;
            int quantity = (int)numericUpDown3.Value;
            if (quantity > 0)
            {
                SellProduct("Radish", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            decimal price = 3.5M;
            int quantity = (int)numericUpDown6.Value;
            if (quantity > 0)
            {
                SellProduct("Sweet Gourd", price, quantity);
            }
            else
            {
                MessageBox.Show("Please select a valid quantity.");
            }
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            decimal price = 4;
            int quantity = (int)numericUpDown7.Value;
            if (quantity > 0)
            {
                SellProduct("Capsicum", price, quantity);
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

        private string GetLoggedInPhoneNumber()
        {
            Random random = new Random();
            // List of possible prefixes
            string[] prefixes = { "013", "014", "019", "016", "017", "018" };

            // Select a random prefix
            string prefix = prefixes[random.Next(prefixes.Length)];

            // Generate a random 8-digit number to complete the phone number (to make it 11 digits)
            string randomNumber = random.Next(10000000, 100000000).ToString();

            // Combine the prefix with the random 8-digit number
            return prefix + randomNumber;
        }

        private void button2_Click(object sender, EventArgs e)//Payment Request
        {
            try
            {
                dbcon();

                // Retrieve the logged-in phone number from session or database
                string phoneNumber = GetLoggedInPhoneNumber();

                // Get total amount
                decimal totalAmount;
                if (!decimal.TryParse(textBox1.Text, out totalAmount) || totalAmount <= 0)
                {
                    MessageBox.Show("Total amount must be greater than 0.");
                    return;
                }

                // Insert data into Unpaid table
                SqlCommand cmd = new SqlCommand("INSERT INTO Unpaid ([Phone Number], [Total Amount]) VALUES (@Phone, @Amount)", con);
                cmd.Parameters.AddWithValue("@Phone", phoneNumber);
                cmd.Parameters.AddWithValue("@Amount", totalAmount.ToString("0.00"));
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Payment request submitted successfully.");


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing payment request: {ex.Message}");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Total Amount
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
