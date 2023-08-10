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

        public async Task<Product> InsertProduct(Product product)
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
            _raContext.ProductInventoryLogs.Add(new ProductInventoryLog(productId: productId, quantity: product.Quantity,expiryDate: product.ExpiryDate));
            await _raContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> ConsumeProduct(Product product)
        {
            Guid productId = Guid.Empty;
            Product existingProduct = await _raContext.Products.FirstOrDefaultAsync(x => x.Name.Equals(product.Name));
            if (existingProduct != null)
            {
                if (product.QuantityUnit != existingProduct.QuantityUnit)
                {
                    // raise exception
                    throw new InvalidOperationException("Quantity Unit should be same");
                }
                else
                {
                    productId = existingProduct.Id;
                    _raContext.Entry(existingProduct).State = EntityState.Modified;
                }
            }
            else
            {
                productId = Guid.NewGuid();
                product.Id = productId;
                _raContext.Products.Add(product);
            }

            // add to product log
            _raContext.ProductInventoryLogs.Add(new ProductInventoryLog(productId: productId, quantity: product.Quantity, expiryDate: product.ExpiryDate));
            await _raContext.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetExpiriedProductsAndRemove()
        {
            var x = await _raContext.ProductInventoryLogs.ToListAsync();
            var products = await(from i in _raContext.ProductInventoryLogs
                                 join p in _raContext.Products
                                 on i.ProductId equals p.Id
                                 where i.ExpiryDate.HasValue && i.ExpiryDate.Value.Date <= DateTime.Now.Date
                                 select new Product()
                                 {
                                     InventoryLogId = i.Id,
                                     Name = p.Name,
                                     Quantity = i.Quantity,
                                     QuantityUnit = p.QuantityUnit,
                                     ExpiryDate = i.ExpiryDate
                                 }).ToListAsync();
            foreach (var product in products)
            {
                ProductInventoryLog productInventoryLog = await _raContext.ProductInventoryLogs.FindAsync(product.InventoryLogId);
                _raContext.ProductInventoryLogs.Remove(productInventoryLog);
            }
            await _raContext.SaveChangesAsync();
            return products;
        }

        public async Task<List<Product>> GetNearExpiryProducts()
        {
            var products =await (from i in _raContext.ProductInventoryLogs
                          join p in _raContext.Products
                          on i.ProductId equals p.Id
                          where i.ExpiryDate.HasValue && i.ExpiryDate.Value.Date == DateTime.Now.Date.AddDays(1)
                                 select new Product()
                          {
                              Name = p.Name,
                              Quantity=  i.Quantity,
                              QuantityUnit= p.QuantityUnit,
                              ExpiryDate =i.ExpiryDate
                          }).ToListAsync();
            return products;
        }
    } 
}
