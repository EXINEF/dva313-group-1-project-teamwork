using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CopilotApp
{
    class StartupMachineDataLoader
    {

        public async Task LoadMachineData()
        {

            DatabaseL database = new DatabaseL();

            /************************************************************
             * Loads the machine data for the vehicle from the database *
             ************************************************************/

            //Prep SQL Query that grabs all the columns we want from the database for the vehicle
            string SQLQuery = "SELECT distance_driven_empty, distance_driven_loaded FROM tpms_vehicle WHERE id = '" + MachineData.machineID + "'";

            //Send Query and get the data into the reader
            MySqlDataReader reader = database.SendQuery(SQLQuery);

            if (reader != null)
            {
                //Extract the data
                reader.Read();

                if (reader["distance_driven_empty"] != DBNull.Value)
                {
                    MachineBusData.distanceDrivenEmpty = double.Parse(reader["distance_driven_empty"].ToString());
                }
                    
                if(reader["distance_driven_loaded"] != DBNull.Value) 
                {
                    MachineBusData.distanceDrivenLoaded = double.Parse(reader["distance_driven_loaded"].ToString());
                }

                Console.WriteLine("MachineBusData.distanceDrivenEmpty:" + MachineBusData.distanceDrivenEmpty + " MachineBusData.distanceDrivenLoaded: " + MachineBusData.distanceDrivenLoaded);
            }
            else
            {
                Console.WriteLine("MySqlDataReader reader is NULL in StartupMachineDataLoader.cs");
            }

            await Task.CompletedTask;
        }
    }
}
