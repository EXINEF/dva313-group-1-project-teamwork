using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CopilotApp
{
    public partial class MainPageViewmodel : INotifyPropertyChanged
    {
        //Copies of the fields in the XAML because apparently grabbing data from code behind is considered bad practice.
        //So we'll just keep automatically updated copies here for now.
        string _tireID = string.Empty;
        string _tireTemperature = string.Empty;
        string _tirePressure = string.Empty;
        string _tot = string.Empty;
        public string tireID { get => _tireID; set { _tireID = value; OnPropertyChanged(nameof(tot)); } }
        public string tirePressure { get => _tirePressure; set { _tirePressure = value; OnPropertyChanged(nameof(tot)); } }
        public string tireTemperature { get => _tireTemperature; set { _tireTemperature = value; OnPropertyChanged(nameof(tot)); } }
        public string tot { get => tireID + ", " + tirePressure + ", " + tireTemperature ; set { _tot = value; } } //This is just used to verify that the other values gets updated


        //Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewmodel()
        {
            //Binds "SendDataCommand" which is called by the button in XAML to the "SendData" function in this class.
            SendDataCommand = new Command(SendData);
        }


        //New command
        public ICommand SendDataCommand { get; }

        void OnPropertyChanged(string name)
        {
            //Some property changed send the event hanlder the name of the property
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Calls the Database.cs class with the data as arguments.
        void SendData()
        {
            Database.SendData(tireID, tirePressure, tireTemperature);
        }

    }
}
