using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPS_926___App_1.Models;
using DPS_926___App_1.Views;
using DPS_926__App_1.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DPS_926___App_1.Views
{
    public partial class HistoryPage : ContentPage
    {
        private Purchase selectedPurchase { get; set; }

        public HistoryPage()
        {
            InitializeComponent();

            Title = "History";
            BindingContext = this;
            listPurchases.ItemsSource = DataStore.History;

            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        /// <summary>
        /// Hook called when a product is tapped from the list on the main page
        /// </summary>
        /// <param name="sender">Object that called the hook</param>
        /// <param name="e">Event details arguments</param>
        void PurchaseTapped(object sender, EventArgs e)
        {
            selectedPurchase = (e as ItemTappedEventArgs).Item as Purchase;
            Navigation.PushAsync(new HistoryDetailPage(selectedPurchase));
        }

        /// <summary>
        /// Refreshes the list data
        /// </summary>
        private void RefreshList()
        {
            listPurchases.ItemsSource = null;
            listPurchases.ItemsSource = DataStore.History;
        }

        /// <summary>
        /// Hook that is called when the list is appearing on the screen at any point
        /// </summary>
        protected override void OnAppearing()
        {
            RefreshList();
            base.OnAppearing();
        }
    }
}