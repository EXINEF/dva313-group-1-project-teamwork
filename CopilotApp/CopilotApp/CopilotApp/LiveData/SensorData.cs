using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    /***************************************************************
     * Here we hold active data for the sensors.                   *
     * This data is static and available to and from all pages.    *
     ***************************************************************/

    public partial class SensorData
    {
        //Front Left Sensor Data
        public static string frontLeftSensorID { get => _frontLeftSensorID; set { _frontLeftSensorID = value; } }
        public static string frontLeftSensorStatus { get => _frontLeftSensorStatus; set { _frontLeftSensorStatus = value; } }
        public static int frontLeftSensorTemperature { get => _frontLeftSensorTemperature; set { _frontLeftSensorTemperature = value; UpdateFrontLeftTireDisplayValues(); } }
        public static double frontLeftSensorPressure { get => _frontLeftSensorPressure; set { _frontLeftSensorPressure = value; UpdateFrontLeftTireDisplayValues();  } }

        
        //Front Right Sensor Data
        public static string frontRightSensorID { get => _frontRightSensorID; set { _frontRightSensorID = value; } }
        public static string frontRightSensorStatus { get => _frontRightSensorStatus; set { _frontRightSensorStatus = value; } }
        public static int frontRightSensorTemperature { get => _frontRightSensorTemperature; set { _frontRightSensorTemperature = value; UpdateFrontRightTireDisplayValues(); } }
        public static double frontRightSensorPressure { get => _frontRightSensorPressure; set { _frontRightSensorPressure = value; UpdateFrontRightTireDisplayValues(); } }

        
        //Rear Left Sensor Data
        public static string rearLeftSensorID { get => _rearLeftSensorID; set { _rearLeftSensorID = value; } }
        public static string rearLeftSensorStatus { get => _rearLeftSensorStatus; set { _rearLeftSensorStatus = value; } }
        public static int rearLeftSensorTemperature { get => _rearLeftSensorTemperature; set { _rearLeftSensorTemperature = value; UpdateRearLeftTireDisplayValues(); } }
        public static double rearLeftSensorPressure { get => _rearLeftSensorPressure; set { _rearLeftSensorPressure = value; UpdateRearLeftTireDisplayValues(); } }

        
        //Rear Right Sensor Data
        public static string rearRightSensorID { get => _rearRightSensorID; set { _rearRightSensorID = value; } }
        public static string rearRightSensorStatus { get => _rearRightSensorStatus; set { _rearRightSensorStatus = value; } }
        public static int rearRightSensorTemperature { get => _rearRightSensorTemperature; set { _rearRightSensorTemperature = value; UpdatesRearRightTireDisplayValues(); } }
        public static double rearRightSensorPressure { get => _rearRightSensorPressure; set { _rearRightSensorPressure = value; UpdatesRearRightTireDisplayValues(); } }

        
        //The instance of the MainPageViewmodel.
        //If we update the data here we want it to be reflected in the display values in the main page.
        //Is initialized by MainPageViewModel.cs when it is loaded.
        public static MainPageViewmodel mainPageViewmodel;


        //Functions for updating the display values in the MainPage.
        public static void UpdateFrontLeftTireDisplayValues()
        {
            if (mainPageViewmodel != null)
            {
                mainPageViewmodel.frontLeftTireTemperatureDisplayValue = frontLeftSensorTemperature.ToString();
                mainPageViewmodel.frontLeftTirePressureDisplayValue = frontLeftSensorPressure.ToString();
            }
        }

        public static void UpdateFrontRightTireDisplayValues()
        {
            if (mainPageViewmodel != null)
            {
                mainPageViewmodel.frontRightTireTemperatureDisplayValue = frontRightSensorTemperature.ToString();
                mainPageViewmodel.frontRightTirePressureDisplayValue = frontRightSensorPressure.ToString();
            }
        }

        public static void UpdateRearLeftTireDisplayValues()
        {
            if (mainPageViewmodel != null)
            {
                mainPageViewmodel.rearLeftTireTemperatureDisplayValue = rearLeftSensorTemperature.ToString();
                mainPageViewmodel.rearLeftTirePressureDisplayValue = rearLeftSensorPressure.ToString();
            }
        }

        public static void UpdatesRearRightTireDisplayValues()
        {
            if (mainPageViewmodel != null)
            {
                mainPageViewmodel.rearRightTireTemperatureDisplayValue = rearRightSensorTemperature.ToString();
                mainPageViewmodel.rearRightTirePressureDisplayValue = rearRightSensorPressure.ToString();
            }
        }
    }
}
