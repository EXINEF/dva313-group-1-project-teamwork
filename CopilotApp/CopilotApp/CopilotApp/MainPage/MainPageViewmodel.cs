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
        static string _frontLeftTireTemperatureDisplayValue; public string frontLeftTireTemperatureDisplayValue { get => _frontLeftTireTemperatureDisplayValue; set { _frontLeftTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(frontLeftTireTemperatureDisplayValue)); } }
        static string _frontLeftTirePressureDisplayValue; public string frontLeftTirePressureDisplayValue { get => _frontLeftTirePressureDisplayValue; set { _frontLeftTirePressureDisplayValue = value; OnPropertyChanged(nameof(frontLeftTirePressureDisplayValue)); } }
        static string _frontRightTireTemperatureDisplayValue; public string frontRightTireTemperatureDisplayValue { get => _frontRightTireTemperatureDisplayValue; set { _frontRightTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(frontRightTireTemperatureDisplayValue)); } }
        static string _frontRightTirePressureDisplayValue; public string frontRightTirePressureDisplayValue { get => _frontRightTirePressureDisplayValue; set { _frontRightTirePressureDisplayValue = value; OnPropertyChanged(nameof(frontRightTirePressureDisplayValue)); } }
        static string _rearLeftTireTemperatureDisplayValue; public string rearLeftTireTemperatureDisplayValue { get => _rearLeftTireTemperatureDisplayValue; set { _rearLeftTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(rearLeftTireTemperatureDisplayValue)); } }
        static string _rearLeftTirePressureDisplayValue; public string rearLeftTirePressureDisplayValue { get => _rearLeftTirePressureDisplayValue; set { _rearLeftTirePressureDisplayValue = value; OnPropertyChanged(nameof(rearLeftTirePressureDisplayValue)); } }
        static string _rearRightTireTemperatureDisplayValue; public string rearRightTireTemperatureDisplayValue { get => _rearRightTireTemperatureDisplayValue; set { _rearRightTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(rearRightTireTemperatureDisplayValue)); } }
        static string _rearRightTirePressureDisplayValue; public string rearRightTirePressureDisplayValue { get => _rearRightTirePressureDisplayValue; set { _rearRightTirePressureDisplayValue = value; OnPropertyChanged(nameof(rearRightTirePressureDisplayValue)); } }


        //Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewmodel()
        {
            //Binds the "TireFrontLeftPressedCommand" called from MainPage.xaml to the TireFrontLeftPressed() C# in this class
            TireFrontLeftPressedCommand = new Command(TireFrontLeftPressed);
            TireFrontRightPressedCommand = new Command(TireFrontRightPressed);
            TireBackLeftPressedCommand = new Command(TireBackLeftPressed);
            TireBackRightPressedCommand = new Command(TireBackRightPressed);
            SimulatorButtonPressedCommand = new Command(SimulatorButtonPressed);
            TKPHCalculations.LoadK1Data();
    }


        //The command we call from the xaml code (Command="{Binding TireFrontLeftPressedCommand}).
        public ICommand TireFrontLeftPressedCommand { get; }
        public ICommand TireFrontRightPressedCommand { get; }
        public ICommand TireBackLeftPressedCommand { get; }
        public ICommand TireBackRightPressedCommand { get; }
        public ICommand SimulatorButtonPressedCommand { get; }

        void OnPropertyChanged(string name)
        {
            //Some property changed send the event hanlder the name of the property.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void UpdateDisplayValues()
        {

        }

        //Tire button actions, code to execute when a tire button is pressed.
        async void TireFrontLeftPressed()
        {
            Console.WriteLine("Tire Front Left Button Pressed");
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void TireFrontRightPressed()
        {
            Console.WriteLine("Tire Front Right Button Pressed");
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void TireBackLeftPressed()
        {
            Console.WriteLine("Tire Back Left Button Pressed");
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void TireBackRightPressed()
        {
            Console.WriteLine("Tire Back Right Button Pressed");
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void SimulatorButtonPressed()
        {
            Console.WriteLine("Simulator Button Pressed");
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
