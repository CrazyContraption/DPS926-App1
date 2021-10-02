using System;
using System.Collections.Generic;
using System.ComponentModel;
using DPS_926___App_1.Models;
using DPS_926__App_1.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DPS_926___App_1.Views
{
    public partial class NewProductPage : ContentPage
    {
        public Product Product { get; set; }

        public NewProductPage()
        {
            InitializeComponent();

            Title = "New Product";
            BindingContext = this;

            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        /// <summary>
        /// Hook for when any button is clicked, the logic handles the action based on the sender's text
        /// </summary>
        /// <param name="sender">Object that called the hook</param>
        /// <param name="e">Event details arguments</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            ToolbarItem theButton = (sender as ToolbarItem);
            switch (theButton.Text.ToLower())
            {
                case "save":
                    if (entryName == null || (entryName.Text ?? "") == "")
                    {
                        DisplayAlert("New Product Error", "The product needs a name to identify it.", "OK");
                        entryName.Focus();
                        return;
                    }
                    else if (entryQuantity == null || (entryQuantity.Text ?? "") == "")
                    {
                        DisplayAlert("New Product Error", "The product needs a starting quantity.", "OK");
                        entryQuantity.Focus();
                        return;
                    }
                    else if (entryPrice == null || (entryPrice.Text ?? "") == "")
                    {
                        DisplayAlert("New Product Error", "The product needs a starting price.", "OK");
                        entryQuantity.Focus();
                        return;
                    }

                    bool isInt = int.TryParse(entryQuantity.Text, out int quantity);
                    bool isDecimal = decimal.TryParse(entryPrice.Text.Substring(1), out decimal price);

                    if (!isDecimal && entryPrice.Text == "FREE")
                    {
                        isDecimal = true;
                        price = 0.00M;
                    }

                    for (ushort index = 0; index < DataStore.Products.Count; index++)
                    {
                        if (DataStore.Products[index].Name == entryName.Text)
                        {
                            DisplayAlert("New Product Error", $"The product '{entryName.Text}' already exists, please choose a different name.", "OK");
                            entryName.Focus();
                            return;
                        }
                    }

                    if (!isDecimal || !isInt)
                    {
                        DisplayAlert("New Product Error", "Something went wrong. Please reset the product and try again.", "Oops");
                        return;
                    }
                    else if (price < 0)
                    {
                        DisplayAlert("New Product Error", "You cannot set a negative price for new products. Please set it to at least $0.00", "OK");
                        entryPrice.Focus();
                        return;
                    }

                    if (quantity < 0)
                        await DisplayAlert("Quantity Warning", "Normally, negative stock would be denied, however this is allowed for cases where stock may be owed.\n\nIf this was unintentional, please restock the product.", "OK");

                    DataStore.Products.Add(new Product(entryName.Text, quantity, price));

                    await DisplayAlert("Success!", $"New product '{entryName.Text}' has been saved successfully.", "OK");

                    await Navigation.PopAsync();
                    break;

                case "cancel":
                    await Navigation.PopAsync();
                    break;
            }
        }

        /// <summary>
        /// Hook for when entry field is deactivated, useful for updating values when a user is done with inputs
        /// </summary>
        /// <param name="sender">Object that called the hook</param>
        /// <param name="e">Event details arguments</param>
        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            Entry theEntry = (sender as Entry);

            if (theEntry.Text.StartsWith("-"))
                theEntry.Text = theEntry.Text.Substring(1);

            if (theEntry.Text.StartsWith("$"))
                theEntry.Text = theEntry.Text.Substring(1);

            bool isDecimal = decimal.TryParse(theEntry.Text, out decimal price);
            if (isDecimal)
            {
                if (price < 0)
                    price *= -1;
                if (price == 0)
                    theEntry.Text = "FREE";
                else
                    theEntry.Text = '$' + price.ToString("0.00");
            }
        }
    }
}