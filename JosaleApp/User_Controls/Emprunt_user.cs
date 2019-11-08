using System;
using Emprunts_Lib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JosaleApp.Classes;
using JosaleApp.Forms;

namespace JosaleApp.User_Controls
{
    public partial class Emprunt_user : UserControl
    {
        public Emprunt_user()
        {
            InitializeComponent();
        }
        void Load_emprunt(IEmprunt emprunt)
        {
            dataGridView1.DataSource = emprunt.AllEmprunt();
        }
        void Search()
        {
          dataGridView1.DataSource = new Emprunt().Search(text_search.Text.Trim());
        }
        void Get_data()
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                lab_name.Text = dataGridView1["Column2", i].Value.ToString();
                lab_lastname.Text = dataGridView1["Column3", i].Value.ToString();
                lab_surname.Text = dataGridView1["Column4", i].Value.ToString();
                lab_mount.Text = dataGridView1["Column5", i].Value.ToString()+"$";
                lab_mountto.Text = dataGridView1["Column7", i].Value.ToString()+"$";
                lab_date.Text  = dataGridView1["Column6", i].Value.ToString();
                lab_dater.Text =DateTime.Parse(dataGridView1["Column8", i].Value.ToString()).ToShortDateString();
            }  catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Modifier()
        {
            New_emprunt emprunt = new New_emprunt();
            int i = dataGridView1.CurrentRow.Index;
            emprunt.id = Convert.ToInt32(dataGridView1["Column1", i].Value.ToString());
            emprunt.textBox1.Text = dataGridView1["Column5", i].Value.ToString();
            emprunt.textBox2.Text = dataGridView1["Column7", i].Value.ToString();
            emprunt.dateTimePicker1.Value = DateTime.Parse(dataGridView1["Column8", i].Value.ToString());
            emprunt.btnSave.Text = " Update";
            emprunt.btnNew.Enabled = false;
            DialogResult dlr = MessageBox.Show("Do you want to change this registration ?", "Message...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dlr == DialogResult.Yes) { emprunt.ShowDialog(); }
        }
        void Somme_emprunt()
        {
            Dynamic_Classe.Instance().Get_Somme_debt(label20, DateTime.Now.Month);
        }

        void Somme_emprunt_annee()
        {
            Dynamic_Classe.Instance().Get_Somme_debt_annee(label21, DateTime.Now.Year);
        }

        void Load_Historic()
        {
            Dynamic_Classe.Instance().Call_Report_Historic_Debit(reportViewer1, "JosaleApp.Report.Loan_historic.rdlc", Convert.ToInt32(text_annee.Text));
        }
        void PreviewHistoric()
        {
            Preview_form frmp = new Preview_form();
            frmp.Size = new Size(825, 601);
            Dynamic_Classe.Instance().Call_Report_Historic_Debit(frmp.reportViewp, "JosaleApp.Report.Loan_historic.rdlc", Convert.ToInt32(text_annee.Text));
            frmp.Show();
        }
        private void Emprunt_user_Load(object sender, EventArgs e)
        {
            Load_emprunt(new Emprunt());
            Somme_emprunt();
            Somme_emprunt_annee();
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_data();
        }

        private void text_annee_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

    private void btnShow_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "btnShow":
                    if (string.IsNullOrEmpty(text_annee.Text))
                        MessageBox.Show("Check the year case please !!!", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        Load_Historic();
                    break;
                case "btnPreview":
                    if(string.IsNullOrEmpty(text_annee.Text))
                        MessageBox.Show("Check the year case please !!!", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        PreviewHistoric();
                    break;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Modifier();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Load_emprunt(new Emprunt());
        }
    }
}
