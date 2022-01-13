
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
    public class DatabaseFunctions
    {
        public DatabaseFunctions()
        {

        }

        //Dynamically extracts the filled in values and produces a SQL command that send all data fields that are not null or empty
        public static int SendTireData(string tireID, string remaining_life, string tireBaselinePressure, string tireFillMaterial, string tireTreadDepth,
                                       string revolutions, string companyID, string sensorID, string is_used)
        {
            //dict["database_variable_name"] = value; // Only place filled in values in the dictionary
            Dictionary<string, string> dict = new Dictionary<string, string>();

            if (tireID != null && tireID != "") { dict["id"] = tireID; } else { return -1; }
            if (remaining_life != null && remaining_life != "") { dict["remaining_life"] = remaining_life; }
            if (tireBaselinePressure != null && tireBaselinePressure != "") { dict["baseline_pressure"] = tireBaselinePressure; }
            if (tireFillMaterial != null && tireFillMaterial != "") { dict["fill_material"] = tireFillMaterial; }
            if (tireTreadDepth != null && tireTreadDepth != "") { dict["tread_depth"] = tireTreadDepth; }
            if (revolutions != null && revolutions != "") { dict["revolutions"] = revolutions; }
            if (companyID != null && companyID != "") { dict["company_id"] = companyID; }
            if (sensorID != null && sensorID != "") { dict["sensor_id"] = sensorID; }
            if (is_used != null && is_used != "") { dict["is_used"] = is_used; }

            string insertParamList = "";
            string insertValueList = "";
            string updateList = "";

            int nrOfKeys = dict.Count;
            int i = 0;
            foreach (var item in dict)
            {
                insertParamList += item.Key;
                insertValueList += ("'" + item.Value + "'");
                updateList += (item.Key + " = VALUES(" + item.Key + ")");

                i += 1;
                if (i < nrOfKeys)
                {
                    insertParamList += ", ";
                    insertValueList += ", ";
                    updateList += ", ";
                }
            }

            string insertStatement = "INSERT INTO tpms_tire(" + insertParamList + ")" + " VALUES(" + insertValueList + ")";
            string updateStatement = " ON DUPLICATE KEY UPDATE " + updateList;

            string sqlStatement = insertStatement + updateStatement;

            int nrOfRowsAffected = Database.SendNonQuery(sqlStatement);

            return nrOfRowsAffected;
        }

        //Dynamically extracts the filled in values and produces a SQL command that send all data fields that are not null or empty
        public static void SendSensorData(string sensorID, string pressure, string temperature, string status, string remainingBattery, string companyID, string is_used)
        {
            //dict["database_variable_name"] = value; // Only place filled in values in the dictionary
            Dictionary<string, string> dict = new Dictionary<string, string>();

            if (sensorID != null && sensorID != "") { dict["id"] = sensorID; } else { return; }
            if (pressure != null && pressure != "") { dict["pressure"] = pressure; }
            if (temperature != null && temperature != "") { dict["temperature"] = temperature; }
            if (status != null && status != "") { dict["status"] = status; }
            if (remainingBattery != null && remainingBattery != "") { dict["remaning_battery"] = remainingBattery; }
            if (companyID != null && companyID != "") { dict["company_id"] = companyID; }
            if (is_used != null && is_used != "") { dict["is_used"] = is_used; }

            string insertParamList = "";
            string insertValueList = "";
            string updateList = "";

            int nrOfKeys = dict.Count;
            int i = 0;
            foreach (var item in dict)
            {
                insertParamList += item.Key;
                insertValueList += ("'" + item.Value + "'");
                updateList += (item.Key + " = VALUES(" + item.Key + ")");

                i += 1;
                if (i < nrOfKeys)
                {
                    insertParamList += ", ";
                    insertValueList += ", ";
                    updateList += ", ";
                }
            }

            string insertStatement = "INSERT INTO tpms_sensor(" + insertParamList + ")" + " VALUES(" + insertValueList + ")";
            string updateStatement = " ON DUPLICATE KEY UPDATE " + updateList;

            string sqlStatement = insertStatement + updateStatement;

            int nrOfRowsAffected = Database.SendNonQuery(sqlStatement);

        }

        //Dynamically extracts the filled in values and produces a SQL command that send all data fields that are not null or empty
        public static int SendMachineData(string machineID, string model, string ambientTemp, string distanceDrivenEmpty, string distanceDrivenLoaded,
        string machineHoursEmpty, string machineHoursLoaded, string payloadTonnes, string payloadBuckets, string consumedFuel,
        string tire_left_front_id, string tire_left_rear_id, string tire_right_front_id, string tire_right_rear_id, string tire_specc, string companyID)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            //dict["database_variable_name"] = value; // Only place filled in values in the dictionary
            if (machineID != null && machineID != "") { dict["id"] = machineID; } else { return -1; }
            if (model != null && model != "") { dict["model"] = model; } else { dict["model"] = "Wheel loader"; }
            if (ambientTemp != null && ambientTemp != "") { dict["ambient_temperature"] = ambientTemp; }
            if (distanceDrivenEmpty != null && distanceDrivenEmpty != "") { dict["distance_driven_empty"] = distanceDrivenEmpty; }
            if (distanceDrivenLoaded != null && distanceDrivenLoaded != "") { dict["distance_driven_loaded"] = distanceDrivenLoaded; }
            if (machineHoursEmpty != null && machineHoursEmpty != "") { dict["machine_hours_empty"] = machineHoursEmpty; }
            if (machineHoursLoaded != null && machineHoursLoaded != "") { dict["machine_hours_loaded"] = machineHoursLoaded; }
            if (payloadTonnes != null && payloadTonnes != "") { dict["payload"] = payloadTonnes; }
            if (payloadBuckets != null && payloadBuckets != "") { dict["buckets"] = payloadBuckets; }
            if (consumedFuel != null && consumedFuel != "") { dict["consumed_fuel"] = consumedFuel; }
            if (tire_left_front_id != null && tire_left_front_id != "") { dict["tire_left_front_id"] = tire_left_front_id; }
            if (tire_left_rear_id != null && tire_left_rear_id != "") { dict["tire_left_rear_id"] = tire_left_rear_id; }
            if (tire_right_front_id != null && tire_right_front_id != "") { dict["tire_right_front_id"] = tire_right_front_id; }
            if (tire_right_rear_id != null && tire_right_rear_id != "") { dict["tire_right_rear_id"] = tire_right_rear_id; }
            if (tire_specc != null && tire_specc != "") { dict["tire_specc"] = tire_specc; }
            if (companyID != null && companyID != "") { dict["company_id"] = companyID; }

            string insertParamList = "";
            string insertValueList = "";
            string updateList = "";

            int nrOfKeys = dict.Count;
            int i = 0;
            foreach(var item in dict)
            {
                insertParamList += item.Key;
                insertValueList += ("'" + item.Value + "'");
                updateList += (item.Key + " = VALUES(" + item.Key + ")");

                i += 1;
                if(i < nrOfKeys)
                {
                    insertParamList += ", ";
                    insertValueList += ", ";
                    updateList += ", ";
                }
            }

            string insertStatement = "INSERT INTO tpms_vehicle(" + insertParamList + ")" + " VALUES(" + insertValueList + ")";
            string updateStatement = " ON DUPLICATE KEY UPDATE " + updateList;

            string sqlStatement = insertStatement + updateStatement;
            int nrOfRowsAffected = Database.SendNonQuery(sqlStatement);

            return nrOfRowsAffected;
        }

        public static void SendLocationData(string machineID, string latitude, string longitude)
        {
            if (machineID != null && machineID != "" && latitude != null && latitude != "" && longitude != null && longitude != "")
            {

                string sqlQuery = "INSERT INTO tpms_location(id, latitude, longitude, creation_datetime)" +
                                  "VALUES(DEFAULT, " + latitude + ", " + longitude + ", CURRENT_TIME); " +
                                  "SELECT LAST_INSERT_ID();";

                MySqlDataReader reader = Database.SendQuery(sqlQuery);

                if (reader != null)
                {
                    reader.Read();

                    int locationID = int.Parse(reader["LAST_INSERT_ID()"].ToString());

                    string sqlCommand = "INSERT into tpms_vehicle_locations(id, vehicle_id, location_id)" +
                                        "VALUES(DEFAULT, '" + machineID + "'," + locationID + ")";

                    int nrOfAffectedRows = Database.SendNonQuery(sqlCommand);
                }
            }
        }

    }
}