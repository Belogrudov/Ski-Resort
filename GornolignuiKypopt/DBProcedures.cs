using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace GornolignuiKypopt
{
    public class DBProcedures
    {
        private SqlCommand command = new SqlCommand("", DBConnection.connection);
        private void commandConfig(string config)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[dbo].[" + config + "]";
            command.Parameters.Clear();
        }

        //Регистрация пользователя
        public void RegistrationUsers(string Familiya, string Name, string Otchestvo, string Login,
            string Password, int ID_Role)
        {
            //Данные
            commandConfig("Klienti_Insert");
            command.Parameters.AddWithValue("@Familiya", Familiya);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Otchestvo", Otchestvo);
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@ID_Role", ID_Role);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление товаров
        public void Tovari_Insert(string Nazvanie, int Kolichestvo, decimal Cena, int ID_Kategorii)
        {
            commandConfig("Tovari_Insert");
            command.Parameters.AddWithValue("@Nazvanie", Nazvanie);
            command.Parameters.AddWithValue("@Kolichestvo", Kolichestvo);
            command.Parameters.AddWithValue("@Cena", Cena);
            command.Parameters.AddWithValue("@ID_Kategorii", ID_Kategorii);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление товара
        public void Tovari_Update(int ID_Tovara, string Product_Name, int Kolichestvo, decimal Cena, int ID_Kategorii)
        {
            commandConfig("Tovari_Update");
            command.Parameters.AddWithValue("@ID_Tovara", ID_Tovara);
            command.Parameters.AddWithValue("@Product_Name", Product_Name);
            command.Parameters.AddWithValue("@Kolichestvo", Kolichestvo);
            command.Parameters.AddWithValue("@Cena", Cena);
            command.Parameters.AddWithValue("@ID_Kategorii", ID_Kategorii);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление товара
        public void Tovari_Delete(int ID_Tovara)
        {
            commandConfig("Tovari_Delete");
            command.Parameters.AddWithValue("@ID_Tovara", ID_Tovara);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление сотрудника
        public void Sotrydniki_Insert(string Familiya, string Name, string Otchestvo, string Login,
            string Password, int ID_Role)
        {       
            //Создание записи о сотрудники
            //Данные
            commandConfig("Sotrydniki_Insert");
            command.Parameters.AddWithValue("@Familiya", Familiya);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Otchestvo", Otchestvo);
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@ID_Role", ID_Role);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление сотрудника
        public void Sotrydniki_Delete(int ID_Sotrydnika)
        {
            commandConfig("Sotrydniki_Delete");
            command.Parameters.AddWithValue("@ID_Sotrydnika", ID_Sotrydnika);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление данных о сотруднике
        public void Sotrydniki_Update(int ID_Sotrydnika, string Familiya, string Name, string Otchestvo, string Login, int ID_Yvolneniya,
            string Password, bool v, int ID_Role)
        {
            //Данные
            command.Parameters.AddWithValue("@ID_Sotrydnika", ID_Sotrydnika);
            command.Parameters.AddWithValue("@Familiya", Familiya);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Otchestvo", Otchestvo);
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@ID_Role", ID_Role);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Создание приказа об увольнении
        public void Yvolnenie_Insert(string Prichina_yvolneniya, int ID_Sotrydnika, string Data_Yvolneniya)
        {
            commandConfig("Yvolnenie_Insert");
            command.Parameters.AddWithValue("@Prichina_yvolneniya", Prichina_yvolneniya);
            command.Parameters.AddWithValue("@ID_Sotrydnika", ID_Sotrydnika);
            command.Parameters.AddWithValue("@Data_Yvolneniya", Data_Yvolneniya);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        //Удаление приказа об увольнении
        public void Yvolnenie_Delete(int ID_Yvolneniya)
        {
            commandConfig("Yvolnenie_Delete");
            command.Parameters.AddWithValue("@ID_Yvolneniya", ID_Yvolneniya);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление приказа об увольнении
        public void Yvolnenie_Update(int ID_Yvolneniya, string Prichina_yvolneniya, int ID_Sotrydnika, string Data_Yvolneniya)
        {
            commandConfig("Fired_Order_Update");
            command.Parameters.AddWithValue("@ID_Yvolneniya", ID_Yvolneniya);
            command.Parameters.AddWithValue("@Prichina_yvolneniya", Prichina_yvolneniya);
            command.Parameters.AddWithValue("@Data_Yvolneniya", Data_Yvolneniya);
            command.Parameters.AddWithValue("@ID_Sotrydnika", ID_Sotrydnika);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Создание заказа
        public void Arenda_Insert(int ID_Tovara, int Kolichestvo, int ID_Klienta, decimal Cymma)
        {
            commandConfig("Arenda_Insert");
            command.Parameters.AddWithValue("@ID_Tovara", ID_Tovara);
            command.Parameters.AddWithValue("@Kolichestvo", Kolichestvo);
            command.Parameters.AddWithValue("@ID_Klienta", ID_Klienta);
            command.Parameters.AddWithValue("@Cymma", Cymma);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление заказа
        public void Arenda_Delete(int ID_Arenda)
        {
            commandConfig("Arenda_Delete");
            command.Parameters.AddWithValue("@ID_Arenda", ID_Arenda);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление заказа
        public void Arenda_Update(int ID_Arenda, int ID_Tovara, int Kolichestvo, int ID_Klienta)
        {
            commandConfig("Arenda_Update");
            command.Parameters.AddWithValue("@ID_Arenda", ID_Arenda);
            command.Parameters.AddWithValue("@ID_Tovara", ID_Tovara);
            command.Parameters.AddWithValue("@Kolichestvo", Kolichestvo);
            command.Parameters.AddWithValue("@ID_Klienta", ID_Klienta);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Добавление типа
        public void Kategorii_Insert(string Hazvanie_kategorii)
        {
            commandConfig("Kategorii_Insert");
            command.Parameters.AddWithValue("@Hazvanie_kategorii", Hazvanie_kategorii);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Обновление типа
        public void Kategorii_Update(int ID_Kategorii, string Hazvanie_kategorii)
        {
            commandConfig("Kategorii_Update");
            command.Parameters.AddWithValue("@ID_Kategorii", ID_Kategorii);
            command.Parameters.AddWithValue("@Hazvanie_kategorii", Hazvanie_kategorii);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        //Удаление типа
        public void Kategorii_Delete(int ID_Kategorii)
        {
            commandConfig("Kategorii_Delete");
            command.Parameters.AddWithValue("@ID_Kategorii", ID_Kategorii);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
    }
}