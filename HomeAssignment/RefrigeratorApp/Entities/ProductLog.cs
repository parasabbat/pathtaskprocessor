using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorApp.Entities
{
    public class ProductLog
    {
        public Guid Id { get; set; }
        public double Quanity { get; set; }
        public string QuantityUnit { get; set; }
    }
}
