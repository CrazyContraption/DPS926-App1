using System;
using System.ComponentModel;
using DPS_926___App_1.Models;
using DPS_926__App_1.Services;
using Xamarin.Forms;

namespace DPS_926___App_1.Views
{
    public partial class RestockPage : ContentPage
    {
        private Product selectedProduct { get; set; }

        public RestockPage()
        {
            InitializeComponent();

            Title = "Restock";
            BindingContext = this;

            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        /// <summary>
        /// Hook called when a product is selected from the list on the main page
        /// </summary>
        /// <param name="sender">Object that called the hook</param>
        /// <param name="e">Event details arguments</param>
        void ProductSelected(object sender, EventArgs e)
            => selectedProduct = (e as SelectedItemChangedEventArgs).SelectedItem as Product;

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
                case "restock":

                    if ((selectedProduct ?? new Product()).Name == "")
                        DisplayAlert("Restock Error", "You must select a product to restock.", "OK");
                    else if ((entryQuantity.Text ?? "") == "")
                    {
                        DisplayAlert("Restock Error", "You must input an amount to restock by.", "OK");
                        entryQuantity.Focus();
                    }
                    else
                    {
                        bool isInt = int.TryParse(entryQuantity.Text, out int quantity);
                        if (!isInt)
                            DisplayAlert("Restock Error", "Something went wrong. Please reset your restock order and try again.", "Oops");
                        else
                        {
                            selectedProduct.Quantity += quantity;
                            DisplayAlert("Success!", $"Restocked {quantity} {selectedProduct.Name}, for a total of {selectedProduct.Quantity}.", "OK");
                            entryQuantity.Text = "";
                            RefreshList();
                        }
                    }
                    break;

                case "cancel":
                    _ = await Navigation.PopAsync();
                    break;
            }
        }

        /// <summary>
        /// Refreshes the list data
        /// </summary>
        private void RefreshList()
        {
            listProducts.ItemsSource = null;
            listProducts.ItemsSource = DataStore.Products;
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