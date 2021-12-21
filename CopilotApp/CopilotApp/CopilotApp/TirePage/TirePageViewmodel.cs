using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MySqlConnector;

namespace CopilotApp
{
    public class TirePageViewmodel : INotifyPropertyChanged
    {
        //Copies of the fields in the XAML because apparently grabbing data from code behind is considered bad practice.
        //So we'll just keep automatically updated copies here for now.

        public static string SelectedTire;
        string _tireID = string.Empty;
        string _baselinePressure;
        string _fillMaterial;
        string _treadDepth;

        public string tireID { get => _tireID; set { _tireID = value; OnPropertyChanged("tireID"); } }
        public string tireBaselinePressure{ get => _baselinePressure; set { _baselinePressure = value; OnPropertyChanged("tireBaselinePressure"); } }
        public string tireFillMaterial { get => _fillMaterial; set { _fillMaterial = value; OnPropertyChanged("tireFillMaterial"); } }
        public string tireTreadDepth { get => _treadDepth; set { _treadDepth = value; OnPropertyChanged("tireTreadDepth"); } }


        //Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        public TirePageViewmodel()
        {
            //Binds "OKCommand" which is called by a button in XAML to the "OKButtonPressed" function in this class.
            OKCommand = new Command(OKButtonPressed);
            CancelCommand = new Command(CancelButtonPressed);
            FetchTestCommand = new Command(FetchTireData);
        }


        //New command. The thing we call from the xaml code.
        public ICommand OKCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand FetchTestCommand { get; }

        void OnPropertyChanged(string name)
        {
            //Some property changed send the event hanlder the name of the property
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Calls the Database.cs class with the data as arguments.
        void OKButtonPressed()
        {
            Database.SendTireData(tireID, tireBaselinePressure, tireFillMaterial, tireTreadDepth);
            ReturnToMainPage();
        }

        void CancelButtonPressed()
        {
            ReturnToMainPage();
        }

        void FetchTireData()
        {
            string query = "SELECT baseline_pressure, fill_material, tread_depth FROM tpms_tire WHERE id = '1'";
            MySqlDataReader reader = (Database.SendQuery(query)).Result;

            if (reader != null)
            {
                while (reader.Read())
                {
                    Console.WriteLine("ID: 1, " + reader["baseline_pressure"] + ", " + reader["fill_material"] + ", " + reader["tread_depth"]);
                    tireID = "1";
                    tireBaselinePressure = reader["baseline_pressure"].ToString();
                    tireFillMaterial = reader["fill_material"].ToString();
                    tireTreadDepth = reader["tread_depth"].ToString();
                }
            }
        }

        async void ReturnToMainPage()
        {
            //The pages work as a stack, when we press a tire button on the MainPage a TirePage is pushed on the stack
            //This pops the tire page from the stack making MainPage the top page again.
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}