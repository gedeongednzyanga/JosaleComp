using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client_Lib;

namespace JosaleApp.User_Controls
{
    public partial class Client_user : UserControl
    {
        public Client_user()
        {
            InitializeComponent();
        }

        void Load_Clients(IClient client_list )
        {
            dataGridView1.DataSource = client_list.AllClient();
        }
        void Search_client()
        {
            dataGridView1.DataSource = new Client().Search(text_search.Text.Trim());
        }
        void Get_customer()
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                lab_name.Text = dataGridView1["Column2", i].Value.ToString() + " " + dataGridView1["Column3", i].Value.ToString()
                    +" "+ dataGridView1["Column4", i].Value.ToString();
                lab_customername.Text = lab_name.Text;
                lab_phone.Text = dataGridView1["Column5", i].Value.ToString();
                lab_mail.Text = dataGridView1["Column6", i].Value.ToString();
                lab_adress.Text = dataGridView1["Column7", i].Value.ToString();
            }
            catch(Exception ex) { MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Client_user_Load(object sender, EventArgs e)
        {
            Load_Clients(new Client());
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            Search_client();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_customer();
        }
    }
}
