using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    public class Machine
    {
        public enum MACHINE_TYPE { WHEEL_LOADER, ARTICULATED_HAULER };
        public static MACHINE_TYPE machineType;

        public static int machineID;
        public static int ambientTemp;
        public static List<Tire> tires = new List<Tire>();
        public static MachineBus machineBus = new MachineBus();

        public Machine()
        {

        }

        void LoadMachineDataFromDatabase()
        {
            
        }

        void LoadTireDataFromDatabase()
        {
            string query = "SELECT tire_id FROM tpms_vehicle_tires WHERE vehicle_id = '" + machineID + "'";
            MySqlDataReader reader = Database.SendQuery(query);

            //Loop Through Tires and Load Their Data
            //
            //
        }

    }
}
