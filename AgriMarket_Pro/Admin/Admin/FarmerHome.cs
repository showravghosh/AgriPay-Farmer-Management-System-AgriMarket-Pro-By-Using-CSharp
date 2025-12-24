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
    public partial class FarmerHome : Form
    {
        public FarmerHome()
        {
            InitializeComponent();
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

        private void buttonVegetable_Click(object sender, EventArgs e)//Vegetable
        {

            this.Hide();

            // Create an instance of Form2 and show it
            FarmerVegetable fv = new FarmerVegetable();
            fv.Show();
        }

        private void buttonFish_Click(object sender, EventArgs e)//Fish
        {
            this.Hide();

            // Create an instance of Form2 and show it
            FarmerFish ff = new FarmerFish();
            ff.Show();
        }

        private void buttonFruits_Click(object sender, EventArgs e)//Fruit
        {
            this.Hide();

            // Create an instance of Form2 and show it
            FarmerFruit ffr = new FarmerFruit();
            ffr.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FarmerHome_Load(object sender, EventArgs e)
        {

        }
    }
}
