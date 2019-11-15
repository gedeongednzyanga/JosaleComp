﻿using System;
using Prets_Lib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using JosaleApp.Classes;
using Pharmacie.Classes;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using JosaleApp.Forms;

namespace JosaleApp.User_Controls
{
    public partial class Credit_user : UserControl
    {
        int Id = 0;
        System.Data.DataTable dtable = new System.Data.DataTable();


        public Credit_user()
        {
            InitializeComponent();
        }

        void Load_annee()
        {
            Dynamic_Classe.Instance().Get_Annee_combo(comboBox1);
        }
        void Get_data()
        {
            int i = dataGridView1.CurrentRow.Index;
            Id = Convert.ToInt32(dataGridView1["Column1", i].Value.ToString());
            Get_gage();
            Total_gage();
        }
        void Get_gage()
        {
            listView1.Items.Clear();
            Dynamic_Classe.Instance().Load_gage(Id, listView1);
        }
        public void Get_Credit(IPrets credit)
        {
            dataGridView1.DataSource = credit.Allcredit();
            Load_annee();
        }
        void Get_Credit_annee(IPrets credit)
        {
            dataGridView1.DataSource = credit.Allcredit_Tri(Convert.ToInt32(comboBox1.Text.Trim()));
        }
        void Get_Credit_annee_mois(IPrets credit)
        {
            dataGridView1.DataSource = credit.Allcredit_Tri_moi(Convert.ToInt32(comboBox1.Text.Trim()), comboBox2.Text.Trim());
        }

        void Search()
        {
            dataGridView1.DataSource = new Prets().Search(text_search.Text.Trim());
        }
        
        void Loard_chart()
        {
            chart1.Series["Series1"].Points.AddXY("Janvier", 12);
            chart1.Series["Series1"].Points.AddXY("Février", 20);
            chart1.Series["Series1"].Points.AddXY("Mars", 50);
            chart1.Series["Series1"].Points.AddXY("Avril", 42);
            chart1.Series["Series1"].Points.AddXY("Mai", 10);
            chart1.Series["Series1"].Points.AddXY("Juin", 30);
            chart1.Series["Series1"].Points.AddXY("Juillet", 90);
            chart1.Series["Series1"].Points.AddXY("Août", 50);
            chart1.Series["Series1"].Points.AddXY("Septembre", 35);
            chart1.Series["Series1"].Points.AddXY("Octobre", 50);
            chart1.Series["Series1"].Points.AddXY("Novembre", 55);
            chart1.Series["Series1"].Points.AddXY("Decembre", 85);
        }
         void Total_gage()
        {
            int i = listView1.Items.Count;
            label6.Text = i.ToString()+" "+" gage(s)";
        }

        public void Modifier()
        {
            try
            {
                Update_credit credit = new Update_credit();
                int i = dataGridView1.CurrentRow.Index;
                credit.id = Convert.ToInt32(dataGridView1["Column1", i].Value.ToString());
                credit.labCustomerName.Text = dataGridView1["Column2", i].Value.ToString() + " " + dataGridView1["Column3", i].Value.ToString() +
                   " " + dataGridView1["Column4", i].Value.ToString();
                credit.textBox7.Text = dataGridView1["Column5", i].Value.ToString();
                DialogResult dlr = MessageBox.Show("Do you want to change this registration ?", "Message...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes) { credit.ShowDialog(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Credit_user_Load(object sender, EventArgs e)
        {
            Get_Credit(new Prets());
            Loard_chart();
        }

        
        private DataParameter _inputparameter;
        struct DataParameter
        {
            public List<IPrets> ListePret;
            public string Filename{ get; set;}
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_data();
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lab_status.Text = string.Format("Progressing...{0}%", e.ProgressPercentage);
            progressBar1.Update();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Thread.Sleep(100);
                MessageBox.Show("File has been Saved...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                progressBar1.Value = 0;
                lab_status.Text = "Progressing...0%";
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<IPrets> liste = ((DataParameter)e.Argument).ListePret;
            string filename = ((DataParameter)e.Argument).Filename;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)excel.ActiveSheet;
            excel.Visible = true;
            int index = 1;
            int progress = liste.Count;
            //Add column
            ws.Cells[1, 1] = "Number";
            ws.Cells[1, 2] = "Name";
            ws.Cells[1, 3] = "Last name";
            ws.Cells[1, 4] = "Surname";
            ws.Cells[1, 5] = "Mount";
            ws.Cells[1, 6] = "Interest";
            ws.Cells[1, 7] = "Repay mount";
            ws.Cells[1, 8] = "Credit date";
            ws.Cells[1, 9] = "Repay date";
            foreach (IPrets pret in liste)
            {
                if(!backgroundWorker.CancellationPending)
                {
                    backgroundWorker.ReportProgress(index++ * 100 / progress);
                    ws.Cells[index, 1] = pret.Id;
                    ws.Cells[index, 2] = pret.Nom;
                    ws.Cells[index, 3] = pret.Postnom;
                    ws.Cells[index, 4] = pret.Prenom;
                    ws.Cells[index, 5] = pret.Montant;
                    ws.Cells[index, 6] = pret.Interet;
                    ws.Cells[index, 7] = pret.Montantpaye;
                    ws.Cells[index, 8] = pret.DatePret;
                    ws.Cells[index, 9] = pret.DateRembour;
                }
            }

            //Save file
            ws.SaveAs(filename, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            excel.Quit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
                return;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel workbook|*.xlsx" })
            {
                if(sfd.ShowDialog()==DialogResult.OK)
                {
                    _inputparameter.Filename = sfd.FileName;
                    _inputparameter.ListePret = new Prets().Allcredit();
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;
                    backgroundWorker.RunWorkerAsync(_inputparameter);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable dt = Dynamic_Classe.Instance().ExportDatagrod_toDatatable(dataGridView1, dtable);
                Dynamic_Classe.Instance().GeneratePDF(dtable, @"E:\Liste_client.pdf", "Friend List");
                System.Diagnostics.Process.Start(@"E:\Liste_client.pdf");
                //this.WindowState = System.Windows.Forms.FormWindowState.Minimized;

            }catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Get_Credit_annee(new Prets());
            }catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message,"Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(comboBox1.Text))
                    return;
                else
                    Get_Credit_annee_mois(new Prets());
            }catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch !=8 && ch != 48)
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Modifier();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Get_Credit(new Prets());
        }
    }
}
