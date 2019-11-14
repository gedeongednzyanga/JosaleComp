using JosaleApp.Classes;
using Prets_Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JosaleApp.Forms
{
    public partial class Update_credit : Form
    {
        public int id = 0;
        int code_interet = 0;
        IPrets prets;
        //int colIndex, rowInde;
        public Update_credit()
        {
            InitializeComponent();
        }

        //Méthode
        void Load_Interet()
        {
            dataGridView1.DataSource = Dynamic_Classe.Instance().Get_Data("Affichage_Interet");
        }
        void Get_interet()
        {
            int i = dataGridView1.CurrentRow.Index;
            code_interet = Convert.ToInt32(dataGridView1["N°", i].Value.ToString());
        }
        void loadGage()
        {
            dataGridView2.DataSource = Dynamic_Classe.Instance().Get_Data("gage", "ref_pret", id);
        }
        void getGageInformation()
        {
            try
            {
                int i = dataGridView2.CurrentRow.Index;
                textBox13.Text = dataGridView2["Column2", i].Value.ToString();
                textBox12.Text = dataGridView2["Column3", i].Value.ToString();
                textBox11.Text = dataGridView2["Column4", i].Value.ToString();
            }  catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Save_Pret(bool btn)
        {
            try
            {
                prets = new Prets();

                prets.Id = Convert.ToInt32(id);
                prets.Montant = float.Parse(textBox7.Text.Trim());
                prets.RefCli = Convert.ToInt32(id);
                prets.RefInteret = Convert.ToInt32(code_interet);
                prets.DateRembour = dateTimePicker1.Value.Date;
                if (btn) {
                    prets.Save(prets);
                    MessageBox.Show("Saved successfully !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
                else
                    prets.Delete(id);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Update_data()
        {
            int i = dataGridView2.CurrentRow.Index;
            dataGridView2["Column2", i].Value = textBox13.Text.Trim();
            dataGridView2["Column3", i].Value = textBox13.Text.Trim();
            dataGridView2["Column4", i].Value = textBox13.Text.Trim();
        }

        private void Update_credit_Load(object sender, EventArgs e)
        {
            Load_Interet();
            loadGage();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_interet();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "btnUpdate":
                    Update_data();
                    break;
                case "btnDelete":
                    MessageBox.Show("Action non définie", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
             getGageInformation();
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "btnSave":
                    Save_Pret(true);
                    break;
                case "btnDelete":
                    MessageBox.Show("Action non définie", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }
    }
}
