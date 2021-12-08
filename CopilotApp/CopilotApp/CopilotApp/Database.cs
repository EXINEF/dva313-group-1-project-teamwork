using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;

namespace CopilotApp
{
    public class Database
    {
        //db4free
        //static string _ip = "db4free.net";
        //static string _port = "3306";
        //static string _username = "dva313user";
        //static string _password = "zlLJiR6JCd";
        //static string _databaseName = "dva313";

        //Heroku
        static string _ip = "eu-cdbr-west-01.cleardb.com";
        static string _port = "3306";
        static string _username = "b630dce8abed0f";
        static string _password = "37cbbdac";
        static string _databaseName = "heroku_71aa00777cf80be";

        //local
        //static string _ip = "127.0.0.1";
        //static string _port = "3306";
        //static string _username = "root";
        //static string _password = "";
        //static string _databaseName = "dva313";


        //This database, "db4free.net" is extremely slow but works for now
        //You can access it(pHpMyAdmin) here: https://www.db4free.net/phpMyAdmin
        //username: dva313user
        //password: zlLJiR6JCd


        //static string myConnectionString = "datasource=db4free.net;port=3306;username=dva313user;password=zlLJiR6JCd;database=dva313";
        //static string myConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dva313";
        static MySqlConnection mySQLConnection;

        public Database()
        {

        }

        public static void SendTireData(string tireID, string tireBaselinePressure, string tireFillMaterial, string tireTreadDepth)
        {
            //Insert
            string sqlStatementPart1 = "INSERT INTO tpms_tire(id, baseline_pressure, fill_material, tread_depth) VALUES('" + tireID + "'," + tireBaselinePressure + ",'" + tireFillMaterial + "'," + tireTreadDepth + ")";
            
            //If ID already exists update the values: for that ID
            string sqlStatementPart2 = "ON DUPLICATE KEY UPDATE baseline_pressure = VALUES(baseline_pressure), fill_material = VALUES(fill_material), tread_depth = VALUES(tread_depth)";

            //Combine into a single statement
            string sqlStatement = sqlStatementPart1 + " " + sqlStatementPart2;
            
            int nrOfRowsAffected = SendNonQuery(sqlStatement);
        }

        public static void SendSensorData(string tireID, string pressure, string temperature)
        {
            string sqlStatement = "INSERT INTO sensors(ID, pressure, temperature) VALUES(" + tireID + "," + pressure + "," + temperature + "," + ")";
            int nrOfRowsAffected = SendNonQuery(sqlStatement);
        }

        public static void SendMachineData(int machineID, Location location)
        {
            string sqlStatement = "INSERT INTO machine(ID, latitude, longitude) VALUES(" + machineID + "," + location.latitude + "," + location.longitude + ")";
            int nrOfRowsAffected = SendNonQuery(sqlStatement);
        }

        //Takes neccessary credentials and return a string in the correct format needed to initialize a SQL connection.
        protected static string GenerateConnectionString(string ip, string port, string username, string password, string database)
        {
            string connection_string = "datasource=" + ip + ";" + "port=" + port + ";" + "username=" + username + ";" + "password=" + password + ";" + "database=" + database;
            return connection_string;
        }

        //SQL DATETIME requires a particular format
        private string GetSQLDateTime()
        {
            DateTime myDateTime = DateTime.Now;
            return myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        //Used for SQL statements that only inserts or updates values and only returns an int showing the number of rows affected
        public static int SendNonQuery(string sqlStatement)
        {
            Console.WriteLine("Sending SQL statement: " + sqlStatement);

            string myConnectionString = GenerateConnectionString(_ip, _port, _username, _password, _databaseName);

            //Establish a new connection for every query
            mySQLConnection = new MySqlConnection(myConnectionString);

            //Set up a SQL statement to be executed takes the statement and the connection to which the the statement should be sent 
            MySqlCommand sqlCmd = new MySqlCommand(sqlStatement, mySQLConnection);

            //Timeout in seconds incase the database does not respond.
            sqlCmd.CommandTimeout = 10;

            int nrOfRowsAffected = -1;
            try
            {
                mySQLConnection.Open();

                //Executue SQL command(Send statement to database)
                nrOfRowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL statement send failed: " + ex.Message);
            }

            return nrOfRowsAffected;
        }

        //Used for SQL queries that grabs data from the database.
        public static MySqlDataReader SendQuery(string query)
        {
            Console.WriteLine("Sending Query: " + query);

            string myConnectionString = GenerateConnectionString(_ip, _port, _username, _password, _databaseName);

            //Establish a new connection for every query
            mySQLConnection = new MySqlConnection(myConnectionString);

            //Set up a SQL Command to be executed takes the query and the connection to which the the query should be sent 
            MySqlCommand sqlCmd = new MySqlCommand(query, mySQLConnection);

            //Timeout in seconds incase the database does not respond.
            sqlCmd.CommandTimeout = 10;

            //Data reader to store the results of the query
            MySqlDataReader sqlDataReader = null;
            try
            {
                mySQLConnection.Open();

                //Executue SQL command(Send query to database). Returns a MySqlDataReader object containing the query data.
                sqlDataReader = sqlCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query failed: " + ex.Message);
            }

            return sqlDataReader;
        }

    }
}