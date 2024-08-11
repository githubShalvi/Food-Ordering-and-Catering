using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Menucard : Form
    {
        public Menucard()
        {
            InitializeComponent();
        }

        private void Menucard_Load(object sender, EventArgs e)
        {
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
                label6.Text = bill;
                label6.Visible = true;
            }
            reader.Close();
            /***********************/
            string q1 = "SELECT SUM(TOTAL) AS BILLD FROM DRINK;";
            SqlDataAdapter da1 = new SqlDataAdapter(q1, conn);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            //if (dt.Rows.Count > 0)
            //{
            //    dataGridView1.DataSource = dt;
            //}
            SqlCommand command1 = new SqlCommand(q1, conn);
            SqlDataReader reader1 = command1.ExecuteReader();

            string bill1;
            while (reader1.Read())
            {
                bill1 = reader1["BILLD"].ToString();
                label7.Text = bill1;
                label7.Visible = true;
            }
            reader1.Close();
            /*************************/
            int a = Convert.ToInt32(label6.Text);
            int b = Convert.ToInt32(label7.Text);
            int res = Convert.ToInt32(a + b);
            label8.Text = res.ToString();
            label8.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn = new SqlConnection(cs);
            string q = "select * from FOODS union all select * from DRINK";
            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

            string cs1 = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection conn1 = new SqlConnection(cs1);
            string query1 = "truncate table DRINK";
            SqlCommand cmd1 = new SqlCommand(query1, conn1);
            //cmd1.Parameters.Add("@DRINKS_ITEM", comboBox1.Text);
            //cmd1.Parameters.Add("@QUANTITY", comboBox2.Text);
            //cmd1.Parameters.Add("@PRICE_PER_ITEM", label9.Text);
            conn1.Open();
            cmd1.ExecuteNonQuery();
            conn1.Close();
            Confirm con = new Confirm();
            con.Show();
            this.Hide();
        }
    }
}
