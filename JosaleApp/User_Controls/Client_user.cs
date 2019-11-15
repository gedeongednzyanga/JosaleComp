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

        public void Load_Clients(IClient client_list )
        {
            gridClient.DataSource = client_list.AllClient();
        }
        void Search_client()
        {
            gridClient.DataSource = new Client().Search(text_search.Text.Trim());
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
            int i = gridClient.CurrentRow.Index;
            Id = Convert.ToInt32(gridClient["Column1", i].Value.ToString());
            Get_gage();
            
           // Total_gage();
        }
        void Get_customer()
        {
            try
            {
                int i = gridClient.CurrentRow.Index;
                lab_name.Text = gridClient["Column2", i].Value.ToString() + " " + gridClient["Column3", i].Value.ToString()
                    +" "+ gridClient["Column4", i].Value.ToString();
                lab_customername.Text = lab_name.Text;
                lab_phone.Text = gridClient["Column5", i].Value.ToString();
                lab_mail.Text = gridClient["Column6", i].Value.ToString();
                lab_adress.Text = gridClient["Column7", i].Value.ToString();
                
            }
            catch(Exception ex) { MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void Modifier()
        {
            try
            {
                Update_Client clients = new Update_Client();
                int a= gridClient.CurrentRow.Index;
                clients.code_cli = Convert.ToInt32(gridClient["Column1", a].Value.ToString());
                clients.textBox1.Text = gridClient["Column2", a].Value.ToString();
                clients.textBox2.Text = gridClient["Column3", a].Value.ToString();
                clients.textBox3.Text = gridClient["Column4", a].Value.ToString();
                clients.textBox4.Text = gridClient["Column5", a].Value.ToString();
                clients.textBox5.Text = gridClient["Column6", a].Value.ToString();
                clients.textBox6.Text = gridClient["Column7", a].Value.ToString();
                DialogResult dlr = MessageBox.Show("Do you want to change this registration ?", "Message...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes) { clients.ShowDialog(); }
            } catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
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
            Preview_form previewF = new Preview_form();
            Dynamic_Classe.Instance().Call_Report(previewF.reportViewp, "JosaleApp.Report.Recu.rdlc", Id);
            previewF.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Modifier();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Load_Clients(new Client());
        }
    }
}
