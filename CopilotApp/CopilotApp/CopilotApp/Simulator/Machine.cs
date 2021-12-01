using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    class Machine
    {
        public enum MACHINE_TYPE { WHEELOADER, ARTICULATED_HAULER };
        public static MACHINE_TYPE machineType;

        public static int machineID;
        public static int ambientTemp;
        public static float dinstanceDrivenLoaded;
        public static float distanceDrivenEmpty;
        public static int machineHoursLoaded;
        public static int machineHoursEmpty;
        public Payload payload = new Payload();
        public static float consumedFuel;
        public static List<Tire> tires = new List<Tire>();

        public Machine()
        {
        }

        void LoadMachineData()
        {
            //Load from file or whatnot

            //Temp solution perhaps we just want it as a choice in the app for the user to select what type of machine it is rather than base it on nr of tires.
            if (tires.Count > 4)
            {
                machineType = MACHINE_TYPE.ARTICULATED_HAULER;
            }
            else
            {
                machineType = MACHINE_TYPE.WHEELOADER;
            }
        }
    }
}
