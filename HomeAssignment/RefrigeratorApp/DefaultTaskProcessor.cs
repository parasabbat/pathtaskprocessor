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
                case 2: // Consume Item
                    Console.WriteLine("Please enter Item Name \n");
                    string? itemName = Console.ReadLine();
                    Console.WriteLine("Quantity to Insert \n");
                    double quantity;
                    while (!double.TryParse(Console.ReadLine(), out quantity))
                    {
                        Console.Write("Invalid Input,Please input again \n");
                    }
                    if (selectedCommand == 2) // consume case , send quanity as negative
                    {
                        quantity = -quantity;
                    }
                    Console.WriteLine("Quantity Unit \n");
                    string? quantityUnit = Console.ReadLine();
                    await _productRepository.AddUpdateProduct(new Entities.Product(name: itemName, currentQuantity: quantity, quantityUnit: quantityUnit));
                    break;
            }
            await Task.Delay(100);
            Console.WriteLine("\nPress any key to exit!");
            Console.ReadKey();
        }
    }
}
