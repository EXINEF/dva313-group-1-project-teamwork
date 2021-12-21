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
        public static string frontLeftSensorTemperature { get => _frontLeftSensorTemperature; set { _frontLeftSensorTemperature = value; UpdateFrontLeftTireDisplayValues(); } }
        public static string frontLeftSensorPressure { get => _frontLeftSensorPressure; set { _frontLeftSensorPressure = value; UpdateFrontLeftTireDisplayValues();  } }

        
        //Front Right Sensor Data
        public static string frontRightSensorID { get => _frontRightSensorID; set { _frontRightSensorID = value; } }
        public static string frontRightSensorStatus { get => _frontRightSensorStatus; set { _frontRightSensorStatus = value; } }
        public static string frontRightSensorTemperature { get => _frontRightSensorTemperature; set { _frontRightSensorTemperature = value; UpdateFrontRightTireDisplayValues(); } }
        public static string frontRightSensorPressure { get => _frontRightSensorPressure; set { _frontRightSensorPressure = value; UpdateFrontRightTireDisplayValues(); } }

        
        //Rear Left Sensor Data
        public static string rearLeftSensorID { get => _rearLeftSensorID; set { _rearLeftSensorID = value; } }
        public static string rearLeftSensorStatus { get => _rearLeftSensorStatus; set { _rearLeftSensorStatus = value; } }
        public static string rearLeftSensorTemperature { get => _rearLeftSensorTemperature; set { _rearLeftSensorTemperature = value; UpdateRearLeftTireDisplayValues(); } }
        public static string rearLeftSensorPressure { get => _rearLeftSensorPressure; set { _rearLeftSensorPressure = value; UpdateRearLeftTireDisplayValues(); } }

        
        //Rear Right Sensor Data
        public static string rearRightSensorID { get => _rearRightSensorID; set { _rearRightSensorID = value; } }
        public static string rearRightSensorStatus { get => _rearRightSensorStatus; set { _rearRightSensorStatus = value; } }
        public static string rearRightSensorTemperature { get => _rearRightSensorTemperature; set { _rearRightSensorTemperature = value; UpdatesRearRightTireDisplayValues(); } }
        public static string rearRightSensorPressure { get => _rearRightSensorPressure; set { _rearRightSensorPressure = value; UpdatesRearRightTireDisplayValues(); } }

        
        //The instance of the MainPageViewmodel.
        //If we update the data here we want it to be reflected in the display values in the main page.
        //Is initialized by MainPageViewModel.cs when it is loaded.
        public static MainPageViewmodel mainPageViewmodel;


        //Functions for updating the display values in the MainPage.
        public static void UpdateFrontLeftTireDisplayValues()
        {
            if (mainPageViewmodel != null)
            {
                mainPageViewmodel.frontLeftTireTemperatureDisplayValue = frontLeftSensorTemperature;
                mainPageViewmodel.frontLeftTirePressureDisplayValue = frontLeftSensorPressure;
            }
        }

        public static void UpdateFrontRightTireDisplayValues()
        {
            if (mainPageViewmodel != null)
            {
                mainPageViewmodel.frontRightTireTemperatureDisplayValue = frontRightSensorTemperature;
                mainPageViewmodel.frontRightTirePressureDisplayValue = frontRightSensorPressure;
            }
        }

        public static void UpdateRearLeftTireDisplayValues()
        {
            if (mainPageViewmodel != null)
            {
                mainPageViewmodel.rearLeftTireTemperatureDisplayValue = rearLeftSensorTemperature;
                mainPageViewmodel.rearLeftTirePressureDisplayValue = rearLeftSensorPressure;
            }
        }

        public static void UpdatesRearRightTireDisplayValues()
        {
            if (mainPageViewmodel != null)
            {
                mainPageViewmodel.rearRightTireTemperatureDisplayValue = rearRightSensorTemperature;
                mainPageViewmodel.rearRightTirePressureDisplayValue = rearRightSensorPressure;
            }
        }
    }
}
