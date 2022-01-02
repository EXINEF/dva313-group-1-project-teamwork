﻿using System;
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
        public static double frontLeftTireTreadDepth;
        public static double frontLeftTireLife;
        public static double frontLeftTireRevolutions;

        //Front Right Tire
        public static string frontRightTireID;
        public static double frontRightTireBaselinePressure;
        public static string frontRightTireFillMaterial;
        public static double frontRightTireTreadDepth;
        public static double frontRightTireLife;
        public static double frontRightTireRevolutions;

        //Rear Left Tire
        public static string rearLeftTireID;
        public static double rearLeftTireBaselinePressure;
        public static string rearLeftTireFillMaterial;
        public static double rearLeftTireTreadDepth;
        public static double rearLeftTireLife;
        public static double rearLeftTireRevolutions;

        //Rear Right Tire
        public static string rearRightTireID;
        public static double rearRightTireBaselinePressure;
        public static string rearRightTireFillMaterial;
        public static double rearRightTireTreadDepth;
        public static double rearRightTireLife;
        public static double rearRightTireRevolutions;
    }
}
