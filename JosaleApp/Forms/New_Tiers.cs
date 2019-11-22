using System;
using Trier_Lib;
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
    public partial class New_Tiers : Form
    {
        public int id = 0;
        public New_Tiers()
        {
            InitializeComponent();
        }

        void InitializeComponents()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            id = 0;
        }

        void Save_tier(bool btn)
        {
            try
            {
                ITiers tiers = new Tiers();
                if(id==0 || textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("")
                    || textBox4.Text.Equals("") || textBox5.Text.Equals("") || textBox6.Text.Equals(""))
                { MessageBox.Show("Complete all textbox please", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    tiers.Id = Convert.ToInt32(id);
                    tiers.Nom = textBox1.Text.Trim();
                    tiers.Postnom = textBox2.Text.Trim();
                    tiers.Prenom = textBox3.Text.Trim();
                    tiers.Contact = textBox4.Text.Trim();
                    tiers.Mail = textBox5.Text.Trim();
                    tiers.Addresse = textBox6.Text.Trim();

                    if (btn) { tiers.Save(tiers); InitializeComponents(); }
                    else { tiers.Delete(id); InitializeComponents(); }
                }
            }catch(Exception ex) { MessageBox.Show("Error " + ex.Message); }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "btnNew":
                    ITiers tier= new Tiers();
                    id = tier.Nouveau();
                    break;
                case "btnSave":
                    Save_tier(true);
                    break;
                case "btnDelete":
                    Save_tier(false);
                    break;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1)
            {
                textBox1.Text = textBox1.Text[0].ToString().ToUpper();
                textBox1.Select(2, 1);
            }
        }
    }
}
