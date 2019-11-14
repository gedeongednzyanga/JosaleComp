using Emprunts_Lib;
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
    public partial class Update_loan : Form
    {
        public int Id = 0, codeEmpr=0;

        Rembourssement_emp Rembou;

        public Update_loan()
        {
            InitializeComponent();
        }

        void Save(bool btn)
        {
            try
            {
                Rembou = new Rembourssement_emp();
                if (Id == 0) { MessageBox.Show("Loan not exist !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else if (codeEmpr == 0 || string.IsNullOrEmpty(lab_dete.Text) || string.IsNullOrEmpty(text_mount.Text) || string.IsNullOrEmpty(lab_reste.Text))
                { MessageBox.Show("Faild to update loan...", "Messasge...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else
                {
                    Rembou.Id = Convert.ToInt32(Id);
                    Rembou.Montant = float.Parse(text_mount.Text.Trim());
                    Rembou.Reste = float.Parse(lab_reste.Text.Trim());
                    Rembou.RefEmp = Convert.ToInt32(codeEmpr);
                    if (btn)
                        Rembou.Save(Rembou);
                    else
                        MessageBox.Show("Function delete not found please create this function");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        private void text_mount_MouseEnter(object sender, EventArgs e)
        {
        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "btnSave":
                    Save(true);
                    break;
                case "btnDelete":
                    Save(false);
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
