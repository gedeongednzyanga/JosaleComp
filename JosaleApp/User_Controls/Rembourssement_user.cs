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

        private void Rembourssement_user_Load(object sender, EventArgs e)
        {
            Get_Rembourssement(new Prets());
        }
    }
}
