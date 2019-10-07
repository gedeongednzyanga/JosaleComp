using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prets_Lib;

namespace JosaleApp.User_Controls
{
    public partial class Rembourssement_user : UserControl
    {
        public Rembourssement_user()
        {
            InitializeComponent();
        }

        void Get_Rembourssement (IPrets rembou)
        {
            dataGridView1.DataSource = rembou.AllRembou();
        }

        void Get_data()
        {
            int i = dataGridView1.CurrentRow.Index;
            customername.Text = " :  " + dataGridView1["Column2", i].Value.ToString()+" " + dataGridView1["Column3", i].Value.ToString()+" " + dataGridView1["Column4", i].Value.ToString();
            mount.Text = " :  " +dataGridView1["Column5", i].Value.ToString()+"$";
            reste.Text = " :  " + dataGridView1["Column6", i].Value.ToString() + "$";
            dateope.Text = " :  " + dataGridView1["Column7", i].Value.ToString();
        }
        private void Rembourssement_user_Load(object sender, EventArgs e)
        {
            Get_Rembourssement(new Prets());
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Get_data();
        }
    }
}
