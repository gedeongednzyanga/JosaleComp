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
        
        public FormPrincipal frmp;
        public delegate void sendData(string datasend1, string datasend2);

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
        
        public void FoundFp(FormPrincipal frmp)
        {
            this.frmp = frmp;
        }
        void Send()
        {
            sendData sent = new sendData(frmp.FundDataLogin);
            sent(UserSession.getInstance().UserNom,(UserSession.getInstance().UserNiveau).ToString());
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
            else {
                test_user(new User());
                Send();
            }
           
        }
    }
}
