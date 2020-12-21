using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace GornolignuiKypopt
{
    public class DBConnection
    {
        public static int userID, selectedRow, contractID;
        //Подключение к БД 
        public static SqlConnection connection = new SqlConnection(
           "Data Source=KIRILL\\SQLEXPRESS; Initial Catalog=GornolignuiKypopt;" +
           "Integrated Security=True; Connect Timeout=30; Encrypt=False;" +
           "TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False");

        public static string qrTovari = "select [ID_Tovara], [Nazvanie] as 'Название товара', [Kolichestvo] as 'Количество', [Cena] as 'Цена', [Tovari].[ID_Kategorii], [Hazvanie_kategorii] as 'Категория' from[Tovari] " +
            "inner join[Kategorii] on[Tovari].[ID_Kategorii] = [Kategorii].[ID_Kategorii]",
            qrKategorii = "select [ID_Kategorii], [Hazvanie_kategorii] as 'Тип товара' from [Kategorii]",
            qrSotrydniki = "select [ID_Sotrydnika], [Familiya] as 'Фамилия', [Name] as 'Имя', [Otchestvo] as 'Отчество', [Login] as 'Логин', [Nazvanie_roli] as 'Должность' " +
            "from [Sotrydniki] inner join [Role] on [Role].[ID_Role] = [Sotrydniki].[ID_Role]",
            qrRole = "select [ID_Role], [Nazvanie_roli] from [Role]",
            qrYvolnenie = "select [ID_Yvolneniya], [Sotrydniki].[ID_Sotrydnika], [Familiya] as 'Фамилия', [Name] as 'Имя', [Otchestvo] as 'Отчество',  " +
            "[Data_Yvolneniya] as 'Дата увольнения', [Prichina_yvolneniya] as 'Причина увольнения' from [Yvolnenie] " +
            "inner join[Sotrydniki] on [Sotrydniki].[ID_Sotrydnika] = [Yvolnenie].[ID_Sotrydnika] ",
            qrSotrydnikiData = "select [ID_Sotrydnika], Concat([Familiya], + ' ' +  [Name], + ' ' + [Otchestvo]) as 'ФИО' from [Sotrydniki]",
            qrArendaList = "select [Nazvanie] as 'Название', [Cena] as 'Цена' from [Arenda] inner join[Tovari] on[ID_Tovara] = [ID_Tovara]",
            qrArenda = "select [ID_Arenda], [Arenda].[ID_Tovara], [Nazvanie] as 'Название товара', [Kolichestvo_tovarov] as 'Количество',  " +
            "[Arenda].[ID_Klienta], CONCAT([Familiya], + ' ' + [Name], + ' ' + [Otchestvo]) as 'Клиент', " +
            "[Cymma] as 'Цена' from[Arenda]  i" +
            "nner join[Tovari] on [Arenda].[ID_Tovara] = [Tovari].[ID_Tovara]  inner join[Klienti] on[Arenda].[ID_Klienta] = [Klienti].[ID_Klienta]",
            qrKlienti = "select [ID_Klienta], Concat([Familiya], + ' ' +  [Name], + ' ' + [Otchestvo]) as 'ФИО' from [Klienti]";
        private SqlCommand command = new SqlCommand("", connection);

        //Авторизация
        public Int32 Authorization(string login, string password)
        {
            try
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select [ID] from [dbo].[Users] " +
               "where [Логин] = '" + login + "' and [Пароль] = '" + password + "'";
                DBConnection.connection.Open();
                userID = Convert.ToInt32(command.ExecuteScalar().ToString());
                connection.Close();
                return (userID);
            }
            catch
            {
                connection.Close();
                userID = 0;
                return (userID);

            }
        }
        //Проверка уникальности логина
        public int UnqLogin(string login)
        {
            int log;
            try
            {
                command.CommandText = "select count(*) from [Employee] where [Login] like '%" + login + "%'";
                connection.Open();
                log = Convert.ToInt32(command.ExecuteScalar().ToString());
                connection.Close();
                return log;
            }
            catch
            {
                connection.Close();
                log = 0;
                return log;
            }
        }
        //Роль пользователя
        public string Role(Int32 User)
        {
            string RoleID;
            int ID_User;
            try
            {
                try
                {
                    command.CommandText = "select [ID_Users] from [Users] where ID like '%" + User + "%'";
                    connection.Open();
                    ID_User = Convert.ToInt32(command.ExecuteScalar().ToString());
                    connection.Close();
                }
                catch
                {
                    ID_User = 0;
                }
                command.CommandText = "select [Роль] from [Users] where [ID_User] like '%" + ID_User + "%'";
                connection.Open();
                RoleID = command.ExecuteScalar().ToString();
                connection.Close();
                return RoleID;
            }
            catch
            {
                connection.Close();
                RoleID = "1";
                return RoleID;
            }
        }
    }
}