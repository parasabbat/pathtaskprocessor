using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RefrigeratorApp.Entities
{
    public class ProductLog
    {
        public ProductLog(Guid productId, double quantity, string quantityUnit)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quanity = quantity;
            QuantityUnit = quantityUnit;
        }
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public double Quanity { get; set; }
        public string QuantityUnit { get; set; }
    }
}
