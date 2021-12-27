using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CopilotApp
{
    public partial class SimulatorPageViewmodel : INotifyPropertyChanged
    {
        //Variables:
        //_backingfield ; public string varName {get; set} 
        //OnPropertyChanged(nameof(varName)) updates the value shown on screen(SimulatorPage.xaml) if we update the variable in here in code


        public SimulatorPageViewmodel()
        {
            //Binds the "BackToMainPageButtonPressedCommand" called in SimulatorPage.xaml to the ReturnToMainPage() function in this class
            BackToMainPageButtonPressedCommand = new Command(ReturnToMainPage);
            SendMachineDataButtonPressedCommand = new Command(SendMachineData);
            SendSensorDataButtonPressedCommand = new Command(SendSensorData);
            SendTireDataButtonPressedCommand = new Command(SendTireData);
            CopyDataToCopilotButtonPressedCommand = new Command(CopyDataToCopilot);
        }

        //New command. The command we call from the xaml code (Command="{Binding BackToOverviewButtonPressedCommand}).
        public ICommand BackToMainPageButtonPressedCommand { get; }
        public ICommand SendMachineDataButtonPressedCommand { get; }
        public ICommand SendSensorDataButtonPressedCommand { get; }
        public ICommand SendTireDataButtonPressedCommand { get; }
        public ICommand CopyDataToCopilotButtonPressedCommand { get; }

        //Enables the properties to be automatically called if updated
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        async void ReturnToMainPage()
        {
            //The pages work as a stack, when we press the simulator button on the MainPage a SimulatorPage is pushed on the stack
            //This pops the simulator page from the stack making MainPage the top page again.
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        void SendMachineData()
        {
            SendMachineDataToDatabase();
            //SendLocationDataToDatabase();
        }

        void SendSensorData()
        {
            SendSensorDataToDatabase();
        }

        void SendTireData()
        {
            SendTireDataToDatabase();
        }

        // Replace the CoPilot live data with the data from the simulator but only if the field in the simulator is not empty.
        void CopyDataToCopilot()
        {
            //Machine
            if (machineID != null && machineID != "") { MachineData.machineID = machineID; }
            if (ambientTemp != null && ambientTemp != "") { MachineData.ambientTemperature = double.Parse(ambientTemp); }

            //MachineBus
            if (distanceDrivenEmpty != null && distanceDrivenEmpty != "") { MachineBusData.distanceDrivenEmpty = double.Parse(distanceDrivenEmpty);  }
            if (distanceDrivenLoaded != null && distanceDrivenLoaded != "") { MachineBusData.distanceDrivenLoaded = double.Parse(distanceDrivenLoaded); }
            if (machineHoursEmpty != null && machineHoursEmpty != "") { MachineBusData.machineHoursEmpty = double.Parse(machineHoursEmpty); }
            if (machineHoursLoaded != null && machineHoursLoaded != "") { MachineBusData.machineHoursLoaded = double.Parse(machineHoursLoaded); }
            if (payloadBuckets != null && payloadBuckets != "") { MachineBusData.payloadBuckets = int.Parse(payloadBuckets); }
            if (payloadTonnes != null && payloadTonnes != "") { MachineBusData.payloadTonnes = double.Parse(payloadTonnes); }
            if (consumedFuel != null && consumedFuel != "") { MachineBusData.consumedFuel = double.Parse(consumedFuel); }

            //GPS
            if (latitude != null && latitude != "") { GPSData.latitude = float.Parse(latitude); }
            if (longitude != null && longitude != "") { GPSData.longitude = float.Parse(longitude); }

            //Time
            if (year != null && year != "") { TimeData.year = int.Parse(year); }
            if (month != null && month != "") { TimeData.month = int.Parse(month); }
            if (day != null && day != "") { TimeData.day = int.Parse(day); }
            if (hour != null && hour != "") { TimeData.hour = int.Parse(hour); }
            if (minute != null && minute != "") { TimeData.minute = int.Parse(minute); }
            if (second != null && second != "") { TimeData.second = int.Parse(second); }

            //Front Left Tire
            if (frontLeftTireID != null && frontLeftTireID != "") { TireData.frontLeftTireID = frontLeftTireID; }
            if (frontLeftTireBaselinePressure != null && frontLeftTireBaselinePressure != "") { TireData.frontLeftTireBaselinePressure = double.Parse(frontLeftTireBaselinePressure); }
            if (frontLeftTireFillMaterial != null && frontLeftTireFillMaterial != "") { TireData.frontLeftTireFillMaterial = frontLeftTireFillMaterial; }
            if (frontLeftTireTreadDepth != null && frontLeftTireTreadDepth != "") { TireData.frontLeftTireTreadDepth = int.Parse(frontLeftTireTreadDepth); }

            //Front Right Tire
            if (frontRightTireID != null && frontRightTireID != "") { TireData.frontRightTireID = frontRightTireID; }
            if (frontRightTireBaselinePressure != null && frontRightTireBaselinePressure != "") { TireData.frontRightTireBaselinePressure = double.Parse(frontRightTireBaselinePressure); }
            if (frontRightTireFillMaterial != null && frontRightTireFillMaterial != "") { TireData.frontRightTireFillMaterial = frontRightTireFillMaterial; }
            if (frontRightTireTreadDepth != null && frontRightTireTreadDepth != "") { TireData.frontRightTireTreadDepth = int.Parse(frontRightTireTreadDepth); }

            //Rear Left Tire
            if (rearLeftTireID != null && rearLeftTireID != "") { TireData.rearLeftTireID = rearLeftTireID; }
            if (rearLeftTireBaselinePressure != null && rearLeftTireBaselinePressure != "") { TireData.rearLeftTireBaselinePressure = double.Parse(rearLeftTireBaselinePressure); }
            if (rearLeftTireFillMaterial != null && rearLeftTireFillMaterial != "") { TireData.rearLeftTireFillMaterial = rearLeftTireFillMaterial; }
            if (rearLeftTireTreadDepth != null && rearLeftTireTreadDepth != "") { TireData.rearLeftTireTreadDepth = int.Parse(rearLeftTireTreadDepth); }

            //Rear Right Tire
            if (rearRightTireID != null && rearRightTireID != "") { TireData.rearRightTireID = rearRightTireID; }
            if (rearRightTireBaselinePressure != null && rearRightTireBaselinePressure != "") { TireData.rearRightTireBaselinePressure = double.Parse(rearRightTireBaselinePressure); }
            if (rearRightTireFillMaterial != null && rearRightTireFillMaterial != "") { TireData.rearRightTireFillMaterial = rearRightTireFillMaterial; }
            if (rearRightTireTreadDepth != null && rearRightTireTreadDepth != "") { TireData.rearRightTireTreadDepth = int.Parse(rearRightTireTreadDepth); }

            //Front Left Sensor
            if (frontLeftSensorID != null && frontLeftSensorID != "") { SensorData.frontLeftSensorID = frontLeftSensorID; }
            if (frontLeftSensorTemp != null && frontLeftSensorTemp != "") { SensorData.frontLeftSensorTemperature = int.Parse(frontLeftSensorTemp); }
            if (frontLeftSensorPressure != null && frontLeftSensorPressure != "") { SensorData.frontLeftSensorPressure = double.Parse(frontLeftSensorPressure); }
            if (frontLeftSensorStatus != null && frontLeftSensorStatus != "") { SensorData.frontLeftSensorStatus = frontLeftSensorStatus; }

            //Front Right Sensor
            if (frontRightSensorID != null && frontRightSensorID != "") { SensorData.frontRightSensorID = frontRightSensorID; }
            if (frontRightSensorTemp != null && frontRightSensorTemp != "") { SensorData.frontRightSensorTemperature = int.Parse(frontRightSensorTemp); }
            if (frontRightSensorPressure != null && frontRightSensorPressure != "") { SensorData.frontRightSensorPressure = double.Parse(frontRightSensorPressure); }
            if (frontRightSensorStatus != null && frontRightSensorStatus != "") { SensorData.frontRightSensorStatus = frontRightSensorStatus; }

            //Rear Left Sensor
            if (rearLeftSensorID != null && rearLeftSensorID != "") { SensorData.rearLeftSensorID = rearLeftSensorID; }
            if (rearLeftSensorTemp != null && rearLeftSensorTemp != "") { SensorData.rearLeftSensorTemperature = int.Parse(rearLeftSensorTemp); }
            if (rearLeftSensorPressure != null && rearLeftSensorPressure != "") { SensorData.rearLeftSensorPressure = double.Parse(rearLeftSensorPressure); }
            if (rearLeftSensorStatus != null && rearLeftSensorStatus != "") { SensorData.rearLeftSensorStatus = rearLeftSensorStatus; }

            //Rear Right Sensor
            if (rearRightSensorID != null && rearRightSensorID != "") { SensorData.rearRightSensorID = rearRightSensorID; }
            if (rearRightSensorTemp != null && rearRightSensorTemp != "") { SensorData.rearRightSensorTemperature = int.Parse(rearRightSensorTemp); }
            if (rearRightSensorPressure != null && rearRightSensorPressure != "") { SensorData.rearRightSensorPressure = double.Parse(rearRightSensorPressure); }
            if (rearRightSensorStatus != null && rearRightSensorStatus != "") { SensorData.rearRightSensorStatus = rearRightSensorStatus; }

            //Sends a message that will notify MainPage to update the DisplayValues.
            MessagingCenter.Send<object>(this, "UpdateMainPageDisplayValues");

        }
    }
}
