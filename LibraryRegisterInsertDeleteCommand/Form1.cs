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

namespace LibraryRegisterInsertDeleteCommand
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connect = new SqlConnection("server=.;database=Library;integrated security=sspi;");

        private void showData()
        {
            listView1.Items.Clear();

            connect.Open();
            SqlCommand command = new SqlCommand("select * from tblBooks", connect);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ListViewItem row = new ListViewItem(reader["libraryId"].ToString());
                row.SubItems.Add(reader["bookname"].ToString());
                row.SubItems.Add(reader["writer"].ToString());
                row.SubItems.Add(reader["publishinghouse"].ToString());
                row.SubItems.Add(reader["numberofpages"].ToString());

                listView1.Items.Add(row);
            }


            connect.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            showData();


        }


        private void button2_Click(object sender, EventArgs e)
        {

            connect.Open();
            SqlCommand command = new SqlCommand("insert into tblBooks(libraryId,bookname,writer,publishinghouse,numberofpages) values('" + txtlibraryId.Text + "','" + txtBookName.Text + "','" + txtWriter.Text + "','" + txtHouse.Text + "','" + txtPages.Text + "')", connect);
            command.ExecuteNonQuery();
            connect.Close();

            listView1.Items.Clear();

            showData();



            txtBookName.Clear();
            txtHouse.Clear();
            txtlibraryId.Clear();
            txtPages.Clear();
            txtWriter.Clear();


        }

        int id = 0;
        private void button3_Click(object sender, EventArgs e)
        {


            connect.Open();
            SqlCommand command = new SqlCommand("delete from tblBooks where libraryId=(" + id + ")", connect);
            command.ExecuteNonQuery();
            connect.Close();
            showData();

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtlibraryId.Text = listView1.SelectedItems[0].SubItems[0].Text;

            id = int.Parse(txtlibraryId.Text);


            txtBookName.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtWriter.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtHouse.Text = listView1.SelectedItems[0].SubItems[3].Text;
            txtPages.Text = listView1.SelectedItems[0].SubItems[4].Text;



        }


    }
}
