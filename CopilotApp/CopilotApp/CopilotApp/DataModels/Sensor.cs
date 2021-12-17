using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    public class Sensor
    {
        int _id;
        float _temperature;
        float _pressure;
        public int ID { get => _id; set { _id = value; } }
        public float temperature { get => _temperature; set { _temperature = value; } }
        public float pressure { get => _pressure; set { _pressure = value; } }
        

        public Sensor()
        {

        }

        public Sensor(int id)
        {
            ID = id;
        }
    }
}
