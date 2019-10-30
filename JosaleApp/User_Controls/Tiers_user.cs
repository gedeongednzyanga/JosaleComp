using System;
using Trier_Lib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using JosaleApp.Classes;

namespace JosaleApp.User_Controls
{
    public partial class Tiers_user : UserControl
    {
        SpeechSynthesizer s = new SpeechSynthesizer();
        private int id_tier = 0;

        public Tiers_user()
        {
           
            InitializeComponent();
        }

        void Load_All_Tier(Tiers tiers)
        {
            dataGridView1.DataSource = tiers.AllTiers();
        }

        void Get_Tier()
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                id_tier = Convert.ToInt32(dataGridView1["Column1", i].Value.ToString());
                lab_names.Text = dataGridView1["Column2", i].Value.ToString() + " " + dataGridView1["Column3", i].Value.ToString() +
                    " " + dataGridView1["Column4", i].Value.ToString();
                lab_phone.Text = dataGridView1["Column5", i].Value.ToString();
                lab_mail.Text = dataGridView1["Column6", i].Value.ToString();
                lab_adresse.Text = dataGridView1["Column7", i].Value.ToString();
            }
            catch(Exception ex) { MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void Search()
        {
            dataGridView1.DataSource = new Tiers().Search(text_search.Text.Trim());
            if(dataGridView1.Rows.Count <= 0) { MessageBox.Show("Aucune référence dans la Bd...", "Messasge...", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
        }

        void SaveMessage(bool btn)
        {
            try
            {
                Cls_Message message = new Cls_Message();
                message.Message = text_message.Text.Trim();
                message.Reftier = id_tier;
                if (btn) { Cls_Message.Insatnce().Save(message); }
                else { MessageBox.Show("Function not fund...", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Tiers_Load(object sender, EventArgs e)
        {
            Load_All_Tier(new Tiers());
            Cls_Message.Insatnce().GetAllPorts(comboBox1);
            comboBox2.SelectedIndex = 4;
            comboBox3.SelectedIndex = 1;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_Tier();
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBox1.Text)|| string.IsNullOrEmpty(comboBox2.Text)||string.IsNullOrEmpty(comboBox3.Text))
            {
                MessageBox.Show("Configuration failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               Cls_Message.Insatnce().SetData(Convert.ToInt32(comboBox1.Text.Trim()), Convert.ToInt32(comboBox2.Text.Trim()),
                   Convert.ToInt32(comboBox3.Text.Trim()));
                Cls_Message.Insatnce().Test_port();
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            s.SelectVoiceByHints(VoiceGender.Male);
            if (string.IsNullOrEmpty(text_phone.Text) || string.IsNullOrEmpty(text_message.Text))
            {
                s.Speak("Specify the number or write a message to send");
                MessageBox.Show("Specify the number or write a message to send.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else { Cls_Message.Insatnce().Send(text_message.Text, text_phone.Text.Trim(), "");
                SaveMessage(true);
            }
        }

        private void text_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch !=8 && ch != 46)
            {
                e.Handled = true;
            }
        }

       
    }
}
