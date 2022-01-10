using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CopilotApp
{
    class Startup
    {
        StartupMachineDataLoader startupMachineDataLoader = new StartupMachineDataLoader();
        StartupTireDataLoader startupTireDataLoader = new StartupTireDataLoader();
        AutomatedDataSending automatedDataSending = new AutomatedDataSending();
        Calculations calc = new Calculations();

        public async Task<Task> Run()
        {
            await TKPHCalculations.LoadK1Data();
            await startupMachineDataLoader.LoadMachineData();
            await startupTireDataLoader.LoadTireData();
            Task.Run(async () => { await calc.run(); });
            Task.Run(async () => { await automatedDataSending.StartSending(); });

            return Task.CompletedTask;
        }

    }


}
