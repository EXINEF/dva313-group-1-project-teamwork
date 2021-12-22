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

        public enum TIRE_POSITION { FRONT_LEFT, FRONT_RIGHT, REAR_LEFT, REAR_RIGHT }
        TIRE_POSITION _tirePosition;
        string _tireID = string.Empty;
        string _baselinePressure;
        string _fillMaterial;
        string _treadDepth;

        public string tireIDDisplayValue { get => _tireID; set { _tireID = value; OnPropertyChanged("tireID"); } }
        public string tireBaselinePressureDisplayValue { get => _baselinePressure; set { _baselinePressure = value; OnPropertyChanged("tireBaselinePressure"); } }
        public string tireFillMaterialDisplayValue { get => _fillMaterial; set { _fillMaterial = value; OnPropertyChanged("tireFillMaterial"); } }
        public string tireTreadDepthDisplayValue { get => _treadDepth; set { _treadDepth = value; OnPropertyChanged("tireTreadDepth"); } }


        //Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        public TirePageViewmodel()
        {
            //Binds "OKCommand" which is called by a button in XAML to the "OKButtonPressed" function in this class.
            OKCommand = new Command(OKButtonPressed);
            CancelCommand = new Command(CancelButtonPressed);
            //FetchTestCommand = new Command(FetchTireData);
            //LoadTireDisplayValues(tirePosition);
        }

        public TirePageViewmodel(TIRE_POSITION tirePosition)
        {
            //Binds "OKCommand" which is called by a button in XAML to the "OKButtonPressed" function in this class.
            OKCommand = new Command(OKButtonPressed);
            CancelCommand = new Command(CancelButtonPressed);
            //FetchTestCommand = new Command(FetchTireData);
            _tirePosition = tirePosition;
            LoadTireDisplayValues(tirePosition);
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
            //Update internal values of the CoPilot
            switch (_tirePosition)
            {
                case TIRE_POSITION.FRONT_LEFT:
                    TireData.frontLeftTireID = tireIDDisplayValue;
                    TireData.frontLeftTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                    TireData.frontLeftTireFillMaterial = tireFillMaterialDisplayValue;
                    TireData.frontLeftTireTreadDepth = int.Parse(tireTreadDepthDisplayValue);
                    break;
                case TIRE_POSITION.FRONT_RIGHT:
                    TireData.frontRightTireID = tireIDDisplayValue;
                    TireData.frontRightTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                    TireData.frontRightTireFillMaterial = tireFillMaterialDisplayValue;
                    TireData.frontRightTireTreadDepth = int.Parse(tireTreadDepthDisplayValue);
                    break;
                case TIRE_POSITION.REAR_LEFT:
                    TireData.rearLeftTireID = tireIDDisplayValue;
                    TireData.rearLeftTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                    TireData.rearLeftTireFillMaterial = tireFillMaterialDisplayValue;
                    TireData.rearLeftTireTreadDepth = int.Parse(tireTreadDepthDisplayValue);
                    break;
                case TIRE_POSITION.REAR_RIGHT:
                    TireData.rearRightTireID = tireIDDisplayValue;
                    TireData.rearRightTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                    TireData.rearRightTireFillMaterial = tireFillMaterialDisplayValue;
                    TireData.rearRightTireTreadDepth = int.Parse(tireTreadDepthDisplayValue);
                    break;
                default:
                    break;

            }

            //Update database values
            DatabaseFunctions.SendTireData(tireIDDisplayValue, tireBaselinePressureDisplayValue, tireFillMaterialDisplayValue, tireTreadDepthDisplayValue);
            ReturnToMainPage();
        }

        void CancelButtonPressed()
        {
            ReturnToMainPage();
        }

        private void LoadTireDisplayValues(TIRE_POSITION tirePosition)
        {
            switch (tirePosition) {
                case TIRE_POSITION.FRONT_LEFT:
                    tireIDDisplayValue = TireData.frontLeftTireID;
                    tireBaselinePressureDisplayValue = TireData.frontLeftTireBaselinePressure.ToString();
                    tireFillMaterialDisplayValue = TireData.frontLeftTireFillMaterial;
                    tireTreadDepthDisplayValue = TireData.frontLeftTireTreadDepth.ToString();
                    break;
                case TIRE_POSITION.FRONT_RIGHT:
                    tireIDDisplayValue = TireData.frontRightTireID;
                    tireBaselinePressureDisplayValue = TireData.frontRightTireBaselinePressure.ToString();
                    tireFillMaterialDisplayValue = TireData.frontRightTireFillMaterial;
                    tireTreadDepthDisplayValue = TireData.frontRightTireTreadDepth.ToString();
                    break;
                case TIRE_POSITION.REAR_LEFT:
                    tireIDDisplayValue = TireData.rearLeftTireID;
                    tireBaselinePressureDisplayValue = TireData.rearLeftTireBaselinePressure.ToString();
                    tireFillMaterialDisplayValue = TireData.rearLeftTireFillMaterial;
                    tireTreadDepthDisplayValue = TireData.rearLeftTireTreadDepth.ToString();
                    break;
                case TIRE_POSITION.REAR_RIGHT:
                    tireIDDisplayValue = TireData.rearRightTireID;
                    tireBaselinePressureDisplayValue = TireData.rearRightTireBaselinePressure.ToString();
                    tireFillMaterialDisplayValue = TireData.rearRightTireFillMaterial;
                    tireTreadDepthDisplayValue = TireData.rearRightTireTreadDepth.ToString();
                    break;
                default:
                    tireIDDisplayValue = "Not found";
                    tireBaselinePressureDisplayValue = "Not found";
                    tireFillMaterialDisplayValue = "Not found";
                    tireTreadDepthDisplayValue = "Not found";
                    break;
            }

        }


        /*
        void FetchTireData()
        {
            string query = "SELECT baseline_pressure, fill_material, tread_depth FROM tpms_tire WHERE id = '1'";
            MySqlDataReader reader = (Database.SendQuery(query)).Result;

            if (reader != null)
            {
                while (reader.Read())
                {
                    tireBaselinePressure = reader["baseline_pressure"].ToString();
                    tireFillMaterial = reader["fill_material"].ToString();
                    tireTreadDepth = reader["tread_depth"].ToString();
                }
            }
        }
        */

        async void ReturnToMainPage()
        {
            //The pages work as a stack, when we press a tire button on the MainPage a TirePage is pushed on the stack
            //This pops the tire page from the stack making MainPage the top page again.
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}