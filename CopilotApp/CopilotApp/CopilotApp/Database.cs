using System;
using System.Collections.Generic;
using System.Text;
//using MySql.Data.MySqlClient;
using MySqlConnector;

namespace CopilotApp
{
    public class Database
    {
        static string _ip = "127.0.0.1";
        static string _port = "3306";
        static string _username = "root";
        static string _password = "";
        static string _database = "dva313";

        static string myConnectionString = "datasource=db4free.net;port=3306;username=dva313user;password=zlLJiR6JCd;database=dva313";
        static MySqlConnection mysql_connection;

        public Database()
        {

        }

        public static void SendData(string tireID, string tirePressure, string tireTemperature)
        {
            // 'GetSQLDateTime()'

            string query = "INSERT INTO tire(ID, Pressure, Temperature) VALUES(" + tireID + "," + tirePressure + "," + tireTemperature + ")";
            SendQuery(query);
        }

        private string GenerateConnectionString(string ip, string port, string username, string password, string database)
        {
            string connection_string = "datasource=" + ip + ";" + "port=" + port + ";" + "username=" + username + ";" + "password=" + password + ";" + "database=" + database;
            return connection_string;
        }

        private string GetSQLDateTime()
        {
            DateTime myDateTime = DateTime.Now;
            return myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public static void SendQuery(string query)
        {
            Console.WriteLine("Sending Query: " + query);
            mysql_connection = new MySqlConnection(myConnectionString);
            MySqlCommand sql_cmd = new MySqlCommand(query, mysql_connection);
            sql_cmd.CommandTimeout = 60;

            try
            {
                mysql_connection.Open();
                MySqlDataReader sql_data_reader = sql_cmd.ExecuteReader();

                if (sql_data_reader.HasRows)
                {
                    while (sql_data_reader.Read())
                    {
                        Console.WriteLine(sql_data_reader.GetString(0), sql_data_reader.GetString(1), sql_data_reader.GetString(2), sql_data_reader.GetString(3));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query failed: " + ex.Message);
            }

        }

    }
}