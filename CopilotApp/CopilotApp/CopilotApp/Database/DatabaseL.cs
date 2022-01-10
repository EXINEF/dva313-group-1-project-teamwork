using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

/*******************************
 * Local instantiable database *
 *******************************/

namespace CopilotApp
{
    public partial class DatabaseL
    {
        //Heroku ClearDB Credentials
        string ip;
        string port;
        string username;
        string password;
        string databaseName;

        static MySqlConnection mySQLConnection;

        public DatabaseL()
        {
            ip = DatabaseCredentials.IP;
            port = DatabaseCredentials.PORT;
            username = DatabaseCredentials.USERNAME;
            password = DatabaseCredentials.PASSWORD;
            databaseName = DatabaseCredentials.DATABASE_NAME;
        }

        //Takes neccessary credentials and return a string in the correct format needed to initialize a SQL connection.
        private string GenerateConnectionString(string ip, string port, string username, string password, string database)
        {
            string connection_string = "datasource=" + ip + ";" + "port=" + port + ";" + "username=" + username + ";" + "password=" + password + ";" + "database=" + database;
            return connection_string;
        }

        //SQL DATETIME requires a particular format, this get the current datetime of the device in the SQL appropriate format
        public string GetSQLDateTime()
        {
            DateTime myDateTime = DateTime.Now;
            return myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        //Used for SQL statements that only inserts or updates values and only returns an int showing the number of rows affected
        public int SendNonQuery(string sqlStatement)
        {
            Console.WriteLine("Sending SQL statement: " + sqlStatement);

            string myConnectionString = GenerateConnectionString(ip, port, username, password, databaseName);

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
        public MySqlDataReader SendQuery(string query)
        {
            Console.WriteLine("Sending Query: " + query);

            string myConnectionString = GenerateConnectionString(ip, port, username, password, databaseName);

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
    }
}
