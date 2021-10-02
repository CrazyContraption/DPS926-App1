using System;

namespace DPS_926___App_1.Models
{
    /// <summary>
    /// Class containing product stock information
    /// </summary>
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; } // I debated ushort, but that's a recipe for disaster
        public decimal Price { get; set; }

        public Product(string name = "", int quantity = 0, decimal price = 0)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}