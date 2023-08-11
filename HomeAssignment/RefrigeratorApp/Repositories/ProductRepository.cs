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

        private async Task AddProductLog(Guid productId, double quantity)
        {
            await _raContext.ProductLogs.AddAsync(new ProductLog() { Id = Guid.NewGuid(), ProductId = productId, Quantity = quantity, EventTimeStamp = DateTime.Now });
            await _raContext.SaveChangesAsync();
        }

        private async Task<ProductMaster> ManageProductMaster(ProductMaster productMaster)
        {
            ProductMaster existingProduct = await _raContext.ProductMaster.FirstOrDefaultAsync(x => x.Name.Equals(productMaster.Name));
            if (existingProduct == null)
            {
                productMaster.Id = Guid.NewGuid();
                _raContext.ProductMaster.Add(productMaster);
                await _raContext.SaveChangesAsync();
                return productMaster;
            }
            return existingProduct;
        }



        public async Task<string> InsertProduct(ProductMaster productMaster)
        {
            ProductMaster existingproductMaster = await ManageProductMaster(productMaster);

            Product existingProduct = await _raContext.Products.FirstOrDefaultAsync(x => x.ProductId.Equals(existingproductMaster.Id)
                                                                    && x.ExpiryDate.Value.Date.Equals(productMaster.ExpiryDate.Value.Date));
            if (existingProduct == null)
            {
                // add to product log
                _raContext.Products.Add(new Product(productId: existingproductMaster.Id, quantity: productMaster.Quantity, expiryDate: productMaster.ExpiryDate));
            }
            else
            {
                existingProduct.Quantity += productMaster.Quantity;
                _raContext.Entry(existingProduct).State = EntityState.Modified;
            }
            await AddProductLog(existingproductMaster.Id, productMaster.Quantity);
            await _raContext.SaveChangesAsync();
            return string.Empty;
        }

        public async Task<string> ConsumeProduct(ProductMaster productMaster)
        {
            ProductMaster existingproductMaster = await ManageProductMaster(productMaster);

            Product existingProduct = await _raContext.Products.FirstOrDefaultAsync(x => x.ProductId.Equals(existingproductMaster.Id)
                                                                    && x.ExpiryDate.Value.Date.Equals(productMaster.ExpiryDate.Value.Date));
            if (existingProduct == null)
            {
                return "This product is not avaiable!please insert first";
            }
            else
            {
                if (productMaster.Quantity > existingProduct.Quantity)
                {
                    return "Can not consume more quantity than avaiable";
                }
                existingProduct.Quantity = existingProduct.Quantity - productMaster.Quantity;
                _raContext.Entry(existingProduct).State = EntityState.Modified;
            }
            await AddProductLog(existingproductMaster.Id, productMaster.Quantity);
            await _raContext.SaveChangesAsync();
            return string.Empty;
        }

        public async Task<List<ProductMaster>> GetExpiriedProductsAndRemove()
        {
            var products = await (from i in _raContext.Products
                                  join p in _raContext.ProductMaster
                                  on i.ProductId equals p.Id
                                  where i.ExpiryDate.HasValue && i.ExpiryDate.Value.Date <= DateTime.Now.Date
                                  select new ProductMaster()
                                  {
                                      InventoryLogId = i.Id,
                                      Name = p.Name,
                                      Quantity = i.Quantity,
                                      QuantityUnit = p.QuantityUnit,
                                      ExpiryDate = i.ExpiryDate
                                  }).ToListAsync();
            foreach (var product in products)
            {
                Product productInventoryLog = await _raContext.Products.FindAsync(product.InventoryLogId);
                _raContext.Products.Remove(productInventoryLog);
            }
            await _raContext.SaveChangesAsync();
            return products;
        }

        public async Task<List<ProductMaster>> GetNearExpiryProducts()
        {
            var products = await (from i in _raContext.Products
                                  join p in _raContext.ProductMaster
                                  on i.ProductId equals p.Id
                                  where i.ExpiryDate.HasValue && i.ExpiryDate.Value.Date == DateTime.Now.Date.AddDays(1)
                                  select new ProductMaster()
                                  {
                                      Name = p.Name,
                                      Quantity = i.Quantity,
                                      QuantityUnit = p.QuantityUnit,
                                      ExpiryDate = i.ExpiryDate
                                  }).ToListAsync();
            return products;
        }


    }
}
