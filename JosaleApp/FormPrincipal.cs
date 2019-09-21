using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JosaleApp.Properties;
using System.Windows.Forms;
using JosaleApp.User_Controls;
using JosaleApp.Forms;
using JosaleApp.Classes;

namespace JosaleApp
{
    public partial class FormPrincipal : Form
    {
        //Création des objects

        Tiers_user tier = new Tiers_user();
        Client client = new Client();

        public FormPrincipal()
        {
            InitializeComponent();
        }

        //Méthodes pour afficher les objects
  
        void ShowTier(object tier)
        {
            Tiers_user tiers = tier as Tiers_user;
            tiers.Dock = DockStyle.Fill;
            panel_principal.Controls.Clear();
            panel_principal.Controls.Add(tiers);
            panel_principal.Show();
        }
        void ShowClient(object client)
        {
            Client clients = client as Client;
            clients.Dock = DockStyle.Fill;
            panel_principal.Controls.Clear();
            panel_principal.Controls.Add(clients);
            panel_principal.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ShowTier(tier);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            new New_Tiers().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowClient(client);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            new New_Client().ShowDialog();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            Test_Configuration.Test_Flies();
        }
    }
}
