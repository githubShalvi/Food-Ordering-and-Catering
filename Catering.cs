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
    public partial class Catering : Form
    {
        public Catering()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/maps/place/17%C2%B020'56.3%22N+78%C2%B030'52.4%22E/@17.348979,78.514544,17z/data=!3m1!4b1!4m4!3m3!8m2!3d17.348979!4d78.514544?entry=ttu");
        }
    }
}
