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
        private void Credit_user_Load(object sender, EventArgs e)
        {
            Get_Credit(new Prets());
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_data();
        }
    }
}
