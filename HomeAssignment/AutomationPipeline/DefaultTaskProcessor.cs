using AutomationPipeline.Interfaces;
using System.Linq.Expressions;

namespace AutomationPipeline
{
    public class DefaultTaskProcessor : ITaskProcessor
    {
        private readonly ICommandService _commandService;
        public DefaultTaskProcessor(ICommandService commandService) 
        {
            _commandService = commandService;
        }
        public void DoWorkAsync()
        {
            Console.WriteLine("Please Enter command Number of your choice from below \n");
            var availableCommands = new Dictionary<int, string>()
            {   
                {1, "Copy File" },
                {2, "Delete File" },
                {3, "Query Folder Files" },
                {4, "Create Folder" },
                {5, "Download File" },
                {6, "Wait" },
                {7, "Search Row Count" }
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

            Console.WriteLine($"You have selected {availableCommands[selectedCommand]} Command");

            switch (selectedCommand)
            {
                case 1:
                    // Copy File
                    Console.WriteLine("Source File Path \n");
                    string? sourceFilePath = Console.ReadLine();
                    Console.WriteLine("Destination File Path \n");
                    string? destinationFilePath = Console.ReadLine();
                    _commandService.FileCopy(sourceFilePath, destinationFilePath);
                    break;
                case 2:
                    // Delete File
                    Console.WriteLine("File Path \n");
                    string? filePath = Console.ReadLine();
                    _commandService.FileDelete(filePath);
                    break;
                case 3:
                    // Query Folder Files
                    break;
                case 4:
                    // Create Folder
                    break;
                case 5:
                    // Download File
                    break;
                case 6:
                    // Wait
                    break;
                case 7:
                    // Search Row Count
                    break;
            }

            Thread.Sleep(100);
            Console.WriteLine("\nPress any key to exit!");
            Console.ReadKey();
        }
    }
}
