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
    public partial class Login_Form : MetroFramework.Forms.MetroForm
    {
        
        public Login_Form()
        {
            InitializeComponent();
        }

        void test_user(IUsers user)
        {
            if(user.Test_user(text_user.Text.Trim(), text_pass.Text.Trim()) == 1)
            {
                this.Close();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(text_user.Text.Trim())|| string.IsNullOrEmpty(text_pass.Text.Trim()))
            {
                MessageBox.Show("Complete user name and password please !!!", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else { test_user(new User()); }
           
        }
    }
}
