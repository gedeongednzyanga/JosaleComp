using JosaleApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JosaleApp.Forms
{
    public partial class Preview_form : Form
    {
       
        public Preview_form()
        {
            InitializeComponent();
        }
        private void Preview_form_Load(object sender, EventArgs e)
        {
            this.reportViewp.RefreshReport();
        }
    }
}
