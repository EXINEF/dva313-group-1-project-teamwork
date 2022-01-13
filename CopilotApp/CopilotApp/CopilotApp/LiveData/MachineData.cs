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
        public static string machineID = "999"; 
        public static string machineType = "Wheel loader";
        public static string companyID = "5";
        public static double ambientTemperature;
    }
}
