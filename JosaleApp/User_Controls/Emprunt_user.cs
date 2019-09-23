using System;
using Emprunts_Lib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JosaleApp.User_Controls
{
    public partial class Emprunt_user : UserControl
    {
        public Emprunt_user()
        {
            InitializeComponent();
        }
        void Load_emprunt(IEmprunt emprunt)
        {
            dataGridView1.DataSource = emprunt.AllEmprunt();
        }
        void Search()
        {
          dataGridView1.DataSource = new Emprunt().Search(text_search.Text.Trim());
        }
        private void Emprunt_user_Load(object sender, EventArgs e)
        {
            Load_emprunt(new Emprunt());
        }

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}
