using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScreenBlocker
{
    public class BackgroundLoading
    {
        public delegate void RunFunction();

        public BackgroundWorker Bw;
        public RunFunction thisFunction;
        LoadingForm newLoading;

        public BackgroundLoading(RunFunction newFunction)
        {
            thisFunction = newFunction;
            Bw = new BackgroundWorker();
            Bw.DoWork += new DoWorkEventHandler(Bw_DoWork);
            Bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bw_RunWorkerCompleted);
        }

        public void Start()
        {
            Bw.RunWorkerAsync();
            newLoading = new LoadingForm();
            newLoading.ShowDialog();
        }

        void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            newLoading.Dispose();
        }

        void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (thisFunction != null)
                thisFunction();
        }
    }
}
