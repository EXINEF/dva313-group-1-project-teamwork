using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CopilotApp
{
    public partial class TirePageViewmodel : INotifyPropertyChanged
    {
        //Copies of the fields in the XAML because apparently grabbing data from code behind is considered bad practice.
        //So we'll just keep automatically updated copies here for now.
        string _tireID = string.Empty;
        string _baselinePressure;
        string _fillMaterial;
        string _treadDepth;

        string _tot = string.Empty;
        public string tireID { get => _tireID; set { _tireID = value; } }
        public string baselinePressure{ get => _baselinePressure; set { _baselinePressure = value; } }
        public string fillMaterial { get => _fillMaterial; set { _fillMaterial = value; } }
        public string treadDepth { get => _treadDepth; set { _treadDepth = value; } }


        //Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        public TirePageViewmodel()
        {
            //Binds "SendDataCommand" which is called by the button in XAML to the "SendData" function in this class.
            SendDataCommand = new Command(SendData);
        }


        //New command. The thing we call from the xaml code.
        public ICommand SendDataCommand { get; }

        void OnPropertyChanged(string name)
        {
            //Some property changed send the event hanlder the name of the property
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Calls the Database.cs class with the data as arguments.
        void SendData()
        {
            //Database.SendData(tireID, tirePressure, tireTemperature);
        }
    }
}