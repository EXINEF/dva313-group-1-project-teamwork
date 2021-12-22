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
        public static double frontLeftTireBaselinePressure;
        public static string frontLeftTireFillMaterial;
        public static int frontLeftTireTreadDepth;

        //Front Right Tire
        public static string frontRightTireID;
        public static double frontRightTireBaselinePressure;
        public static string frontRightTireFillMaterial;
        public static int frontRightTireTreadDepth;

        //Rear Left Tire
        public static string rearLeftTireID;
        public static double rearLeftTireBaselinePressure;
        public static string rearLeftTireFillMaterial;
        public static int rearLeftTireTreadDepth;

        //Rear Right Tire
        public static string rearRightTireID;
        public static double rearRightTireBaselinePressure;
        public static string rearRightTireFillMaterial;
        public static int rearRightTireTreadDepth;
    }
}
