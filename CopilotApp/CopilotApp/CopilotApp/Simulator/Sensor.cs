using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    class Sensor
    {
        int _id;
        int ID { get => _id; set { _id = value; } }

        public Sensor()
        {

        }

        public Sensor(int id)
        {
            ID = id;
        }


        //Quick function that will just produce random temp/pressure data and send i to Input.cs
        public void SimulateRandomData(float baselinePressure)
        {
            Random random = new Random();
            int temperature = Machine.ambientTemp + random.Next(1, 100);
            float pressure = baselinePressure + (float)random.NextDouble(); ;
            Input.ReceiveSensorInput(ID, temperature, pressure);
        }
    }
}
