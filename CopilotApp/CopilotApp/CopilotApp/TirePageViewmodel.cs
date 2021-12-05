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
            //Binds "OKCommand" which is called by a button in XAML to the "OKButtonPressed" function in this class.
            OKCommand = new Command(OKButtonPressed);
            CancelCommand = new Command(CancelButtonPressed);
        }


        //New command. The thing we call from the xaml code.
        public ICommand OKCommand { get; }
        public ICommand CancelCommand { get; }

        void OnPropertyChanged(string name)
        {
            //Some property changed send the event hanlder the name of the property
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Calls the Database.cs class with the data as arguments.
        void OKButtonPressed()
        {
            //Database.SendData(tireID, tirePressure, tireTemperature);
            ReturnToMainPage();
        }

        void CancelButtonPressed()
        {
            ReturnToMainPage();
        }

        async void ReturnToMainPage()
        {
            //The pages work as a stack, when we press a tire button on the MainPage a TirePage is pushed on the stack
            //This pops the tire page from the stack making MainPage the top page again.
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}