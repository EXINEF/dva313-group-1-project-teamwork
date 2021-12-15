using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CopilotApp
{
    public partial class SimulatorPageViewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SimulatorPageViewmodel()
        {
            //Binds the "BackToMainPageButtonPressedCommand" called in SimulatorPage.xaml to the ReturnToMainPage() function in this class
            BackToMainPageButtonPressedCommand = new Command(ReturnToMainPage);
        }

        //New command. The command we call from the xaml code (Command="{Binding BackToOverviewButtonPressedCommand}).
        public ICommand BackToMainPageButtonPressedCommand { get; }

        async void ReturnToMainPage()
        {
            //The pages work as a stack, when we press the simulator button on the MainPage a SimulatorPage is pushed on the stack
            //This pops the simulator page from the stack making MainPage the top page again.
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
