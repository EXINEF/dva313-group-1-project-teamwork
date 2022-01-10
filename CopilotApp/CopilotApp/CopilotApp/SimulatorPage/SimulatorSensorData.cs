using System;
using System.Collections.Generic;
using System.Text;

namespace CopilotApp
{
    public partial class SimulatorPageViewmodel
    {
        //Front Left
        string _frontLeftSensorID; public string frontLeftSensorID { get => _frontLeftSensorID; set { _frontLeftSensorID = value; OnPropertyChanged(nameof(frontLeftSensorID)); } }
        string _frontLeftSensorStatus; public string frontLeftSensorStatus { get => _frontLeftSensorStatus; set { _frontLeftSensorStatus = value; OnPropertyChanged(nameof(frontLeftSensorStatus)); } }
        string _frontLeftSensorTemp; public string frontLeftSensorTemp { get => _frontLeftSensorTemp; set { _frontLeftSensorTemp = value; OnPropertyChanged(nameof(frontLeftSensorTemp)); } }
        string _frontLeftSensorPressure; public string frontLeftSensorPressure { get => _frontLeftSensorPressure; set { _frontLeftSensorPressure = value; OnPropertyChanged(nameof(frontLeftSensorPressure)); } }

        //Front Right
        string _frontRightSensorID; public string frontRightSensorID { get => _frontRightSensorID; set { _frontRightSensorID = value; OnPropertyChanged(nameof(frontRightSensorID)); } }
        string _frontRightSensorStatus; public string frontRightSensorStatus { get => _frontRightSensorStatus; set { _frontRightSensorStatus = value; OnPropertyChanged(nameof(frontRightSensorStatus)); } }
        string _frontRightSensorTemp; public string frontRightSensorTemp { get => _frontRightSensorTemp; set { _frontRightSensorTemp = value; OnPropertyChanged(nameof(frontRightSensorTemp)); } }
        string _frontRightSensorPressure; public string frontRightSensorPressure { get => _frontRightSensorPressure; set { _frontRightSensorPressure = value; OnPropertyChanged(nameof(frontRightSensorPressure)); } }

        //Rear Left
        string _rearLeftSensorID; public string rearLeftSensorID { get => _rearLeftSensorID; set { _rearLeftSensorID = value; OnPropertyChanged(nameof(rearLeftSensorID)); } }
        string _rearLeftSensorStatus; public string rearLeftSensorStatus { get => _rearLeftSensorStatus; set { _rearLeftSensorStatus = value; OnPropertyChanged(nameof(rearLeftSensorStatus)); } }
        string _rearLeftSensorTemp; public string rearLeftSensorTemp { get => _rearLeftSensorTemp; set { _rearLeftSensorTemp = value; OnPropertyChanged(nameof(rearLeftSensorTemp)); } }
        string _rearLeftSensorPressure; public string rearLeftSensorPressure { get => _rearLeftSensorPressure; set { _rearLeftSensorPressure = value; OnPropertyChanged(nameof(rearLeftSensorPressure)); } }

        //Rear Right
        string _rearRightSensorID; public string rearRightSensorID { get => _rearRightSensorID; set { _rearRightSensorID = value; OnPropertyChanged(nameof(rearRightSensorID)); } }
        string _rearRightSensorStatus; public string rearRightSensorStatus { get => _rearRightSensorStatus; set { _rearRightSensorStatus = value; OnPropertyChanged(nameof(rearRightSensorStatus)); } }
        string _rearRightSensorTemp; public string rearRightSensorTemp { get => _rearRightSensorTemp; set { _rearRightSensorTemp = value; OnPropertyChanged(nameof(rearRightSensorTemp)); } }
        string _rearRightSensorPressure; public string rearRightSensorPressure { get => _rearRightSensorPressure; set { _rearRightSensorPressure = value; OnPropertyChanged(nameof(rearRightSensorPressure)); } }

        public void SendSensorDataToDatabase()
        {
            DatabaseFunctions.SendSensorData(_frontLeftSensorID, frontLeftSensorPressure, frontLeftSensorTemp, frontLeftSensorStatus, null, companyID, "0");
            DatabaseFunctions.SendSensorData(_frontRightSensorID, frontRightSensorPressure, frontRightSensorTemp, frontRightSensorStatus, null, companyID, "0");
            DatabaseFunctions.SendSensorData(_rearLeftSensorID, rearLeftSensorPressure, rearLeftSensorTemp, frontLeftSensorStatus, null, companyID, "0");
            DatabaseFunctions.SendSensorData(_rearRightSensorID, rearRightSensorPressure, rearRightSensorTemp, frontRightSensorStatus, null, companyID, "0");
        }
    }
}
