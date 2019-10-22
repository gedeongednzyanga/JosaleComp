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
    }
}
