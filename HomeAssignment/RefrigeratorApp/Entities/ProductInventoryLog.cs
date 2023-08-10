using System.ComponentModel.DataAnnotations.Schema;

namespace RefrigeratorApp.Entities
{
    public class ProductInventoryLog
    {
        public ProductInventoryLog(Guid productId, double quantity, DateTime? expiryDate)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            ExpiryDate = expiryDate;
        }
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public double Quantity { get; set; }

        public DateTime? ExpiryDate { get; set; }

        
    }
}
