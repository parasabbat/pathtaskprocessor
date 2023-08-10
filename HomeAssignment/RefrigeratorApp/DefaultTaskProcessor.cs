using RefrigeratorApp.Entities;
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
        private const string ConsoleBanner = @"
        
            █▀█ █▀▀ █▀▀ █▀█ █ █▀▀ █▀▀ █▀█ ▄▀█ ▀█▀ █▀█ █▀█   ▄▀█ █▀█ █▀█
            █▀▄ ██▄ █▀░ █▀▄ █ █▄█ ██▄ █▀▄ █▀█ ░█░ █▄█ █▀▄   █▀█ █▀▀ █▀▀
        ";
        private IProductRepository _productRepository;
        public DefaultTaskProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task DoWorkAsync()
        {
            Console.WriteLine(ConsoleBanner);
            await ManageNearExpiryProducts();
            await ManageExpiriedProducts();
            Console.WriteLine("Please Enter operation Number you would like to perform \n");
            var availableCommands = new Dictionary<int, string>()
            {
                {1, "Insert Item" },
                {2, "Consume Item" }
            };

            foreach (var command in availableCommands)
            {
                Console.WriteLine($"{command.Key}-{command.Value} \n");
            }
            int selectedCommand;
            while (!int.TryParse(Console.ReadLine(), out selectedCommand)
                || selectedCommand <= 0
                || selectedCommand > availableCommands.Count)
            {
                Console.Write("Invalid Input,Please input again \n");
            }

            Console.WriteLine($"You have selected {availableCommands[selectedCommand]} Operation");

            switch (selectedCommand)
            {
                case 1: // Insert Item
                    Console.WriteLine("Please enter Item Name \n");
                    string? itemName = Console.ReadLine();
                    Console.WriteLine("Quantity to Insert \n");
                    double quantity;
                    while (!double.TryParse(Console.ReadLine(), out quantity))
                    {
                        Console.Write("Invalid Input,Please input again \n");
                    }
                    DateTime expiryDate = DateTime.MaxValue;

                    Console.WriteLine("Expiry date in MM/dd/yy format \n");
                    while (!DateTime.TryParse(Console.ReadLine(), out expiryDate))
                    {
                        Console.Write("Invalid Input,Please input again \n");
                    }
                    Console.WriteLine("Quantity Unit \n");
                    string? quantityUnit = Console.ReadLine();
                    string errorMessage = await _productRepository.InsertProduct(new Entities.ProductMaster() { Name = itemName, QuantityUnit = quantityUnit, Quantity = quantity, ExpiryDate = expiryDate });
                    break;
                case 2: // Consume Item
                    Console.WriteLine("Please enter Item Name \n");
                    itemName = Console.ReadLine();
                    Console.WriteLine("Quantity to Consume \n");
                    while (!double.TryParse(Console.ReadLine(), out quantity))
                    {
                        Console.Write("Invalid Input,Please input again \n");
                    }
                    expiryDate = DateTime.MaxValue;
                    Console.WriteLine("Expiry date in MM/dd/yy format \n");
                    while (!DateTime.TryParse(Console.ReadLine(), out expiryDate))
                    {
                        Console.Write("Invalid Input,Please input again \n");
                    }
                    Console.WriteLine("Quantity Unit \n");
                    quantityUnit = Console.ReadLine();
                    await _productRepository.ConsumeProduct(new Entities.ProductMaster() { Name = itemName, QuantityUnit = quantityUnit, Quantity = quantity, ExpiryDate = expiryDate });
                    break;
            }

            await Task.Delay(100);
            Console.WriteLine("\nPress any key to exit!");
            Console.ReadKey();
        }

        public async Task ManageNearExpiryProducts()
        {
            List<ProductMaster> nearExpiryProducts = await _productRepository.GetNearExpiryProducts();
            if (nearExpiryProducts.Count > 0)
            {
                Console.WriteLine($"{nearExpiryProducts.Count} product/products is/are near Expiry, Please consume them as soon as Possible!");
                foreach (var product in nearExpiryProducts)
                {
                    Console.WriteLine($"Name - {product.Name} , Quantity - {product.Quantity} {product.QuantityUnit}, Expiry Date - {product.ExpiryDate.Value.ToString("MM/dd/yy")} ");
                }
            }

        }

        public async Task ManageExpiriedProducts()
        {
            List<ProductMaster> exipriedProducts = await _productRepository.GetExpiriedProductsAndRemove();
            if (exipriedProducts.Count > 0)
            {
                Console.WriteLine($"{exipriedProducts.Count} product/products have expired , Please remove them from refrigerator!");
                foreach (var product in exipriedProducts)
                {
                    Console.WriteLine($"Name - {product.Name} , Quantity - {product.Quantity} {product.QuantityUnit}, Expiry Date - {product.ExpiryDate.Value.ToString("MM/dd/yy")} ");

                }

            }

        }
    }
}
