using System;
using Emprunts_Lib;
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
    public partial class New_emprunt : Form
    {
        int id = 0;
        int code_tier = 0;

        public New_emprunt()
        {
            InitializeComponent();
        }

        void InitializeComponents()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            code_tier = 0;
        }

        void Load_Tier(IEmprunt empTier)
        {
            dataGridView2.DataSource = empTier.Get_TierNomId();
        }

        void Get_IdTier()
        {
            int i = dataGridView2.CurrentRow.Index;
            code_tier = Convert.ToInt32(dataGridView2["Column1", i].Value.ToString());
            lab_supp.Text = dataGridView2["Column2", i].Value.ToString() + " is selected.";
        }

        void Save_Emprunt (bool btn)
        {
            try
            {
                IEmprunt emprunt = new Emprunt();
                if(id ==0 || code_tier==0 || textBox1.Text.Equals("") || textBox2.Text.Equals(""))
                {
                    label5.Visible = true;
                    lab_error.Visible = true;
                    lab_error.Text = "Select a supplier in the table below !!!";
                    MessageBox.Show("Complete all textbox please", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                else
                {
                    label5.Visible = false;
                    lab_error.Visible = false;
                    emprunt.Id = Convert.ToInt32(id);
                    emprunt.Montant = float.Parse(textBox1.Text.Trim());
                    emprunt.DateRembu = dateTimePicker1.Value.Date;
                    emprunt.MontantRemb = float.Parse(textBox2.Text.Trim());
                    emprunt.Reftier = Convert.ToInt32(code_tier);

                    if (btn) { emprunt.Save(emprunt); InitializeComponents(); }
                    else { emprunt.Delete(id); InitializeComponents(); } 
                }
            }catch (Exception ex) { MessageBox.Show("Error " + ex.Message); }
        }

        private void New_emprunt_Load(object sender, EventArgs e)
        {
            Load_Tier(new Emprunt());
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "btnNew":
                    IEmprunt emprunt = new Emprunt();
                    id = emprunt.Nouveau();
                    break;
                case "btnSave":
                    Save_Emprunt(true);
                    break;
                case "btnDelete":
                    Save_Emprunt(false);
                    break;
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            Get_IdTier();
        }
    }
}
