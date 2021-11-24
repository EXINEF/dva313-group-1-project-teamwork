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
        //So we'll just keep automatically updated copies here.
        string _tireID = string.Empty;
        string _tireTemperature = string.Empty;
        string _tirePressure = string.Empty;
        string _tot = string.Empty;
        public string tireID { get => _tireID; set { _tireID = value; OnPropertyChanged(nameof(tot)); } }
        public string tirePressure { get => _tirePressure; set { _tirePressure = value; OnPropertyChanged(nameof(tot)); } }
        public string tireTemperature { get => _tireTemperature; set { _tireTemperature = value; OnPropertyChanged(nameof(tot)); } }
        public string tot { get => tireID + ", " + tirePressure + ", " + tireTemperature ; set { _tot = value; } }


        //Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        //New command
        public ICommand OnClickSendDataButton;

        public MainPageViewmodel()
        {
            //Binds "OnClickSendDataButton" which is called by the button in XAML to the "SendData" function in this class.
            OnClickSendDataButton = new Command(SendData);
        }

        void OnPropertyChanged(string name)
        {
            //Some property changed ?? Eventmagic
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Calls the Database.cs class and uploads the data.
        void SendData()
        {
            Database.SendData(tireID, tirePressure, tireTemperature);
        }

    }
}
