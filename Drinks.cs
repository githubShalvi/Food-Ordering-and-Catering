using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Drinks : Form
    {
        public Drinks()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Please add drinks to place order!");
            }
            else
            {
                PlaceOrder2 place2 = new PlaceOrder2();
                place2.Show();
                this.Hide();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!comboBox1.Items.Contains(comboBox1.Text))
            //{
            //    comboBox1.Items.Add(comboBox1.Text);
            //    label6.Text = "150";
            //}
            //else
            //{
            //    if (comboBox1.Text == "Mojito")
            //    { label6.Text = "230"; }
            //    else if (comboBox1.Text == "Iced Tea")
            //    { label6.Text = "200"; }
            //    else if (comboBox1.Text == "Green Tea")
            //    { label6.Text = "70"; }
            //    else if (comboBox1.Text == "Lemon Tea")
            //    { label6.Text = "120"; }
            //    else if (comboBox1.Text == "Coca Cola")
            //    { label6.Text = "80"; }
            //    else if (comboBox1.Text == "Mirinda")
            //    { label6.Text = "68"; }
            //    else if (comboBox1.Text == "Sprite")
            //    { label6.Text = "40"; }
            //    //else if (comboBox1.Text == "")
            //    //{ label9.Text = "0"; }
            //}
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
                label7.Text = result.ToString();
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);
                string query = "insert into DRINK values (@DRINKS_ITEM,@QUANTITY,@PRICE_PER_ITEM,@TOTAL)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@DRINKS_ITEM", comboBox1.Text);
                cmd.Parameters.Add("@QUANTITY", comboBox2.Text);
                cmd.Parameters.Add("@PRICE_PER_ITEM", label6.Text);
                cmd.Parameters.Add("@TOTAL", label7.Text);


                conn.Open();
                cmd.ExecuteNonQuery();
                display();
                label8.Text = comboBox1.Text + " added successfully!!";
                label8.Visible = true;
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
            string q = "select DRINKS_ITEM,QUANTITY,PRICE_PER_ITEM,total from DRINK";
            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ERROR PROVIDER PAHIJ NA ETH
            //if (comboBox2.Text == "")
            //{
            //    label8.Text = "This field cant be empty! ";
            //    label8.Visible = true;
            //}
            //else if (comboBox2.Text == "1" || comboBox2.Text == "2" || comboBox2.Text == "3" || comboBox2.Text == "4" || comboBox2.Text == "5")
            //{
            //    label8.Visible = false;
            //}
            //else
            //{ 
            //    label8.Text = "Smthng went wrong.. ";
            //    label8.Visible = true;
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            if (comboBox1.Text == "" || comboBox2.Text == "")
            { MessageBox.Show("Enter food details to remove item!"); }
            else
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                string query2 = "select DRINKS_ITEM from DRINK where DRINKS_ITEM=@DRINKS_ITEM and QUANTITY=@QUANTITY";
                SqlCommand checkfood_item = new SqlCommand(query2, conn);
                checkfood_item.Parameters.Add("@DRINKS_ITEM", comboBox1.Text);
                checkfood_item.Parameters.Add("@QUANTITY", comboBox2.Text);

                string item = (string)checkfood_item.ExecuteScalar();
                if (item == comboBox1.Text)
                {
                    string query = "delete from DRINK where DRINKS_ITEM=@DRINKS_ITEM and QUANTITY=@QUANTITY";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@DRINKS_ITEM", comboBox1.Text);
                    cmd.Parameters.Add("@QUANTITY", comboBox2.Text);
                    cmd.Parameters.Add("@PRICE_PER_ITEM", label6.Text);
                    cmd.ExecuteNonQuery();
                    display();
                    label8.Text = comboBox1.Text + " removed successfully!!";
                    label8.Visible = true;
                }
                else
                {
                    MessageBox.Show("Match not found !");
                }

                conn.Close();
                //comboBox1.ResetText();
                //comboBox2.ResetText();
                label6.Text = "0";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label8.Visible = false;
            if (comboBox1.Text == "" || comboBox2.Text == "")
            { MessageBox.Show("Enter food details to update item!"); }
            else
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);
                string query = "update DRINK set QUANTITY=@QUANTITY where DRINKS_ITEM=@DRINKS_ITEM";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@DRINKS_ITEM", comboBox1.Text);
                cmd.Parameters.Add("@QUANTITY", comboBox2.Text);
                cmd.Parameters.Add("@PRICE_PER_ITEM", label6.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                display();
                label3.Text = comboBox1.Text + " updated successfully!!";
                label3.Visible = true;
                conn.Close();
                comboBox1.ResetText();
                comboBox2.ResetText();
                label6.Text = "0";
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
            /*******************/
            if (comboBox1.Text == "")
            {
                label6.Text = "0";
            }
            else if (!comboBox1.Items.Contains(comboBox1.Text))
            {
                MessageBox.Show("Error:" + " We apologize for the unavailability of the dish you selected...Will look forward to serve you better next time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.ResetText();
            }
            else
            {
                if (comboBox1.Text == "Mojito")
                { label6.Text = "230"; }
                else if (comboBox1.Text == "Iced Tea")
                { label6.Text = "200"; }
                else if (comboBox1.Text == "Green Tea")
                { label6.Text = "70"; }
                else if (comboBox1.Text == "Lemon Tea")
                { label6.Text = "120"; }
                else if (comboBox1.Text == "Coca Cola")
                { label6.Text = "80"; }
                else if (comboBox1.Text == "Mirinda")
                { label6.Text = "68"; }
                else if (comboBox1.Text == "Sprite")
                { label6.Text = "40"; }

            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            label8.ResetText();
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
