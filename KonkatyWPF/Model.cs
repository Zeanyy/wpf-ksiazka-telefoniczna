using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using static KonkatyWPF.Person;
using System.Windows.Controls;

namespace KonkatyWPF
{
    public class Model
    {
        static SQLiteConnection connect = new SQLiteConnection("Data Source=osoby.db");

        public static int limit = 3;
        public static string tableName = "baza";

        public static void Open()
        {
            connect.Open();
        }
        public static List<Person> GetPersons(int page, string searchText)
        {
            List<Person> persons = new List<Person>();

            SQLiteCommand command = connect.CreateCommand();

            string where = "";

            if(searchText.Length > 0)
            {
                where = $"WHERE first_name LIKE '%{searchText}%' OR last_name LIKE '%{searchText}%' OR address LIKE '%{searchText}%' OR phone LIKE '%{searchText}%' OR state LIKE '%{searchText}%' OR plec LIKE '%{searchText}%'";
            }

            command.CommandText = $"SELECT * FROM {tableName} {where} LIMIT {limit * page}, {limit}";

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string surname = reader.GetString(2);
                string gender = reader.GetString(6);
                string email = reader.GetString(3);
                string state = reader.GetString(5);
                int number = reader.GetInt32(4);

                persons.Add(new Person(id, name, surname, gender, email, state, number));
            }
            reader.Close();
            return persons;
        }
        public static int countPersons(string searchText)
        {
            SQLiteCommand command = connect.CreateCommand();

            string where = "";

            if (searchText.Length > 0)
            {
                where = $"WHERE first_name LIKE '%{searchText}%' OR last_name LIKE '%{searchText}%' OR address LIKE '%{searchText}%' OR phone LIKE '%{searchText}%' OR state LIKE '%{searchText}%' OR plec LIKE '%{searchText}%'";
            }

            command.CommandText = $"SELECT COUNT(*) FROM {tableName} {where}";

            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            var count = reader.GetInt32(0);
            reader.Close();
            return count;
        }
        public static void addPerson(Person person)
        {
            SQLiteCommand command = connect.CreateCommand();
            command.CommandText = $"INSERT INTO {tableName} (first_name, last_name, address, phone, state, plec) VALUES ('{person.name}', '{person.surname}', '{person.email}', '{person.number}', '{person.state}', '{person.gender}')";
            command.ExecuteReader();
        }
        public static void deletePerson(int id)
        {
            SQLiteCommand command = connect.CreateCommand();
            command.CommandText = $"DELETE FROM {tableName} WHERE user_id = {id}";
            command.ExecuteReader();
        }
        public static void editPerson(Person person)
        {
            SQLiteCommand command = connect.CreateCommand();
            command.CommandText = $"UPDATE {tableName} SET first_name='{person.name}',last_name='{person.surname}',address='{person.email}',phone='{person.number}',state='{person.state}',plec='{person.gender}' WHERE user_id={person.id}";
            command.ExecuteReader();
        }
        public static void addTable(string name)
        {
            tableName = name;
            SQLiteCommand command = connect.CreateCommand();
            command.CommandText = $"CREATE TABLE IF NOT EXISTS {name} (user_id INTEGER PRIMARY KEY UNIQUE, first_name STRING, last_name STRING, address STRING, phone STRING, state STRING, plec VARCHAR (1))";
            command.ExecuteReader();
        }
    }
}
