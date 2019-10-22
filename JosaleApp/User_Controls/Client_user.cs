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
using JosaleApp.Classes;
using JosaleApp.Forms;

namespace JosaleApp.User_Controls
{
    public partial class Client_user : UserControl
    {
        int Id = 0;
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
        void Show_recu()
        {
            Dynamic_Classe.Instance().Call_Report(Report_preview, "JosaleApp.Report.Recu.rdlc", Id);
        }
        void Get_gage()
        {
            listView1.Items.Clear();
            Dynamic_Classe.Instance().Load_gage(Id, listView1);
            Dynamic_Classe.Instance().Get_Data_one("prets", Id, "montant_remb", label9);
        }
        void Get_data()
        {
            int i = dataGridView1.CurrentRow.Index;
            Id = Convert.ToInt32(dataGridView1["Column1", i].Value.ToString());
            Get_gage();
            
           // Total_gage();
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
            Get_data();
        }

        private void button1_Click(object sender, EventArgs e)   
        {
            Show_recu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Preview_form previewF = new Preview_form(Id);
            previewF.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
