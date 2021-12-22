using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Essentials;

/**********************************************************
 * Prefab functionality for database queries and commands *
 **********************************************************/


namespace CopilotApp
{
    public class DatabaseFunctions
    {
        public DatabaseFunctions()
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

            int nrOfRowsAffected = Database.SendNonQuery(sqlStatement).Result;
        }

        public static void SendSensorData(string tireID, string pressure, string temperature)
        {
            string sqlStatement = "INSERT INTO sensors(ID, pressure, temperature) VALUES(" + tireID + "," + pressure + "," + temperature + "," + ")";
            int nrOfRowsAffected = Database.SendNonQuery(sqlStatement).Result;
        }

        public static void SendMachineData(int machineID, Location location)
        {
            string sqlStatement = "INSERT INTO machine(ID, latitude, longitude) VALUES(" + machineID + "," + location.Latitude + "," + location.Longitude + ")";
            int nrOfRowsAffected = Database.SendNonQuery(sqlStatement).Result;
        }

        public static void SendLocationData()
        {

        }
    }
}