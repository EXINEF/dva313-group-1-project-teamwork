using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    class Payload
    {
        long _tons;
        long _numberOfBuckets;

        long tons { get => _tons; set { _tons = value; } }
        long numberOfBuckets { get => _numberOfBuckets; set { _numberOfBuckets = value; } }
        public Payload()
        {

        }
    }
}
