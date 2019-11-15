using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prets_Lib;
using JosaleApp.Classes;
using JosaleApp.Forms;

namespace JosaleApp.User_Controls
{
    public partial class Rembourssement_user : UserControl
    {
        private int Id;
        public Rembourssement_user()
        {
            InitializeComponent();
        }

        void Get_Rembourssement (IPrets rembou)
        {
            dataGridView1.DataSource = rembou.AllRembou();
        }

        void Get_data()
        {
            int i = dataGridView1.CurrentRow.Index;
            Id = Convert.ToInt32(dataGridView1["Column1", i].Value.ToString());
            customername.Text = " :  " + dataGridView1["Column2", i].Value.ToString()+" " + dataGridView1["Column3", i].Value.ToString()+" " + dataGridView1["Column4", i].Value.ToString();
            mount.Text = " :  " +dataGridView1["Column5", i].Value.ToString()+"$";
            reste.Text = " :  " + dataGridView1["Column6", i].Value.ToString() + "$";
            dateope.Text = " :  " + dataGridView1["Column7", i].Value.ToString();
           
        }
        void Search()
        {
            dataGridView1.DataSource = new Prets().Search_Rembou(text_search.Text.Trim());
        }

        void Display_recu()
        {
            try
            {
                Dynamic_Classe.Instance().Call_Report_Recu_Rembou(reportViewer1, "JosaleApp.Report.Recu_rembou.rdlc", Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Display_Historic()
        {
            try
            {
                Dynamic_Classe.Instance().Call_Report_Historic_Rembou(reportViewer1, "JosaleApp.Report.Historic_Credit.rdlc", 
                    Convert.ToInt32(text_annee.Text),comboBox2.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetRembouUpdate()
        {
            Update_rembou_credit credit = new Update_rembou_credit();
            int i = dataGridView1.CurrentRow.Index;
            credit.Id = Convert.ToInt32(dataGridView1["Column1", i].Value.ToString());
            credit.lab_name.Text = dataGridView1["Column2", i].Value.ToString()+' '+ dataGridView1["Column3", i].Value.ToString()+
                ' '+ dataGridView1["Column4", i].Value.ToString();
            credit.lab_dete.Text = ((double.Parse(dataGridView1["Column5", i].Value.ToString()) + (double.Parse(dataGridView1["Column6", i].Value.ToString())))).ToString();
            credit.text_mount.Text = dataGridView1["Column5", i].Value.ToString();
            credit.client = Convert.ToInt32(dataGridView1["Column10", i].Value.ToString());
            DialogResult dlr = MessageBox.Show("Do you want to change this registration ?", "Message...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                credit.ShowDialog();
            }
        }

        private void Rembourssement_user_Load(object sender, EventArgs e)
        {
            Get_Rembourssement(new Prets());
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_data();
            Display_recu();
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          if(string.IsNullOrEmpty(text_annee.Text) || string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Enter the year or Select month please. Then display again...", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else { Display_Historic(); }
        }

        private void text_annee_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch !=8 && ch != 46)                                                     
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Get_Rembourssement(new Prets());
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetRembouUpdate();
        }
    }
}
