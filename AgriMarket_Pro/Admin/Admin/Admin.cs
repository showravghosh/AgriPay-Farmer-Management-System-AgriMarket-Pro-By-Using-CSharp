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
    public partial class Admin : Form

    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //See Employee List
        {
            EmployeeList employeelist = new EmployeeList();

            // Subscribe to the FormClosed event of Form2
            employeelist.FormClosed += (s, args) => this.Show();

            // Show employeeList
            employeelist.Show();

            // Hide Admin
            this.Hide();
        }


        private void button2_Click(object sender, EventArgs e)//see Farmer List
        {
            FarmerList farmerlist = new FarmerList();

            // Subscribe to the FormClosed event of Form2
            farmerlist.FormClosed += (s, args) => this.Show();

            // Show Form2
            farmerlist.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)//Payment Frame
        {
            UnPaid up = new UnPaid();

            // Subscribe to the FormClosed event of Form2
            up.FormClosed += (s, args) => this.Show();

            // Show Form2
            up.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)//See User List
        {
           UserList userlist = new UserList();

            // Subscribe to the FormClosed event of Form2
            userlist.FormClosed += (s, args) => this.Show();

            // Show Form2
            userlist.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)///Add New Admin
        {
            AdminReg adminreg = new AdminReg();

            // Subscribe to the FormClosed event of Form2
            adminreg.FormClosed += (s, args) => this.Show();

            // Show Form2
            adminreg.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)//See Admin List
        {
            AdminList adminlist = new AdminList();

            // Subscribe to the FormClosed event of Form2
            adminlist.FormClosed += (s, args) => this.Show();

            // Show Form2
            adminlist.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)//See Products List
        {
            ProductsList productslist = new ProductsList();

            // Subscribe to the FormClosed event of Form2
            productslist.FormClosed += (s, args) => this.Show();

            // Show Form2
            productslist.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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

        private void button7_Click(object sender, EventArgs e)//Back
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            DeletedEmployee de = new DeletedEmployee();

            // Subscribe to the FormClosed event of Form2
            de.FormClosed += (s, args) => this.Show();

            // Show Form2
            de.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DeletedFarmer df = new DeletedFarmer();

            // Subscribe to the FormClosed event of Form2
            df.FormClosed += (s, args) => this.Show();

            // Show Form2
            df.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DeleteUser du = new DeleteUser();

            // Subscribe to the FormClosed event of Form2
            du.FormClosed += (s, args) => this.Show();

            // Show Form2
            du.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DeleteAdmin da = new DeleteAdmin();

            // Subscribe to the FormClosed event of Form2
            da.FormClosed += (s, args) => this.Show();

            // Show Form2
            da.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            UserPaid up = new UserPaid();

            // Subscribe to the FormClosed event of Form2
            up.FormClosed += (s, args) => this.Show();

            // Show Form2
            up.Show();

            // Hide Form1 instead of closing it
            this.Hide();
        }
    }
}
