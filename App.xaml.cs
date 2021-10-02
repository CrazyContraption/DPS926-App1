using System;
using DPS_926___App_1.Views;
using DPS_926__App_1.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DPS_926___App_1
{
    public partial class App : Application
    {
        /// <summary>
        /// Class containing the core of the app, of which everything builds off of
        /// </summary>
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
