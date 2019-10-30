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
        //Création des variables

        int sommePret = 0;
        int sommeCustomer = 0;
        int sommeDebt = 0;
        int sommeSupplier = 0;

        //Création des objects

        Tiers_user tier = new Tiers_user();
        Client_user client = new Client_user();
        Emprunt_user empruts= new Emprunt_user();
        Credit_user credits = new Credit_user();
        Rembourssement_user rembou = new Rembourssement_user();
        Loan_rembourssement_user loan = new Loan_rembourssement_user();

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
        void ShowRembou(object rembu)
        {
            Rembourssement_user rembus = rembu as Rembourssement_user;
            rembus.Dock = DockStyle.Fill;
            panel_principal.Controls.Clear();
            panel_principal.Controls.Add(rembus);
            panel_principal.Show();
        }
        void ShowRembouLoan(object rembu_loan)
        {
            Loan_rembourssement_user rembuL = rembu_loan as Loan_rembourssement_user;
            rembuL.Dock = DockStyle.Fill;
            panel_principal.Controls.Clear();
            panel_principal.Controls.Add(rembuL);
            panel_principal.Show();
        }

        //Méthodes design

        void Enter_label(Label label)
        {
            label.ForeColor = Color.Maroon;
            label.Font = new Font(label.Font, FontStyle.Underline);
        }
        void Leave_label (Label label)
        {
            label.ForeColor = Color.Black;
            label.Font = new Font(label.Font, FontStyle.Regular);
        }

        //Autres méthodes
        void Load_Somme()
        {
            sommePret=Dynamic_Classe.Instance().Count_data("prets", "code_pret", sommePret);
            label18.Text = "All credits     " + "("+sommePret.ToString()+")";
            sommeCustomer = Dynamic_Classe.Instance().Count_data("client", "code_cli", sommeCustomer);
            label6.Text = "All customers   " + "(" + sommeCustomer.ToString() + ")";
            sommeDebt = Dynamic_Classe.Instance().Count_data("emprunts", "code_emprunt", sommeDebt);
            label9.Text = "All debits   " + "(" + sommeDebt.ToString() + ")";
            sommeSupplier = Dynamic_Classe.Instance().Count_data("tier", "code_tier", sommeSupplier);
            label15.Text = "All suppliers   " + "(" + sommeSupplier.ToString() + ")";
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
            ShowCredit(credits);
            Load_Somme();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
        }

        private void label16_Click(object sender, EventArgs e)
        {
            ShowRembou(rembou);
        }

        private void label17_Click(object sender, EventArgs e)
        {
            new New_Payement().ShowDialog();
        }

        private void label17_MouseEnter(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "label17":
                    Enter_label(label17);
                    break;
                case "label16":
                    Enter_label(label16);
                    break;
                case "label18":
                    Enter_label(label18);
                    break;
                case "label4":
                    Enter_label(label4);
                    break;
                case "label5":
                    Enter_label(label5);
                    break;
                case "label6":
                    Enter_label(label6);
                    break;
                case "label8":
                    Enter_label(label8);
                    break;
                case "label9":
                    Enter_label(label9);
                    break;
                case "label10":
                    Enter_label(label10);
                    break;
                case "label13":
                    Enter_label(label13);
                    break;
                case "label14":
                    Enter_label(label14);
                    break;
                case "label15":
                    Enter_label(label15);
                    break;
            }
        }

        private void label17_MouseLeave(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "label17":
                    Leave_label(label17);
                    break;
                case "label16":
                    Leave_label(label16);
                    break;
                case "label18":
                    Leave_label(label18);
                    break;
                case "label4":
                    Leave_label(label4);
                    break;
                case "label5":
                    Leave_label(label5);
                    break;
                case "label6":
                    Leave_label(label6);
                    break;
                case "label8":
                    Leave_label(label8);
                    break;
                case "label9":
                    Leave_label(label9);
                    break;
                case "label10":
                    Leave_label(label10);
                    break;
                case "label13":
                    Leave_label(label13);
                    break;
                case "label14":
                    Leave_label(label14);
                    break;
                case "label15":
                    Leave_label(label15);
                    break;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            ShowRembouLoan(loan);
        }
    }
}
