using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    class Tire
    {
        public int ID;
        public int baselinePressure;
        public string fillMaterial;
        public float treadDepth;
        public Sensor sensor = new Sensor();
        public int revolutions;

        //Indicator for which position on the machine the tire is occupying
        public enum POSITION { FRONT_LEFT, FRONT_RIGHT, BACK_LEFT, BACK_RIGHT, MIDDLE_LEFT, MIDDLE_RIGHT }
        public POSITION position;

        public Tire()
        {

        }

        public Tire(int ID, int baselinePressure, string fillMaterial, float treadDepth)
        {
            this.ID = ID;
            this.baselinePressure = baselinePressure;
            this.fillMaterial = fillMaterial;
            this.treadDepth = treadDepth;
        }

        public Tire(int ID, int baselinePressure, string fillMaterial, float treadDepth, Sensor sensor)
        {
            this.ID = ID;
            this.baselinePressure = baselinePressure;
            this.fillMaterial = fillMaterial;
            this.treadDepth = treadDepth;
            this.sensor = sensor;
        }

        public void SetData(int ID, int baselinePressure, string fillMaterial, float treadDepth)
        {
            this.ID = ID;
            this.baselinePressure = baselinePressure;
            this.fillMaterial = fillMaterial;
            this.treadDepth = treadDepth;
        }

        public void SetData(int ID, int baselinePressure, string fillMaterial, float treadDepth, Sensor sensor)
        {
            this.ID = ID;
            this.baselinePressure = baselinePressure;
            this.fillMaterial = fillMaterial;
            this.treadDepth = treadDepth;
            this.sensor = sensor;
        }

    }
}
