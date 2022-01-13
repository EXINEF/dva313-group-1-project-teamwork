using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    public partial class SimulatorPageViewmodel
    {
        public enum MACHINE_TYPE { WHEEL_LOADER, ARTICULATED_HAULER };
        public MACHINE_TYPE machineType;

        string _machineID; public string machineID { get => _machineID; set { _machineID = value; OnPropertyChanged(nameof(machineID)); } }
        string _ambientTemp; public string ambientTemp { get => _ambientTemp; set { _ambientTemp = value; OnPropertyChanged(nameof(ambientTemp)); } }
        string _longitude; public string longitude { get => _longitude; set { _longitude = value; OnPropertyChanged(nameof(longitude)); } }
        string _latitude; public string latitude { get => _latitude; set { _latitude = value; OnPropertyChanged(nameof(latitude)); } }
        string _companyID; public string companyID { get => _companyID; set { _companyID = value; OnPropertyChanged(nameof(companyID)); } }


        private void SendMachineDataToDatabase()
        {

            DatabaseFunctions.SendMachineData(machineID, "Wheel loader", ambientTemp, distanceDrivenEmpty, distanceDrivenLoaded, machineHoursEmpty, machineHoursLoaded, 
                                                       payloadTonnes, payloadBuckets, consumedFuel, frontLeftTireID, rearLeftTireID, frontRightTireID, rearRightTireID, null, companyID);

            return;
        }

        //Sends the longitude and latitude of the machine to the database.
        private void SendLocationDataToDatabase()
        {
            DatabaseFunctions.SendLocationData(machineID, latitude, longitude);
        }

    }
}
