using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    public partial class SimulatorPageViewmodel
    {
        string _distanceDrivenEmpty; public string distanceDrivenEmpty { get => _distanceDrivenEmpty; set { _distanceDrivenEmpty = value; OnPropertyChanged(nameof(distanceDrivenEmpty)); } }
        string _distanceDrivenLoaded; public string distanceDrivenLoaded { get => _distanceDrivenLoaded; set { _distanceDrivenLoaded = value; OnPropertyChanged(nameof(distanceDrivenLoaded)); } }
        string _machineHoursEmpty; public string machineHoursEmpty { get => _machineHoursEmpty; set { _machineHoursEmpty = value; OnPropertyChanged(nameof(machineHoursEmpty)); } }
        string _machineHoursLoaded; public string machineHoursLoaded { get => _machineHoursLoaded; set { _machineHoursLoaded = value; OnPropertyChanged(nameof(machineHoursLoaded)); } }
        string _payloadTonnes; public string payloadTonnes { get => _payloadTonnes; set { _payloadTonnes = value; OnPropertyChanged(nameof(payloadTonnes)); } }
        string _payloadBuckets; public string payloadBuckets { get => _payloadBuckets; set { _payloadBuckets = value; OnPropertyChanged(nameof(payloadBuckets)); } }
        string _consumedFuel; public string consumedFuel { get => _consumedFuel; set { _consumedFuel = value; OnPropertyChanged(nameof(consumedFuel)); } }
    }


}
