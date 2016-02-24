using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace WhatsApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

           // ToggleConfigEncryption(Application.ExecutablePath);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //SQLiteBDDataSet ds = new SQLiteBDDataSet();
            Application.Run(new Form1());
        }
        static void ToggleConfigEncryption(string exeConfigName)
        {
            // Takes the executable file name without the
            // .config extension.
            try
            {
                Configuration config = ConfigurationManager.
                OpenExeConfiguration(exeConfigName);
                ConnectionStringsSection section =
                    config.GetSection("connectionStrings")
                    as ConnectionStringsSection;
                if (section.SectionInformation.IsProtected)
                    section.SectionInformation.UnprotectSection();
                section.ConnectionStrings["WhatsApp.Properties.Settings.SQLiteBDConnectionString1"].ConnectionString = @"data source="+Application.StartupPath+@"\SQLiteBD.bd";
                // Save the current configuration.
                config.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program.cs Зпись в конфиг" + ex.Message);
            }
        }
    }
}
