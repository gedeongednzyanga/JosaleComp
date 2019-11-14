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
    public partial class Update_rembou_credit : Form
    {
        Rembourssement rembou;
        public int Id = 0, client = 0;

        public Update_rembou_credit()
        {
            InitializeComponent();
        }

        void Save_Rembou(bool btn)
        {
            try
            {
                rembou = new Rembourssement();
                if (Id == 0)
                { MessageBox.Show("Credit not exist !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else if (client == 0 || string.IsNullOrEmpty(lab_reste.Text) || string.IsNullOrEmpty(text_mount.Text))
                { MessageBox.Show("Faild to update credit...", "Messasge...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else
                {
                    rembou.Id = Convert.ToInt32(Id);
                    rembou.Montant = double.Parse(text_mount.Text.Trim());
                    rembou.RefCli = Convert.ToInt32(client);
                    rembou.Reste = double.Parse(lab_reste.Text.Trim());
                    if (btn)
                        rembou.Save(rembou);
                    else
                        MessageBox.Show("Function delete not found please create this function");

                }
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
                    Save_Rembou(true);
                    break;
                case "btnDelete":
                    Save_Rembou(false);
                    break;
            }
        }

        private void text_mount_TextChanged(object sender, EventArgs e)
        {
             if (text_mount.Text == "") { text_mount.Text = "0"; lab_reste.Text = "0"; }
            else if (float.Parse(text_mount.Text.Trim()) > float.Parse(lab_dete.Text.Trim()))
            {
                MessageBox.Show("Mount can't be greater than loan !!!", "Messasge...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lab_reste.Text = "";
                text_mount.Text = "";
            }else
            try
            {
                lab_reste.Text = ((double.Parse(lab_dete.Text.Trim()) - (double.Parse(text_mount.Text.Trim())))).ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
