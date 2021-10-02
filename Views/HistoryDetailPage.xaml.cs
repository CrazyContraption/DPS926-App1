using System;
using System.ComponentModel;
using DPS_926___App_1.Models;
using Xamarin.Forms;

namespace DPS_926___App_1.Views
{
    public partial class HistoryDetailPage : ContentPage
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }

        public HistoryDetailPage(Purchase purchase)
        {
            InitializeComponent();

            Name = purchase.Name;
            Quantity = purchase.Quantity;
            Total = purchase.Total;
            Date = purchase.Date;
            BindingContext = this;

            NavigationPage.SetBackButtonTitle(this, "History Items");
        }
    }
}