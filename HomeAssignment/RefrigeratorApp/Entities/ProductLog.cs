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

        public Guid ProductId { get; set; }

        public double Quantity { get; set; }

        public DateTime EventTimeStamp { get; set; }
    }
}
