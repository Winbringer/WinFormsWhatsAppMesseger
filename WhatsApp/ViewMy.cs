using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsApp
{
    /// <summary>
    /// Класс для визуализации таблицы Каналы, Контакты и Группы на форме.
    /// </summary>
    class ViewMyDataTableToGridView : IDisposable
    {
        DataTable dt;
        DataGridView dg;
        bool disposed = false;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dt1">Таблица из которой будут браться данные</param>
        /// <param name="dg1">Таблица на форме в которой будут отображаться данные</param>
        public ViewMyDataTableToGridView(DataTable dt1, DataGridView dg1)
        {
            this.dg = dg1;
            this.dt = dt1;
            this.dg.ColumnCount = this.dt.Columns.Count;
            this.dg.ColumnHeadersVisible = true;
            this.dg.AllowUserToAddRows = false;
            this.dg.RowCount = this.dt.Rows.Count;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new System.Drawing.Font("Verdana", 8, FontStyle.Bold);
            this.dg.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        } //Конец конструктора
        /// <summary>
        /// Метод для отображения таблицы каналлы на форме.
        /// </summary>
        /// <param name="lAll">Текстовая меткая для отображения общего количества каналлнов</param>
        /// <param name="lActive">Метка для отображения количества активных каналов</param>
        /// <param name="lLocked">Метка для отображения количества заблокированных каналов</param>
        /// <param name="cb">Чекбокс для потверждения отображения только заблокированных каналов</param>
        public void ChannelsGridFiller(Label lAll, Label lActive, Label lLocked, CheckBox cb)
        {
            //var m = this.dt.Select().Where(x => x["is_active"].ToString().Trim() == "0");

            int activ = 0, loked = 0;
            lAll.Text = String.Format("Всего: {0}", this.dg.RowCount);
            dg.Columns[0].Name = "Айди";
            dg.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dg.Columns[1].Name = "Активный";
            dg.Columns[2].Name = "Номер";
            dg.Columns[3].Name = "Пароль";
            dg.Columns[4].Name = "Ник";
            dg.Columns[5].Name = "Сегодня отправленно сообщений";
            dg.Columns[6].Name = "Всего отправленно сообщений";
            dg.Columns[7].Name = "Дата регистрации";
            dg.Columns[8].Name = "Последний раз заходил";
            dg.Columns[9].Name = "Дата блокировки";
            dg.Columns[10].Name = "Ошибка при аутентификации";
            dg.Columns.Remove("Активный");
            DataGridViewCheckBoxColumn dgC = new DataGridViewCheckBoxColumn();

            dgC.Name = "Активный";
            dg.Columns.Insert(1, dgC);
            for (int i = 1; i < 11; ++i)
            {
                dg.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            for (int i = 0; i < this.dg.Rows.Count; ++i)
            {
                if (this.dt.Rows[i]["Is_active"].ToString() == "1")
                {
                    this.dg.Rows[i].Cells["Активный"].Value = CheckState.Checked;
                    ++activ;
                    this.dg.Rows[i].DefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.FromArgb(255, 255, 255) };
                    this.dg.Rows[i].Visible = !cb.Checked;
                }
                if (this.dt.Rows[i]["Is_active"].ToString() == "0" || this.dt.Rows[i]["Is_active"].ToString() == "")
                {
                    this.dg.Rows[i].Cells["Активный"].Value = CheckState.Unchecked;
                    ++loked;
                    this.dg.Rows[i].DefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.FromArgb(255, 150, 150) };
                    this.dg.Rows[i].Visible = true;
                }
                this.dg.Rows[i].Cells["Айди"].Value = this.dt.Rows[i]["Id_cannels"].ToString();
                this.dg.Rows[i].Cells["Номер"].Value = this.dt.Rows[i]["Number"].ToString();
                this.dg.Rows[i].Cells["Пароль"].Value = this.dt.Rows[i]["Password"].ToString();
                this.dg.Rows[i].Cells["Ник"].Value = this.dt.Rows[i]["Nick_name"].ToString();
                this.dg.Rows[i].Cells["Сегодня отправленно сообщений"].Value = this.dt.Rows[i]["Today_send"].ToString();
                this.dg.Rows[i].Cells["Всего отправленно сообщений"].Value = this.dt.Rows[i]["Total_send"].ToString();
                string dr = "";
                if (this.dt.Rows[i]["Date_register"] != DBNull.Value)
                {
                    DateTime dtm = LogicMy.unixToDT(Double.Parse(this.dt.Rows[i]["Date_register"].ToString()));
                    dr = String.Format("{0}", dtm);
                }
                this.dg.Rows[i].Cells["Дата регистрации"].Value = dr;
                string dr1 = "";
                if (this.dt.Rows[i]["Last_login"] != DBNull.Value)
                {
                    DateTime dtm = LogicMy.unixToDT(Double.Parse(this.dt.Rows[i]["Last_login"].ToString()));
                    dr1 = String.Format("{0}", dtm);
                }
                this.dg.Rows[i].Cells["Последний раз заходил"].Value = dr1;
                string dr2 = "";
                if (this.dt.Rows[i]["Block_date"] != DBNull.Value)
                {
                    DateTime dtm = LogicMy.unixToDT(Double.Parse(this.dt.Rows[i]["Block_date"].ToString()));
                    dr2 = String.Format("{0}", dtm);
                }
                this.dg.Rows[i].Cells["Дата блокировки"].Value = dr2;
                this.dg.Rows[i].Cells["Ошибка при аутентификации"].Value = this.dt.Rows[i]["Login_error"].ToString();
                for (int j = 0; j < 11; ++j) this.dg.Rows[i].Cells[j].ReadOnly = true;

            } //Конец  for (int i = 0; i < this.dg.Rows.Count; ++i)
            lActive.Text = String.Format("Активных: {0}", activ);
            lLocked.Text = String.Format("Заблокированных: {0}", loked);
            dg.Columns[0].HeaderCell.SortGlyphDirection = SortOrder.None;
        } // Конец ChannelsGridFiller()
        /// <summary>
        /// Метод для заполнения таблицы группы
        /// </summary>
        public void GroupsGridFiller()
        {
            dg.Columns[0].Name = "Айди";
            dg.Columns[1].Name = "Название группы";
            dg.Columns[2].Name = "Коментарий";
            for (int i = 1; i < 3; ++i)
            {
                dg.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            for (int i = 0; i < this.dg.Rows.Count; ++i)
            {
                this.dg.Rows[i].Cells["Айди"].Value = this.dt.Rows[i]["id_group"].ToString();
                this.dg.Rows[i].Cells["Название группы"].Value = this.dt.Rows[i]["name_group"].ToString();
                this.dg.Rows[i].Cells["Коментарий"].Value = this.dt.Rows[i]["coment"].ToString();
            }
        }
        /// <summary>
        /// Заполняет таблицу контакты
        /// </summary>
        /// <param name="checkBox2">Галочка для проверки фильтровать ли по группам</param>
        /// <param name="comboBox1">Название группы по которой надо фильтровать.</param>
        public void ContactsGridFiller(CheckBox checkBox2, ComboBox comboBox1)
        {
            dg.Columns[0].Name = "Айди";
            dg.Columns[1].Name = "Название группы";
            dg.Columns[2].Name = "Дата регистрации";
            dg.Columns[3].Name = "Фамилия";
            dg.Columns[4].Name = "Имя";
            dg.Columns[5].Name = "Дата рождения";
            dg.Columns[6].Name = "Телефон";
            dg.Columns[7].Name = "Email";
            dg.Columns[8].Name = "Пол";
            dg.Columns[9].Name = "Is WhatsApp?";
            dg.Columns[10].Name = "Subscrible?";
            dg.Columns[11].Name = "Коментарий";
            for (int i = 0; i < 12; ++i)
            {
                dg.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            if (dg == null) return;
            DataTable dtm = null;
            using (DataMy dt = DataMy.GetInstanse())
            {
                 dtm = dt.GetTable("group_contacts");

            }
            for (int i = 0; i < this.dg.Rows.Count; ++i)
            {
                this.dg.Rows[i].Cells["Айди"].Value = this.dt.Rows[i]["Id_contact"].ToString();
                var s = dtm.AsEnumerable();
                var m = from ss in s
                    where ss["id_group"].ToString() == this.dt.Rows[i]["Id_group"].ToString()
                    select ss["name_group"];
                var k = m.FirstOrDefault();
                string nameGrop=String.Empty;
                if (k != null) nameGrop = k.ToString();
                this.dg.Rows[i].Cells["Название группы"].Value = nameGrop;
                string dataString = this.dt.Rows[i]["Date_reg"].ToString();
                DateTime dt = LogicMy.unixToDT(Convert.ToInt32(dataString));
                this.dg.Rows[i].Cells["Дата регистрации"].Value = dt;
                this.dg.Rows[i].Cells["Фамилия"].Value = this.dt.Rows[i]["Familiya"].ToString();
                this.dg.Rows[i].Cells["Имя"].Value = this.dt.Rows[i]["Name"].ToString();
                this.dg.Rows[i].Cells["Дата рождения"].Value =LogicMy.unixToDT(Convert.ToInt32( this.dt.Rows[i]["Date_of_birth"].ToString()));
                this.dg.Rows[i].Cells["Телефон"].Value = this.dt.Rows[i]["Phone"].ToString();
                this.dg.Rows[i].Cells["Email"].Value = this.dt.Rows[i]["Email"].ToString();
                this.dg.Rows[i].Cells["Пол"].Value = this.dt.Rows[i]["Sex"].ToString();
                this.dg.Rows[i].Cells["Is WhatsApp?"].Value = this.dt.Rows[i]["Is_whats_app"].ToString().Trim()=="1"?"Yes":"No";
                this.dg.Rows[i].Cells["Subscrible?"].Value = this.dt.Rows[i]["Subscrible"].ToString().Trim() == "1" ? "Yes" : "No";
                this.dg.Rows[i].Cells["Коментарий"].Value = this.dt.Rows[i]["coment"].ToString();
                if (checkBox2 != null && checkBox2.Checked)
                {
                   // MessageBox.Show(comboBox1.Text);
                    if ((string) dg.Rows[i].Cells["Название группы"].Value!=comboBox1.Text)
                    {
                        dg.Rows[i].Visible = false;
                    }
                    else
                    {
                        dg.Rows[i].Visible = true;
                    }
                }
                else
                {
                    dg.Rows[i].Visible = true;
                }
               
            }

        }
        //Реализация интерфейса IDisposable для выгрузки из памяти потоковых данных
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Освобождаем только управляемые ресурсы
                    this.dt.Dispose();
                    //this.dg.Dispose();
                }

                // Освобождаем неуправляемые ресурсы
                disposed = true;
            }
        }
        //Конец Деструктора
    } // Конец class ViewMyDataTableToGridView

}
//   dataGridView1.Rows[0].Cells[0].Style.BackColor = Color.DarkKhaki;
//dg.Columns[10].ValueType = typeof(DataGridViewCheckBoxColumn);