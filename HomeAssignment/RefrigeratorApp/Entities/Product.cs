using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorApp.Entities
{
    public class Product
    {
        public Product()
        {
           
        }

        public Product(string name, string quantityUnit)
        {
            Name = name;
            QuantityUnit = quantityUnit;
        }
       
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string QuantityUnit { get; set; }

        [NotMapped]
        public DateTime? ExpiryDate { get; set; }
        [NotMapped]
        public double Quantity { get; set; }

        [NotMapped]
        public Guid InventoryLogId { get; set; }
    }
}
