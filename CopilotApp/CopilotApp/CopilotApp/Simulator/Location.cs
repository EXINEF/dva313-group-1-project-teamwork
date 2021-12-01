using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    class Location
    {
        long _longitude;
        long _latitude;

        long longitude { get => _longitude; set { _longitude = value; } }
        long latitude { get => _latitude; set { _latitude = value; } }
        public Location()
        {

        }

    }
}
