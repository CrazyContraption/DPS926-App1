using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using DPS_926___App_1.Models;

namespace DPS_926__App_1.Services
{
    /// <summary>
    /// Static class that stores all of the inter-page information
    /// </summary>
    public static class DataStore
    {
        /// <summary>
        /// The list of observable products
        /// </summary>
        public static ObservableCollection<Product> Products { get; }

        /// <summary>
        /// The list of observable purchases
        /// </summary>
        public static ObservableCollection<Purchase> History = new ObservableCollection<Purchase>();

        /// <summary>
        /// Static constructor containing the defaults of all products
        /// </summary>
        static DataStore()
        {
            Products = new ObservableCollection<Product>()
            {
                new Product { Quantity=20, Price=50.70M, Name = "Pants" },
                new Product { Quantity=50, Price=90.00M, Name = "Shoes" },
                new Product { Quantity=10, Price=20.50M, Name = "Hats" },
                new Product { Quantity=10, Price=42.50M, Name = "T-Shirts" },
                new Product { Quantity=24, Price=71.20M, Name = "Dresses" },
                new Product { Quantity=50, Price=12.50M, Name = "Socks" }
            };
        }
    }
}