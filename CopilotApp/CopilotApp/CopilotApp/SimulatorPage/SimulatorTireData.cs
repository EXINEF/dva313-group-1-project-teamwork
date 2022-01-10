using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;

namespace CopilotApp
{
    public partial class SimulatorPageViewmodel
    {
        //---Tire Variables---//
        //Front Left
        string _frontLeftTireID; public string frontLeftTireID { get => _frontLeftTireID; set { _frontLeftTireID = value; OnPropertyChanged(nameof(frontLeftTireID)); } }
        string _frontLeftTireBaselinePressure; public string frontLeftTireBaselinePressure { get => _frontLeftTireBaselinePressure; set { _frontLeftTireBaselinePressure = value; OnPropertyChanged(nameof(frontLeftTireBaselinePressure)); } }
        string _frontLeftTireFillMaterial; public string frontLeftTireFillMaterial { get => _frontLeftTireFillMaterial; set { _frontLeftTireFillMaterial = value; OnPropertyChanged(nameof(frontLeftTireFillMaterial)); } }
        string _frontLeftTireTreadDepth; public string frontLeftTireTreadDepth { get => _frontLeftTireTreadDepth; set { _frontLeftTireTreadDepth = value; OnPropertyChanged(nameof(frontLeftTireTreadDepth)); } }

        //Front Right
        string _frontRightTireID; public string frontRightTireID { get => _frontRightTireID; set { _frontRightTireID = value; OnPropertyChanged(nameof(frontRightTireID)); } }
        string _frontRightTireBaselinePressure; public string frontRightTireBaselinePressure { get => _frontRightTireBaselinePressure; set { _frontRightTireBaselinePressure = value; OnPropertyChanged(nameof(frontRightTireBaselinePressure)); } }
        string _frontRightTireFillMaterial; public string frontRightTireFillMaterial { get => _frontRightTireFillMaterial; set { _frontRightTireFillMaterial = value; OnPropertyChanged(nameof(frontRightTireFillMaterial)); } }
        string _frontRightTireTreadDepth; public string frontRightTireTreadDepth { get => _frontRightTireTreadDepth; set { _frontRightTireTreadDepth = value; OnPropertyChanged(nameof(frontRightTireTreadDepth)); } }

        //Rear Left
        string _rearLeftTireID; public string rearLeftTireID { get => _rearLeftTireID; set { _rearLeftTireID = value; OnPropertyChanged(nameof(rearLeftTireID)); } }
        string _rearLeftTireBaselinePressure; public string rearLeftTireBaselinePressure { get => _rearLeftTireBaselinePressure; set { _rearLeftTireBaselinePressure = value; OnPropertyChanged(nameof(rearLeftTireBaselinePressure)); } }
        string _rearLeftTireFillMaterial; public string rearLeftTireFillMaterial { get => _rearLeftTireFillMaterial; set { _rearLeftTireFillMaterial = value; OnPropertyChanged(nameof(rearLeftTireFillMaterial)); } }
        string _rearLeftTireTreadDepth; public string rearLeftTireTreadDepth { get => _rearLeftTireTreadDepth; set { _rearLeftTireTreadDepth = value; OnPropertyChanged(nameof(rearLeftTireTreadDepth)); } }

        //Rear Right
        string _rearRightTireID; public string rearRightTireID { get => _rearRightTireID; set { _rearRightTireID = value; OnPropertyChanged(nameof(rearRightTireID)); } }
        string _rearRightTireBaselinePressure; public string rearRightTireBaselinePressure { get => _rearRightTireBaselinePressure; set { _rearRightTireBaselinePressure = value; OnPropertyChanged(nameof(rearRightTireBaselinePressure)); } }
        string _rearRightTireFillMaterial; public string rearRightTireFillMaterial { get => _rearRightTireFillMaterial; set { _rearRightTireFillMaterial = value; OnPropertyChanged(nameof(rearRightTireFillMaterial)); } }
        string _rearRightTireTreadDepth; public string rearRightTireTreadDepth { get => _rearRightTireTreadDepth; set { _rearRightTireTreadDepth = value; OnPropertyChanged(nameof(rearRightTireTreadDepth)); } }

        //Indicator for which position on the machine the tire is fitted.
        public enum POSITION { FRONT_LEFT, FRONT_RIGHT, REAR_LEFT, REAR_RIGHT, CENTER_LEFT, CENTER_RIGHT }
        public POSITION position;

        //This will be determined by calculations before sending to the database
        public enum TIRE_SPECC { OVER, UNDER, NEUTRAL }


        public void LoadTireDataFromDatabase(string tireID)
        {
            string query = "SELECT tire_id FROM tpms_vehicle_tires WHERE vehicle_id = '" + tireID + "'";
            MySqlDataReader reader = Database.SendQuery(query);
        }

        public void SendTireDataToDatabase()
        {
            DatabaseFunctions.SendTireData(frontLeftTireID, null,frontLeftTireBaselinePressure, frontLeftTireFillMaterial, frontLeftTireTreadDepth, null, companyID, frontLeftSensorID, "0");
            DatabaseFunctions.SendTireData(frontRightTireID, null, frontRightTireBaselinePressure, frontRightTireFillMaterial, frontRightTireTreadDepth, null, companyID, frontRightSensorID, "0");
            DatabaseFunctions.SendTireData(rearLeftTireID, null, rearLeftTireBaselinePressure, rearLeftTireFillMaterial, rearLeftTireTreadDepth, null, companyID, rearLeftSensorID, "0");
            DatabaseFunctions.SendTireData(rearRightTireID, null, rearRightTireBaselinePressure, rearRightTireFillMaterial, rearRightTireTreadDepth, null, companyID, rearRightSensorID, "0");
        }


    }
}
