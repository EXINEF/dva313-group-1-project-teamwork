using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CopilotApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Default single page view
            //MainPage = new MainPage();


            //Allows for navigation / multiple pages with MainPage() as the starting page.
            MainPage = new NavigationPage(new MainPage())
            {
                //Can add properties here for navbar(a row at the top of the app) colors / texts / fonts.
                //For now the navbar disabled in MainPage.xaml -> NavigationPage.HasNavigationBar="False"

                //BarBackgroundColor = Color.Azure,
                //BarTextColor = Color.Black
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
