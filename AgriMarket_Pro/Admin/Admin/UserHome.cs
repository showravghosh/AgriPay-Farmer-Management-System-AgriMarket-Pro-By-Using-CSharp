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
    public partial class UserHome : Form
    {
        public UserHome()
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
            UserVegetable uv = new UserVegetable();
            uv.Show();
        }

        private void buttonFish_Click(object sender, EventArgs e)//Fish
        {
            this.Hide();

            // Create an instance of Form2 and show it
            UserFish uf = new UserFish();
            uf.Show();
        }

        private void buttonFruits_Click(object sender, EventArgs e)//Fruits
        {
            this.Hide();

            // Create an instance of Form2 and show it
            UserFruit ufr = new UserFruit();
            ufr.Show();
        }

        private void UserHome_Load(object sender, EventArgs e)
        {

        }
    }
}
