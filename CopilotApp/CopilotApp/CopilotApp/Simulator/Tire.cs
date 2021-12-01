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

        public Tire()
        {

        }
    }
}
