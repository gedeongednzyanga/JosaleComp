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
        void Somme_emprunt()
        {
            Dynamic_Classe.Instance().Get_Somme_debt(label20, DateTime.Now.Month);
        }

        void Somme_emprunt_annee()
        {
            Dynamic_Classe.Instance().Get_Somme_debt_annee(label21, DateTime.Now.Year);
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
    }
}
