using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;

namespace CopilotApp
{
    class Tire
    {
        public int tireID;
        public int tireBaselinePressure;
        public string tireFillMaterial;
        public float tireTreadDepth;
        public Sensor sensor = new Sensor();
        public int revolutions;
        public string position = String.Empty;

        //Indicator for which position on the machine the tire is occupying
        public enum POSITION { FRONT_LEFT, FRONT_RIGHT, BACK_LEFT, BACK_RIGHT, MIDDLE_LEFT, MIDDLE_RIGHT }
        public POSITION GPSCoordinates;

        public Tire()
        {

        }

        public Tire(int tireID, int tireBaselinePressure, string tireFillMaterial, float tireTreadDepth)
        {
            this.tireID = tireID;
            this.tireBaselinePressure = tireBaselinePressure;
            this.tireFillMaterial = tireFillMaterial;
            this.tireTreadDepth = tireTreadDepth;
        }

        public Tire(int tireID, int tireBaselinePressure, string tireFillMaterial, float tireTreadDepth, Sensor sensor)
        {
            this.tireID = tireID;
            this.tireBaselinePressure = tireBaselinePressure;
            this.tireFillMaterial = tireFillMaterial;
            this.tireTreadDepth = tireTreadDepth;
            this.sensor = sensor;
        }

        public void SetData(int tireID, int tireBaselinePressure, string tireFillMaterial, float tireTreadDepth)
        {
            this.tireID = tireID;
            this.tireBaselinePressure = tireBaselinePressure;
            this.tireFillMaterial = tireFillMaterial;
            this.tireTreadDepth = tireTreadDepth;
        }

        public void SetData(int tireID, int tireBaselinePressure, string tireFillMaterial, float tireTreadDepth, Sensor sensor)
        {
            this.tireID = tireID;
            this.tireBaselinePressure = tireBaselinePressure;
            this.tireFillMaterial = tireFillMaterial;
            this.tireTreadDepth = tireTreadDepth;
            this.sensor = sensor;
        }

        public void LoadTireData(string machineID)
        {
            string query = "SELECT tire_id FROM tpms_vehicle_tires WHERE vehicle_id = '" + machineID + "'";

            MySqlDataReader reader = Database.SendQuery(query);

        }

    }
}
