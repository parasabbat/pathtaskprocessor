using RefrigeratorApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorApp.Repositories
{
    public interface IProductRepository
    {
        public Task<Product> InsertProduct(Product product);

        public Task<Product> ConsumeProduct(Product product);

        public Task<List<Product>> GetNearExpiryProducts();

        public Task<List<Product>> GetExpiriedProductsAndRemove();
   
    }
}
