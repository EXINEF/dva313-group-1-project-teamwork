﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MySqlConnector;
using System.Threading.Tasks;

namespace CopilotApp
{
    public class TirePageViewmodel : INotifyPropertyChanged
    {
        //Copies of the fields in the XAML because apparently grabbing data from code behind is considered bad practice.
        //So we'll just keep automatically updated copies here for now.

        public enum TIRE_POSITION { FRONT_LEFT, FRONT_RIGHT, REAR_LEFT, REAR_RIGHT }
        public static TIRE_POSITION tirePosition;
        string _tireID = string.Empty;
        string _baselinePressure;
        string _fillMaterial;
        string _treadDepth;
        string _life;
        string _revolutions;

        public string tireIDDisplayValue { get => _tireID; set { _tireID = value; OnPropertyChanged("tireID"); } }
        public string tireBaselinePressureDisplayValue { get => _baselinePressure; set { _baselinePressure = value; OnPropertyChanged("tireBaselinePressure"); } }
        public string tireFillMaterialDisplayValue { get => _fillMaterial; set { _fillMaterial = value; OnPropertyChanged("tireFillMaterial"); } }
        public string tireTreadDepthDisplayValue { get => _treadDepth; set { _treadDepth = value; OnPropertyChanged("tireTreadDepth"); } }
        public string tireLifeDisplayValue { get => _life; set { _life = value; OnPropertyChanged("tireLife"); } }
        public string tireRevolutionsDisplayValue { get => _revolutions; set { _revolutions = value; OnPropertyChanged("tireRevolutions"); } }


        //Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        public TirePageViewmodel()
        {
            //Binds "OKCommand" which is called by a button in XAML to the "OKButtonPressed()" function in this class.
            OKCommand = new Command(OKButtonPressed);
            CancelCommand = new Command(CancelButtonPressed);
            LoadTireDisplayValues(tirePosition);
        }

        //New command. The thing we call from the xaml code.
        public ICommand OKCommand { get; }
        public ICommand CancelCommand { get; }

        void OnPropertyChanged(string name)
        {
            //Some property changed send the event hanlder the name of the property
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Updates the CoPilot and the database with the new values
        void OKButtonPressed()
        {
            Task.Run(async () => { await SendTireDataAsync(tireIDDisplayValue, tireBaselinePressureDisplayValue, tireFillMaterialDisplayValue, tireTreadDepthDisplayValue); });
            ReturnToMainPage();

            /*
            //SendTireDataAsync(tirePosition);
            int nrOfRowsAffected = DatabaseFunctions.SendTireData(tireIDDisplayValue, null, tireBaselinePressureDisplayValue, tireFillMaterialDisplayValue, tireTreadDepthDisplayValue, null, null, null);

            //Update machine data in the database and the internal values if the database was successfully updated so they match
            if (nrOfRowsAffected != -1)
            {
                switch (tirePosition)
                {
                    case TIRE_POSITION.FRONT_LEFT:
                        DatabaseFunctions.SendMachineData(MachineData.machineID, MachineData.machineType, null, null, null, null, null, null, null, null, tireIDDisplayValue, null, null, null);
                        TireData.frontLeftTireID = tireIDDisplayValue;
                        TireData.frontLeftTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                        TireData.frontLeftTireFillMaterial = tireFillMaterialDisplayValue;
                        TireData.frontLeftTireTreadDepth = double.Parse(tireTreadDepthDisplayValue);
                        break;
                    case TIRE_POSITION.FRONT_RIGHT:
                        DatabaseFunctions.SendMachineData(MachineData.machineID, MachineData.machineType, null, null, null, null, null, null, null, null, null, null, tireIDDisplayValue, null);
                        TireData.frontRightTireID = tireIDDisplayValue;
                        TireData.frontRightTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                        TireData.frontRightTireFillMaterial = tireFillMaterialDisplayValue;
                        TireData.frontRightTireTreadDepth = double.Parse(tireTreadDepthDisplayValue);
                        break;
                    case TIRE_POSITION.REAR_LEFT:
                        DatabaseFunctions.SendMachineData(MachineData.machineID, MachineData.machineType, null, null, null, null, null, null, null, null, null, tireIDDisplayValue, null, null);
                        TireData.rearLeftTireID = tireIDDisplayValue;
                        TireData.rearLeftTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                        TireData.rearLeftTireFillMaterial = tireFillMaterialDisplayValue;
                        TireData.rearLeftTireTreadDepth = double.Parse(tireTreadDepthDisplayValue);
                        break;
                    case TIRE_POSITION.REAR_RIGHT:
                        DatabaseFunctions.SendMachineData(MachineData.machineID, MachineData.machineType, null, null, null, null, null, null, null, null, null, null, null, tireIDDisplayValue);
                        TireData.rearRightTireID = tireIDDisplayValue;
                        TireData.rearRightTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                        TireData.rearRightTireFillMaterial = tireFillMaterialDisplayValue;
                        TireData.rearRightTireTreadDepth = double.Parse(tireTreadDepthDisplayValue);
                        break;
                    default:
                        break;

                }
            }
            */
            //ReturnToMainPage();

        }

        public static async Task SendTireDataAsync(string tireIDDisplayValue, string tireBaselinePressureDisplayValue, string tireFillMaterialDisplayValue, string tireTreadDepthDisplayValue)
        {

            int nrOfRowsAffected = DatabaseFunctions.SendTireData(tireIDDisplayValue, null, tireBaselinePressureDisplayValue, tireFillMaterialDisplayValue, tireTreadDepthDisplayValue, null, null, null);

            //If the database was successfully updated, then update the internal values as well.s
            if (nrOfRowsAffected != -1)
            {
                switch (tirePosition)
                {
                    case TIRE_POSITION.FRONT_LEFT:
                        DatabaseFunctions.SendMachineData(MachineData.machineID, MachineData.machineType, null, null, null, null, null, null, null, null, tireIDDisplayValue, null, null, null, null);
                        TireData.frontLeftTireID = tireIDDisplayValue;
                        TireData.frontLeftTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                        TireData.frontLeftTireFillMaterial = tireFillMaterialDisplayValue;
                        TireData.frontLeftTireTreadDepth = double.Parse(tireTreadDepthDisplayValue);
                        break;
                    case TIRE_POSITION.FRONT_RIGHT:
                        DatabaseFunctions.SendMachineData(MachineData.machineID, MachineData.machineType, null, null, null, null, null, null, null, null, null, null, tireIDDisplayValue, null, null);
                        TireData.frontRightTireID = tireIDDisplayValue;
                        TireData.frontRightTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                        TireData.frontRightTireFillMaterial = tireFillMaterialDisplayValue;
                        TireData.frontRightTireTreadDepth = double.Parse(tireTreadDepthDisplayValue);
                        break;
                    case TIRE_POSITION.REAR_LEFT:
                        DatabaseFunctions.SendMachineData(MachineData.machineID, MachineData.machineType, null, null, null, null, null, null, null, null, null, tireIDDisplayValue, null, null, null);
                        TireData.rearLeftTireID = tireIDDisplayValue;
                        TireData.rearLeftTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                        TireData.rearLeftTireFillMaterial = tireFillMaterialDisplayValue;
                        TireData.rearLeftTireTreadDepth = double.Parse(tireTreadDepthDisplayValue);
                        break;
                    case TIRE_POSITION.REAR_RIGHT:
                        DatabaseFunctions.SendMachineData(MachineData.machineID, MachineData.machineType, null, null, null, null, null, null, null, null, null, null, null, tireIDDisplayValue, null);
                        TireData.rearRightTireID = tireIDDisplayValue;
                        TireData.rearRightTireBaselinePressure = double.Parse(tireBaselinePressureDisplayValue);
                        TireData.rearRightTireFillMaterial = tireFillMaterialDisplayValue;
                        TireData.rearRightTireTreadDepth = double.Parse(tireTreadDepthDisplayValue);
                        break;
                    default:
                        break;
                }
            }

            await Task.CompletedTask;
        }

        void CancelButtonPressed()
        {
            ReturnToMainPage();
        }


        //Grab the Live Data and set it as the display values for the tire page.
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

        async void ReturnToMainPage()
        {
            //The pages work as a stack, when we press a tire button on the MainPage a TirePage is pushed on the stack
            //This pops the tire page from the stack making MainPage the top page again.
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}