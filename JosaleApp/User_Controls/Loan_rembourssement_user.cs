using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JosaleApp.Classes;

namespace JosaleApp.User_Controls
{
    public partial class Loan_rembourssement_user : UserControl
    {
        public Loan_rembourssement_user()
        {
            InitializeComponent();
        }
        void Get_Emprunt()
        {
            dataGridView2.Rows.Clear();
            Dynamic_Classe.Instance().Load_Emprunt(dataGridView2);
        }
        private void Loan_rembourssement_user_Load(object sender, EventArgs e)
        {
            Get_Emprunt();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            textBox4.Width = 233;
        }

        private void textBox4_MouseLeave(object sender, EventArgs e)
        {
            textBox4.Width = 0;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Dynamic_Classe.Instance().Search_Emprunt(dataGridView2, textBox4.Text.Trim());
            }  catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
