using System.ComponentModel.DataAnnotations.Schema;

namespace RefrigeratorApp.Entities
{
    public class ProductMaster
    {
        public ProductMaster()
        {
           
        }

        public ProductMaster(string name, string quantityUnit)
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
