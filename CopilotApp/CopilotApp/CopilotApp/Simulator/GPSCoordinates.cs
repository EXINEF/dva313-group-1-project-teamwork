using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    public class GPSCoordinates
    {
        long _longitude;
        long _latitude;

        public long longitude { get => _longitude; set { _longitude = value; } }
        public long latitude { get => _latitude; set { _latitude = value; } }
        public GPSCoordinates()
        {

        }

    }
}
