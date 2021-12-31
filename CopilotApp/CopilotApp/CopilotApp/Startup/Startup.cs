using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CopilotApp
{
    class Startup
    {

        public static void Run()
        {
            Task.Run(async () => { await TKPHCalculations.LoadK1Data(); });
            //Load tire data
        }
    }


}
