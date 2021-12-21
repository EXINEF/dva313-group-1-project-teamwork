using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    /***************************************************************
     * Here we hold active data for the tires.                     *
     * This data is static and available to and from all pages.    *
     ***************************************************************/

    public class TireData
    {
        //Front Left Tire
        public static string frontLeftTireID;
        public static string frontLeftTireBaselinePressure;
        public static string frontLeftTireFillMaterial;
        public static string frontLeftTireTreadDepth;

        //Front Right Tire
        public static string frontRightTireID;
        public static string frontRightTireBaselinePressure;
        public static string frontRightTireFillMaterial;
        public static string frontRightTireTreadDepth;

        //Rear Left Tire
        public static string rearLeftTireTireID;
        public static string rearLeftBaselinePressure;
        public static string rearLeftTireFillMaterial;
        public static string rearLeftTireTreadDepth;

        //Rear Right Tire
        public static string rearRightTireID;
        public static string rearRightTireBaselinePressure;
        public static string rearRightTireFillMaterial;
        public static string rearRightTireTreadDepth;
    }
}
