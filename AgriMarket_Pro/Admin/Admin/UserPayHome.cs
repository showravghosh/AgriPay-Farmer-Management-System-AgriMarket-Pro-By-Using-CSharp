using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin
{
    public partial class UserPayHome : Form
    {
        private Form _sourceForm;
        private static Form _lastValidSourceForm;
        private decimal _totalAmount;


        public UserPayHome(Form sourceForm, decimal totalAmount)
        {
            InitializeComponent();
            _totalAmount = totalAmount;
            _sourceForm = sourceForm;

            if (_sourceForm is UserFruit || _sourceForm is UserFish || _sourceForm is UserVegetable)
            {
                _lastValidSourceForm = _sourceForm; // Store the valid form
            }


            textBox1.Text = _totalAmount.ToString("0.00");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Pass the total amount to Bkash form
            Bkash bk = new Bkash(_lastValidSourceForm, _totalAmount);
            bk.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Create an instance of Form2 and show it
            Nagad nag = new Nagad(_lastValidSourceForm, _totalAmount);
            nag.Show();
        }

        private void UserPayHome_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)//Back
        {
            if (_lastValidSourceForm != null)
            {
                // Go back to the last valid form (UserFruit, UserFish, or UserVegetable)
                _lastValidSourceForm.Show();
            }
            else
            {
                // If no valid source form was tracked, show a message or handle accordingly
                MessageBox.Show("This action is not allowed from the current form.");
            }

            this.Close(); // Close current form after navigating back
        }

        private void button7_Click(object sender, EventArgs e)//Home
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
            Rocket roc = new Rocket(_lastValidSourceForm, _totalAmount);
            roc.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Create an instance of Form2 and show it
            CityBank cb = new CityBank(_lastValidSourceForm, _totalAmount);
            cb.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Create an instance of Form2 and show it
            BracBank bb = new BracBank(_lastValidSourceForm, _totalAmount);
            bb.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Total Amount
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
