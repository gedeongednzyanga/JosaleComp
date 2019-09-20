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
    }
}
