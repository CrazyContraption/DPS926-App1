using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DPS_926___App_1.Models;
using DPS_926__App_1.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DPS_926___App_1.Views
{
    public partial class MainPage : ContentPage
    {
        private Product selectedProduct { get; set; }
        private decimal runningTotal { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            Title = "Cash Register App";
            BindingContext = this;

            Reset();
        }

        /// <summary>
        /// Resets the text fields back to "empty states"
        /// </summary>
        private void Reset()
        {
            selectedProduct = null;
            runningTotal = 0;
            labelType.Text = "";
            labelTotal.Text = "$0.00";
            labelQuantity.Text = "0";
        }

        /// <summary>
        /// Hook called when a product is selected from the list on the main page
        /// </summary>
        /// <param name="sender">Object that called the hook</param>
        /// <param name="e">Event details arguments</param>
        void ProductSelected(object sender, EventArgs e)
        {
            selectedProduct = (e as SelectedItemChangedEventArgs).SelectedItem as Product;
            labelType.Text = selectedProduct.Name;
            UpdateTotal();
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
                case "manager":
                    await Navigation.PushAsync(new ManagerPage());
                    break;

                case "buy":
                    if (HandleBuy())
                        Reset();
                    break;

                case "del":
                    if (labelQuantity.Text.Length > 1)
                        labelQuantity.Text = labelQuantity.Text.Substring(0, labelQuantity.Text.Length - 1);
                    else
                        labelQuantity.Text = "0";
                    break;

                default:
                    if (labelQuantity.Text == "0" && theButton.Text != "0")
                        labelQuantity.Text = theButton.Text;
                    else if (labelQuantity.Text == "0")
                        break;
                    else
                        labelQuantity.Text = labelQuantity.Text + theButton.Text;
                    break;
            }
            UpdateTotal();
        }

        /// <summary>
        /// Gets, calculates, validates, and updates the running total of the purchases
        /// </summary>
        private void UpdateTotal()
        {
            if (selectedProduct != null)
            {
                if (int.TryParse(labelQuantity.Text, out int quantity))
                    runningTotal = (decimal)quantity * selectedProduct.Price;
                labelTotal.Text = "$" + runningTotal.ToString("0.00");
            }
        }

        /// <summary>
        /// Handles the buying action(s)
        /// </summary>
        /// <returns>Bool of if the puchase succeeded</returns>
        private bool HandleBuy()
        {
            bool isInt = int.TryParse(labelQuantity.Text, out int quantity);
            if (!isInt)
                DisplayAlert("Purchase Error", "Something went wrong. Please reset your purchase and try again.", "Oops");
            else if ((selectedProduct ?? new Product()).Name == "")
                DisplayAlert("Purchase Error", "You must select an item to be bought.", "OK");
            else if (quantity <= 0)
                DisplayAlert("Purchase Error", "You must input a quantity of at least 1.", "OK");
            else if (quantity > selectedProduct.Quantity)
            {
                if (selectedProduct.Quantity > 0)
                    DisplayAlert("Purchase Error", $"Desired quantity exceeds currently held stock. Enter a quantity of {selectedProduct.Quantity} or lower.", "OK");
                else
                    DisplayAlert("Purchase Error", "No stock available for this product, please contact a manager to restock it first.", "OK");

            }
            else
            {
                DisplayAlert($"Item{(labelQuantity.Text != "1" ? "s" : "")} Purchased!", $"Thank-you for you patronage!\n\nYour total was: ${runningTotal.ToString("0.00")}", "OK");
                selectedProduct.Quantity -= quantity;
                DataStore.History.Add(new Purchase(selectedProduct.Name, quantity, runningTotal));
                RefreshList();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Refreshes the list data
        /// </summary>
        private void RefreshList()
        {
            UpdateTotal();
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