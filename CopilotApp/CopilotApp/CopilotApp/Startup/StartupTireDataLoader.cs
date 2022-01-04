using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CopilotApp
{
    class StartupTireDataLoader
    {
        public static async Task LoadTireData()
        {

            /*********************************************************
             * Loads the tire data for the vehicle from the database *
             *********************************************************/

            //Prep SQL Query that grabs all the tire id's for the vehicle
            string SQLQuery = "SELECT tire_left_front_id, tire_right_front_id, tire_left_rear_id, tire_right_rear_id FROM tpms_vehicle WHERE id = '" + MachineData.machineID + "'";

            //Send Query and get the results(tire id's) into the reader
            MySqlDataReader tireIDReader = Database.SendQuery(SQLQuery);

            //Reader used to grab the inividual tires data, needed because tireIDReader is used contiuously and we do not want to overwrite its contents.
            MySqlDataReader reader = null;

            if(tireIDReader != null)
            {

                //Extract the tire IDs
                tireIDReader.Read();

                //Load Front Left Tire Data
                if (tireIDReader["tire_left_front_id"] != DBNull.Value)
                {
                    TireData.frontLeftTireID = tireIDReader["tire_left_front_id"].ToString();

                    SQLQuery = "SELECT baseline_pressure, fill_material, tread_depth, sensor_id, revolutions FROM tpms_tire WHERE id = '" + TireData.frontLeftTireID + "'";
                    reader = Database.SendQuery(SQLQuery);

                    if (reader != null)
                    {
                        reader.Read();
                        if (reader["baseline_pressure"] != DBNull.Value) TireData.frontLeftTireBaselinePressure = double.Parse(reader["baseline_pressure"].ToString());
                        if (reader["fill_material"] != DBNull.Value) TireData.frontLeftTireFillMaterial = reader["fill_material"].ToString();
                        if (reader["tread_depth"] != DBNull.Value) TireData.frontLeftTireTreadDepth = double.Parse(reader["tread_depth"].ToString());
                        if (reader["sensor_id"] != DBNull.Value) SensorData.frontLeftSensorID = reader["sensor_id"].ToString();
                        if (reader["revolutions"] != DBNull.Value) TireData.frontLeftTireRevolutions = double.Parse(reader["revolutions"].ToString());
                    }
                }

                //Load Front Right Tire Data
                if (tireIDReader["tire_right_front_id"] != DBNull.Value)
                {
                    TireData.frontRightTireID = tireIDReader["tire_right_front_id"].ToString();
                    Console.WriteLine("LOADING TIRE DATA FOR ID: " + TireData.frontRightTireID);

                    SQLQuery = "SELECT baseline_pressure, fill_material, tread_depth, sensor_id, revolutions FROM tpms_tire WHERE id = '" + TireData.frontRightTireID + "'";
                    reader = Database.SendQuery(SQLQuery);

                    if (reader != null)
                    {
                        reader.Read();
                        if (reader["baseline_pressure"] != DBNull.Value) TireData.frontRightTireBaselinePressure = double.Parse(reader["baseline_pressure"].ToString());
                        if (reader["fill_material"] != DBNull.Value) TireData.frontRightTireFillMaterial = reader["fill_material"].ToString();
                        if (reader["tread_depth"] != DBNull.Value) TireData.frontRightTireTreadDepth = double.Parse(reader["tread_depth"].ToString());
                        if (reader["sensor_id"] != DBNull.Value) SensorData.frontRightSensorID = reader["sensor_id"].ToString();
                        if (reader["revolutions"] != DBNull.Value) TireData.frontRightTireRevolutions = double.Parse(reader["revolutions"].ToString());
                    }
                }

                //Load Rear Left Tire Data
                if (tireIDReader["tire_left_rear_id"] != DBNull.Value)
                {
                    TireData.rearLeftTireID = tireIDReader["tire_left_rear_id"].ToString();
                    Console.WriteLine("LOADING TIRE DATA FOR ID: " + TireData.rearLeftTireID);

                    SQLQuery = "SELECT baseline_pressure, fill_material, tread_depth, sensor_id, revolutions FROM tpms_tire WHERE id = '" + TireData.rearLeftTireID + "'";
                    reader = Database.SendQuery(SQLQuery);

                    if (reader != null)
                    {
                        reader.Read();
                        if (reader["baseline_pressure"] != DBNull.Value) TireData.rearLeftTireBaselinePressure = double.Parse(reader["baseline_pressure"].ToString());
                        if (reader["fill_material"] != DBNull.Value) TireData.rearLeftTireFillMaterial = reader["fill_material"].ToString();
                        if (reader["tread_depth"] != DBNull.Value) TireData.rearLeftTireTreadDepth = double.Parse(reader["tread_depth"].ToString());
                        if (reader["sensor_id"] != DBNull.Value) SensorData.rearLeftSensorID = reader["sensor_id"].ToString();
                        if (reader["revolutions"] != DBNull.Value) TireData.rearLeftTireRevolutions = double.Parse(reader["revolutions"].ToString());
                    }
                }

                //Load Rear Right Tire Data
                if (tireIDReader["tire_right_rear_id"] != DBNull.Value)
                {
                    TireData.rearRightTireID = tireIDReader["tire_right_rear_id"].ToString();
                    Console.WriteLine("LOADING TIRE DATA FOR ID: " + TireData.rearRightTireID);

                    SQLQuery = "SELECT baseline_pressure, fill_material, tread_depth, sensor_id, revolutions FROM tpms_tire WHERE id = '" + TireData.rearRightTireID + "'";
                    reader = Database.SendQuery(SQLQuery);

                    if (reader != null)
                    {
                        reader.Read();
                        if (reader["baseline_pressure"] != DBNull.Value) TireData.rearRightTireBaselinePressure = double.Parse(reader["baseline_pressure"].ToString());
                        if (reader["fill_material"] != DBNull.Value) TireData.rearRightTireFillMaterial = reader["fill_material"].ToString();
                        if (reader["tread_depth"] != DBNull.Value) TireData.rearRightTireTreadDepth = double.Parse(reader["tread_depth"].ToString());
                        if (reader["sensor_id"] != DBNull.Value) SensorData.rearRightSensorID = reader["sensor_id"].ToString();
                        if (reader["revolutions"] != DBNull.Value) TireData.frontLeftTireRevolutions = double.Parse(reader["revolutions"].ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine("tireIDReader IS NULL");
            }

            await Task.CompletedTask;
        }
    }
}
