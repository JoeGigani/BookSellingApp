using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loading
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\BookShopDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from BookTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            if(BillQtyTb.Text == "" || Convert.ToInt32(BillQtyTb.Text) > stock)
            {
                MessageBox.Show("No Enough Stock");
            }
            else
            {
                int total = Convert.ToInt32(BillQtyTb.Text) * Convert.ToInt32(BilPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = BTitleTb.Text;
                newRow.Cells[2].Value = BillQtyTb.Text;
                newRow.Cells[3].Value = BilPriceTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV .Rows.Add(newRow);
                n++;
            }
        }
        int key = 0,stock = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].ToString();
            //QtyTb.Text = BookDGV.SelectedRows[0].Cells[2].ToString();
            //BCatCb.SelectedItem = BookDGV.SelectedRows[0].Cells[3].ToString();
            BillQtyTb.Text = BookDGV.SelectedRows[0].Cells[4].ToString();
            BilPriceTb.Text = BookDGV.SelectedRows[0].Cells[5].ToString();
            if (BTitleTb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].ToString());
                stock = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[4].ToString());
            }
        }

        private void Reset()
        {
            BTitleTb.Text = "";
            BillQtyTb.Text = "";
            BilPriceTb.Text = "";
            ClientNameTb.Text = "";
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
