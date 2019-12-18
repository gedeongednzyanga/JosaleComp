using Client_Lib;
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
    public partial class Update_Client : Form
    {
        public int code_cli = 0;
        IClient client;
        public Update_Client()
        {
            InitializeComponent();
        }
        void Save_Client(bool btn)
        {
            try
            {
                client = new Client();

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
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "btnSave":
                    Save_Client(true);
                    break;
                case "btnDelete":
                    MessageBox.Show("Action non définie", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 1)
            {
                textBox2.Text = textBox2.Text[0].ToString().ToUpper();
                textBox2.Select(2, 1);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 1)
            {
                textBox3.Text = textBox3.Text[0].ToString().ToUpper();
                textBox3.Select(2, 1);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch !=8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length == 1)
            {
                textBox6.Text = textBox6.Text[0].ToString().ToUpper();
                textBox6.Select(2, 1);
            }
        }
    }
}
