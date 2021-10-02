using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DPS_926___App_1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagerPage : ContentPage
    {
        public ManagerPage()
        {
            InitializeComponent();

            Title = "Manager Menu";
            BindingContext = this;

            NavigationPage.SetBackButtonTitle(this, "Register");
        }

        /// <summary>
        /// Hook for when any button is clicked, the logic handles the action based on the sender's text
        /// </summary>
        /// <param name="sender">Object that called the hook</param>
        /// <param name="e">Event details arguments</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button theButton = (sender as Button);
            switch (theButton.Text.ToLower())
            {
                case "history":
                    await Navigation.PushAsync(new HistoryPage(), true);
                    break;

                case "restock":
                    await Navigation.PushAsync(new RestockPage(), true);
                    break;

                case "add new product":
                    await Navigation.PushAsync(new NewProductPage(), true);
                    break;
            }
        }
    }
}