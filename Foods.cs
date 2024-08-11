using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Project1
{
    public partial class Foods : Form
    {
        public Foods()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Panipuri")
            {
                label6.Text = "20";
            }

            else if (comboBox1.Text == "Bhel")
            { label6.Text = "30"; }
            else if (comboBox1.Text == "Dahibhalla")
            { label6.Text = "60"; }
            else if (comboBox1.Text == "Noodles")
            { label6.Text = "129"; }
            else if (comboBox1.Text == "Burger")
            { label6.Text = "120"; }
            else if (comboBox1.Text == "Paneer65")
            { label6.Text = "200"; }
            else if (comboBox1.Text == "AlooTikki")
            { label6.Text = "80"; }
            else if (comboBox1.Text == "CutDosa")
            { label6.Text = "100"; }
            else if (comboBox1.Text == "Pongal")
            { label6.Text = "40"; }
            else if (comboBox1.Text == "DalBaati")
            { label6.Text = "230"; }

            else
            { label6.Text = "100"; }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "")
            { MessageBox.Show("Enter food details to order!"); }

            else
            {
                int a = Convert.ToInt32(comboBox2.Text);
                int b = Convert.ToInt32(label6.Text);
                int result = 0;
                for (int i = 0; i < b; i++)
                {
                    result += a;
                }
                label8.Text = result.ToString();
                label8.Visible = false;
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);
                string query = "insert into FOODS values (@FOOD_ITEM,@QUANTITY,@PRICE_PER_ITEM,@TOTAL)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@FOOD_ITEM", comboBox1.Text);
                cmd.Parameters.Add("@QUANTITY", comboBox2.Text);
                cmd.Parameters.Add("@PRICE_PER_ITEM", label6.Text);
                cmd.Parameters.Add("@TOTAL", label8.Text);


                conn.Open();
                cmd.ExecuteNonQuery();
                display();
                label9.Text = comboBox1.Text + " added successfully!!";
                label9.Visible = true;
                conn.Close();
                //comboBox1.ResetText();
                //comboBox2.ResetText();
                label6.Text = "0";
                
            }
        }
        public void display()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(cs);
            string q = "select FOOD_ITEM,QUANTITY,PRICE_PER_ITEM,TOTAL from FOODS";
            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                errorProvider1.SetError(this.comboBox1, "This field cant be empty!");
            }
            else
            {
                errorProvider1.Clear();
            }
            /****************/
            if(comboBox1.Text=="")
            {
                label6.Text = "0";
            }
            else if (!comboBox1.Items.Contains(comboBox1.Text))
            {
                //comboBox1.Items.Add(comboBox1.Text);
                //label6.Text = "150";
                MessageBox.Show("Error:"+" We apologize for the unavailability of the dish you selected...Will look forward to serve you better next time","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                comboBox1.ResetText();
            }
            else
            {
                if (comboBox1.Text == "Panipuri")
                { label6.Text = "20"; }
                else if (comboBox1.Text == "Bhel")
                { label6.Text = "30"; }
                else if (comboBox1.Text == "Dahibhalla")
                { label6.Text = "60"; }
                else if (comboBox1.Text == "Noodles")
                { label6.Text = "129"; }
                else if (comboBox1.Text == "Burger")
                { label6.Text = "120"; }
                else if (comboBox1.Text == "Paneer65")
                { label6.Text = "200"; }
                else if (comboBox1.Text == "AlooTikki")
                { label6.Text = "80"; }
                else if (comboBox1.Text == "CutDosa")
                { label6.Text = "100"; }
                else if (comboBox1.Text == "Pongal")
                { label6.Text = "40"; }
                else if (comboBox1.Text == "DalBaati")
                { label6.Text = "230"; }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label9.Visible = false;
            if (comboBox1.Text == "" || comboBox2.Text == "")
            { MessageBox.Show("Enter food details to remove item!"); }
            else
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                string query2 = "select FOOD_ITEM from FOODS where FOOD_ITEM=@FOOD_ITEM and QUANTITY=@QUANTITY";
                SqlCommand checkfood_item = new SqlCommand(query2, conn);
                checkfood_item.Parameters.Add("@FOOD_ITEM", comboBox1.Text);
                checkfood_item.Parameters.Add("@QUANTITY", comboBox2.Text);
                string item = (string)checkfood_item.ExecuteScalar();
                if (item == comboBox1.Text)
                {
                    string query = "delete from FOODS where FOOD_ITEM=@FOOD_ITEM and QUANTITY=@QUANTITY";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@FOOD_ITEM", comboBox1.Text);
                    cmd.Parameters.Add("@QUANTITY", comboBox2.Text);
                    cmd.Parameters.Add("@PRICE_PER_ITEM", label6.Text);
                    cmd.ExecuteNonQuery();
                    display();
                    label9.Text = comboBox1.Text + " removed successfully!!";
                    label9.Visible = true;
                }
                else
                {
                    MessageBox.Show( "Match not found!");
                }

                conn.Close();
                //comboBox1.ResetText();
                //comboBox2.ResetText();
                label6.Text = "0";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label9.Visible = false;
            if (comboBox1.Text == "" || comboBox2.Text == "")
            { MessageBox.Show("Enter food details to update item!"); }
            else
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);
                string query = "update FOODS set TOTAL=QUANTITY*PRICE_PER_ITEM,QUANTITY=@QUANTITY where FOOD_ITEM=@FOOD_ITEM";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@FOOD_ITEM", comboBox1.Text);
                //cmd.Parameters.Add("@TOTAL", label8.Text);
                cmd.Parameters.Add("@QUANTITY", comboBox2.Text);
                cmd.Parameters.Add("@PRICE_PER_ITEM", label6.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                display();
                label9.Text = comboBox1.Text + " updated successfully!!";
                label9.Visible = true;
                conn.Close();
                //comboBox1.ResetText();
                //comboBox2.ResetText();
                label6.Text = "0";
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            label9.ResetText();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Please add food to place order!");
            }
            else
            {
                PlaceOrder1 place = new PlaceOrder1();
                place.Show();
                this.Hide();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                errorProvider2.SetError(this.comboBox2, "This field cant be empty!");
            }
            else if (comboBox2.Text == "1" || comboBox2.Text == "2" || comboBox2.Text == "3" || comboBox2.Text == "4" || comboBox2.Text == "5")
            {
                errorProvider2.Clear();
            }
            else
            {
                errorProvider2.Clear();
            }

        }
    }
}

