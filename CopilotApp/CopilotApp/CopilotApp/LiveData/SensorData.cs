using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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
        public static double frontLeftSensorTemperature { get => _frontLeftSensorTemperature; set { _frontLeftSensorTemperature = value; UpdateFrontLeftTireDisplayValues(); } }
        public static double frontLeftSensorPressure { get => _frontLeftSensorPressure; set { _frontLeftSensorPressure = value; UpdateFrontLeftTireDisplayValues();  } }

        
        //Front Right Sensor Data
        public static string frontRightSensorID { get => _frontRightSensorID; set { _frontRightSensorID = value; } }
        public static string frontRightSensorStatus { get => _frontRightSensorStatus; set { _frontRightSensorStatus = value; } }
        public static double frontRightSensorTemperature { get => _frontRightSensorTemperature; set { _frontRightSensorTemperature = value; UpdateFrontRightTireDisplayValues(); } }
        public static double frontRightSensorPressure { get => _frontRightSensorPressure; set { _frontRightSensorPressure = value; UpdateFrontRightTireDisplayValues(); } }

        
        //Rear Left Sensor Data
        public static string rearLeftSensorID { get => _rearLeftSensorID; set { _rearLeftSensorID = value; } }
        public static string rearLeftSensorStatus { get => _rearLeftSensorStatus; set { _rearLeftSensorStatus = value; } }
        public static double rearLeftSensorTemperature { get => _rearLeftSensorTemperature; set { _rearLeftSensorTemperature = value; UpdateRearLeftTireDisplayValues(); } }
        public static double rearLeftSensorPressure { get => _rearLeftSensorPressure; set { _rearLeftSensorPressure = value; UpdateRearLeftTireDisplayValues(); } }

        
        //Rear Right Sensor Data
        public static string rearRightSensorID { get => _rearRightSensorID; set { _rearRightSensorID = value; } }
        public static string rearRightSensorStatus { get => _rearRightSensorStatus; set { _rearRightSensorStatus = value; } }
        public static double rearRightSensorTemperature { get => _rearRightSensorTemperature; set { _rearRightSensorTemperature = value; UpdatesRearRightTireDisplayValues(); } }
        public static double rearRightSensorPressure { get => _rearRightSensorPressure; set { _rearRightSensorPressure = value; UpdatesRearRightTireDisplayValues(); } }

        
        //If new sensor data is received we want it reflected in the display in the MainPage.
        //These functions sends a message that is received by the MainPage which then grabs these new values and updates the graphics accordingly.
        public static void UpdateFrontLeftTireDisplayValues()
        {
            MessagingCenter.Send<object>(Application.Current, "UpdateFrontLeftTireGraphics");
        }

        public static void UpdateFrontRightTireDisplayValues()
        {
            MessagingCenter.Send<object>(Application.Current, "UpdateFrontRightTireGraphics");
        }

        public static void UpdateRearLeftTireDisplayValues()
        {
            MessagingCenter.Send<object>(Application.Current, "UpdateRearLeftTireGraphics");
        }

        public static void UpdatesRearRightTireDisplayValues()
        {
            MessagingCenter.Send<object>(Application.Current, "UpdateRearRightTireGraphics");
        }
        
    }
}
