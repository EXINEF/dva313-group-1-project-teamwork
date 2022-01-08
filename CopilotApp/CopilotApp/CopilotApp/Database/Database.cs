using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Essentials;

/****************************************
 * Static database functionality        *
 * that can be called from anywhere     *
 ****************************************/

namespace CopilotApp
{
    public class Database
    {
        //Heroku ClearDB Credentials
        static string _ip = "mysql-64846-0.cloudclusters.net";
        static string _port = "12457";
        static string _username = "bob";
        static string _password = "MVa5fPoA";
        static string _databaseName = "tpmsDB";

        static MySqlConnection mySQLConnection;

        //Takes neccessary credentials and return a string in the correct format needed to initialize a SQL connection.
        protected static string GenerateConnectionString(string ip, string port, string username, string password, string database)
        {
            string connection_string = "datasource=" + ip + ";" + "port=" + port + ";" + "username=" + username + ";" + "password=" + password + ";" + "database=" + database;
            return connection_string;
        }

        //SQL DATETIME requires a particular format, this get the current datetime of the device in the SQL appropriate format
        public static string GetSQLDateTime()
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
            sqlCmd.CommandTimeout = 5;

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

        //Used for SQL queries that grabs and returns data from the database.
        public static MySqlDataReader SendQuery(string query)
        {
            Console.WriteLine("Sending Query: " + query);

            string myConnectionString = GenerateConnectionString(_ip, _port, _username, _password, _databaseName);

            //Establish a new connection for every query
            mySQLConnection = new MySqlConnection(myConnectionString);

            //Set up a SQL Command to be executed takes the query and the connection to which the the query should be sent 
            MySqlCommand sqlCmd = new MySqlCommand(query, mySQLConnection);

            //Timeout in seconds incase the database does not respond.
            sqlCmd.CommandTimeout = 5;

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


        /**************************************
        *                ASYNC                *
        ***************************************/
        /*

        //Used for SQL statements that only inserts or updates values and only returns an int showing the number of rows affected
        public static async Task<int> SendNonQueryAsync(string sqlStatement)
        {
            Console.WriteLine("Sending SQL statement: " + sqlStatement);

            string myConnectionString = GenerateConnectionString(_ip, _port, _username, _password, _databaseName);

            //Establish a new connection for every query
            mySQLConnection = new MySqlConnection(myConnectionString);

            //Set up a SQL statement to be executed takes the statement and the connection to which the the statement should be sent 
            MySqlCommand sqlCmd = new MySqlCommand(sqlStatement, mySQLConnection);

            //Timeout in seconds incase the database does not respond.
            sqlCmd.CommandTimeout = 5;

            int nrOfRowsAffected = -1;
            try
            {
                await mySQLConnection.OpenAsync();

                //Executue SQL command(Send statement to database)
                nrOfRowsAffected = await sqlCmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL statement send failed: " + ex.Message);
            }

            return nrOfRowsAffected;
        }

        //Used for SQL queries that grabs and returns data from the database.
        public static async Task<MySqlDataReader> SendQueryAsync(string query)
        {
            Console.WriteLine("Sending Query: " + query);

            string myConnectionString = GenerateConnectionString(_ip, _port, _username, _password, _databaseName);

            //Establish a new connection for every query
            mySQLConnection = new MySqlConnection(myConnectionString);

            //Set up a SQL Command to be executed takes the query and the connection to which the the query should be sent 
            MySqlCommand sqlCmd = new MySqlCommand(query, mySQLConnection);

            //Timeout in seconds incase the database does not respond.
            sqlCmd.CommandTimeout = 5;

            //Data reader to store the results of the query
            MySqlDataReader sqlDataReader = null;
            try
            {
                await mySQLConnection.OpenAsync();

                //Executue SQL command(Send query to database). Returns a MySqlDataReader object containing the query data.
                sqlDataReader = await sqlCmd.ExecuteReaderAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query failed: " + ex.Message);
            }

            return sqlDataReader;
        }
        */
    }
}