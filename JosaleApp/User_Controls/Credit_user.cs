using System;
using Prets_Lib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JosaleApp.Classes;
using System.Threading;

namespace JosaleApp.User_Controls
{
    public partial class Credit_user : UserControl
    {
        int Id = 0;
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
        void Get_Credit(IPrets credit)
        {
            dataGridView1.DataSource = credit.Allcredit();
            Load_annee();
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

        void Export_data()
        {
            DataTable dt = new DataTable();
            DataTable ds = new DataTable();
            while (dt.Columns.Count < ds.Columns.Count)
            {

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
            public int progress;
            public int delay;
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
            //if (e.Error == null)
            //{
            //Thread.Sleep(100);
            MessageBox.Show("Progress has been completed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int progress = ((DataParameter)e.Argument).progress;
            int delay = ((DataParameter)e.Argument).delay;
            int idex = 1;
            try
            {
                for (int i = 0; i < progress; i++)
                {
                    backgroundWorker.ReportProgress(idex++ * 100 / progress, string.Format("Progress data {0}", i));
                    Thread.Sleep(delay);
                }
            }
            catch (Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker.IsBusy)
            {
                _inputparameter.delay = 10;
                _inputparameter.progress = 1200;
                backgroundWorker.RunWorkerAsync(_inputparameter);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
        }
    }
}
