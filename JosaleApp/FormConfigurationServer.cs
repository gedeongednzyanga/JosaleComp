using System;

using Connexion_manager;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JosaleApp
{
    public partial class FormConfigurationServer : Form
    {
        Sgbd sgbd = new Sgbd();

        public FormConfigurationServer()
        {
            InitializeComponent();
        }

        void Get_Server()
        {
            comboBox2.Items.Add(".");
            comboBox2.Items.Add("local");
            comboBox2.Items.Add(@".\SQLEXPRESS");
            comboBox2.Items.Add(string.Format(@"{0}", Environment.MachineName));
            comboBox2.SelectedIndex = 3;
        }

        private void FormConfigurationServer_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetNames(typeof(Sgbd));
            comboBox1.SelectedIndex = 0;
            Get_Server();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    sgbd = Sgbd.SQLServer;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    sgbd = Sgbd.SQLServer;
                    break;
                case 1:
                    sgbd = Sgbd.MySQL;
                    break;
                case 2:
                    sgbd = Sgbd.PostGrsSQL;
                    break;
                case 3:
                    sgbd = Sgbd.Oracle;
                    break;
                case 4:
                    sgbd = Sgbd.Access;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "" && comboBox2.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "") {
                    File.WriteAllText(Chemin_Connexion.Table.server, comboBox2.Text.Trim());
                    File.WriteAllText(Chemin_Connexion.Table.database, textBox2.Text.Trim());
                    File.WriteAllText(Chemin_Connexion.Table.user, textBox3.Text.Trim());
                    File.WriteAllText(Chemin_Connexion.Table.password, textBox4.Text.Trim());
                    this.Close();
                    Cls_Connexion.Instance().Connect();
                }
                else
                {
                    MessageBox.Show("Invalid server configuration", "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception ex) { MessageBox.Show("Error : " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error); }
           
        }
    }
}
