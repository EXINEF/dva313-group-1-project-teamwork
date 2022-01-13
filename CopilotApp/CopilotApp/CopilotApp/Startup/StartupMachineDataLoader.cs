using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/************************************************************
 * Loads the machine data for the vehicle from the database *
 ************************************************************/

namespace CopilotApp
{
    class StartupMachineDataLoader
    {

        public bool LoadMachineData()
        {
            bool loadSuccessful = true;
            DatabaseL database = new DatabaseL();

            //Prep SQL Query that grabs all the columns we want from the database for the vehicle
            string SQLQuery = "SELECT distance_driven_empty, distance_driven_loaded FROM tpms_vehicle WHERE id = '" + MachineData.machineID + "'";

            //Send Query and store the result data in the reader
            MySqlDataReader reader = database.SendQuery(SQLQuery);

            if (reader != null)
            {
                //Extract the data
                reader.Read();

                if (reader["distance_driven_empty"] != DBNull.Value)
                {
                    MachineBusData.distanceDrivenEmpty = double.Parse(reader["distance_driven_empty"].ToString());
                }
                else
                {
                    loadSuccessful = false;
                }
                    
                if(reader["distance_driven_loaded"] != DBNull.Value) 
                {
                    MachineBusData.distanceDrivenLoaded = double.Parse(reader["distance_driven_loaded"].ToString());
                }
                else
                {
                    loadSuccessful = false;
                }
            }
            else
            {
                loadSuccessful = false;
            }

            return loadSuccessful;
        }
    }
}
