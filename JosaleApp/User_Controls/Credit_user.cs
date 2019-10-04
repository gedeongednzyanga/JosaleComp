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

namespace JosaleApp.User_Controls
{
    public partial class Credit_user : UserControl
    {
        int Id = 0;
        public Credit_user()
        {
            InitializeComponent();
        }

        void Get_data()
        {
            int i = dataGridView1.CurrentRow.Index;
            Id = Convert.ToInt32(dataGridView1["Column1", i].Value.ToString());
            Get_gage();
        }
        void Get_gage()
        {
            listView1.Items.Clear();
            Dynamic_Classe.Instance().Load_gage(Id, listView1);
        }
        void Get_Credit(IPrets credit)
        {
            dataGridView1.DataSource = credit.Allcredit();
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
    
        private void Credit_user_Load(object sender, EventArgs e)
        {
            Get_Credit(new Prets());
            Loard_chart();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_data();
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}
