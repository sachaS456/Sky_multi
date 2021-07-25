using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Sky_multi
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static int Main()
        {
            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_Exception);
            Application.Run(new MainForm());

            return 0;
        }

        private static void Application_Exception(object sender, ThreadExceptionEventArgs e)
        {
            if (MessageBox.Show("An error has occurred! " + e.Exception.Message + "Do you want to close Sky multi?", "Sky multi", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                Environment.Exit(-1);
            }
        }
    }
}
