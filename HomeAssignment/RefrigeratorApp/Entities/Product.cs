using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorApp.Entities
{
    public class Product
    {
        public Product(string name, double currentQuantity, string quantityUnit) 
        {
            Name = name;
            CurrentQuantity = currentQuantity;
            QuantityUnit = quantityUnit;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double CurrentQuantity { get; set; }
        public string QuantityUnit { get; set; }
    }
}
