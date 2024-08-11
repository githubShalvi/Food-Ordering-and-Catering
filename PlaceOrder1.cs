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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class PlaceOrder1 : Form
    {
        public PlaceOrder1()
        {
            InitializeComponent();
        }

        private void PlaceOrder1_Load(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            Foods foods=new Foods();
            foods.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                label11.Text = "We need receipt to count total..";
                label11.Visible = true;
            }
            else
            {
                dataGridView1.Visible = false;
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                string q = "SELECT SUM(TOTAL) AS BILL FROM FOODS;";
                SqlDataAdapter da = new SqlDataAdapter(q, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    dataGridView1.DataSource = dt;
                //}
                SqlCommand command = new SqlCommand(q, conn);
                SqlDataReader reader = command.ExecuteReader();
                string bill;
                while (reader.Read())
                {
                    bill = reader["BILL"].ToString();
                    label9.Text = bill;

                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                label11.Text = "Fill details to view receipt..";
                label11.Visible = true;
            }
            else
            {
                label11.Visible = false;
                dataGridView1.Visible = true;
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                string q = "SELECT * FROM FOODS";
                SqlDataAdapter da = new SqlDataAdapter(q, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                conn.Close();
                checkcustomer();
                addcustomer();


            }
        }
        public void addcustomer()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(cs);
            string query = "insert into CUSTOMERS (CUST_NAME,ADDRESS,PHONE,PAYMENT) values (@CUST_NAME,@ADDRESS,@PHONE,@PAYMENT)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@CUST_NAME", textBox1.Text);
            cmd.Parameters.Add("@ADDRESS", comboBox1.Text);
            cmd.Parameters.Add("@PHONE", textBox2.Text);
            cmd.Parameters.Add("@PAYMENT", comboBox2.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            display();
            conn.Close();
        }
        public void checkcustomer()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(cs);
            string q = "select * from CUSTOMERS where CUST_NAME=@CUST_NAME";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.Parameters.Add("@CUST_NAME", textBox1.Text);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                MessageBox.Show("Hello" + textBox1.Text + "\nNice to see you again!..Happy Dinning!");
            }
            else
            {
                MessageBox.Show("Welcome" + textBox1.Text + "Hope your visit is just the beginning of a beautiful relationship with AARVI FOODS");
            }

        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Aurangabad" || comboBox1.Text == "Nanded" || comboBox1.Text == "Kolhapur" || comboBox1.Text == "Hyderabad" || comboBox1.Text == "Nagpur")
            {
                errorProvider1.Clear();
                label12.Text = "Valid!";
                label12.Visible = true;
            }
            else if (comboBox1.Text == "")
            {
                label12.Visible = false;
                errorProvider1.SetError(this.comboBox1, "Field cant be empty!");
            }
            else
            {
                label12.Visible = false;
                errorProvider1.SetError(this.comboBox1, "We dont have our branch at this location!");
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            string phonenumber = textBox2.Text;
            Regex regex = new Regex(@"^\d{3}-?\d{3}-?\d{4}$");
            if (!regex.IsMatch(phonenumber))
            {
                errorProvider2.SetError(textBox2, "Invalid phone number");
                e.Cancel = true;
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Something went wrong!");
            }
            else
            {
                Drinks drinks = new Drinks();
                drinks.Show();
                this.Hide();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Something went wrong!");
            }
            else
            {
                //string cs1 = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                //SqlConnection conn1 = new SqlConnection(cs1);
                //string query1 = "truncate table FOODS";
                //SqlCommand cmd1 = new SqlCommand(query1, conn1);
                //cmd1.Parameters.Add("@FOOD_ITEM", comboBox1.Text);
                //cmd1.Parameters.Add("@QUANTITY", comboBox2.Text);
                //cmd1.Parameters.Add("@PRICE_PER_ITEM", label9.Text);
                //conn1.Open();
                //cmd1.ExecuteNonQuery();
                //conn1.Close();
                Menucard menu = new Menucard();
                menu.Show();
                this.Hide();
            }
        }
    }
}
