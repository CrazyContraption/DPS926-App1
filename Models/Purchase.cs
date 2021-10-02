using System;

namespace DPS_926___App_1.Models
{
    /// <summary>
    /// Class containing purchase history information
    /// </summary>
    public class Purchase
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }

        public Purchase(string name = "", int quantity = 0, decimal total = 0)
        {
            Name = name;
            Quantity = quantity;
            Total = total;
            Date = DateTime.Now;
        }
    }
}