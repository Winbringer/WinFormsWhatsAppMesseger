using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data;
using WhatsApp.SQLiteBDDataSetTableAdapters;


namespace WhatsApp
{
    /// <summary>
    /// Класс для работы с базой данных
    /// </summary>
    class DataMy : IDisposable
    {
        const string DB_NAME = "\\SQLiteBD.bd";
        private string dataSourseMy;
        SQLiteConnection connection;
        private bool _disposed = false;
        private static DataMy instanse;
        /// <summary>
        /// //Конструктор класса, созданиние таблиц и создание подключения
        /// </summary>
        private DataMy()
        {
            dataSourseMy = "Data Source =" + Application.StartupPath + DB_NAME;
            connection = new SQLiteConnection(this.dataSourseMy);
            string sql = "CREATE TABLE IF NOT EXISTS [Cannels]"
                + "([Id_cannels] integer PRIMARY KEY NOT NULL,[Number] text,"
                + "[Password] text,[Nick_name] text,[Today_send] integer,[Total_send] integer,"
                + "[Date_register] integer,[Last_login] integer,[Block_date] integer," +
                "[Login_error] text,[Is_active] integer);";
            this.execute(sql);
            string sql1 = "CREATE TABLE IF NOT EXISTS [Contacts] ("
    + "[Id_contact] integer PRIMARY KEY NOT NULL,"
   + " [Id_group] integer,"
   + " [Date_reg] integer,"
   + " [Familiya] text, [Name] text,[Date_of_birth] integer," +
    "[Phone] text, [Email] text, [Sex] text DEFAULT неопределен," +
    "[Is_whats_app] integer, [Subscrible] integer DEFAULT 1,[coment] text);";
            this.execute(sql1);
            string sql2 = "CREATE TABLE IF NOT EXISTS [group_contacts] (" +
   " [id_group] integer PRIMARY KEY NOT NULL," +
   " [name_group] text,[coment] text);";
            this.execute(sql2);

        }  //Конец Конструктора DataMy()
        /// <summary>
        /// Получение инстанса одиночки
        /// </summary>
        /// <returns>экземпляр класса DataMy </returns>
        public static DataMy GetInstanse()
        {
            if (instanse == null)
            {
                instanse = new DataMy();
                return instanse;

            }
            if (instanse.connection == null)
            {
                instanse = new DataMy();
                return instanse;
            }
            if (!instanse._disposed) return instanse;
            instanse = new DataMy();
            return instanse;
        } //Конец GetInstanse()
        /// <summary>
        /// Метод для выполнения заранее собранной SQL строки
        /// </summary>
        /// <param name="sql">Строковое представление sql команды</param>
        private void execute(string sql)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message + " : " + e.Source);
            }
        } //Конец excute
        /// <summary>
        /// Мето дла получения все данных из таблицы по имени.
        /// </summary>
        /// <param name="tableName">Имя таблицы которую надо получить.</param>
        /// <returns>Таблицу DataTable</returns>
        public DataTable GetTable(string tableName)
        {
            using (DataTable dt = new DataTable())
            {
                try
                {
                    string sql = "SELECT * FROM " + tableName;
                    SQLiteCommand command = new SQLiteCommand(sql, this.connection);
                    //command.Parameters.Add("@name", DbType.String).Value = 'Alex';
                    connection.Open();
                    SQLiteDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                    dt.Dispose();
                }
                catch (Exception e)
                {
                    if (_disposed == false) connection.Close();
                    MessageBox.Show(e.Message + " : " + e.Source + " : " + e.TargetSite);
                }
                return dt;
            }
        }  //Конец GetTable
        /// <summary>
        ///  Метод для добавления нового канала в БД
        /// </summary>
        /// <param name="namber">Номер терефона</param>
        /// <param name="password">Пароль</param>
        /// <param name="nickName">Имя пользователя</param>
        /// <param name="todaySend">Сегодня отправленно</param>
        /// <param name="totalSend">Всего отправленно</param>
        /// <param name="dateRegister">Дата регистрации</param>
        /// <param name="lastLogin">Последний раз заходил</param>
        /// <param name="blockDate">Заблокирован</param>
        /// <param name="loginError">Ошибка при авторизации</param>
        /// <param name="isActive">Активный или нет</param>
        public void InsertChannels(string namber,
            string password, string nickName,
            int todaySend, int totalSend,
            int dateRegister, int lastLogin,
            int? blockDate, string loginError, int isActive)
        {

            try
            {
                string sql = "INSERT INTO Cannels" +
                    " (Number, Password,Nick_name,Today_send,Total_send," +
                    "Date_register,Last_login,Block_date,Login_error,Is_active)" +
                "VALUES (@Number, @Password,@Nick_name,@Today_send,@Total_send," +
                "@Date_register,@Last_login,@Block_date,@Login_error,@Is_active)";
                SQLiteCommand mycommand = new SQLiteCommand(sql, connection);
                mycommand.Parameters.Add("@Number", DbType.String).Value = namber;
                mycommand.Parameters.Add("@Password", DbType.String).Value = password;
                mycommand.Parameters.Add("@Nick_name", DbType.String).Value = nickName;
                mycommand.Parameters.Add("@Today_send", DbType.Int32).Value = todaySend;
                mycommand.Parameters.Add("@Total_send", DbType.Int32).Value = totalSend;
                mycommand.Parameters.Add("@Date_register", DbType.Int32).Value = dateRegister;
                mycommand.Parameters.Add("@Last_login", DbType.Int32).Value = lastLogin;
                mycommand.Parameters.Add("@Block_date", DbType.Int32).Value = blockDate;
                mycommand.Parameters.Add("@Login_error", DbType.String).Value = loginError;
                mycommand.Parameters.Add("@Is_active", DbType.Int32).Value = isActive;
                ExecuteCommandMy(mycommand);
            } //Конец try
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message + " : " + e.Source + ": InsertChannels :" + e.StackTrace);
            }
        } //Конец InsertChannels
        /// <summary>
        /// Метод для удаления каналла из бд
        /// </summary>
        /// <param name="id">Уникальный Айди строки которую надо удалить в базе данных</param>
        public void DelChannels(int id)
        {
            string sql = "DELETE FROM Cannels WHERE Id_cannels=" + id.ToString();
            this.execute(sql);
        } //Конец delChannels
        /// <summary>
        /// Метод для редактирования данных канала
        /// </summary>
        /// <param name="idChannels">Айди канала</param>
        /// <param name="namber">Телефонный норм</param>
        /// <param name="password">Пароль</param>
        /// <param name="nickName">Ник пользователя</param>
        /// <param name="todaySend">Сегодня отправленно</param>
        /// <param name="totalSend">Всего отправленно</param>
        /// <param name="dateRegister">Дата регистрации</param>
        /// <param name="lastLogin">Последний раз заходил</param>
        /// <param name="blockDate">Дата блокировки</param>
        /// <param name="loginError">Общибка пре регистрации</param>
        /// <param name="isActive">Активный или нет</param>
        public void UpdateChannels(int idChannels, string namber,
             string password, string nickName,
             int todaySend, int totalSend,
             int dateRegister, int lastLogin,
             int blockDate, string loginError, int isActive)
        {
            string sql = "UPDATE Cannels " +
 "SET Number=@Number, " +
 "Password=@Password, Nick_name=@Nick_name, " +
 "Today_send=@Today_send, Total_send=@Total_send, " +
 "Date_register=@Date_register, Last_login=@Last_login, " +
 "Block_date=@Block_date, Login_error=@Login_error, " +
 "Is_active=@Is_active " +
"WHERE Id_cannels=@Id_cannels";
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(sql, connection);
                mycommand.Parameters.Add("@Number", DbType.String).Value = namber;
                mycommand.Parameters.Add("@Password", DbType.String).Value = password;
                mycommand.Parameters.Add("@Nick_name", DbType.String).Value = nickName;
                mycommand.Parameters.Add("@Today_send", DbType.Int32).Value = todaySend;
                mycommand.Parameters.Add("@Total_send", DbType.Int32).Value = totalSend;
                mycommand.Parameters.Add("@Date_register", DbType.Int32).Value = dateRegister;
                mycommand.Parameters.Add("@Last_login", DbType.Int32).Value = lastLogin;
                mycommand.Parameters.Add("@Block_date", DbType.Int32).Value = blockDate;
                mycommand.Parameters.Add("@Login_error", DbType.String).Value = loginError;
                mycommand.Parameters.Add("@Is_active", DbType.Int32).Value = isActive;
                mycommand.Parameters.Add("@Id_cannels", DbType.Int32).Value = idChannels;
                ExecuteCommandMy(mycommand);
            } //try
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message + " : " + e.Source + " : " + e.StackTrace);
            }

        } // UpdateChannels 
        /// <summary>
        /// Обновить ошибку при соединении каналла в базе.
        /// </summary>
        /// <param name="idChannels">Айди каналла который надо обновить</param>
        /// <param name="ex">Ошибка которую надо записать в Login_error</param>
        public void UpdateEror(int idChannels, string ex)
        {
            string sql = "UPDATE Cannels SET  Login_error=@Login_error WHERE Id_cannels = @Id_cannels";
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(sql, connection);
                mycommand.Parameters.Add("@Login_error", DbType.String).Value = ex;
                mycommand.Parameters.Add("@Id_cannels", DbType.Int32).Value = idChannels;
                ExecuteCommandMy(mycommand);
            } //try
            catch (Exception e)
            {
                connection.Close();
                LogicMy.LogErorrMy(e, "DataMy.UpdateEror(int idChannels, int isActive)");
            }

        } // Конец  UpdateEror
        /// <summary>
        /// Обновление даты блокировки
        /// </summary>
        /// <param name="idChannels">Ади строки которую надо обновить</param>
        /// <param name="date">Юникс дата которую надо установить</param>
        public void UpdateDateBlock(int idChannels, int date)
        {
            string sql = "UPDATE Cannels SET  Block_date=@Block_date WHERE Id_cannels = @Id_cannels";
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(sql, connection);
                mycommand.Parameters.Add("@Block_date", DbType.Int32).Value = date;
                mycommand.Parameters.Add("@Id_cannels", DbType.Int32).Value = idChannels;
                ExecuteCommandMy(mycommand);
            } //try
            catch (Exception e)
            {
                connection.Close();
                LogicMy.LogErorrMy(e, "DataMy.UpdateDateBlock(int idChannels, int date)");
            }

        } // Конец  UpdateDateBlock
        /// <summary>
        /// Обновление даты последнего обновления
        /// </summary>
        /// <param name="idChannels">Ади строки которую надо обновить</param>
        /// <param name="date">Юникс дата которую надо установить</param>
        public void UpdateDateLastLogin(int idChannels, int date)
        {
            string sql = "UPDATE Cannels SET  Last_login=@Last_login WHERE Id_cannels = @Id_cannels";
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(sql, connection);
                mycommand.Parameters.Add("@Last_login", DbType.Int32).Value = date;
                mycommand.Parameters.Add("@Id_cannels", DbType.Int32).Value = idChannels;
                ExecuteCommandMy(mycommand);
            } //try
            catch (Exception e)
            {
                connection.Close();
                LogicMy.LogErorrMy(e, "DataMy.UpdateDateBlock(int idChannels, int date)");
            }

        } // Конец  UpdateDateBlock
        /// <summary>
        /// Метод для обновления столбца Is_Activ
        /// </summary>
        /// <param name="idChannels">Айди строки которую нужно обновить</param>
        /// <param name="isActive">Значение которое нужно поставить в столбец Is_Active</param>
        public void UpdateIsActive(int idChannels, int isActive)
        {
            string sql = "UPDATE Cannels SET  Is_active=@Is_active WHERE Id_cannels = @Id_cannels";
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(sql, connection);              
                mycommand.Parameters.Add("@Is_active", DbType.Int32).Value = isActive;
                mycommand.Parameters.Add("@Id_cannels", DbType.Int32).Value = idChannels;
                ExecuteCommandMy(mycommand);
            } //try
            catch (Exception e)
            {
                connection.Close();
                LogicMy.LogErorrMy(e, "DataMy.UpdateIsActive(int idChannels, int isActive)");                
            }

        } // UpdateIsActive 
        /// <summary>
        /// Метод для выполнения заранее собранной команды к БД
        /// </summary>
        /// <param name="mycommand">SQLiteCommand которую нужно выполнить</param>
        private void ExecuteCommandMy(SQLiteCommand mycommand)
        {
            if (connection.State != ConnectionState.Open) connection.Open();
            mycommand.ExecuteNonQuery();
            connection.Close();
            mycommand.Dispose();
        } //ExecuteCommandMy
        /// <summary>
        /// Метод для добавления контактов
        /// </summary>
        /// <param name="idGroup">Айди группы в ктотрую входит контакт</param>
        /// <param name="dateReg">Дата регистрации</param>
        /// <param name="familiya">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="dateOfBirth">Дата рождения</param>
        /// <param name="phone">Телефоный номер</param>
        /// <param name="email">Емайл</param>
        /// <param name="sex">Пол</param>
        /// <param name="isWhatsApp">Пользуеться ли WhatsApp</param>
        /// <param name="subscrible">Можно ли подписаться</param>
        /// <param name="coment">Коментарии</param>
        public void InsertContacts(int idGroup, int dateReg,
            string familiya, string name,
            int dateOfBirth, string phone, string email, string sex,
            int isWhatsApp, int subscrible, string coment)
        {
            string sql = "INSERT INTO Contacts " +
                         "(Id_group, Date_reg, Familiya, Name, Date_of_birth, Phone, Sex, Is_whats_app, Subscrible, coment, Email) " +
"VALUES (@Id_group, @Date_reg, @Familiya, @Name, " +
"@Date_of_birth, @Phone, @Sex, @Is_whats_app, @Subscrible, @coment, @Email)";
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(sql, connection);
                mycommand.Parameters.Add("@Id_group", DbType.Int32).Value = idGroup;
                mycommand.Parameters.Add("@Date_reg", DbType.Int32).Value = dateReg;
                mycommand.Parameters.Add("@Familiya", DbType.String).Value = familiya;
                mycommand.Parameters.Add("@Name", DbType.String).Value = name;
                mycommand.Parameters.Add("@Date_of_birth", DbType.Int32).Value = dateOfBirth;
                mycommand.Parameters.Add("@Phone", DbType.String).Value = phone;
                mycommand.Parameters.Add("@Sex", DbType.String).Value = sex;
                mycommand.Parameters.Add("@Is_whats_app", DbType.Int32).Value = isWhatsApp;
                mycommand.Parameters.Add("@Subscrible", DbType.Int32).Value = subscrible;
                mycommand.Parameters.Add("@coment", DbType.String).Value = coment;
                mycommand.Parameters.Add("@Email", DbType.String).Value = email;
                ExecuteCommandMy(mycommand);
            } //try
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message + " : " + e.Source + " : " + e.StackTrace);
            }
        } // InsertContacts
        /// <summary>
        /// //Метод для обновления контактов
        /// </summary>
        /// <param name="idContact">Уникальный айди строки</param>
        /// <param name="idGroup">Айди группы в ктотрую входит контакт</param>
        /// <param name="dateReg">Дата регистрации</param>
        /// <param name="familiya">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="dateOfBirth">Дата рождения</param>
        /// <param name="phone">Телефоный номер</param>
        /// <param name="email">Емайл</param>
        /// <param name="sex">Пол</param>
        /// <param name="isWhatsApp">Пользуеться ли WhatsApp</param>
        /// <param name="subscrible">Можно ли подписаться</param>
        /// <param name="coment">Коментарии</param>
        public void UpdateContacts(int idContact, int idGroup, int dateReg,
            string familiya, string name,
            int dateOfBirth, string phone, string email, string sex,
            int isWhatsApp, int subscrible, string coment)
        {
            string sql = "UPDATE Contacts " +
"SET  Id_group = @Id_group, Date_reg = @Date_reg, " +
"Familiya = @Familiya, Name = @Name, " +
"Date_of_birth = @Date_of_birth, Phone = @Phone, Email = @Email," +
                         "Sex = @Sex, Is_whats_app = @Is_whats_app, Subscrible = @Subscrible, coment = @coment " +
"WHERE  (Id_contact = @Id_contact)";
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(sql, connection);
                mycommand.Parameters.Add("@Id_contact", DbType.Int32).Value = idContact;
                mycommand.Parameters.Add("@Id_group", DbType.Int32).Value = idGroup;
                mycommand.Parameters.Add("@Date_reg", DbType.Int32).Value = dateReg;
                mycommand.Parameters.Add("@Familiya", DbType.String).Value = familiya;
                mycommand.Parameters.Add("@Name", DbType.String).Value = name;
                mycommand.Parameters.Add("@Date_of_birth", DbType.Int32).Value = dateOfBirth;
                mycommand.Parameters.Add("@Phone", DbType.String).Value = phone;
                mycommand.Parameters.Add("@Sex", DbType.String).Value = sex;
                mycommand.Parameters.Add("@Is_whats_app", DbType.Int32).Value = isWhatsApp;
                mycommand.Parameters.Add("@Subscrible", DbType.Int32).Value = subscrible;
                mycommand.Parameters.Add("@coment", DbType.String).Value = coment;
                mycommand.Parameters.Add("@Email", DbType.String).Value = email;
                ExecuteCommandMy(mycommand);
            } //try
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message + " : " + e.Source + " : " + e.StackTrace);
            }
        } //UpdateContacts
        /// <summary>
        /// //Метод для удаления контактов
        /// </summary>
        /// <param name="id">Айди удаляемой строки</param>
        public void DelContacts(int id)
        {
            string sql = "DELETE FROM Contacts WHERE Id_contact =" + id.ToString();
            this.execute(sql);
        } //Конец DelContacts
        /// <summary>
        /// //Метод для добавления груп
        /// </summary>
        /// <param name="nameG">Имя группы</param>
        /// <param name="coment">Коментраии к группе</param>
        public void InsertGrops(string nameG, string coment)
        {
            string sql = "INSERT INTO group_contacts " +
                         "(name_group, coment)" +
           "VALUES (@name_group, @coment)";
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(sql, connection);
                mycommand.Parameters.Add("@name_group", DbType.String).Value = nameG;
                mycommand.Parameters.Add("@coment", DbType.String).Value = coment;
                ExecuteCommandMy(mycommand);
            } //try
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message + " : " + e.Source + " : " + e.StackTrace);
            }
        } //InsertGrops
       /// <summary>
        ///  //Метод для редактирования груп.
       /// </summary>
       /// <param name="id">Айди строки которую надо редактировать</param>
        /// <param name="nameG">Имя группы</param>
        /// <param name="coment">Коментарии к группе</param>
        public void UpdateGrops(int id, string nameG, string coment)
        {
            string sql = "UPDATE group_contacts " +
"SET name_group = @name_group , coment = @coment " +
"WHERE (id_group = @id_group) ";
            try
            {
                SQLiteCommand mycommand = new SQLiteCommand(sql, connection);
                mycommand.Parameters.Add("@id_group", DbType.String).Value = id;
                mycommand.Parameters.Add("@name_group", DbType.String).Value = nameG;
                mycommand.Parameters.Add("@coment", DbType.String).Value = coment;
                ExecuteCommandMy(mycommand);
            } //try
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message + " : " + e.Source + " : " + e.StackTrace);
            }
        } //UpdateGrops
        /// <summary>
        /// //Метод для удаления груп.
        /// </summary>
        /// <param name="id">Айди строки которую надо удалить</param>
        public void DelGroups(int id)
        {
            string sql = "DELETE FROM group_contacts WHERE id_group =" + id.ToString();
            this.execute(sql);
        } //Конец DelGroups
        /// <summary>
        /// //Метод для загрузки несколькоких  каналов в базу из таблицы представления
        /// </summary>
        /// <param name="dt">Таблица с данными которые нужно добавить</param>
        public static void LoadChannelsToDT(DataTable dt)
        {
            using (CannelsTableAdapter cn = new CannelsTableAdapter())
            {
                cn.Connection = new SQLiteConnection("Data Source =" + Environment.CurrentDirectory + "\\SQLiteBD.bd");
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        if (dt.Columns.Contains("Id_cannels")) dr["Id_cannels"] = null;
                        using (SQLiteConnection connection1 = new SQLiteConnection("Data Source =" + Environment.CurrentDirectory + "\\SQLiteBD.bd"))
                        {
                            string sql = "SELECT * FROM Cannels  WHERE Cannels.Number =" + "'" + dr["Number"].ToString() + "'";
                            using (SQLiteCommand command1 = new SQLiteCommand(sql, connection1))
                            {
                                connection1.Open();
                                using (SQLiteDataReader reader = command1.ExecuteReader())
                                {
                                    if (!reader.HasRows)
                                        cn.Update(dr);
                                }
                            }
                            connection1.Close();
                        }

                    }
                    catch (Exception e) { MessageBox.Show(e.TargetSite + "  " + e.Message + "  " + e.Source + "  LoadChannelsToDT(DataTable dt)"); }
                }

            }
        } // LoadChannelsDT
        /// <summary>
        /// //Метод для загрузки несколькоких  контактов в базу из таблицы представления
        /// </summary>
        /// <param name="dt">Таблица с данными которые нужно добавить</param>
        public static void LoadContactsToDT(DataTable dt)
        {
            using (ContactsTableAdapter cn = new ContactsTableAdapter())
            {
                cn.Connection = new SQLiteConnection("Data Source =" + Environment.CurrentDirectory + "\\SQLiteBD.bd");

                foreach (DataRow dr in dt.Rows)
                {
                    cn.Update(dr);
                }

            }
        } // LoadContactsDT
        /// <summary>
        /// //Метод для загрузки несколькоких  груп в базу из таблицы представления
        /// </summary>
        /// <param name="dt">Таблица с данными которые нужно добавить</param>
        public static void LoadGroupsToDT(DataTable dt)
        {
            using (group_contactsTableAdapter cn = new group_contactsTableAdapter())
            {
                cn.Connection = new SQLiteConnection("Data Source =" + Environment.CurrentDirectory + "\\SQLiteBD.bd");
                foreach (DataRow dr in dt.Rows)
                {
                    cn.Update(dr);
                }

            }
        } //LoadGroupDT
        /// <summary>
        /// Метод для преобразования множества значений, cписка или массива, в таблицу. Например можно массив классов типа Channels Превратить в таблицу.
        /// </summary>
        /// <typeparam name="T">Тип элемента множества которое нужно преобразовать в таблицу</typeparam>
        /// <param name="values">Моножество которое нужно перевести в таблицу</param>
        /// <returns>Таблицу типа DataTable</returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> values)
        {
            DataTable table = new DataTable();
            foreach (T value in values)
            {
                if (table.Columns.Count == 0)
                {
                    foreach (var p in value.GetType().GetProperties())
                    {
                        table.Columns.Add(p.Name);
                    }
                }

                DataRow dr = table.NewRow();
                foreach (var p in value.GetType().GetProperties())
                {
                    dr[p.Name] = p.GetValue(value, null) + "";

                }
                table.Rows.Add(dr);
            }

            return table;
        } // Конец ToDataTable;
        /// <summary>
        /// Реализация интерфейса IDisposable для выгрузки из памяти потоковых данных
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing)
            {
                // Освобождаем только управляемые ресурсы
                connection.Dispose();
            }

            // Освобождаем неуправляемые ресурсы
            _disposed = true;
        }
        #region Не используемый код
        // if (!File.Exists(dataSourseMy)) SQLiteConnection.CreateFile(dataSourseMy); 
        //dt.Rows[0][0]=10;
        /*
        public void insertChannels()
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO 'example' ('id', 'value') VALUES (1, 'Вася');", this.connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void selectChannels()
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'example';",this.connection);
            SQLiteDataReader reader = command.ExecuteReader();
            //foreach (DbDataRecord record in reader)
            //{
            //    string id = record["id"].ToString();                
            //    string value = record["value"].ToString();               
               
            //}
        }
       public void deleteChannels(int id){
           connection.Open();
           SQLiteCommand command = new SQLiteCommand("DELETE FROM msgs WHERE id="+id, this.connection);
           command.ExecuteNonQuery();
           connection.Close();
        }
       public void updateChannels(int id)
       {
           connection.Open();
           SQLiteCommand command = new SQLiteCommand("UPDATE COMPANY SET ADDRESS = 'Texas' WHERE id =" + id, this.connection);
           command.ExecuteNonQuery();
           connection.Close();
           
       }
         */
        /* public static DataTable GetDataTable(string sql)
{
  SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source=Base.db");
  sqliteConnection.Open();
  SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection);
  DataTable dt = new DataTable();
  try
  {
    sqliteCommand.CommandText = sql;
    SQLiteDataReader sqliteReader = sqliteCommand.ExecuteReader();
    dt.Load(sqliteReader);
    sqliteReader.Close();
  }
  catch
  {
    // Закрывать соединение нужно в любом случае
    sqliteConnection.Close();
  }
  sqliteConnection.Close();
  return dt;
}

public static int ExecuteNonQuery(string sql)
{
  SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source=Base.db");
  sqliteConnection.Open();
  SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection);
  sqliteCommand.CommandText = sql;
  int rowsUpdated = sqliteCommand.ExecuteNonQuery();
  sqliteConnection.Close();
  return rowsUpdated;
}

public static string ExecuteScalar(string sql)
{
  SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source=Base.db");
  sqliteConnection.Open();
  SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteConnection);
  sqliteCommand.CommandText = sql;
  object value = sqliteCommand.ExecuteScalar();
  sqliteConnection.Close();
  if (value != null)
  {
    return value.ToString();
  }
  return "";
}*/
        #endregion
    }    // Конец класса DataMy

    /* public class Item
    {
        string _name;
        int _phoneNumber;
        int _id;

        public Item(int id, string name, int phoneNumber)
        {
            _id = id;
            _name = name;
            _phoneNumber = phoneNumber;
        }

        public int ID
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
        }
        DataTable GetTable(string n)
        {
            DataTable dt = new DataTable();
            return dt;
        }
        public List<Item> GetItemList(string name)
        {
            return ConvertToItemList(GetTable(name).Rows);
        }
        //Конец GetItemList
        private List<Item> ConvertToItemList(DataRowCollection rows)
        {
            List<Item> itemList = new List<Item>();

            for (int i = 0; i < rows.Count; i++)
            {
                itemList.Add(new Item(
                    Convert.ToInt32(rows[i]["id"]),
                    rows[i]["name"].ToString(),
                    Convert.ToInt32(rows[i]["number"])));
            }

            return itemList;
        }
        // Конец ConvertToItemList

    }
    //конец классна Item
     * */
}
