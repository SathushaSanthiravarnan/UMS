using System;
using System.Windows.Forms;
using Unicom_Tic_Management_System.Datas;
using Unicom_Tic_Management_System.ViewForms;
namespace Unicom_Tic_Management_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Migration.CreateTables();
            DatabaseSeeder.SeedAdminUser();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
