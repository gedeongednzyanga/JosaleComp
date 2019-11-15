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
using Emprunts_Lib;
using JosaleApp.Forms;

namespace JosaleApp.User_Controls
{
    public partial class Loan_rembourssement_user : UserControl
    {
        Rembourssement_emp Rembou; 
        private int codeRembu = 0;
        private int codeEmpr = 0;
        public Loan_rembourssement_user()
        {
            InitializeComponent();
        }
        void Get_Emprunt()
        {
            dataGridView2.Rows.Clear();
            Dynamic_Classe.Instance().Load_Emprunt(dataGridView2);
        }
        void Get_Emprunt_Rem(IEmprunt rembou)
        {
            dataGridView1.DataSource = rembou.AllEmprunt_Remb();
        }

        void Nouveau()
        {
            Rembou = new Rembourssement_emp();
            codeRembu = Rembou.Nouveau();
        }

        void Get_emprunt_pay()
        {
            try
            {
                int i = dataGridView2.CurrentRow.Index;
                codeEmpr = Convert.ToInt32(dataGridView2["Column13", i].Value.ToString());
                lab_name.Text = dataGridView2["Column14", i].Value.ToString();
                lab_dete.Text = dataGridView2["Column15", i].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Save (bool btn)
        {
            try
            {
                Rembou = new Rembourssement_emp();
                if (codeRembu == 0) { MessageBox.Show("Clik at first the new Button !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else if (codeEmpr == 0 || string.IsNullOrEmpty(lab_dete.Text) || string.IsNullOrEmpty(text_mount.Text) || string.IsNullOrEmpty(lab_reste.Text))
                { MessageBox.Show("Complete the mount case or select supplier please", "Messasge...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                else
                {
                    Rembou.Id = Convert.ToInt32(codeRembu);
                    Rembou.Montant = float.Parse(text_mount.Text.Trim());
                    Rembou.Reste = float.Parse(lab_reste.Text.Trim());
                    Rembou.RefEmp = Convert.ToInt32(codeEmpr);
                    if (btn)
                        Rembou.Save(Rembou);
                    else
                        MessageBox.Show("Function delete not found please create this function");
                }
            } catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Get_Emprunt();
        }

        public void GetRembouUpdate()
        {
            Update_loan loans = new Update_loan();
            int i = dataGridView1.CurrentRow.Index;
            loans.Id = Convert.ToInt32(dataGridView1["Column1", i].Value.ToString());
            loans.lab_name.Text = dataGridView1["Column2", i].Value.ToString();
            loans.lab_dete.Text = ((double.Parse(dataGridView1["Column5", i].Value.ToString()) + (double.Parse(dataGridView1["Column4", i].Value.ToString())))).ToString();
            loans.text_mount.Text = dataGridView1["Column4", i].Value.ToString();
            loans.codeEmpr = Convert.ToInt32(dataGridView1["Column7", i].Value.ToString());
            DialogResult dlr = MessageBox.Show("Do you want to change this registration ?", "Message...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                loans.ShowDialog();
            }
        }
        private void Loan_rembourssement_user_Load(object sender, EventArgs e)
        {
            Get_Emprunt();
            Get_Emprunt_Rem(new Emprunt());
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "btnNew":
                    Nouveau();
                    break;
                case "btnSave":
                    Save(true);
                    break;
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            Get_emprunt_pay();
        }

        private void text_mount_TextChanged(object sender, EventArgs e)
        {
            if( text_mount.Text == "") { text_mount.Text = "0"; lab_reste.Text = "0"; }
            else if(float.Parse(text_mount.Text.Trim()) > float.Parse(lab_dete.Text.Trim())) {
                MessageBox.Show("Mount can't be greater than loan !!!", "Messasge...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lab_reste.Text = "";
                text_mount.Text = "";
            }
            else
            {
                try
                {
                    lab_reste.Text = (float.Parse(lab_dete.Text.Trim()) - float.Parse(text_mount.Text.Trim())).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }  
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = new Emprunt().Search_remb(text_search.Text.Trim());
            } catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void text_mount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch !=8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Get_Emprunt_Rem(new Emprunt());
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetRembouUpdate(); 
            
        }
    }
}
