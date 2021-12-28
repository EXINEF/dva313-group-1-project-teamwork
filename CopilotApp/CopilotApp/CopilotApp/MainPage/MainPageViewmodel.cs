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
        public enum TEMPERATURE_STATUS { OK, HIGH, VERY_HIGH }
        public enum PRESSURE_STATUS { VERY_LOW, LOW, OK, HIGH, VERY_HIGH }


        //These variables are tied to the temperature and pressure labels in the MainPage.xaml and is what is being displayed.
        string _frontLeftTireTemperatureDisplayValue; public string frontLeftTireTemperatureDisplayValue { get => _frontLeftTireTemperatureDisplayValue; set { _frontLeftTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(frontLeftTireTemperatureDisplayValue)); } }
        string _frontLeftTirePressureDisplayValue; public string frontLeftTirePressureDisplayValue { get => _frontLeftTirePressureDisplayValue; set { _frontLeftTirePressureDisplayValue = value; OnPropertyChanged(nameof(frontLeftTirePressureDisplayValue)); } }
        string _frontRightTireTemperatureDisplayValue; public string frontRightTireTemperatureDisplayValue { get => _frontRightTireTemperatureDisplayValue; set { _frontRightTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(frontRightTireTemperatureDisplayValue)); } }
        string _frontRightTirePressureDisplayValue; public string frontRightTirePressureDisplayValue { get => _frontRightTirePressureDisplayValue; set { _frontRightTirePressureDisplayValue = value; OnPropertyChanged(nameof(frontRightTirePressureDisplayValue)); } }
        string _rearLeftTireTemperatureDisplayValue; public string rearLeftTireTemperatureDisplayValue { get => _rearLeftTireTemperatureDisplayValue; set { _rearLeftTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(rearLeftTireTemperatureDisplayValue)); } }
        string _rearLeftTirePressureDisplayValue; public string rearLeftTirePressureDisplayValue { get => _rearLeftTirePressureDisplayValue; set { _rearLeftTirePressureDisplayValue = value; OnPropertyChanged(nameof(rearLeftTirePressureDisplayValue)); } }
        string _rearRightTireTemperatureDisplayValue; public string rearRightTireTemperatureDisplayValue { get => _rearRightTireTemperatureDisplayValue; set { _rearRightTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(rearRightTireTemperatureDisplayValue)); } }
        string _rearRightTirePressureDisplayValue; public string rearRightTirePressureDisplayValue { get => _rearRightTirePressureDisplayValue; set { _rearRightTirePressureDisplayValue = value; OnPropertyChanged(nameof(rearRightTirePressureDisplayValue)); } }

        //Event handler
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewmodel()
        {
            //Binds the "TireFrontLeftPressedCommand" called from MainPage.xaml to the TireFrontLeftPressed() C# in this class
            TireFrontLeftPressedCommand = new Command(TireFrontLeftPressed);
            TireFrontRightPressedCommand = new Command(TireFrontRightPressed);
            TireRearLeftPressedCommand = new Command(TireRearLeftPressed);
            TireRearRightPressedCommand = new Command(TireRearRightPressed);
            SimulatorButtonPressedCommand = new Command(SimulatorButtonPressed);
            DismissNotificationCommand = new Command(DismissNotification);
            TestNotificationCommand = new Command(TestNotification);
            TKPHCalculations.LoadK1Data();
            /*having trouble with getting values from database. Thus it is commented out*/
            //Calculations calc = new Calculations();
            //calc.run(); 
            //Subscribe to messaging so that other pages can tell us to update our displayvalues.
            MessagingCenter.Subscribe<object>(this, "UpdateMainPageDisplayValues", (sender) => { UpdateDisplay(); } );

            //Subscribe to receive notification using MessagingCenter use MainPageViewmodel.PushNotification("hello world"); to push a notification from anywhere.
            MessagingCenter.Subscribe<Xamarin.Forms.Application, string> (Xamarin.Forms.Application.Current, "NewNotification", (sender, message) => {
                AddNotification(message);
            });

            //Default images, needed because otherwise no images wont show when changed.
            frontLeftTireImage = ImageSourceTireDefault;
            frontRightTireImage = ImageSourceTireDefault;
            rearLeftTireImage = ImageSourceTireDefault;
            rearRightTireImage = ImageSourceTireDefault;

            frontLeftTireLine = ImageSourceGreenUpperTireLine;
            frontLeftTireUpperBranch = ImageSourceGreenUpperBranch;
            frontLeftTireLowerBranch = ImageSourceGreenLowerBranch;

            frontRightTireLine = ImageSourceGreenUpperTireLine;
            frontRightTireUpperBranch = ImageSourceGreenUpperBranch;
            frontRightTireLowerBranch = ImageSourceGreenLowerBranch;

            rearLeftTireLine = ImageSourceGreenLowerTireLine;
            rearLeftTireUpperBranch = ImageSourceGreenUpperBranch;
            rearLeftTireLowerBranch = ImageSourceGreenLowerBranch;

            rearRightTireLine = ImageSourceGreenLowerTireLine;
            rearRightTireUpperBranch = ImageSourceGreenUpperBranch;
            rearRightTireLowerBranch = ImageSourceGreenLowerBranch;
        }

        //The command we call from the xaml code (Command="{Binding TireFrontLeftPressedCommand}).
        public ICommand TireFrontLeftPressedCommand { get; }
        public ICommand TireFrontRightPressedCommand { get; }
        public ICommand TireRearLeftPressedCommand { get; }
        public ICommand TireRearRightPressedCommand { get; }
        public ICommand SimulatorButtonPressedCommand { get; }
        public ICommand DismissNotificationCommand { get; }
        public ICommand TestNotificationCommand { get; }

        protected void OnPropertyChanged(string name)
        {
            //Some property changed send the event hanlder the name of the property.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void TestNotification()
        {
            PushNotification("Just some text to try the notification system");
        }

        //Pull values from the LiveData and update the graphics
        public void UpdateDisplay()
        {
            frontLeftTireTemperatureDisplayValue = SensorData.frontLeftSensorTemperature.ToString() + " °C";
            frontLeftTirePressureDisplayValue = SensorData.frontLeftSensorPressure.ToString() + " kPa";
            frontRightTireTemperatureDisplayValue = SensorData.frontRightSensorTemperature.ToString() + " °C";
            frontRightTirePressureDisplayValue = SensorData.frontRightSensorPressure.ToString() + " kPa";
            rearLeftTireTemperatureDisplayValue = SensorData.rearLeftSensorTemperature.ToString() + " °C";
            rearLeftTirePressureDisplayValue = SensorData.rearLeftSensorPressure.ToString() + " kPa";
            rearRightTireTemperatureDisplayValue = SensorData.rearRightSensorTemperature.ToString() + " °C";
            rearRightTirePressureDisplayValue = SensorData.rearRightSensorPressure.ToString() + " kPa";

            UpdateFrontLeftTireGraphics();
            UpdateFrontRightTireGraphics();
            UpdateRearLeftTireGraphics();
            UpdateRearRightTireGraphics();
        }


        //Tire button actions, code to execute when a tire button is pressed.
        async void TireFrontLeftPressed()
        {
            TirePageViewmodel.tirePosition = TirePageViewmodel.TIRE_POSITION.FRONT_LEFT;
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void TireFrontRightPressed()
        {
            TirePageViewmodel.tirePosition = TirePageViewmodel.TIRE_POSITION.FRONT_RIGHT;
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void TireRearLeftPressed()
        {
            TirePageViewmodel.tirePosition = TirePageViewmodel.TIRE_POSITION.REAR_LEFT;
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void TireRearRightPressed()
        {
            TirePageViewmodel.tirePosition = TirePageViewmodel.TIRE_POSITION.REAR_RIGHT;
            await Application.Current.MainPage.Navigation.PushAsync(new TirePage());
        }
        async void SimulatorButtonPressed()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SimulatorPage());
        }

        private void UpdateFrontLeftTireGraphics()
        {
            TEMPERATURE_STATUS tempStatus = GetTemperatureStatus(SensorData.frontLeftSensorTemperature);
            PRESSURE_STATUS pressureStatus = GetPressureStatus(SensorData.frontLeftSensorPressure, TireData.frontLeftTireBaselinePressure);

            //Temperature / Lower Branch
            if(tempStatus == TEMPERATURE_STATUS.OK)
            {
                frontLeftTireLowerBranch = ImageSourceGreenLowerBranch;
            }
            else if (tempStatus == TEMPERATURE_STATUS.HIGH)
            {
                frontLeftTireLowerBranch = ImageSourceYellowLowerBranch;
            }
            else
            {
                frontLeftTireLowerBranch = ImageSourceRedLowerBranch;
            }

            //Pressure / Upper Branch
            if (pressureStatus == PRESSURE_STATUS.OK)
            {
                frontLeftTireUpperBranch = ImageSourceGreenUpperBranch;
            }
            else if (pressureStatus == PRESSURE_STATUS.LOW || pressureStatus == PRESSURE_STATUS.HIGH)
            {
                frontLeftTireUpperBranch = ImageSourceYellowUpperBranch;
            }
            else
            {
                frontLeftTireUpperBranch = ImageSourceRedUpperBranch;
            }

            //Tire and main Tire Line / Circle Indicator
            if(tempStatus == TEMPERATURE_STATUS.OK && pressureStatus == PRESSURE_STATUS.OK)
            {
                frontLeftTireImage = ImageSourceTireDefault;
                frontLeftTireLine = ImageSourceGreenUpperTireLine;
            }
            else if(tempStatus == TEMPERATURE_STATUS.VERY_HIGH || pressureStatus == PRESSURE_STATUS.VERY_HIGH || pressureStatus == PRESSURE_STATUS.VERY_LOW)
            {
                frontLeftTireImage = ImageSourceTireRed;
                frontLeftTireLine = ImageSourceRedUpperTireLine;
            }
            else
            {
                frontLeftTireImage = ImageSourceTireYellow;
                frontLeftTireLine = ImageSourceYellowUpperTireLine;
            }

        }
        private void UpdateFrontRightTireGraphics()
        {
            TEMPERATURE_STATUS tempStatus = GetTemperatureStatus(SensorData.frontRightSensorTemperature);
            PRESSURE_STATUS pressureStatus = GetPressureStatus(SensorData.frontRightSensorPressure, TireData.frontRightTireBaselinePressure);

            //Temperature / Lower Branch
            if (tempStatus == TEMPERATURE_STATUS.OK)
            {
                frontRightTireLowerBranch = ImageSourceGreenLowerBranch;
            }
            else if (tempStatus == TEMPERATURE_STATUS.HIGH)
            {
                frontRightTireLowerBranch = ImageSourceYellowLowerBranch;
            }
            else
            {
                frontRightTireLowerBranch = ImageSourceRedLowerBranch;
            }

            //Pressure / Upper Branch
            if (pressureStatus == PRESSURE_STATUS.OK)
            {
                frontRightTireUpperBranch = ImageSourceGreenUpperBranch;
            }
            else if (pressureStatus == PRESSURE_STATUS.LOW || pressureStatus == PRESSURE_STATUS.HIGH)
            {
                frontRightTireUpperBranch = ImageSourceYellowUpperBranch;
            }
            else
            {
                frontRightTireUpperBranch = ImageSourceRedUpperBranch;
            }

            //Tire and main Tire Line / Circle Indicator
            if (tempStatus == TEMPERATURE_STATUS.OK && pressureStatus == PRESSURE_STATUS.OK)
            {
                frontRightTireImage = ImageSourceTireDefault;
                frontRightTireLine = ImageSourceGreenUpperTireLine;
            }
            else if (tempStatus == TEMPERATURE_STATUS.VERY_HIGH || pressureStatus == PRESSURE_STATUS.VERY_HIGH || pressureStatus == PRESSURE_STATUS.VERY_LOW)
            {
                frontRightTireImage = ImageSourceTireRed;
                frontRightTireLine = ImageSourceRedUpperTireLine;
            }
            else
            {
                frontRightTireImage = ImageSourceTireYellow;
                frontRightTireLine = ImageSourceYellowUpperTireLine;
            }
        }
        private void UpdateRearLeftTireGraphics()
        {
            TEMPERATURE_STATUS tempStatus = GetTemperatureStatus(SensorData.rearLeftSensorTemperature);
            PRESSURE_STATUS pressureStatus = GetPressureStatus(SensorData.rearLeftSensorPressure, TireData.rearLeftTireBaselinePressure);

            //Temperature / Lower Branch
            if (tempStatus == TEMPERATURE_STATUS.OK)
            {
                rearLeftTireLowerBranch = ImageSourceGreenLowerBranch;
            }
            else if (tempStatus == TEMPERATURE_STATUS.HIGH)
            {
                rearLeftTireLowerBranch = ImageSourceYellowLowerBranch;
            }
            else
            {
                rearLeftTireLowerBranch = ImageSourceRedLowerBranch;
            }

            //Pressure / Upper Branch
            if (pressureStatus == PRESSURE_STATUS.OK)
            {
                rearLeftTireUpperBranch = ImageSourceGreenUpperBranch;
            }
            else if (pressureStatus == PRESSURE_STATUS.LOW || pressureStatus == PRESSURE_STATUS.HIGH)
            {
                rearLeftTireUpperBranch = ImageSourceYellowUpperBranch;
            }
            else
            {
                rearLeftTireUpperBranch = ImageSourceRedUpperBranch;
            }

            //Tire and main Tire Line / Circle Indicator
            if (tempStatus == TEMPERATURE_STATUS.OK && pressureStatus == PRESSURE_STATUS.OK)
            {
                rearLeftTireImage = ImageSourceTireDefault;
                rearLeftTireLine = ImageSourceGreenLowerTireLine;
            }
            else if (tempStatus == TEMPERATURE_STATUS.VERY_HIGH || pressureStatus == PRESSURE_STATUS.VERY_HIGH || pressureStatus == PRESSURE_STATUS.VERY_LOW)
            {
                rearLeftTireImage = ImageSourceTireRed;
                rearLeftTireLine = ImageSourceRedLowerTireLine;
            }
            else
            {
                rearLeftTireImage = ImageSourceTireYellow;
                rearLeftTireLine = ImageSourceYellowLowerTireLine;
            }
        }
        private void UpdateRearRightTireGraphics()
        {
            TEMPERATURE_STATUS tempStatus = GetTemperatureStatus(SensorData.rearRightSensorTemperature);
            PRESSURE_STATUS pressureStatus = GetPressureStatus(SensorData.rearRightSensorPressure, TireData.rearRightTireBaselinePressure);

            //Temperature / Lower Branch
            if (tempStatus == TEMPERATURE_STATUS.OK)
            {
                rearRightTireLowerBranch = ImageSourceGreenLowerBranch;
            }
            else if (tempStatus == TEMPERATURE_STATUS.HIGH)
            {
                rearRightTireLowerBranch = ImageSourceYellowLowerBranch;
            }
            else
            {
                rearRightTireLowerBranch = ImageSourceRedLowerBranch;
            }

            //Pressure / Upper Branch
            if (pressureStatus == PRESSURE_STATUS.OK)
            {
                rearRightTireUpperBranch = ImageSourceGreenUpperBranch;
            }
            else if (pressureStatus == PRESSURE_STATUS.LOW || pressureStatus == PRESSURE_STATUS.HIGH)
            {
                rearRightTireUpperBranch = ImageSourceYellowUpperBranch;
            }
            else
            {
                rearRightTireUpperBranch = ImageSourceRedUpperBranch;
            }

            //Tire and main Tire Line / Circle Indicator
            if (tempStatus == TEMPERATURE_STATUS.OK && pressureStatus == PRESSURE_STATUS.OK)
            {
                rearRightTireImage = ImageSourceTireDefault;
                rearRightTireLine = ImageSourceGreenLowerTireLine;
            }
            else if (tempStatus == TEMPERATURE_STATUS.VERY_HIGH || pressureStatus == PRESSURE_STATUS.VERY_HIGH || pressureStatus == PRESSURE_STATUS.VERY_LOW)
            {
                rearRightTireImage = ImageSourceTireRed;
                rearRightTireLine = ImageSourceRedLowerTireLine;
            }
            else
            {
                rearRightTireImage = ImageSourceTireYellow;
                rearRightTireLine = ImageSourceYellowLowerTireLine;
            }
        }

        private TEMPERATURE_STATUS GetTemperatureStatus(double temperature)
        {
            TEMPERATURE_STATUS status = TEMPERATURE_STATUS.OK;

            if(temperature > TemperatureThresholds.HIGH_TEMPERATURE_THRESHOLD)
            {
                status = TEMPERATURE_STATUS.HIGH;
            }
            if(temperature > TemperatureThresholds.VERY_HIGH_TEMPERATURE_THRESHOLD)
            {
                status = TEMPERATURE_STATUS.VERY_HIGH;
            }

            return status;
        }

        private PRESSURE_STATUS GetPressureStatus(double pressure, double baselinePressure)
        {
            PRESSURE_STATUS status;
            double LOW_PRESSSURE_THRESHOLD = baselinePressure * PressureCoefficients.LOW_TIRE_PRESSURE_COEFFICIENT;
            double VERY_LOW_PRESSSURE_THRESHOLD = baselinePressure * PressureCoefficients.VERY_LOW_TIRE_PRESSURE_COEFFICIENT;
            double HIGH_PRESSSURE_THRESHOLD = baselinePressure * PressureCoefficients.HIGH_PRESSURE_COEFFICIENT;
            double VERY_HIGH_PRESSSURE_THRESHOLD = baselinePressure * PressureCoefficients.VERY_HIGH_TIRE_PRESSURE_COEFFICIENT;

            if(pressure < VERY_LOW_PRESSSURE_THRESHOLD)
            {
                status = PRESSURE_STATUS.VERY_LOW;
            }
            else if(pressure >= VERY_LOW_PRESSSURE_THRESHOLD && pressure <= LOW_PRESSSURE_THRESHOLD)
            {
                status = PRESSURE_STATUS.LOW;
            }
            else if(pressure > VERY_HIGH_PRESSSURE_THRESHOLD)
            {
                status = PRESSURE_STATUS.VERY_HIGH;
            }
            else if(pressure >= HIGH_PRESSSURE_THRESHOLD && pressure <= VERY_HIGH_PRESSSURE_THRESHOLD)
            {
                status = PRESSURE_STATUS.HIGH;
            }
            else
            {
                status = PRESSURE_STATUS.OK;
            }

            return status;
        }

        

    }
}
