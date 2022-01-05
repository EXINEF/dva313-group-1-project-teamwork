using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CopilotApp
{
    class Startup
    {

        public static void Run()
        {
            Calculations calc = new Calculations();
            

            Task.Run(async () => { 
                await TKPHCalculations.LoadK1Data();
                await StartupMachineDataLoader.LoadMachineData();
                await StartupTireDataLoader.LoadTireData();
                Task.Run(async () => { await calc.run(); });
                await AutomatedDataSending.StartSending();
            });

        }

    }


}
