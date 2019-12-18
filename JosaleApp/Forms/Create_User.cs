using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using User_lib;

namespace JosaleApp.Forms
{
    public partial class Create_User : MetroFramework.Forms.MetroForm
    {
        int userId = 0;
        public Create_User()
        {
            InitializeComponent();
        }

        void Save_uer(bool btn)
        {
            try
            {
                User user = new User();
                user.Id = Convert.ToInt32(userId);
                user.Nom_user = text_user.Text.Trim();
                user.User_name = text_username.Text.Trim();
                user.Pass_user = text_pass.Text.Trim();
                user.Niveau = Convert.ToInt32(comb_niveau.Text.Trim());
                if (btn)
                    user.Save(user);
                else
                    MessageBox.Show("Function no defined...", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {   if(string.IsNullOrEmpty(text_user.Text.Trim()) || string.IsNullOrEmpty(text_username.Text.Trim()) ||
                  string.IsNullOrEmpty(text_pass.Text.Trim()) || string.IsNullOrEmpty(comb_niveau.Text.Trim()))
            { MessageBox.Show("Complete all cases please !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else { Save_uer(true); this.Close(); }
            
        }

        private void text_user_TextChanged(object sender, EventArgs e)
        {
            if (text_user.Text.Length == 1)
            {
                text_user.Text = text_user.Text[0].ToString().ToUpper();
                text_user.Select(2, 1);
            }
        }
    }
}
