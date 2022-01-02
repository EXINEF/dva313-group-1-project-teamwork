using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace CopilotApp
{
    /***************************************************************
     * Here we hold active data for the machine bus.               *
     * This data is static and available to and from all pages.    *
     ***************************************************************/

    public class MachineBusData
    {
        public static double distanceDrivenEmpty;
        public static double distanceDrivenLoaded;
        public static double machineHoursEmpty;
        public static double machineHoursLoaded;
        public static int payloadBuckets; //AKA Cycles
        public static double payloadTonnes;
        public static double consumedFuel;
    }
}