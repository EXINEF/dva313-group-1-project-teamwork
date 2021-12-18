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
        }

        //New command. The command we call from the xaml code (Command="{Binding BackToOverviewButtonPressedCommand}).
        public ICommand BackToMainPageButtonPressedCommand { get; }
        public ICommand SendMachineDataButtonPressedCommand { get; }
        public ICommand SendSensorDataButtonPressedCommand { get; }
        public ICommand SendTireDataButtonPressedCommand { get; }

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
            SendLocationDataToDatabase();
        }

        void SendSensorData()
        {
            SendSensorDataToDatabase();
        }

        void SendTireData()
        {
            SendTireDataToDatabase();
        }
    }
}
