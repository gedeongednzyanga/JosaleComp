﻿using System;
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
        Client_user client = new Client_user();
        Emprunt_user empruts= new Emprunt_user();
        Credit_user credits = new Credit_user();

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
            Client_user clients = client as Client_user;
            clients.Dock = DockStyle.Fill;
            panel_principal.Controls.Clear();
            panel_principal.Controls.Add(clients);
            panel_principal.Show();
        }
        void ShowEmprunt(object emprunt)
        {
            Emprunt_user emprunts = emprunt as Emprunt_user;
            emprunts.Dock = DockStyle.Fill;
            panel_principal.Controls.Clear();
            panel_principal.Controls.Add(emprunts);
            panel_principal.Show();
        }
        void ShowCredit(object credit)
        {
            Credit_user credits = credit as Credit_user;
            credits.Dock = DockStyle.Fill;
            panel_principal.Controls.Clear();
            panel_principal.Controls.Add(credits);
            panel_principal.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void label8_Click(object sender, EventArgs e)
        {
           
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

        private void label9_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void label14_Click(object sender, EventArgs e)
        {
            new New_Tiers().ShowDialog();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            ShowTier(tier);
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            new New_emprunt().ShowDialog();
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            ShowEmprunt(empruts);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            ShowClient(new Client_user());
        }

        private void label5_Click(object sender, EventArgs e)
        {
            new New_Client().ShowDialog();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            ShowCredit(credits);
        }
    }
}
