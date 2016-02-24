using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data;
namespace WhatsApp
{
    /// <summary>
    /// Клас для выгрузки и загрузки данных из в бд.
    /// </summary>
    class LoadingInFromFile : IDisposable
    {        
        private static LoadingInFromFile instance;
        private bool disposed = false;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;        
        /// <summary>
        /// Конструктор
        /// </summary>
        protected LoadingInFromFile()
        {

            this.openFileDialog1 = new OpenFileDialog()
            {
                DefaultExt = "csv",
                InitialDirectory = Application.StartupPath.ToString(),
                Filter = "В формате csv(*.csv)|*.csv| В формате программы (*.wpsm)|*.wpsm"
            }; //openFileDialog1
            this.saveFileDialog1 = new SaveFileDialog()
            {
                DefaultExt = "wpsm",
                InitialDirectory = Application.StartupPath.ToString(),
                Filter = "В формате программы (*.wpsm)|*.wpsm"
            };  //saveFileDialog1         


        } //LoadingChannelsInFromFile
        
        /// <summary>
        /// Сохраниние каналов в файл c типом программы
        /// </summary>
        public void SaveChannelsToFile()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            using (DataMy dt = DataMy.GetInstanse())
            {
                using (DataTable dtb = dt.GetTable("Cannels"))
                {
                    dtb.WriteXml(saveFileDialog1.FileName);
                }
            }
        } //SaveChannelsToFile       
        /// <summary>
        ///Загрузка данных из файла в таблицу каналы БД
        /// </summary>
        public void LoadChannelsFromFile()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            if (openFileDialog1.FileName.ToString().ToLower().EndsWith(".wpsm"))
            {
                using (DataSet ds = new DataSet())
                {
                    ds.ReadXml(openFileDialog1.FileName);
                    foreach (DataTable dbt in ds.Tables)
                    {
                        DataMy.LoadChannelsToDT(dbt);
                    }
                }
            } // if (openFileDialog1.FileName.ToString().IndexOf(".wpsm")>0)
            if (openFileDialog1.FileName.ToString().ToLower().EndsWith(".csv"))
            {

                String[] f = File.ReadAllLines(openFileDialog1.FileName);
                using (DataTable dt = new DataTable())
                {
                    dt.Columns.Add("Number");
                    dt.Columns.Add("Password");
                    dt.Columns.Add("Today_send");
                    dt.Columns.Add("Total_send");
                    dt.Columns.Add("Date_register");
                    dt.Columns.Add("Is_active");
                    for (int i = 0; i < f.Length; ++i)
                    {
                        string[] sf = f[i].Split(',');
                        dt.Rows.Add(sf[0], sf[1],0,0,LogicMy.dtToUnix(DateTime.Now),0);
                    }
                    DataMy.LoadChannelsToDT(dt);
                }


            }//if (openFileDialog1.FileName.ToString().IndexOf(".csv") > 0)

        }//LoadChannelsFromFile
        /// <summary>
        /// Свойство для получения экземпляра класса одиночки
        /// </summary>
        public static LoadingInFromFile Instance
        {
            get
            {
                if (instance != null && instance.disposed) return instance = new LoadingInFromFile();
                return instance ?? (instance = new LoadingInFromFile());
            }
        }
        /// <summary>
        /// Реализация интерфейса IDisposeble
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } //Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Освобождаем только управляемые ресурсы
                    openFileDialog1.Dispose();
                    saveFileDialog1.Dispose();
                }

                // Освобождаем неуправляемые ресурсы
                disposed = true;
            }
        }//Dispose(bool disposing)
    } //Конец class LoadingInFromFile
}
