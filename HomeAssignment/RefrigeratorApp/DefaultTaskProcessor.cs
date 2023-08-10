using RefrigeratorApp.Interfaces;
using RefrigeratorApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorApp
{
    public class DefaultTaskProcessor : ITaskProcessor
    {
        private IProductRepository _productRepository;
        public DefaultTaskProcessor(IProductRepository productRepository) 
        { 
            _productRepository = productRepository;
        }
        public async Task DoWorkAsync()
        {
            var product = await _productRepository.AddProduct(new Entities.Product { Id = Guid.NewGuid(), Name ="New product", CurrentQuantity=2,QuantityUnit="KG" });
            var products = await _productRepository.GetProducts();
        }
    }
}
