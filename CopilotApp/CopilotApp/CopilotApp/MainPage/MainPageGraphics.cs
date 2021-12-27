using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace CopilotApp
{
    public partial class MainPageViewmodel : INotifyPropertyChanged
    {

        //Preloaded ImageSources
        /*
            TireLine is the lines going from the tire including the circle
            UpperBranch is the upper branch stemming from the circle on which the pressure is displayed
            LowerBranch is the lower branch stemming from the circle on which the temperature is displayed
         */
        //Tires
        public ImageSource ImageSourceTireDefault = ImageSource.FromFile("TireDefault.png");
        public ImageSource ImageSourceTireRed = ImageSource.FromFile("TireRed.png");
        public ImageSource ImageSourceTireYellow = ImageSource.FromFile("TireYellow.png");

        //Green Lines
        public ImageSource ImageSourceGreenLowerBranch = ImageSource.FromFile("GreenLowerBranch.png");
        public ImageSource ImageSourceGreenLowerTireLine = ImageSource.FromFile("GreenLowerTireLine.png");
        public ImageSource ImageSourceGreenUpperBranch = ImageSource.FromFile("GreenUpperBranch.png");
        public ImageSource ImageSourceGreenUpperTireLine = ImageSource.FromFile("GreenUpperTireLine.png");

        //Red Lines
        public ImageSource ImageSourceRedLowerBranch = ImageSource.FromFile("RedLowerBranch.png");
        public ImageSource ImageSourceRedLowerTireLine = ImageSource.FromFile("RedLowerTireLine.png");
        public ImageSource ImageSourceRedUpperBranch = ImageSource.FromFile("RedUpperBranch.png");
        public ImageSource ImageSourceRedUpperTireLine = ImageSource.FromFile("RedUpperTireLine");

        //Yellow Lines
        public ImageSource ImageSourceYellowLowerBranch = ImageSource.FromFile("YellowLowerBranch.png");
        public ImageSource ImageSourceYellowLowerTireLine = ImageSource.FromFile("YellowLowerTireLine.png");
        public ImageSource ImageSourceYellowUpperBranch = ImageSource.FromFile("YellowUpperBranch.png");
        public ImageSource ImageSourceYellowUpperTireLine = ImageSource.FromFile("YellowUpperTireLine");


        //These ImageSources are bound to the XAML and are the images that are being displayed to the user.
        //Tire Display Images
        ImageSource _frontLeftTireImage; public ImageSource frontLeftTireImage { get => _frontLeftTireImage; set { _frontLeftTireImage = value; OnPropertyChanged(nameof(frontLeftTireImage)); } }
        ImageSource _frontRightTireImage; public ImageSource frontRightTireImage { get => _frontRightTireImage; set { _frontRightTireImage = value; OnPropertyChanged(nameof(frontRightTireImage)); } }
        ImageSource _rearLeftTireImage; public ImageSource rearLeftTireImage { get => _rearLeftTireImage; set { _rearLeftTireImage = value; OnPropertyChanged(nameof(rearLeftTireImage)); } }
        ImageSource _rearRightTireImage; public ImageSource rearRightTireImage { get => _rearRightTireImage; set { _rearRightTireImage = value; OnPropertyChanged(nameof(rearRightTireImage)); } }


        //Front Left Display Lines
        ImageSource _frontLeftTireLine; public ImageSource frontLeftTireLine { get => _frontLeftTireLine; set { _frontLeftTireLine = value; OnPropertyChanged(nameof(frontLeftTireLine)); } }
        ImageSource _frontLeftTireUpperBranch; public ImageSource frontLeftTireUpperBranch { get => _frontLeftTireUpperBranch; set { _frontLeftTireUpperBranch = value; OnPropertyChanged(nameof(frontLeftTireUpperBranch)); } }
        ImageSource _frontLeftTireLowerBranch; public ImageSource frontLeftTireLowerBranch { get => _frontLeftTireLowerBranch; set { _frontLeftTireLowerBranch = value; OnPropertyChanged(nameof(frontLeftTireLowerBranch)); } }


        //Front Right Display Lines
        ImageSource _frontRightTireLine; public ImageSource frontRightTireLine { get => _frontRightTireLine; set { _frontRightTireLine = value; OnPropertyChanged(nameof(frontRightTireLine)); } }
        ImageSource _frontRightTireUpperBranch; public ImageSource frontRightTireUpperBranch { get => _frontRightTireUpperBranch; set { _frontRightTireUpperBranch = value; OnPropertyChanged(nameof(frontRightTireUpperBranch)); } }
        ImageSource _frontRightTireLowerBranch; public ImageSource frontRightTireLowerBranch { get => _frontRightTireLowerBranch; set { _frontRightTireLowerBranch = value; OnPropertyChanged(nameof(frontRightTireLowerBranch)); } }


        //Rear Left Display Lines
        ImageSource _rearLeftTireLine; public ImageSource rearLeftTireLine { get => _rearLeftTireLine; set { _rearLeftTireLine = value; OnPropertyChanged(nameof(rearLeftTireLine)); } }
        ImageSource _rearLeftTireUpperBranch; public ImageSource rearLeftTireUpperBranch { get => _rearLeftTireUpperBranch; set { _rearLeftTireUpperBranch = value; OnPropertyChanged(nameof(rearLeftTireUpperBranch)); } }
        ImageSource _rearLeftTireLowerBranch; public ImageSource rearLeftTireLowerBranch { get => _rearLeftTireLowerBranch; set { _rearLeftTireLowerBranch = value; OnPropertyChanged(nameof(rearLeftTireLowerBranch)); } }


        //Rear Right Display Lines
        ImageSource _rearRightTireLine; public ImageSource rearRightTireLine { get => _rearRightTireLine; set { _rearRightTireLine = value; OnPropertyChanged(nameof(rearRightTireLine)); } }
        ImageSource _rearRightTireUpperBranch; public ImageSource rearRightTireUpperBranch { get => _rearRightTireUpperBranch; set { _rearRightTireUpperBranch = value; OnPropertyChanged(nameof(rearRightTireUpperBranch)); } }
        ImageSource _rearRightTireLowerBranch; public ImageSource rearRightTireLowerBranch { get => _rearRightTireLowerBranch; set { _rearRightTireLowerBranch = value; OnPropertyChanged(nameof(rearRightTireLowerBranch)); } }


    }
}
