using Microsoft.EntityFrameworkCore;
using RefrigeratorApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly RaContext _raContext;
        public ProductRepository(RaContext raContext)
        {
            _raContext = raContext;
        }
        public async Task<Product> AddProduct(Product product)
        {
            _raContext.Products.Add(product);
            await _raContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _raContext.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProducts()
        {
           return await _raContext.Products.ToListAsync();
        }
    } 
}
