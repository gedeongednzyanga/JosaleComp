using JosaleApp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JosaleApp
{
    public partial class SplachScreen : MetroFramework.Forms.MetroForm
    {
        public SplachScreen()
        {
            InitializeComponent();
        }

        Task ProgressImport (List<string> data, IProgress<ProgressReport>progress)
        {
            int index = 1;
            int totalProgress = data.Count;
            var progressReport = new ProgressReport();
            return Task.Run(() =>
            { for (int i = 0; i < totalProgress; i++)
                {
                    progressReport.PercentComplete = index++ * 100 / totalProgress;
                    progress.Report(progressReport);
                    Thread.Sleep(5);
                }
            });
        }

        async void Loading()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 1000; i++)
                list.Add(i.ToString());
            label1.Text = "Working...";
            var progressReport = new Progress<ProgressReport>();
            progressReport.ProgressChanged += (o, report) =>
            {
                label1.Text = string.Format("Loading...{0}%", report.PercentComplete);
                bunifuProgressBar1.Value = report.PercentComplete;
                bunifuProgressBar1.Update();
            };
            await ProgressImport(list, progressReport);
            this.Close();
        }

        private void SplachScreen_Load(object sender, EventArgs e)
        {
            Loading();
        }
    }

 
}
