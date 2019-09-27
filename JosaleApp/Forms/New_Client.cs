using System;
using Prets_Lib;
using Client_Lib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JosaleApp.Classes;

namespace JosaleApp.Forms
{
    public partial class New_Client : Form
    {
        IClient client;
        IPrets prets;
        Gage gage;
        int code_cli=0, code_pret=0, code_interet=0, code_gage;

        public New_Client()
        {
            InitializeComponent();
        }

        void Nouveau_Client_Pret()
        {
            client = new Client();
            prets = new Prets();
            gage = new Gage();
            code_cli = client.Nouveau();
            code_pret = prets.Nouveau();
            code_gage = gage.Nouveau();
        }
        void Load_Interet()
        {
            dataGridView1.DataSource = Dynamic_Classe.Instance().Get_Data("Affichage_Interet");
        }
        void Get_interet()
        {
            int i = dataGridView1.CurrentRow.Index;
            code_interet = Convert.ToInt32(dataGridView1["N°", i].Value.ToString());
        }

        void Save_Client(bool btn)
        {
            try
            {
                client = new Client();
                if(code_cli == 0 || textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") ||
                    textBox4.Text.Equals("") || textBox5.Text.Equals("") || textBox6.Text.Equals(""))
                { MessageBox.Show("Complete all input fields for customer !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    client.Id = Convert.ToInt32(code_cli);
                    client.Nom = textBox1.Text.Trim();
                    client.Postnom = textBox2.Text.Trim();
                    client.Prenom = textBox3.Text.Trim();
                    client.Contact = textBox4.Text.Trim();
                    client.Mail = textBox5.Text.Trim();
                    client.Addresse = textBox6.Text.Trim();

                    if (btn)
                        client.Save(client);
                    else
                        client.Delete(code_cli);
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Save_Pret(bool btn)
        {
            try
            {
                prets = new Prets();
                if (code_pret == 0 || code_cli == 0 || textBox7.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") ||
                    textBox4.Text.Equals("") || textBox5.Text.Equals("") || textBox6.Text.Equals(""))
                { MessageBox.Show("Complete all input fields for credit detail !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    prets.Id = Convert.ToInt32(code_pret);
                    prets.Montant = float.Parse(textBox7.Text.Trim());
                    prets.RefCli = Convert.ToInt32(code_cli);
                    prets.RefInteret = Convert.ToInt32(code_interet);
                    prets.DateRembour = dateTimePicker1.Value.Date;
                    if (btn)
                        prets.Save(prets);
                    else
                        prets.Delete(code_pret);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Save_Gage(bool btn)
        {
            try
            {
                gage = new Gage();
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    gage.Id = Convert.ToInt32(dataGridView2[0, i].Value.ToString());
                    gage.Designation = dataGridView2[1, i].Value.ToString();
                    gage.Valeur =float.Parse( dataGridView2[2, i].Value.ToString());
                    gage.Refpret = code_pret;
                    gage.Nombre = Convert.ToInt32(dataGridView2[3, i].Value.ToString());
                    gage.Save(gage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Save_Credit()
        {
            Save_Client(true);
            Save_Pret(true);
            Save_Gage(true);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void New_Client_Load(object sender, EventArgs e)
        {
            Load_Interet();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            lab_customer.Text = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            lab_customer.Text = textBox1.Text+" "+textBox2.Text;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (code_gage ==0 || string.IsNullOrEmpty(textBox13.Text) || string.IsNullOrEmpty(textBox12.Text) ||
               string.IsNullOrEmpty(textBox11.Text))
                return;
            dataGridView2.Rows.Add(code_gage.ToString(), textBox13.Text.Trim(), textBox12.Text.Trim(), textBox11.Text.Trim());
            code_gage++;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_interet();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "btnNew":
                    Nouveau_Client_Pret();
                    break;
                case "btnSave":
                    Save_Credit();
                    break;
                case "btnDelete":
                    break;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            lab_customer.Text = textBox1.Text + " " + textBox2.Text+" "+ textBox3.Text;
        }
    }
}
