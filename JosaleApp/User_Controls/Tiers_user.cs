using System;
using Trier_Lib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JosaleApp.User_Controls
{
    public partial class Tiers_user : UserControl
    {
        public Tiers_user()
        {
            InitializeComponent();
        }

        void Load_All_Tier(Tiers tiers)
        {
            dataGridView1.DataSource = tiers.AllTiers();
        }

        void Get_Tier()
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                lab_names.Text = dataGridView1["Column2", i].Value.ToString() + " " + dataGridView1["Column3", i].Value.ToString() +
                    " " + dataGridView1["Column4", i].Value.ToString();
                lab_phone.Text = dataGridView1["Column5", i].Value.ToString();
                lab_mail.Text = dataGridView1["Column6", i].Value.ToString();
                lab_adresse.Text = dataGridView1["Column7", i].Value.ToString();
            }
            catch(Exception ex) { MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void Search()
        {
            dataGridView1.DataSource = new Tiers().Search(text_search.Text.Trim());
            if(dataGridView1.Rows.Count <= 0) { MessageBox.Show("Aucune référence dans la Bd...", "Messasge...", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Tiers_Load(object sender, EventArgs e)
        {
           Load_All_Tier(new Tiers());
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_Tier();
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}
