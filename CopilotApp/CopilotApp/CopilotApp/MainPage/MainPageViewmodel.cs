using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CopilotApp
{
    public partial class MainPageViewmodel : INotifyPropertyChanged
    {
        public enum TEMPERATURE_STATUS { OK, HIGH, VERY_HIGH }
        public enum PRESSURE_STATUS { VERY_LOW, LOW, OK, HIGH, VERY_HIGH }

        const string COLOR_NO_DANGER = "White";
        const string COLOR_MODERATE_DANGER = "Yellow";
        const string COLOR_HIGH_DANGER = "Red";

        //These variables are tied to the temperature and pressure labels in the MainPage.xaml and is what is being displayed.
        string _frontLeftTireTemperatureDisplayValue; public string frontLeftTireTemperatureDisplayValue { get => _frontLeftTireTemperatureDisplayValue; set { _frontLeftTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(frontLeftTireTemperatureDisplayValue)); } }
        string _frontLeftTirePressureDisplayValue; public string frontLeftTirePressureDisplayValue { get => _frontLeftTirePressureDisplayValue; set { _frontLeftTirePressureDisplayValue = value; OnPropertyChanged(nameof(frontLeftTirePressureDisplayValue)); } }
        string _frontRightTireTemperatureDisplayValue; public string frontRightTireTemperatureDisplayValue { get => _frontRightTireTemperatureDisplayValue; set { _frontRightTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(frontRightTireTemperatureDisplayValue)); } }
        string _frontRightTirePressureDisplayValue; public string frontRightTirePressureDisplayValue { get => _frontRightTirePressureDisplayValue; set { _frontRightTirePressureDisplayValue = value; OnPropertyChanged(nameof(frontRightTirePressureDisplayValue)); } }
        string _rearLeftTireTemperatureDisplayValue; public string rearLeftTireTemperatureDisplayValue { get => _rearLeftTireTemperatureDisplayValue; set { _rearLeftTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(rearLeftTireTemperatureDisplayValue)); } }
        string _rearLeftTirePressureDisplayValue; public string rearLeftTirePressureDisplayValue { get => _rearLeftTirePressureDisplayValue; set { _rearLeftTirePressureDisplayValue = value; OnPropertyChanged(nameof(rearLeftTirePressureDisplayValue)); } }
        string _rearRightTireTemperatureDisplayValue; public string rearRightTireTemperatureDisplayValue { get => _rearRightTireTemperatureDisplayValue; set { _rearRightTireTemperatureDisplayValue = value; OnPropertyChanged(nameof(rearRightTireTemperatureDisplayValue)); } }
        string _rearRightTirePressureDisplayValue; public string rearRightTirePressureDisplayValue { get => _rearRightTirePressureDisplayValue; set { _rearRightTirePressureDisplayValue = value; OnPropertyChanged(nameof(rearRightTirePressureDisplayValue)); } }

        //Color bindings for the pressure and temperaure display values indicating the color of the temperature and pressure labels.
        string _frontLeftPressureTextColor; public string frontLeftPressureTextColor { get => _frontLeftPressureTextColor; set { _frontLeftPressureTextColor = value; OnPropertyChanged(nameof(frontLeftPressureTextColor)); } }
        string _frontLeftTemperatureTextColor; public string frontLeftTemperatureTextColor { get => _frontLeftTemperatureTextColor; set { _frontLeftTemperatureTextColor = value; OnPropertyChanged(nameof(frontLeftTemperatureTextColor)); } }
        string _frontRightPressureTextColor; public string frontRightPressureTextColor { get => _frontRightPressureTextColor; set { _frontRightPressureTextColor = value; OnPropertyChanged(nameof(frontRightPressureTextColor)); } }
        string _frontRightTemperatureTextColor; public string frontRightTemperatureTextColor { get => _frontRightTemperatureTextColor; set { _frontRightTemperatureTextColor = value; OnPropertyChanged(nameof(frontRightTemperatureTextColor)); } }
        string _rearLeftPressureTextColor; public string rearLeftPressureTextColor { get => _rearLeftPressureTextColor; set { _rearLeftPressureTextColor = value; OnPropertyChanged(nameof(rearLeftPressureTextColor)); } }
        string _rearLeftTemperatureTextColor; public string rearLeftTemperatureTextColor { get => _rearLeftTemperatureTextColor; set { _rearLeftTemperatureTextColor = value; OnPropertyChanged(nameof(rearLeftTemperatureTextColor)); } }
        string _rearRightPressureTextColor; public string rearRightPressureTextColor { get => _rearRightPressureTextColor; set { _rearRightPressureTextColor = value; OnPropertyChanged(nameof(rearRightPressureTextColor)); } }
        string _rearRightTemperatureTextColor; public string rearRightTemperatureTextColor { get => _rearRightTemperatureTextColor; set { _rearRightTemperatureTextColor = value; OnPropertyChanged(nameof(rearRightTemperatureTextColor)); } }


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

            Startup.Run();
            //Moved to Startup.cs so it only begins calculating after all data is loaded
            //Calculations calc = new Calculations();
            //Task.Run(async () => { await calc.run(); });

            //Subscribe to messaging so that other pages can tell us to update our displayvalues.
            MessagingCenter.Subscribe<object>(this, "UpdateMainPageDisplayValues", (sender) => { UpdateDisplay(); });
            MessagingCenter.Subscribe<object>(this, "UpdateFrontLeftTireGraphics", (sender) => { UpdateFrontLeftTireGraphics(); });
            MessagingCenter.Subscribe<object>(this, "UpdateFrontRightTireGraphics", (sender) => { UpdateFrontRightTireGraphics(); });
            MessagingCenter.Subscribe<object>(this, "UpdateRearLeftTireGraphics", (sender) => { UpdateRearLeftTireGraphics(); });
            MessagingCenter.Subscribe<object>(this, "UpdateRearRightTireGraphics", (sender) => { UpdateRearRightTireGraphics(); });

            //Subscribe to receive notification using MessagingCenter use MainPageViewmodel.PushNotification("hello world"); to push a notification from anywhere.
            MessagingCenter.Subscribe<Xamarin.Forms.Application, string> (Xamarin.Forms.Application.Current, "NewNotification", (sender, message) => {
                AddNotification(message);
            });

            //Default images, needed because otherwise no images wont show when changed. (Guessing XAML defaults the image size to 0 and wont resize.)
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

        //Update All graphics
        public void UpdateDisplay()
        {
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


        //Evaluate the temperature and pressure and update the graphics accordingly.
        private void UpdateFrontLeftTireGraphics()
        {
            //Grab newest data from the Live Data and set as the display values
            frontLeftTireTemperatureDisplayValue = SensorData.frontLeftSensorTemperature.ToString() + " °C";
            frontLeftTirePressureDisplayValue = SensorData.frontLeftSensorPressure.ToString() + " kPa";

            TEMPERATURE_STATUS tempStatus = GetTemperatureStatus(SensorData.frontLeftSensorTemperature);
            PRESSURE_STATUS pressureStatus = GetPressureStatus(SensorData.frontLeftSensorPressure, TireData.frontLeftTireBaselinePressure);

            //Temperature Values / Lower Branch
            if(tempStatus == TEMPERATURE_STATUS.OK)
            {
                frontLeftTemperatureTextColor = COLOR_NO_DANGER;
                frontLeftTireLowerBranch = ImageSourceGreenLowerBranch;
            }
            else if (tempStatus == TEMPERATURE_STATUS.HIGH)
            {
                frontLeftTemperatureTextColor = COLOR_MODERATE_DANGER;
                frontLeftTireLowerBranch = ImageSourceYellowLowerBranch;
            }
            else
            {
                frontLeftTemperatureTextColor = COLOR_HIGH_DANGER;
                frontLeftTireLowerBranch = ImageSourceRedLowerBranch;
            }

            //Pressure Values / Upper Branch
            if (pressureStatus == PRESSURE_STATUS.OK)
            {
                frontLeftPressureTextColor = COLOR_NO_DANGER;
                frontLeftTireUpperBranch = ImageSourceGreenUpperBranch;
            }
            else if (pressureStatus == PRESSURE_STATUS.LOW || pressureStatus == PRESSURE_STATUS.HIGH)
            {
                frontLeftPressureTextColor = COLOR_MODERATE_DANGER;
                frontLeftTireUpperBranch = ImageSourceYellowUpperBranch;
            }
            else
            {
                frontLeftPressureTextColor = COLOR_HIGH_DANGER;
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
            //Grab newest data from the Live Data and set as the display values
            frontRightTireTemperatureDisplayValue = SensorData.frontRightSensorTemperature.ToString() + " °C";
            frontRightTirePressureDisplayValue = SensorData.frontRightSensorPressure.ToString() + " kPa";

            TEMPERATURE_STATUS tempStatus = GetTemperatureStatus(SensorData.frontRightSensorTemperature);
            PRESSURE_STATUS pressureStatus = GetPressureStatus(SensorData.frontRightSensorPressure, TireData.frontRightTireBaselinePressure);

            //Temperature Values / Lower Branch
            if (tempStatus == TEMPERATURE_STATUS.OK)
            {
                frontRightTemperatureTextColor = COLOR_NO_DANGER;
                frontRightTireLowerBranch = ImageSourceGreenLowerBranch;
            }
            else if (tempStatus == TEMPERATURE_STATUS.HIGH)
            {
                frontRightTemperatureTextColor = COLOR_MODERATE_DANGER;
                frontRightTireLowerBranch = ImageSourceYellowLowerBranch;
            }
            else
            {
                frontRightTemperatureTextColor = COLOR_HIGH_DANGER;
                frontRightTireLowerBranch = ImageSourceRedLowerBranch;
            }

            //Pressure Values / Upper Branch
            if (pressureStatus == PRESSURE_STATUS.OK)
            {
                frontRightPressureTextColor = COLOR_NO_DANGER;
                frontRightTireUpperBranch = ImageSourceGreenUpperBranch;
            }
            else if (pressureStatus == PRESSURE_STATUS.LOW || pressureStatus == PRESSURE_STATUS.HIGH)
            {
                frontRightPressureTextColor = COLOR_MODERATE_DANGER;
                frontRightTireUpperBranch = ImageSourceYellowUpperBranch;
            }
            else
            {
                frontRightPressureTextColor = COLOR_HIGH_DANGER;
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
            //Grab newest data from the Live Data and set as the display values
            rearLeftTireTemperatureDisplayValue = SensorData.rearLeftSensorTemperature.ToString() + " °C";
            rearLeftTirePressureDisplayValue = SensorData.rearLeftSensorPressure.ToString() + " kPa";

            TEMPERATURE_STATUS tempStatus = GetTemperatureStatus(SensorData.rearLeftSensorTemperature);
            PRESSURE_STATUS pressureStatus = GetPressureStatus(SensorData.rearLeftSensorPressure, TireData.rearLeftTireBaselinePressure);

            //Temperature Values / Lower Branch
            if (tempStatus == TEMPERATURE_STATUS.OK)
            {
                rearLeftTemperatureTextColor = COLOR_NO_DANGER;
                rearLeftTireLowerBranch = ImageSourceGreenLowerBranch;
            }
            else if (tempStatus == TEMPERATURE_STATUS.HIGH)
            {
                rearLeftTemperatureTextColor = COLOR_MODERATE_DANGER;
                rearLeftTireLowerBranch = ImageSourceYellowLowerBranch;
            }
            else
            {
                rearLeftTemperatureTextColor = COLOR_HIGH_DANGER;
                rearLeftTireLowerBranch = ImageSourceRedLowerBranch;
            }

            //Pressure Values / Upper Branch
            if (pressureStatus == PRESSURE_STATUS.OK)
            {
                rearLeftPressureTextColor = COLOR_NO_DANGER;
                rearLeftTireUpperBranch = ImageSourceGreenUpperBranch;
            }
            else if (pressureStatus == PRESSURE_STATUS.LOW || pressureStatus == PRESSURE_STATUS.HIGH)
            {
                rearLeftPressureTextColor = COLOR_MODERATE_DANGER;
                rearLeftTireUpperBranch = ImageSourceYellowUpperBranch;
            }
            else
            {
                rearLeftPressureTextColor = COLOR_HIGH_DANGER;
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
            //Grab newest data from the Live Data and set as the display values
            rearRightTireTemperatureDisplayValue = SensorData.rearRightSensorTemperature.ToString() + " °C";
            rearRightTirePressureDisplayValue = SensorData.rearRightSensorPressure.ToString() + " kPa";

            TEMPERATURE_STATUS tempStatus = GetTemperatureStatus(SensorData.rearRightSensorTemperature);
            PRESSURE_STATUS pressureStatus = GetPressureStatus(SensorData.rearRightSensorPressure, TireData.rearRightTireBaselinePressure);

            //Temperature Values / Lower Branch
            if (tempStatus == TEMPERATURE_STATUS.OK)
            {
                rearRightTemperatureTextColor = COLOR_NO_DANGER;
                rearRightTireLowerBranch = ImageSourceGreenLowerBranch;
            }
            else if (tempStatus == TEMPERATURE_STATUS.HIGH)
            {
                rearRightTemperatureTextColor = COLOR_MODERATE_DANGER;
                rearRightTireLowerBranch = ImageSourceYellowLowerBranch;
            }
            else
            {
                rearRightTemperatureTextColor = COLOR_HIGH_DANGER;
                rearRightTireLowerBranch = ImageSourceRedLowerBranch;
            }

            //Pressure Values / Upper Branch
            if (pressureStatus == PRESSURE_STATUS.OK)
            {
                rearRightPressureTextColor = COLOR_NO_DANGER;
                rearRightTireUpperBranch = ImageSourceGreenUpperBranch;
            }
            else if (pressureStatus == PRESSURE_STATUS.LOW || pressureStatus == PRESSURE_STATUS.HIGH)
            {
                rearRightPressureTextColor = COLOR_MODERATE_DANGER;
                rearRightTireUpperBranch = ImageSourceYellowUpperBranch;
            }
            else
            {
                rearRightPressureTextColor = COLOR_HIGH_DANGER;
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
