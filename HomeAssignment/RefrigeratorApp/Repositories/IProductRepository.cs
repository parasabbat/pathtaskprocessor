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
        public Task<string> InsertProduct(ProductMaster productMaster);

        public Task<string> ConsumeProduct(ProductMaster productMaster);

        public Task<List<ProductMaster>> GetNearExpiryProducts();

        public Task<List<ProductMaster>> GetExpiriedProductsAndRemove();
   
    }
}
