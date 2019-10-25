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
    public partial class New_Payement : Form
    {
        Rembourssement rembou;
        int codeRembu = 0;
        int codeClient = 0;
        public New_Payement()
        {
            InitializeComponent();
        }

        void Nouveau()
        {
            rembou = new Rembourssement();
            codeRembu = rembou.Nouveau();
        }

        void Save_Rembou(bool btn)
        {
            try
            {
                rembou = new Rembourssement();
                if (codeRembu == 0)
                { MessageBox.Show("Clik at first the new Button !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else if (codeClient == 0 || string.IsNullOrEmpty(label3.Text) || string.IsNullOrEmpty(textBox1.Text))
                { MessageBox.Show("Complete the mount case or select custumer please", "Messasge...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else
                {
                    rembou.Id = Convert.ToInt32(codeRembu);
                    rembou.Montant = double.Parse(textBox1.Text.Trim());
                    rembou.RefCli = Convert.ToInt32(codeClient);
                    rembou.Reste = double.Parse(label8.Text.Trim());
                    if (btn)
                        rembou.Save(rembou);
                    else
                        MessageBox.Show("Function delete not found please create this function");
                       
                }
            } catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Get_Credit();
        }
        void Get_Credit()
        {
           dataGridView1.Rows.Clear();
           Dynamic_Classe.Instance().Load_Credit(dataGridView1);
        }

        void Get_data()
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                label3.Text = dataGridView1["Column2", i].Value.ToString();
                label6.Text = dataGridView1["Column3", i].Value.ToString();
                codeClient = Convert.ToInt32(dataGridView1["Column1", i].Value.ToString());
            } catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }
        private void New_Payement_Load(object sender, EventArgs e)
        {
            Get_Credit();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Get_data();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "btnNew":
                    Nouveau();
                    break;
                case "btnSave":
                    Save_Rembou(true);
                    break;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch !=8  && ch != 46)
            {
                e.Handled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            Dynamic_Classe.Instance().Search_Credit(dataGridView1, text_search.Text.Trim());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(Convert.ToDouble(label6.Text.Trim()) < Convert.ToDouble(textBox1.Text.Trim())) {
                    MessageBox.Show("Mount is super than credit...", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "0";
                    label8.Text = "0";
                }
                else { label8.Text = (Convert.ToDouble(label6.Text.Trim()) - (Convert.ToDouble(textBox1.Text.Trim()))).ToString(); }
                
            }   catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
