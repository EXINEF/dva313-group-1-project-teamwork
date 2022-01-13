using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CopilotApp
{
    public partial class SimulatorPageViewmodel
    {
        string _year; public string year { get => _year; set { _year = value; OnPropertyChanged(nameof(year)); } }
        string _month; public string month { get => _month; set { _month = value; OnPropertyChanged(nameof(month)); } }
        string _day; public string day { get => _day; set { _day = value; OnPropertyChanged(nameof(day)); } }
        string _hour; public string hour { get => _hour; set { _hour = value; OnPropertyChanged(nameof(hour)); } }
        string _minute; public string minute { get => _minute; set { _minute = value; OnPropertyChanged(nameof(minute)); } }
        string _second; public string second { get => _second; set { _second = value; OnPropertyChanged(nameof(second)); } }

        private string GetDateTime()
        {

            if( year == null || year == "" ||
                month == null || month == ""||
                day == null || day == ""||
                hour == null || hour == ""||
                minute == null || minute == ""||
                second == null || second == "")
            {
                return null;
            }

            //2021-06-15 11:16:47.000000
            //Format example: 2021-12-15 18:56:53.632463
            string dateTime = "'" + year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second + ".000000'";
            return dateTime;
        }
    }
}