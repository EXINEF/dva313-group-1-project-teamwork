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
        public static double _distanceDrivenEmpty;
        public static double distanceDrivenEmpty
        {
            get => _distanceDrivenEmpty;
            set
            {
                previousTotalDistanceDrivenEmpty = distanceDrivenEmpty == 0 ? value : distanceDrivenEmpty;
                _distanceDrivenEmpty = value;
            }
        }
        public static double _distanceDrivenLoaded;
        public static double distanceDrivenLoaded
        {
            get => _distanceDrivenLoaded;
            set
            {
                //Everytime we set distanceDrivenLoaded we also update the number of tire revolutions
                previousTotalDistanceDrivenLoaded = distanceDrivenLoaded == 0 ? value : distanceDrivenLoaded;
                _distanceDrivenLoaded = value;
                UpdateTireRevolutions();
            }
        }

        public static double machineHoursEmpty;
        public static double machineHoursLoaded;
        public static int payloadBuckets; //AKA Cycles
        public static double payloadTonnes;
        public static double consumedFuel;

        public static double previousTotalDistanceDrivenEmpty;
        public static double previousTotalDistanceDrivenLoaded;

        //Recalculate tire revolutions
        public static void UpdateTireRevolutions()
        {
            
            double deltaDistance = (distanceDrivenEmpty + distanceDrivenLoaded) - (previousTotalDistanceDrivenEmpty + previousTotalDistanceDrivenLoaded);

            //Check just in case
            if (deltaDistance > 0 && TireData.rollingCircumference != 0)
            {
                double deltaRevolutions = (deltaDistance / TireData.rollingCircumference);

                TireData.frontLeftTireRevolutions = TireData.frontLeftTireRevolutions + deltaRevolutions;
                TireData.frontRightTireRevolutions = TireData.frontRightTireRevolutions + deltaRevolutions;
                TireData.rearLeftTireRevolutions = TireData.rearLeftTireRevolutions + deltaRevolutions;
                TireData.rearRightTireRevolutions = TireData.rearRightTireRevolutions + deltaRevolutions;

            }       
        }
    }
}