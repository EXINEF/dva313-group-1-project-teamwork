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
        int retryDelayms = 20000;

        public async Task<Task> Run()
        {
            bool machineDataLoaded = false;
            bool tireDataLoaded = false;
            bool connectionErrorNotificationSent = false;
            await TKPHCalculations.LoadK1Data();

            while (machineDataLoaded == false || tireDataLoaded == false)
            {
                machineDataLoaded = startupMachineDataLoader.LoadMachineData();
                tireDataLoaded = startupTireDataLoader.LoadTireData();

                if(machineDataLoaded == true && tireDataLoaded == true)
                {
                    break;
                }
                else
                {
                    if (!connectionErrorNotificationSent)
                    {
                        MainPageViewmodel.PushNotification("ERROR: Could not fetch cloud data!");
                        connectionErrorNotificationSent = true;
                    }
                    
                    Thread.Sleep(retryDelayms);
                }
            }

            Task.Run(async () => { await calc.run(); });
            Task.Run(async () => { await automatedDataSending.StartSending(); });

            return Task.CompletedTask;
        }

    }


}
