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
        public Task<Product> AddProduct(Product product);

        public Task<List<Product>> GetProducts();

        public Task<Product> GetProductById(Guid id);
    }
}
