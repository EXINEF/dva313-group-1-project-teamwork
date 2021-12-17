using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;

namespace CopilotApp
{
    public class Tire
    {
        public int tireID;
        public int baselinePressure;
        public string fillMaterial;
        public float treadDepth;
        public Sensor sensor = new Sensor();
        public int revolutions;

        //Indicator for which position on the machine the tire is fitted.
        public enum POSITION { FRONT_LEFT, FRONT_RIGHT, REAR_LEFT, REAR_RIGHT, MIDDLE_LEFT, MIDDLE_RIGHT }
        public POSITION position;

        public Tire()
        {

        }
        public Tire(int tireID)
        {
            this.tireID = tireID;
        }

        public Tire(int tireID, int tireBaselinePressure, string tireFillMaterial, float tireTreadDepth)
        {
            this.tireID = tireID;
            this.baselinePressure = tireBaselinePressure;
            this.fillMaterial = tireFillMaterial;
            this.treadDepth = tireTreadDepth;
        }

        public Tire(int tireID, int tireBaselinePressure, string tireFillMaterial, float tireTreadDepth, Sensor sensor)
        {
            this.tireID = tireID;
            this.baselinePressure = tireBaselinePressure;
            this.fillMaterial = tireFillMaterial;
            this.treadDepth = tireTreadDepth;
            this.sensor = sensor;
        }

        public void SetData(int tireID, int tireBaselinePressure, string tireFillMaterial, float tireTreadDepth)
        {
            this.tireID = tireID;
            this.baselinePressure = tireBaselinePressure;
            this.fillMaterial = tireFillMaterial;
            this.treadDepth = tireTreadDepth;
        }

        public void SetData(int tireID, int tireBaselinePressure, string tireFillMaterial, float tireTreadDepth, Sensor sensor)
        {
            this.tireID = tireID;
            this.baselinePressure = tireBaselinePressure;
            this.fillMaterial = tireFillMaterial;
            this.treadDepth = tireTreadDepth;
            this.sensor = sensor;
        }

        public void LoadTireDataFromDatabase(string tireID)
        {
            string query = "SELECT tire_id FROM tpms_vehicle_tires WHERE vehicle_id = '" + tireID + "'";
            MySqlDataReader reader = Database.SendQuery(query);
        }

    }
}
