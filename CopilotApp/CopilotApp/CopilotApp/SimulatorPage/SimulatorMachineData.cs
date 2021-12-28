using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    public partial class SimulatorPageViewmodel
    {
        public enum MACHINE_TYPE { WHEEL_LOADER, ARTICULATED_HAULER };
        public MACHINE_TYPE machineType;

        string _machineID; public string machineID { get => _machineID; set { _machineID = value; OnPropertyChanged(nameof(machineID)); } }
        string _ambientTemp; public string ambientTemp { get => _ambientTemp; set { _ambientTemp = value; OnPropertyChanged(nameof(ambientTemp)); } }
        string _longitude; public string longitude { get => _longitude; set { _longitude = value; OnPropertyChanged(nameof(longitude)); } }
        string _latitude; public string latitude { get => _latitude; set { _latitude = value; OnPropertyChanged(nameof(latitude)); } }

        void LoadMachineDataFromDatabase()
        {
            LoadTireDataFromDatabase();
        }

        private void LoadTireDataFromDatabase()
        {
            string query = "SELECT tire_left_front_id, tire_right_front_id, tire_left_rear_id, tire_right_rear_id FROM tpms_vehicle WHERE id='" + machineID + "';";

            //foreach tire{ load tire data from database() }
        }

        private void SendMachineDataToDatabase()
        {
            string SQLCommand = "UPDATE tpms_vehicle SET " +
                "ambient_temperature = '" + ambientTemp + "', " +
                "distance_driven_empty = '" + distanceDrivenEmpty + "', " +
                "distance_driven_loaded = '" + distanceDrivenLoaded + "', " +
                "machine_hours_empty = '" + machineHoursEmpty + "', " +
                "machine_hours_loaded = '" + machineHoursLoaded + "', " +
                "machine_hours_loaded = '" + machineHoursLoaded + "', " +
                "payload = '" + payloadTonnes + "', " +
                "buckets = '" + payloadBuckets + "', " +
                "consumed_fuel = '" + consumedFuel + "' " +
                "WHERE id = '" + machineID + "';";

            int ColumnsAffected = Database.SendNonQuery(SQLCommand);
        }

        private void SendLocationDataToDatabase()
        {
            string SQLCommand = "INSERT INTO tpms_location (" + "id, " + "latitude, " + "longitude, " + "creation_datetime) " +
            "VALUES(" + machineID + ", '" + latitude + "', '" + longitude + "', '" + GetDateTime() + "' );";

            int ColumnsAffected = Database.SendNonQuery(SQLCommand);
        }

    }
}
