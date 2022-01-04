using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    /***************************************************************
     * Here we hold active data for the machine.                   *
     * This data is static and available to and from all pages.    *
     ***************************************************************/

    public class MachineData
    {
        public static string machineID = "999"; //switched from 1 to test the values that exists in DB.
        public static string machineType = "Wheel loader";
        public static string companyName = "Volvo";
        public static double ambientTemperature;
    }
}
