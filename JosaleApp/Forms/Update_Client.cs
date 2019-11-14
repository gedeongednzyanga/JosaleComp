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
    }
}
