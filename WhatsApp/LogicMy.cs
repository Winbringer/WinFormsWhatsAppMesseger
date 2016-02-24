using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace WhatsApp
{
    sealed class LogicMy
    {
        /// <summary>
        /// Возврашяет цифры в виде времени
        /// </summary>
        /// <param name="unixTime">Целое число прдеставляющее собой количество секунд прощедших с 01.01.1970</param>
        /// <returns>Дату и время соответствующую заданному в параметрах числу</returns>
        public static DateTime unixToDT(double unixTime)
        {
            DateTime Fecha = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Fecha.ToLocalTime().AddSeconds(unixTime);
        } //unixToDT
        /// <summary>
        /// Возварщает время в виде цифр.
        /// </summary>
        /// <param name="dt">Местное Датя и время которое нужно превратить в цифры.</param>
        /// <returns>Целое число прдеставляющее собой количество секунд прощедших с 01.01.1970 соответствующих задданно в параметрах дате</returns>
        public static int dtToUnix(DateTime dt)
        {
            int unixTime = (int)(dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            return unixTime;
        } //dtToUnix  
        /// <summary>
        /// Метод для удаления записи из бд  По айди взятому из таблицы на форме.
        /// </summary>
        /// <param name="dg">Таблица DataGridView на форме</param>
        /// <param name="tb">Название таблицы в бд из которой нужно удалить строку</param>
        public static void DelFromGridInDb(DataGridView dg, String tb)
        {
            if (MessageBox.Show("Вы действительно хотите удалить выбранную строку из базы?", "Подтвердите", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                using (DataMy dt = DataMy.GetInstanse())
                {
                    if (dg.CurrentRow != null)
                    {
                        switch (tb)
                        {
                            case "Cannels":
                                dt.DelChannels(Int32.Parse(dg.CurrentRow.Cells[0].Value.ToString()));
                                break;
                            case "Contacts":
                                dt.DelContacts(Int32.Parse(dg.CurrentRow.Cells[0].Value.ToString()));
                                break;
                            case "group_contacts":
                                dt.DelGroups(Int32.Parse(dg.CurrentRow.Cells[0].Value.ToString()));
                                break;
                            default:
                                MessageBox.Show("Введите правильно название таблицы!");
                                return;
                        }  //switch(tb)                            
                    } //if (dg.CurrentRow != null)
                } //using (DataMy dt = DataMy.GetInstanse())
            } //If  == DialogResult.OK)
        } //DelFromGridInDb
        /// <summary>
        /// Мето для ведения лога ощибок в отдельном текстовом файле
        /// </summary>
        /// <param name="e"> Ощибка типа Exception Которую нужно записать в файл</param>
        /// <param name="pathE">Путь где возникла ошибка в виде строки. Например в Классе ClassMy В Методе MetodMy в блоке if(a==b) </param>
        public static void LogErorrMy(Exception e, String pathE)
        {
            using (StreamWriter sr = File.AppendText(Application.StartupPath + @"\LogExceptions.txt"))
                sr.Write(e.Message + ":---:" + e.Source + ":---:" + e.TargetSite + ":---:" + pathE + "\r\n");

        }
        /// <summary>
        /// Метод для добавления каналов из специально созданной для этого формы.
        /// </summary>
        /// <param name="maskedTextBox1">TextBox C номером телефона</param>
        /// <param name="maskedTextBox2">Сегодня отправленно</param>
        /// <param name="maskedTextBox3">Всего отправленно</param>
        /// <param name="dateTimePicker1">Дата регистрации</param>
        /// <param name="dateTimePicker2">Последний раз авторизовался</param>
        /// <param name="dateTimePicker3">Дата блокировки канала</param>
        /// <param name="checkBox1">Активный ли канал</param>
        /// <param name="textBox1">Пароль</param>
        /// <param name="textBox2">Ник пользователя</param>
        /// <param name="textBox5">Ощибка пре авторизации</param>
        /// <returns>Возврашает true при успешном выполнени операции. Иначе ничего</returns>
        public static bool AddChannel(TextBox maskedTextBox1, MaskedTextBox maskedTextBox2,
            MaskedTextBox maskedTextBox3, DateTimePicker dateTimePicker1,
            DateTimePicker dateTimePicker2, DateTimePicker dateTimePicker3,
            CheckBox checkBox1,
            TextBox textBox1, TextBox textBox2, TextBox textBox5)
        {
            int dr = LogicMy.dtToUnix(dateTimePicker1.Value);
            int ll = LogicMy.dtToUnix(dateTimePicker2.Value);
            int? bd = LogicMy.dtToUnix(dateTimePicker3.Value);
            if (dateTimePicker3.Value.Date == DateTime.Now.Date)
            {
                bd = null;
            }
            Int32 i;
            Int32 j;
            if (!Int32.TryParse(maskedTextBox2.Text.Trim().Replace(" ", ""), out i)) i = 0;
            if (!Int32.TryParse(maskedTextBox3.Text.Trim().Replace(" ", ""), out j)) j = 0;
            Int32 k = 0;
            if (checkBox1.Checked) k = 1;
            using (DataMy dt = DataMy.GetInstanse())
            {
                dt.InsertChannels(
                    maskedTextBox1.Text.Trim(),
                   textBox1.Text,
                    textBox2.Text,
                   i,
                    j,
                    dr,
                    ll,
                    bd,
                   textBox5.Text,
                   k);

            }
            return true;
        }// Конец AddChannel
        /// <summary>
        /// Метод для сортировки грида по айди.
        /// </summary>
        /// <param name="dataGridView1">Таблица которую нужно сортировать</param>
        /// <param name="b">Булев флаг сообщающий о том была ли таблица отсортированна до этого</param>
        public static void SortChannelsIdColumn(DataGridView dataGridView1, bool b)
        {
            if (b)
            {
                dataGridView1.Sort(new RowComparer(SortOrder.Ascending));
            }
            else
            {
                dataGridView1.Sort(new RowComparer(SortOrder.Descending));
            }
            dataGridView1.Columns[0].HeaderCell.SortGlyphDirection = b ? SortOrder.Ascending : SortOrder.Descending;
        } //Конец метода для сортировки по стобцу Айди
        /// <summary>
        /// Класс для Реализции метода Компаре при помоши которого сортируеться грид
        /// </summary>
        private class RowComparer : System.Collections.IComparer
        {
            private static int sortOrderModifier = 1;

            public RowComparer(SortOrder sortOrder)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

                //Попытка соритовать по Айди
                int CompareResult = IntCompare(
                       Int32.Parse(DataGridViewRow1.Cells[0].Value.ToString()),
                        Int32.Parse(DataGridViewRow2.Cells[0].Value.ToString()));
                //Если не вышло то сорируем по соседнему стобцу
                if (CompareResult == 0)
                {
                    CompareResult = String.Compare(
                       DataGridViewRow1.Cells[1].Value.ToString(),
                        DataGridViewRow2.Cells[1].Value.ToString());
                }
                return CompareResult * sortOrderModifier;
            }
            public int IntCompare(int a, int b)
            {
                int c = 0;
                if (a < b) c = -1;
                if (a > b) c = 1;
                if (a == b) c = 0;
                return c;
            }
        }//Класс РоуКомпарер
        
        /// <summary>
        /// Проверяет каналлы на активность.
        /// </summary>
        public static void CheckChannels(ProgressBar pb, DataGridView dgv)
        {
            using (DataMy dt = DataMy.GetInstanse())
            {
                using (DataTable dtt = dt.GetTable("Cannels"))
                {
                    pb.Minimum = 0;
                    pb.Maximum = dtt.Rows.Count;
                    pb.Value = 0;
                    pb.Step = 1;
                    pb.Visible = true;
                    for (int j = 0; j < dtt.Rows.Count; ++j) 
                    {
                        ScrolRow(dgv, j);
                        pb.PerformStep();
                        DataRow dr = dtt.Rows[j];
                        ChekWALogin(dr, dt);
                    } // foreach (DataRow dr in dtt)
                }//using (DataTable dtt = dt.GetTable("Cannels"))
            } //using (DataMy dt = DataMy.GetInstanse())
        }

        private static void ChekWALogin( DataRow dr, DataMy dt)
        {
            Int32 ok = 1;
            Int32 no = 0;
            decimal iiii;
            Int32 i = Convert.ToInt32(dr[0].ToString());
            dr[1] = dr[1].ToString().Replace("+", "");
            dr[1] = dr[1].ToString().Replace("-", "");
            dr[1] = dr[1].ToString().Replace("(", "");
            dr[1] = dr[1].ToString().Replace(")", "");
            dr[1] = dr[1].ToString().TrimStart(new char[] { '0' });
            // MessageBox.Show(dr[1].ToString()+"Первый");
            decimal.TryParse(dr[1].ToString().Trim(), out iiii);
            if (iiii <= 0) return;
            if (i >= 0 && i < 2147483647)
            {
                //  MessageBox.Show(dr[1].ToString()+"Второй"+iiii.ToString());
                string namber = dr[1].ToString() ?? " ";
                //WhatsAppApi.Parser.PhoneNumber pn =new WhatsAppApi.Parser.PhoneNumber(namber);
                string passWord = dr[2].ToString() ?? " ";
                string nik = dr[3].ToString() ?? " ";
                WhatsAppApi.WhatsApp wa = new WhatsAppApi.WhatsApp(namber, passWord, nik);

                wa.OnConnectSuccess += () =>
                {
                    //MessageBox.Show("Присоеденились");                  
                    wa.OnLoginSuccess += (phoneNumber, data) =>
                    {
                        // MessageBox.Show("Залогинились");
                        dt.UpdateIsActive(i, ok);
                        dt.UpdateEror(i, " ");
                        dt.UpdateDateLastLogin(i, LogicMy.dtToUnix(DateTime.Now));
                    };
                    wa.OnLoginFailed += (data) =>
                    {
                        // MessageBox.Show("Не удалось залогиниться: " +data);
                        dt.UpdateIsActive(i, no);
                        dt.UpdateEror(i, data.ToString());
                        dt.UpdateDateBlock(i, LogicMy.dtToUnix(DateTime.Now));
                    };
                    wa.OnError += (id, from, code, text) =>
                    {
                        dt.UpdateIsActive(i, no);
                        dt.UpdateEror(i, id + " : " + @from + " : " + code + " : " + text);
                        //MessageBox.Show(id + " : " + from + " : " + code + " : " + text);
                    };
                    wa.Login();
                };
                wa.OnConnectFailed += (ex) =>
                {
                    // MessageBox.Show("Не удалось присоедениться");
                    dt.UpdateEror(i, ex.Message);
                };
                wa.Connect();
                wa.Disconnect();
            }
        }

        public static void ChekWALoginMy(DataRow dr, DataMy dt)
        {
            Int32 ok = 1;
            Int32 no = 0;
            decimal iiii;
            Int32 i = Convert.ToInt32(dr[0].ToString());
            dr[1] = dr[1].ToString().Replace("+", "");
            dr[1] = dr[1].ToString().Replace("-", "");
            dr[1] = dr[1].ToString().Replace("(", "");
            dr[1] = dr[1].ToString().Replace(")", "");
            dr[1] = dr[1].ToString().TrimStart(new char[] { '0' });
            // MessageBox.Show(dr[1].ToString()+"Первый");
            decimal.TryParse(dr[1].ToString().Trim(), out iiii);
            if (iiii <= 0) return;
            if (i >= 0 && i < 2147483647)
            {
                //  MessageBox.Show(dr[1].ToString()+"Второй"+iiii.ToString());
                string namber = dr[1].ToString() ?? " ";
                //WhatsAppApi.Parser.PhoneNumber pn =new WhatsAppApi.Parser.PhoneNumber(namber);
                string passWord = dr[2].ToString() ?? " ";
                string nik = dr[3].ToString() ?? " ";
                WhatsAppApi.WhatsApp wa = new WhatsAppApi.WhatsApp(namber, passWord, nik);

                wa.OnConnectSuccess += () =>
                {
                  // MessageBox.Show("Присоеденились");                  
                    wa.OnLoginSuccess += (phoneNumber, data) =>
                    {
                         MessageBox.Show("Залогинились");
                        dt.UpdateIsActive(i, ok);
                        dt.UpdateEror(i, " ");
                        dt.UpdateDateLastLogin(i, LogicMy.dtToUnix(DateTime.Now));
                    };
                    wa.OnLoginFailed += (data) =>
                    {
                         MessageBox.Show("Не удалось залогиниться: " +data);
                        dt.UpdateIsActive(i, no);
                        dt.UpdateEror(i, data.ToString());
                        dt.UpdateDateBlock(i, LogicMy.dtToUnix(DateTime.Now));
                    };
                    wa.OnError += (id, from, code, text) =>
                    {
                        dt.UpdateIsActive(i, no);
                        dt.UpdateEror(i, id + " : " + @from + " : " + code + " : " + text);
                        MessageBox.Show(id + " : " + from + " : " + code + " : " + text);
                    };
                    wa.Login();
                };
                wa.OnConnectFailed += (ex) =>
                {
                     MessageBox.Show("Не удалось присоедениться");
                    dt.UpdateEror(i, ex.Message);
                };
                wa.Connect();
                wa.Disconnect();
            }
        }
        private static void ScrolRow(DataGridView dgv, int j)
        {
            if (j < dgv.Rows.Count)
            {
                dgv.ClearSelection();
                dgv.Rows[j].Selected = true;
                dgv.FirstDisplayedScrollingRowIndex = j;
                dgv.Update();
            }
        }

// Конец CheckChannels()
    } //Конец класса LogicMy

}
