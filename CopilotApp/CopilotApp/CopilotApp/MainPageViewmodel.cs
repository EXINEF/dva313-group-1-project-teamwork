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
        
        //Copies of the fields in the XAML because apparently grabbing data from code behind is considered bad practice.
        //So we'll just keep automatically updated copies here for now.
        string _tireID = string.Empty;
        string _tireTemperature = string.Empty;
        string _tirePressure = string.Empty;
        string _tot = string.Empty;
        public string tireID { get => _tireID; set { _tireID = value; } }
        public string tirePressure { get => _tirePressure; set { _tirePressure = value; } }
        public string tireTemperature { get => _tireTemperature; set { _tireTemperature = value; } }


        //Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewmodel()
        {
            //Binds the "TireFrontLeftPressedCommand" called in MainPage.xaml to the TireFrontLeftPressed() C# in this class
            TireFrontLeftPressedCommand = new Command(TireFrontLeftPressed);
            TireFrontRightPressedCommand = new Command(TireFrontRightPressed);
            TireBackLeftPressedCommand = new Command(TireBackLeftPressed);
            TireBackRightPressedCommand = new Command(TireBackRightPressed);
            SimulatorButtonPressedCommand = new Command(SimulatorButtonPressed);
            TKPHCalculations.LoadK1Data();
    }


        //New command. The command we call from the xaml code (Command="{Binding TireFrontLeftPressedCommand}).
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

    }
}
