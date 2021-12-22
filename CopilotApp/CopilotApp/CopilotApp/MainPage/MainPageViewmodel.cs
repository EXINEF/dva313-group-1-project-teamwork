using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CopilotApp
{
    public class MainPageViewmodel : INotifyPropertyChanged
    {

        //These variables are tied to the temperature and pressure labels in the MainPage.xaml and is what is being displayed.
        string _frontLeftTireTemperatureDisplayValue; public string frontLeftTireTemperatureDisplayValue { get => _frontLeftTireTemperatureDisplayValue; set { _frontLeftTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(frontLeftTireTemperatureDisplayValue)); } }
        string _frontLeftTirePressureDisplayValue; public string frontLeftTirePressureDisplayValue { get => _frontLeftTirePressureDisplayValue; set { _frontLeftTirePressureDisplayValue = value; OnPropertyChanged(nameof(frontLeftTirePressureDisplayValue)); } }
        string _frontRightTireTemperatureDisplayValue; public string frontRightTireTemperatureDisplayValue { get => _frontRightTireTemperatureDisplayValue; set { _frontRightTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(frontRightTireTemperatureDisplayValue)); } }
        string _frontRightTirePressureDisplayValue; public string frontRightTirePressureDisplayValue { get => _frontRightTirePressureDisplayValue; set { _frontRightTirePressureDisplayValue = value; OnPropertyChanged(nameof(frontRightTirePressureDisplayValue)); } }
        string _rearLeftTireTemperatureDisplayValue; public string rearLeftTireTemperatureDisplayValue { get => _rearLeftTireTemperatureDisplayValue; set { _rearLeftTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(rearLeftTireTemperatureDisplayValue)); } }
        string _rearLeftTirePressureDisplayValue; public string rearLeftTirePressureDisplayValue { get => _rearLeftTirePressureDisplayValue; set { _rearLeftTirePressureDisplayValue = value; OnPropertyChanged(nameof(rearLeftTirePressureDisplayValue)); } }
        string _rearRightTireTemperatureDisplayValue; public string rearRightTireTemperatureDisplayValue { get => _rearRightTireTemperatureDisplayValue; set { _rearRightTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(rearRightTireTemperatureDisplayValue)); } }
        string _rearRightTirePressureDisplayValue; public string rearRightTirePressureDisplayValue { get => _rearRightTirePressureDisplayValue; set { _rearRightTirePressureDisplayValue = value; OnPropertyChanged(nameof(rearRightTirePressureDisplayValue)); } }

        //Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewmodel()
        {
            //Binds the "TireFrontLeftPressedCommand" called from MainPage.xaml to the TireFrontLeftPressed() C# in this class
            TireFrontLeftPressedCommand = new Command(TireFrontLeftPressed);
            TireFrontRightPressedCommand = new Command(TireFrontRightPressed);
            TireRearLeftPressedCommand = new Command(TireRearLeftPressed);
            TireRearRightPressedCommand = new Command(TireRearRightPressed);
            SimulatorButtonPressedCommand = new Command(SimulatorButtonPressed);
            TKPHCalculations.LoadK1Data();

            //Subscribe to messaging so that other pages can tell us to update our displayvalues.
            MessagingCenter.Subscribe<object>(this, "UpdateMainPageDisplayValues",  (sender) => { UpdateDisplayValues(); } );
        }

        //The command we call from the xaml code (Command="{Binding TireFrontLeftPressedCommand}).
        public ICommand TireFrontLeftPressedCommand { get; }
        public ICommand TireFrontRightPressedCommand { get; }
        public ICommand TireRearLeftPressedCommand { get; }
        public ICommand TireRearRightPressedCommand { get; }
        public ICommand SimulatorButtonPressedCommand { get; }

        protected void OnPropertyChanged(string name)
        {
            //Some property changed send the event hanlder the name of the property.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void UpdateDisplayValues()
        {
            UpdateFrontLeftTireGraphics();
            UpdateFrontRightTireGraphics();
            UpdateRearLeftTireGraphics();
            UpdateRearRightTireGraphics();

            frontLeftTireTemperatureDisplayValue = SensorData.frontLeftSensorTemperature.ToString() + " °C";
            frontLeftTirePressureDisplayValue = SensorData.frontLeftSensorPressure.ToString() + " kPa";
            frontRightTireTemperatureDisplayValue = SensorData.frontRightSensorTemperature.ToString() + " °C";
            frontRightTirePressureDisplayValue = SensorData.frontRightSensorPressure.ToString() + " kPa";
            rearLeftTireTemperatureDisplayValue = SensorData.rearLeftSensorTemperature.ToString() + " °C";
            rearLeftTirePressureDisplayValue = SensorData.rearLeftSensorPressure.ToString() + " kPa";
            rearRightTireTemperatureDisplayValue = SensorData.rearRightSensorTemperature.ToString() + " °C";
            rearRightTirePressureDisplayValue = SensorData.rearRightSensorPressure.ToString() + " kPa";
        }


        //Tire button actions, code to execute when a tire button is pressed.
        async void TireFrontLeftPressed()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void TireFrontRightPressed()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void TireRearLeftPressed()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void TireRearRightPressed()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void SimulatorButtonPressed()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SimulatorPage());
        }

        private void UpdateFrontLeftTireGraphics()
        {

        }
        private void UpdateFrontRightTireGraphics()
        {

        }
        private void UpdateRearLeftTireGraphics()
        {

        }
        private void UpdateRearRightTireGraphics()
        {

        }

    }
}
