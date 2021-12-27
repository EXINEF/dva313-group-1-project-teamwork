using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{

    public class TemperatureThresholds
    {
        public static double HIGH_TEMPERATURE_THRESHOLD = 75;
        public static double VERY_HIGH_TEMPERATURE_THRESHOLD = 80;
    }

    public class PressureCoefficients
    {
        public static double VERY_LOW_TIRE_PRESSURE_COEFFICIENT = 0.8;
        public static double LOW_TIRE_PRESSURE_COEFFICIENT = 0.9;
        public static double HIGH_PRESSURE_COEFFICIENT = 1.25;
        public static double VERY_HIGH_TIRE_PRESSURE_COEFFICIENT = 1.4;

    }
}
