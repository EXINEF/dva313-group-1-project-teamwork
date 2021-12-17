using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    public class MachineBus
    {
        public static float consumedFuel;
        public static DistanceDriven distanceDriven = new DistanceDriven();
        public static MachineHours machineHours = new MachineHours();
        public static Payload payload = new Payload();
    }

    public class DistanceDriven
    {
        float _loaded;
        float _empty;

        public float loaded { get => _loaded; set { _loaded = value; } }
        public float empty { get => _empty; set { _empty = value; } }

    }
    public class MachineHours
    {
        float _loaded;
        float _empty;
        public float loaded { get => _loaded; set { _loaded = value; } }
        public float empty { get => _empty; set { _empty = value; } }
    }
    public class Payload
    {
        long _tons;
        long _numberOfBuckets;

        long tons { get => _tons; set { _tons = value; } }
        long numberOfBuckets { get => _numberOfBuckets; set { _numberOfBuckets = value; } }
    }
}
