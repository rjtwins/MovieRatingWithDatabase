using System;
using System.Windows.Forms;

namespace MovieRatingWithDatabase
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IDataController APICaller = new DataController();
            Form1 Display = new Form1();

            IController Controller = new Controller(Display, APICaller);
            Display.SetController(Controller);

            Application.Run(Display);
        }
    }
}