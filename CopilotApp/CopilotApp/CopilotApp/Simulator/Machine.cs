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
            //Perhaps load from a file or just put in some values here.
        }

    }
}
