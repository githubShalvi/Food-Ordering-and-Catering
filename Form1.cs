using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Foods foods=new Foods();
            foods.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Drinks drinks=new Drinks();
            drinks.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Catering catering =new Catering();
            catering.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.BringToFront();
            
        }
    }
}
