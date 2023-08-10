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

        public async Task<Product> AddUpdateProduct(Product product)
        {
            Guid productId = Guid.Empty;
            Product existingProduct = await _raContext.Products.FirstOrDefaultAsync(x => x.Name.Equals(product.Name));
            if (existingProduct != null)
            {
                if(product.QuantityUnit != existingProduct.QuantityUnit)
                {
                    // raise exception
                    throw new InvalidOperationException("Quantity Unit should be same");
                }
                else
                {
                    productId = existingProduct.Id;
                    existingProduct.CurrentQuantity = existingProduct.CurrentQuantity + product.CurrentQuantity;
                    _raContext.Entry(existingProduct).State = EntityState.Modified;
                }
            }
            else
            {
                productId  = Guid.NewGuid();
                product.Id = productId;
                _raContext.Products.Add(product);
            }

            // add to product log
            _raContext.ProductLogs.Add(new ProductLog(productId: productId, quantity: product.CurrentQuantity, quantityUnit: product.QuantityUnit));
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
